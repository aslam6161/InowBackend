using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InowBackend.Services.Utility
{
    public class FileWriterService : IFileWriterService
    {
        public List<string> GetFileElementsByLimit(int limit)
        {
            string path = @"c:\Inow\reportFile.txt";

            List<string> elements = new List<string>();

            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);

                var selectedElements = text.Split(',').Take(limit);

                foreach (var element in selectedElements)
                {
                    if (element.Contains('.'))
                    {
                        elements.Add($"{element} – float");
                    }
                    else if (Regex.IsMatch(element, @"[^a-zA-Z0-9]"))
                    {
                        elements.Add($"{element} – alphanumeric");
                    }
                    else
                    {
                        elements.Add($"{element} – numeric");
                    }
                }

            }

            return elements;
        }

        public void WriteStrToFile(string str)
        {
            string path = @"c:\Inow"; 
            try
            {
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
            catch (Exception ex)
            {

            }
        }
    }
}
