namespace Config
{
    partial class PortConfigEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortConfigEditForm));
            this.tbCfgValue = new System.Windows.Forms.TextBox();
            this.cbCfgValue = new System.Windows.Forms.ComboBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.btResetTag = new System.Windows.Forms.Button();
            this.btResetDefault = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.dgvPortConfig = new System.Windows.Forms.DataGridView();
            this.tbCfgName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // tbCfgValue
            // 
            this.tbCfgValue.Location = new System.Drawing.Point(355, 48);
            this.tbCfgValue.Name = "tbCfgValue";
            this.tbCfgValue.Size = new System.Drawing.Size(209, 21);
            this.tbCfgValue.TabIndex = 2;
            this.tbCfgValue.TextChanged += new System.EventHandler(this.tbCfgValue_TextChanged);
            // 
            // cbCfgValue
            // 
            this.cbCfgValue.FormattingEnabled = true;
            this.cbCfgValue.Location = new System.Drawing.Point(354, 49);
            this.cbCfgValue.Name = "cbCfgValue";
            this.cbCfgValue.Size = new System.Drawing.Size(210, 20);
            this.cbCfgValue.TabIndex = 4;
            this.cbCfgValue.SelectedIndexChanged += new System.EventHandler(this.cbCfgValue_SelectedIndexChanged);
            this.cbCfgValue.SelectedValueChanged += new System.EventHandler(this.cbCfgValue_SelectedValueChanged);
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(356, 96);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(208, 21);
            this.tbComment.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Comment:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Definition:";
            // 
            // tbDescription
            // 
            this.tbDescription.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbDescription.Location = new System.Drawing.Point(357, 145);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDescription.Size = new System.Drawing.Size(208, 275);
            this.tbDescription.TabIndex = 8;
            // 
            // btResetTag
            // 
            this.btResetTag.Location = new System.Drawing.Point(355, 426);
            this.btResetTag.Name = "btResetTag";
            this.btResetTag.Size = new System.Drawing.Size(102, 23);
            this.btResetTag.TabIndex = 9;
            this.btResetTag.Text = "Reset Tag";
            this.btResetTag.UseVisualStyleBackColor = true;
            this.btResetTag.Click += new System.EventHandler(this.btResetTag_Click);
            // 
            // btResetDefault
            // 
            this.btResetDefault.Location = new System.Drawing.Point(463, 426);
            this.btResetDefault.Name = "btResetDefault";
            this.btResetDefault.Size = new System.Drawing.Size(102, 23);
            this.btResetDefault.TabIndex = 10;
            this.btResetDefault.Text = "Reset Default";
            this.btResetDefault.UseVisualStyleBackColor = true;
            this.btResetDefault.Click += new System.EventHandler(this.btResetDefault_Click);
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(355, 455);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(102, 23);
            this.btOk.TabIndex = 11;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(463, 455);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(102, 23);
            this.btCancel.TabIndex = 12;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // dgvPortConfig
            // 
            this.dgvPortConfig.AllowUserToAddRows = false;
            this.dgvPortConfig.AllowUserToDeleteRows = false;
            this.dgvPortConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPortConfig.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPortConfig.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPortConfig.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPortConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPortConfig.ColumnHeadersVisible = false;
            this.dgvPortConfig.Location = new System.Drawing.Point(12, 12);
            this.dgvPortConfig.MultiSelect = false;
            this.dgvPortConfig.Name = "dgvPortConfig";
            this.dgvPortConfig.ReadOnly = true;
            this.dgvPortConfig.RowHeadersVisible = false;
            this.dgvPortConfig.RowHeadersWidth = 30;
            this.dgvPortConfig.RowTemplate.Height = 23;
            this.dgvPortConfig.Size = new System.Drawing.Size(336, 468);
            this.dgvPortConfig.TabIndex = 13;
            this.dgvPortConfig.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPortConfig_CellEnter);
            this.dgvPortConfig.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPortConfig_CellLeave);
            this.dgvPortConfig.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPortConfig_CellPainting);
            // 
            // tbCfgName
            // 
            this.tbCfgName.Enabled = false;
            this.tbCfgName.Location = new System.Drawing.Point(355, 12);
            this.tbCfgName.Name = "tbCfgName";
            this.tbCfgName.Size = new System.Drawing.Size(209, 21);
            this.tbCfgName.TabIndex = 14;
            // 
            // PortConfigEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 492);
            this.Controls.Add(this.tbCfgName);
            this.Controls.Add(this.dgvPortConfig);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btResetDefault);
            this.Controls.Add(this.btResetTag);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.cbCfgValue);
            this.Controls.Add(this.tbCfgValue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(585, 530);
            this.MinimumSize = new System.Drawing.Size(585, 530);
            this.Name = "PortConfigEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PortConfig";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCfgValue;
        private System.Windows.Forms.ComboBox cbCfgValue;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button btResetTag;
        private System.Windows.Forms.Button btResetDefault;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.DataGridView dgvPortConfig;
        private System.Windows.Forms.TextBox tbCfgName;
    }
}