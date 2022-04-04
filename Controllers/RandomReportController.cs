using InowBackend.Models;
using InowBackend.Services.Randoms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InowBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RandomReportController : ControllerBase
    {
        private readonly IRandomNumberService _randomNumberService;

        public RandomReportController(IRandomNumberService randomNumberService)
        {
            _randomNumberService = randomNumberService;
        }
        [HttpPost]
        public async Task<Response> StartGeneratingRandomNumber([FromBody] Option option)
        {

            try
            {
                await _randomNumberService.GenerateRandomNumber(option.SelectedOptions, option.FileSize,option.NumericP,option.AlphaNumericP,option.FloatP);

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
                _randomNumberService.Stop();

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
                var info = _randomNumberService.GetReportData();

                return new Response(true, "Success", info);
            }

            catch (Exception ex)
            {
                return new Response(false, ex.Message, null);
            }

        }

    }
}
