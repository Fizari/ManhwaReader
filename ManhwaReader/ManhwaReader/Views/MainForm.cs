using ManhwaReader.Forms;
using ManhwaReader.Model;
using ManhwaReader.Presenters;
using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace ManhwaReader
{
    public partial class MainForm : CoverableForm
    {
        private ClickOverlayForm _clickOverlayForm;

        private static int _scrollStepValue = 120;//TODO user can change this value within options

        private IMainPresenter _presenter;

        public event EventHandler PictureLoaded;
        
        public MainForm()
        {
            _presenter = new MainPresenter(this);
            this.KeyPreview = true;
            InitializeComponent();
            _clickOverlayForm = new ClickOverlayForm(this,_presenter);
            _clickOverlayForm.Show();
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

        public void LoadFile (string filePath)
        {
            this.toolStripLabel1.Text = filePath;
            this.mainPictureBox.Image = Image.FromFile(filePath);
            this.mainContainerPanel.AutoScrollPosition = new Point(0, 0);
            ResizePicture();
        }
        
        public void ShowOpenFileDialog()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                _presenter.OpenChosenFile(filePath);
            }
        }

        private void ResizePicture()
        {
            var bla = this.mainContainerPanel;
            var img = mainPictureBox.Image;
            var ratio = Convert.ToDouble(img.Height) / img.Width;
            this.mainPictureBox.Height = Convert.ToInt32((this.mainPictureBox.Width * ratio));
        }

        public void EnableFullScreen()
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        public void DisableFullScreen ()
        {
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
        }
        
        public void ScrollMainPanel(bool up)
        {
            var step = _scrollStepValue;
            step = up ? step * -1 : step;
            var scrollValue = mainContainerPanel.VerticalScroll.Value;
            var newValue = 0;
            if (scrollValue + step < 0)
                newValue = 0;
            else if (scrollValue + step > mainContainerPanel.VerticalScroll.Maximum)
                newValue = mainContainerPanel.VerticalScroll.Maximum;
            else
                newValue = scrollValue + step;
            mainContainerPanel.AutoScrollPosition =
            new Point(0, newValue);
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
            _presenter.ShowFileChooser();
        }

        private void OnFullScreenBtnClick(object sender, EventArgs e)
        {
            _presenter.SwitchFullScreen();
        }
        private void OnFrameSizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //AFTER minimized...
            }
            if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            {
                
                mainContainerPanel.VerticalScroll.Value = _presenter.GetVerticalScrollPosition();
                mainContainerPanel.PerformLayout();
                
            }
        }

        #endregion

        #region windows behavior biding
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var res = _presenter.KeyPressed(ref msg, keyData);
            return res ? res : base.ProcessCmdKey(ref msg, keyData);
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
                        _presenter.RegisterState(mainContainerPanel.VerticalScroll.Value);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
