using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureTBNClientSDK = Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.AzureServiceReference;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.TestClient
{
    public class TestClient
    {
        bool isTaskQueueRequestsRunning = false;
        bool isTaskProcessRequestsRunning = false;
        int taskCounter = 1;
        public static object _globalLock = new object();
        public static Process _process;

        public static void RunIt()
        {
            var risBinaryFullPath = @"D:\Spst\Code\ResourceIntegratorService\Services.ResourceIntegratorService.Client\bin\Debug\Microsoft.IT.RelationshipManagement.Services.ResourceIntegratorService.Client.exe";
            var infoMessage = string.Empty;
            var errorMessage = string.Empty;

            _process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = risBinaryFullPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            _process.Start();
            //_process.WaitForExit();
            while (!_process.StandardOutput.EndOfStream)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(_process.StandardOutput.ReadLine());
            }

            while (!_process.StandardOutput.EndOfStream)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(_process.StandardError.ReadLine());
            }
        }

        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        public static void Main()
        {
            /*DateTime de = DateTime.Parse("2017-08-29 09:40:00 AM");
            Console.WriteLine(de);
            Stopwatch stopIt = new Stopwatch();
            stopIt.Start();
            var task = Task.Run(() => RunIt());//you can pass parameters to the method as well
            if (task.Wait(TimeSpan.FromSeconds(30)))
                Console.WriteLine(task.Status); //the method returns elegantly
            else
            {
                stopIt.Stop();
                Console.WriteLine(stopIt.ElapsedMilliseconds / 1000);
            }*/

            var testClient = new TestClient();
#if dev
            var dict1 = new Dictionary<string, string>() { { "color1", "red" }, { "color2", "blue" }, { "color3", "red" }, { "color5", "red" } };
            var dict2 = new Dictionary<string, string>() { { "color1", "red" }, { "color2", "blue" }, { "color3", "blue" }, { "color4", "yellow" } };

            var allKeys = dict1.Keys.ToList<string>().Union(dict2.Keys.ToList<string>());

            foreach (var key in allKeys)
            {
                string value1 = string.Empty;
                string value2 = string.Empty;
                Console.Write(!dict1.TryGetValue(key, out value1) ? key + ": _NA_ | " : key + ": " + value1 + " | ");
                Console.Write(!dict2.TryGetValue(key, out value2) ? key + ": _NA_ | " : key + ": " + value2 + " | ");

                if (value1 != value2)
                    Console.WriteLine("No match");
                else
                    Console.WriteLine("Match");
            }
#endif

#if thread_lock_sync_check
            Thread[] t = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                t[i] = new Thread(() => testClient.sample());
                t[i].Name = i.ToString();
            }

            for (int i = 0; i < 5; i++)
            {
                t[i].Start();
            }

            while (true) ;
#endif

#if epoch_time_convertor
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var normalutcdatetime = epoch.AddSeconds(1485504374);
            Console.WriteLine(normalutcdatetime);
