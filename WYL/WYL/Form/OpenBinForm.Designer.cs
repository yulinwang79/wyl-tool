namespace WYL
{
    partial class OpenBinForm
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
            this.btOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMTK6223 = new System.Windows.Forms.RadioButton();
            this.rbMTK6225 = new System.Windows.Forms.RadioButton();
            this.rbMTKOthers = new System.Windows.Forms.RadioButton();
            this.rbSpreadtrum = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(229, 109);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(75, 23);
            this.btOpen.TabIndex = 0;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSpreadtrum);
            this.groupBox1.Controls.Add(this.rbMTKOthers);
            this.groupBox1.Controls.Add(this.rbMTK6225);
            this.groupBox1.Controls.Add(this.rbMTK6223);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 149);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Platform";
            // 
            // rbMTK6223
            // 
            this.rbMTK6223.AutoSize = true;
            this.rbMTK6223.Location = new System.Drawing.Point(23, 20);
            this.rbMTK6223.Name = "rbMTK6223";
            this.rbMTK6223.Size = new System.Drawing.Size(65, 16);
            this.rbMTK6223.TabIndex = 0;
            this.rbMTK6223.TabStop = true;
            this.rbMTK6223.Text = "MTK6223";
            this.rbMTK6223.UseVisualStyleBackColor = true;
            // 
            // rbMTK6225
            // 
            this.rbMTK6225.AutoSize = true;
            this.rbMTK6225.Checked = true;
            this.rbMTK6225.Location = new System.Drawing.Point(23, 48);
            this.rbMTK6225.Name = "rbMTK6225";
            this.rbMTK6225.Size = new System.Drawing.Size(65, 16);
            this.rbMTK6225.TabIndex = 1;
            this.rbMTK6225.TabStop = true;
            this.rbMTK6225.Text = "MTK6225";
            this.rbMTK6225.UseVisualStyleBackColor = true;
            // 
            // rbMTKOthers
            // 
            this.rbMTKOthers.AutoSize = true;
            this.rbMTKOthers.Location = new System.Drawing.Point(23, 79);
            this.rbMTKOthers.Name = "rbMTKOthers";
            this.rbMTKOthers.Size = new System.Drawing.Size(83, 16);
            this.rbMTKOthers.TabIndex = 2;
            this.rbMTKOthers.TabStop = true;
            this.rbMTKOthers.Text = "MTK Others";
            this.rbMTKOthers.UseVisualStyleBackColor = true;
            // 
            // rbSpreadtrum
            // 
            this.rbSpreadtrum.AutoSize = true;
            this.rbSpreadtrum.Location = new System.Drawing.Point(23, 111);
            this.rbSpreadtrum.Name = "rbSpreadtrum";
            this.rbSpreadtrum.Size = new System.Drawing.Size(83, 16);
            this.rbSpreadtrum.TabIndex = 3;
            this.rbSpreadtrum.TabStop = true;
            this.rbSpreadtrum.Text = "Spreadtrum";
            this.rbSpreadtrum.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(229, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // OpenBinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 168);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOpen);
            this.Name = "OpenBinForm";
            this.Text = "OpenBinForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbMTKOthers;
        private System.Windows.Forms.RadioButton rbMTK6225;
        private System.Windows.Forms.RadioButton rbMTK6223;
        private System.Windows.Forms.RadioButton rbSpreadtrum;
        private System.Windows.Forms.Button button1;
    }
}