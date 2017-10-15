namespace ManhwaReader.Forms
{
    partial class ClickOverlayForm
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
            this.leftClickPanel = new System.Windows.Forms.Panel();
            this.rightClickPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // leftClickPanel
            // 
            this.leftClickPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.leftClickPanel.Location = new System.Drawing.Point(12, 12);
            this.leftClickPanel.Name = "leftClickPanel";
            this.leftClickPanel.Size = new System.Drawing.Size(215, 352);
            this.leftClickPanel.TabIndex = 0;
            this.leftClickPanel.Click += new System.EventHandler(this.OnLeftPanelClick);
            // 
            // rightClickPanel
            // 
            this.rightClickPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rightClickPanel.Location = new System.Drawing.Point(262, 12);
            this.rightClickPanel.Name = "rightClickPanel";
            this.rightClickPanel.Size = new System.Drawing.Size(222, 352);
            this.rightClickPanel.TabIndex = 1;
            this.rightClickPanel.Click += new System.EventHandler(this.OnRightPanelClick);
            // 
            // ClickOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 376);
            this.Controls.Add(this.rightClickPanel);
            this.Controls.Add(this.leftClickPanel);
            this.Name = "ClickOverlay";
            this.Text = "ClickOverlay";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel leftClickPanel;
        private System.Windows.Forms.Panel rightClickPanel;
    }
}