namespace ManhwaReader.Views
{
    partial class NewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.testButton = new System.Windows.Forms.ToolStripButton();
            this.fullScreenButton = new System.Windows.Forms.ToolStripButton();
            this.testLabel = new System.Windows.Forms.ToolStripLabel();
            this.dualSplitterContainer = new ManhwaReader.Controls.DualSplitterContainer();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.testButton,
            this.fullScreenButton,
            this.testLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(847, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // openFileButton
            // 
            this.openFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openFileButton.Image = ((System.Drawing.Image)(resources.GetObject("openFileButton.Image")));
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(23, 22);
            this.openFileButton.Text = "toolStripButton1";
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // testButton
            // 
            this.testButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.testButton.Image = ((System.Drawing.Image)(resources.GetObject("testButton.Image")));
            this.testButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(23, 22);
            this.testButton.Text = "testButton";
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // fullScreenButton
            // 
            this.fullScreenButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fullScreenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fullScreenButton.Image = ((System.Drawing.Image)(resources.GetObject("fullScreenButton.Image")));
            this.fullScreenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fullScreenButton.Name = "fullScreenButton";
            this.fullScreenButton.Size = new System.Drawing.Size(23, 22);
            this.fullScreenButton.Text = "toolStripButton3";
            this.fullScreenButton.Click += new System.EventHandler(this.fullScreenButton_Click);
            // 
            // testLabel
            // 
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(86, 22);
            this.testLabel.Text = "toolStripLabel1";
            // 
            // dualSplitterContainer
            // 
            this.dualSplitterContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dualSplitterContainer.IsMainPanelOnly = false;
            this.dualSplitterContainer.Location = new System.Drawing.Point(0, 28);
            this.dualSplitterContainer.Name = "dualSplitterContainer";
            this.dualSplitterContainer.Size = new System.Drawing.Size(847, 445);
            this.dualSplitterContainer.SplitterColor = System.Drawing.Color.Pink;
            this.dualSplitterContainer.SplittersHidden = false;
            this.dualSplitterContainer.SplitterWidth = 0;
            this.dualSplitterContainer.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 473);
            this.Controls.Add(this.dualSplitterContainer);
            this.Controls.Add(this.toolStrip);
            this.Name = "NewForm";
            this.Text = "NewForm";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.DualSplitterContainer dualSplitterContainer;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.ToolStripButton testButton;
        private System.Windows.Forms.ToolStripButton fullScreenButton;
        private System.Windows.Forms.ToolStripLabel testLabel;
    }
}