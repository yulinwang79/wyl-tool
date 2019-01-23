namespace LYC_DownLoader
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslConnectState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsReboot = new System.Windows.Forms.ToolStripButton();
            this.tbCoreFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbCore = new System.Windows.Forms.RadioButton();
            this.rbHmi = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tsPB = new System.Windows.Forms.ProgressBar();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.btUpdateCore = new System.Windows.Forms.Button();
            this.btUpdateHmi = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHmiFile = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslConnection,
            this.tsslConnectState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 184);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(439, 22);
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
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnect,
            this.toolStripSeparator5,
            this.toolStripSeparator3,
            this.tsReboot});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(439, 56);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "tsMain";
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
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 59);
            this.toolStripSeparator5.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 56);
            this.toolStripSeparator3.Visible = false;
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
            this.tsReboot.Visible = false;
            this.tsReboot.Click += new System.EventHandler(this.tsReboot_Click);
            // 
            // tbCoreFile
            // 
            this.tbCoreFile.Location = new System.Drawing.Point(66, 62);
            this.tbCoreFile.Name = "tbCoreFile";
            this.tbCoreFile.Size = new System.Drawing.Size(274, 26);
            this.tbCoreFile.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "CORE:";
            // 
            // rbCore
            // 
            this.rbCore.AutoSize = true;
            this.rbCore.Checked = true;
            this.rbCore.Location = new System.Drawing.Point(139, 133);
            this.rbCore.Name = "rbCore";
            this.rbCore.Size = new System.Drawing.Size(58, 20);
            this.rbCore.TabIndex = 4;
            this.rbCore.TabStop = true;
            this.rbCore.Text = "CORE";
            this.rbCore.UseVisualStyleBackColor = true;
            this.rbCore.Visible = false;
            // 
            // rbHmi
            // 
            this.rbHmi.AutoSize = true;
            this.rbHmi.Location = new System.Drawing.Point(66, 130);
            this.rbHmi.Name = "rbHmi";
            this.rbHmi.Size = new System.Drawing.Size(50, 20);
            this.rbHmi.TabIndex = 5;
            this.rbHmi.Text = "HMI";
            this.rbHmi.UseVisualStyleBackColor = true;
            this.rbHmi.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Path:";
            this.label2.Visible = false;
            // 
            // tsPB
            // 
            this.tsPB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsPB.Location = new System.Drawing.Point(0, 156);
            this.tsPB.Name = "tsPB";
            this.tsPB.Size = new System.Drawing.Size(437, 23);
            this.tsPB.TabIndex = 7;
            this.tsPB.Visible = false;
            // 
            // cbSize
            // 
            this.cbSize.FormattingEnabled = true;
            this.cbSize.Items.AddRange(new object[] {
            "450",
            "200",
            "128",
            "64"});
            this.cbSize.Location = new System.Drawing.Point(219, 126);
            this.cbSize.MaxLength = 3;
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(121, 24);
            this.cbSize.TabIndex = 9;
            this.cbSize.Text = "450";
            this.cbSize.Visible = false;
            this.cbSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbSize_KeyPress);
            // 
            // btUpdateCore
            // 
            this.btUpdateCore.Enabled = false;
            this.btUpdateCore.Location = new System.Drawing.Point(356, 62);
            this.btUpdateCore.Name = "btUpdateCore";
            this.btUpdateCore.Size = new System.Drawing.Size(75, 23);
            this.btUpdateCore.TabIndex = 10;
            this.btUpdateCore.Text = "Update";
            this.btUpdateCore.UseVisualStyleBackColor = true;
            this.btUpdateCore.Click += new System.EventHandler(this.btUpdateCore_Click);
            // 
            // btUpdateHmi
            // 
            this.btUpdateHmi.Enabled = false;
            this.btUpdateHmi.Location = new System.Drawing.Point(356, 97);
            this.btUpdateHmi.Name = "btUpdateHmi";
            this.btUpdateHmi.Size = new System.Drawing.Size(75, 23);
            this.btUpdateHmi.TabIndex = 13;
            this.btUpdateHmi.Text = "Update";
            this.btUpdateHmi.UseVisualStyleBackColor = true;
            this.btUpdateHmi.Click += new System.EventHandler(this.btUpdateHmi_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "HMI:";
            // 
            // tbHmiFile
            // 
            this.tbHmiFile.Location = new System.Drawing.Point(66, 94);
            this.tbHmiFile.Name = "tbHmiFile";
            this.tbHmiFile.Size = new System.Drawing.Size(274, 26);
            this.tbHmiFile.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 206);
            this.Controls.Add(this.btUpdateHmi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbHmiFile);
            this.Controls.Add(this.btUpdateCore);
            this.Controls.Add(this.cbSize);
            this.Controls.Add(this.tsPB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbHmi);
            this.Controls.Add(this.rbCore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCoreFile);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LYC Update 1.3";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnectState;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnection;
        private System.Windows.Forms.TextBox tbCoreFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbCore;
        private System.Windows.Forms.RadioButton rbHmi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar tsPB;
        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.ToolStripButton tsReboot;
        private System.Windows.Forms.Button btUpdateCore;
        private System.Windows.Forms.Button btUpdateHmi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHmiFile;
    }
}

