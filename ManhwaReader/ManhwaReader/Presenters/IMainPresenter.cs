using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhwaReader.Presenters
{
    public interface IMainPresenter
    {
        void LoadChosenFile(string filePath);

        void LoadNextPicture();

        void LoadPreviousPicture();

        void ShowFileChooser();

        void OpenChosenFile(string filePath);

        int GetVerticalScrollPosition();

        void RegisterState(int scrollPosition);

        void SwitchFullScreen();

        bool KeyPressed(ref Message msg, Keys keyData);
    }
}
