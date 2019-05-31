/**
 * Copyright (c) 2019 DeadHamster35
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
using System.Text;
using System.Windows.Forms;
using Be.IO;

namespace PorkChop
{
    public partial class Looper : Form
    {
        private readonly string settings_path =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PorkChop.txt");

        private string tagsdir = "";

        public Looper()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var slFiles = new DirectoryInfo(dir.Text).GetFiles("*.sound", SearchOption.TopDirectoryOnly);

            foreach (var slFile in slFiles)
            {
                var namelength = slFile.FullName.Length;

                BatchLoop(slFile.FullName, namelength);
            }

            MessageBox.Show("Processed - " + (slFiles.GetUpperBound(0) + 1) + " files");
        }


        private void BatchLoop(string filename, int nlength)
        {
            var demotag = Path.Combine(Environment.CurrentDirectory, "demo.sound_looping");

            using (var fs = new FileStream(demotag, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var ds = new FileStream(filename + "_looping", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var ms = new MemoryStream())
            using (var bw = new BeBinaryWriter(ms))
            using (var br = new BeBinaryReader(ms))
            {
                fs.CopyTo(ms);
                ms.Position = 0;

                var tagpath = filename.Substring(tagsdir.Length + 1);
                tagpath = tagpath.Substring(0, tagpath.Length - 6);

                MessageBox.Show(tagpath);

                var  namelength = tagpath.Length;
                byte nan        = 0;

                bw.BaseStream.Seek(220, SeekOrigin.Begin);
                bw.Write(namelength);

                bw.BaseStream.Seek(308, SeekOrigin.Begin);
                bw.Write(Encoding.ASCII.GetBytes(tagpath));
                bw.Write(nan);

                ms.Position = 0;
                fs.Position = 0;
                ms.CopyTo(ds);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog {ShowNewFolderButton = true};

            if (fbd.ShowDialog() == DialogResult.OK)
                dir.Text = fbd.SelectedPath;
        }

        private void Looper_Load(object sender, EventArgs e)
        {
            ForceMaps();
        }

        private void ForceMaps()
        {
            if (File.Exists(settings_path))
            {
                tagsdir     = File.ReadAllText(settings_path);
                tagdir.Text = "Tags Folder - " + tagsdir;
            }
            else
            {
                MessageBox.Show("Please select your Maps Folder");
                setMapsFolder();
                ForceMaps();
            }
        }

        private void setMapsFolder()
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tagsdir = fbd.SelectedPath;

                MessageBox.Show(tagsdir);
                File.WriteAllText(settings_path, tagsdir);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            setMapsFolder();
            tagdir.Text = "Tags Folder - " + tagsdir;
        }
    }
}