using InowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Services.Randoms
{
    public interface IRandomNumberService
    {
        Task GenerateRandomNumber(List<int> selectedOptions, int fileSize);
        void Stop();
        ReportInfo GetReportData();
    }
}
