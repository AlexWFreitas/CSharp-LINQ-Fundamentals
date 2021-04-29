using System.Collections.Generic;
using System.IO;

namespace Introduction
{
    public class FileInfoComparer : IComparer<FileInfo>
    {
        // Compare compares two files and then orders then by smaller to higher ( Ascending ) -1 if lower, 0 if equal, 1 if higher
        // Our code inside compares and returns -1 if bigger, 0 if equal, 1 if lower.
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
