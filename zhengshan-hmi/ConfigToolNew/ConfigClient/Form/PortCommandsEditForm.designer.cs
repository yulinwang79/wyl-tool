namespace Config
{
    partial class PortCommandsEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortCommandsEditForm));
            this.dgvPortCommands = new System.Windows.Forms.DataGridView();
            this.btAddRow = new System.Windows.Forms.Button();
            this.btInsertRow = new System.Windows.Forms.Button();
            this.btDeleteRow = new System.Windows.Forms.Button();
            this.btMoveUp = new System.Windows.Forms.Button();
            this.btMoveDown = new System.Windows.Forms.Button();
            this.btCopyRow = new System.Windows.Forms.Button();
            this.btPasteRow = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btResetDefault = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPortCommands
            // 
            this.dgvPortCommands.AllowUserToAddRows = false;
            this.dgvPortCommands.AllowUserToDeleteRows = false;
            this.dgvPortCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPortCommands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPortCommands.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvPortCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPortCommands.Location = new System.Drawing.Point(16, 15);
            this.dgvPortCommands.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPortCommands.MultiSelect = false;
            this.dgvPortCommands.Name = "dgvPortCommands";
            this.dgvPortCommands.ReadOnly = true;
            this.dgvPortCommands.RowTemplate.Height = 23;
            this.dgvPortCommands.Size = new System.Drawing.Size(1221, 465);
            this.dgvPortCommands.TabIndex = 0;
            this.dgvPortCommands.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPortCommands_CellLeave);
            this.dgvPortCommands.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPortCommands_CellMouseMove);
            this.dgvPortCommands.DoubleClick += new System.EventHandler(this.dgvPortCommands_DoubleClick);
            // 
            // btAddRow
            // 
            this.btAddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAddRow.Location = new System.Drawing.Point(16, 488);
            this.btAddRow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAddRow.Name = "btAddRow";
            this.btAddRow.Size = new System.Drawing.Size(100, 29);
            this.btAddRow.TabIndex = 2;
            this.btAddRow.Text = "Add Row";
            this.btAddRow.UseVisualStyleBackColor = true;
            this.btAddRow.Click += new System.EventHandler(this.btAddRow_Click);
            // 
            // btInsertRow
            // 
            this.btInsertRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btInsertRow.Location = new System.Drawing.Point(125, 488);
            this.btInsertRow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btInsertRow.Name = "btInsertRow";
            this.btInsertRow.Size = new System.Drawing.Size(100, 29);
            this.btInsertRow.TabIndex = 3;
            this.btInsertRow.Text = "Insert Row";
            this.btInsertRow.UseVisualStyleBackColor = true;
            this.btInsertRow.Click += new System.EventHandler(this.btInsertRow_Click);
            // 
            // btDeleteRow
            // 
            this.btDeleteRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDeleteRow.Location = new System.Drawing.Point(235, 488);
            this.btDeleteRow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btDeleteRow.Name = "btDeleteRow";
            this.btDeleteRow.Size = new System.Drawing.Size(100, 29);
            this.btDeleteRow.TabIndex = 4;
            this.btDeleteRow.Text = "Delete Row";
            this.btDeleteRow.UseVisualStyleBackColor = true;
            this.btDeleteRow.Click += new System.EventHandler(this.btDeleteRow_Click);
            // 
            // btMoveUp
            // 
            this.btMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btMoveUp.Location = new System.Drawing.Point(563, 488);
            this.btMoveUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btMoveUp.Name = "btMoveUp";
            this.btMoveUp.Size = new System.Drawing.Size(100, 29);
            this.btMoveUp.TabIndex = 5;
            this.btMoveUp.Text = "Move Up";
            this.btMoveUp.UseVisualStyleBackColor = true;
            this.btMoveUp.Click += new System.EventHandler(this.btMoveUp_Click);
            // 
            // btMoveDown
            // 
            this.btMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btMoveDown.Location = new System.Drawing.Point(672, 488);
            this.btMoveDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btMoveDown.Name = "btMoveDown";
            this.btMoveDown.Size = new System.Drawing.Size(100, 29);
            this.btMoveDown.TabIndex = 6;
            this.btMoveDown.Text = "Move Down";
            this.btMoveDown.UseVisualStyleBackColor = true;
            this.btMoveDown.Click += new System.EventHandler(this.btMoveDown_Click);
            // 
            // btCopyRow
            // 
            this.btCopyRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCopyRow.Location = new System.Drawing.Point(344, 488);
            this.btCopyRow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btCopyRow.Name = "btCopyRow";
            this.btCopyRow.Size = new System.Drawing.Size(100, 29);
            this.btCopyRow.TabIndex = 8;
            this.btCopyRow.Text = "Copy Row";
            this.btCopyRow.UseVisualStyleBackColor = true;
            this.btCopyRow.Click += new System.EventHandler(this.btCopyRow_Click);
            // 
            // btPasteRow
            // 
            this.btPasteRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btPasteRow.Location = new System.Drawing.Point(453, 488);
            this.btPasteRow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btPasteRow.Name = "btPasteRow";
            this.btPasteRow.Size = new System.Drawing.Size(100, 29);
            this.btPasteRow.TabIndex = 9;
            this.btPasteRow.Text = "Paste Row";
            this.btPasteRow.UseVisualStyleBackColor = true;
            this.btPasteRow.Click += new System.EventHandler(this.btPasteRow_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.Location = new System.Drawing.Point(1015, 488);
            this.btOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(100, 29);
            this.btOK.TabIndex = 10;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCancel.Location = new System.Drawing.Point(1123, 488);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 29);
            this.btCancel.TabIndex = 11;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btResetDefault
            // 
            this.btResetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btResetDefault.Location = new System.Drawing.Point(781, 488);
            this.btResetDefault.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btResetDefault.Name = "btResetDefault";
            this.btResetDefault.Size = new System.Drawing.Size(100, 29);
            this.btResetDefault.TabIndex = 12;
            this.btResetDefault.Text = "Reset Default";
            this.btResetDefault.UseVisualStyleBackColor = true;
            this.btResetDefault.Click += new System.EventHandler(this.btResetDefault_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btEdit.Location = new System.Drawing.Point(889, 488);
            this.btEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(100, 29);
            this.btEdit.TabIndex = 13;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // PortCommandsEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 521);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btResetDefault);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btPasteRow);
            this.Controls.Add(this.btCopyRow);
            this.Controls.Add(this.btMoveDown);
            this.Controls.Add(this.btMoveUp);
            this.Controls.Add(this.btDeleteRow);
            this.Controls.Add(this.btInsertRow);
            this.Controls.Add(this.btAddRow);
            this.Controls.Add(this.dgvPortCommands);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "PortCommandsEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PortCommandsEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortCommands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPortCommands;
        private System.Windows.Forms.Button btAddRow;
        private System.Windows.Forms.Button btInsertRow;
        private System.Windows.Forms.Button btDeleteRow;
        private System.Windows.Forms.Button btMoveUp;
        private System.Windows.Forms.Button btMoveDown;
        private System.Windows.Forms.Button btCopyRow;
        private System.Windows.Forms.Button btPasteRow;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btResetDefault;
        private System.Windows.Forms.Button btEdit;
    }
}