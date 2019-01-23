namespace Config
{
    partial class CommandForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.btStart = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.gbPort = new System.Windows.Forms.GroupBox();
            this.btStop = new System.Windows.Forms.Button();
            this.cbG4A = new System.Windows.Forms.CheckBox();
            this.cbMbtcpClient = new System.Windows.Forms.CheckBox();
            this.cbG3B = new System.Windows.Forms.CheckBox();
            this.cbMbtcpServer = new System.Windows.Forms.CheckBox();
            this.cbG3A = new System.Windows.Forms.CheckBox();
            this.cbG2B = new System.Windows.Forms.CheckBox();
            this.cbPC1 = new System.Windows.Forms.CheckBox();
            this.cbG2A = new System.Windows.Forms.CheckBox();
            this.cbPC0 = new System.Windows.Forms.CheckBox();
            this.cbG1B = new System.Windows.Forms.CheckBox();
            this.cbG4B = new System.Windows.Forms.CheckBox();
            this.cbG1A = new System.Windows.Forms.CheckBox();
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAddr = new System.Windows.Forms.TextBox();
            this.btNextPage = new System.Windows.Forms.Button();
            this.btPrevPage = new System.Windows.Forms.Button();
            this.btFresh = new System.Windows.Forms.Button();
            this.cbLoop = new System.Windows.Forms.CheckBox();
            this.btExit = new System.Windows.Forms.Button();
            this.lbRecived = new System.Windows.Forms.ListBox();
            this.cmsRecived = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbPort.SuspendLayout();
            this.gbDatabase.SuspendLayout();
            this.cmsRecived.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Command:";
            // 
            // tbSend
            // 
            this.tbSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSend.Location = new System.Drawing.Point(14, 33);
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(731, 21);
            this.tbSend.TabIndex = 1;
            this.tbSend.TextChanged += new System.EventHandler(this.tbSend_TextChanged);
            this.tbSend.Enter += new System.EventHandler(this.tbSend_Enter);
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.Enabled = false;
            this.btStart.Location = new System.Drawing.Point(6, 169);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(147, 23);
            this.btStart.TabIndex = 2;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.Location = new System.Drawing.Point(592, 473);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(147, 23);
            this.btClear.TabIndex = 4;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // gbPort
            // 
            this.gbPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPort.Controls.Add(this.btStop);
            this.gbPort.Controls.Add(this.cbG4A);
            this.gbPort.Controls.Add(this.cbMbtcpClient);
            this.gbPort.Controls.Add(this.cbG3B);
            this.gbPort.Controls.Add(this.cbMbtcpServer);
            this.gbPort.Controls.Add(this.cbG3A);
            this.gbPort.Controls.Add(this.btStart);
            this.gbPort.Controls.Add(this.cbG2B);
            this.gbPort.Controls.Add(this.cbPC1);
            this.gbPort.Controls.Add(this.cbG2A);
            this.gbPort.Controls.Add(this.cbPC0);
            this.gbPort.Controls.Add(this.cbG1B);
            this.gbPort.Controls.Add(this.cbG4B);
            this.gbPort.Controls.Add(this.cbG1A);
            this.gbPort.Location = new System.Drawing.Point(586, 70);
            this.gbPort.Name = "gbPort";
            this.gbPort.Size = new System.Drawing.Size(159, 227);
            this.gbPort.TabIndex = 5;
            this.gbPort.TabStop = false;
            this.gbPort.Text = "Port Frame";
            // 
            // btStop
            // 
            this.btStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStop.Enabled = false;
            this.btStop.Location = new System.Drawing.Point(6, 198);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(147, 23);
            this.btStop.TabIndex = 7;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // cbG4A
            // 
            this.cbG4A.AutoSize = true;
            this.cbG4A.Location = new System.Drawing.Point(54, 68);
            this.cbG4A.Name = "cbG4A";
            this.cbG4A.Size = new System.Drawing.Size(42, 16);
            this.cbG4A.TabIndex = 6;
            this.cbG4A.Tag = "G4A";
            this.cbG4A.Text = "G4A";
            this.cbG4A.UseVisualStyleBackColor = true;
            this.cbG4A.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbMbtcpClient
            // 
            this.cbMbtcpClient.AutoSize = true;
            this.cbMbtcpClient.Location = new System.Drawing.Point(54, 136);
            this.cbMbtcpClient.Name = "cbMbtcpClient";
            this.cbMbtcpClient.Size = new System.Drawing.Size(96, 16);
            this.cbMbtcpClient.TabIndex = 5;
            this.cbMbtcpClient.Tag = "MBTCP_CLIENT";
            this.cbMbtcpClient.Text = "MBTCP_CLIENT";
            this.cbMbtcpClient.UseVisualStyleBackColor = true;
            this.cbMbtcpClient.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG3B
            // 
            this.cbG3B.AutoSize = true;
            this.cbG3B.Location = new System.Drawing.Point(54, 44);
            this.cbG3B.Name = "cbG3B";
            this.cbG3B.Size = new System.Drawing.Size(42, 16);
            this.cbG3B.TabIndex = 5;
            this.cbG3B.Tag = "G3B";
            this.cbG3B.Text = "G3B";
            this.cbG3B.UseVisualStyleBackColor = true;
            this.cbG3B.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbMbtcpServer
            // 
            this.cbMbtcpServer.AutoSize = true;
            this.cbMbtcpServer.Location = new System.Drawing.Point(54, 114);
            this.cbMbtcpServer.Name = "cbMbtcpServer";
            this.cbMbtcpServer.Size = new System.Drawing.Size(96, 16);
            this.cbMbtcpServer.TabIndex = 4;
            this.cbMbtcpServer.Tag = "MBTCP_SERVER";
            this.cbMbtcpServer.Text = "MBTCP_SERVER";
            this.cbMbtcpServer.UseVisualStyleBackColor = true;
            this.cbMbtcpServer.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG3A
            // 
            this.cbG3A.AutoSize = true;
            this.cbG3A.Location = new System.Drawing.Point(54, 20);
            this.cbG3A.Name = "cbG3A";
            this.cbG3A.Size = new System.Drawing.Size(42, 16);
            this.cbG3A.TabIndex = 4;
            this.cbG3A.Tag = "G3A";
            this.cbG3A.Text = "G3A";
            this.cbG3A.UseVisualStyleBackColor = true;
            this.cbG3A.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG2B
            // 
            this.cbG2B.AutoSize = true;
            this.cbG2B.Location = new System.Drawing.Point(6, 92);
            this.cbG2B.Name = "cbG2B";
            this.cbG2B.Size = new System.Drawing.Size(42, 16);
            this.cbG2B.TabIndex = 3;
            this.cbG2B.Tag = "G2B";
            this.cbG2B.Text = "G2B";
            this.cbG2B.UseVisualStyleBackColor = true;
            this.cbG2B.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbPC1
            // 
            this.cbPC1.AutoSize = true;
            this.cbPC1.Location = new System.Drawing.Point(6, 136);
            this.cbPC1.Name = "cbPC1";
            this.cbPC1.Size = new System.Drawing.Size(42, 16);
            this.cbPC1.TabIndex = 2;
            this.cbPC1.Tag = "PC1";
            this.cbPC1.Text = "PC1";
            this.cbPC1.UseVisualStyleBackColor = true;
            this.cbPC1.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG2A
            // 
            this.cbG2A.AutoSize = true;
            this.cbG2A.Location = new System.Drawing.Point(6, 68);
            this.cbG2A.Name = "cbG2A";
            this.cbG2A.Size = new System.Drawing.Size(42, 16);
            this.cbG2A.TabIndex = 2;
            this.cbG2A.Tag = "G2A";
            this.cbG2A.Text = "G2A";
            this.cbG2A.UseVisualStyleBackColor = true;
            this.cbG2A.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbPC0
            // 
            this.cbPC0.AutoSize = true;
            this.cbPC0.Location = new System.Drawing.Point(6, 114);
            this.cbPC0.Name = "cbPC0";
            this.cbPC0.Size = new System.Drawing.Size(42, 16);
            this.cbPC0.TabIndex = 1;
            this.cbPC0.Tag = "PC0";
            this.cbPC0.Text = "PC0";
            this.cbPC0.UseVisualStyleBackColor = true;
            this.cbPC0.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG1B
            // 
            this.cbG1B.AutoSize = true;
            this.cbG1B.Location = new System.Drawing.Point(6, 44);
            this.cbG1B.Name = "cbG1B";
            this.cbG1B.Size = new System.Drawing.Size(42, 16);
            this.cbG1B.TabIndex = 1;
            this.cbG1B.Tag = "G1B";
            this.cbG1B.Text = "G1B";
            this.cbG1B.UseVisualStyleBackColor = true;
            this.cbG1B.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG4B
            // 
            this.cbG4B.AutoSize = true;
            this.cbG4B.Location = new System.Drawing.Point(54, 92);
            this.cbG4B.Name = "cbG4B";
            this.cbG4B.Size = new System.Drawing.Size(42, 16);
            this.cbG4B.TabIndex = 0;
            this.cbG4B.Tag = "G4B";
            this.cbG4B.Text = "G4B";
            this.cbG4B.UseVisualStyleBackColor = true;
            this.cbG4B.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // cbG1A
            // 
            this.cbG1A.AutoSize = true;
            this.cbG1A.Location = new System.Drawing.Point(6, 20);
            this.cbG1A.Name = "cbG1A";
            this.cbG1A.Size = new System.Drawing.Size(42, 16);
            this.cbG1A.TabIndex = 0;
            this.cbG1A.Tag = "G1A";
            this.cbG1A.Text = "G1A";
            this.cbG1A.UseVisualStyleBackColor = true;
            this.cbG1A.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // gbDatabase
            // 
            this.gbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatabase.Controls.Add(this.label2);
            this.gbDatabase.Controls.Add(this.tbAddr);
            this.gbDatabase.Controls.Add(this.btNextPage);
            this.gbDatabase.Controls.Add(this.btPrevPage);
            this.gbDatabase.Controls.Add(this.btFresh);
            this.gbDatabase.Controls.Add(this.cbLoop);
            this.gbDatabase.Location = new System.Drawing.Point(586, 303);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.Size = new System.Drawing.Size(159, 152);
            this.gbDatabase.TabIndex = 6;
            this.gbDatabase.TabStop = false;
            this.gbDatabase.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "DB Offset:";
            // 
            // tbAddr
            // 
            this.tbAddr.Location = new System.Drawing.Point(6, 45);
            this.tbAddr.Name = "tbAddr";
            this.tbAddr.Size = new System.Drawing.Size(147, 21);
            this.tbAddr.TabIndex = 7;
            this.tbAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAddr_KeyPress);
            // 
            // btNextPage
            // 
            this.btNextPage.Location = new System.Drawing.Point(85, 117);
            this.btNextPage.Name = "btNextPage";
            this.btNextPage.Size = new System.Drawing.Size(68, 23);
            this.btNextPage.TabIndex = 6;
            this.btNextPage.Text = "NextPage";
            this.btNextPage.UseVisualStyleBackColor = true;
            this.btNextPage.Click += new System.EventHandler(this.btNextPage_Click);
            // 
            // btPrevPage
            // 
            this.btPrevPage.Location = new System.Drawing.Point(6, 117);
            this.btPrevPage.Name = "btPrevPage";
            this.btPrevPage.Size = new System.Drawing.Size(73, 23);
            this.btPrevPage.TabIndex = 5;
            this.btPrevPage.Text = "PrevPage";
            this.btPrevPage.UseVisualStyleBackColor = true;
            this.btPrevPage.Click += new System.EventHandler(this.btPrevPage_Click);
            // 
            // btFresh
            // 
            this.btFresh.Location = new System.Drawing.Point(6, 81);
            this.btFresh.Name = "btFresh";
            this.btFresh.Size = new System.Drawing.Size(73, 23);
            this.btFresh.TabIndex = 4;
            this.btFresh.Text = "Refresh";
            this.btFresh.UseVisualStyleBackColor = true;
            this.btFresh.Click += new System.EventHandler(this.btFresh_Click);
            // 
            // cbLoop
            // 
            this.cbLoop.AutoSize = true;
            this.cbLoop.Enabled = false;
            this.cbLoop.Location = new System.Drawing.Point(102, 85);
            this.cbLoop.Name = "cbLoop";
            this.cbLoop.Size = new System.Drawing.Size(48, 16);
            this.cbLoop.TabIndex = 3;
            this.cbLoop.Tag = "LOOP";
            this.cbLoop.Text = "Loop";
            this.cbLoop.UseVisualStyleBackColor = true;
            this.cbLoop.Visible = false;
            this.cbLoop.CheckedChanged += new System.EventHandler(this.cbLoop_CheckedChanged);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btExit.Location = new System.Drawing.Point(592, 504);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(147, 23);
            this.btExit.TabIndex = 7;
            this.btExit.Text = "Exit";
            this.btExit.UseVisualStyleBackColor = true;
            // 
            // lbRecived
            // 
            this.lbRecived.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRecived.ContextMenuStrip = this.cmsRecived;
            this.lbRecived.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecived.FormattingEnabled = true;
            this.lbRecived.ItemHeight = 14;
            this.lbRecived.Location = new System.Drawing.Point(14, 70);
            this.lbRecived.Name = "lbRecived";
            this.lbRecived.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbRecived.Size = new System.Drawing.Size(566, 452);
            this.lbRecived.TabIndex = 8;
            // 
            // cmsRecived
            // 
            this.cmsRecived.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.cmsRecived.Name = "cmsRecived";
            this.cmsRecived.Size = new System.Drawing.Size(138, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.ShowShortcutKeys = false;
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.ShowShortcutKeys = false;
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 539);
            this.Controls.Add(this.lbRecived);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.gbDatabase);
            this.Controls.Add(this.gbPort);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.tbSend);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 577);
            this.Name = "CommandForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommandForm_FormClosing);
            this.gbPort.ResumeLayout(false);
            this.gbPort.PerformLayout();
            this.gbDatabase.ResumeLayout(false);
            this.gbDatabase.PerformLayout();
            this.cmsRecived.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.GroupBox gbPort;
        private System.Windows.Forms.CheckBox cbG4A;
        private System.Windows.Forms.CheckBox cbMbtcpClient;
        private System.Windows.Forms.CheckBox cbG3B;
        private System.Windows.Forms.CheckBox cbMbtcpServer;
        private System.Windows.Forms.CheckBox cbG3A;
        private System.Windows.Forms.CheckBox cbG2B;
        private System.Windows.Forms.CheckBox cbPC1;
        private System.Windows.Forms.CheckBox cbG2A;
        private System.Windows.Forms.CheckBox cbPC0;
        private System.Windows.Forms.CheckBox cbG1B;
        private System.Windows.Forms.CheckBox cbG4B;
        private System.Windows.Forms.CheckBox cbG1A;
        private System.Windows.Forms.GroupBox gbDatabase;
        private System.Windows.Forms.CheckBox cbLoop;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAddr;
        private System.Windows.Forms.Button btNextPage;
        private System.Windows.Forms.Button btPrevPage;
        private System.Windows.Forms.Button btFresh;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.ListBox lbRecived;
        private System.Windows.Forms.ContextMenuStrip cmsRecived;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}