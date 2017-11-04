using ManhwaReader.Model;
using ManhwaReader.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader.Views
{
    public partial class MainForm : AlertForm
    {
        private static int _scrollStepValue = 120;//TODO user can change this value in 'options'
        private static Color _backgroundColor = Color.Black;//TODO user can change background color in 'options'
        private static Color _splitterColor = Color.Blue;//TODO what can we do with that ?

        private IMainPresenter _presenter;
        private PictureBox _pictureBox;
        private Panel _pContainer;//pictureBoxContainer
        private DrawingPool _drawingPool;

        public MainForm()
        {
            _presenter = new MainPresenter(this);
            this.KeyPreview = true;
            InitializeComponent();
            //pictureBoxContainer
            _pContainer = new Panel();
            _pContainer.AutoScroll = true;
            _pContainer.Dock = DockStyle.Fill;
            dualSplitterContainer.MainPanel.Controls.Add(_pContainer);
            //pictureBox
            _pictureBox = new PictureBox();
            _pContainer.Controls.Add(_pictureBox);
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _pictureBox.Location = Point.Empty;
            _pictureBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
            //right panel
            dualSplitterContainer.RightPanel.BackColor = _backgroundColor;
            //left panel
            dualSplitterContainer.LeftPanel.BackColor = _backgroundColor;
            //splitters
            dualSplitterContainer.SplitterColor = _splitterColor;
            dualSplitterContainer.SplitterWidth = 10;
            //drawingPool
            _drawingPool = new DrawingPool();
            dualSplitterContainer.DrawingPool = _drawingPool;

            dualSplitterContainer.FinishedDrawing += OnDSCFinishedDrawing;
            dualSplitterContainer.Init();
        }

        private void OnDSCFinishedDrawing(object sender, EventArgs e)
        {
            if (_pictureBox.Image != null)
                ResizePicture();
            _drawingPool.ResumeDrawing();
        }

        public void DisplayFile(string filePath)
        {
            Text = filePath;
            _pictureBox.Image = Image.FromFile(filePath);
            _pContainer.AutoScrollPosition = new Point(0, 0);
            ResizePicture();
        }
        
        private void ResizePicture()
        {
            var img = _pictureBox.Image;
            var ratio = Convert.ToDouble(img.Height) / img.Width;
            var width = _pContainer.ClientSize.Width;
            var newHeight = Convert.ToInt32(width * ratio);
            var newSize = new Size(width, newHeight);
            _pictureBox.Height = newHeight;

            testLabel.Text = _pictureBox.Height+"";
        }

        public void ShowOpenFileDialog()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                _presenter.OpenChosenFile(filePath);
            }
        }
        
        public void EnableFullScreen()
        {
            //this.TopMost = true;
            var formerSize = _pictureBox.Width;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            dualSplitterContainer.Location = Point.Empty;
            dualSplitterContainer.Height = this.Height;
        }

        public void DisableFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;

            dualSplitterContainer.Location = new Point(0,toolStrip.Height);
            dualSplitterContainer.Height = this.Height - toolStrip.Height;
        }

        public void EnableNormalMode()
        {
            dualSplitterContainer.IsMainPanelOnly = false;
        }

        public void EnableMainPanelOnlyMode()
        {
            dualSplitterContainer.IsMainPanelOnly = true;
            
        }

        public void ScrollMainPanel(bool up)
        {
            var step = _scrollStepValue;
            step = up ? step * -1 : step;
            var scrollValue = _pContainer.VerticalScroll.Value;
            var newValue = 0;
            if (scrollValue + step < 0)
                newValue = 0;
            else if (scrollValue + step > _pContainer.VerticalScroll.Maximum)
                newValue = _pContainer.VerticalScroll.Maximum;
            else
                newValue = scrollValue + step;
            _pContainer.AutoScrollPosition = new Point(0, newValue);
        }

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
                        _presenter.RegisterState(_pContainer.VerticalScroll.Value);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region controls binding

        private void openFileButton_Click(object sender, EventArgs e)
        {
            _presenter.ShowFileChooser();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            _presenter.SwitchModes();
        }

        private void fullScreenButton_Click(object sender, EventArgs e)
        {
            _presenter.SwitchFullScreen();
        }

        #endregion
    }
}
