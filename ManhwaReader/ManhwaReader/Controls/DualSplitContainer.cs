using System;
using System.Drawing;
using System.Windows.Forms;
using ManhwaReader.Extensions;
using ManhwaReader.Model;

namespace ManhwaReader.Controls
{
    public class DualSplitContainer : Panel
    {
        private VerticalSplitter _leftSplitter;
        private VerticalSplitter _rightSplitter;
        private TransparentPanel _mainpanel;
        private TransparentPanel _leftPanel;
        private TransparentPanel _rightPanel;

        public event EventHandler ValuesChanged;
        public event EventHandler FinishedDrawing;

        private double _ratioLS = 1.0;
        private double _ratioLimitL = 1.0;
        private double _ratioLimitML = 1.0;
        private int _splitterWidth = 20;
        private Color _splitterColor = Color.Pink;
        private int _savedSplitterWidth;
        private bool _splitterHidden;
        private bool _locked;
        private bool _mainPanelOnly = false;
        private DrawingPool _drawingPool;

        public bool Locked {
            get
            {
                return _locked;
            }
            set
            {
                if (value)
                {
                    Lock();
                }
                else
                {
                    Unlock();
                }
                _locked = value;
            }
        }
        public bool SplittersHidden
        {
            get
            {
                return _splitterHidden;
            }
            set
            {
                if (value)
                {
                    _savedSplitterWidth = SplitterWidth;
                    SplitterWidth = 0;
                }
                else
                {
                    SplitterWidth = _savedSplitterWidth;
                }
                _splitterHidden = value;
            }
        }
        public int SplitterWidth
        {
            get
            {
                return _splitterWidth;
            }
            set
            {
                _splitterWidth = value;
                _leftSplitter.Width = value;
                _rightSplitter.Width = value;
                OnValuesChanged();
            }
        }
        public Color SplitterColor
        {
            get
            {
                return _splitterColor;
            }
            set
            {
                this._leftSplitter.BackColor = value;
                this._rightSplitter.BackColor = value;
                _splitterColor = value;
            }
        }
        public bool IsMainPanelOnly
        {
            get
            {
                return _mainPanelOnly;
            }
            set
            {
                if (_mainPanelOnly != value)
                {
                    _mainPanelOnly = value;
                    EnableMainPanelOnly(_mainPanelOnly);
                }
            }
        }
        public DrawingPool DrawingPool
        {
            get
            {
                return _drawingPool;
            }
            set
            {
                _drawingPool = value;
                if (_drawingPool != null)
                {
                    _leftSplitter.DrawingPool = _drawingPool;
                    _rightSplitter.DrawingPool = _drawingPool;
                    _drawingPool.Register(_leftPanel);
                    _drawingPool.Register(_leftSplitter);
                    _drawingPool.Register(_rightPanel);
                    _drawingPool.Register(_rightSplitter);
                    _drawingPool.Register(_mainpanel);
                }
            }
        }

        public TransparentPanel LeftPanel
        {
            get
            {
                return _leftPanel;
            }
        }
        public TransparentPanel RightPanel
        {
            get
            {
                return _rightPanel;
            }
        }
        public TransparentPanel MainPanel
        {
            get
            {
                return _mainpanel;
            }
        }
        
        private void OnValuesChanged ()
        {
            if (ValuesChanged != null)
                ValuesChanged(this, new EventArgs());
            DrawPanels();
        }
        
        private void OnDrawingFinished()
        {
            if (FinishedDrawing != null)
                FinishedDrawing(this, new EventArgs());
        }

        public DualSplitContainer ()
        {
            _leftSplitter = new VerticalSplitter(0,this.Width/2);
            _rightSplitter = new VerticalSplitter(this.Width / 2, this.Width);

            _mainpanel = new TransparentPanel();
            _leftPanel = new TransparentPanel();
            _rightPanel = new TransparentPanel();

            _leftSplitter.BackColor = Color.Cyan;
            _rightSplitter.BackColor = Color.Blue;
            _mainpanel.BackColor = Color.Red;
            _leftPanel.BackColor = Color.Green;
            _rightPanel.BackColor = Color.Pink;

            this.Controls.AddRange( new Control[] {
                _mainpanel,
                _leftPanel,
                _rightPanel,
                _leftSplitter,
                _rightSplitter
            });

            _leftPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom);
            _rightPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
            _mainpanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);