#endif

            

            try
            {
#if DEBUG
                Stopwatch sw000 = new Stopwatch();
                sw000.Start();
                while (sw000.Elapsed.Seconds < 10)
                {
                    sw000.Restart();
                    Console.WriteLine(CreateTbnGenericRequestMipUnittest10("eimip002@microsoft.com", "001", "Ashokan", "Sivapragasam"));
                    Console.WriteLine(sw000.ElapsedMilliseconds);
                }
                Console.WriteLine(CreateTbnGenericRequestMipUnittest10("eimip002@outlook.com", "001", "Ashokan", "Sivapragasam"));

                return;

                Console.WriteLine(CreateTbnWweRequestMipUnittest("v-assiva@microsoft.com", "Ashokan", "Sivapragasam"));
                Console.WriteLine(CreateTbnWweRequestMipUnittest("eimip001@outlook.com", "Email", "Mip"));
                

#endif

                var d = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);
                Console.WriteLine(d);

                Thread[] threads = new Thread[1];
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(() => testClient.CreateBulksendRequests(1));
                    threads[i].Name = (i + 1).ToString("0000");
                    threads[i].Start();
                }

                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Join();
                }

                Console.WriteLine("OK");


                var result = string.Empty;
                var successCount = 0;
                var failureCount = 0;
                var exceptionMsg = string.Empty;
                var sw = new Stopwatch();

                //Console.WriteLine(MSI_SUBSCRIBE_V2("velvetry@live.com", 112489));
                //Console.WriteLine(MSI_SUBSCRIBE_V2("testmsbc@hotmail.com", 117));
                //Console.WriteLine(MSI_SUBSCRIBE_V2("opttest@hotmail.com", 117));
                //Console.WriteLine(MSI_SUBSCRIBE_V2("vetrivel.p@gmail.com", 418));
                //Console.WriteLine(MSI_SUBSCRIBE_V2("doubleopttest@outlook.com", 51));

                while (true)
                {
                   sw.Start();
                    exceptionMsg = string.Empty;
                    try
                    {
                        result = MSI_SUBSCRIBE_V2("v-assiva@microsoft.com", 143, 0);
                        successCount = successCount + (result.Contains("Success") ? 1 : 0);
                        failureCount = failureCount + (!result.Contains("Success") ? 1 : 0);
                        exceptionMsg += (!result.Contains("Success") ? result : string.Empty);

                        result = MSI_SUBSCRIBE_V2("v-assiva@microsoft.com", 143, 1);
                        successCount = successCount + (result.Contains("Success") ? 1 : 0);
                        failureCount = failureCount + (!result.Contains("Success") ? 1 : 0);
                        exceptionMsg += (!result.Contains("Success") ? " | " + result : string.Empty);
                    }
                    catch (Exception ex)
                    {
                        exceptionMsg += " | " + ex.Message;
                        failureCount += 1;
                    }

                    Console.Write("\rElapsedMinutes: " + (sw.ElapsedMilliseconds / (1000 * 60)).ToString("0000000") + " | Success: " + successCount.ToString("0000000") + " | Failure: " + failureCount.ToString("0000000") + " Exception - " + exceptionMsg.Replace("\r", " ").Replace("\n", " ") + (!string.IsNullOrWhiteSpace(exceptionMsg) ? Environment.NewLine : string.Empty));
                }

                Console.WriteLine(Create_TBN_Generic_UNITTEST_9("v-assiva@microsoft.com", "Ashok"));
                //Console.WriteLine(Create_TBN_Generic_UNITTEST_8("v-assiva@microsoft.com", "Ashok", "Subject Line 001", "Pcs & Team"));

                /*Console.WriteLine(Create_TBN_Generic_UNITTEST_8("v-assiva@microsoft.com", "", ""));
                Console.WriteLine(Create_TBN_Regsys_LP_UNITTEST("v-assiva@microsoft.com"));

                Console.WriteLine(Create_TBN_Generic_UNITTEST_7("ashish.singla@microsoft.com", "Ashish", "Ashish Singla"));
                Console.WriteLine(Create_TBN_Generic_UNITTEST_7("v-assiva@microsoft.com", "Ashokan", "Ashokan Sivapragasam"));*/
                /*
                 * TBN(Generic)
                 */
                /*Console.WriteLine(Create_TBN_Generic_UNITTEST_2("v-assiva@microsoft.com", "Ashok", "http://dummyimage.com/600x400/000/fff.jpg&text=Microsoft+Retail"));
                Console.WriteLine(Create_TBN_Generic_UNITTEST_2("v-kukhus@microsoft.com", "Khushboo", "http://dummyimage.com/600x400/000/fff.jpg&text=Microsoft+Retail"));*/

                /*Console.WriteLine(Create_TBN_Generic_UNITTEST_3("v-kukhus@microsoft.com", "Khushboo", "", "CustomData001"));
                Console.WriteLine(Create_TBN_Generic_UNITTEST_3("v-assiva@microsoft.com", "Ashok", "Sivapragasam", "CustomData002"));
                Console.WriteLine(Create_TBN_Generic_UNITTEST_3("v-savole@microsoft.com", "Satish", "Voleti", "CustomData003"));
                
                Console.WriteLine(Create_TBN_Generic_UNITTEST_4("v-kukhus@microsoft.com", "Khushboo"));
                Console.WriteLine(Create_TBN_Generic_UNITTEST_4("v-assiva@microsoft.com", "Ashok"));*/

                /*Console.WriteLine(Create_TBN_Generic_UNITTEST_5("v-kukhus@microsoft.com", "body01", "body02"));
                Console.WriteLine(Create_TBN_Generic_UNITTEST_5("v-assiva@microsoft.com", "body01", "body02"));*/
                /*Console.WriteLine(Create_TBN_Generic_UNITTEST_5("ajit.krishnan@microsoft.com", "body01", "body02"));*/

                //Console.WriteLine(Create_TBN_Generic_UNITTEST_6("eintdev@microsoft.com", "Ashokan", "Sivapragasam", "CustomData"));
                //Console.WriteLine(Create_TBN_TAGM_V2_UNITTEST("v-assiva@microsoft.com"));

                //BulkSendv2();

                /*
                 * TBN(EmailTracking)
                 */
                /*Console.WriteLine(GetEmailTrackingTriggerSendSummaryData(39327, "TBN_WWE_Template_1").EmailTrackingResult.Sent);*/

                /*
                 * TBN(WWE)
                 */
                /*Console.WriteLine(Create_TBN_Event_UNITTEST("v-assiva@microsoft.com"));*/
                /*Console.WriteLine(Create_TBN_WWE_V2_UNITTEST("v-assiva@microsoft.com"));*/

                /*
                 * TBN(LP)
                 */
                /*Console.WriteLine(Create_Regsys_Lp("v-assiva@microsoft.com"));*/

                /*
                 * TBN(TAGM)
                 */
                /*Console.WriteLine(Create_TBN_TAGM_UNITTEST("v-assiva@microsoft.com"));*/

                /*
                 * Get request status
                 */
                /*try { Console.WriteLine(" GetRequestStatus() -- " + GetRequestStatus()); }
                catch (Exception ex) { Console.WriteLine(" GetRequestStatus() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

                /*
                 * Send bulk-send request
                 */

                /*var srcFileName = @"D:\Usr\Ashok\Media_services_Dynamic_002.tsv";
                var destFileName = @"D:\Usr\Ashok\Media_services_Dynamic_002_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv";*/
                //var srcFileName = @"D:\Usr\Ashutosh\Ei_Azure_Sql_Dynamic_Bulksend_Input_File.tsv";
                //var destFileName = @"D:\Usr\Ashutosh\Ei_Azure_Sql_Dynamic_Bulksend_Input_File_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv";
                for (int i = 0; i < 100; i++)
                {
                    var srcFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile.tsv";
                    var destFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv";
                    File.Copy(srcFileName, destFileName, true);
                    try
                    {
                        Console.WriteLine(" C -- '' BS() -- " + BulkSend(
                            batchId: "0",
                            bulkSendId: "1",
                            contentId: 353864, //351062(10900830),//41411(39327), //350882, //Dynamic email content 350762, //Static Email Content - 350207,
                            filePath: destFileName,
                            tenantAccountId: "10460681",//"10903835",
                            bulkSendEmailType: BulkSendEmailType.Transactional,
                            isSendInvoke: true,
                            isDynamicDataExtension: true,
                            isOverrideConfiguration: true,
                            dataImportType: DataImportType.Overwrite,
                            dynamicDataExtensionTemplateName: "TriggeredSendDataExtension"));

                        /*Console.WriteLine(" Counter -- '' BulkSend() -- " + BulkSend(
                            batchId: "ThisWillBeIgnored",
                            bulkSendId: "ThisWillBeIgnored",
                            contentId: 40553,//335738,//335662,//335727,
                            filePath: destFileName,
                            tenantAccountId: "39327",//"10017488",
                            bulkSendEmailType: BulkSendEmailType.Transactional,
                            isSendInvoke: true,
                            isDynamicDataExtension: true,
                            isOverrideConfiguration: true,
                            dataImportType: DataImportType.Overwrite,
                            dynamicDataExtensionTemplateName: "TriggeredSendDataExtension"));*/

                        /*Console.WriteLine(" Counter -- '' BulkSend() -- " + BulkSend(
                            batchId: "3",
                            bulkSendId: "3",
                            contentId: 334174,
                            filePath: destFileName,
                            tenantAccountId: "10888857",
                            bulkSendEmailType: BulkSendEmailType.Transactional,
                            isSendInvoke: true,
                            isDynamicDataExtension: true,
                            isOverrideConfiguration: true,
                            dataImportType: DataImportType.Overwrite,
                            dynamicDataExtensionTemplateName: "DE_XboxMusicBrandChangeEmail"));*/
                    }
                    catch (Exception ex) { Console.WriteLine(" BulkSend() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
                }

                /*
                 * Get tracking summary of bulk-send emails by AccountId, BatchId & BulksendGuid
                 */
                /*try { Console.WriteLine(" GetEmailTrackingBulkSendSummaryData() -- " + GetEmailTrackingBulkSendSummaryData()); }
                catch (Exception ex) { Console.WriteLine(" GetEmailTrackingBulkSendSummaryData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/
                try { Console.WriteLine(" GetEmailTrackingBulkSendSummaryDataV2() -- " + GetEmailTrackingBulkSendSummaryDataV2()); }
                catch (Exception ex) { Console.WriteLine(" GetEmailTrackingBulkSendSummaryDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

                /*
                 * Get tracking status of emails by AccountId, SubscriberKey & TrackingField
                 */
                
                /*try { Console.WriteLine(" GetEmailTrackingStatusData() -- " + GetEmailTrackingStatusData()); }
                catch (Exception ex) { Console.WriteLine(" GetEmailTrackingStatusData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/
                //try { Console.WriteLine(" GetEmailTrackingStatusDataV2() -- " + GetEmailTrackingStatusDataV2()); }
                //catch (Exception ex) { Console.WriteLine(" GetEmailTrackingStatusDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

                Console.WriteLine("Press any key to close this window");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

#if DEV
        public void Get()
        {
            try
            {
                var personalaccesstoken = "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(
                                "https://{account}.visualstudio.com/DefaultCollection/_apis/build/builds").Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
#endif

        public void sample()
        {
            lock (_globalLock)
            {
                Console.WriteLine("Start Thread_" + Thread.CurrentThread.Name + " | ");
                Thread.Sleep(Convert.ToInt32(Thread.CurrentThread.Name) * 10 * 1000);
                Console.WriteLine("End Thread_" + Thread.CurrentThread.Name + " | ");
            }
        }

        public void CreateBulksendRequests(int numberOfRequests)
        {
            for (int i = 0; i < numberOfRequests; i++)
            {
                var srcFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile.tsv";
                var destFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile_Thread_" + Thread.CurrentThread.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv";
                File.Copy(srcFileName, destFileName, true);
                try
                {
                    Console.WriteLine(" Counter -- '' BulkSend() -- " + BulkSend(
                        batchId: "0",
                        bulkSendId: "1",
                        contentId: 353864,
                        filePath: destFileName,
                        tenantAccountId: "10460681",
                        bulkSendEmailType: BulkSendEmailType.Transactional,
                        isSendInvoke: true,
                        isDynamicDataExtension: true,
                        isOverrideConfiguration: true,
                        dataImportType: DataImportType.Overwrite,
                        dynamicDataExtensionTemplateName: "TriggeredSendDataExtension"));
                }
                catch (Exception ex) { Console.WriteLine(" BulkSend() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
            }
        }

        #region REQUEST_STATUS_METHODS
        /// <summary>
        /// EmailInterchange: Gets status of the request by EiId
        /// </summary>
        /// <returns></returns>
        private static string GetRequestStatus()
        {
            Guid EIID = new Guid("C0250D47-E511-4D4D-A78B-C00BB8815596");
            return CallRequestStatus(EIID, RequestTypeForEnhancedAPI.TBN);
        }
        #endregion

        #region BULKSEND_METHODS
        /// <summary>
        /// Test Method for BulkSend
        /// </summary>
        /// <returns></returns>
        private static string BulkSend(string batchId, string bulkSendId, int contentId, string filePath, string tenantAccountId, BulkSendEmailType bulkSendEmailType, bool isSendInvoke, bool isDynamicDataExtension, bool isOverrideConfiguration, DataImportType dataImportType, string dynamicDataExtensionTemplateName)
        {
            //"b480ee7d-2444-4f37-ba6d-db279b87b60a","b480ee7d-2444-4f37-ba6d-db279b87b60a",@"D:\Usr\ShankarBaradwaj\MslMailerBulkSendList-20150313-255746814.tsv","10290011",BulkSendEmailType.Transactional,true, true,true,DataImportType.AddAndUpdate,"MSL Email Campaigns DE"
            var fileRequest = new FileRequest()
            {
                BatchId = batchId,
                BulkSendId = bulkSendId,
                ContentId = contentId,
                FilePath = filePath,
                TenantAccountId = tenantAccountId,
                BulkSendEmailType = bulkSendEmailType,
                IsSendInvoke = isSendInvoke,
                IsDynamicDataExtension = isDynamicDataExtension,
                IsOverrideConfiguration = isOverrideConfiguration,
                DataImportType = dataImportType,
                DynamicDataExtensionTemplateName = dynamicDataExtensionTemplateName
            };

            var azureTBNClient = new AzureTBNClientSDK.InterchangeConnect();
            var filesendResult = azureTBNClient.SendFileRequest(fileRequest);
            return (filesendResult.Result + "; EI ID: '" + filesendResult.EmailInterchangeId + "'");
        }

        /*private static void BulkSendv2()
        {
            var frquest = new FileRequest();
            frquest.BatchId = "0";
            frquest.BulkSendId = "GMED_TestJob_2";

            frquest.BulkSendEmailType = BulkSendEmailType.Transactional;
            frquest.DataImportType = DataImportType.Overwrite;
            frquest.ContentId = 0;
            frquest.FilePath = @"C:\Temp\gmedtest_2.tsv.zip";
            frquest.FriendlyFromName = "Test";
            frquest.TenantAccountId = "10420808";

            frquest.IsSendInvoke = false;
            frquest.IsDynamicDataExtension = true;
            frquest.IsOverrideConfiguration = true;

            frquest.DynamicDataExtensionTemplateName = "GMOv2_GMED_DE_Sendable";

            TriggerRequestNotificationClient client = new TriggerRequestNotificationClient();

            FileRequestSendResult result = null;
            try
            {
                result = client.SendFileRequest(frquest);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Submitted request to EI failed, trying again. BatchID: {3}.  {0}: {1}. {2}", ex.GetType().Name, ex.Message, ex.InnerException == null ? "" : ex.InnerException.GetType().Name + ": " + ex.InnerException.Message, frquest.BatchId);
            }

            Console.WriteLine(result.Result.ToString());
        }*/
        #endregion

        #region EMAILTRACKING_METHODS
        /// <summary>
        /// EmailTracking: Get BulkSend Summary
        ///     - NumberTargeted
        ///     - NumberDelivered
        ///     - NumberErrored
        ///     - UniqueClicks
        ///     - UniqueOpens
        ///     - Unsubscribes, etc.,
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingBulkSendSummaryData()
        {
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            EmailTrackingOutputOfBulkSendSummaryDetails bulkSummary;

            //Construct Email Tracking Bulk send Summary input object.
            EmailTrackingInputOfBulkSendSummaryInput inputBulkSummary = new EmailTrackingInputOfBulkSendSummaryInput
            {
                EmailTrackingCriteria = new BulkSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = 39327,
                    //Assign Batch Id.
                    TenantBatchID = "e084362d-6e61-4ef3-9a97-92eaf7365fd2",
                    //Assign Bulk Send Id
                    TenantBulkSendId = "e084362d-6e61-4ef3-9a97-92eaf7365fe3"
                }
            };

            //Retrieve call.
            bulkSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfBulkSendSummaryInput, EmailTrackingOutputOfBulkSendSummaryDetails>(inputBulkSummary);
            Console.WriteLine(bulkSummary.EmailTrackingResult);

            return StringResources.EMAIL_TRACKING_BULK_SEND_SUMMARY_STATUS;
        }

        /// <summary>
        /// EmailTracking: Get BulkSend Summary Version 2
        ///     - NumberTargeted
        ///     - NumberDelivered
        ///     - NumberErrored
        ///     - UniqueClicks
        ///     - UniqueOpens
        ///     - Unsubscribes, etc.,
        ///     - EIRequestId
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingBulkSendSummaryDataV2()
        {
            System.Guid requestID;
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            EmailTrackingOutputOfBulkSendSummaryDetails bulkSummary;
            //Construct Email Tracking Bulk send Summary input object.
            EmailTrackingInputOfBulkSendSummaryInput inputBulkSummary = new EmailTrackingInputOfBulkSendSummaryInput
            {
                EmailTrackingCriteria = new BulkSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = 10460681,
                    //Assign Batch Id.
                    TenantBatchID = "1",
                    //Assign Bulk Send Id
                    TenantBulkSendId = "1"
                }
            };

            //Retrieve call.
            bulkSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfBulkSendSummaryInput, EmailTrackingOutputOfBulkSendSummaryDetails>(inputBulkSummary, out requestID);

            return StringResources.EMAIL_TRACKING_BULK_SEND_SUMMARY_STATUS + "EI ID: " + requestID.ToString();
        }

        /// <summary>
        /// EmailTracking: GetStatus
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingStatusData()
        {
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            //EmailTrackingOutput type.
            EmailTrackingOutputOfEmilTrackingOutputDetails emailTracking;
            //Construct EmailTracking Input type.
            EmailTrackingInputOfEmailTrackingInputCriteria objStatusInput = new EmailTrackingInputOfEmailTrackingInputCriteria
            {
                EmailTrackingCriteria = new EmailTrackingInputCriteria
                {
                    ClientID = 39327,
                    TenantSubscriberKey = "e084362d-6e61-4ef3-9a97-92eaf7365ff1",
                    TenantTrackingField = "e084362d-6e61-4ef3-9a97-92eaf7365fe3"
                }

            };
            //Retrieve call. 
            emailTracking = client.GetEmailTrackingStatusData(objStatusInput);
            return StringResources.EMAIL_TRACKING_STATUS;
        }

        /// <summary>
        /// EmailTracking: GetStatus Version 2
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingStatusDataV2()
        {
            System.Guid requestID;
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            //EmailTrackingOutput type.
            EmailTrackingOutputOfEmilTrackingOutputDetails emailTracking;
            //Construct EmailTracking Input type.
            EmailTrackingInputOfEmailTrackingInputCriteria objStatusInput = new EmailTrackingInputOfEmailTrackingInputCriteria
            {
                EmailTrackingCriteria = new EmailTrackingInputCriteria
                {
                    ClientID = 10460681,
                    TenantSubscriberKey = "9afcee23-2b03-4a00-b8b0-7ef7188c8379",
                    TenantTrackingField = "9afcee23-2b03-4a00-b8b0-7ef7188c8379"
                }

            };
            //Retrieve call. 
            emailTracking = client.GetEmailTrackingStatusData(objStatusInput, out requestID);
            return StringResources.EMAIL_TRACKING_STATUS + "EI ID: " + requestID.ToString();
        }

        #region EMAILTRACKING_METHODS

        private static EmailTrackingOutputOfTriggerSendSummaryDetails GetEmailTrackingTriggerSendSummaryData(int businessUnitAccountId, string triggeredSendCustomerKey)
        {
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            EmailTrackingOutputOfTriggerSendSummaryDetails triggerSummary;

            //Construct Email Tracking Trigger send Summary input object.
            EmailTrackingInputOfTriggerSendSummaryInput inputSummary = new EmailTrackingInputOfTriggerSendSummaryInput
            {
                EmailTrackingCriteria = new TriggerSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = businessUnitAccountId,//MID Account#
                    //Assign Trigger Send Customer Key.
                    TriggerSendCustomerKey = triggeredSendCustomerKey //Add Your TriggerSend Definition 
                }
            };

            //Retrieve call.
            triggerSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfTriggerSendSummaryInput, EmailTrackingOutputOfTriggerSendSummaryDetails>(inputSummary);

            return triggerSummary;
        }

        private static string GetEmailTrackingTriggerSendSummaryDataV2()
        {
            System.Guid requestID;
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            EmailTrackingOutputOfTriggerSendSummaryDetails triggerSummary;

            //Construct Email Tracking Trigger send Summary input object.
            EmailTrackingInputOfTriggerSendSummaryInput inputSummary = new EmailTrackingInputOfTriggerSendSummaryInput
            {
                EmailTrackingCriteria = new TriggerSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = 10460681,//MID Account#
                    //Assign Trigger Send Customer Key.
                    TriggerSendCustomerKey = "TBN_3456" //Add Your TriggerSend Definition
                }
            };

            //Retrieve call.
            triggerSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfTriggerSendSummaryInput, EmailTrackingOutputOfTriggerSendSummaryDetails>(inputSummary, out requestID);

            return StringResources.EMAIL_TRACKING_TRIGGER_SEND_SUMMARY_STATUS + "EI ID: " + requestID.ToString();
        }

        #endregion

        #endregion

        #region TBN_METHODS

        /// <summary>
        /// Test Method for TAGM
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_TAGM_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_TBN_TAGM_Request_Packet(emailAddress);
            return CallSend(newRequest);
        }

        /// <summary>
        /// Test Method for TAGM_V2
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_TAGM_V2_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_TBN_TAGM_Request_Packet_002(emailAddress);
            return CallSendV2(newRequest);
        }

        /// <summary>
        /// Test Method for Limited Program
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_LP_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_TBN_LP_Request_Packet(emailAddress);
            return CallSend(newRequest);
        }

        /// <summary>
        /// Test Method for Limited Program
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_Regsys_LP_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_Regsys_Lp(emailAddress);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Regsys_LP_UNITTEST_002(string emailAddress, int communicationId)
        {
            TriggeredRequestBase newRequest = Create_Regsys_Lp_002(emailAddress, communicationId);
            return CallSend(newRequest);
        }

        /// <summary>
        /// Test Method for Limited Program
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_LP_V2_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_TBN_LP_Request_Packet(emailAddress);
            return CallSendV2(newRequest);
        }

        /// <summary>
        /// Test Method for Partner(Generic)
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_Partner_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_TBN_Partner_Request_Packet(emailAddress);
            return CallSend(newRequest);
        }

        /// <summary>
        /// Test Method for Partner(Generic)
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_Partner_V2_UNITTEST(string emailAddress)
        {
            TriggeredRequestBase newRequest = Create_TBN_Partner_Request_Packet(emailAddress);
            return CallSendV2(newRequest);
        }

        /// <summary>
        /// Test Method for World Wide Events
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_WWE_UNITTEST(string emailAddress)
        {
            EventRequestData newRequest = Create_TBN_WWE_Request_Packet(emailAddress);
            return CallSend(newRequest);
        }

        /// <summary>
        /// Test Method for World Wide Events
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_WWE_V2_UNITTEST(string emailAddress)
        {
            EventRequestData newRequest = Create_TBN_WWE_Request_Packet(emailAddress);
            return CallSendV2(newRequest);
        }

        /// <summary>
        /// Test Method for World Wide Events
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_Event_UNITTEST(string emailAddress)
        {
            EventRequestData newRequest = Create_TBN_Event_Request_Packet(emailAddress);
            /*Guid reqID;
            string errorMsg;
            EventRequestData newRequest = SendBulkEmail("TBN_1234", "not required",
                new Dictionary<string, Dictionary<string, string>> 
                { 
                    {"v-assiva@microsoft.com",new Dictionary<string,string>
                        {
                            {"FirstName", "Ashokan"},
                            {"LastName1", "Sivapragasam"}
                        }
                    },
                    {"v-kukhus@microsoft.com",new Dictionary<string,string>
                        {
                            {"FirstName", "Vinoth"},
                            {"LastName1", "Sivapragasam"}
                        }
                    }
                }
                , out reqID, out errorMsg);*/

            return CallSend(newRequest);
        }

        private static string CreateTbnWweRequestMipUnittest(string emailAddress, string firstName, string lastName)
        {
            EventRequestData newRequest = CreateTbnEventRequestMipNormalSize(emailAddress, firstName, lastName);
            return CallSendV2(newRequest);
        }
        

        /// <summary>
        /// Test Method for Generic TBN
        /// </summary>
        /// <returns></returns>
        private static string Create_TBN_Generic_UNITTEST_2(string emailAddress, string firstName, string imageHyperLink)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV1(emailAddress, firstName, imageHyperLink);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_3(string emailAddress, string firstName, string lastName, string customData)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV2(emailAddress, firstName, lastName, customData);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_4(string emailAddress, string firstName)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV3(emailAddress, firstName);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_5(string emailAddress, string body01, string body02)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV4(emailAddress, body01, body02);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_6(string emailAddress, string firstName, string lastName, string customData)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV5(emailAddress, firstName, lastName, customData);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_7(string emailAddress, string firstName, string fullName)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV6(emailAddress, firstName, fullName);
            return CallSend(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_8(string emailAddress, string firstName, string fullName)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV7(emailAddress, firstName, fullName);
            return CallSendV2(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_8(string emailAddress, string firstName, string CustomSubjectPart, string EmailSender)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV8(emailAddress, firstName, CustomSubjectPart, EmailSender);
            return CallSendV2(newRequest);
        }

        private static string Create_TBN_Generic_UNITTEST_9(string emailAddress, string firstName)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV9(emailAddress, firstName);
            return CallSendV2(newRequest);
        }

        private static string CreateTbnGenericRequestMipUnittest10(string emailAddress, string subjectId, string firstName, string lastName)
        {
            TriggeredRequestBase newRequest = CreateTbnGenericRequestMip(emailAddress, subjectId, firstName, lastName);
            return CallSendV2(newRequest);
        }
        #endregion

        #region MSI_SUBSCRIPTION_METHODS
        /// <summary>
        /// Test Method for MSI Subscribe
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="communicationId"></param>
        /// <returns></returns>
        private static string MSI_SUBSCRIBE_V2(string emailAddress, int communicationId, int deliFormatID)
        {
            /*Creating Input Values*/
            string email = emailAddress; /*Enter your email address*/
            int commID = communicationId; /*Give your Communication ID*/
            //int deliFormatID = 0; /*0 = Html , 1 = Text*/
            var results = new EmailInterchangeResponseToken();
            results.Result = EmailInterchangeResult.None;

            /*Instantiating the Proxy Class*/
            var client = new InterchangeConnect();
            results = client.SubscribeV2(email, commID, deliFormatID);
            return (results.Result.ToString() + "; EI ID: " + results.EmailInterchangeId.ToString());
        }

        /// <summary>
        /// Test Method for MSI Unsubscribe
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="communicationId"></param>
        /// <returns></returns>
        private static string MSI_UNSUBSCRIBE_V2(string emailAddress, int communicationId)
        {
            /*Creating Input Values*/
            string email = emailAddress; /*Enter your email address*/
            int commID = communicationId; /*Give your Communication ID*/
            int deliFormatID = 0; /*0 = Html , 1 = Text*/
            var results = new EmailInterchangeResponseToken();
            results.Result = EmailInterchangeResult.None;

            /*Instantiating the Proxy Class*/
            var client = new InterchangeConnect();
            results = client.UnsubscribeV2(email, commID, deliFormatID);
            return (results.Result.ToString() + "; EI ID: " + results.EmailInterchangeId.ToString());
        }

        /// <summary>
        /// Test Method for MSI Ping
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="communicationId"></param>
        /// <returns></returns>
        private static string MSI_PING()
        {
            /*Creating Input Values*/
            var results = new EmailInterchangeResponseToken();
            results.Result = EmailInterchangeResult.None;

            /*Instantiating the Proxy Class*/
            var client = new InterchangeConnect();
            return (results.Result.ToString() + "; EI ID: " + results.EmailInterchangeId.ToString());
        }

        #endregion

        #region HELPER_METHODS
        private static string CallRequestStatus(System.Guid emailInterchangeId, AzureServiceReference.RequestTypeForEnhancedAPI requestType)
        {
            var interchangerequeststatus = new InterchangeRequestStatus();
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            interchangerequeststatus = client.GetRequestStatus(emailInterchangeId, requestType);
            return string.Format(StringResources.REQUEST_STATUS_MESSAGE, emailInterchangeId + Convert.ToString(interchangerequeststatus.RequestStatusMessage));
        }

        private static string CallSend(RequestBase newRequest)
        {
            EmailInterchangeResult results = EmailInterchangeResult.None;

            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            results = client.Send(newRequest);
            return (results.ToString());
        }

        private static string CallSendV2(RequestBase newRequest)
        {
            EmailInterchangeResponseToken results = new EmailInterchangeResponseToken();
            results.Result = EmailInterchangeResult.None;

            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            results = client.SendV2(newRequest);
            return (results.Result.ToString() + "; EI ID: " + results.EmailInterchangeId.ToString());
        }

        private static TriggeredRequestBase Create_TBN_TAGM_Request_Packet(string emailAddress)
        {
            /* Instantiating a new Subscriber (TAGM TBN)*/
            TagmTriggerSubscriber newSubscriber = new TagmTriggerSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            string Wizard1 = "00000000-0000-0000-0000-000000010018";
            string Wizard2 = "00000000-0000-0000-0000-000000010019";
            Guid[] WizardIDs = new Guid[2];
            WizardIDs[0] = new Guid(Wizard1);// (strGuid[0].ToString());
            WizardIDs[1] = new Guid(Wizard2);

            string[] Question = { "00000000-0000-0000-0000-000000003174", "00000000-0000-0000-0000-000000003175" };
            Guid[] QuestionIDs = new Guid[2]; //new Guid({new Guid(Question[0],new Guid(Question[1]});
            QuestionIDs[0] = new Guid(Question[0]);
            QuestionIDs[1] = new Guid(Question[1]);

            // population of all properties

            newSubscriber.FirstName = "Amit";
            newSubscriber.MiddleName = "Kumar";
            newSubscriber.LastName1 = "Gupta";
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            newSubscriber.WizardIds = WizardIDs; //{ "00000000-0000-0000-0000-000000010018" };
            newSubscriber.QuestionIds = QuestionIDs; // { "00000000-0000-0000-0000-000000003174", "00000000-0000-0000-0000-000000003175" };
            newSubscriber.RegistrationDate = DateTime.UtcNow;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "Amit";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName";
            attribute.Value = "Kumar";
            newSubscriber.Attributes[1] = attribute;

            //attribute.Name = "DynamicParameter2";
            //attribute.Value = "Ashok";
            //newSubscriber.Attributes[1] = attribute;

            newSubscriber.WizardData = @"  <CustomerAnswer>
    <answer questionId='6669D5DA-154E-43ED-B67A-13C20277C7E3' answerId='293F1B4B-F694-42FC-A0B3-75BECFFC3809' answerText='Intermediate – I have created a few programs, I can write code from scratch and solve problems without the aid of others.' />
    <answer questionId='00000000-0000-0000-0000-000000000007' answerId='00000000-0000-0000-0000-000000000007' answerText='' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='A7ABF500-C4E9-4285-A9AA-0305DF4BBFE3' answerText='PHP' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='EE5CDB47-F777-4F02-B085-20E890E1D106' answerText='HTML' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='0F9690CD-DD4A-46F6-BFE3-2E8313F87E95' answerText='Visual Basic .NET' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='92977BDE-BD1C-4632-B172-3092B8F10EF3' answerText='AJAX' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='93DA9166-40A3-46AD-B4FD-4AB346A691E3' answerText='C#' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='8C3699D4-86E1-4273-87EB-531DE2EBA365' answerText='C/C++' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='C72A6E10-D362-4C0A-9BF3-62657F19A489' answerText='VBScript, JScript, or JavaScript' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='D18D7057-F797-4215-B123-6F91863AB95E' answerText='Java' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='F32BAB0C-5BA3-4898-84D1-7244B8D70A90' answerText='SQL' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='005D4CBC-9EA4-4965-ACC0-7F1FFF3C67C0' answerText='Ruby on Rails' />
    <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='DC2D1CB8-5B4A-4D8E-8153-D78BF3F2BA26' answerText='ASP.NET' />
    <answer questionId='4FF5DE43-03EB-4315-8A26-44F2F5ACE5EB' answerId='ACEA469A-3556-4475-B1EF-84F4D91D6B22' answerText='Developer' />
    <answer questionId='3B238FD0-60E0-4A79-9268-F7F41BC396B4' answerId='6D370848-43D9-4C66-A4C5-1CEFF2E21D5F' answerText='Yes' />
    <answer questionId='00000000-0000-0000-0000-000000000016' answerId='5B81B4A0-CA43-4B9E-AA85-73BBF962610B' answerText='United States' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='F399AA75-9143-45E8-874A-38AEC322AE91' answerText='Windows 7' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='FB3B4AE2-D3F8-4077-A1FA-60D56C1B8B27' answerText='Window Forms and Smart Client' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='BD96B8CC-2494-4475-8261-66EDA32ACBA7' answerText='SQL Server and Data Access' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='C9CD0E6B-96E2-40A7-A792-6B52FCDB6EA4' answerText='Application Architecture and Patterns' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='C497A001-3A1A-4A78-993A-7B68B73DEDD0' answerText='Unix or Linux Interoperability' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='E0F41501-3707-45BA-8A57-85894051A47F' answerText='ASP.NET MVC' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='6D51482E-18A3-4365-9BC0-94510561476B' answerText='Windows Phone Development' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='023A0D98-7604-44DF-A9EE-A70AE352502B' answerText='Office Client Development/VSTO (Outlook, Excel etc)' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='E0466D6E-4D9F-4C6B-841A-CF800312DEA0' answerText='Visual Studio 2008' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='74AA91BC-6734-49A4-9A40-EDD879A35D0C' answerText='Windows Server System' />
    <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='8E51E532-BB05-4D87-A225-FA5C1CFBC0FB' answerText='Web Development' />
    <answer questionId='C7E7002F-6B24-41BF-BA21-78805AFC0C0D' answerId='902EFEE9-E897-43B0-B8A7-23263AC0260D' answerText='Work projects related to a second business (including moonlighting or weekend entrepreneurial activities)' />
  </CustomerAnswer>";

            /* Creating a new request*/
            TriggeredRequestBase newRequest = new TriggeredRequestBase();

            newRequest.ApplicationName = "Regsys Profile Center";

            //newRequest.CommunicationId = 103058;   //  For 86991
            newRequest.CommunicationId = 127038; //126964;   //  For 39327
            //newRequest.CommunicationId = 126744;    //  For 87092

            //Done for negative test case
            //newRequest.CommunicationId = 126739;    //  For 87092
            //newRequest.CommunicationId = 127349;    //  For 86991

            newRequest.DeliveryType = EmailType.Html;
            newRequest.TriggerType = TriggeredSendType.NoDelay;
            newRequest.TriggerDataSource = TriggerDataSource.MSIndividual;
            newRequest.EventDescription = "Some Business Reason";
            newRequest.ConversationId = Guid.NewGuid().ToString();

            newRequest.Subscribers = new TagmTriggerSubscriber[1];
            newRequest.Subscribers[0] = newSubscriber;
            return newRequest;
        }

        private static TriggeredRequestBase Create_TBN_TAGM_Request_Packet_002(string emailAddress)
        {
            /* Instantiating a new Subscriber (TAGM TBN)*/
            TagmTriggerSubscriber newSubscriber = new TagmTriggerSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            string Wizard1 = "a7f7efe5-926a-47a6-8d62-c8a1e4bce55d";
            Guid[] WizardIDs = new Guid[1];
            WizardIDs[0] = new Guid(Wizard1);

            string[] Question = { "a2e20551-8c98-48dc-8342-b5516a8583f4", "1f14a091-1309-4316-bc6c-2dc45cd8e265", "00000000-0000-0000-0000-000000050005", "5f4b93fe-fbc9-4d9e-a0cf-54282ea10c08" };
            Guid[] QuestionIDs = new Guid[4];
            QuestionIDs[0] = new Guid(Question[0]);
            QuestionIDs[1] = new Guid(Question[1]);
            QuestionIDs[1] = new Guid(Question[2]);
            QuestionIDs[1] = new Guid(Question[3]);

            // population of all properties

            newSubscriber.FirstName = "Amit";
            newSubscriber.MiddleName = "Kumar";
            newSubscriber.LastName1 = "Gupta";
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            newSubscriber.WizardIds = WizardIDs; //{ "00000000-0000-0000-0000-000000010018" };
            newSubscriber.QuestionIds = QuestionIDs; // { "00000000-0000-0000-0000-000000003174", "00000000-0000-0000-0000-000000003175" };
            newSubscriber.RegistrationDate = DateTime.UtcNow;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "Amit";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName1";
            attribute.Value = "Kumar";
            newSubscriber.Attributes[1] = attribute;

            //attribute.Name = "DynamicParameter2";
            //attribute.Value = "Ashok";
            //newSubscriber.Attributes[1] = attribute;

            newSubscriber.WizardData = @"<CustomerAnswer><answer questionId='00000000-0000-0000-0000-000000050005' answerId='00000000-0000-0000-0000-000000050005' answerText='Equiniti Pension Solutions' /><answer questionId='5f4b93fe-fbc9-4d9e-a0cf-54282ea10c08' answerId='424cd2c4-80de-46e0-9e72-8dd5d181c9a4' answerText='500+ employees' /></CustomerAnswer>";

            /* Creating a new request*/
            TriggeredRequestBase newRequest = new TriggeredRequestBase();

            newRequest.ApplicationName = "Regsys Profile Center";

            //newRequest.CommunicationId = 103058;   //  For 86991
            newRequest.CommunicationId = 126326; //126964;   //  For 39327
            //newRequest.CommunicationId = 126744;    //  For 87092

            //Done for negative test case
            //newRequest.CommunicationId = 126739;    //  For 87092
            //newRequest.CommunicationId = 127349;    //  For 86991

            newRequest.DeliveryType = EmailType.Html;
            newRequest.TriggerType = TriggeredSendType.NoDelay;
            newRequest.TriggerDataSource = TriggerDataSource.MSIndividual;
            newRequest.EventDescription = "Some Business Reason";
            newRequest.ConversationId = Guid.NewGuid().ToString();

            newRequest.Subscribers = new TagmTriggerSubscriber[1];
            newRequest.Subscribers[0] = newSubscriber;
            return newRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static TriggeredRequestBase Create_TBN_LP_Request_Packet(string emailAddress)
        {
            /* Instantiating a new Subscriber (LP TBN)*/
            LimitedProgramSubscriber newSubscriber = new LimitedProgramSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = System.Guid.NewGuid().ToString();

            string Wizard1 = "00000000-0000-0000-0000-000000010018";
            string Wizard2 = "00000000-0000-0000-0000-000000010019";
            Guid[] WizardIDs = new Guid[2];
            WizardIDs[0] = new Guid(Wizard1);
            WizardIDs[1] = new Guid(Wizard2);
            newSubscriber.WizardIds = WizardIDs;

            string[] Question = { "00000000-0000-0000-0000-000000003174", "00000000-0000-0000-0000-000000003175" };
            Guid[] QuestionIDs = new Guid[2]; //new Guid({new Guid(Question[0],new Guid(Question[1]});
            QuestionIDs[0] = new Guid(Question[0]);
            QuestionIDs[1] = new Guid(Question[1]);
            newSubscriber.QuestionIds = QuestionIDs;

            newSubscriber.FirstName = "Amit";
            newSubscriber.MiddleName = "Kumar";
            newSubscriber.LastName1 = "Gupta";
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();
            newSubscriber.RegistrationDate = DateTime.UtcNow.AddDays(5);

            //addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "Amit";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName";
            attribute.Value = "Kumar";
            newSubscriber.Attributes[1] = attribute;

            newSubscriber.WizardData = @"  <CustomerAnswer>
            <answer questionId='6669D5DA-154E-43ED-B67A-13C20277C7E3' answerId='293F1B4B-F694-42FC-A0B3-75BECFFC3809' answerText='Intermediate – I have created a few programs, I can write code from scratch and solve problems without the aid of others.' />
            <answer questionId='00000000-0000-0000-0000-000000000007' answerId='00000000-0000-0000-0000-000000000007' answerText='' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='A7ABF500-C4E9-4285-A9AA-0305DF4BBFE3' answerText='PHP' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='EE5CDB47-F777-4F02-B085-20E890E1D106' answerText='HTML' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='0F9690CD-DD4A-46F6-BFE3-2E8313F87E95' answerText='Visual Basic .NET' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='92977BDE-BD1C-4632-B172-3092B8F10EF3' answerText='AJAX' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='93DA9166-40A3-46AD-B4FD-4AB346A691E3' answerText='C#' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='8C3699D4-86E1-4273-87EB-531DE2EBA365' answerText='C/C++' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='C72A6E10-D362-4C0A-9BF3-62657F19A489' answerText='VBScript, JScript, or JavaScript' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='D18D7057-F797-4215-B123-6F91863AB95E' answerText='Java' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='F32BAB0C-5BA3-4898-84D1-7244B8D70A90' answerText='SQL' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='005D4CBC-9EA4-4965-ACC0-7F1FFF3C67C0' answerText='Ruby on Rails' />
            <answer questionId='A706BB3F-B5DB-41F9-9504-2ACC7A1B21A1' answerId='DC2D1CB8-5B4A-4D8E-8153-D78BF3F2BA26' answerText='ASP.NET' />
            <answer questionId='4FF5DE43-03EB-4315-8A26-44F2F5ACE5EB' answerId='ACEA469A-3556-4475-B1EF-84F4D91D6B22' answerText='Developer' />
            <answer questionId='3B238FD0-60E0-4A79-9268-F7F41BC396B4' answerId='6D370848-43D9-4C66-A4C5-1CEFF2E21D5F' answerText='Yes' />
            <answer questionId='00000000-0000-0000-0000-000000000016' answerId='5B81B4A0-CA43-4B9E-AA85-73BBF962610B' answerText='United States' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='F399AA75-9143-45E8-874A-38AEC322AE91' answerText='Windows 7' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='FB3B4AE2-D3F8-4077-A1FA-60D56C1B8B27' answerText='Window Forms and Smart Client' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='BD96B8CC-2494-4475-8261-66EDA32ACBA7' answerText='SQL Server and Data Access' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='C9CD0E6B-96E2-40A7-A792-6B52FCDB6EA4' answerText='Application Architecture and Patterns' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='C497A001-3A1A-4A78-993A-7B68B73DEDD0' answerText='Unix or Linux Interoperability' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='E0F41501-3707-45BA-8A57-85894051A47F' answerText='ASP.NET MVC' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='6D51482E-18A3-4365-9BC0-94510561476B' answerText='Windows Phone Development' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='023A0D98-7604-44DF-A9EE-A70AE352502B' answerText='Office Client Development/VSTO (Outlook, Excel etc)' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='E0466D6E-4D9F-4C6B-841A-CF800312DEA0' answerText='Visual Studio 2008' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='74AA91BC-6734-49A4-9A40-EDD879A35D0C' answerText='Windows Server System' />
            <answer questionId='1DEE58AD-9102-4ABB-831B-5B4F6619F9D1' answerId='8E51E532-BB05-4D87-A225-FA5C1CFBC0FB' answerText='Web Development' />
            <answer questionId='C7E7002F-6B24-41BF-BA21-78805AFC0C0D' answerId='902EFEE9-E897-43B0-B8A7-23263AC0260D' answerText='Work projects related to a second business (including moonlighting or weekend entrepreneurial activities)' />
          </CustomerAnswer>";

            /* Creating a new request*/
            TriggeredRequestBase newRequest = new TriggeredRequestBase();
            newRequest.ApplicationName = "Limited Program";
            newRequest.CommunicationId = 126722;
            newRequest.DeliveryType = EmailType.Html;
            newRequest.TriggerType = TriggeredSendType.NoDelay;
            newRequest.TriggerDataSource = TriggerDataSource.MSIndividual;
            newRequest.EventDescription = "Some Business Reason";

            newRequest.Subscribers = new LimitedProgramSubscriber[1];
            newRequest.Subscribers[0] = newSubscriber;
            return newRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private static TriggeredRequestBase Create_Regsys_Lp(string emailAddress)
        {
            var request = new TriggeredRequestBase();
            request.ApplicationName = "RegSys Adopter";
            request.DeliveryType = EmailType.Html;
            request.TriggerType = TriggeredSendType.NoDelay;
            request.TriggerDataSource = TriggerDataSource.MSIndividual;
            request.EventDescription = string.Empty;
            request.CommunicationId = 126273;//126393;//126723;
            request.ConversationId = Guid.NewGuid().ToString();

            string[] Question = { "00000000-0000-0000-0000-000000003174", "00000000-0000-0000-0000-000000003175" };
            Guid[] QuestionIDs = new Guid[2]; //new Guid({new Guid(Question[0],new Guid(Question[1]});
            QuestionIDs[0] = new Guid(Question[0]);
            QuestionIDs[1] = new Guid(Question[1]);

            //addition of custom attributes
            var attribs = new AzureTBNClientSDK.AzureServiceReference.Attribute[4];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = "XmlField";
            attribs[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName";
            attribute.Value = "Kumar";
            attribs[1] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LCID";
            attribute.Value = "1033";
            attribs[2] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "XmlField";
            attribute.Value = "<XmlFields><XmlField id='1'>Ashok<XmlField><XmlField id='2'>Ashok<XmlField><XmlField id='3'>Ashok<XmlField></XmlFields>";
            attribs[3] = attribute;

            string Wizard1 = "77c8613e-7465-4926-817a-1604ceb4885c";
            Guid[] WizardIDs = new Guid[1];
            WizardIDs[0] = new Guid(Wizard1);

            request.LimitedProgramId = 12345;
            var subscriber = new LimitedProgramSubscriber()
            {
                FirstName = "Senba",
                MiddleName = "",
                LastName1 = "Kumar",
                LastName2 = "",
                NamePrefix = "",
                NameSuffix = "",
                WizardIds = WizardIDs,
                QuestionIds = QuestionIDs,
                EmailAddress = emailAddress,
                SubscriberKey = Guid.NewGuid().ToString(),
                Attributes = attribs,
                RegistrationDate = DateTime.UtcNow,
                WizardData = "CustomerAnswer"
            };
            request.Subscribers = new SubscriberBase[] { subscriber };
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private static TriggeredRequestBase Create_Regsys_Lp_002(string emailAddress, int communicationId)
        {
            var request = new TriggeredRequestBase();
            request.ApplicationName = "RegSys Adopter";
            request.DeliveryType = EmailType.Html;
            request.TriggerType = TriggeredSendType.NoDelay;
            request.TriggerDataSource = TriggerDataSource.MSIndividual;
            request.EventDescription = string.Empty;
            request.CommunicationId = communicationId; //126326
            request.ConversationId = Guid.NewGuid().ToString();

            string[] Question = { "a2e20551-8c98-48dc-8342-b5516a8583f4", "1f14a091-1309-4316-bc6c-2dc45cd8e265", "00000000-0000-0000-0000-000000050005", "5f4b93fe-fbc9-4d9e-a0cf-54282ea10c08" };
            Guid[] QuestionIDs = new Guid[4];
            QuestionIDs[0] = new Guid(Question[0]);
            QuestionIDs[1] = new Guid(Question[1]);
            QuestionIDs[1] = new Guid(Question[2]);
            QuestionIDs[1] = new Guid(Question[3]);

            //addition of custom attributes
            var attribs = new AzureTBNClientSDK.AzureServiceReference.Attribute[1];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LCID";
            attribute.Value = "2057";
            attribs[0] = attribute;

            string Wizard1 = "a7f7efe5-926a-47a6-8d62-c8a1e4bce55d";
            Guid[] WizardIDs = new Guid[1];
            WizardIDs[0] = new Guid(Wizard1);

            request.LimitedProgramId = 12345;
            var subscriber = new LimitedProgramSubscriber()
            {
                FirstName = "Andy",
                MiddleName = "",
                LastName1 = "Hart",
                LastName2 = "Lotto",
                NamePrefix = "Mr.",
                NameSuffix = "Principal",
                WizardIds = WizardIDs,
                QuestionIds = QuestionIDs,
                EmailAddress = emailAddress,
                SubscriberKey = Guid.NewGuid().ToString(),
                Attributes = attribs,
                RegistrationDate = DateTime.UtcNow,
                WizardData = "&lt;CustomerAnswer&gt;&amp;lt;answer questionId='00000000-0000-0000-0000-000000050005' answerId='00000000-0000-0000-0000-000000050005' answerText='Equiniti Pension Solutions' /&amp;gt;&amp;lt;answer questionId='5f4b93fe-fbc9-4d9e-a0cf-54282ea10c08' answerId='424cd2c4-80de-46e0-9e72-8dd5d181c9a4' answerText='500+ employees' /&amp;gt;&lt;/CustomerAnswer&gt;"
            };
            request.Subscribers = new SubscriberBase[] { subscriber };
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static TriggeredRequestBase Create_TBN_Partner_Request_Packet(string emailAddress)
        {
            /* Instantiating a new Subscriber (Generic TBN)*/
            GenericSubscriber newSubscriber = new GenericSubscriber();
            //newSubscriber.EmailAddress = "amgupta@microsoft.com";
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = System.Guid.NewGuid().ToString();
            //addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "Amit";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName";
            attribute.Value = "Kumar";
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Partner";

            //request.CommunicationId = 103058;   //  For 86991
            //request.CommunicationId = 126964;   //  For 39327
            //request.CommunicationId = 126744;    //  For 87092
            request.CommunicationId = 51;    //  For 87092

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            request.Subscribers[0] = newSubscriber;
            return request;
        }

        private static EventRequestData Create_TBN_WWE_Request_Packet(string emailAddress)
        {
            /* Instantiating a new Subscriber (WWE TBN)*/
            EventSubscriber newSubscriber = new EventSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.Body = @"---------------------------
        
        ---------------------------
        Exception Thank you for registering for Azure Boot Camp. This email confirms your registration for this event.
        
        Name: aa fn bb ln
        
        Confirmation Number: 1305803409
        
        Event Code: 1032446140
        Event Name: Azure Boot Camp
        
        Location: Microsoft Office - Houston 
        Room: 
        City: Houston
        
        Start Date: 2010-05-24 08:00:00
        Start Time: 2010-05-24 08:00:00
        
        End Date: 2010-05-25 17:00:00
        End Time: 2010-05-25 17:00:00
        
        Please click this link to access your admission ticket to the event 
        https://tk3perfwwewb06.dns.microsoft.com/cui/r.aspx?t=1&amp;c=en-us&amp;r=1305803409&amp;h=&amp;a=0
        
        Thank you for your interest in Microsoft Events.  We look forward to seeing you at the event!
        
        Microsoft respects your privacy. Please read our online Privacy Statement. http://go.microsoft.com/fwlink/?LinkId=74170.
        ---------------------------
        OK   
        ---------------------------
        ";
            newSubscriber.CampaignCode = "MSEVNT01";
            newSubscriber.EmailTypeId = 1;
            newSubscriber.EventId = 7328;
            newSubscriber.From = emailAddress;
            newSubscriber.LocaleId = 51;
            newSubscriber.ReplyTo = emailAddress;
            newSubscriber.Subject = "Date for event you registered has been changed";
            newSubscriber.SubscriberKey = System.Guid.NewGuid().ToString();
            newSubscriber.TargetAudience = "SQL Server 11 - Next Gen Data Processing";
            newSubscriber.TargetProduct = "SQL Server";
            newSubscriber.WweRequestSendDateTime = DateTime.UtcNow;

            //addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "Amit";
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName";
            attribute.Value = "Kumar";
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            EventRequestData newRequest = new EventRequestData();
            newRequest.ApplicationName = "World Wide Events";

            newRequest.BatchId = System.Guid.NewGuid();
            newRequest.TemplateId = "WWE_Delhi–US_1";
            newRequest.RequestExecutionPriority = RequestExecutionPriorityLevels.High;
            newRequest.ConversationId = Guid.NewGuid().ToString();

            newRequest.TriggerType = TriggeredSendType.Batch;

            newRequest.Subscribers = new EventSubscriber[1];

            for (int i = 0; i < 1; i++)
            {
                newRequest.Subscribers[i] = newSubscriber;
            }
            return newRequest;
        }

        private static EventRequestData Create_TBN_Event_Request_Packet(string emailAddress)
        {
            /* Instantiating a new Subscriber (WWE TBN)*/
            EventSubscriber newSubscriber = new EventSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.Body = @"
        Thank you for registering for Azure Boot Camp. This email confirms your registration for this event.
        
        Name: aa fn bb ln
        
        Confirmation Number: 1305803409
        
        Event Code: 1032446140
        Event Name: Azure Boot Camp
        
        Location: Microsoft Office - Houston 
        Room: 
        City: Houston
        
        Start Date: 2010-05-24 08:00:00
        Start Time: 2010-05-24 08:00:00
        
        End Date: 2010-05-25 17:00:00
        End Time: 2010-05-25 17:00:00
        
        Please click this link to access your admission ticket to the event 
        https://tk3perfwwewb06.dns.microsoft.com/cui/r.aspx?t=1&amp;c=en-us&amp;r=1305803409&amp;h=&amp;a=0
        
        Thank you for your interest in Microsoft Events.  We look forward to seeing you at the event!
        
        Microsoft respects your privacy. Please read our online Privacy Statement. http://go.microsoft.com/fwlink/?LinkId=74170.
        ";
            newSubscriber.CampaignCode = "MSEVNT01";
            newSubscriber.EmailTypeId = 1;
            newSubscriber.EventId = 7328;
            newSubscriber.From = emailAddress;
            newSubscriber.LocaleId = 51;
            newSubscriber.ReplyTo = emailAddress;
            newSubscriber.Subject = "Date for event you registered has been changed";
            newSubscriber.SubscriberKey = System.Guid.NewGuid().ToString();
            newSubscriber.TargetAudience = "SQL Server 11 - Next Gen Data Processing";
            newSubscriber.TargetProduct = "SQL Server";
            newSubscriber.WweRequestSendDateTime = DateTime.UtcNow;

            //addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "Amit";
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName1";
            attribute.Value = "Kumar";
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            EventRequestData newRequest = new EventRequestData();
            newRequest.ApplicationName = "World Wide Events";

            newRequest.BatchId = System.Guid.NewGuid();
            //newRequest.TemplateId = "WWE_Template_1";
            newRequest.CustomerKey = "TBN_1234";
            newRequest.RequestExecutionPriority = RequestExecutionPriorityLevels.High;
            newRequest.ConversationId = Guid.NewGuid().ToString();

            newRequest.TriggerType = TriggeredSendType.Batch;

            newRequest.Subscribers = new EventSubscriber[3];

            for (int i = 0; i < 3; i++)
            {
                newRequest.Subscribers[i] = newSubscriber;
            }
            return newRequest;
        }

        private static EventRequestData CreateTbnEventRequestMipNormalSize(string emailAddress, string firstName, string lastName)
        {
            /* Creating a new request*/
            EventRequestData newRequest = new EventRequestData();
            newRequest.ApplicationName = "World Wide Events";

            newRequest.BatchId = System.Guid.NewGuid();
            newRequest.CustomerKey = "TbnWweEmailSendDefn001";
            newRequest.RequestExecutionPriority = RequestExecutionPriorityLevels.High;
            newRequest.ConversationId = Guid.NewGuid().ToString();

            newRequest.TriggerType = TriggeredSendType.Batch;
            newRequest.Subscribers = new EventSubscriber[3];

            EventSubscriber newSubscriber;
            for (int i = 0; i < 3; i++)
            {
                /* Instantiating a new Subscriber (WWE TBN)*/
                newSubscriber = new EventSubscriber();
                newSubscriber.EmailAddress = emailAddress;
                newSubscriber.Body = @"
                Thank you for registering for Azure Boot Camp. This email confirms your registration for this event.
        
                Name: aa fn bb ln
        
                Confirmation Number: 1305803409
        
                Event Code: 1032446140
                Event Name: Azure Boot Camp
        
                Location: Microsoft Office - Houston 
                Room: 
                City: Houston
        
                Start Date: 2010-05-24 08:00:00
                Start Time: 2010-05-24 08:00:00
        
                End Date: 2010-05-25 17:00:00
                End Time: 2010-05-25 17:00:00
        
                Please click this link to access your admission ticket to the event 
                https://tk3perfwwewb06.dns.microsoft.com/cui/r.aspx?t=1&amp;c=en-us&amp;r=1305803409&amp;h=&amp;a=0
        
                Thank you for your interest in Microsoft Events.  We look forward to seeing you at the event!
        
                Microsoft respects your privacy. Please read our online Privacy Statement. http://go.microsoft.com/fwlink/?LinkId=74170.
                ";
                newSubscriber.CampaignCode = "MsWweMipEvent001";
                newSubscriber.EmailTypeId = 1;
                newSubscriber.EventId = 1;
                newSubscriber.From = emailAddress;
                newSubscriber.LocaleId = 2;
                newSubscriber.ReplyTo = emailAddress;
                newSubscriber.SubscriberKey = System.Guid.NewGuid().ToString();
                newSubscriber.Subject = "Tbn Wwe Mip";
                newSubscriber.TargetAudience = "SQL Server 11 - Next Gen Data Processing";
                newSubscriber.TargetProduct = "SQL Server";
                newSubscriber.WweRequestSendDateTime = DateTime.UtcNow;

                //addition of custom attributes
                newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
                AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

                attribute.Name = "FirstName";
                attribute.Value = firstName;
                newSubscriber.Attributes[0] = attribute;

                attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
                attribute.Name = "LastName1";
                attribute.Value = lastName;
                newSubscriber.Attributes[1] = attribute;

                newRequest.Subscribers[i] = newSubscriber;
            }
            return newRequest;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV2(string emailAddress, string firstName, string lastName, string customData)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[3];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName";
            attribute.Value = firstName;
            newSubscriber.Attributes[1] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "CustomData";
            attribute.Value = firstName;
            newSubscriber.Attributes[2] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CommunicationId = 201345; //TBN_103050 //TBN_201345
            //request.CustomerKey = "MsPhotoBooth";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV1(string emailAddress, string firstName, string imageHyperLink)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[1];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "ImageHyperLink";
            attribute.Value = imageHyperLink;
            newSubscriber.Attributes[0] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            //request.CommunicationId = 201508; //TBN_103050
            request.CustomerKey = "MsPhotoBooth";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV3(string emailAddress, string firstName)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[1];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CommunicationId = 126296; //TBN_103050
            //request.CustomerKey = "EIOnboardingAshishTbn001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV4(string emailAddress, string body01, string body02)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "body01";
            attribute.Value = body01;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "body02";
            attribute.Value = body02;
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CustomerKey = "IamRaveTriggerSendDefinition001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV5(string emailAddress, string firstName, string lastName, string customData)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[3];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName";
            attribute.Value = lastName;
            newSubscriber.Attributes[1] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "CustomData";
            attribute.Value = customData;
            newSubscriber.Attributes[2] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CustomerKey = "TBN_201345";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV6(string emailAddress, string firstName, string fullName)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FullName";
            attribute.Value = fullName;
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CustomerKey = "EI_Ashish_Send_Defn_001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV7(string emailAddress, string firstName, string fullName)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[4];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "ProgramName";
            attribute.Value = "CSP 1T Reseller";
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "InvitationUrl";
            attribute.Value = "https://partnerincentives.microsoft.com";
            newSubscriber.Attributes[1] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "Locale";
            attribute.Value = "en-US";
            newSubscriber.Attributes[2] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "UserName";
            attribute.Value = "";
            newSubscriber.Attributes[3] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CustomerKey = "PI_Enrollment_AdminInvitation";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV8(string emailAddress, string firstName, string CustomSubjectPart, string EmailSender)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[3];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "CustomSubjectPart";
            attribute.Value = CustomSubjectPart;
            newSubscriber.Attributes[1] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "EmailSender";
            attribute.Value = EmailSender;
            newSubscriber.Attributes[2] = attribute;


            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "PcsApp";
            request.CustomerKey = "PcsSendDefn001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase Create_TBN_Generic_Request_PacketV9(string emailAddress, string firstName)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[1];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "FirstName";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "EiDemo";
            request.CustomerKey = "MipTbnDefn001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        private static TriggeredRequestBase CreateTbnGenericRequestMip(string emailAddress, string subjectId, string firstName, string lastName)
        {
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "div01";
            attribute.Value = firstName;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "div02";
            attribute.Value = lastName;
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "MipDemo";
            request.CustomerKey = "TbnEiHtmlPaste001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            return request;
        }

        #region TBN_METHODS
        private static bool SendTriggeredWelcomeEmail(string address)
        {
            if (address == null)
            {
                return false;
            }

            // Create the subscriber
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = address;
            // Create the request
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Sales Integration";
            request.CustomerKey = "TBN_SSB_Trigger_send_Defination";
            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;
            request.Subscribers = new GenericSubscriber[1];
            request.Subscribers[0] = newSubscriber;

            EmailInterchangeResult result = EmailInterchangeResult.None;

            try
            {
                AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
                result = client.Send(request);

                return (result != EmailInterchangeResult.Success) ? false : true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured sending the welcome email. " + ex);
            }

            return false;
        }
        #endregion

        private static void WriteEmailDatalistToConsole(AzureTBNClientSDK.AzureServiceReference.EmailDataList emailDataList)
        {
            Console.WriteLine("Name\tSubject\tContentID\tCreatedDate");
            Console.WriteLine("================================================================================");
            foreach (EmailData line in emailDataList)
            {
                Console.WriteLine(line.Name.ToString() + "\t" + line.Subject.ToString() + "\t" + line.ContentID.ToString() + "\t" + line.CreatedDate.ToString());
            }
        }

        private static void WriteDataFolderListToConsole(DataFolderList dataFolderList)
        {
            Console.WriteLine("ID\tFolderName\tParentFolderID\tParentFolderName\tCreatedDate\tModifiedDate");
            Console.WriteLine("================================================================================");
            foreach (DataFolder line in dataFolderList)
            {
                Console.WriteLine(line.FolderID.ToString() + "\t" + line.FolderName.ToString() + "\t" + line.ParentFolder.FolderID.ToString() + "\t" + line.ParentFolder.FolderName + "\t" + line.CreatedDate.ToString() + "\t" + line.ModifiedDate.ToString());
            }
        }

        private static RequestForReturnEmailList PrepareRequestForReturnEmailList()
        {
            AzureTBNClientSDK.AzureServiceReference.RequestForReturnEmailList request = new RequestForReturnEmailList();

            request.TenantAccountID = 39327;

            //int NumberOfDays = 356;
            //DateTime Now = DateTime.Now;
            //request.Date = Now.AddDays(-NumberOfDays);

            request.FolderId = 621558;
            request.Date = System.Convert.ToDateTime("01-01-2012");

            return request;
        }

        # endregion

        #region IGNORE
        public void MonitorEntryPoint()
        {
            while (true)
            {
                try
                {
                    if (!isTaskQueueRequestsRunning)
                        System.Threading.Tasks.Task.Factory.StartNew(this.QueueRequests, System.Threading.Tasks.TaskCreationOptions.LongRunning);
                    if (!isTaskProcessRequestsRunning)
                        System.Threading.Tasks.Task.Factory.StartNew(this.ProcessRequests, System.Threading.Tasks.TaskCreationOptions.LongRunning);
                    //else if (taskCounter % 3 == 0)
                    //{
                    //    isTaskQueueRequestsRunning = false;
                    //    isTaskProcessRequestsRunning = false;
                    //}

                    taskCounter += 1;
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("sleeping");
                Thread.Sleep(3 * 1000);
            }
        }

        public void QueueRequests()
        {
            Console.WriteLine("QueueRequests");
            try
            {
                isTaskQueueRequestsRunning = true;
                throw new Exception("**QueueRequests**");
            }
            catch (Exception ex)
            {
                isTaskQueueRequestsRunning = false;
                throw new Exception("**QueueRequestsInnerException**");
            }
        }

        public void ProcessRequests()
        {
            Console.WriteLine("ProcessRequests");
            try
            {
                isTaskProcessRequestsRunning = true;
                //throw new Exception("**ProcessRequests**");
            }
            catch (Exception ex)
            {
                isTaskProcessRequestsRunning = false;
                throw new Exception("**ProcessRequestsInnerException**");
            }
        }
        #endregion
    }
}
