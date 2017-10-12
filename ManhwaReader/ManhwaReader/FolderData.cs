using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManhwaReader.Exceptions;

namespace ManhwaReader
{
    public class FolderData
    {
        private static string[] _validExtensions = { "bmp", "gif", "jpeg", "jpg", "png", "tiff" };
        private DirectoryInfo _folder;
        private FileInfo[] _files;
        private int _cpt = 0;

        public void Setup (string filePath)
        {
            var file = new FileInfo(filePath);
            _folder = file.Directory;

            var fileList = new List<FileInfo>();
            var innerCpt = 0;
            foreach (FileInfo f in _folder.GetFiles())
            {
                if (_validExtensions.Contains(f.Extension))
                {
                    if (f.FullName == filePath)
                        _cpt = innerCpt;
                    fileList.Add(f);
                    innerCpt++;
                }
            }
            if (fileList.Count == 0)
                throw new FolderEmptyException("The folder is empty (and if you coded that right you should not be there)");
            _files = fileList.ToArray();
        }

        public string GetNextFilePath()
        {
            return GetNextOrPreviousFilePath(1);
        }

        public string GetPreviousFilePath()
        {
            return GetNextOrPreviousFilePath(-11);
        }

        public string GetNextOrPreviousFilePath (int i)
        {
            if (_cpt + i > _files.Length - 1)
                return _files[0].FullName;
            else if (_cpt + i < 0)
                return _files[_files.Length - 1].FullName;
            else
                return _files[_cpt + i].FullName;
        }
    }
}
