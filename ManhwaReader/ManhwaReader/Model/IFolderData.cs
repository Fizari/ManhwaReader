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
        event EventHandler CurrentImageLoaded;
        ImageData CurrentFile { get; }
        bool Load(string filePath);
        ImageData GetNextFile();
        ImageData GetPreviousFile();
    }
}
