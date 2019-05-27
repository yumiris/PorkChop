namespace PorkChop
{
    partial class Form1
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
            this.chop = new System.Windows.Forms.Button();
            this.backup = new System.Windows.Forms.CheckBox();
            this.dir = new System.Windows.Forms.TextBox();
            this.brs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chop
            // 
            this.chop.Location = new System.Drawing.Point(160, 39);
            this.chop.Name = "chop";
            this.chop.Size = new System.Drawing.Size(90, 23);
            this.chop.TabIndex = 0;
            this.chop.Text = "Chop Sounds";
            this.chop.UseVisualStyleBackColor = true;
            this.chop.Click += new System.EventHandler(this.Chop_Click);
            // 
            // backup
            // 
            this.backup.AutoSize = true;
            this.backup.Location = new System.Drawing.Point(67, 43);
            this.backup.Name = "backup";
            this.backup.Size = new System.Drawing.Size(87, 17);
            this.backup.TabIndex = 1;
            this.backup.Text = "Backup Files";
            this.backup.UseVisualStyleBackColor = true;
            // 
            // dir
            // 
            this.dir.Location = new System.Drawing.Point(12, 10);
            this.dir.Name = "dir";
            this.dir.Size = new System.Drawing.Size(198, 20);
            this.dir.TabIndex = 2;
            // 
            // brs
            // 
            this.brs.Location = new System.Drawing.Point(216, 9);
            this.brs.Name = "brs";
            this.brs.Size = new System.Drawing.Size(34, 22);
            this.brs.TabIndex = 3;
            this.brs.Text = "...";
            this.brs.UseVisualStyleBackColor = true;
            this.brs.Click += new System.EventHandler(this.brwsbtn);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 70);
            this.Controls.Add(this.brs);
            this.Controls.Add(this.dir);
            this.Controls.Add(this.backup);
            this.Controls.Add(this.chop);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chop;
        private System.Windows.Forms.CheckBox backup;
        private System.Windows.Forms.TextBox dir;
        private System.Windows.Forms.Button brs;
    }
}

