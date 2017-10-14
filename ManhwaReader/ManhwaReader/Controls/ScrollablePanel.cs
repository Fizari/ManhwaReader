using System.Windows.Forms;

namespace ManhwaReader.Controls
{
    public class ScrollablePanel : Panel
    {
        public ScrollablePanel () 
        {
        }

        public void MouseWheeled(MouseEventArgs e)
        {
            this.OnMouseWheel(e);
        }
        
    }
}
