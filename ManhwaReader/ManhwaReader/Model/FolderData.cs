using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManhwaReader.Exceptions;
using ManhwaReader.Model;
using ManhwaReader.Extensions;

namespace ManhwaReader
{
    public class FolderData : IFolderData
    {
        public static string[] VALID_EXTENSIONS = { ".bmp", ".gif", ".jpeg", ".jpg", ".png", ".tiff" };
        
        private DirectoryInfo _folder;
        private List<ImageData> _files;
        private int _cpt = 0;
        private int _minListSizeForPreLoading;
        private int _preLoadingIndex; // number of next/prev files to load in background
        
        public int PreLoadingIndex //todo user can change in options
        {
            get
            {
                return _preLoadingIndex;
            }
            set
            {
                _preLoadingIndex = value;
                _minListSizeForPreLoading = 1 + 2 * _preLoadingIndex;
            }
        }
        public ImageData CurrentFile
        {
            get
            {
                if (_files != null && _cpt > -1 && _cpt < _files.Count)
                    return _files[_cpt];
                else
                    return null;
            }
        }

        public event EventHandler CurrentImageLoaded;

        public FolderData()
        {
            PreLoadingIndex = 1;
        }
        
        public bool Load(string filePath)
        {
            var file = new FileInfo(filePath);
            if (!VALID_EXTENSIONS.Contains(file.Extension))
            {
                return false;
            }

            if (_folder == null || _folder.FullName != file.Directory.FullName) // new folder
            {
                _folder = file.Directory;

                if (_files != null)
                    DisposeCurrentlyLoadedFiles();

                _files = new List<ImageData>();
                var innerCpt = 0;
                _folder.GetFiles().CustomSort().ForEach(f =>
                {
                    if (VALID_EXTENSIONS.Contains(f.Extension))
                    {
                        if (f.FullName == filePath)
                            _cpt = innerCpt;
                        _files.Add(new ImageData(f, Imageloaded));
                        innerCpt++;
                    }
                });
                if (_files.Count == 0)
                    throw new FolderEmptyException("The folder is empty (and if you coded that right you should not be there)");
                InitLoadingImages();
            }
            else // known folder
            {
                var newCpt = 0;
                var innerCpt = 0;
                _files.ForEach(f =>
               {
                   if (filePath == f.File.FullName)
                   {
                       newCpt = innerCpt;
                   }
                   innerCpt++;
               });

                if (newCpt <= _cpt + PreLoadingIndex && newCpt >= _cpt - PreLoadingIndex)
                {//newCpt is within old range
                    CompleteLoadedFiles(newCpt, _cpt);
                    _cpt = newCpt;
                    Imageloaded(_files[newCpt]);
                }
                else
                {
                    DisposeCurrentlyLoadedFiles();
                    _cpt = newCpt;
                    InitLoadingImages();
                }
            }
            return true;
        }

        //walk the cpt to the next file and load and dispose appropriate files
        public ImageData GetNextFile()
        {
            _cpt = ConvertCPT(_cpt + 1);
            if (_files.Count > _minListSizeForPreLoading)//to prevent redundancy
            {
                LoadImageData(_cpt + PreLoadingIndex);
                DisposeImageData(_cpt - (PreLoadingIndex + 1));
            }
            return _files[_cpt];
        }

        //walk the cpt to the previous file and load and dispose appropriate files
        public ImageData GetPreviousFile()
        {
            _cpt = ConvertCPT(_cpt - 1);
            if (_files.Count > _minListSizeForPreLoading)//to prevent redundancy
            {
                LoadImageData(_cpt - PreLoadingIndex);
                DisposeImageData(_cpt + (PreLoadingIndex + 1));
            }
            return _files[_cpt];
        }

        //To initiate the files first
        public void InitLoadingImages ()
        {
            LoadOrDisposeNeighbors(i => LoadImageData(i));
        }
        
        //To clear the currently loaded files
        public void DisposeCurrentlyLoadedFiles()
        {
            LoadOrDisposeNeighbors(i => DisposeImageData(i));
        }

        private void LoadOrDisposeNeighbors(Action<int> LoadOrDispose)
        {
            if (_files.Count <= _minListSizeForPreLoading)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    LoadOrDispose(i);
                }
            }
            else
            {
                LoadOrDispose(_cpt);
                for (int i = 1; i <= PreLoadingIndex; i++)
                {
                    LoadOrDispose(_cpt + i);
                    LoadOrDispose(_cpt - i);
                }
            }
        }

        //When the file is already loaded (meaning the file is within the 
        //range of the previous load), finish the loading of the range
        public void CompleteLoadedFiles(int newCpt, int oldCpt)
        {
            var offset = newCpt - oldCpt;
            var factor = Math.Sign(offset);
            for (int i = 1; i <= Math.Abs(offset); i++)
            {
                var loadIndex = oldCpt + factor * (PreLoadingIndex + i);
                var disposeIndex = oldCpt - factor * (PreLoadingIndex - 1 + i);
                LoadImageData(loadIndex);
                DisposeImageData(disposeIndex);
            }
        }

        // Convert i to stay in range of the List _files, simulating a circular list
        private int ConvertCPT (int i)
        {
            var newCpt = i < 0 ? i + Math.Ceiling((Convert.ToDouble(i*-1)/_files.Count)) * _files.Count : i % _files.Count;
            return Convert.ToInt32(newCpt);
        }
        
        private void LoadImageData(int index)
        {
            _files[ConvertCPT(index)].LoadImageAsync();
        }
        
        private void DisposeImageData(int index)
        {
            this.PrintDebug("sender : " + _files[ConvertCPT(index)].File.Name);
            _files[ConvertCPT(index)].Dispose();
        }

        public void Imageloaded (ImageData sender)
        {
            this.PrintDebug("sender : "+sender.File.Name);
            if (sender == CurrentFile && CurrentImageLoaded != null)
                CurrentImageLoaded(this, new EventArgs());
        }
    }
}
