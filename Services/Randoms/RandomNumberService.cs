using InowBackend.Hubs;
using InowBackend.Models;
using InowBackend.Services.Utility;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InowBackend.Services.Randoms
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly IHubContext<SignalrGenerateRandomHub, ISignalrGenerateRandomHub> _hubContext;
        private readonly IFileWriterService _fileWriterService;

        private bool isStopped = false;
        StringBuilder reportFile;

        public int totalNumeric = 0, totalAlphaNumeric = 0, totalFloat = 0;


        public RandomNumberService(IHubContext<SignalrGenerateRandomHub, ISignalrGenerateRandomHub> hubContext,
            IFileWriterService fileWriterService)
        {
            _hubContext = hubContext;
            reportFile = new StringBuilder();
            totalNumeric = 0;
            totalAlphaNumeric = 0;
            totalFloat = 0;
            _fileWriterService = fileWriterService;
        }

        public async Task GenerateRandomNumber(List<int> selectedOptions, int fileSize)
        {
            Reset();

            while (isStopped == false)
            {
                Random ran = new Random();
                int curRandom = ran.Next(selectedOptions.Count);

                if (selectedOptions[curRandom] == (int)OptionType.Numeric)
                {
                    reportFile.Append(reportFile.Length == 0 ? GetRandomNumeric().ToString() : "," + GetRandomNumeric().ToString());
                    totalNumeric++;
                }
                else if (selectedOptions[curRandom] == (int)OptionType.Alphanumeric)
                {
                    reportFile.Append(reportFile.Length == 0 ? GetRandomAlphaNumeric() : "," + GetRandomAlphaNumeric());
                    totalAlphaNumeric++;
                }
                else
                {
                    reportFile.Append(reportFile.Length == 0 ? GetRandomFloat().ToString() : "," + GetRandomFloat().ToString());
                    totalFloat++;
                }

                await _hubContext.Clients.All.CounterUpdate(new CounterDTO
                {
                    counter1 = totalNumeric,
                    counter2 = totalAlphaNumeric,
                    counter3 = totalFloat
                });

                if (reportFile.Length > fileSize * 1020)
                {
                    isStopped = true;
                }
            }
            _fileWriterService.WriteStrToFile(reportFile.ToString());
        }

        public void Stop()
        {
            isStopped = true;
        }

        public ReportInfo GetReportData()
        {
            var totalObject = this.totalNumeric + this.totalAlphaNumeric + this.totalFloat;


            ReportInfo info = new ReportInfo()
            {
                NumericPercentage = (int)Math.Round(((double)this.totalNumeric / (double)totalObject) * 100),
                AlphaNumericPercentage = (int)Math.Round(((double)this.totalAlphaNumeric / (double)totalObject) * 100),
                FloatPercentage = (int)Math.Round(((double)this.totalFloat / (double)totalObject) * 100.0)
            };

            return info;
        }


        private int GetRandomNumeric()
        {
            Random ran = new Random();
            return ran.Next(0, 10000000);
        }


        private double GetRandomFloat()
        {
            Random ran = new Random();
            return ran.NextDouble() * ran.Next(0, 10000000);
        }

        private string GetRandomAlphaNumeric()
        {

            Random ran = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            StringBuilder str = new StringBuilder();
            str.Append(' ',5);
            str.Append(new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[ran.Next(s.Length)]).ToArray()));
            str.Append(' ',5);
            return str.ToString();
        }

        private void Reset()
        {
            reportFile = reportFile.Clear();
            isStopped = false;
            totalAlphaNumeric = totalFloat = totalNumeric = 0;
        }
        //public override async Task OnConnectedAsync()
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
        //    await base.OnConnectedAsync();
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}