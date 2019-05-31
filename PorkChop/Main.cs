using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.IO;
using PorkChop.Library;

namespace PorkChop
{
    public partial class PorkChop : Form
    {
        string settings_path = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "PorkChop.txt");


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
            status.Text = "";
        }


        private void brwsbtn(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog {ShowNewFolderButton = true};

            if (fbd.ShowDialog() == DialogResult.OK) dir.Text = fbd.SelectedPath;
        }

        private async void Chop_Click(object sender, EventArgs e)
        {
            var soundsample  = "22050";
            var soundchannel = "1";
            var soundtype    = false;
            var soundsplit   = false;
            var soundtime    = timebox.Text;

            if (kbox44.Checked)
                soundsample = "44100";

            if (cbox2.Checked)
                soundchannel = "2";

            if (sbox.Checked)
                soundsplit = true;

            if (mbox.Checked)
                soundtype = true;

            var files = Directory.GetFiles(dir.Text, "*", SearchOption.AllDirectories);

            status.Text = "Currently Processing...";

            await Task.Run(() =>
                Parallel.ForEach(files, soundfile =>
                {
                    Codec.Encode(new Codec.Configuration
                    {
                        Mp3File    = soundfile,
                        SoundName  = Path.GetFileNameWithoutExtension(soundfile)?.Replace(' ', '_'),
                        SampleRate = soundsample,
                        Channel    = soundchannel,
                        SoundType  = soundtype,
                        Split      = soundsplit,
                        SoundTime  = soundtime
                    });
                })
            );

            status.Text = "Completed - " + files.Count() + " files processed.";

            FinalClean();

        }

        void FinalClean()
        {

            Directory.Delete(Path.Combine(Environment.CurrentDirectory, "DATA"), true);
            Directory.Delete(Path.Combine(Environment.CurrentDirectory, "temp"), true);
            string presentday = DateTime.Now.ToString("MM-dd_HH-mm");
            
            Directory.Move(Path.Combine(Environment.CurrentDirectory, "tags"), Path.Combine(Environment.CurrentDirectory, presentday));
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //
        }


        private void Label1_Click(object sender, EventArgs e)
        {
            //
        }

        private void TagName_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SoundLooperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f2 = new Looper();
            f2.Show();
        }

        private void AboutPorkChopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form f2 = new About();
            f2.Show();
        }
    }
}