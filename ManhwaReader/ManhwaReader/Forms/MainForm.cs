using ManhwaReader.Forms;
using ManhwaReader.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader
{
    public partial class MainForm : CoverableForm
    {
        private bool _isFullScreen = false;
        private FolderData _folderData;
        private ReaderState _state;

        public event EventHandler PictureLoaded;
        
        public MainForm()
        {
            this.KeyPreview = true;
            _folderData = new FolderData();
            _state = new ReaderState();
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
            _folderData.Load(filePath);
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

        public void ScrollMainPanel(bool up)
        {
            var step = 120;
            step = up ? step * -1 : step;
            var scrollValue = mainContainerPanel.VerticalScroll.Value;
            var newValue = 0;
            if (scrollValue + step < 0)
                newValue = 0;
            else if (scrollValue + step > mainContainerPanel.VerticalScroll.Maximum)
                newValue = VerticalScroll.Maximum;
            else
                newValue = scrollValue + step;
            mainContainerPanel.VerticalScroll.Value = newValue;
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

        private void OnFrameSizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //AFTER minimized...
            }
            if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            {
                mainContainerPanel.VerticalScroll.Value = _state.VerticalScrollPosition;
                mainContainerPanel.PerformLayout();
            }
        }

        #endregion

        #region windows behavior biding
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
            if (keyData == Keys.Down)
            {
                ScrollMainPanel(false);
                return true;
            }
            if (keyData == Keys.Up)
            {
                ScrollMainPanel(true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xF020;

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MINIMIZE) //Window BEFORE minimized...
                    {
                        _state.VerticalScrollPosition = mainContainerPanel.VerticalScroll.Value;
                        _state.File = _folderData.GetCurrentFile();
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
