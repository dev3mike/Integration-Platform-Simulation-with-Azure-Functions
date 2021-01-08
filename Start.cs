using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationPlatformFunctions
{
    public static class Start
    {
        [FunctionName("Orchestration_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "integration/{clientName}")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            string clientName,
            ILogger log)
        {
            string instanceId = await starter.StartNewAsync($"{clientName}_Orchestration", null);

            log.LogInformation($"Client Integration {clientName} orchestration started with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);

        }

    }
}
