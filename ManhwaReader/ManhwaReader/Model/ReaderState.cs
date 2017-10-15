﻿using System.IO;

namespace ManhwaReader.Model
{
    public class ReaderState : IReaderState
    {
        public FileInfo File { get; set; }
        public int VerticalScrollPosition { get; set; }

        public ReaderState()
        {
        }
    }
}