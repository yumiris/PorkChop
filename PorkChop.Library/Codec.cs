/**
 * Copyright (c) 2019 Emilian Roman
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
using System.Threading.Tasks;

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
        /// <param name="mp3">
        ///     MP3 file on the filesystem.
        /// </param>
        public static void Encode(string mp3 , string halofolder)
        {
            CheckUp(); /* checks the current env */
            
            Prepare(); /* create directories */
            Execute(); /* encode the mp3 */
            CleanUp(); /* clean up files */
            Compile(); /* executes tool.exe */

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
                    if (!File.Exists(dependency))
                        throw new FileNotFoundException("Dependency not found - " + dependency);
            }

            /**
             * Gracefully create the directories which will be used for the encoding process.
             */

            void Prepare()
            {
                var directories = new List<string>
                {
                    Path.Combine(halofolder, "data"),
                    Path.Combine(Environment.CurrentDirectory, "temp")
                };

                foreach (var directory in directories)
                    Directory.CreateDirectory(directory);
            }

            /**
             * Asynchronously runs the encoding processes.
             */

            async void Execute()
            {
                var processes = new List<Process>
                {
                    new Process
                    {
                        Executable = Path.Combine(Environment.CurrentDirectory, "convcodec.exe"),
                        Arguments  = $"\"{mp3}\" temp.wav /v /R44100 /B16 /C2 /#"
                    },
                    new Process
                    {
                        Executable = Path.Combine(Environment.CurrentDirectory, "conv.exe"),
                        Arguments  = "-of 44100 -oc2 -ob16 -idel temp.wav temp\\temp.ogg"
                    },
                    new Process
                    {
                        Executable = Path.Combine(Environment.CurrentDirectory, "premu.exe"),
                        Arguments  = "-o@n -d temp -t 0.30 temp\\temp.ogg"
                    },
                    new Process
                    {
                        Executable = Path.Combine(Environment.CurrentDirectory, "conv.exe"),
                        Arguments  = "-llw CONVLIST.LST -of 44100 -oc2 -ob16  -idel"
                    }
                };

                foreach (var process in processes)
                    await Task.Run(() => { process.Start().WaitForExit(); });
            }

            /**
             * Cleans up temporary files and moves the encoded WAV.
             */

            void CleanUp()
            {
                var tempDir  = Path.Combine(Environment.CurrentDirectory, "temp");
                var dataDir  = Path.Combine(halofolder, "DATA\\PORKCHOP");
                var wavFiles = new DirectoryInfo(tempDir).GetFiles("*.wav", SearchOption.TopDirectoryOnly);
                var oggFiles = new DirectoryInfo(tempDir).GetFiles("*.ogg", SearchOption.TopDirectoryOnly);

                foreach (var wavFile in wavFiles)
                {
                    File.Copy(wavFile.FullName, Path.Combine(dataDir, wavFile.Name));
                    File.Delete(wavFile.FullName);
                }

                foreach (var oggFile in oggFiles)
                    File.Delete(oggFile.FullName);

                File.Delete("temp.mp3");
                File.Delete(Path.Combine(tempDir, "temp.ogg"));
            }

            /**
             * Invokes tool.exe for compiling the sounds in the data directory.
             */

            async void Compile()
            {
                await Task.Run(() =>
                {
                    new Process
                    {
                        Executable = Path.Combine(halofolder, "tool.exe"),
                        Arguments  = "sounds DATA\\PORKCHOP ogg 1"
                    }.Start().WaitForExit();
                });
            }
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
                    
                    FileName    = Executable,
                    Arguments   = Arguments
                });
            }
        }
    }
}