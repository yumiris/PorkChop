namespace PorkChop
{
    partial class PorkChop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PorkChop));
            this.chop = new System.Windows.Forms.Button();
            this.dir = new System.Windows.Forms.TextBox();
            this.brs = new System.Windows.Forms.Button();
            this.kbox44 = new System.Windows.Forms.RadioButton();
            this.kbox22 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbox1 = new System.Windows.Forms.RadioButton();
            this.cbox2 = new System.Windows.Forms.RadioButton();
            this.sbox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.timebox = new System.Windows.Forms.MaskedTextBox();
            this.status = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chop
            // 
            this.chop.Location = new System.Drawing.Point(142, 106);
            this.chop.Name = "chop";
            this.chop.Size = new System.Drawing.Size(100, 23);
            this.chop.TabIndex = 0;
            this.chop.Text = "Convert Sounds";
            this.chop.UseVisualStyleBackColor = true;
            this.chop.Click += new System.EventHandler(this.Chop_Click);
            // 
            // dir
            // 
            this.dir.Location = new System.Drawing.Point(52, 9);
            this.dir.Name = "dir";
            this.dir.Size = new System.Drawing.Size(190, 20);
            this.dir.TabIndex = 2;
            // 
            // brs
            // 
            this.brs.Location = new System.Drawing.Point(12, 8);
            this.brs.Name = "brs";
            this.brs.Size = new System.Drawing.Size(34, 22);
            this.brs.TabIndex = 3;
            this.brs.Text = "...";
            this.brs.UseVisualStyleBackColor = true;
            this.brs.Click += new System.EventHandler(this.brwsbtn);
            // 
            // kbox44
            // 
            this.kbox44.AutoSize = true;
            this.kbox44.Checked = true;
            this.kbox44.Location = new System.Drawing.Point(6, 42);
            this.kbox44.Name = "kbox44";
            this.kbox44.Size = new System.Drawing.Size(69, 17);
            this.kbox44.TabIndex = 5;
            this.kbox44.TabStop = true;
            this.kbox44.Text = "44100 hz";
            this.kbox44.UseVisualStyleBackColor = true;
            // 
            // kbox22
            // 
            this.kbox22.AutoSize = true;
            this.kbox22.Location = new System.Drawing.Point(6, 19);
            this.kbox22.Name = "kbox22";
            this.kbox22.Size = new System.Drawing.Size(69, 17);
            this.kbox22.TabIndex = 6;
            this.kbox22.Text = "22050 hz";
            this.kbox22.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kbox44);
            this.groupBox1.Controls.Add(this.kbox22);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(78, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Rate ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbox1);
            this.groupBox2.Controls.Add(this.cbox2);
            this.groupBox2.Location = new System.Drawing.Point(96, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(78, 65);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Channels ";
            // 
            // cbox1
            // 
            this.cbox1.AutoSize = true;
            this.cbox1.Location = new System.Drawing.Point(6, 19);
            this.cbox1.Name = "cbox1";
            this.cbox1.Size = new System.Drawing.Size(67, 17);
            this.cbox1.TabIndex = 5;
            this.cbox1.Text = "1 - Mono";
            this.cbox1.UseVisualStyleBackColor = true;
            // 
            // cbox2
            // 
            this.cbox2.AutoSize = true;
            this.cbox2.Checked = true;
            this.cbox2.Location = new System.Drawing.Point(6, 42);
            this.cbox2.Name = "cbox2";
            this.cbox2.Size = new System.Drawing.Size(71, 17);
            this.cbox2.TabIndex = 6;
            this.cbox2.TabStop = true;
            this.cbox2.Text = "2 - Stereo";
            this.cbox2.UseVisualStyleBackColor = true;
            // 
            // sbox
            // 
            this.sbox.AutoSize = true;
            this.sbox.Checked = true;
            this.sbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sbox.Location = new System.Drawing.Point(6, 18);
            this.sbox.Name = "sbox";
            this.sbox.Size = new System.Drawing.Size(46, 17);
            this.sbox.TabIndex = 9;
            this.sbox.Text = "Split";
            this.sbox.UseVisualStyleBackColor = true;
            this.sbox.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.timebox);
            this.groupBox3.Controls.Add(this.sbox);
            this.groupBox3.Location = new System.Drawing.Point(180, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(62, 65);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Length ";
            // 
            // timebox
            // 
            this.timebox.Location = new System.Drawing.Point(6, 39);
            this.timebox.Mask = "9990.00";
            this.timebox.Name = "timebox";
            this.timebox.Size = new System.Drawing.Size(46, 20);
            this.timebox.TabIndex = 13;
            this.timebox.Text = "000030";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(9, 130);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 13);
            this.status.TabIndex = 10;
            // 
            // PorkChop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 144);
            this.Controls.Add(this.status);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.brs);
            this.Controls.Add(this.dir);
            this.Controls.Add(this.chop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PorkChop";
            this.Text = "PorkChop";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chop;
        private System.Windows.Forms.TextBox dir;
        private System.Windows.Forms.Button brs;
        private System.Windows.Forms.RadioButton kbox44;
        private System.Windows.Forms.RadioButton kbox22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton cbox1;
        private System.Windows.Forms.RadioButton cbox2;
        private System.Windows.Forms.CheckBox sbox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox timebox;
        private System.Windows.Forms.Label status;
    }
}

