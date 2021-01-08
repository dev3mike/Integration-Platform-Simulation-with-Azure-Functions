using IntegrationPlatformFunctions.Clients.EMG.models;
using IntegrationPlatformFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationPlatformFunctions.EMG
{
    public class FileProcessor
    {

        [FunctionName("EMG_FilesProcessor")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context
            )
        {
            List<SFTPFile> filesList = context.GetInput<List<SFTPFile>>();

 
            var output = new List<string>();

            for (int i = 0; i < filesList.Count; i++)
            {
                output.Add($"File = {filesList[i].Name}, " + await context.CallSubOrchestratorAsync<string>("EMG_ProcessFile", filesList[i]));
            }

            return output;
        }


        [FunctionName("EMG_ProcessFile")]
        public async Task<string> ProcessFile(
            [OrchestrationTrigger] IDurableOrchestrationContext context
            )
        {
            SFTPFile file = context.GetInput<SFTPFile>();

            // We Process our File Here

            var retryOptions = new RetryOptions(
                    firstRetryInterval: TimeSpan.FromSeconds(8),
                    maxNumberOfAttempts: 2);

            var numberOfTotalData = 10000;

            var tasks = new Task<bool>[numberOfTotalData];

            for (int i = 0; i < numberOfTotalData; i++)
            {
                // Map to Model and Save to Database
                var OurModel = new Transaction
                {
                    Title = "Transaction Title",
                    Amount = 100,
                    CreatedDate = DateTime.UtcNow
                };

                tasks[i] = context.CallActivityWithRetryAsync<bool>("EMG_SaveItemToDatabase", retryOptions, "");
            }

            await Task.WhenAll(tasks);

            var ok = tasks.Count(x => x.Result == true);
            var failed = tasks.Count(x => x.Result == false);

            return $"Files Processed. OK = {ok}, Failed = {failed}";
        }
    }
}
