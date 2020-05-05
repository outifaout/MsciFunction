using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;


namespace MsciFunction
{
    public static class MsciFunction
    {
        [FunctionName("MsciFunction")]
        public static async void Run([ServiceBusTrigger("msciprovider", "mscicompany", Connection = "MsciConnectionString")]string mySbMsg,
            IBinder binder, ILogger log)
        {

            var collectionName = "CandriamContainer";

            var attribute = new CosmosDBAttribute("CandriamDB", collectionName)
            {
                CreateIfNotExists = true,
                ConnectionStringSetting = "CosmosDBConnection",
                
            };

            var collector = await binder.BindAsync<IAsyncCollector<dynamic>>(attribute);

            await collector.AddAsync(new { Message = mySbMsg, id = Guid.NewGuid(), ProviderId = 2 });
            log.LogInformation("Document created !");
        }
    }
}
