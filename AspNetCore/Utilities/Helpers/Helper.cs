using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Utilities.Helpers
{
    public static class Helper
    {
        public static string GetFilePath(string root,string folter, string filename)
        {
            return Path.Combine(root, folter, filename);
        }
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
