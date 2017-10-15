﻿using ManhwaReader.Presenters;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManhwaReader.Forms
{
    public partial class ClickOverlayForm : TransparentOverlayForm
    {

        private MainForm _mainForm;
        private IMainPresenter _presenter;

        public ClickOverlayForm (CoverableForm owner, IMainPresenter presenter) : base(owner)
        {
            _mainForm = (MainForm)owner;
            _presenter = presenter;
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
            _presenter.LoadPreviousPicture();
        }
        
        private void OnRightPanelClick(object sender, EventArgs e)
        {
            _presenter.LoadNextPicture();
        }
    }
}