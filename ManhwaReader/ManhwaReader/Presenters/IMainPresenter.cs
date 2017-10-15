using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
