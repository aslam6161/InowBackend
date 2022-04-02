using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Services.Utility
{
    public class FileWriterService : IFileWriterService
    {
        string path;
        public void WriteStrToFile(string str)
        {
            try
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
            catch(Exception ex)
            {

            }
        }
    }
}
