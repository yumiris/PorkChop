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


  
    
















    public partial class PorkChop : Form
    {
        public PorkChop()
        {
            InitializeComponent();
        }



        public void BatchChop(string FHost)
        {
            

            



            var files = Directory.GetFiles(FHost, $"*.sound", SearchOption.AllDirectories);

            

            foreach (var soundtag in files)
            {
                var filename = Path.GetFullPath(soundtag);
                
                int permutation_count = 0;

                MessageBox.Show(filename);

                using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var ms = new MemoryStream())
                using (var bw = new Be.IO.BeBinaryWriter(ms))
                using (var br = new Be.IO.BeBinaryReader(ms))
                {

//                    br.
                    fs.CopyTo(ms);
                    ms.Position = 0;


                    bw.BaseStream.Seek(64, SeekOrigin.Begin);
                    bw.Write(2);

                    

                    br.BaseStream.Seek(288, SeekOrigin.Begin);
                    permutation_count = br.ReadInt32();
                    MessageBox.Show(permutation_count.ToString());

                   

                    bw.BaseStream.Seek(342, SeekOrigin.Begin);
                    for (int i = 0; i < permutation_count; i++)
                    {

                        if (i != (permutation_count-1))
                        {
                            bw.Write((Int16)(i + 1));
                        }
                        else
                        {
                            bw.Write((Int16)(-1));
                        }
                        
                        bw.BaseStream.Seek(122, SeekOrigin.Current);

                        MessageBox.Show(permutation_count.ToString() + "--" + i.ToString());

                    }

                    

                    ms.Position = 0;
                    fs.Position = 0;
                    ms.CopyTo(fs);

                    MessageBox.Show("Done");

                    
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
            BatchChop(dir.Text);
        }
    }
}
