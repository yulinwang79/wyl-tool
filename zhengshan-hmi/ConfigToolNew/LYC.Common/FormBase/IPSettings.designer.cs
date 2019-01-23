namespace LYC.Common
{

    partial class IPSettings
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
            this.btOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tIPAddress = new System.Windows.Forms.TextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbSerialPort = new System.Windows.Forms.RadioButton();
            this.rbNetwork = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(347, 53);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(90, 25);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "OK";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP Address:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tIPAddress
            // 
            this.tIPAddress.Location = new System.Drawing.Point(99, 41);
            this.tIPAddress.Name = "tIPAddress";
            this.tIPAddress.Size = new System.Drawing.Size(223, 21);
            this.tIPAddress.TabIndex = 0;
            this.tIPAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SERVER_KeyPress);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(347, 92);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(90, 25);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbSerialPort);
            this.groupBox1.Controls.Add(this.rbNetwork);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tIPAddress);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(99, 70);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(223, 20);
            this.cbPort.TabIndex = 10;
            this.cbPort.DropDown += new System.EventHandler(this.cbPort_DropDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Serial Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbSerialPort
            // 
            this.rbSerialPort.AutoSize = true;
            this.rbSerialPort.Location = new System.Drawing.Point(104, 18);
            this.rbSerialPort.Name = "rbSerialPort";
            this.rbSerialPort.Size = new System.Drawing.Size(83, 16);
            this.rbSerialPort.TabIndex = 8;
            this.rbSerialPort.TabStop = true;
            this.rbSerialPort.Text = "SerialPort";
            this.rbSerialPort.UseVisualStyleBackColor = true;
            this.rbSerialPort.CheckedChanged += new System.EventHandler(this.rbutton_CheckedChanged);
            // 
            // rbNetwork
            // 
            this.rbNetwork.AutoSize = true;
            this.rbNetwork.Location = new System.Drawing.Point(19, 18);
            this.rbNetwork.Name = "rbNetwork";
            this.rbNetwork.Size = new System.Drawing.Size(65, 16);
            this.rbNetwork.TabIndex = 4;
            this.rbNetwork.TabStop = true;
            this.rbNetwork.Text = "Network";
            this.rbNetwork.UseVisualStyleBackColor = true;
            this.rbNetwork.CheckedChanged += new System.EventHandler(this.rbutton_CheckedChanged);
            // 
            // IPSettings
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 125);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.MinimumSize = new System.Drawing.Size(457, 121);
            this.Name = "IPSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tIPAddress;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton rbNetwork;
        public System.Windows.Forms.RadioButton rbSerialPort;
    }
}
