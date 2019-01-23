namespace WYL
{
    partial class LoadFontRangeDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadFontRangeDataForm));
            this.rtbRangeData = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbCustFontData = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbRangeData
            // 
            this.rtbRangeData.Location = new System.Drawing.Point(14, 184);
            this.rtbRangeData.Name = "rtbRangeData";
            this.rtbRangeData.Size = new System.Drawing.Size(627, 110);
            this.rtbRangeData.TabIndex = 7;
            this.rtbRangeData.Text = "{\n { 35,35},\n    { 42,44},\n        {48,59},\n            {65,65},\n                " +
                "{77,77},\n                    {80,80},{87,87},{112,112},{119,119},}\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "RangeData：";
            // 
            // rtbCustFontData
            // 
            this.rtbCustFontData.Location = new System.Drawing.Point(14, 24);
            this.rtbCustFontData.Name = "rtbCustFontData";
            this.rtbCustFontData.Size = new System.Drawing.Size(627, 114);
            this.rtbCustFontData.TabIndex = 5;
            this.rtbCustFontData.Text = resources.GetString("rtbCustFontData.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "sCustFontData：";
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(435, 297);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(101, 23);
            this.btOk.TabIndex = 8;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(542, 297);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(99, 23);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // LoadFontRangeDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 329);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.rtbRangeData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtbCustFontData);
            this.Controls.Add(this.label1);
            this.Name = "LoadFontRangeDataForm";
            this.Text = "LoadFontRangeDataForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        public System.Windows.Forms.RichTextBox rtbRangeData;
        public System.Windows.Forms.RichTextBox rtbCustFontData;
    }
}