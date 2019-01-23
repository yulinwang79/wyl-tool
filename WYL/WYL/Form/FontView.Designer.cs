namespace WYL
{
    partial class FontView
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsLoadBdfL = new System.Windows.Forms.ToolStripButton();
            this.tsLoadDataL = new System.Windows.Forms.ToolStripButton();
            this.tsSaveL = new System.Windows.Forms.ToolStripButton();
            this.tsMergeR = new System.Windows.Forms.ToolStripButton();
            this.tsSaveR = new System.Windows.Forms.ToolStripButton();
            this.tsMerge = new System.Windows.Forms.ToolStripButton();
            this.tsLoadDataR = new System.Windows.Forms.ToolStripButton();
            this.tsLoadBdfR = new System.Windows.Forms.ToolStripButton();
            this.btInsertL = new System.Windows.Forms.Button();
            this.btLoadBmp = new System.Windows.Forms.Button();
            this.pbBitmap = new System.Windows.Forms.PictureBox();
            this.btInsertR = new System.Windows.Forms.Button();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFontL = new System.Windows.Forms.DataGridView();
            this.dgvFontR = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBitmap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFontL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFontR)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLoadBdfL,
            this.tsLoadDataL,
            this.tsSaveL,
            this.tsMergeR,
            this.tsSaveR,
            this.tsMerge,
            this.tsLoadDataR,
            this.tsLoadBdfR});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 36);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsLoadBdfL
            // 
            this.tsLoadBdfL.Image = ((System.Drawing.Image)(resources.GetObject("tsLoadBdfL.Image")));
            this.tsLoadBdfL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLoadBdfL.Name = "tsLoadBdfL";
            this.tsLoadBdfL.Size = new System.Drawing.Size(67, 33);
            this.tsLoadBdfL.Text = "Load Bdf";
            this.tsLoadBdfL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsLoadBdfL.Click += new System.EventHandler(this.tsLoadBdfL_Click);
            // 
            // tsLoadDataL
            // 
            this.tsLoadDataL.Image = ((System.Drawing.Image)(resources.GetObject("tsLoadDataL.Image")));
            this.tsLoadDataL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLoadDataL.Name = "tsLoadDataL";
            this.tsLoadDataL.Size = new System.Drawing.Size(74, 33);
            this.tsLoadDataL.Text = "Load Date";
            this.tsLoadDataL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsLoadDataL.Click += new System.EventHandler(this.tsLoadData_Click);
            // 
            // tsSaveL
            // 
            this.tsSaveL.Enabled = false;
            this.tsSaveL.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveL.Image")));
            this.tsSaveL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveL.Name = "tsSaveL";
            this.tsSaveL.Size = new System.Drawing.Size(39, 33);
            this.tsSaveL.Text = "Save";
            this.tsSaveL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSaveL.Click += new System.EventHandler(this.tsSaveL_Click);
            // 
            // tsMergeR
            // 
            this.tsMergeR.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsMergeR.Enabled = false;
            this.tsMergeR.Image = ((System.Drawing.Image)(resources.GetObject("tsMergeR.Image")));
            this.tsMergeR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMergeR.Name = "tsMergeR";
            this.tsMergeR.Size = new System.Drawing.Size(81, 33);
            this.tsMergeR.Text = "Merge L->R";
            this.tsMergeR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsMergeR.Click += new System.EventHandler(this.tsMergeR_Click);
            // 
            // tsSaveR
            // 
            this.tsSaveR.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsSaveR.Enabled = false;
            this.tsSaveR.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveR.Image")));
            this.tsSaveR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveR.Name = "tsSaveR";
            this.tsSaveR.Size = new System.Drawing.Size(39, 33);
            this.tsSaveR.Text = "Save";
            this.tsSaveR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSaveR.Click += new System.EventHandler(this.tsSaveR_Click);
            // 
            // tsMerge
            // 
            this.tsMerge.Enabled = false;
            this.tsMerge.Image = ((System.Drawing.Image)(resources.GetObject("tsMerge.Image")));
            this.tsMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMerge.Name = "tsMerge";
            this.tsMerge.Size = new System.Drawing.Size(81, 33);
            this.tsMerge.Text = "Merge L<-R";
            this.tsMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsMerge.Click += new System.EventHandler(this.tsMergeL_Click);
            // 
            // tsLoadDataR
            // 
            this.tsLoadDataR.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsLoadDataR.Image = ((System.Drawing.Image)(resources.GetObject("tsLoadDataR.Image")));
            this.tsLoadDataR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLoadDataR.Name = "tsLoadDataR";
            this.tsLoadDataR.Size = new System.Drawing.Size(74, 33);
            this.tsLoadDataR.Text = "Load Date";
            this.tsLoadDataR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsLoadDataR.Click += new System.EventHandler(this.tsLoadDataR_Click);
            // 
            // tsLoadBdfR
            // 
            this.tsLoadBdfR.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsLoadBdfR.Image = ((System.Drawing.Image)(resources.GetObject("tsLoadBdfR.Image")));
            this.tsLoadBdfR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLoadBdfR.Name = "tsLoadBdfR";
            this.tsLoadBdfR.Size = new System.Drawing.Size(67, 33);
            this.tsLoadBdfR.Text = "Load Bdf";
            this.tsLoadBdfR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsLoadBdfR.Click += new System.EventHandler(this.tsLoadBdfR_Click);
            // 
            // btInsertL
            // 
            this.btInsertL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btInsertL.Enabled = false;
            this.btInsertL.Location = new System.Drawing.Point(265, 412);
            this.btInsertL.Name = "btInsertL";
            this.btInsertL.Size = new System.Drawing.Size(75, 23);
            this.btInsertL.TabIndex = 12;
            this.btInsertL.Text = "<<Insert";
            this.btInsertL.Click += new System.EventHandler(this.btInsertL_Click);
            // 
            // btLoadBmp
            // 
            this.btLoadBmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btLoadBmp.Location = new System.Drawing.Point(12, 408);
            this.btLoadBmp.Name = "btLoadBmp";
            this.btLoadBmp.Size = new System.Drawing.Size(75, 23);
            this.btLoadBmp.TabIndex = 11;
            this.btLoadBmp.Text = "Load Bmp";
            this.btLoadBmp.Click += new System.EventHandler(this.btLoadBmp_Click);
            // 
            // pbBitmap
            // 
            this.pbBitmap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbBitmap.BackColor = System.Drawing.Color.White;
            this.pbBitmap.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbBitmap.Location = new System.Drawing.Point(103, 437);
            this.pbBitmap.Name = "pbBitmap";
            this.pbBitmap.Size = new System.Drawing.Size(103, 77);
            this.pbBitmap.TabIndex = 10;
            this.pbBitmap.TabStop = false;
            // 
            // btInsertR
            // 
            this.btInsertR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btInsertR.Enabled = false;
            this.btInsertR.Location = new System.Drawing.Point(346, 412);
            this.btInsertR.Name = "btInsertR";
            this.btInsertR.Size = new System.Drawing.Size(75, 23);
            this.btInsertR.TabIndex = 13;
            this.btInsertR.Text = "Insert>>";
            this.btInsertR.Click += new System.EventHandler(this.btInsertR_Click);
            // 
            // tbCode
            // 
            this.tbCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbCode.Location = new System.Drawing.Point(142, 410);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(64, 21);
            this.tbCode.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "Code:";
            // 
            // dgvFontL
            // 
            this.dgvFontL.AllowUserToAddRows = false;
            this.dgvFontL.AllowUserToDeleteRows = false;
            this.dgvFontL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFontL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFontL.Location = new System.Drawing.Point(0, 0);
            this.dgvFontL.Name = "dgvFontL";
            this.dgvFontL.ReadOnly = true;
            this.dgvFontL.RowTemplate.Height = 23;
            this.dgvFontL.Size = new System.Drawing.Size(390, 353);
            this.dgvFontL.TabIndex = 0;
            this.dgvFontL.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFontL_CellEnter);
            // 
            // dgvFontR
            // 
            this.dgvFontR.AllowUserToAddRows = false;
            this.dgvFontR.AllowUserToDeleteRows = false;
            this.dgvFontR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFontR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFontR.Location = new System.Drawing.Point(3, 0);
            this.dgvFontR.Name = "dgvFontR";
            this.dgvFontR.ReadOnly = true;
            this.dgvFontR.RowTemplate.Height = 23;
            this.dgvFontR.Size = new System.Drawing.Size(357, 353);
            this.dgvFontR.TabIndex = 1;
            this.dgvFontR.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFontR_CellEnter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(5, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvFontL);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvFontR);
            this.splitContainer1.Size = new System.Drawing.Size(760, 356);
            this.splitContainer1.SplitterDistance = 393;
            this.splitContainer1.TabIndex = 16;
            // 
            // FontView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 515);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pbBitmap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.btInsertR);
            this.Controls.Add(this.btInsertL);
            this.Controls.Add(this.btLoadBmp);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FontView";
            this.Text = "FontView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBitmap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFontL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFontR)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsLoadBdfL;
        private System.Windows.Forms.ToolStripButton tsLoadDataL;
        private System.Windows.Forms.ToolStripButton tsSaveL;
        private System.Windows.Forms.ToolStripButton tsMerge;
        private System.Windows.Forms.ToolStripButton tsLoadDataR;
        private System.Windows.Forms.ToolStripButton tsLoadBdfR;
        private System.Windows.Forms.ToolStripButton tsSaveR;
        private System.Windows.Forms.ToolStripButton tsMergeR;
        private System.Windows.Forms.Button btInsertL;
        private System.Windows.Forms.Button btLoadBmp;
        public System.Windows.Forms.PictureBox pbBitmap;
        private System.Windows.Forms.Button btInsertR;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvFontL;
        private System.Windows.Forms.DataGridView dgvFontR;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}

