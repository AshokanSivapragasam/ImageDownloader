using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using SftpUploader.Implementation;

namespace SftpUploader
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            //SftpUploaderImpl sftp = new SftpUploaderImpl();

            try
            {
                // This is a sample worker implementation. Replace with your logic.
                /*Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "SftpUploader entry point called"));
                SftpUploaderImpl.noOfRuns += 1;
                Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Create instance for blob container"));
                Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Created instance for blob container"));*/
            }
            catch (Exception ex)
            {
                //Logger.AddMessage(new Message(-1, "Exception: " + ex.Message));
            }

            while (true)
                Thread.Sleep(10000);
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
