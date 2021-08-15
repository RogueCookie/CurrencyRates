using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CurrencyRates.Report.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Produces("application/json", "application/xml")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dratuti
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> Get()
        {
            return "kotiki";
        }
    }
}
