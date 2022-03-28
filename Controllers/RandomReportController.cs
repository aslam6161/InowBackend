﻿using InowBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InowBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RandomReportController : ControllerBase
    {
        [HttpPost]
        public async Task<string> StartGeneratingRandomNumber([FromBody] Option option)
        {
            RandomNumberGenerator instance = RandomNumberGenerator.Instance;
            var thread = new Thread(
                    () =>  instance.GenerateRandomNumber(option.SelectedOptions, option.FileSize));
            thread.Start();
            
            return  "Started";
        }

        [HttpGet]
        public string Stop()
        {
            RandomNumberGenerator instance = RandomNumberGenerator.Instance;
            instance.Stop();
            return "Stopped";
        }

        [HttpGet]
        public ReportInfo GetReportData()
        {
            try
            {
                RandomNumberGenerator RG = RandomNumberGenerator.Instance;
                var totalObject = RG.totalNumeric + RG.totalAlphaNumeric + RG.totalFloat;


                ReportInfo info = new ReportInfo()
                {
                    NumericPercentage =(int)Math.Round( ((double)RG.totalNumeric / (double)totalObject) * 100),
                    AlphaNumericPercentage = (int)Math.Round(((double)RG.totalAlphaNumeric / (double)totalObject) * 100),
                    FloatPercentage = (int)Math.Round(((double)RG.totalFloat / (double)totalObject) * 100.0)
                };

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}