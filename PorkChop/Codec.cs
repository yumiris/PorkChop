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
using System.IO;
using System.Threading.Tasks;
using static System.IO.SearchOption;

namespace PorkChop
{
    /// <summary>
    ///     Splits and encodes an inbound MP3 file.
    /// </summary>
    public class Codec
    {
        /// <summary>
        ///     Split and encodes an inbound MP3 file.
        /// </summary>
        /// <param name="mp3">
        ///     MP3 file on the filesystem.
        /// </param>
        public async void Encode(string mp3)
        {
            Prepare(); /* create directories */
            Execute(); /* encode the mp3 */
            CleanUp(); /* clean up files */
            Compile(); /* executes tool.exe */

            /**
             * Gracefully create the directories which will be used for the encoding process.
             */
            void Prepare()
            {
                var directories = new List<string>
                {
                    Path.Combine(Environment.CurrentDirectory, "data"),
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
                        Executable = "convcodec.exe",
                        Arguments  = $"\"{mp3}\" temp.wav /v /R44100 /B16 /C2 /#"
                    },
                    new Process
                    {
                        Executable = "conv.exe",
                        Arguments  = "-of 44100 -oc2 -ob16 -idel temp.wav temp\\temp.ogg"
                    },
                    new Process
                    {
                        Executable = "premu.exe",
                        Arguments  = "-o@n -d temp -t 0.30 temp\\temp.ogg"
                    },
                    new Process
                    {
                        Executable = "conv.exe",
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
                var dataDir  = Path.Combine(Environment.CurrentDirectory, "data");
                var wavFiles = new DirectoryInfo(tempDir).GetFiles("*.wav", TopDirectoryOnly);
                var oggFiles = new DirectoryInfo(tempDir).GetFiles("*.ogg", TopDirectoryOnly);

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
                        Executable = "tool.exe",
                        Arguments  = "sounds data ogg 1"
                    }.Start().WaitForExit();
                });
            }
        }
    }
}