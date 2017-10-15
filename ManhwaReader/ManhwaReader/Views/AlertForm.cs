using ManhwaReader.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader.Views
{
    public class AlertForm : Form
    {
        private IAlertBox _alertBox;

        public AlertForm () : base()
        {
            _alertBox = new BasicAlertBox();
        }

        private void ShowYesNoAlert (string caption, string msg, Action yesAction, Action noAction)
        {
            _alertBox.ShowYesNoAlert(caption, msg, yesAction, noAction);
        }

        public void ShowOKErrorAlert(string msg)
        {
            _alertBox.ShowOkAlert("Error", msg, null);
        }

        public void ShowOKErrorAlert(string caption, string msg)
        {
            _alertBox.ShowOkAlert(caption, msg, null);
        }

        public void ShowOKErrorAlert(string msg, Action action)
        {
            _alertBox.ShowOkAlert("Error", msg, action);
        }

        public void ShowOKErrorAlert(string caption, string msg, Action action)
        {
            _alertBox.ShowOkAlert(caption, msg, action);
        }
    }
}
