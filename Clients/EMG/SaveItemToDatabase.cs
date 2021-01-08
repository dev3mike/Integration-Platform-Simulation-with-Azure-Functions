using IntegrationPlatformFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationPlatformFunctions.Clients.EMG
{
    public class SaveItemToDatabase
    {
        [FunctionName("EMG_SaveItemToDatabase")]
        public async Task<bool> SaveItem([ActivityTrigger] string fileContent)
        {
            // In here we need to map data to model
            // and save it to the database


            Random rnd = new Random();

            await Task.Delay(rnd.Next(1000, 1800));

            return true;
        }
    }
}
