using System.Collections.Generic;

namespace CurrencyRates.Scheduler.Api.Models
{
    /// <summary>
    /// Represent the list of all jobs
    /// </summary>
    public class JobServiceOption
    {
        public IEnumerable<JobOption> Jobs { get; set; }
    }
}