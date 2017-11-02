using ManhwaReader.Model;
using ManhwaReader.Views;
using System;
using System.Windows.Forms;

namespace ManhwaReader.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        protected NewForm _form;
        private IFolderData _folderData;
        private IReaderState _state;
        
        private bool _isFullScreen = false;
        private bool _areClicksEnabled = false;

        public event EventHandler PictureLoaded;

        public MainPresenter (NewForm form)
        {
            _form = form;
            _folderData = new FolderData();
            _state = new ReaderState();
        }
        
        public void OnPictureLoaded ()
        {
            if (PictureLoaded != null)
                PictureLoaded(this, new EventArgs());
        }

        public void LoadChosenFile (string filePath)
        {
            var isValid = _folderData.Load(filePath);
            if (isValid)
            {
                OnPictureLoaded();
                _form.DisplayFile(filePath);
                EnableClicks(true);
            } 
            else
            {
                _form.ShowOKErrorAlert("Incorrect file type","The file type is not valid. Please choose a picture.");
                ShowFileChooser();
            }
        }

        public void LoadNextPicture ()
        {
            if (_areClicksEnabled)
            {
                var nextFilePath = _folderData.GetNextFilePath();
                _form.DisplayFile(nextFilePath);
            }
        }

        public void LoadPreviousPicture()
        {
            if (_areClicksEnabled)
            {
                var previousFilePath = _folderData.GetPreviousFilePath();
                _form.DisplayFile(previousFilePath);
            }
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

        public void SwitchFullScreen ()
        {
            if(_isFullScreen)
            {
                _form.DisableFullScreen();
            }
            else
            {
                _form.EnableFullScreen();
            }
            _isFullScreen = !_isFullScreen;
        }

        public void EnableClicks(bool enabled)
        {
            _areClicksEnabled = enabled;
        }

        public void SwitchModes()
        {
            if (_state.IsMainPanelOnly)
                _form.EnableNormalMode();
            else
                _form.EnableMainPanelOnlyMode();
            _state.IsMainPanelOnly = !_state.IsMainPanelOnly;
        }

        #region key binding

        public bool KeyPressed (ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && _isFullScreen)
            {
                SwitchFullScreen();
                return true;
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                ShowFileChooser();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Enter))
            {
                SwitchFullScreen();
                return true;
            }
            if (keyData == Keys.Right)
            {
                LoadNextPicture();
                return true;
            }
            if (keyData == Keys.Left)
            {
                LoadPreviousPicture();
                return true;
            }
            if (keyData == Keys.Down)
            {
                _form.ScrollMainPanel(false);
                return true;
            }
            if (keyData == Keys.Up)
            {
                _form.ScrollMainPanel(true);
                return true;
            }
            return false;
        }
        #endregion
    }
}
