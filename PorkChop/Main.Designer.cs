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
            this.label1 = new System.Windows.Forms.Label();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "<3 Hamp";
            // 
            // PorkChop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 70);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.brs);
            this.Controls.Add(this.dir);
            this.Controls.Add(this.chop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PorkChop";
            this.Text = "PorkChop";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chop;
        private System.Windows.Forms.TextBox dir;
        private System.Windows.Forms.Button brs;
        private System.Windows.Forms.Label label1;
    }
}

