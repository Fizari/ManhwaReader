using System.IO;

namespace ManhwaReader.Model
{
    public class ReaderState : IReaderState
    {
        public ImageData File { get; set; }
        public int VerticalScrollPosition { get; set; }
        public int LeftSplitterPosition { get; set; }
        public bool IsMainPanelOnly { get; set; }

        public ReaderState()
        {
        }
    }
}
