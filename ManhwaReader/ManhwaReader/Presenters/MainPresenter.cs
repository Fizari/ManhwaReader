using ManhwaReader.Model;
using System;

namespace ManhwaReader.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        protected MainForm _form;
        private IFolderData _folderData;
        private IReaderState _state;

        public MainPresenter (MainForm form)
        {
            _form = form;
            _folderData = new FolderData();
            _state = new ReaderState();
        }

        public void LoadChosenFile (string filePath)
        {
            _folderData.Load(filePath);
            _form.OnPictureLoaded(new EventArgs());
            _form.LoadFile(filePath);
        }

        public void LoadNextPicture ()
        {
            var nextFilePath = _folderData.GetNextFilePath();
            _form.LoadFile(nextFilePath);
        }

        public void LoadPreviousPicture()
        {
            var previousFilePath = _folderData.GetPreviousFilePath();
            _form.LoadFile(previousFilePath);
        }

        public void ShowFileChooser ()
        {
            _form.ShowOpenFileDialog();
        }

        public void OpenChosenFile(string filePath)
        {
            LoadChosenFile(filePath);
        }

        public int GetVerticalScrollPosition ()
        {
            return _state.VerticalScrollPosition;
        }

        public void RegisterState (int scrollPosition)
        {
            _state.VerticalScrollPosition = scrollPosition;
            _state.File = _folderData.GetCurrentFile();
        }
    }
}