            //_rightSplitter.LocationChanged += RSplitterlocationChanged;
            //_leftSplitter.LocationChanged += LSplitterlocationChanged;
            _rightSplitter.SplitValueChanged += RSplitterlocationChanged;
            _leftSplitter.SplitValueChanged += LSplitterlocationChanged;

            Locked = false;
        }

        public void Init(int postionLeftSplitter)
        {
            _leftSplitter.Height = Height;
            _rightSplitter.Height = Height;
            _leftSplitter.Top = 0;
            _rightSplitter.Top = 0;

            _leftSplitter.Position = postionLeftSplitter;
            syncSplitters(_leftSplitter, _rightSplitter);
            
            //todo change on init
            SetLimits(0, (Width / 2));
            this.Resize += OnWindowResize;
            OnWindowResize(this, new EventArgs());
        }
        
        public void Init()
        {
            Init(0);
        }

        public void Lock()
        {
            _leftSplitter.Locked = true;
            _rightSplitter.Locked = true;
        }

        public void Unlock()
        {
            _leftSplitter.Locked = false;
            _rightSplitter.Locked = false;
        }

        private void EnableMainPanelOnly(bool enable)
        {
            if (enable)
            {
                this._mainpanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                this._mainpanel.Location = Point.Empty;
                this._mainpanel.Size = new Size(this.Width, this.Height);
                DrawPanels();
            }
            else
            {
                this._mainpanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom );
                DrawPanels();
            }
        }

        private void OnWindowResize(object sender, EventArgs e)
        {
            if (DrawingPool != null)
                DrawingPool.SuspendDrawing();
            
            UpdateLimits();

            _leftSplitter.Position = Convert.ToInt32((_leftSplitter.MaximumPosition - _leftSplitter.MinimumPosition) * _ratioLS) +( _leftSplitter.MinimumPosition);
            syncSplitters(_leftSplitter, _rightSplitter);
        }

        private void LSplitterlocationChanged(object sender, EventArgs e)
        {
            SplitterLocationChanged(_leftSplitter, _rightSplitter, RSplitterlocationChanged);
        }

        private void RSplitterlocationChanged(object sender, EventArgs e)
        {
            SplitterLocationChanged(_rightSplitter, _leftSplitter, LSplitterlocationChanged);
        }

        private void SplitterLocationChanged(VerticalSplitter movingSplitter, VerticalSplitter coupledSplitter,  EventHandler coupledSplitterHandler)
        {
            coupledSplitter.SplitValueChanged -= coupledSplitterHandler;
            syncSplitters(movingSplitter, coupledSplitter);
            coupledSplitter.SplitValueChanged += coupledSplitterHandler;
            DrawPanels();

            _ratioLS = Convert.ToDouble(_leftSplitter.Position) / (_leftSplitter.MaximumPosition - _leftSplitter.MinimumPosition);
        }

        private void syncSplitters(VerticalSplitter movedSplitter, VerticalSplitter splitterToSync)
        {
            var newX = movedSplitter.Location.X;
            splitterToSync.Position = this.Width - newX - splitterToSync.Width;
        }

        public void SetLimits(int left, int middleLeft)
        {
            this._ratioLimitL = Convert.ToDouble(left) / Width;
            this._ratioLimitML = Convert.ToDouble(middleLeft) / Width;

            UpdateLimits();
        }

        public void UpdateLimits ()
        {
            var left = Convert.ToInt32(Width * _ratioLimitL);
            var middleLeft = Convert.ToInt32(Width * _ratioLimitML);

            _leftSplitter.MinimumPosition = left;
            _leftSplitter.MaximumPosition = middleLeft;

            var mid = Width / 2;
            _rightSplitter.MinimumPosition = mid + (mid - middleLeft);
            _rightSplitter.MaximumPosition = Width - left;
        }
        
        public void DrawPanels()
        {
            if (IsMainPanelOnly)
            {
                OnDrawingFinished();
                return;
            }

            _leftPanel.Width = _leftSplitter.Location.X;

            _mainpanel.Location = new Point (_leftSplitter.Location.X + _leftSplitter.Width,0);
            _mainpanel.Width = _rightSplitter.Location.X - (_leftSplitter.Location.X + _leftSplitter.Width);

            _rightPanel.Width = this.Width - (_rightSplitter.Location.X + _rightSplitter.Width);
            _rightPanel.Location = new Point(_rightSplitter.Location.X + _rightSplitter.Width);

            _leftSplitter.Height = Height;
            _rightSplitter.Height = Height;

            OnDrawingFinished();

        }
    }
}
