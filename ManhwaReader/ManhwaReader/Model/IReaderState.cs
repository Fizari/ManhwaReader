using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader.Model
{
    public interface IReaderState
    {
        FileInfo File { get; set; }
        int VerticalScrollPosition { get; set; }
        int LeftSplitterPosition { get; set; }
        bool IsMainPanelOnly { get; set; }
    }
}
