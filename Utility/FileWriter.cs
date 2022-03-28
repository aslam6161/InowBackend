using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Utility
{
    public class FileWriter
    {
        string path;
        public FileWriter() { }

        public void WriteStrToFile(string str)
        {
            path = @"c:\Inow";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += @"\reportFile.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(str);
                }
            }
        }
    }
}