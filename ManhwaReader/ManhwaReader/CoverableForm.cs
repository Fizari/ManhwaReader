using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader
{
    public class CoverableForm : Form
    {
        public CoverableForm () : base()
        {
        }

        public virtual Size CoverableArea()
        {
            return Size;
        }
        public virtual Point CoverableLocation()
        {
            return Location;
        }

        public virtual void MouseWheeled(object sender, MouseEventArgs me){}
    }
}
