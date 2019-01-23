namespace Config
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslConnectState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tvPortInfo = new System.Windows.Forms.TreeView();
            this.cmsPortConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPortConfigConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPortConfigCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPortConfigPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbPortInfo = new System.Windows.Forms.RichTextBox();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDownLoad = new System.Windows.Forms.ToolStripButton();
            this.tsReadBack = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPB = new System.Windows.Forms.ToolStripProgressBar();
            this.tsReboot = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.cmsPortConfig.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslConnection,
            this.tsslConnectState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 570);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(889, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslConnection
            // 
            this.tsslConnection.Name = "tsslConnection";
            this.tsslConnection.Size = new System.Drawing.Size(131, 17);
            this.tsslConnection.Text = "toolStripStatusLabel1";
            // 
            // tsslConnectState
            // 
            this.tsslConnectState.Name = "tsslConnectState";
            this.tsslConnectState.Size = new System.Drawing.Size(131, 17);
            this.tsslConnectState.Text = "toolStripStatusLabel1";
            // 
            // tvPortInfo
            // 
            this.tvPortInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvPortInfo.Location = new System.Drawing.Point(0, 59);
            this.tvPortInfo.Name = "tvPortInfo";
            this.tvPortInfo.Size = new System.Drawing.Size(192, 508);
            this.tvPortInfo.TabIndex = 3;
            this.tvPortInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPortInfo_AfterSelect);
            this.tvPortInfo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvPortInfo_NodeMouseDoubleClick);
            this.tvPortInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvPortInfo_MouseDown);
            // 
            // cmsPortConfig
            // 
            this.cmsPortConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPortConfigConfigure,
            this.tsmiPortConfigCopy,
            this.tsmiPortConfigPaste});
            this.cmsPortConfig.Name = "cmsPortConfig";
            this.cmsPortConfig.Size = new System.Drawing.Size(134, 70);
            this.cmsPortConfig.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPortConfig_Opening);
            // 
            // tsmiPortConfigConfigure
            // 
            this.tsmiPortConfigConfigure.Name = "tsmiPortConfigConfigure";
            this.tsmiPortConfigConfigure.Size = new System.Drawing.Size(133, 22);
            this.tsmiPortConfigConfigure.Text = "Configure";
            this.tsmiPortConfigConfigure.Click += new System.EventHandler(this.tsmiPortConfigConfigure_Click);
            // 
            // tsmiPortConfigCopy
            // 
            this.tsmiPortConfigCopy.Name = "tsmiPortConfigCopy";
            this.tsmiPortConfigCopy.Size = new System.Drawing.Size(133, 22);
            this.tsmiPortConfigCopy.Text = "Copy";
            this.tsmiPortConfigCopy.Click += new System.EventHandler(this.tsmiPortConfigCopy_Click);
            // 
            // tsmiPortConfigPaste
            // 
            this.tsmiPortConfigPaste.Name = "tsmiPortConfigPaste";
            this.tsmiPortConfigPaste.Size = new System.Drawing.Size(133, 22);
            this.tsmiPortConfigPaste.Text = "Paste";
            this.tsmiPortConfigPaste.Click += new System.EventHandler(this.tsmiPortConfigPaste_Click);
            // 
            // rtbPortInfo
            // 
            this.rtbPortInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbPortInfo.BackColor = System.Drawing.SystemColors.Window;
            this.rtbPortInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbPortInfo.Location = new System.Drawing.Point(203, 59);
            this.rtbPortInfo.Name = "rtbPortInfo";
            this.rtbPortInfo.ReadOnly = true;
            this.rtbPortInfo.Size = new System.Drawing.Size(686, 508);
            this.rtbPortInfo.TabIndex = 4;
            this.rtbPortInfo.Text = "";
            // 
            // tsOpen
            // 
            this.tsOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsOpen.Image")));
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(44, 53);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.Click += new System.EventHandler(this.tsOpen_Click);
            // 
            // tsNew
            // 
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(38, 53);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // tsDownLoad
            // 
            this.tsDownLoad.Enabled = false;
            this.tsDownLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsDownLoad.Image")));
            this.tsDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDownLoad.Name = "tsDownLoad";
            this.tsDownLoad.Size = new System.Drawing.Size(74, 53);
            this.tsDownLoad.Text = "DownLoad";
            this.tsDownLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsDownLoad.Click += new System.EventHandler(this.tsDownLoad_Click);
            // 
            // tsReadBack
            // 
            this.tsReadBack.Enabled = false;
            this.tsReadBack.Image = ((System.Drawing.Image)(resources.GetObject("tsReadBack.Image")));
            this.tsReadBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsReadBack.Name = "tsReadBack";
            this.tsReadBack.Size = new System.Drawing.Size(70, 53);
            this.tsReadBack.Text = "ReadBack";
            this.tsReadBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsReadBack.Click += new System.EventHandler(this.tsReadBack_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOpen,
            this.tsNew,
            this.tsSave,
            this.tsSaveAs,
            this.toolStripSeparator1,
            this.tsConnect,
            this.toolStripSeparator5,
            this.tsCommand,
            this.tsDownLoad,
            this.tsReadBack,
            this.toolStripSeparator3,
            this.tsPB,
            this.tsReboot});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(889, 56);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "tsMain";
            // 
            // tsSave
            // 
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(39, 53);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsSaveAs
            // 
            this.tsSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveAs.Image")));
            this.tsSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveAs.Name = "tsSaveAs";
            this.tsSaveAs.Size = new System.Drawing.Size(57, 53);
            this.tsSaveAs.Text = "Save As";
            this.tsSaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSaveAs.Click += new System.EventHandler(this.tsSaveAs_Click);
            // 
            // tsConnect
            // 
            this.tsConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsConnect.Image")));
            this.tsConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConnect.Name = "tsConnect";
            this.tsConnect.Size = new System.Drawing.Size(59, 53);
            this.tsConnect.Text = "Connect";
            this.tsConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsConnect.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 56);
            // 
            // tsCommand
            // 
            this.tsCommand.Enabled = false;
            this.tsCommand.Image = ((System.Drawing.Image)(resources.GetObject("tsCommand.Image")));
            this.tsCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCommand.Name = "tsCommand";
            this.tsCommand.Size = new System.Drawing.Size(51, 53);
            this.tsCommand.Text = "Debug";
            this.tsCommand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCommand.Click += new System.EventHandler(this.tsCommand_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 56);
            // 
            // tsPB
            // 
            this.tsPB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsPB.AutoSize = false;
            this.tsPB.Name = "tsPB";
            this.tsPB.Size = new System.Drawing.Size(300, 25);
            this.tsPB.Visible = false;
            // 
            // tsReboot
            // 
            this.tsReboot.Enabled = false;
            this.tsReboot.Image = ((System.Drawing.Image)(resources.GetObject("tsReboot.Image")));
            this.tsReboot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsReboot.Name = "tsReboot";
            this.tsReboot.Size = new System.Drawing.Size(55, 53);
            this.tsReboot.Text = "Reboot";
            this.tsReboot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsReboot.Click += new System.EventHandler(this.tsReboot_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 592);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rtbPortInfo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tvPortInfo);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LYC Tool 1.3";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.cmsPortConfig.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView tvPortInfo;
        private System.Windows.Forms.ContextMenuStrip cmsPortConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiPortConfigConfigure;
        private System.Windows.Forms.RichTextBox rtbPortInfo;
        private System.Windows.Forms.ToolStripButton tsOpen;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsDownLoad;
        private System.Windows.Forms.ToolStripButton tsReadBack;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsSaveAs;
        private System.Windows.Forms.ToolStripButton tsCommand;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnectState;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsConnect;
        private System.Windows.Forms.ToolStripButton tsReboot;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiPortConfigCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPortConfigPaste;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnection;
        private System.Windows.Forms.ToolStripProgressBar tsPB;
    }
}

