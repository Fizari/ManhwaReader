using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader.Controls
{
    public interface IAlertBox
    {
        void ShowYesNoAlert(string caption, string msg, Action yesAction, Action noAction);
        void ShowOkAlert(string caption, string msg, Action action);
    }
}
