using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader.Model
{
    public class ReaderState
    {
        public FileInfo File { get; set; }
        public int VerticalScrollPosition { get; set; }

        public ReaderState()
        {
        }
    }
}
