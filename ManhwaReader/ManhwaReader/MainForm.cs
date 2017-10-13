using ManhwaReader.Forms;
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
    public partial class MainForm : CoverableForm
    {
        private bool _isFullScreen = false;
        private FolderData _folderData;

        public event EventHandler PictureLoaded;
        
        public MainForm()
        {
            this.KeyPreview = true;
            InitializeComponent();
            var clickOverlayForm = new ClickOverlay(this);
            clickOverlayForm.Show();
        }

        public override Size CoverableArea()
        {
            return this.mainContainerPanel.ClientSize;
        }
        public override Point CoverableLocation()
        {
            return this.mainContainerPanel.Bounds.Location;
        }

        public override void MouseWheeled(object sender, MouseEventArgs me)
        {
            mainContainerPanel.MouseWheeled(me);
        }

        public virtual void OnPictureLoaded (EventArgs e)
        {
            if (PictureLoaded != null)
                PictureLoaded(this, e);
        }

        private void QuickLoadFile (string filePath)
        {
            this.toolStripLabel1.Text = filePath;
            this.mainPictureBox.Image = Image.FromFile(filePath);
            this.mainContainerPanel.AutoScrollPosition = new Point(0, 0);
            ResizePicture();
        }

        private void LoadFile (string filePath)
        {
            _folderData = new FolderData(filePath);
            QuickLoadFile(filePath);
            OnPictureLoaded(new EventArgs());
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
            var bla = this.mainContainerPanel;
            var img = mainPictureBox.Image;
            var ratio = Convert.ToDouble(img.Height) / img.Width;
            this.mainPictureBox.Height = Convert.ToInt32((this.mainPictureBox.Width * ratio));
        }

        private void SwitchFullScreen()
        {
            if (_isFullScreen)
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
            _isFullScreen = this.TopMost == true;
        }

        public void LoadNextPicture()
        {
            var nextFilePath = _folderData.GetNextFilePath();
            QuickLoadFile(nextFilePath);
        }

        public void LoadPreviousPicture()
        {
            var previousFilePath = _folderData.GetPreviousFilePath();
            QuickLoadFile(previousFilePath);
        }

        #region Events Handlers

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

        private void OnLeftClickPanel(object sender, EventArgs e)
        {
            LoadPreviousPicture();
        }

        private void OnRightClickPanel(object sender, EventArgs e)
        {
            LoadNextPicture();
        }

        #endregion

        #region key biding
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && _isFullScreen)
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
            if (keyData == Keys.Right)
            {
                LoadNextPicture();
                return true;
            }
            if (keyData == Keys.Left)
            {
                LoadPreviousPicture();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion
        
    }
}
