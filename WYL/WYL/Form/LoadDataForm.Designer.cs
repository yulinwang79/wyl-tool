namespace WYL
{
    partial class LoadDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadDataForm));
            this.label1 = new System.Windows.Forms.Label();
            this.rtbCustFontData = new System.Windows.Forms.RichTextBox();
            this.rtbRangeData = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbWidth = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbOffset = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbData = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btCreate = new System.Windows.Forms.Button();
            this.rtbDWidth = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "sCustFontData：";
            // 
            // rtbCustFontData
            // 
            this.rtbCustFontData.Location = new System.Drawing.Point(14, 24);
            this.rtbCustFontData.Name = "rtbCustFontData";
            this.rtbCustFontData.Size = new System.Drawing.Size(627, 41);
            this.rtbCustFontData.TabIndex = 1;
            this.rtbCustFontData.Text = resources.GetString("rtbCustFontData.Text");
            // 
            // rtbRangeData
            // 
            this.rtbRangeData.Location = new System.Drawing.Point(14, 83);
            this.rtbRangeData.Name = "rtbRangeData";
            this.rtbRangeData.Size = new System.Drawing.Size(627, 43);
            this.rtbRangeData.TabIndex = 3;
            this.rtbRangeData.Text = "{\n { 35,35},\n    { 42,44},\n        {48,59},\n            {65,65},\n                " +
                "{77,77},\n                    {80,80},{87,87},{112,112},{119,119},}\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "RangeData：";
            // 
            // rtbWidth
            // 
            this.rtbWidth.Location = new System.Drawing.Point(12, 148);
            this.rtbWidth.Name = "rtbWidth";
            this.rtbWidth.Size = new System.Drawing.Size(627, 100);
            this.rtbWidth.TabIndex = 5;
            this.rtbWidth.Text = "{\n\n0x26,0x22,0x23,0x1E,0x24,0x22,0x24,0x24,0x24,0x24,0x24,0x24,0x24,0x24,0x22,0x1" +
                "E,\n0x26,0x32,0x24,0x34,0x23,0x3C,}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width：";
            // 
            // rtbOffset
            // 
            this.rtbOffset.Location = new System.Drawing.Point(12, 415);
            this.rtbOffset.Name = "rtbOffset";
            this.rtbOffset.Size = new System.Drawing.Size(627, 100);
            this.rtbOffset.TabIndex = 7;
            this.rtbOffset.Text = "{\n\n0x0000,0x0106,0x01F0,0x02E1,0x03B0,0x04A8,0x0592,0x068A,0x0782,0x087A,0x0972,0" +
                "x0A6A,0x0B62,0x0C5A,0x0D52,0x0E3C,\n0x0F0B,0x1011,0x1169,0x1261,0x13C7,0x14B8,0x1" +
                "655,}";
            this.rtbOffset.TextChanged += new System.EventHandler(this.richTextBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Offset：";
            // 
            // rtbData
            // 
            this.rtbData.Location = new System.Drawing.Point(12, 533);
            this.rtbData.Name = "rtbData";
            this.rtbData.Size = new System.Drawing.Size(627, 144);
            this.rtbData.TabIndex = 9;
            this.rtbData.Text = resources.GetString("rtbData.Text");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 518);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data：";
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(566, 683);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(75, 23);
            this.btCreate.TabIndex = 10;
            this.btCreate.Text = "Load";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // rtbDWidth
            // 
            this.rtbDWidth.Location = new System.Drawing.Point(12, 276);
            this.rtbDWidth.Name = "rtbDWidth";
            this.rtbDWidth.Size = new System.Drawing.Size(627, 111);
            this.rtbDWidth.TabIndex = 12;
            this.rtbDWidth.Text = "{\n\n0x26,0x22,0x23,0x1E,0x24,0x22,0x24,0x24,0x24,0x24,0x24,0x24,0x24,0x24,0x22,0x1" +
                "E,\n0x26,0x32,0x24,0x34,0x23,0x3C,}";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "DWidth：";
            // 
            // LoadDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 710);
            this.Controls.Add(this.rtbDWidth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btCreate);
            this.Controls.Add(this.rtbData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtbOffset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtbWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtbRangeData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtbCustFontData);
            this.Controls.Add(this.label1);
            this.Name = "LoadDataForm";
            this.Text = "Load Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbCustFontData;
        private System.Windows.Forms.RichTextBox rtbRangeData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbOffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.RichTextBox rtbDWidth;
        private System.Windows.Forms.Label label6;
    }
}