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
using System.IO;
using PorkChop.Library;

namespace PorkChop.CLI
{
    /// <summary>
    ///   CLI for PorkChop.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
                Exit(1, "Not enough args.");

            var mp3 = args[0];
            var dir = args[1];

            if (!File.Exists(mp3))
                Exit(2, "MP3 not found.");

            if (!Directory.Exists(dir))
                Exit(3, "Directory not found.");

            try
            {
                Codec.Encode(mp3 ,dir);
            }
            catch (Exception e)
            {
                Exit(4, e.Message);
            }

            void Exit(int code, string message)
            {
                Console.WriteLine(message);
                Environment.Exit(code);
            }
        }
    }
}