using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader.Exceptions
{
    public class FolderEmptyException : Exception
    {

        public FolderEmptyException()
        {
        }

        public FolderEmptyException(string message)
        : base(message)
        {
        }

        public FolderEmptyException(string message, Exception inner)
        : base(message, inner)
    {
        }
    }
}
