using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRates.CzBank.Grpc.Rss;

namespace CurrencyRates.CzBank.Elastic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QaController : ControllerBase
    {
        private readonly RssService.RssServiceClient _client;

        public QaController(Grpc.Rss.RssService.RssServiceClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventUrlAsync()
        {
            try
            {

                var res = await _client.DownloadAsync(new GetInfoRequest());
                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
         }
	}
}
