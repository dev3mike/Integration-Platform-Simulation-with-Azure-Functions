using IntegrationPlatformFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationPlatformFunctions.EMG
{
    public class Orchestration
    {

        [FunctionName("EMG_Orchestration")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context
            )
        {
            var outputLogs = new List<string>();

            // Integration Flow

            // 1- Get Secure Info From KeyVault
            var keyvaultKey = "company-ftp-information-key";

            FTPConnectionInfo sftpConnectionInfo = await context.CallActivityAsync<FTPConnectionInfo>("GetFTPInfoFromVault", keyvaultKey);
            outputLogs.Add("SFTP Connection Info Fetched Successfully From Keyvault");

            // 2- Connect to FTP Client
            SFTPConnection ftpConnection = await context.CallActivityAsync<SFTPConnection>("SFTPGetConnection", sftpConnectionInfo);
            outputLogs.Add("SFTP Connected Successfully");

            // 3- Fetch List of Files From SFTP
            ftpConnection.Directory = "client/directory/path";

            List<SFTPFile> filesList = await context.CallActivityAsync<List<SFTPFile>>("SFTPGetFilesList", ftpConnection);
            outputLogs.Add("SFTP Files List Fetched Successfully");

            // 4- Start Processing Files
            List<string> filesImportLogs = await context.CallSubOrchestratorAsync<List<string>>("EMG_FilesProcessor", filesList);
            outputLogs.AddRange(filesImportLogs);
            outputLogs.Add($"{filesImportLogs.Count} SFTP Files Processed Successfully");


            return outputLogs;
        }
    }
}
