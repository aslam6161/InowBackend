using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Services.Utility
{
   public interface IFileWriterService
    {
        void WriteStrToFile(string str);
    }
}
