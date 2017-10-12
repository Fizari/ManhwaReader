using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader
{
    public partial class ManhwaReader : Form
    {
        private bool isFullScreen = false;

        public ManhwaReader()
        {
            this.KeyPreview = true;
            InitializeComponent();
        }

        private void LoadFile (string filePath)
        {
            this.toolStripLabel1.Text = filePath;
            this.mainPictureBox.Image = Image.FromFile(filePath);
            ResizePicture();
        }

        private void ShowOpenFileDialog()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadFile(filePath);
            }
        }

        private void ResizePicture()
        {
            var img = mainPictureBox.Image;
            var ratio = Convert.ToDouble(img.Height) / img.Width;
            this.mainPictureBox.Height = Convert.ToInt32((this.mainPictureBox.Width * ratio));
        }

        private void SwitchFullScreen()
        {
            if (isFullScreen)
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            isFullScreen = this.TopMost == true;
        }

        private void OnMainPictureBoxResize(object sender, EventArgs e)
        {
            if (mainPictureBox.Image != null)
            {
                ResizePicture();
            }
        }
        
        private void OnOpenFileButtonClick(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }

        private void OnFullScreenBtnClick(object sender, EventArgs e)
        {
            SwitchFullScreen();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && isFullScreen)
            {
                SwitchFullScreen();
                return true;
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                ShowOpenFileDialog();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Enter))
            {
                SwitchFullScreen();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
