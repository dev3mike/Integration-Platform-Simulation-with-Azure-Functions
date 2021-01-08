using IntegrationPlatformFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationPlatformFunctions.Helper
{
    public class FtpRepository
    {
        [FunctionName("SFTPGetConnection")]
        public async Task<SFTPConnection> ConnectToSFTP([ActivityTrigger] FTPConnectionInfo fTPConnectionInfo)
        {
            await Task.Delay(2000);

            return new SFTPConnection
            {
                Status = "connected",
            };
        }
        
        [FunctionName("SFTPGetFilesList")]
        public async Task<List<SFTPFile>> GetFilesList([ActivityTrigger] SFTPConnection fTPConnectionInfo)
        {
            await Task.Delay(2000);

            var list = new List<SFTPFile>
            {
                new SFTPFile { Name = "file1.json", Content = "" },
                new SFTPFile { Name = "file2.json", Content = "" },
                new SFTPFile { Name = "file3.json", Content = "" },
                new SFTPFile { Name = "file4.json", Content = "" },
                new SFTPFile { Name = "file5.json", Content = "" },
                new SFTPFile { Name = "file6.json", Content = "" },
                new SFTPFile { Name = "file7.json", Content = "" },
                new SFTPFile { Name = "file8.json", Content = "" },
                new SFTPFile { Name = "file9.json", Content = "" }
            };

            return list;
        }
    }
}
