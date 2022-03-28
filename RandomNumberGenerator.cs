using InowBackend.Hubs;
using InowBackend.Model;
using InowBackend.Utility;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend
{
    public class RandomNumberGenerator : Hub<ISignalrGenerateRandomHub>
    {
        private bool isStopped = false;
        string reportFile;
        private static RandomNumberGenerator instance;
        public int totalNumeric=0, totalAlphaNumeric=0, totalFloat=0;
        private static object obj = new object();
        private FileWriter fileWriter;

        //public void Counter()
        //{
        //    Clients.Caller.CounterUpdate(new CounterDTO
        //    {
        //        counter1 = totalNumeric,
        //        counter2 = totalAlphaNumeric,
        //        counter3 = totalFloat
        //    });


        //}

        private RandomNumberGenerator()
        {
            reportFile = "";
            totalNumeric = 0;
            totalAlphaNumeric = 0;
            totalFloat = 0;
            fileWriter = new FileWriter();
        }

        public static RandomNumberGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new RandomNumberGenerator();
                        }
                    }
                }

                return instance;
            }
        }

        public async Task GenerateRandomNumber(List<int> selectedOptions, int fileSize)
        {
            Reset();

            while (isStopped == false)
            {
                Random ran = new Random();
                int curRandom = ran.Next(selectedOptions.Count);
                if (selectedOptions[curRandom] == 0)
                {
                    reportFile += reportFile.Length == 0 ? GetRandomNumeric().ToString() : "," + GetRandomNumeric().ToString();
                    totalNumeric++;
                }
                else if (selectedOptions[curRandom] == 1)
                {
                    reportFile += reportFile.Length == 0 ? GetRandomAlphaNumeric() : "," + GetRandomAlphaNumeric();
                    totalAlphaNumeric++;
                }
                else
                {
                    reportFile += reportFile.Length == 0 ? GetRandomFloat().ToString() : "," + GetRandomFloat().ToString();
                    totalFloat++;
                }

               await Clients.Caller.CounterUpdate(new CounterDTO
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
            fileWriter.WriteStrToFile(reportFile);
        }

        public void Stop()
        {
            isStopped = true;
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
            string str = "";
            str += new string(' ', ran.Next(5));
            str += new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[ran.Next(s.Length)]).ToArray());
            str += new string(' ', ran.Next(5));
            return str;
        }

        private void Reset()
        {
            reportFile = "";
            isStopped = false;
            totalAlphaNumeric = totalFloat = totalNumeric = 0;
        }
    }
}
