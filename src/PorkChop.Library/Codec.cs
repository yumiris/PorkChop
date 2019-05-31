/**
 * Copyright (c) 2019 Emilian Roman, DeadHamster35
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Be.IO;
using static System.Console;

namespace PorkChop.Library
{
    /// <summary>
    ///     Splits and encodes an inbound MP3 file.
    /// </summary>
    public static class Codec
    {
        /// <summary>
        ///     Split and encodes an inbound MP3 file.
        /// </summary>
        /// <param name="configuration">
        ///     Configuration used for the encoding routine.
        /// </param>
        public static void Encode(Configuration configuration)
        {
            var mp3        = configuration.Mp3File;    /* for compatibility with below code */
            var soundname  = configuration.SoundName;  /* for compatibility with below code */
            var samplerate = configuration.SampleRate; /* for compatibility with below code */
            var channel    = configuration.Channel;    /* for compatibility with below code */
            var stype      = configuration.SoundType;  /* for compatibility with below code */
            var split      = configuration.Split;      /* for compatibility with below code */
            var stime      = configuration.SoundTime;  /* for compatibility with below code */
            var temp       = Guid.NewGuid();           /* random string to avoid collisions */

            WriteLine("Initiate encoding process");
            CheckUp(); /* checks the current env */
            Prepare(); /* create directories */
            Execute(); /* encode the mp3 */
