using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TrucostFunction
{
    public static class TrucostFunction
    {
        [FunctionName("TrucostFunction")]
        public static void Run([ServiceBusTrigger("TrucostProvider", "TrucostCompany", Connection = "TrucostConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"New data received from Trucost: {mySbMsg}");
        }
    }
}
