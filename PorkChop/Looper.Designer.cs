namespace PorkChop
{
    partial class Looper
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
            this.button1 = new System.Windows.Forms.Button();
            this.dir = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tagdir = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // dir
            // 
            this.dir.Location = new System.Drawing.Point(57, 15);
            this.dir.Name = "dir";
            this.dir.Size = new System.Drawing.Size(199, 20);
            this.dir.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(262, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "Batch";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // tagdir
            // 
            this.tagdir.Location = new System.Drawing.Point(57, 56);
            this.tagdir.Name = "tagdir";
            this.tagdir.Size = new System.Drawing.Size(270, 20);
            this.tagdir.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 22);
            this.button3.TabIndex = 4;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Looper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 88);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tagdir);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dir);
            this.Controls.Add(this.button1);
            this.Name = "Looper";
            this.Text = "Sound Looping Tool";
            this.Load += new System.EventHandler(this.Looper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox dir;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tagdir;
        private System.Windows.Forms.Button button3;
    }
}