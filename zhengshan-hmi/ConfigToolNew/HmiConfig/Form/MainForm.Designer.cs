namespace HmiConfig
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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPB = new System.Windows.Forms.ToolStripProgressBar();
            this.tsReboot = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lValveId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lGroupId = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.tbCtrlPosCmd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCtrlPosAddr = new System.Windows.Forms.TextBox();
            this.tbCtrlPoscCmd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbCtrlPoscAddr = new System.Windows.Forms.TextBox();
            this.tbCtrlStopCmd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbCtrlStopAddr = new System.Windows.Forms.TextBox();
            this.tbCtrlCloseCmd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCtrlCloseAddr = new System.Windows.Forms.TextBox();
            this.tbCtrlOpenCmd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCtrlOpenAddr = new System.Windows.Forms.TextBox();
            this.tbViewPosMax = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbValveName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbViewPosAddr = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbViewLRAddr = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbViewCloseAddr = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbViewOpenAddr = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSaveValveData = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.cmsPortConfig.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslConnection,
            this.tsslConnectState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
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
            this.tvPortInfo.Location = new System.Drawing.Point(6, 56);
            this.tvPortInfo.Margin = new System.Windows.Forms.Padding(4);
            this.tvPortInfo.Name = "tvPortInfo";
            this.tvPortInfo.Size = new System.Drawing.Size(279, 566);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.7789F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.2211F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lGroupId, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, -1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 101);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lValveId, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(87, 5);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(56, 91);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // lValveId
            // 
            this.lValveId.AutoSize = true;
            this.lValveId.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lValveId.Location = new System.Drawing.Point(5, 46);
            this.lValveId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lValveId.Name = "lValveId";
            this.lValveId.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
            this.lValveId.Size = new System.Drawing.Size(15, 31);
            this.lValveId.TabIndex = 1;
            this.lValveId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(15, 7, 0, 0);
            this.label1.Size = new System.Drawing.Size(36, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "Addr";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lGroupId
            // 
            this.lGroupId.AutoSize = true;
            this.lGroupId.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lGroupId.Location = new System.Drawing.Point(5, 1);
            this.lGroupId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lGroupId.Name = "lGroupId";
            this.lGroupId.Padding = new System.Windows.Forms.Padding(10, 30, 0, 0);
            this.lGroupId.Size = new System.Drawing.Size(10, 78);
            this.lGroupId.TabIndex = 1;
            this.lGroupId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(152, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(60, 27, 0, 0);
            this.label4.Size = new System.Drawing.Size(237, 62);
            this.label4.TabIndex = 4;
            this.label4.Text = "Set Valve";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(-1, 104);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlPosCmd);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlPosAddr);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlPoscCmd);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlPoscAddr);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlStopCmd);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlStopAddr);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlCloseCmd);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlCloseAddr);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlOpenCmd);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.tbCtrlOpenAddr);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbViewPosMax);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.tbValveName);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.tbViewPosAddr);
            this.splitContainer1.Panel2.Controls.Add(this.label15);
            this.splitContainer1.Panel2.Controls.Add(this.tbViewLRAddr);
            this.splitContainer1.Panel2.Controls.Add(this.label16);
            this.splitContainer1.Panel2.Controls.Add(this.tbViewCloseAddr);
            this.splitContainer1.Panel2.Controls.Add(this.label17);
            this.splitContainer1.Panel2.Controls.Add(this.tbViewOpenAddr);
            this.splitContainer1.Size = new System.Drawing.Size(595, 573);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(99, 29);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 29);
            this.label10.TabIndex = 15;
            this.label10.Text = "Control ";
            // 
            // tbCtrlPosCmd
            // 
            this.tbCtrlPosCmd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlPosCmd.Location = new System.Drawing.Point(198, 297);
            this.tbCtrlPosCmd.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlPosCmd.Name = "tbCtrlPosCmd";
            this.tbCtrlPosCmd.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlPosCmd.TabIndex = 14;
            this.tbCtrlPosCmd.TextChanged += new System.EventHandler(this.tbPos_TextChanged);
            this.tbCtrlPosCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(25, 301);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "POS:";
            // 
            // tbCtrlPosAddr
            // 
            this.tbCtrlPosAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlPosAddr.Location = new System.Drawing.Point(87, 297);
            this.tbCtrlPosAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlPosAddr.Name = "tbCtrlPosAddr";
            this.tbCtrlPosAddr.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlPosAddr.TabIndex = 12;
            this.tbCtrlPosAddr.TextChanged += new System.EventHandler(this.tbCtrlDbOffset_TextChanged);
            this.tbCtrlPosAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // tbCtrlPoscCmd
            // 
            this.tbCtrlPoscCmd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlPoscCmd.Location = new System.Drawing.Point(197, 248);
            this.tbCtrlPoscCmd.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlPoscCmd.Name = "tbCtrlPoscCmd";
            this.tbCtrlPoscCmd.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlPoscCmd.TabIndex = 11;
            this.tbCtrlPoscCmd.TextChanged += new System.EventHandler(this.tbCtrlDbCmd_TextChanged);
            this.tbCtrlPoscCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(25, 252);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "PosC:";
            // 
            // tbCtrlPoscAddr
            // 
            this.tbCtrlPoscAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlPoscAddr.Location = new System.Drawing.Point(87, 248);
            this.tbCtrlPoscAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlPoscAddr.Name = "tbCtrlPoscAddr";
            this.tbCtrlPoscAddr.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlPoscAddr.TabIndex = 9;
            this.tbCtrlPoscAddr.TextChanged += new System.EventHandler(this.tbCtrlDbOffset_TextChanged);
            this.tbCtrlPoscAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // tbCtrlStopCmd
            // 
            this.tbCtrlStopCmd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlStopCmd.Location = new System.Drawing.Point(198, 199);
            this.tbCtrlStopCmd.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlStopCmd.Name = "tbCtrlStopCmd";
            this.tbCtrlStopCmd.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlStopCmd.TabIndex = 8;
            this.tbCtrlStopCmd.TextChanged += new System.EventHandler(this.tbCtrlDbCmd_TextChanged);
            this.tbCtrlStopCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(25, 203);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Stop:";
            // 
            // tbCtrlStopAddr
            // 
            this.tbCtrlStopAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlStopAddr.Location = new System.Drawing.Point(87, 199);
            this.tbCtrlStopAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlStopAddr.Name = "tbCtrlStopAddr";
            this.tbCtrlStopAddr.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlStopAddr.TabIndex = 6;
            this.tbCtrlStopAddr.TextChanged += new System.EventHandler(this.tbCtrlDbOffset_TextChanged);
            this.tbCtrlStopAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // tbCtrlCloseCmd
            // 
            this.tbCtrlCloseCmd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlCloseCmd.Location = new System.Drawing.Point(198, 150);
            this.tbCtrlCloseCmd.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlCloseCmd.Name = "tbCtrlCloseCmd";
            this.tbCtrlCloseCmd.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlCloseCmd.TabIndex = 5;
            this.tbCtrlCloseCmd.TextChanged += new System.EventHandler(this.tbCtrlDbCmd_TextChanged);
            this.tbCtrlCloseCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(14, 154);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Close:";
            // 
            // tbCtrlCloseAddr
            // 
            this.tbCtrlCloseAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlCloseAddr.Location = new System.Drawing.Point(87, 150);
            this.tbCtrlCloseAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlCloseAddr.Name = "tbCtrlCloseAddr";
            this.tbCtrlCloseAddr.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlCloseAddr.TabIndex = 3;
            this.tbCtrlCloseAddr.TextChanged += new System.EventHandler(this.tbCtrlDbOffset_TextChanged);
            this.tbCtrlCloseAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // tbCtrlOpenCmd
            // 
            this.tbCtrlOpenCmd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlOpenCmd.Location = new System.Drawing.Point(198, 101);
            this.tbCtrlOpenCmd.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlOpenCmd.Name = "tbCtrlOpenCmd";
            this.tbCtrlOpenCmd.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlOpenCmd.TabIndex = 2;
            this.tbCtrlOpenCmd.TextChanged += new System.EventHandler(this.tbCtrlDbCmd_TextChanged);
            this.tbCtrlOpenCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 105);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Open:";
            // 
            // tbCtrlOpenAddr
            // 
            this.tbCtrlOpenAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCtrlOpenAddr.Location = new System.Drawing.Point(87, 101);
            this.tbCtrlOpenAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbCtrlOpenAddr.Name = "tbCtrlOpenAddr";
            this.tbCtrlOpenAddr.Size = new System.Drawing.Size(96, 26);
            this.tbCtrlOpenAddr.TabIndex = 0;
            this.tbCtrlOpenAddr.TextChanged += new System.EventHandler(this.tbCtrlDbOffset_TextChanged);
            this.tbCtrlOpenAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // tbViewPosMax
            // 
            this.tbViewPosMax.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbViewPosMax.Location = new System.Drawing.Point(176, 248);
            this.tbViewPosMax.Margin = new System.Windows.Forms.Padding(4);
            this.tbViewPosMax.Name = "tbViewPosMax";
            this.tbViewPosMax.Size = new System.Drawing.Size(98, 26);
            this.tbViewPosMax.TabIndex = 33;
            this.tbViewPosMax.TextChanged += new System.EventHandler(this.tbPos_TextChanged);
            this.tbViewPosMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(25, 253);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "POS:";
            // 
            // tbValveName
            // 
            this.tbValveName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbValveName.Location = new System.Drawing.Point(70, 297);
            this.tbValveName.Margin = new System.Windows.Forms.Padding(4);
            this.tbValveName.Name = "tbValveName";
            this.tbValveName.Size = new System.Drawing.Size(204, 26);
            this.tbValveName.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(95, 29);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 29);
            this.label13.TabIndex = 32;
            this.label13.Text = "View";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(17, 301);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 16);
            this.label14.TabIndex = 27;
            this.label14.Text = "Name:";
            // 
            // tbViewPosAddr
            // 
            this.tbViewPosAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbViewPosAddr.Location = new System.Drawing.Point(70, 249);
            this.tbViewPosAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbViewPosAddr.Name = "tbViewPosAddr";
            this.tbViewPosAddr.Size = new System.Drawing.Size(98, 26);
            this.tbViewPosAddr.TabIndex = 26;
            this.tbViewPosAddr.TextChanged += new System.EventHandler(this.tbViewDbOffset_TextChanged);
            this.tbViewPosAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(25, 203);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 24;
            this.label15.Text = "L_R:";
            // 
            // tbViewLRAddr
            // 
            this.tbViewLRAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbViewLRAddr.Location = new System.Drawing.Point(70, 199);
            this.tbViewLRAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbViewLRAddr.Name = "tbViewLRAddr";
            this.tbViewLRAddr.Size = new System.Drawing.Size(204, 26);
            this.tbViewLRAddr.TabIndex = 23;
            this.tbViewLRAddr.TextChanged += new System.EventHandler(this.tbViewDbOffset_TextChanged);
            this.tbViewLRAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(6, 155);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 16);
            this.label16.TabIndex = 21;
            this.label16.Text = "Close:";
            // 
            // tbViewCloseAddr
            // 
            this.tbViewCloseAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbViewCloseAddr.Location = new System.Drawing.Point(70, 151);
            this.tbViewCloseAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbViewCloseAddr.Name = "tbViewCloseAddr";
            this.tbViewCloseAddr.Size = new System.Drawing.Size(204, 26);
            this.tbViewCloseAddr.TabIndex = 20;
            this.tbViewCloseAddr.TextChanged += new System.EventHandler(this.tbViewDbOffset_TextChanged);
            this.tbViewCloseAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(14, 105);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 16);
            this.label17.TabIndex = 18;
            this.label17.Text = "Open:";
            // 
            // tbViewOpenAddr
            // 
            this.tbViewOpenAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbViewOpenAddr.Location = new System.Drawing.Point(70, 101);
            this.tbViewOpenAddr.Margin = new System.Windows.Forms.Padding(4);
            this.tbViewOpenAddr.Name = "tbViewOpenAddr";
            this.tbViewOpenAddr.Size = new System.Drawing.Size(204, 26);
            this.tbViewOpenAddr.TabIndex = 17;
            this.tbViewOpenAddr.TextChanged += new System.EventHandler(this.tbViewDbOffset_TextChanged);
            this.tbViewOpenAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Data_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btSaveValveData);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(293, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 566);
            this.panel1.TabIndex = 6;
            // 
            // btSaveValveData
            // 
            this.btSaveValveData.Enabled = false;
            this.btSaveValveData.Location = new System.Drawing.Point(491, 4);
            this.btSaveValveData.Margin = new System.Windows.Forms.Padding(4);
            this.btSaveValveData.Name = "btSaveValveData";
            this.btSaveValveData.Size = new System.Drawing.Size(103, 96);
            this.btSaveValveData.TabIndex = 6;
            this.btSaveValveData.Text = "Enter";
            this.btSaveValveData.UseVisualStyleBackColor = true;
            this.btSaveValveData.Click += new System.EventHandler(this.btSaveValveData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 662);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvPortInfo);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(905, 700);
            this.MinimumSize = new System.Drawing.Size(905, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LYC Hmi Tool 1.3";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.cmsPortConfig.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView tvPortInfo;
        private System.Windows.Forms.ContextMenuStrip cmsPortConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiPortConfigConfigure;
        private System.Windows.Forms.ToolStripButton tsOpen;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsDownLoad;
        private System.Windows.Forms.ToolStripButton tsReadBack;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsSaveAs;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnectState;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiPortConfigCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPortConfigPaste;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lGroupId;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lValveId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbCtrlPosCmd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCtrlPosAddr;
        private System.Windows.Forms.TextBox tbCtrlPoscCmd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbCtrlPoscAddr;
        private System.Windows.Forms.TextBox tbCtrlStopCmd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbCtrlStopAddr;
        private System.Windows.Forms.TextBox tbCtrlCloseCmd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCtrlCloseAddr;
        private System.Windows.Forms.TextBox tbCtrlOpenCmd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCtrlOpenAddr;
        private System.Windows.Forms.TextBox tbViewPosMax;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbValveName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbViewPosAddr;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbViewLRAddr;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbViewCloseAddr;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbViewOpenAddr;
        private System.Windows.Forms.Button btSaveValveData;
        private System.Windows.Forms.ToolStripProgressBar tsPB;
        private System.Windows.Forms.ToolStripButton tsReboot;
    }
}

