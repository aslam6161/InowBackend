using InowBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InowBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RandomReportController : ControllerBase
    {
        private readonly RandomNumberGenerator _randomNumberGenerator;

        public RandomReportController(RandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }
        [HttpPost]
        public async Task<Response> StartGeneratingRandomNumber([FromBody] Option option)
        {

            try
            {
                await _randomNumberGenerator.GenerateRandomNumber(option.SelectedOptions, option.FileSize);

                return new Response(true, "Started", null);
            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message, null);
            }
        }

        [HttpGet]
        public Response Stop()
        {
            try
            {
                _randomNumberGenerator.Stop();

                return new Response(true, "Stoped", null);
            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message, null);
            }


        }

        [HttpGet]
        public Response GetReportData()
        {
            try
            {
                RandomNumberGenerator RG = _randomNumberGenerator;
                var totalObject = RG.totalNumeric + RG.totalAlphaNumeric + RG.totalFloat;


                ReportInfo info = new ReportInfo()
                {
                    NumericPercentage = (int)Math.Round(((double)RG.totalNumeric / (double)totalObject) * 100),
                    AlphaNumericPercentage = (int)Math.Round(((double)RG.totalAlphaNumeric / (double)totalObject) * 100),
                    FloatPercentage = (int)Math.Round(((double)RG.totalFloat / (double)totalObject) * 100.0)
                };

                return new Response(true, "Success", info);


            }

            catch (Exception ex)
            {
                return new Response(false, ex.Message, null);
            }

        }

    }
}
