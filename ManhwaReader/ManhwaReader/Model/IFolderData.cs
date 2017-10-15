using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader.Model
{
    public interface IFolderData
    {

        bool Load(string filePath);
        string GetNextFilePath();
        string GetPreviousFilePath();
        string GetNextOrPreviousFilePath(int i);
        FileInfo GetCurrentFile();
    }
}
