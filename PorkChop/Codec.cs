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

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Diagnostics.ProcessWindowStyle;
using static System.Environment;
using static System.IO.File;
using static System.IO.Path;
using static System.IO.SearchOption;
using static System.Threading.Tasks.Task;

namespace PorkChop
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
        public static async void Encode(string mp3)
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
                    Combine(CurrentDirectory, "convcodec.exe"),
                    Combine(CurrentDirectory, "conv.exe"),
                    Combine(CurrentDirectory, "premu.exe")
                };

                foreach (var dependency in dependencies)
                    if (!Exists(dependency))
                        throw new FileNotFoundException("Dependency not found - " + dependency);
            }

            /**
             * Gracefully create the directories which will be used for the encoding process.
             */

            void Prepare()
            {
                var directories = new List<string>
                {
                    Combine(CurrentDirectory, "data"),
                    Combine(CurrentDirectory, "temp")
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
                        Executable = Combine(CurrentDirectory, "convcodec.exe"),
                        Arguments  = $"\"{mp3}\" temp.wav /v /R44100 /B16 /C2 /#"
                    },
                    new Process
                    {
                        Executable = Combine(CurrentDirectory, "conv.exe"),
                        Arguments  = "-of 44100 -oc2 -ob16 -idel temp.wav temp\\temp.ogg"
                    },
                    new Process
                    {
                        Executable = Combine(CurrentDirectory, "premu.exe"),
                        Arguments  = "-o@n -d temp -t 0.30 temp\\temp.ogg"
                    },
                    new Process
                    {
                        Executable = Combine(CurrentDirectory, "conv.exe"),
                        Arguments  = "-llw CONVLIST.LST -of 44100 -oc2 -ob16  -idel"
                    }
                };

                foreach (var process in processes)
                    await Run(() => { process.Start().WaitForExit(); });
            }

            /**
             * Cleans up temporary files and moves the encoded WAV.
             */

            void CleanUp()
            {
                var tempDir  = Combine(CurrentDirectory, "temp");
                var dataDir  = Combine(CurrentDirectory, "data");
                var wavFiles = new DirectoryInfo(tempDir).GetFiles("*.wav", TopDirectoryOnly);
                var oggFiles = new DirectoryInfo(tempDir).GetFiles("*.ogg", TopDirectoryOnly);

                foreach (var wavFile in wavFiles)
                {
                    Copy(wavFile.FullName, Combine(dataDir, wavFile.Name));
                    Delete(wavFile.FullName);
                }

                foreach (var oggFile in oggFiles)
                    Delete(oggFile.FullName);

                Delete("temp.mp3");
                Delete(Combine(tempDir, "temp.ogg"));
            }

            /**
             * Invokes tool.exe for compiling the sounds in the data directory.
             */

            async void Compile()
            {
                await Run(() =>
                {
                    new Process
                    {
                        Executable = Combine(CurrentDirectory, "tool.exe"),
                        Arguments  = "sounds data ogg 1"
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
                    WindowStyle = Hidden,
                    FileName    = Executable,
                    Arguments   = Arguments
                });
            }
        }
    }
}