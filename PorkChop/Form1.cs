using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PorkChop
{


  
    
















    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        public void Chop(string FHost)
        {
            

            



            var files = Directory.GetFiles(FHost, $"*.sound", SearchOption.AllDirectories);

            

            foreach (var soundtag in files)
            {
                var filename = Path.GetFullPath(soundtag);
                string displaytext = "";

                using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var ms = new MemoryStream())
                using (var bw = new Be.IO.BeBinaryWriter(ms))
                using (var br = new Be.IO.BeBinaryReader(ms))
                {

//                    br.
                    fs.CopyTo(ms);
                    ms.Position = 0;

                    br.BaseStream.Seek(288, SeekOrigin.Begin);

                    displaytext = br.ReadInt32().ToString();

                    ms.Position = 0;
                    ms.CopyTo(fs);



                    MessageBox.Show(displaytext);
                }

                Directory.CreateDirectory(Path.GetDirectoryName(soundtag));

                
            }

            
        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void brwsbtn(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dir.Text = fbd.SelectedPath;

            }
        }

        private void Chop_Click(object sender, EventArgs e)
        {
            Chop(dir.Text);
        }
    }
}
