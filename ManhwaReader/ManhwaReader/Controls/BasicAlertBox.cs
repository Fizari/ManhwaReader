using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader.Controls
{
    public class BasicAlertBox : IAlertBox
    {
        public void ShowOkAlert(string caption, string msg, Action action)
        {
            var result = MessageBox.Show(msg, caption, MessageBoxButtons.OK);

            if (result == DialogResult.OK)
                if (action != null)
                    action();
        }

        public void ShowYesNoAlert(string caption, string msg, Action yesAction, Action noAction)
        {
            var result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                if (yesAction != null)
                    yesAction();
            if (result == DialogResult.No)
                if (noAction != null)
                    noAction();
        }
    }
}
