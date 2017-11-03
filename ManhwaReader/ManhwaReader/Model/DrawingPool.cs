using ManhwaReader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader.Model
{
    public class DrawingPool
    {
        private List<Control> _pool;
        private bool _isSuspended;

        public DrawingPool ()
        {
            _pool = new List<Control>();
        }

        public void Register (Control c)
        {
            if (!_pool.Contains(c))
            {
                _pool.Add(c);
            }
        }

        public void Unregister(Control c)
        {
            _pool.Remove(c);
        }

        public void SuspendDrawing()
        {
            if (!_isSuspended)
            {
                _pool.ForEach(c => c.SuspendDrawing());
                _isSuspended = true;
            }
        }

        public void ResumeDrawing()
        {
            if (_isSuspended)
            {
                _pool.ForEach(c => c.ResumeDrawing());
                _isSuspended = false;
            }
        }

        public void Draw ()
        {
            _pool.ForEach(c => c.Refresh());
        }

        public void ResumeAndDraw()
        {
            ResumeDrawing();
            Draw();
        }
    }
}
