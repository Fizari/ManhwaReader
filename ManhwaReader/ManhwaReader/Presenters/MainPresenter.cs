using ManhwaReader.Extensions;
using ManhwaReader.Model;
using ManhwaReader.Views;
using System;
using System.Windows.Forms;

namespace ManhwaReader.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        protected MainForm _form;
        private IFolderData _folderData;
        private IReaderState _state;
        
        private bool _isFullScreen = false;
        private bool _areClicksEnabled = false;
        
        public MainPresenter (MainForm form)
        {
            _form = form;
            _folderData = new FolderData();
            _folderData.CurrentImageLoaded += OnCurrentImageJustLoaded;
            _state = new ReaderState();
        }

        public void LoadChosenFile (string filePath)
        {
            _form.ShowLoadingUI();
            var isValid = _folderData.Load(filePath);
            if (!isValid)
            {
                _form.ShowOKErrorAlert("Incorrect file type","The file type is not valid. Please choose a picture.");
                ShowFileChooser();
                _form.HideLoadingUI();
            }
        }

        private void OnCurrentImageJustLoaded(object sender, EventArgs e)
        {
            
            _form.DisplayFile(_folderData.CurrentFile);
            this.PrintDebug(_folderData.CurrentFile.File.FullName);
        }

        public void LoadNextPicture ()
        {
            var nextFilePath = _folderData.GetNextFile();
            if (nextFilePath.Image != null)
            {
                _form.DisplayFile(nextFilePath);
            }
        }

        public void LoadPreviousPicture()
        {
            var previousFilePath = _folderData.GetPreviousFile();
            if (previousFilePath.Image != null)
            {
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
            _state.File = _folderData.CurrentFile;
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
