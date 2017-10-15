using ManhwaReader.Views;
using System.Drawing;
using System.Windows.Forms;

namespace ManhwaReader
{
    public class CoverableForm : AlertForm
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
