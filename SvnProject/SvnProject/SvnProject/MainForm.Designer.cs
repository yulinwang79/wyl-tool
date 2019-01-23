namespace SvnProject
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbProject = new System.Windows.Forms.ToolStripComboBox();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tscbDays = new System.Windows.Forms.ToolStripComboBox();
            this.tvVernoInfo = new System.Windows.Forms.TreeView();
            this.rtbVernoInfo = new System.Windows.Forms.RichTextBox();
            this.dgvName = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvName)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLoad,
            this.toolStripSeparator3,
            this.tscbProject,
            this.tsbExport,
            this.tscbDays});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(789, 56);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "tsMain";
            // 
            // tsLoad
            // 
            this.tsLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsLoad.Image")));
            this.tsLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLoad.Name = "tsLoad";
            this.tsLoad.Size = new System.Drawing.Size(41, 53);
            this.tsLoad.Text = "Load";
            this.tsLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsLoad.Click += new System.EventHandler(this.tsLoad_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 56);
            // 
            // tscbProject
            // 
            this.tscbProject.DropDownWidth = 121;
            this.tscbProject.Items.AddRange(new object[] {
            "name",
            "project"});
            this.tscbProject.Name = "tscbProject";
            this.tscbProject.Size = new System.Drawing.Size(121, 56);
            this.tscbProject.Text = "project";
            this.tscbProject.TextChanged += new System.EventHandler(this.tscbProject_TextChanged);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(50, 53);
            this.tsbExport.Text = "Export";
            this.tsbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExport.Visible = false;
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tscbDays
            // 
            this.tscbDays.DropDownWidth = 121;
            this.tscbDays.Items.AddRange(new object[] {
            "Within a month",
            "Within two months",
            "All"});
            this.tscbDays.Name = "tscbDays";
            this.tscbDays.Size = new System.Drawing.Size(121, 56);
            this.tscbDays.Text = "All";
            this.tscbDays.Visible = false;
            this.tscbDays.TextChanged += new System.EventHandler(this.tscbDays_TextChanged);
            // 
            // tvVernoInfo
            // 
            this.tvVernoInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvVernoInfo.Location = new System.Drawing.Point(5, 55);
            this.tvVernoInfo.Name = "tvVernoInfo";
            this.tvVernoInfo.Size = new System.Drawing.Size(229, 450);
            this.tvVernoInfo.TabIndex = 4;
            this.tvVernoInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvVernoInfo_AfterSelect);
            // 
            // rtbVernoInfo
            // 
            this.rtbVernoInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbVernoInfo.BackColor = System.Drawing.SystemColors.Window;
            this.rtbVernoInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbVernoInfo.Location = new System.Drawing.Point(240, 55);
            this.rtbVernoInfo.Name = "rtbVernoInfo";
            this.rtbVernoInfo.ReadOnly = true;
            this.rtbVernoInfo.Size = new System.Drawing.Size(537, 444);
            this.rtbVernoInfo.TabIndex = 5;
            this.rtbVernoInfo.Text = "";
            // 
            // dgvName
            // 
            this.dgvName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvName.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvName.Location = new System.Drawing.Point(240, 55);
            this.dgvName.Name = "dgvName";
            this.dgvName.RowTemplate.Height = 23;
            this.dgvName.Size = new System.Drawing.Size(537, 444);
            this.dgvName.TabIndex = 6;
            this.dgvName.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 511);
            this.Controls.Add(this.dgvName);
            this.Controls.Add(this.rtbVernoInfo);
            this.Controls.Add(this.tvVernoInfo);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "SvnProject";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox tscbProject;
        private System.Windows.Forms.ToolStripButton tsLoad;
        private System.Windows.Forms.TreeView tvVernoInfo;
        private System.Windows.Forms.RichTextBox rtbVernoInfo;
        private System.Windows.Forms.DataGridView dgvName;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripComboBox tscbDays;
    }
}

