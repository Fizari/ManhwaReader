using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader
{
    public static class Extensions
    {
        public static void ForEach (this DirectoryInfo dir, Action <FileInfo> action)
        {
            foreach(FileInfo f in dir.GetFiles())
            {
                action(f);
            }
        }
    }
}
