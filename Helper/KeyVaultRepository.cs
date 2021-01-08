using IntegrationPlatformFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationPlatformFunctions.Helper
{
    public class KeyVaultRepository
    {
        [FunctionName("GetFTPInfoFromVault")]
        public async Task<FTPConnectionInfo> GetValue([ActivityTrigger] string key)
        {
            await Task.Delay(1200);

            return new FTPConnectionInfo {
                ServerUrl = "server-url",
                Port = "21",
                Username = "test-username",
                Password = "test-password"
            };
        }
    }
}
