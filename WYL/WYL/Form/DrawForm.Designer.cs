namespace WYL
{
    partial class DrawForm
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
            this.pbBitmap = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btLoad = new System.Windows.Forms.Button();
            this.btInsert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBitmap)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBitmap
            // 
            this.pbBitmap.BackColor = System.Drawing.Color.White;
            this.pbBitmap.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbBitmap.Location = new System.Drawing.Point(0, 0);
            this.pbBitmap.Name = "pbBitmap";
            this.pbBitmap.Size = new System.Drawing.Size(80, 57);
            this.pbBitmap.TabIndex = 0;
            this.pbBitmap.TabStop = false;
            this.pbBitmap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pbBitmap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pbBitmap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pbBitmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(71, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 7;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(448, 3);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(75, 23);
            this.btLoad.TabIndex = 8;
            this.btLoad.Text = "Load";
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btInsert
            // 
            this.btInsert.Enabled = false;
            this.btInsert.Location = new System.Drawing.Point(448, 34);
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(75, 23);
            this.btInsert.TabIndex = 9;
            this.btInsert.Text = "Insert";
            this.btInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // DrawForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(552, 397);
            this.Controls.Add(this.btInsert);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbBitmap);
            this.Name = "DrawForm";
            this.Text = "DrawForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbBitmap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btInsert;
    }
}