﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Be.IO;

namespace PorkChop
{
    public partial class PorkChop : Form
    {
        public PorkChop()
        {
            InitializeComponent();
        }

        public void BatchChop(string FHost)
        {
            var files = Directory.GetFiles(FHost, "*.sound", SearchOption.AllDirectories);

            foreach (var soundtag in files)
            {
                var filename = Path.GetFullPath(soundtag);

                int permutation_count;

                using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var ms = new MemoryStream())
                using (var bw = new BeBinaryWriter(ms))
                using (var br = new BeBinaryReader(ms))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;

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

                    ms.Position = 0;
                    fs.Position = 0;
                    ms.CopyTo(fs);
                }
            }

            MessageBox.Show("Converted " + files.Length + " files");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void brwsbtn(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog() == DialogResult.OK) dir.Text = fbd.SelectedPath;
        }

        private void Chop_Click(object sender, EventArgs e)
        {
            BatchChop(dir.Text);
        }
    }
}