using System;
using System.Net;
using System.Threading;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Scheduler.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Produces("application/json", "application/xml")]
    public class SchedulerController : ControllerBase
    {
        private readonly ILogger<SchedulerController> _logger;

        public SchedulerController(ILogger<SchedulerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Allow to setup the time for execution particular job
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> Get()
        {
            BackgroundJob.Enqueue(() => RunInBackground());
            return "meow";
        }

        public static void RunInBackground()
        {
            Console.WriteLine($"good job,val from scheduler by {Thread.CurrentThread.Name}");
        }
    }
}
