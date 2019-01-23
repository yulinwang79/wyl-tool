namespace WYL
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsLoad = new System.Windows.Forms.ToolStripButton();
            this.tsFontView = new System.Windows.Forms.ToolStripButton();
            this.tsOLFont = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLoad,
            this.tsOLFont,
            this.tsFontView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(749, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsLoad
            // 
            this.tsLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsLoad.Image")));
            this.tsLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLoad.Name = "tsLoad";
            this.tsLoad.Size = new System.Drawing.Size(41, 37);
            this.tsLoad.Text = "Load";
            this.tsLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsLoad.Click += new System.EventHandler(this.tsLoad_Click);
            // 
            // tsFontView
            // 
            this.tsFontView.Image = ((System.Drawing.Image)(resources.GetObject("tsFontView.Image")));
            this.tsFontView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFontView.Name = "tsFontView";
            this.tsFontView.Size = new System.Drawing.Size(64, 37);
            this.tsFontView.Text = "FontView";
            this.tsFontView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsFontView.ToolTipText = "FontView";
            this.tsFontView.Click += new System.EventHandler(this.tsFontView_Click);
            // 
            // tsOLFont
            // 
            this.tsOLFont.Image = ((System.Drawing.Image)(resources.GetObject("tsOLFont.Image")));
            this.tsOLFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOLFont.Name = "tsOLFont";
            this.tsOLFont.Size = new System.Drawing.Size(100, 37);
            this.tsOLFont.Text = "Only Load Font";
            this.tsOLFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOLFont.Click += new System.EventHandler(this.tsOLFont_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 433);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsFontView;
        private System.Windows.Forms.ToolStripButton tsLoad;
        private System.Windows.Forms.ToolStripButton tsOLFont;
    }
}