//            CleanUp(); /* clean up files */
            Compile(); /* executes tool.exe */
            TagEdit(); /* sets up the tag for split parts */

            WriteLine("Finished encoding process");

            /**
             * Verify that the current environment meets the dependency requirements.
             */

            void CheckUp()
            {
                var dependencies = new List<string>
                {
                    Path.Combine(Environment.CurrentDirectory, "convcodec.exe"),
                    Path.Combine(Environment.CurrentDirectory, "conv.exe"),
                    Path.Combine(Environment.CurrentDirectory, "premu.exe")
                };

                foreach (var dependency in dependencies)
                    if (File.Exists(dependency))
                        WriteLine("CHECKUP: Dependency found - " + dependency);
                    else
                        throw new FileNotFoundException("Dependency not found - " + dependency);
            }

            /**
             * Gracefully create the directories which will be used for the encoding process.
             */

            void Prepare()
            {
                var directories = new List<string>
                {
                    Path.Combine(Environment.CurrentDirectory, "data", soundname),
                    Path.Combine(Environment.CurrentDirectory, "temp")
                };

                foreach (var directory in directories)
                {
                    Directory.CreateDirectory(directory);
                    WriteLine("PREPARE: Created directory - " + directory);
                }
            }

            /**
             * Asynchronously runs the encoding processes.
             */

            void Execute()
            {
                var tempfolder = temp.ToString();

                MakeList(tempfolder);

                var processes = split
                    ? new List<Process> /* split up sound */
                    {
                        new Process
                        {
                            Executable = Path.Combine(Environment.CurrentDirectory, "convcodec.exe"),
                            Arguments  = $"\"{mp3}\" {temp}.wav /v /R{samplerate} /B16 /C{channel} /#"
                        },
                        new Process
                        {
                            Executable = Path.Combine(Environment.CurrentDirectory, "conv.exe"),
                            Arguments =
                                $"-of {samplerate} -oc{channel} -ob16 -idel {temp}.wav temp\\{tempfolder}\\{temp}.ogg"
                        },
                        new Process
                        {
                            Executable = Path.Combine(Environment.CurrentDirectory, "premu.exe"),
                            Arguments  = $"-o@n -d temp\\{tempfolder} -t {stime} temp\\{tempfolder}\\{temp}.ogg"
                        },
                        new Process
                        {
                            Executable = Path.Combine(Environment.CurrentDirectory, "conv.exe"),
                            Arguments =
                                $"-llw temp\\{tempfolder}\\CONVLIST.LST -of {samplerate} -oc{channel} -ob16  -idel"
                        }
                    }
                    : new List<Process> /* straight sound */
                    {
                        new Process
                        {
                            Executable = Path.Combine(Environment.CurrentDirectory, "convcodec.exe"),
                            Arguments  = $"\"{mp3}\" {temp}.wav /v /R{samplerate} /B16 /C{channel} /#"
                        },
                        new Process
                        {
                            Executable = Path.Combine(Environment.CurrentDirectory, "conv.exe"),
                            Arguments =
                                $"-of {samplerate} -oc{channel} -ob16 -idel {temp}.wav temp\\{tempfolder}\\{temp}.wav"
                        }
                    };

                foreach (var process in processes)
                {
                    WriteLine("EXECUTE: Initiate - " + process.Executable);
                    process.Start().WaitForExit();
                    WriteLine("EXECUTE: Finished - " + process.Executable);
                }

                CleanUp(tempfolder);
            }

            void MakeList(string listfolder)
            {
                var listdir  = Path.Combine(Environment.CurrentDirectory, "temp", listfolder);
                var listfile = Path.Combine(listdir,                      "CONVLIST.LST");

                var writetext = "";

                Directory.CreateDirectory(listdir);
                File.WriteAllText(listfile, "");

                for (var i = 0; i != 60; i++)
                {
                    writetext = "temp\\" + listfolder + "\\" + i.ToString("000") + ".ogg" + Environment.NewLine;
                    File.AppendAllText(listfile, writetext);
                }
            }

            /**
             * Cleans up temporary files and moves the encoded WAV.
             */

            void CleanUp(string oggfolder)
            {
                var tempDir  = Path.Combine(Environment.CurrentDirectory, "temp", oggfolder);
                var dataDir  = Path.Combine(Environment.CurrentDirectory, "DATA", soundname);
                var wavFiles = new DirectoryInfo(tempDir).GetFiles("*.wav", SearchOption.TopDirectoryOnly);
                var oggFiles = new DirectoryInfo(tempDir).GetFiles("*.ogg", SearchOption.TopDirectoryOnly);

                foreach (var wavFile in wavFiles)
                {
                    File.Move(wavFile.FullName, Path.Combine(dataDir, wavFile.Name));
                    WriteLine("CLEANUP: Moved WAV - " + Path.Combine(dataDir, wavFile.Name));
                }

                foreach (var oggFile in oggFiles)
                {
                    File.Delete(oggFile.FullName);
                    WriteLine("CLEANUP: Deleted OGG - " + oggFile.Name);
                }

                File.Delete($"{temp}.mp3");
                File.Delete(Path.Combine(tempDir, $"{temp}.ogg"));

                WriteLine("CLEANUP: Deleted remaining data");
            }

            /**
             * Invokes tool.exe for compiling the sounds in the data directory.
             */

            void Compile()
            {
                new Process
                {
                    Executable = Path.Combine(Environment.CurrentDirectory, "tool.exe"),
                    Arguments  = "sounds " + soundname + " ogg 1"
                }.Start().WaitForExit();
            }

            /**
             * Splits up the audio data.
             */

            void TagEdit()
            {
                var filename = Path.Combine(Environment.CurrentDirectory, "TAGS", soundname + ".sound");

                int permutation_count;

                using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var ms = new MemoryStream())
                using (var bw = new BeBinaryWriter(ms))
                using (var br = new BeBinaryReader(ms))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;

                    bw.BaseStream.Seek(68, SeekOrigin.Begin);
                    bw.Write(stype ? (short) 32 : (short) 4); /* IDtype */

                    if (split)
                    {
                        bw.BaseStream.Seek(64, SeekOrigin.Begin);
                        bw.Write(2);

                        br.BaseStream.Seek(288, SeekOrigin.Begin);
                        permutation_count = br.ReadInt32();

                        bw.BaseStream.Seek(342, SeekOrigin.Begin);

                        for (var i = 0; i < permutation_count; i++)
                        {
                            if (i != permutation_count - 1)
                                bw.Write((short) (i + 1));
                            else
                                bw.Write((short) -1);

                            bw.BaseStream.Seek(122, SeekOrigin.Current);
                        }
                    }

                    ms.Position = 0;
                    fs.Position = 0;
                    ms.CopyTo(fs);
                }
            }
        }

        public class Configuration
        {
            public string Mp3File    { get; set; }
            public string SoundName  { get; set; }
            public string SampleRate { get; set; }
            public string Channel    { get; set; }
            public bool   SoundType  { get; set; }
            public bool   Split      { get; set; }
            public string SoundTime  { get; set; }
        }

        /// <summary>
        ///     Simple wrapper around the Diagnostics Process.
        /// </summary>
        public class Process
        {
            public string Executable { get; set; } /* executable to invoke */
            public string Arguments  { get; set; } /* arguments to pass onto the specified executable */

            /// <summary>
            ///     Invokes the inbound executable with the inbound arguments.
            /// </summary>
            /// <remarks>
            ///     The executable will be invoked silently/in the background.
            /// </remarks>
            public System.Diagnostics.Process Start()
            {
                return System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName    = Executable,
                    Arguments   = Arguments
                });
            }
        }
    }
}