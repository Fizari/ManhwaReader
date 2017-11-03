using ManhwaReader.Extensions;
using ManhwaReader.Model;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ManhwaReader.Controls
{
    public class VerticalSplitter : TransparentPanel
    {
        private bool _isDragged;
        private Point _previousLocation;

        public event EventHandler SplitValueChanged;

        public int MaximumPosition { get; set; }
        public int MinimumPosition { get; set; }
        public int Position
        {
            get
            {
                return Location.X;
            }
            set
            {
                Location = new Point(value,Location.Y);
                OnSplitValueChanged();
            }
        }
        private bool _locked = true;
        public bool Locked
        {
            get { return _locked; }
            set
            {
                if (value != _locked)
                {
                    if (value)
                    {
                        UnbindEventHandler();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        BindEventHandler();
                        this.Cursor = Cursors.VSplit;
                    }
                    _locked = value;
                }
            }
        }
        public DrawingPool DrawingPool { get; set; }

        public VerticalSplitter()
        {
            MaximumPosition = UInt16.MaxValue;
            MinimumPosition = 0;
            Locked = false;
        }

        public VerticalSplitter (int minimum, int maximum)
        {
            

            MaximumPosition = maximum;
            MinimumPosition = minimum;

            Locked = false;
        }

        private void OnSplitValueChanged()
        {
            if (SplitValueChanged != null)
                SplitValueChanged(this, new EventArgs());
        }

        private void BindEventHandler ()
        {
            this.MouseMove += OnMouseMove;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
        }

        private void UnbindEventHandler()
        {
            this.MouseMove -= OnMouseMove;
            this.MouseDown -= OnMouseDown;
            this.MouseUp -= OnMouseUp;
        }

        void OnMouseDown(object sender, MouseEventArgs e)
        {
            _isDragged = true;
            _previousLocation = e.Location;
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragged)
                return;

            if (DrawingPool != null)
                DrawingPool.SuspendDrawing();

            var l = Location;
            l.Offset(e.Location.X - _previousLocation.X, 0);

            if (l.X < MinimumPosition)
            {
                Location = new Point(MinimumPosition, Location.Y);
            }
            else if (l.X > MaximumPosition - Width)
            {
                Location = new Point(MaximumPosition - Width, Location.Y);
            }
            else
            {
                if (l != Location)
                {
                    Location = l;
                    OnSplitValueChanged();
                }
                else
                {
                    if (DrawingPool != null)
                        DrawingPool.ResumeAndDraw();
                }
            }
        }

        void OnMouseUp(object sender, MouseEventArgs e)
        {
            _isDragged = false;
        }

    }
}
