using ManhwaReader.Controls;

namespace ManhwaReader
{
    partial class MainForm : CoverableForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripFullScreenBtn = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.mainContainerPanel = new ManhwaReader.Controls.ScrollablePanel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.mainContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.toolStripLabel1,
            this.toolStripFullScreenBtn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(982, 25);
            this.toolStrip.TabIndex = 1;
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
            this.openFileButton.Click += new System.EventHandler(this.OnOpenFileButtonClick);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // toolStripFullScreenBtn
            // 
            this.toolStripFullScreenBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripFullScreenBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFullScreenBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFullScreenBtn.Image")));
            this.toolStripFullScreenBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFullScreenBtn.Name = "toolStripFullScreenBtn";
            this.toolStripFullScreenBtn.Size = new System.Drawing.Size(23, 22);
            this.toolStripFullScreenBtn.Text = "toolStripButton1";
            this.toolStripFullScreenBtn.Click += new System.EventHandler(this.OnFullScreenBtnClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openImageFileDialog";
            this.openFileDialog.Filter = "prouti files (*.prouti)|*.prouti|Pictures (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|A" +
    "ll files (*.*)|*.*";
            this.openFileDialog.FilterIndex = 2;
            this.openFileDialog.InitialDirectory = "D:\\Manhwa\\Tamen De Machin\\";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(982, 446);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.Resize += new System.EventHandler(this.OnMainPictureBoxResize);
            // 
            // mainContainerPanel
            // 
            this.mainContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContainerPanel.AutoScroll = true;
            this.mainContainerPanel.Controls.Add(this.mainPictureBox);
            this.mainContainerPanel.Location = new System.Drawing.Point(0, 28);
            this.mainContainerPanel.Name = "mainContainerPanel";
            this.mainContainerPanel.Size = new System.Drawing.Size(982, 446);
            this.mainContainerPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 471);
            this.Controls.Add(this.mainContainerPanel);
            this.Controls.Add(this.toolStrip);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainForm";
            this.Text = "ManhwaReader";
            this.SizeChanged += new System.EventHandler(this.OnFrameSizeChanged);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.mainContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripFullScreenBtn;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private ScrollablePanel mainContainerPanel;
    }
}

