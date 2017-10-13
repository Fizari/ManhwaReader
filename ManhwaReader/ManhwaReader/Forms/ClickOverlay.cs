using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader.Forms
{
    public partial class ClickOverlay : TransparentOverlayForm
    {

        private MainForm _mainForm;

        public ClickOverlay (CoverableForm owner) : base(owner)
        {
            _mainForm = (MainForm)owner;
            InitializeComponent();
            MouseWheel += OnMouseWheel;
            ResizeClickPanels();
            _mainForm.PictureLoaded += OnPictureLoaded;
        }

        private void OnPictureLoaded(object sender, EventArgs e)
        {
            base.Cover_ClientSizeChanged(sender, e);
            ResizeClickPanels();
        }

        private void OnMouseWheel(object sender, MouseEventArgs me)
        {
            _mainForm.MouseWheeled(sender, me);
        }

        public override void Cover_LocationChanged(object sender, EventArgs e)
        {
            base.Cover_LocationChanged(sender, e);
            ResizeClickPanels();
        }

        public override void Cover_ClientSizeChanged(object sender, EventArgs e)
        {
            base.Cover_ClientSizeChanged(sender, e);
            ResizeClickPanels();
        }

        private void ResizeClickPanels()
        {
            var width = (Width / 2) - 2;
            var height = Height;

            this.leftClickPanel.Size = new Size(width, height);
            this.leftClickPanel.Location = Point.Empty;

            this.rightClickPanel.Size = new Size(width, height);
            this.rightClickPanel.Location = new Point(width + 2, 0);
        }

        private void OnLeftPanelClick(object sender, EventArgs e)
        {
            _mainForm.LoadPreviousPicture();
        }
        
        private void OnRightPanelClick(object sender, EventArgs e)
        {
            _mainForm.LoadNextPicture();
        }
    }
}
