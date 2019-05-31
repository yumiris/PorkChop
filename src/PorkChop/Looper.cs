using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Be.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PorkChop
{



    



    public partial class Looper : Form
    {

        string settings_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PorkChop.txt");
        string tagsdir = "";

        public Looper()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            var slFiles = new DirectoryInfo(dir.Text).GetFiles("*.sound", SearchOption.TopDirectoryOnly);

            foreach (var slFile in slFiles)
            {

                int namelength = slFile.FullName.Length;
                
                BatchLoop(slFile.FullName, namelength);



            }

            MessageBox.Show("Processed - "+(slFiles.GetUpperBound(0)+1).ToString() + " files");

        }



        private void BatchLoop (string filename, int nlength)

        {
            string demotag = Path.Combine(Environment.CurrentDirectory, "demo.sound_looping");


            using (var fs = new FileStream(demotag, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var ds = new FileStream(filename+"_looping", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var ms = new MemoryStream())
            using (var bw = new BeBinaryWriter(ms))
            using (var br = new BeBinaryReader(ms))
            {
                fs.CopyTo(ms);
                ms.Position = 0;
                string tagpath = filename.Substring(tagsdir.Length + 1);
                tagpath = tagpath.Substring(0 , tagpath.Length - 6);
                MessageBox.Show(tagpath);
                Int32 namelength = tagpath.Length;
                Byte nan = 0;



                bw.BaseStream.Seek(220, SeekOrigin.Begin);
                bw.Write(namelength);



                bw.BaseStream.Seek(308, SeekOrigin.Begin);
                bw.Write(tagpath);
                bw.Write(nan);

                ms.Position = 0;
                fs.Position = 0;
                ms.CopyTo(ds);
                
            }




        }



        private void Button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog { ShowNewFolderButton = true };

            if (fbd.ShowDialog() == DialogResult.OK) dir.Text = fbd.SelectedPath;
        }

        private void Looper_Load(object sender, EventArgs e)
        {
            ForceMaps();
        }


        private void ForceMaps()
        {
            if (File.Exists(settings_path))
            {
                tagsdir = File.ReadAllText(settings_path);
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




            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;


            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tagsdir = fbd.SelectedPath;


                MessageBox.Show(tagsdir);
                System.IO.File.WriteAllText(settings_path, tagsdir);
            }

            

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            setMapsFolder();
            tagdir.Text = "Tags Folder - " + tagsdir;
        }
    }
}
