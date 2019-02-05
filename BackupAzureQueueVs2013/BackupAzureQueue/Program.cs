using Jscape.Sftp;
using Jscape.Ssh;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.DirectoryServices.AccountManagement;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using System.Xml.Xsl;
using EAGetMail;
using HtmlAgilityPack;
using Microsoft.Exchange.WebServices.Data;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Xml.Schema;
using System.Xml.Linq;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using System.Drawing;
using System.IO.Compression;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Azure.Services.Common;
using System.ServiceModel.Syndication;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
using System.Xml.XPath;
using Microsoft.ServiceBus.Messaging;
using StackExchange.Redis;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Web.Services3.Design;
using System.Collections.Concurrent;
using WinSCP;

namespace BackupAzureQueue
{
    public class Program : IBackupAzureQueue
    {
        class ReleaseEnvironment
        {
            public string environmentName { get; set; }
            public string variableName { get; set; }
            public string variableValue { get; set; }
        }

        public enum OtherConstants
        {
            ReconciliationSummaryData,
            MinuteMultiplierConstant = 60000
        }

        ManualResetEvent _doneEvent;
        int number;
        int _n = 0;
        static int sizeofw = 0;
        List<string> _list = new List<string>();
        // A semaphore that simulates a limited resource pool. 
        private static Semaphore _pool;
        // A padding interval to make the output more orderly. 
        private static int _padding;
        static System.Timers.Timer redisCacheConfigTimer; // From System.Timers
        public static Dictionary<string, SoapWebServiceConnectionProperties> soapWebServiceConnectionPropertiesLookup = new Dictionary<string, SoapWebServiceConnectionProperties>();

        public int counterTick = 0;

        public int Add(int number1, int number2)
        {
            return 1;
        }

        public Program()
        {
        }

        public Program(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }

        private static void redisCacheConfigTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ParseJson();
        }

        private static void ParseJson()
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("LogicAppTransformationSettingsdit2.redis.cache.windows.net:6380,password=68s8ds6OdH/efsS12+2O1rCorqajwk4bzEiTbBJ0ZRI=,ssl=True,abortConnect=False");
            //("LogicAppTransformationSettingsdit2.redis.cache.windows.net:6380,password=lIBDemvAhr7yggI2ju6XhZnHpbvp4IXpU+WXL4mTKyw=,ssl=True,abortConnect=False");
            IDatabase cache4 = connection.GetDatabase();
            //Console.WriteLine(cache4.StringGet("AllocadiaMapTransformKey002"));
            var sfmcEnterpriseAccountDetailsText = cache4.StringGet("SfmcEnterpriseAccountSecrets");

            var jObject = JArray.Parse("[{\"PseudoCollection\" : " + sfmcEnterpriseAccountDetailsText + "}]")[0];
            var sfmcEnterpriseAccountDetailsCollection = jObject.SelectToken("PseudoCollection");
            foreach (var sfmcEnterpriseAccountDetails in sfmcEnterpriseAccountDetailsCollection)
            {
                var sfmcEnterpriseAccountId = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcEnterpriseAccountId");
                if (soapWebServiceConnectionPropertiesLookup.ContainsKey(sfmcEnterpriseAccountId))
                {
                    soapWebServiceConnectionPropertiesLookup[sfmcEnterpriseAccountId] = new SoapWebServiceConnectionProperties
                    {
                        sfmcEnterpriseAccountId = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcEnterpriseAccountId"),
                        sfmcWebServiceEndpointUrl = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcWebServiceEndpointUrl"),
                        sfmcWebServiceUsername = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcWebServiceUsername"),
                        sfmcWebServicePassword = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcWebServicePassword"),
                        sfmcEnterpriseAccountName = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcEnterpriseAccountName"),
                        webServiceCloseTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceCloseTimeoutFromMinutes"),
                        webServiceSendTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceSendTimeoutFromMinutes"),
                        webServiceOpenTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceOpenTimeoutFromMinutes"),
                        webServiceReceiveTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceReceiveTimeoutFromMinutes")
                    };
                }
                else
                {
                    soapWebServiceConnectionPropertiesLookup.Add(sfmcEnterpriseAccountId, new SoapWebServiceConnectionProperties
                    {
                        sfmcEnterpriseAccountId = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcEnterpriseAccountId"),
                        sfmcWebServiceEndpointUrl = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcWebServiceEndpointUrl"),
                        sfmcWebServiceUsername = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcWebServiceUsername"),
                        sfmcWebServicePassword = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcWebServicePassword"),
                        sfmcEnterpriseAccountName = (string)sfmcEnterpriseAccountDetails.SelectToken("SfmcEnterpriseAccountName"),
                        webServiceCloseTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceCloseTimeoutFromMinutes"),
                        webServiceSendTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceSendTimeoutFromMinutes"),
                        webServiceOpenTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceOpenTimeoutFromMinutes"),
                        webServiceReceiveTimeoutFromMinutes = (int)sfmcEnterpriseAccountDetails.SelectToken("WebServiceReceiveTimeoutFromMinutes")
                    });
                }
            }
        }

        public class SoapWebServiceConnectionProperties
        {
            public string sfmcEnterpriseAccountId { get; set; }
            public string sfmcWebServiceEndpointUrl { get; set; }
            public string sfmcWebServiceUsername { get; set; }
            public string sfmcWebServicePassword { get; set; }
            public string sfmcEnterpriseAccountName { get; set; }
            public int webServiceCloseTimeoutFromMinutes { get; set; }
            public int webServiceSendTimeoutFromMinutes { get; set; }
            public int webServiceOpenTimeoutFromMinutes { get; set; }
            public int webServiceReceiveTimeoutFromMinutes { get; set; }
        }

        public static async Task<string> GetBuilds(string vsoRestApiUri, string personalAccessToken, string mediaType)
        {
            var responseBody = string.Empty;
            try
            {
                var authToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", personalAccessToken)));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                    using (HttpResponseMessage response = client.GetAsync(vsoRestApiUri).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return responseBody;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            File.WriteAllText(@"D:\_files\app_offline.htm", @"D:\Usr\Ashok\Projects\AngularJsWebTemplate\TamilYogiWeb\app_offline.htm");
            Program p = new Program();
            var choice = args[0];

            Regex rgxMatchesImportNotificationEmailAddressElement = new Regex(@"<ImportNotificationAddress>(.*)</ImportNotificationAddress>");

            var fileTriggerResponseControlManifest = @"<Tasks>
            <Task>
                <ImportActivity>
                    <OtherElement>AnyText</OtherElement>
                    <ImportNotificationAddress>dummy@exacttarget.com</ImportNotificationAddress>
                </ImportActivity>
            </Task>
            <Task>
                <ImportActivity>
                    <OtherElement>AnyText</OtherElement>
                    <ImportNotificationAddress>email@exacttarget.com</ImportNotificationAddress>
                </ImportActivity>
            </Task>
            <Task>
                <ImportActivity>
                    <OtherElement>AnyText</OtherElement>
                </ImportActivity>
            </Task>
        </Tasks>";
            var fileTriggerResponseControlManifest2 = rgxMatchesImportNotificationEmailAddressElement.Replace(fileTriggerResponseControlManifest, string.Empty);
            fileTriggerResponseControlManifest2 = fileTriggerResponseControlManifest2.Replace("</ImportActivity>", @"   <ImportNotificationAddress>email@exacttarget.com</ImportNotificationAddress>
                </ImportActivity>");
            Console.WriteLine(fileTriggerResponseControlManifest2);


            //Program.UploadFromFileShareToSftp(@"D:\_files\d0b0288a-8de5-470a-a727-7d725f990e9c.tsv.zip.aes", @"/Import/FileTriggers/RussianSuppression/LP_NL/");

            Program.VsoReleaseManagerPieceOfAutomationPoc();

            string[] noStringsAttached = new[] { "one", "two" };

            var xmlText = p.Serialize001(p);
            try
            {
                switch (choice.ToUpper())
                {
                    case "JSON_SELECTORS":
                        {
                            #region JSON_SELECTORS
                            var xDoc001 = new XmlDocument();
                            xDoc001.LoadXml("<contact type='employee'>not available</contact>");

                            var inputXsl = JsonConvert.SerializeXmlNode(xDoc001);

                            var json01 = "{\"a\": {\"b\": \"<html class=\\\"a\\\"></html>\"}}";
                            var j0 = JObject.Parse(json01);
                            var j1 = (string)j0.SelectToken("a.b");
                            Console.WriteLine(j1);
                            #endregion

                            break;
                        }
                    case "JOLT_XSL":
                        {
                            #region JOLT_XSL
                            var xDoc001 = new XmlDocument();
                            var xDoc002 = new XmlDocument();
                            xDoc001.LoadXml(File.ReadAllText(@"D:\Usr\Ashok\Documents\Visual Studio 2013\Projects\BackupAzureQueue\BackupAzureQueue\XSLTFile2.xslt"));
                            xDoc002.LoadXml(File.ReadAllText(@"D:\Usr\Ashok\Documents\Visual Studio 2013\Projects\BackupAzureQueue\BackupAzureQueue\XMLFile1.xml"));

                            var inputXml = JsonConvert.SerializeXmlNode(xDoc002);
                            var inputXsl = JsonConvert.SerializeXmlNode(xDoc001);
                            var counter2 = 0;
                            Stopwatch sw2 = new Stopwatch();
                            sw2.Start();
                            while (true)
                            {
                                counter2 += 1;
                                Jolt.TransformJsonbyJstl(inputXml, inputXsl);
                                if (counter2 % 50000 == 0)
                                    Console.WriteLine("Counter " + counter2 + " | " + sw2.ElapsedMilliseconds);
                            }
                            sw2.Stop();
                            Console.WriteLine(sw2.ElapsedMilliseconds);
                            #endregion

                            break;
                        }
                    case "JOLT":
                        {
                            #region JOLT
                            var _jsonInputFiles = Directory.GetFiles(@"D:\Usr\Swapnajeet", "In*", SearchOption.TopDirectoryOnly);
                            foreach (var _jsonInputFile in _jsonInputFiles)
                            {
                                var counter1 = 0;
                                Stopwatch sw1 = new Stopwatch();
                                sw1.Start();
                                var jsonInput = "{\"defaultroot\" : " + File.ReadAllText(_jsonInputFile) + "}";
                                var jsonStylesheet = File.ReadAllText(_jsonInputFile.Replace("InFunctionTest", @"Local\MapFunctionTest"));
                                while (counter1 < 100000)
                                {
                                    counter1 += 1;
                                    Jolt.TransformJsonbyJstl(jsonInput, jsonStylesheet);
                                    if (counter1 % 50000 == 0)
                                        Console.WriteLine("Counter " + counter1 + " | " + sw1.ElapsedMilliseconds);
                                }
                            }
                            
                            #endregion

                            break;
                        }

                    case "JSON_JOBJECT_001":
                        {
                            #region JSON_JOBJECT_001
                            var jo = JObject.Parse("{\"TriggeredRequest\": {\"Type\": \"Generic\"}}");
                            jo.Add("Prop", DateTime.UtcNow.ToString());

                            var enterpriseAccountDetails = "[{	\"SfmcEnterpriseAccountId\" : \"10460681\",	\"SfmcWebServiceEndpointUrl\" : \"https://webservice-microsoft.exacttarget.com/service.asmx\",	\"SfmcWebServiceUsername\" : \"API_10460681\",	\"SfmcWebServicePassword\" : \"welcome@1\",	\"SfmcEnterpriseAccountName\" : \"Microsoft Gmo Onboarding\",	\"WebServiceCloseTimeoutFromMinutes\" : \"1\",	\"WebServiceSendTimeoutFromMinutes\" : \"1\",	\"WebServiceOpenTimeoutFromMinutes\" : \"1\",	\"WebServiceReceiveTimeoutFromMinutes\" : \"1\"},{	\"SfmcEnterpriseAccountId\" : \"96540\",	\"SfmcWebServiceEndpointUrl\" : \"https://webservice-microsoft.exacttarget.com/service.asmx\",	\"SfmcWebServiceUsername\" : \"Api_96540\",	\"SfmcWebServicePassword\" : \"welcome@1\",	\"SfmcEnterpriseAccountName\" : \"Gmo v1\",	\"WebServiceCloseTimeoutFromMinutes\" : \"1\",	\"WebServiceSendTimeoutFromMinutes\" : \"1\",	\"WebServiceOpenTimeoutFromMinutes\" : \"1\",	\"WebServiceReceiveTimeoutFromMinutes\" : \"1\"}]";

                            var jObject = JArray.Parse("[{\"PseudoCollection\" : " + enterpriseAccountDetails + "}]")[0];

                            var collections = jObject.SelectToken("PeudoCollection");
                            foreach (var item in collections)
                            {
                                Console.WriteLine(item);
                            }

                            var red = jObject.SelectToken("PseudoCollection[0]").ToString();

                            Console.WriteLine(red);
                            #endregion

                            break;
                        }

                    case "JSON_JOBJECT_002":
                        {
                            #region JSON_JOBJECT_002
                            var testJson = "{\"CellTrackingCode\" : \"5\",	\"CellName\" : \"ActivityName5\",	\"CellOwner\" : \"ActivityOwner5\",	\"CellDescription\" : \"ExecutionTeam5\"}";
                            var straightJson = "{\"ohh000\": \"nice\",\"ohh000\": \"wah\"}";
                            var arrayJson = "[{\"ohh000\": \"nice\", \"ohh001\": \"perfect\"}, {\"ohh000\": \"wow\"}]";

                            var jObject = JArray.Parse("[{\"pseudo\" : " + testJson + "}]")[0];
                            var collections = jObject.SelectToken("pseud");
                            foreach (var item in collections)
                            {
                                Console.WriteLine();
                            }

                            var inputJsonText = "{\"Attributes\" : [{\"Key\" : \"Firstname\",\"Value\" : \"Firstname_1\"}, {\"Key\" : \"Key_2\",\"Value\" : \"Value_2\"}]}";
                            jObject = JObject.Parse(inputJsonText);
                            var jsonSchemaTextFilePath = @"D:\Usr\Ashok\SampleJsonSchema.json";
                            var jsonSchemaText = File.ReadAllText(jsonSchemaTextFilePath);
                            var jsonSchema = JsonSchema.Parse(jsonSchemaText);

                            IList<string> jsonValidationErrorMessages;
                            var isValid = jObject.IsValid(jsonSchema, out jsonValidationErrorMessages);
                            Console.WriteLine(jsonValidationErrorMessages);
                            #endregion

                            break;
                        }

                    case "JTOKEN_DEEPEQUALS":
                        {
                            #region JTOKEN_DEEPEQUALS
                            var jo1 = JObject.Parse("{\"TriggeredRequest\": {\"Name\": \"Something\", \"Type\": \"Generic\"}}");
                            var jo2 = JObject.Parse("{\"TriggeredRequest\": {\"Type\": \"Generic\", \"Name\": \"Something\"}}");
                            var jt1 = jo1.SelectToken("TriggeredRequest");
                            var jt2 = jo2.SelectToken("TriggeredRequest");
                            var eq = JObject.DeepEquals(jo1, jo2);
                            Console.WriteLine(eq);
                            #endregion

                            break;
                        }

                    case "PEEKQUEUE":
                        {
                            #region MOVEQUEUE

                            string queueConnectionString = args[1];
                            string sourceQueueName = args[2];
                            string targetQueueName = args[3];
                            int maxMessages = Convert.ToInt32(args[4]);
                            int invisibleTimeoutInMinutes = Convert.ToInt32(args[5]);

                            CloudQueue sourceQueue = null;
                            CloudQueue targetQueue = null;

                            var isSourceQueueConnected = p.CreateConnection(queueConnectionString, sourceQueueName, false, out sourceQueue);
                            Console.WriteLine("Source queue connection " + (isSourceQueueConnected ? "established" : "not established!!"));

                            var isTargetQueueConnected = p.CreateConnection(queueConnectionString, targetQueueName, true, out targetQueue);
                            Console.WriteLine("Target queue connection " + (isTargetQueueConnected ? "established" : "not established!!"));

                            int totalPassedItems = 0;
                            int totalFailedItems = 0;
                            var sourceQueueExists = sourceQueue.Exists();
                            var targetQueueExists = targetQueue.Exists();
                            if (sourceQueue != null && sourceQueueExists && targetQueue != null && targetQueueExists && isSourceQueueConnected && isTargetQueueConnected)
                                p.PeekMessages(sourceQueue, targetQueue, maxMessages, TimeSpan.FromMinutes(invisibleTimeoutInMinutes), out totalPassedItems, out totalFailedItems);
                            else
                                Console.WriteLine(sourceQueueExists ? "Source queue object is NULL!!" : "Source queue, '" + sourceQueueName + "' does not exists");

                            Console.WriteLine("# of messages copied: " + totalPassedItems + " # of messages failed: " + totalPassedItems);
                            #endregion

                            break;
                        }

                    #region Ignore
                    case "IGNORE_LATER_REF":
                        {
#if IGNORE_LATER_REF
                            SqlCommand sc = new SqlCommand();
                            var connection = new SqlConnection();
                            connection.ConnectionString = "Server=tcp:w4k2434z3i.database.windows.net;Database=InterchangeDB1;User ID=bluradar@w4k2434z3i;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False";
                            connection.Open();

                            sc.Connection = connection;
                            sc.CommandType = System.Data.CommandType.StoredProcedure;
                            sc.CommandText = "CreateRequest";

                            SqlParameter RequestID = new SqlParameter("@RequestID", Guid.Parse("35851a5f-fd1c-45bb-8efc-153aff693560"));
                            RequestID.DbType = System.Data.DbType.Guid;
                            SqlParameter objectParam = new SqlParameter("@Object", "<root/>");
                            objectParam.DbType = System.Data.DbType.Xml;
                            SqlParameter RequestType = new SqlParameter("@RequestType", "requestType");
                            RequestType.DbType = System.Data.DbType.String;
                            SqlParameter TenantName = new SqlParameter("@TenantName", "tenantName");
                            RequestType.DbType = System.Data.DbType.String;
                            SqlParameter RequestExecutionPriority = new SqlParameter("@RequestExecutionPriority", "1");
                            RequestType.DbType = System.Data.DbType.String;
                            string dataCenter = string.Empty;

                            SqlParameter DataCenterName = new SqlParameter("@DataCenterName", dataCenter);
                            RequestType.DbType = System.Data.DbType.String;
                            sc.Parameters.Add(RequestID);
                            sc.Parameters.Add(objectParam);
                            sc.Parameters.Add(RequestType);
                            sc.Parameters.Add(TenantName);
                            sc.Parameters.Add(RequestExecutionPriority);
                            sc.Parameters.Add(DataCenterName);
                            int numberOfRecords = sc.ExecuteNonQuery();

                            Console.WriteLine(numberOfRecords);

                            dynamic messageTemplate = new
                            {
                                Header = new
                                {
                                    MessageId = Guid.NewGuid(),
                                    MessageCorrelationId = Guid.NewGuid(),
                                    ServiceName = "eivnextallocadia",
                                    SourceSystemName = "Allocadia",
                                    SourceSystemReferenceId = "DhrTst069",
                                    EventCaptureTimeStamp = DateTime.Now.ToString(),
                                    MessageBornTimeStamp = DateTime.Now.ToString(),
                                    SchemaName = "MIPEventMessage",
                                    PublisherBridgeName = "PublishToPlaningActivity",
                                    OriginSystemName = "Allocadia",
                                    OriginSystemReferenceId = "DhrTst069",
                                    OriginatingEventName = "Allocadia",
                                    TargetSystemName = "SFMC"

                                },
                                MessageBody = JsonConvert.DeserializeObject("{\"ohh\": \"nice\"}")
                            };

                            dynamic json = new
                            {
                                TriggeredRequest = new
                                {
                                    Type = "GenericSubscriber",
                                    EnterpriseAccountId = "39327",
                                    SubsidiaryAccountId = "39327",
                                    TriggeredSendDefinitionExternalKey = "TBN_333",
                                    Subscribers = new[] 
                    {
                        new
                        {
                            EmailAddress = "sample001@smail.com"
                        },
                        new
                        {
                            EmailAddress = "sample002@smail.com"
                        }
                    }
                                }
                            };

                            XmlDocument xDoc = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(json));

                            //var transformedXml = p.XslTransformerV3(@"", xDoc.OuterXml);
                            var xDocument = new XmlDocument();
                            xDocument.LoadXml(@"
<TriggeredRequest>
   <Type>Generic</Type>
   <EnterpriseAccountId>96540</EnterpriseAccountId>
   <SubsidiaryAccountId>113015suporteti@multiar.com.br</SubsidiaryAccountId>
   <TriggeredSendDefinitionExternalKey>113015</TriggeredSendDefinitionExternalKey>
   <Subscribers>
      <EmailAddress>suporteti@multiar.com.br</EmailAddress>
      <SubscriberKey>suporteti@multiar.com.br</SubscriberKey>
      <Attributes>
         <Key>LCID</Key>
         <Value>1033</Value>
      </Attributes>
      <Attributes>
         <Key>SampleKey001</Key>
         <Value>SampleValue001</Value>
      </Attributes>
      <Attributes>
         <Key>SampleKey002</Key>
         <Value>SampleValue003</Value>
      </Attributes>
   </Subscribers>
   <Subscribers>
      <EmailAddress>suporteti@multiar.com</EmailAddress>
      <SubscriberKey>suporteti@multiar.com</SubscriberKey>
      <Attributes>
         <Key>LCID</Key>
         <Value>1033</Value>
      </Attributes>
      <Attributes>
         <Key>CommunicationId</Key>
         <Value>1033</Value>
      </Attributes>
      <Attributes>
         <Key>SampleKey001</Key>
         <Value>SampleValue001</Value>
      </Attributes>
      <Attributes>
         <Key>SampleKey002</Key>
         <Value>SampleValue003</Value>
      </Attributes>
   </Subscribers>
</TriggeredRequest>

");
                            Console.WriteLine(JsonConvert.SerializeXmlNode(xDocument));

                            //Console.WriteLine(JsonConvert.SerializeObject(json));


                            //var client = WebRequest.Create("http://localhost:16950/api/values/5") as HttpWebRequest;
                            //var clientCertificate = new X509Certificate2(File.ReadAllBytes(@"D:\Usr\Amit\LatestCertificates\PFX\ei.microsoft.com.pfx"), "Microsoft~1");
                            //client.ClientCertificates.Add(clientCertificate);
                            //string response = new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();

                            //while (true)
                            //{
                            //    Example();

                            //    var typedText = Console.ReadLine();
                            //    Console.WriteLine("Type " + typedText);
                            //}

                            //JObject jObject = JObject.Parse("{'People':[{'Age':'Jeff'},{'Name':'Joe'}]}");

                            //// get name token of first person and convert to a string
                            //var jObjectSelectTokens = jObject.SelectToken("People").Select(t => (JToken)t).ToList();

                            //foreach (var jObjectSelectToken in jObjectSelectTokens)
                            //{
                            //    //foreach (var jObjectSelectTokenItem in jObjectSelectTokens.ElementAt(0).ToList())
                            //    //{
                            //    Console.WriteLine(jObjectSelectToken.SelectToken("Name"));
                            //    //}
                            //}

                            /*byte[] data1 = Convert.FromBase64String("eyJlbmRwb2ludFVybCI6Imh0dHBzOi8vd2Vic2VydmljZS1taWNyb3NvZnQuZXhhY3R0YXJnZXQuY29tL3NlcnZpY2UuYXNteCIsIndlYlNlcnZpY2VVc2VyTmFtZSI6IkFQSV8xMDQ2MDY4MSIsIndlYlNlcnZpY2VQYXNzd29yZCI6IndlbGNvbWVAMSIsImVtYWlsQWRkcmVzc2VzIjpbeyJlbWFpbEFkZHJlc3MiOiJ2LWtyc2FybUBtaWNyb3NvZnQuY29tIiwic3Vic2NyaWJlcktleSI6IiJ9LHsiZW1haWxBZGRyZXNzIjoibGFycnlqQG1pY3Jvc29mdC5jb20iLCJzdWJzY3JpYmVyS2V5IjoiIn0seyJlbWFpbEFkZHJlc3MiOiJ2LWtyc2FybUBtaWNyb3NvZnQuY29tIiwic3Vic2NyaWJlcktleSI6IiJ9XX0=");
                            string decodedString = Encoding.UTF8.GetString(data1);

                            Console.WriteLine(decodedString);
            
                            var higherPriorityEngines = new Dictionary<object, object> { { "PollingInterval", "30" } };
                            Console.WriteLine(DateTime.UtcNow.Subtract(DateTime.Parse("2016-09-29 14:20:05.267")).TotalSeconds < 5 * 30);

                            Inherit002 currentMsg = new Inherit002 
                            { 
                                prop01 = string.Empty,
                                prop02 = string.Empty,
                                prop03 = string.Empty,
                                prop06 = "prop06",
                                prop07 = "prop07",
                            };

                            if(!(currentMsg is Inherit001))
                                Console.WriteLine("where");

                            var inherit002Obj = currentMsg as BaseQueueMsg;
                            Console.WriteLine(((Inherit002)inherit002Obj).prop06);*/

                            #region IGNORE
                            /*Console.WriteLine(string.Format("{0}.{1}", null, "gz"));

            var connection = new SqlConnection();
            var command = new SqlCommand();

            try
            {
                connection.ConnectionString = "Server=tcp:w4k2434z3i.database.windows.net;Database=InterchangeDB1;User ID=bluradar@w4k2434z3i;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False";
                connection.Open();

                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "CreateRequest";

                var request = @"<Request xmlns='http://schemas.datacontract.org/2004/07/Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core' xmlns:i='http://www.w3.org/2001/XMLSchema-instance'>
  <AccountId i:nil='true' />
  <ComponentStates xmlns:a='http://schemas.microsoft.com/2003/10/Serialization/Arrays' i:nil='true' />
  <CompressedFileSize>0</CompressedFileSize>
  <CompressionTime>0</CompressionTime>
  <CurrentPullDateTime>0001-01-01T00:00:00</CurrentPullDateTime>
  <CurrentRetryAttempt>0</CurrentRetryAttempt>
  <Data i:nil='true' />
  <DataEndpoints i:nil='true' />
  <DecompressionTime>0</DecompressionTime>
  <DecryptionTime>0</DecryptionTime>
  <DelayBeforeRetry>0</DelayBeforeRetry>
  <DestinationConnectionString i:nil='true' />
  <DestinationConnectionType>None</DestinationConnectionType>
  <DestinationName>None</DestinationName>
  <Direction>None</Direction>
  <EmailInterchangeRequestId>99be502a-5710-479a-988c-0bc2afb23b91</EmailInterchangeRequestId>
  <EncryptedFileSize>0</EncryptedFileSize>
  <EncryptionTime>0</EncryptionTime>
  <EndDateTime>2015-11-25T11:20:26.2316148Z</EndDateTime>
  <EnterpriseAccountId>39327</EnterpriseAccountId>
  <ExactTargetRequestId>99be502a-5710-479a-988c-0bc2afb23b91</ExactTargetRequestId>
  <HasBusinessLogic>false</HasBusinessLogic>
  <IsCompressionEnabled>false</IsCompressionEnabled>
  <IsDecompressionEnabled>false</IsDecompressionEnabled>
  <IsDecryptionEnabled>false</IsDecryptionEnabled>
  <IsEncryptionEnabled>false</IsEncryptionEnabled>
  <LastPullDateTime>0001-01-01T00:00:00</LastPullDateTime>
  <MaxRetryAttempts>0</MaxRetryAttempts>
  <NotificationEmailAddress i:nil='true' />
  <OperationStatus>ExternalReceived</OperationStatus>
  <ProcessStatus>RequestAssemble</ProcessStatus>
  <ProcessingFileName>CustomerRefreshData_01202010.aes</ProcessingFileName>
  <ProcessingFileNameSpec i:nil='true' />
  <RequestError>None</RequestError>
  <RequestParameter>&lt;?xml version='1.0' encoding='utf-16'?&gt;
&lt;Request&gt;
	&lt;Type&gt;ReportExtractData&lt;/Type&gt;
	&lt;Parameters&gt;
		&lt;Parameter&gt;
			&lt;Name&gt;AccountID&lt;/Name&gt;
			&lt;Value&gt;39327&lt;/Value&gt;
		&lt;/Parameter&gt;
		&lt;Parameter&gt;
			&lt;Name&gt;FilePath&lt;/Name&gt;
			&lt;Value&gt;\Export&lt;/Value&gt;
		&lt;/Parameter&gt;
		&lt;Parameter&gt;
			&lt;Name&gt;FileName&lt;/Name&gt;
			&lt;Value&gt;CustomerRefreshData_01202010.aes&lt;/Value&gt;
		&lt;/Parameter&gt;
	&lt;/Parameters&gt;
&lt;/Request&gt;
</RequestParameter>
  <RequestScheduleId>0</RequestScheduleId>
  <RequestType>ReportExtractData</RequestType>
  <ScheduledDate>0001-01-01T00:00:00</ScheduledDate>
  <ScheduledDateSpecified>false</ScheduledDateSpecified>
  <SourceConnectionString i:nil='true' />
  <SourceConnectionType>None</SourceConnectionType>
  <SourceFileName i:nil='true' />
  <SourceName>None</SourceName>
  <StartDateTime>2015-11-25T11:20:26.2316148Z</StartDateTime>
  <TransactionType>None</TransactionType>
</Request>";

                SqlParameter RequestID = new SqlParameter("@RequestID", Guid.Parse("99be502a-5710-479a-988c-0bc2afb23b91"));
                RequestID.DbType = System.Data.DbType.Guid;
                SqlParameter objectParam = new SqlParameter("@Object", request);
                objectParam.DbType = System.Data.DbType.Xml;
                SqlParameter RequestType = new SqlParameter("@RequestType", "ReportExtractData");
                RequestType.DbType = System.Data.DbType.String;
                SqlParameter TenantName = new SqlParameter("@TenantName", "EI");
                RequestType.DbType = System.Data.DbType.String;
                SqlParameter RequestExecutionPriority = new SqlParameter("@RequestExecutionPriority", 1);
                RequestType.DbType = System.Data.DbType.String;
                string dataCenter = string.Empty;
                dataCenter = "DEV_FABRIC";

                SqlParameter DataCenterName = new SqlParameter("@DataCenterName", dataCenter);
                RequestType.DbType = System.Data.DbType.String;
                command.Parameters.Add(RequestID);
                command.Parameters.Add(objectParam);
                command.Parameters.Add(RequestType);
                command.Parameters.Add(TenantName);
                command.Parameters.Add(RequestExecutionPriority);
                command.Parameters.Add(DataCenterName);
                int numberOfRecords = command.ExecuteNonQuery();
                Console.WriteLine(numberOfRecords == 0);
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }*/

                            /*SyndicationFeed sf = new SyndicationFeed();
                            SyndicationItem item1 = new SyndicationItem(
                    title: "Item One",
                    content: "This is the content for item one",
                    itemAlternateLink: new Uri("http://localhost/Content/One"),
                    id: "ItemOneID",
                    lastUpdatedTime: DateTime.Now);

                            object obje = new FileRequestMessageClassification
                            {
                                Context = null,
                                Id = new Guid(),
                                isFallbackProcessing = false,
                                numberOfAttemptsForCreatingBulksendDefinition = 3,
                                RequestType = null,
                                Tenant = "Ashok",
                                TenantId = 2,
                                UserId = 3
                            };

                            var vari = p.Serialize(obje);*/
                            
                            #endregion
#endif

                            break;
                        }
                    case "MOVEQUEUE":
                        {
                            #region MOVEQUEUE

                            string queueConnectionString = args[1];
                            string sourceQueueName = args[2];
                            string targetQueueName = args[3];
                            int maxMessages = Convert.ToInt32(args[4]);
                            int invisibleTimeoutInMinutes = Convert.ToInt32(args[5]);

                            CloudQueue sourceQueue = null;
                            CloudQueue targetQueue = null;

                            var isSourceQueueConnected = p.CreateConnection(queueConnectionString, sourceQueueName, false, out sourceQueue);
                            Console.WriteLine("Source queue connection " + (isSourceQueueConnected ? "established" : "not established!!"));

                            var isTargetQueueConnected = p.CreateConnection(queueConnectionString, targetQueueName, true, out targetQueue);
                            Console.WriteLine("Target queue connection " + (isTargetQueueConnected ? "established" : "not established!!"));

                            int totalPassedItems = 0;
                            int totalFailedItems = 0;
                            var sourceQueueExists = sourceQueue.Exists();
                            var targetQueueExists = targetQueue.Exists();
                            if (sourceQueue != null && sourceQueueExists && targetQueue != null && targetQueueExists && isSourceQueueConnected && isTargetQueueConnected)
                                p.MoveMessages(sourceQueue, targetQueue, maxMessages, TimeSpan.FromMinutes(invisibleTimeoutInMinutes), out totalPassedItems, out totalFailedItems);
                            else
                                Console.WriteLine(sourceQueueExists ? "Source queue object is NULL!!" : "Source queue, '" + sourceQueueName + "' does not exists");

                            Console.WriteLine("# of messages copied: " + totalPassedItems + " # of messages failed: " + totalPassedItems);
                            #endregion

                            break;
                        }

                    case "BACKUPBLOB":
                        {
                            #region BACKUPBLOB

                            string queueConnectionString = args[1];
                            string sourceQueueName = args[2];
                            string targetQueueName = args[3];
                            int maxMessages = Convert.ToInt32(args[4]);
                            int invisibleTimeoutInMinutes = Convert.ToInt32(args[5]);

                            CloudQueue sourceQueue = null;
                            CloudQueue targetQueue = null;

                            var isSourceQueueConnected = p.CreateConnection(queueConnectionString, sourceQueueName, false, out sourceQueue);
                            Console.WriteLine("Source queue connection " + (isSourceQueueConnected ? "established" : "not established!!"));

                            var isTargetQueueConnected = p.CreateConnection(queueConnectionString, targetQueueName, true, out targetQueue);
                            Console.WriteLine("Target queue connection " + (isTargetQueueConnected ? "established" : "not established!!"));

                            int totalPassedItems = 0;
                            int totalFailedItems = 0;
                            var sourceQueueExists = sourceQueue.Exists();
                            var targetQueueExists = targetQueue.Exists();
                            if (sourceQueue != null && sourceQueueExists && targetQueue != null && targetQueueExists && isSourceQueueConnected && isTargetQueueConnected)
                                p.MoveMessages(sourceQueue, targetQueue, maxMessages, TimeSpan.FromMinutes(invisibleTimeoutInMinutes), out totalPassedItems, out totalFailedItems);
                            else
                                Console.WriteLine(sourceQueueExists ? "Source queue object is NULL!!" : "Source queue, '" + sourceQueueName + "' does not exists");

                            Console.WriteLine("# of messages copied: " + totalPassedItems + " # of messages failed: " + totalPassedItems);
                            #endregion

                            break;
                        }

                    case "MONITORQUEUE":
                        {
                            #region MONITORQUEUE
                            CloudQueue queue = null;
                            string queueConnectionString = args[1];
                            string queueName = args[2];
                            int sleepInMinutes = Convert.ToInt32(args[3]);

                            var isQueueConnected = p.CreateConnection(queueConnectionString, queueName, false, out queue);
                            Console.WriteLine("Queue connection " + (isQueueConnected ? "established" : "not established!!"));

                            if (isQueueConnected)
                                p.MonitorQueue(queue, sleepInMinutes);
                            #endregion

                            break;
                        }

                    case "PoCQUEUE":
                        {
                            #region PoCQUEUE
                            CloudQueue queue = null;
                            string queueConnectionString = args[1];
                            string queueName = args[2];

                            var isQueueConnected = p.CreateConnection(queueConnectionString, queueName, false, out queue);
                            Console.WriteLine("Queue connection " + (isQueueConnected ? "established" : "not established!!"));

                            if (isQueueConnected)
                                p.DeleteQueueMessages(queue);

                            /*if (isQueueConnected)
                                p.Enqueue(queue);

                            p.Dequeue(queue);

                            Thread.Sleep(70 * 1000);

                            p.Dequeue(queue);*/
                            #endregion

                            break;
                        }

                    case "UPDATEMSGINQUEUE":
                        {
                            #region UPDATEMSGINQUEUE
                            CloudQueue queue = null;
                            string queueConnectionString = args[1];
                            string queueName = args[2];

                            var isQueueConnected = p.CreateConnection(queueConnectionString, queueName, false, out queue);
                            Console.WriteLine("Queue connection " + (isQueueConnected ? "established" : "not established!!"));

                            while (true)
                            {
                                if (isQueueConnected)
                                    p.EnqueueMessage(queue, 1, "Ashok");

                                var qMessages = p.Dequeue(queue);
                                foreach (var qMessage in qMessages)
                                {
                                    p.UpdateQueueMessage(queue, qMessage);
                                }
                                Thread.Sleep(30 * 1000);
                            }
                            #endregion

                            break;
                        }

                    case "MONITORBLOB":
                        {
                            #region MONITORBLOB
                            string storageAcConnectionString = args[1];
                            string blobContainerName = args[2];
                            int sleepInMinutes = Convert.ToInt32(args[3]);
                            double expBackoffTime = 5;
                            int maxRetryAttempts = 3;
                            int maxExecutionTime = 30;
                            int parallelOperationThreadCount = 20;
                            int buffersize = 1024 * 1024;

                            var cloudblobcontainer = p.GetCloudBlobContainer(storageAcConnectionString, blobContainerName, expBackoffTime, maxRetryAttempts, maxExecutionTime, parallelOperationThreadCount, buffersize);

                            if (cloudblobcontainer != null)
                                p.MonitorBlob(cloudblobcontainer, sleepInMinutes);
                            #endregion

                            break;
                        }

                    case "UPLOADBLOB":
                        {
                            #region UPLOADBLOB
                            string storageAcConnectionString = args[1];
                            string blobContainerName = args[2];
                            int sleepInMinutes = Convert.ToInt32(args[3]);
                            string blobName = args[4];
                            string sftpServer = args[5];
                            string sftpUser = args[6];
                            string sftpPassword = args[7];
                            string licenseKey = args[8];
                            string ftpDirectory = args[9];

                            double expBackoffTime = 5;
                            int maxRetryAttempts = 3;
                            int maxExecutionTime = 30;
                            int parallelOperationThreadCount = 20;
                            int buffersize = 1024 * 1024;

                            //var cloudblobcontainer = p.GetCloudBlobContainer(storageAcConnectionString, blobContainerName, expBackoffTime, maxRetryAttempts, maxExecutionTime, parallelOperationThreadCount, buffersize);
                            var cloudblobcontainer = p.GetCloudBlobContainer(storageAcConnectionString, blobContainerName);

                            if (cloudblobcontainer != null)
                            {
                                var transSuccess = p.TransferBlobToSFTP(cloudblobcontainer, blobName, sftpServer, sftpUser, sftpPassword, licenseKey, ftpDirectory);
                            }
                            #endregion

                            break;
                        }

                    case "GETDC":
                        {
                            #region GETDC
                            RestApiUtils util = new RestApiUtils();

                            var location = util.GetDataCenterLocation(ConfigurationManager.AppSettings.Get("deploymentId"));
                            Console.WriteLine(location);
                            #endregion

                            break;
                        }

                    case "DECRYPT":
                        {
                            #region DECRYPT
                            string encryptedText = args[1];
                            string certThumbprint = args[2];
                            string decryptedText = p.Decrypt(encryptedText, certThumbprint);

                            Console.WriteLine(decryptedText);
                            #endregion

                            break;
                        }

                    case "ENCRYPTNEWSECRET":
                        {
                            #region ENCRYPTNEWSECRET
                            string driverFile = args[1];

                            if (!File.Exists(driverFile))
                                Console.WriteLine("File does not exists. '" + driverFile + "'");

                            using (var sr = new StreamReader(driverFile))
                            {
                                var lineNumber = 0;
                                while (!sr.EndOfStream)
                                {
                                    lineNumber += 1;
                                    string[] driverValues = sr.ReadLine().Split(new[] { "^" }, StringSplitOptions.None);

                                    var oldEncryptedText = string.Empty;
                                    var oldCertThumbprint = string.Empty;
                                    var newCertThumbprint = string.Empty;
                                    var decryptedText = string.Empty;
                                    var newEncryptedText = string.Empty;
                                    var exMessage = string.Empty;

                                    try
                                    {
                                        oldEncryptedText = driverValues[0];
                                        oldCertThumbprint = driverValues[1];
                                        newCertThumbprint = driverValues[2];
                                        try
                                        {
                                            decryptedText = p.Decrypt(oldEncryptedText, oldCertThumbprint);
                                        }
                                        catch
                                        {
                                            decryptedText = p.Decrypt(oldEncryptedText, newCertThumbprint);
                                        }
                                        newEncryptedText = p.Encrypt(decryptedText, newCertThumbprint);
                                    }
                                    catch (Exception ex)
                                    {
                                        exMessage = ex.Message.Replace("\r", " ").Replace("\n", " ");
                                    }

                                    Console.WriteLine(lineNumber + "^" + oldEncryptedText + "^" + decryptedText + "^" + newEncryptedText + "^" + exMessage);
                                }
                            }
                            #endregion

                            break;
                        }

                    case "OBJCACHE":
                        {
                            #region OBJCACHE
                            ObjectCache cache = MemoryCache.Default;
                            DataRow userAttributes = null;
                            if (cache.Contains("EnterpriseAccountData"))
                            {
                                DataSet ds = new DataSet();
                                ds = (DataSet)cache.Get("EnterpriseAccountData");

                                if (ds.Tables.Count > 0)
                                {
                                    userAttributes = (from r in ds.Tables[0].AsEnumerable()
                                                      where r.Field<Int32>("AccountId") == 39327
                                                      select r).FirstOrDefault();
                                    Console.WriteLine(p.Decrypt(userAttributes["SFTPUserName"].ToString(), "1B647BE314FBC0E8ECCD885C3442A9E63C7EB5B5"));
                                    Console.WriteLine(p.Decrypt(userAttributes["SFTPPassword"].ToString(), "1B647BE314FBC0E8ECCD885C3442A9E63C7EB5B5"));
                                }
                            }
                            Console.WriteLine("Completed");
                            #endregion

                            break;
                        }

                    case "TBNDATASERIALIZATION":
                        {
                            #region TBNDATASERIALIZATION
                            ///*/RequestPacket/_x003C_Request_x003E_k__BackingField[@i:type='TriggeredRequest']/Subscribers[0]/SubscriberBase[@i:type='TagmTriggerSubscriber']*/
                            //var xmlFilePath = args[1];
                            //XmlDocument xDoc = new XmlDocument();
                            //xDoc.Load(xmlFilePath);

                            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                            //nsmgr.AddNamespace("ctrlns", "http://schemas.datacontract.org/2004/07/Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core");
                            //nsmgr.AddNamespace("ctrlns_i", "http://www.w3.org/2001/XMLSchema-instance");

                            //var tbnXmlNode = xDoc.SelectSingleNode("//ctrlns:_x003C_Request_x003E_k__BackingField[@ctrlns_i:type='TriggeredRequest']", nsmgr);
                            //var tbnXmlContent = tbnXmlNode != null ? tbnXmlNode.OuterXml.Replace("_x003C_Request_x003E_k__BackingField", "TriggeredRequest") : string.Empty;
                            //var tbnXmlNodeType = tbnXmlNode.Attributes[0].Value;

                            //var obj = p.DataContractDeserialize<TriggeredRequest>(tbnXmlContent);

                            ///*var tbnXmlNode = xDoc.SelectSingleNode("//ctrlns:SubscriberBase[@ctrlns_i:type='TagmTriggerSubscriber']", nsmgr);
                            //var tbnXmlContent = tbnXmlNode != null ? tbnXmlNode.OuterXml : string.Empty;
                            //var tbnXmlNodeType = tbnXmlNode.Attributes[0].Value;

                            //var typeDictionary = new Dictionary<string, object>
                            //{
                            //    {"EventSubscriber", typeof(EventSubscriber)}
                            //    ,{"GenericSubscriber", typeof(GenericSubscriber)}
                            //    ,{"PartnerTriggerSubscriber", typeof(PartnerTriggerSubscriber)}
                            //    ,{"LimitedProgramSubscriber", typeof(LimitedProgramSubscriber)}
                            //    ,{"TagmTriggerSubscriber", typeof(TagmTriggerSubscriber)}
                            //};

                            //var obj = p.DataContractDeserialize<TriggeredRequest>(tbnXmlContent);*/

                            //Console.WriteLine(obj.ToString());

                            #endregion

                            break;
                        }

                    case "HTMLDIFF":
                        {
                            #region HTMLDIFF
                            /*HtmlDiff htmlDiff = new HtmlDiff("Hey! Hi, How are you?", "Ha Ha :) How are you?");
                            Console.WriteLine(htmlDiff.Build());*/
                            #endregion

                            break;
                        }

                    case "ENCRYPTFILE":
                        {
                            #region ENCRYPTFILE
                            string sourceFile = args[1];
                            string destFile = args[2];
                            string sharedKey = args[3];
                            p.EncryptFile(sourceFile, destFile, sharedKey);
                            Console.WriteLine("Completed");
                            #endregion

                            break;
                        }

                    case "DECRYPTFILE":
                        {
                            #region DECRYPTFILE
                            string sourceFile = args[1];
                            string destFile = args[2];
                            string sharedKey = args[3];
                            p.DecryptFile(sourceFile, destFile, sharedKey);
                            Console.WriteLine("Completed");
                            #endregion

                            break;
                        }

                    case "SHA2":
                        {
                            #region SHA2
                            string fileName = args[1];

                            var stopwatch1 = new Stopwatch();
                            stopwatch1.Start();
                            string str1 = GetChecksum(fileName);
                            stopwatch1.Stop();

                            /*
                            var stopwatch2 = new Stopwatch();
                            stopwatch2.Start();
                            var fileStream = new FileStream(fileName, FileMode.OpenOrCreate,
                                FileAccess.Read);
                            string str2 = GetChecksumBuffered(fileStream);
                            stopwatch2.Stop();
                             */

                            Console.WriteLine(str1 + " " + stopwatch1.ElapsedMilliseconds);
                            //Console.WriteLine(str2 + " " + stopwatch2.ElapsedMilliseconds);

                            #endregion

                            break;
                        }

                    case "CHDATE":
                        {
                            #region CHDATE
                            File.SetCreationTime(@"D:\Usr\Ashok\CustomCloudInterchangePackage\Temp\CloudInterchange_20150108_Modified\InterchangeAPI_1ff9fd68-93c9-41ef-abd3-73a52111ab2d\approot\bin\DisableSslv3.cmd", File.GetCreationTime(@"D:\Usr\Ashok\CustomCloudInterchangePackage\Temp\CloudInterchange_20150108_Modified\InterchangeAPI_1ff9fd68-93c9-41ef-abd3-73a52111ab2d\approot\bin\Startup.cmd"));
                            File.SetLastWriteTime(@"D:\Usr\Ashok\CustomCloudInterchangePackage\Temp\CloudInterchange_20150108_Modified\InterchangeAPI_1ff9fd68-93c9-41ef-abd3-73a52111ab2d\approot\bin\DisableSslv3.cmd", File.GetLastWriteTime(@"D:\Usr\Ashok\CustomCloudInterchangePackage\Temp\CloudInterchange_20150108_Modified\InterchangeAPI_1ff9fd68-93c9-41ef-abd3-73a52111ab2d\approot\bin\Startup.cmd"));
                            File.SetLastAccessTime(@"D:\Usr\Ashok\CustomCloudInterchangePackage\Temp\CloudInterchange_20150108_Modified\InterchangeAPI_1ff9fd68-93c9-41ef-abd3-73a52111ab2d\approot\bin\DisableSslv3.cmd", File.GetLastAccessTime(@"D:\Usr\Ashok\CustomCloudInterchangePackage\Temp\CloudInterchange_20150108_Modified\InterchangeAPI_1ff9fd68-93c9-41ef-abd3-73a52111ab2d\approot\bin\Startup.cmd"));
                            #endregion

                            break;
                        }

                    case "CRFOLDER":
                        {
                            #region CRFOLDER
                            var userPreferences = new Dictionary<string, FileSystemRights>
                                {
                                    {@"fareast\v-larami", FileSystemRights.Read|FileSystemRights.Write},
                                    {@"fareast\v-assiva", FileSystemRights.FullControl},
                                    {"everyone", FileSystemRights.Read}
                                };

                            p.CreateFolderWithNeccessaryPermissions(@"D:\usr\v-alias\restricted", userPreferences, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
                            #endregion

                            Console.WriteLine("Completed");

                            break;
                        }

                    case "REGEX":
                        {
                            #region REGEX
                            Regex regex = new Regex(@"320011|320010|320012|320011");
                                //(@"_(\d{14})_(\d+)");
                            var match = regex.Match(@"Email Record Type: EmailPreference.Exception :
The record that was requested to be updated could not be found.  No action was performed and  no retries will be attempted.

Here is the information that was returned by MDM:
ResultCode: 320010
ResultDescription: Record/s not found. EmailAddress.
ExceptionAttribute: EmailAddress
        . InnerExceptionMessage: ");
                                //("MMOSUBUNSUB_19022015152742_3.tsv");
                            if (match != null && match.Groups.Count.Equals(3))
                            {
                                Console.WriteLine(match.Groups[2].Value);
                            }
                            #endregion

                            break;
                        }

                    case "GETMSITENANTS":
                        {
                            #region GETMSITENANTS
                            var dtTable = p.GetMsiTenants("Server=tcp:w4k2434z3i.database.windows.net;Database=InterchangeDB1;User ID=bluradar@w4k2434z3i;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Encrypt=True");
                            Console.WriteLine(dtTable.Rows.Count);
                            #endregion

                            break;
                        }

                    case "EPOCH":
                        {
                            #region EPOCH
                            Console.WriteLine(29102014021326 < 17112014152642 ? "true" : "false");
                            #endregion

                            break;
                        }

                    case "DOWNLOADFILE":
                        {
                            #region DOWNLOADFILE
                            p.DownloadFile(args[1], args[2], args[3], args[4], args[5], args[6]);
                            p.DecryptFile(args[3].TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + args[1], args[3].TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + args[1].Replace(".aes", string.Empty), "EIisPENSreplacement!");
                            Console.WriteLine("Completed");
                            #endregion

                            break;
                        }

                    case "UPLOADFILE":
                        {
                            #region UPLOADFILE
                            p.EncryptFile(args[1], args[1] + ".aes", "EIisPENSreplacement!");
                            p.UploadFile(args[1] + ".aes", args[2], args[3], args[4], args[5]);
                            Console.WriteLine("Completed");
                            #endregion

                            break;
                        }

                    case "FOREACH":
                        {
                            #region FOREACH
                            PrincipalContext ctx = new PrincipalContext(ContextType.Domain,
                                            "fareast.corp.microsoft.com",
                                            "DC=fabrikam,DC=com",
                                            "fareast\v-assiva",
                                            "");

                            GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx,
                                                                               IdentityType.Name,
                                                                               "Domain Admins");

                            if (grp != null)
                            {
                                foreach (Principal pa in grp.GetMembers(true))
                                {
                                    Console.WriteLine(pa.Name);
                                }
                                grp.Dispose();
                            }

                            ctx.Dispose();
                            #endregion

                            break;
                        }

                    case "THREAD_POOL":
                        {
                            #region THREAD_POOL

                            // One event is used for each Fibonacci object
                            ManualResetEvent[] doneEvents = new ManualResetEvent[64];

                            //Queuing the work items to thread pool..
                            var programs = new Program[64];

                            var isSet = ThreadPool.SetMaxThreads(1, 1);
                            Console.WriteLine("isSet - " + isSet);

                            int workerThreads;
                            int completionPortThreads;
                            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
                            Console.WriteLine(" GetMinThread " + workerThreads + " " + completionPortThreads);
                            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
                            Console.WriteLine(" GetMaxThread " + workerThreads + " " + completionPortThreads);

                            for (int x = 0; x < 64; x++)
                            {
                                doneEvents[x] = new ManualResetEvent(false);
                                programs[x] = new Program(doneEvents[x]);
                                ThreadPool.QueueUserWorkItem(new WaitCallback(programs[x].DoSomething), x);
                            }

                            //Waiting for all threads to complete..
                            ManualResetEvent.WaitAll(doneEvents);

                            Console.WriteLine("All threads are complete");
                            #endregion

                            break;
                        }

                    case "PARALLEL_FOREACH":
                        {
                            #region PARALLEL_FOREACH

                            //Queuing the work items to thread pool..
                            List<Program> programs = new List<Program>();
                            var prgrm = new Program();

                            for (int idx = 1; idx <= 3; idx += 1)
                            {
                                prgrm = new Program();
                                prgrm.number = idx;
                                programs.Add(prgrm);
                            }

                            ParallelOptions parallelOptions = new ParallelOptions()
                            {
                                MaxDegreeOfParallelism = 2
                            };

                            var _lock = new object();
                            var _number = 0;
                            Parallel.ForEach(programs, parallelOptions, program =>
                            {
                                try
                                {
                                    program.Do(program.number);
                                    lock (_lock)
                                    {
                                        Console.Write("Thread: " + Thread.CurrentThread.ManagedThreadId + " Number: " + program._list.Count + " Sleeping: " + (program.number * 10) + " seconds" + Environment.NewLine);
                                        Thread.Sleep(program.number * 10 * 1000);
                                        Console.Write("Thread: " + Thread.CurrentThread.ManagedThreadId + " Number: " + program._list.Count + " Awaken after: " + (program.number * 10) + " seconds" + Environment.NewLine);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Exception: " + ex.Message.Replace(@"\r", " ").Replace(@"\n", " "));
                                }
                                programs.Add(program);
                                //Console.WriteLine("Added work item - " + program.number);
                            });


                            Console.WriteLine("All threads are complete" + programs.Count);
                            #endregion

                            break;
                        }

                    case "SEMAPHORE":
                        {
                            #region SEMAPHORE
                            // Create a semaphore that can satisfy up to three 
                            // concurrent requests. Use an initial count of zero, 
                            // so that the entire semaphore count is initially 
                            // owned by the main program thread. 
                            //
                            _pool = new Semaphore(0, 3);

                            // Create and start five numbered threads.  
                            // 
                            for (int i = 1; i <= 5; i++)
                            {
                                Thread t = new Thread(new ParameterizedThreadStart(Worker));

                                // Start the thread, passing the number. 
                                //
                                t.Start(i);
                            }

                            // Wait for half a second, to allow all the 
                            // threads to start and to block on the semaphore. 
                            //
                            Thread.Sleep(500);

                            // The main thread starts out holding the entire 
                            // semaphore count. Calling Release(3) brings the  
                            // semaphore count back to its maximum value, and 
                            // allows the waiting threads to enter the semaphore, 
                            // up to three at a time. 
                            //
                            Console.WriteLine("Main thread calls Release(3).");
                            _pool.Release(3);

                            Console.WriteLine("Main thread exits.");
                            #endregion

                            break;
                        }

                    case "CREATEDATATABLE":
                        {
                            #region CREATEDATATABLE
                            p.CreateDatatable(args[1]);
                            #endregion

                            break;
                        }

                    case "TRYCATCHFINALLY":
                        {
                            #region TRYCATCHFINALLY
                            p.Trycatchfinally();
                            #endregion

                            break;
                        }

                    case "FILESYSTEMRIGHTS":
                        {
                            #region FILESYSTEMRIGHTS

                            /*string names = "ashok    ,bharathiraja   ,siva,,,,,,,";
                            var splitNames = names.Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var name in splitNames)
                            {
                                Console.WriteLine("'" + name + "'");
                            }*/

                            var Items = new Dictionary<string, object>();
                            var fsRights = Enum.GetNames(typeof(FileSystemRights)).ToArray();
                            foreach (string fsRight in fsRights)
                            {
                                try
                                {
                                    Items.Add(Convert.ToString(fsRight), Convert.ToString(fsRight));
                                    Console.WriteLine(Convert.ToString(fsRight));
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(fsRight + " -  " + ex.Message);
                                }
                            }

                            FileSystemRights fsRight1 = FileSystemRights.Read;

                            foreach (var str in "ReadData, Write".Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                fsRight1 = fsRight1 | (FileSystemRights)Enum.Parse(typeof(FileSystemRights), str);
                                Console.WriteLine(Convert.ToString(fsRight1));
                            }

                            #endregion

                            break;
                        }

                    case "MSIFILEPROCESSOR":
                        {
                            #region MSIFILEPROCESSOR
                            /*string[] tokens = "YAMMERSUBUNSUB_20150326124553_3.tsv.aes".Split('_');
                            Console.WriteLine(Int64.Parse(tokens[1]));*/

                            Regex regex = new Regex(@"_(\d{14})");
                            var match = regex.Match("YAMMERSUBUNSUB_20150326124553.tsv.aes");
                            if (match != null && match.Groups.Count >= 2)
                                Console.WriteLine(Convert.ToInt64(match.Groups[1].Value));
                            else
                                throw new Exception("Filename does not follow our expected naming conventions. We support following naming conventions, '<TenantLabel>_<yyyyMMddHHmmss>.tsv.aes', '<TenantLabel>_<EpochTimestamp>_<NoOfRecords>.tsv.aes'");
                            #endregion

                            break;
                        }

                    case "SENDMAIL":
                        {
                            #region SENDMAIL
                            Console.WriteLine(p.SendEmail("Jane Miller <v-assiva@microsoft.com>; Ashok Miller <ashokan_s@infosys.com>; Ashok Miller <ashokansivapragasam@gmail.com>", "Microsoft Retail Html Photo Booth", @"<html><head>
<meta http-equiv='Content-Type' content='text/html; charset=us-ascii'><style>
			body { font-family: Segoe UI; font-size: 0.8em}
			table { font-family: Segoe UI; border: solid 2px teal; border-collapse: collapse; font-size: 0.83em}
			th { border: 1px solid black; text-align: left}
			td { border: 1px solid black; }
			table tr.header { background: #02add3; color: #FEFAFE; align: left}
		</style>
	</head>
	<body>
		Hi,
		<br><br>
		This is a sample test email for MS Photo Booth Application. 
		<br><br>
		<img src='http://dummyimage.com/600x400/000/fff.jpg&amp;text=Microsoft&#43;Retail'><!-- </img> -->
		<br><br>
		Thanks,<br>
		EI &amp; Microsoft Retail Services Team
<br>
<table cellpadding='2' cellspacing='0' width='600' id='Table5' border='0'><tr><td><font face='verdana' size='1' color='#444444'>This email was sent to:  v-assiva@microsoft.com <br><br><b>Email Sent By:</b> Microsoft Onboarding<br>One Microsoft Way Redmond, WA, 98052, USA<br><br></font></td></tr></table>
<br>
<img src='http://cl.exct.net/open.aspx?ffcb10-fe891776706602757d-fe271d7477670079701d70-fe6515707162047b7d15-feb91c787c600474-fdf31571746c0d797111727c-ffcd16&amp;d=10050' width='1' height='1'>
	</body>
</html>
"));
                            #endregion

                            break;
                        }

                    case "SENDMAILV2":
                        {
                            #region SENDMAILV2
                            Console.WriteLine(p.SendEmail("Jane Miller <v-assiva@microsoft.com>; Ashok Miller <ashokan_s@infosys.com>; Ashok Miller <ashokansivapragasam@gmail.com>", "Microsoft Retail Html Photo Booth", File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_016.html")));
                            #endregion

                            break;
                        }

                    case "VALIDEXTN":
                        {
                            #region VALIDEXTN
                            Console.WriteLine(Path.GetExtension("YMRSUBUNSUB_20150306163314.tsv.aes").ToLower() == Path.GetExtension(".aes").ToLower());

                            Regex regex = new Regex(@"_(\d{14})");
                            var match = regex.Match("YMRSUBUNSUB_20150306163314.tsv.aes");
                            Console.WriteLine(((match != null && match.Captures.Count >= Convert.ToInt32("2")) ? true : false) + " " + match.Captures.Count);

                            #endregion

                            break;
                        }

                    case "GETSQLMIXEDREPORT":
                        {
                            #region GETSQLMIXEDREPORT
                            //var mixedReports = p.GetSqlMixedReport(@"Server=tcp:w4k2434z3i.database.windows.net;Database=InterchangeDB1;User ID=bluradar@w4k2434z3i;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False");
                            //Console.WriteLine(mixedReports);
                            #endregion

                            break;
                        }

                    case "EXCEPTIONTYPING":
                        {
                            #region EXCEPTIONTYPING
                            try
                            {
                                p.InternalMethod();
                            }
                            /*catch (SshAuthenticationException sshAuthenticationException)
                            {
                                Console.WriteLine("SshAuthenticationException");
                            }
                            catch (SshTaskTimeoutException sshTaskTimeoutException)
                            {
                                Console.WriteLine("SshTaskTimeoutException");
                            }
                            catch (SshException sshException)
                            {
                                Console.WriteLine("SshException");
                            }*/
                            catch (Exception exception)
                            {
                                if (exception is SshException)
                                    Console.WriteLine("SshException");
                                if (exception is Exception)
                                    Console.WriteLine("Exception");
                            }
                            #endregion

                            break;
                        }

                    case "RENAMEFILE":
                        {
                            #region RENAMEFILE
                            p.RenameFile("ftp.test.exacttarget.com", "39327", "7hUjecRA", "");
                            #endregion

                            break;
                        }

                    case "COMMID_ATTRIBUTE":
                        {
                            #region COMMID_ATTRIBUTE

                            var mappings = new Dictionary<string, string>
                            {
                                {"HideFromProfileCenter","/Communication/HideFromProfileCenter"},
                                {"CommunicationTitle","/Communication/CommunicationTitle"},
                                {"CommunicationDescription","/Communication/CommunicationDescription"},
                                {"CountryCode","/Communication/CountryCode"},
                                {"LCID","/Communication/LCID"}
                            };

                            var communication = new Communication
                            {
                                HideFromProfileCenter = "0",
                                CommunicationTitle = "EiDevTeam",
                                CommunicationDescription = "This is test communication",
                                CountryCode = "en-US",
                                LCID = "1033"
                            };

                            var cSharpSerializedXmlDocument = p.Serialize<Communication>(communication);

                            string rawXmlDocument = @"<communicationattribs>
	<attribute attributeid='7' attributevalue='{HideFromProfileCenter}' />
	<attribute attributeid='8' attributevalue='{CommunicationTitle}' />
	<attribute attributeid='9' attributevalue='{CommunicationDescription}' />
	<attribute attributeid='11' attributevalue='{CountryCode}' />
	<attribute attributeid='12' attributevalue='{LCID}' />
    <attribute attributeid='20' attributevalue='{CountryCode}' />
</communicationattribs>";

                            Console.WriteLine(p.CsharpObjectToXmlDOcument(rawXmlDocument, cSharpSerializedXmlDocument, mappings));
                            #endregion

                            break;
                        }

                    case "IFTHENSEMANTICS":
                        {
                            #region IFTHENSEMANTICS
                            p.IfThenSemantics();
                            #endregion

                            break;
                        }

                    case "XSLTRANSFORMER":
                        {
                            #region XSLTRANSFORMER
                            p.XslTransformer(@"D:\Usr\Ashok\Dwight_Transform_003.xsl", @"D:\Usr\Ashok\GregVerveris.xml", @"D:\Usr\Ashok\GregVerveris_out.xml");
                            #endregion

                            break;
                        }

                    case "XSLTRANSFORMERV2":
                        {
                            #region XSLTRANSFORMERV2
                            var result = p.XslTransformerV2(@"D:\OneItVso\Email Interchange\Application\Main\Automation\AutomatedPulseSvc\DriverFiles\ApplicationDriverSettings.xsl"
                                , @"D:\OneItVso\Email Interchange\Application\Main\Automation\AutomatedPulseSvc\DriverFiles\ApplicationDriverSettings.xml");

                            Console.WriteLine(result);
                            #endregion

                            break;
                        }

                    case "IMAP4SERVER":
                        {
                            #region IMAP4SERVER
                            p.Imap4Server();
                            #endregion

                            break;
                        }

                    case "EXCHANGESERVER":
                        {
                            #region EXACT_TARGET
                            var textEmailBody = p.GetTextEmailBodyByEmailSubjectFromExchangeServer(
                                                "eimip001"
                                                , "Microsoft~1"
                                                , "https://outlook.com/EWS/Exchange.asmx"
                                                , "eimip001@outlook.com"
                                                , "Inbox"
                                                , "5bbbf1380ebb4eba804fd62e9e0f5fce");

                            var dictionary = p.ParseValuesFromRawTextEmailBody(
                                                @"D:\Usr\Ashok\Analysis\DevThoughts_ExactTargetDataDriver.xml"
                                                , textEmailBody);

                            p.PushValuesFromTextEmailBodyToSqlServer(
                                @"Data Source=localhost;Initial Catalog=StackOverflow;Integrated Security=True"
                                , @"D:\Usr\Ashok\Analysis\DevThoughts_ExactTargetDataDriver.xml"
                                , dictionary);
                            #endregion

                            #region GENESIS

                            var data = p.GetSqlXmlReport(@"Data Source=localhost;Initial Catalog=StackOverflow;Integrated Security=True", "GetSqlXmlReport");

                            dictionary = p.ParseValuesFromXmlDocument(
                                                @"D:\Usr\Ashok\Analysis\DevThoughts_GenesisDataDriver.xml"
                                                , data.Rows[0][0].ToString());

                            p.PushValuesFromTextEmailBodyToSqlServer(
                                @"Data Source=localhost;Initial Catalog=StackOverflow;Integrated Security=True"
                                , @"D:\Usr\Ashok\Analysis\DevThoughts_GenesisDataDriver.xml"
                                , dictionary);
                            #endregion

                            break;
                        }

                    case "PACKETDATA":
                        {
                            #region PACKETDATA
                            p.PacketData();
                            #endregion

                            break;
                        }

                    case "XSDVALIDATE":
                        {
                            #region XSDVALIDATE
                            XmlSchemaSet schemas = new XmlSchemaSet();
                            schemas.Add("", @"D:\SMCbase\Tools\CommunicationReportSynchronizer\CommunicationReportSynchronizer.Host\DriverFiles\SubscriptionManagementDriverFile.xsd");
                            XDocument doc = XDocument.Load(@"D:\SMCbase\Tools\CommunicationReportSynchronizer\CommunicationReportSynchronizer.Host\DriverFiles\SubscriptionManagementDriverFile.xml");
                            string msg = "";
                            doc.Validate(schemas, (o, e) =>
                            {
                                msg += e.Message + Environment.NewLine;
                            });
                            Console.WriteLine(msg == "" ? "Document is valid" : "Document invalid: " + msg);
                            #endregion
                            break;
                        }

                    case "IMPORTDATAFROMCSVTODATATABLE":
                        {
                            #region IMPORTDATAFROMCSVTODATATABLE
                            p.GetDictionaryFromDelimitedFile(@"D:\Usr\Ashok\Analysis\ExactTargetSampleSummaryFile_20150522.tsv", "\t");
                            p.ImportDataFromCsvToDatatable();
                            #endregion

                            break;
                        }

                    case "APPINSIGHTSCONFIGREADER":
                        {
                            #region APPINSIGHTSCONFIGREADER
                            var appInsightsConfigReader = AppInsightsConfigReader.LoadConfigurationFile(@"D:\Usr\Ashok\Documents\Visual Studio 2012\Projects\BackupAzureQueue\BackupAzureQueue\ApplicationInsights.config");
                            Console.WriteLine(appInsightsConfigReader);
                            #endregion

                            break;
                        }

                    case "UPDATEAPPINSIGHTSCONFIGFILEBYSERVICECONFIGVALUES":
                        {
                            #region UPDATEAPPINSIGHTSCONFIGFILEBYSERVICECONFIGVALUES
                            p.UpdateAppInsightsConfigFileByServiceConfigValues
                                (@"D:\Usr\Ashok\ApplicationInsights.config"
                                , "http://schemas.microsoft.com/ApplicationInsights/2013/Settings"
                                , "{Microsoft.AppInsights.AccountId}{/ns:ApplicationInsights/ns:Profiles/ns:Profile[@name='Production']/ns:ComponentSettings/ns:AccountID},{Microsoft.AppInsights.Instrumentationkey}{/ns:ApplicationInsights/ns:Profiles/ns:Profile[@name='Production']/ns:ComponentSettings/ns:LicenseKey},{Microsoft.AppInsights.ApplicationInsightsID}{/ns:ApplicationInsights/ns:Profiles/ns:Profile[@name='Production']/ns:ComponentSettings/ns:ComponentID},{Microsoft.AppInsights.DisplayName}{/ns:ApplicationInsights/ns:Profiles/ns:Profile[@name='Production']/ns:ComponentSettings/ns:ComponentName}"
                                , @"\{([\w\.]+)\}\{([\/\w\[\]\=\@\'\:]+)\}");
                            #endregion

                            break;
                        }

                    case "RESEARCHONLIST":
                        {
                            #region RESEARCHONLIST

                            for (int idx = 0; idx < 1000000; idx += 1)
                            {
                                p.ResearchOnList(@"C:\temp\prasanna\2015_05_29_11_00_06_4027.tsv", idx);
                            }
                            #endregion

                            break;
                        }

                    case "GENERATEBULKSENDINPUTFILE":
                        {
                            #region GENERATEBULKSENDINPUTFILE
                            p.GenerateBulksendInputFile();
                            #endregion

                            break;
                        }

                    case "CREATEPLFILE":
                        {
                            #region CREATEPLFILE
                            //p.CreatePlFile(args[1], Convert.ToInt32(args[2]));
                            p.CreateDynamicBulksendFile(args[1], Convert.ToInt32(args[2]));
                            #endregion

                            break;
                        }

                    case "DOWNLOADBLOB":
                        {
                            #region DOWNLOADBLOB
                            //int counter = 0;
                            //while (counter < 10)
                            //{
                            //    var interchange = new InterchangeConnect();
                            //    var filename = @"D:\Usr\Ashok\Test\Set1\Tenant1SUBUNSUB_20150306163260_50000.tsv";
                            //    var storageAcConnectionString = "DefaultEndpointsProtocol=https;AccountName=interchangeapistoraged7;AccountKey=jYX1Ho1YFQbGiDqFJj4YfjrE4ubjFyL1DJn52dTQ0ChshfjSjJ6VQew97hOSGdiU/jXO4eii5/bntIgrp9tovg==";
                            //    var blobContainerName = "fileuploadstore";
                            //    var cloudblobcontainer = p.GetCloudBlobContainer(storageAcConnectionString, blobContainerName);

                            //    //99BC12AE8C5A23F8CED89E83B9E2F174
                            //    var expectedMd5CheckSum = Program.GetChecksum(filename);
                            //    var StartTime = DateTime.Now;
                            //    interchange.FileUpload(filename);
                            //    //p.UploadFileToBlob(cloudblobcontainer, filename);
                            //    var EndTime = DateTime.Now;
                            //    Console.WriteLine(StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "\t" + EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "\t" + (EndTime - StartTime).TotalSeconds);

                            //    /*
                            //    var actualMd5Checksum = string.Empty;
                            //    if (cloudblobcontainer != null)
                            //        p.TransferBlobToLocalFileSystem(cloudblobcontainer, Path.GetFileName(filename), filename + ".bak");

                            //    actualMd5Checksum = Program.GetChecksum(filename + ".bak");

                            //    Console.WriteLine("File checksum is " + (actualMd5Checksum.Equals(expectedMd5CheckSum) ? "pairing" : "not pairing"));
                            //     */

                            //    counter += 1;
                            //}
                            #endregion

                            break;
                        }

                    case "CONVERTIMAGEASBASE64STRING":
                        {
                            #region CONVERTIMAGEASBASE64STRING
                            var base64String = p.ConvertImageAsBase64String(@"D:\_files\PhotoShoot.jpg");
                            Console.WriteLine(base64String.Length);

                            Console.WriteLine(CompressString(base64String).Length);
                            #endregion

                            break;
                        }

                    case "WATCHFILES":
                        {
                            #region WATCHFILES
                            var watchDirectory = args[1];

                            //while (true)
                            {
                                var files = Directory.EnumerateFiles(watchDirectory);
                                if (files.Count() > 0)
                                    Console.WriteLine("File# " + files.Count());

                                Thread.Sleep(60 * 1000);
                            }
                            #endregion

                            break;
                        }

                    case "SQLCONNECTIONPOC":
                        {
                            #region SQLCONNECTIONPOC
                            //var sqlConnectionString = "Data Source=AZEIDEVSQL01;Initial Catalog=AzureArchiveDb;;Integrated Security=SSPI;Connection Timeout=2;";
                            var sqlConnectionString = "Server=tcp:ac5b2hxv7z.database.windows.net;Database=SubManDB;User ID=SubManAdmin@ac5b2hxv7z;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=4;Pooling=true;Max Pool Size=30";
                            //Pooling=true;Max Pool Size=30";
                            //while (true)
                            {
                                p.SqlConnectionPoc(sqlConnectionString);
                            }
                            #endregion

                            break;
                        }
#endregion

                    case "SQLASYNCPROCESSINGPOC":
                        {
                            #region SQLASYNCPROCESSINGPOC
                            //var sqlConnectionString = "Server=tcp:ac5b2hxv7z.database.windows.net;Database=SubManDB;User ID=SubManAdmin@ac5b2hxv7z;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=4;Pooling=true;Max Pool Size=30;Asynchronous Processing=true";
                            var sqlConnectionString = "Server=tcp:ac5b2hxv7z.database.windows.net;Database=SubManDB;User ID=SubManAdmin@ac5b2hxv7z;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=4;Asynchronous Processing=true";
                            int counter = 0;
                            while (counter < 2)
                            {
                                p.SqlAsyncProcessingPoc(sqlConnectionString, "WAITFOR DELAY '00:00:05'; select top 10 'SomeText' as SomeColumn, * from TransactionalLog");
                                counter += 1;
                            }
                            #endregion

                            break;
                        }

                    case "PREPAREAPPDRIVERSETTINGSMODEL":
                        {
                            #region PREPAREAPPDRIVERSETTINGSMODEL

                            p.PrepareAppDriverSettingsModel();

                            #endregion

                            break;
                        }

                    case "ADDKEYTOREDISCACHE":
                        {
                            #region ADDKEYTOREDISCACHE
                            p.AddKeyToRedisCache();
                            #endregion

                            break;
                        }

                    case "ADDMESSAGESTOTOPICSUBSCRIPTION":
                        {
                            #region ADDMESSAGESTOTOPICSUBSCRIPTION
                            p.AddMessagesToTopicSubscription(
                                //"Endpoint=sb://tbnrequestsservicebusdit.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rIgCrPwbVXGCjUE9Pr/D0bx3m3/3mat4xPFmiOMH9Ww="
                                //"Endpoint=sb://tbnrequestsservicebusdit.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tRQgDxlH7jI9oyvUDiV83a0mnNpGBbWXgzOPt8hAqss="
                                "Endpoint=sb://allocadiamipdev1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yAFMCqAC7Zf+xNklSaIU+H0Hj8LOB31lpGg2j1DL41U="
                                , "planningactivity");
                                //("Endpoint=sb://allocadiamip.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CIK4GrpMnNAWGpoU27WCMgTtjRn87rvTDbpE8a7liRg=", "planningactivity");
                            #endregion

                            break;
                        }

                    case "SENDBULKTBNREQUESTSTOWEBAPI":
                        {
                            #region SENDBULKTBNREQUESTSTOWEBAPI
                            var access_token = p.GetAccessToken("https://login.microsoftonline.com/microsoft.onmicrosoft.com/oauth2/token"
                                , "client_credentials"
                                , "6a40900d-2fc4-4aa6-a2df-e0e0ffc323c9"
                                , "nON3OYQRa6tfuVBNBnFYrX8zLDpymgrwd+pWm6Qou+I="
                                , "https://login.microsoftonline.com/microsoft.onmicrosoft.com/eivnextrestwebapippe");

                            p.SendBulkTbnRequestsToWebApi("http://eivnextrestwebapippe.azurewebsites.net/"
                                , "api/TriggeredSendNotification"
                                , "application/json"
                                , access_token);
                            #endregion

                            break;
                        }

                    case "INSTALLCERTIFICATE":
                        {
                            #region INSTALLCERTIFICATE
                            Program.InstallCertificate(@"D:\Usr\Amit\LatestCertificates\CER\smdevwebsvcauth.cer");
                            #endregion

                            break;
                        }

                    case "GETACCESSTOKEN":
                        {
                            #region GETACCESSTOKEN
                            var access_token = p.GetAccessToken("https://login.microsoftonline.com/microsoft.onmicrosoft.com/oauth2/token"
                                , "client_credentials"
                                , "6a40900d-2fc4-4aa6-a2df-e0e0ffc323c9"
                                , "nON3OYQRa6tfuVBNBnFYrX8zLDpymgrwd+pWm6Qou+I="
                                , "https://login.microsoftonline.com/microsoft.onmicrosoft.com/eivnextrestwebapippe");
                            Console.WriteLine(access_token);
                            #endregion

                            break;
                        }

                    case "VERIFYEMAIL":
                        {
                            #region /* 7. Verify email */
                            FolderId folderId = null;
                            ItemView itemview = null;
                            var textEmailBody = string.Empty;
                            
                            //var msExchangeService = ConnectToMsExchangeServer("v-assiva", "g00.g2g00.g2", "fareast", "v-assiva@microsoft.com", "Reports/Tbn", out folderId, out itemview);
                            var msExchangeService = ConnectToMsExchangeServer("eippsql", "1bA&(A)HLukpD6hf", "redmond", "eippsql@microsoft.com", "Inbox", out folderId, out itemview);

                            var searchQueryString = string.Empty;
                            var searchText = "6192e403d788416aafffdec119b9f763";
                            for (int idx = 0; searchText.Length > 0 && string.IsNullOrWhiteSpace(textEmailBody); idx += 1)
                            {
                                searchText = "6192e403d788416aafffdec119b9f763".Substring(0, "6192e403d788416aafffdec119b9f763".Length - idx);
                                searchQueryString = "body:(" + searchText + ")";
                                textEmailBody = GetTextEmailBodyByEmailSubjectFromExchangeServer(msExchangeService, folderId, itemview, searchQueryString);

                                Console.WriteLine("Run: " + idx + " SearchQueryString: " + searchQueryString + "TextEmailBodyLength: " + textEmailBody.Length);
                            }
                            #endregion

                            break;
                        }

                    case "ENCNDEC":
                        {
                            #region ENCnDEC
                            var enc = Base64Encode("g00.g2g00.g2");
                            Console.WriteLine(enc);
                            var dec = Base64Decode(enc);
                            Console.WriteLine(dec);
                            #endregion

                            break;
                        }

                    case "BROWSEBLOBCONTAINER":
                        {
                            #region BROWSEBLOBCONTAINER
                            var blobContainerConnectionString = "DefaultEndpointsProtocol=https;AccountName=interchangeapistorage;AccountKey=dqBBaXfZdd6Ddjk9sEyQ9JD9TukyfuJn1FaryIok66yGVJUfnYZ0Kn7NahBsSqHU6CcMfCfyxy7lBRdM9rI/Zg==";
                            var blobContainerName = "fileprocessingstore";
                            p.BrowseBlobContainer(blobContainerConnectionString, blobContainerName);
                            #endregion

                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
                Console.WriteLine("I have reached!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void VsoReleaseManagerPieceOfAutomationPoc()
        {
            var allPolicyTypes = await Program.GetBuilds(vsoRestApiUri: "https://microsoftit.visualstudio.com/DefaultCollection/OneITVSO/_git/policy/types?api-version=2.0-preview"
                , personalAccessToken: "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq"
                , mediaType: "application/json");
//#if dev
            #region ALL_RELEASE_DEFINITIONS
            var allReleaseDefinitionsResponse = await Program.GetBuilds(vsoRestApiUri: "https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions?$expand=environments&api-version=3.0-preview.1"
                , personalAccessToken: "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq"
                , mediaType: "application/json");

            var releaseDefns = new Dictionary<int, string>();

            var jObject = JObject.Parse(allReleaseDefinitionsResponse);
            foreach (var item in jObject["value"])
                releaseDefns.Add(Convert.ToInt32(item["id"]), Convert.ToString(item["name"]));
            #endregion

            #region RELEASE_DEFINITION
            var releaseDefinitionResponse = await Program.GetBuilds(vsoRestApiUri: "https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions/2239?$expand=environments&api-version=3.0-preview.1"
                , personalAccessToken: "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq"
                , mediaType: "application/json");
            #endregion

            #region REVISIONS
            var revisionsResponse = await Program.GetBuilds(vsoRestApiUri: "https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions/2239/revisions?api-version=3.0-preview.1"
                , personalAccessToken: "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq"
                , mediaType: "application/json");
            #endregion

            #region RELEASES
            var releasesResponse = await Program.GetBuilds(vsoRestApiUri: "https://microsoftit.vsrm.visualstudio.com/defaultcollection/OneITVSO/_apis/release/releases?api-version=3.0-preview.1&definitionId=2239&$expand=environments"
                , personalAccessToken: "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq"
                , mediaType: "application/json");
            #endregion
//#endif

            var releaseVariables = new Dictionary<string, DataTable>();
            var _dataTable = new DataTable();
            jObject = JObject.Parse(releaseDefinitionResponse);
            var _environmentName = string.Empty;
            foreach (var _environment in jObject["environments"])
            {
                _environmentName = _environment["name"].ToString();
                _dataTable = new DataTable();
                _dataTable.Columns.Add("VariableName", typeof(string));
                _dataTable.Columns.Add("VariableValue", typeof(string));
                foreach (var _variable in _environment["variables"])
                {
                    _dataTable.Rows.Add(((JProperty)_variable).Name, ((JProperty)_variable).Value["value"].ToString());
                    //releaseVariables.Add(new ReleaseEnvironment { environmentName = _environmentName, variableName = ((JProperty)_variable).Name, variableValue = ((JProperty)_variable).Value["value"].ToString() });
                }
                releaseVariables.Add(_environmentName, _dataTable);
            }

#if dev
            #region RELEASES
            var releaseResponse = await Program.GetBuilds(vsoRestApiUri: "https://microsoftit.vsrm.visualstudio.com/defaultcollection/OneITVSO/_apis/release/releases/185377?api-version=3.0-preview.1&definitionId=2239&$expand=environments"
                , personalAccessToken: "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq"
                , mediaType: "application/json");

            var releaseVariables = new Dictionary<string, DataTable>();
            var _dataTable = new DataTable();
            jObject = JObject.Parse(releaseResponse);
            var _environmentName = string.Empty;
            foreach (var _environment in jObject["environments"])
            {
                _environmentName = _environment["name"].ToString();
                _dataTable = new DataTable();
                _dataTable.Columns.Add("VariableName", typeof(string));
                _dataTable.Columns.Add("VariableValue", typeof(string));
                foreach (var _variable in _environment["variables"])
                {
                    _dataTable.Rows.Add(((JProperty)_variable).Name, ((JProperty)_variable).Value["value"].ToString());
                    //releaseVariables.Add(new ReleaseEnvironment { environmentName = _environmentName, variableName = ((JProperty)_variable).Name, variableValue = ((JProperty)_variable).Value["value"].ToString() });
                }
                releaseVariables.Add(_environmentName, _dataTable);
            }

            #region SET_OPERATIONS
            #endregion

            #endregion
#endif
        }

        /// <summary>
        /// Uploads a file from the specified path to a secured FTP location
        /// </summary>
        /// <param name="sourceFilePath">Specifies the file name with full path which has to be uploaded to the secured FTP location</param>
        /// <param name="connectionInfo">Specifies the information required to establish a connection with the secured FTP location</param>
        public static void UploadFromFileShareToSftp(string sourceFilePath, string targetFtpFolder)
        {
            try
            {
                var sessionOptions = new SessionOptions()
                {
                    HostName = "ftp1.exacttarget.com",
                    UserName = "10420810",
                    Password = "Ri9..Sn2s7",
                    Protocol = WinSCP.Protocol.Sftp,
                    FtpMode = FtpMode.Passive,
                    SshPrivateKeyPath = null
                };

                using (var session = new Session())
                {
                    if (true)
                    {
                        string sftpLogFilename = "UploadFromFileShareToSftp__Thread_" + Thread.CurrentThread.ManagedThreadId + "_Debug.log";
                        string sftpLogPath = @"D:\_files.sftp001.txt";

                        if (!Directory.Exists(sftpLogPath))
                            Directory.CreateDirectory(sftpLogPath);
                        string pathToSFTPLog = Path.Combine(sftpLogPath, string.Format(@"{0}", sftpLogFilename));
                        session.SessionLogPath = pathToSFTPLog;
                    }
                    sessionOptions.SshHostKeyFingerprint = session.ScanFingerprint(sessionOptions);

                    // Connect
                    session.Open(sessionOptions);
                    
                    //session.Timeout = TimeSpan.FromMilliseconds(connectionInfo.SftpTimeoutInMilliseconds);

                    // Upload files
                    var transferOptions = new TransferOptions()
                    {
                        ResumeSupport = new TransferResumeSupport { State = TransferResumeSupportState.Off },
                        TransferMode = TransferMode.Binary
                    };

                    var transferResult = session.PutFiles(sourceFilePath, targetFtpFolder, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();
                }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Gets the DateTime stamp associated with Geneva's latest data publication
        /// Error Codes = 624x
        /// </summary>
        /// <param name="externalRequestId">ETRequestID used for logging purposes</param>
        /// <param name="internalRequestId">EIRequestID used for logging purposes</param>
        /// <param name="connectionString">ConnectionString to connect to Geneva's data publication</param>
        /// <param name="genevaVersion">Version of the Geneva API to be used to connect to Geneva</param>
        /// <returns>Specifies the DateTime stamp when Geneva's latest data publication was made</returns>
        public static DateTime GetLatestPublicationDateTime(String connectionString, String genevaVersion = "4.0")
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException("connectionString");

            SqlConnection connection = null;
            DateTime publicationDateTime = DateTime.MinValue;
            SqlDataReader dataReader;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                command = new SqlCommand("spGetLatestPublication", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter genevaVersionParameter = new SqlParameter("@GenevaVersion", SqlDbType.VarChar, 20);
                genevaVersionParameter.Value = genevaVersion;

                if (command.Parameters != null)
                {
                    command.Parameters.Add(genevaVersionParameter);
                }

                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows)
                {
                    dataReader.Read();
                    publicationDateTime = (DateTime)dataReader["PublishDate"];
                }

                dataReader.Close();
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine(invalidOpEx.Message);
                publicationDateTime = DateTime.MinValue;
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
                publicationDateTime = DateTime.MinValue;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                publicationDateTime = DateTime.MinValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return publicationDateTime;
        }

        public void BrowseBlobContainer(string blobContainerConnectionString, string blobContainerName)
        {
            var container = CloudStorageAccount.Parse(blobContainerConnectionString)
                             .CreateCloudBlobClient()
                             .GetContainerReference(blobContainerName);

            foreach (CloudBlockBlob cloudBlockBlob in container.ListBlobs(new BlobRequestOptions { UseFlatBlobListing = true }))
            {
                Console.WriteLine(cloudBlockBlob.Name + "|" + cloudBlockBlob.Properties.Length + "|" + cloudBlockBlob.Properties.LastModifiedUtc.ToString());
            }
        }

       

        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static bool ValidateRedirectionUrlCallback(string autodiscoverUrl)
        {
            // Return true if the URL is an HTTPS URL.
            return autodiscoverUrl != null && autodiscoverUrl.ToLower().StartsWith("https://");
        }

        /// <summary>
        /// Gets text body of the specific email from MS Exchange Server by Email Subject
        /// </summary>
        /// <param name="microsoftUsername"></param>
        /// <param name="microsoftPassword"></param>
        /// <param name="microsoftDomainName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="emailSubject"></param>
        public static ExchangeService ConnectToMsExchangeServer(string microsoftUsername, string microsoftPassword, string microsoftDomainName, string emailAddress, string emailFolderName, out FolderId folderId, out ItemView itemView)
        {
            #region INIT
            ExchangeService exchangeService = null;
            #endregion

            #region CONNECTING_TO_MSEXCHANGE_SERVER
            //Instead of pushing ExchangeService to use specific version of ExchangeServer,
            //let ExchangeService choose the latest available version of Exchange Server
            exchangeService = new ExchangeService();
            exchangeService.TraceEnabled = true;
            exchangeService.Credentials = new NetworkCredential(microsoftUsername, microsoftPassword, microsoftDomainName);
            exchangeService.AutodiscoverUrl(emailAddress, ValidateRedirectionUrlCallback);

            var folderView = new FolderView(1);
            itemView = new ItemView(1);
            itemView.OrderBy.Add(ItemSchema.DateTimeReceived, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);
            itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);

            var emailFolderParts = emailFolderName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            FindFoldersResults folders = null;
            folderId = null;
            if (emailFolderParts != null && emailFolderParts.Length > 0)
            {
                folderId = emailFolderParts[0].ToLower().Equals("inbox") ? WellKnownFolderName.MsgFolderRoot : WellKnownFolderName.Inbox;
                foreach (var emailFolderPart in emailFolderParts)
                {
                    folders = exchangeService.FindFolders(folderId, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, emailFolderPart)), folderView);
                    folderId = folders.FirstOrDefault().Id;
                }
            }
            #endregion

            return exchangeService;
        }

        /// <summary>
        /// Gets text body of the specific email from MS Exchange Server by query string
        /// </summary>
        /// <param name="exchangeService"></param>
        /// <param name="folderId"></param>
        /// <param name="itemView"></param>
        /// <param name="searchQueryString"></param>
        /// <returns></returns>
        public static string GetTextEmailBodyByEmailSubjectFromExchangeServer(ExchangeService exchangeService, FolderId folderId, ItemView itemView, string searchQueryString)
        {
            #region INIT
            var textEmailBody = string.Empty;
            var htmlDocument = new HtmlDocument();
            #endregion

            #region FINDING_EMAIL
            var findResults = exchangeService.FindItems(folderId, searchQueryString, itemView);
            
            foreach (Item item in findResults.Items)
            {
                #region TRY_QUERYING_WITH_NEWER_VERSION_OF_MSEXCHANGE_SERVER_FIRST
                if (exchangeService.RequestedServerVersion.ToString().Contains("2013"))
                {
                    var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body, ItemSchema.TextBody);
                    var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                    textEmailBody = ((message != null && message.TextBody != null && !string.IsNullOrWhiteSpace(message.TextBody.Text)) ? message.TextBody.Text : string.Empty);
                }
                #endregion

                #region FALLBACK_TO_OLDER_VERSION_IF_OTHERWISE
                if (string.IsNullOrWhiteSpace(textEmailBody))
                {
                    var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body);
                    var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                    htmlDocument.LoadHtml(message.Body);
                    textEmailBody = htmlDocument.DocumentNode.InnerText;
                }
                #endregion
            }
            #endregion

            return textEmailBody;
        }

        private static void InstallCertificate(string cerFileName)
        {
            X509Certificate2 certificate = new X509Certificate2(cerFileName);
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            store.Add(certificate);
            store.Close();
        }

        public void SendBulkTbnRequestsToWebApi(string webApiUri, string resourceRelativeUri, string contentType, string bearerAuthToken)
        {
            try
            {
                var httpClient = new HttpClient();
                var httpContent = string.Empty;
                var noOfTbnRequests = 1000;
                httpClient.Timeout = TimeSpan.FromMinutes(60);

                httpClient.BaseAddress = new Uri(webApiUri);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerAuthToken);

                var jsonContentTemplate = "{\"TriggeredRequest\":{\"Type\":\"Generic\",\"EnterpriseAccountId\":\"96540\",\"SubsidiaryAccountId\":\"0\",\"ConversationId\":\"[ConversationId]\",\"TriggeredSendDefinitionExternalKey\":\"[TriggeredSendDefinitionExternalKey]\",\"Subscribers\":[{\"EmailAddress\":\"[EmailAddress]\",\"SubscriberKey\":\"[SubscriberKey001]\",\"Attributes\":[{\"Key\":\"FirstName\",\"Value\":\"Ashokan\"},{\"Key\":\"LastName\",\"Value\":\"Sivapragasam\"}]}]}}";
                
                var tasks = new Task<HttpResponseMessage>[noOfTbnRequests];
                for (int taskIndex = 0; taskIndex < noOfTbnRequests; taskIndex += 1)
                {
                    var expectedSubscriberKey001 = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    var expectedSubscriberKey002 = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    var conversationId = expectedSubscriberKey001;
                    httpContent = jsonContentTemplate.Replace("[ConversationId]", conversationId);
                    httpContent = httpContent.Replace("[EmailAddress]", "v-assiva@microsoft.com");
                    httpContent = httpContent.Replace("[SubscriberKey001]", expectedSubscriberKey001);
                    httpContent = httpContent.Replace("[SubscriberKey002]", expectedSubscriberKey002);
                    httpContent = httpContent.Replace("[TriggeredSendDefinitionExternalKey]", "TbnGenericEmailSendDefn001");

                    tasks[taskIndex] = httpClient.PostAsJsonAsync(resourceRelativeUri, httpContent);

                    Console.WriteLine("sent #" + taskIndex);
                }

                for (int taskIndex = 0; taskIndex < noOfTbnRequests; taskIndex += 1)
                {
                    try
                    {
                        var response = tasks[taskIndex].Result;
                        Console.WriteLine(response.ReasonPhrase + " | " + taskIndex);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void Example()
        {
            // This method runs asynchronously.
            int t = await System.Threading.Tasks.Task.Run(() => Allocate());
            Console.WriteLine("Compute: " + t);
        }

        static int Allocate()
        {
            // Compute total count of digits in strings.
            sizeofw += 1;
            //for (int z = 0; z < 100; z++)
            //{
            //    for (int i = 0; i < 1000000; i++)
            //    {
            //        string value = i.ToString();
            //        if (value == null)
            //        {
            //            return 0;
            //        }
            //        size += value.Length;
            //    }
            //}

            Thread.Sleep(3 * 1000);
            return sizeofw;
        }

        //static async void Allocate()
        //{
        //    var dateTime = await System.Threading.Tasks.Task.Run(() => AllocateThis());
        //    Console.WriteLine(dateTime);
        //}

        //static DateTime AllocateThis()
        //{
        //    return DateTime.Now;
        //}

        public void Download()
        {
            // Call and await separately.
            //Task<int> getLengthTask = AccessTheWebAsync();
            //// You can do independent work here.
            //int contentLength = await getLengthTask;

            AccessTheWebAsync().ContinueWith(OnCompletedAccess);
        }

        async void OnCompletedAccess(Task<int> contentLength)
        {
            new System.Threading.Tasks.Task(() =>
            {
                Console.WriteLine(
                    String.Format("\r\nLength of the downloaded string: {0}.\r\n", contentLength));
            });
        }

        // Three things to note in the signature:
        //  - The method has an async modifier. 
        //  - The return type is Task or Task<T>. (See "Return Types" section.)
        //    Here, it is Task<int> because the return statement returns an integer.
        //  - The method name ends in "Async."
        async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.
            HttpClient client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the
            // task you'll get a string (urlContents).
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            // You can do work here that doesn't rely on the string from GetStringAsync.
            DoIndependentWork();

            // The await operator suspends AccessTheWebAsync.
            //  - AccessTheWebAsync can't continue until getStringTask is complete.
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.
            //  - Control resumes here when getStringTask is complete. 
            //  - The await operator then retrieves the string result from getStringTask.
            string urlContents = await getStringTask;

            // The return statement specifies an integer result.
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.
            return urlContents.Length;
        }

        void DoIndependentWork()
        {
            Console.WriteLine(
            "Working . . . . . . .\r\n");
        }

        public void AddKeyToRedisCache()
        {
            //ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("LogicAppTransformationSettingsdit2.redis.cache.windows.net:6380,password=0niPK1hGjwd4IE/EFwlylFN2MIKCUrk1U/TG2ruNeco=,ssl=True,abortConnect=False");
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("EIvNextRedisCachedit.redis.cache.windows.net:6380,password=m5Iu/A9Pauok7tBORKo7BuKK2SBQ/lBccM+05vrI9Gs=,ssl=True,abortConnect=False");
            IDatabase cache4 = connection.GetDatabase();
            //Console.WriteLine(cache4.StringGet("SalesforceEnterpriseAccountDetails"));
            cache4.StringSet("6e68fd47-4a56-42ac-90b4-27d0dd57a1ac", "MipEivNext");
            //cache4.StringSet("SalesforceEnterpriseAccountDetails", "[{	\"SfmcEnterpriseAccountId\" : \"10420810\",	\"SfmcWebServiceEndpointUrl\" : \"https://webservice-microsoft.exacttarget.com/service.asmx\",	\"SfmcWebServiceUsername\" : \"API_10420810\",	\"SfmcWebServicePassword\" : \"welcome@1\",	\"SfmcEnterpriseAccountName\" : \"Microsoft Gmo v2\",	\"WebServiceCloseTimeoutFromMinutes\" : \"1\",	\"WebServiceSendTimeoutFromMinutes\" : \"1\",	\"WebServiceOpenTimeoutFromMinutes\" : \"1\",	\"WebServiceReceiveTimeoutFromMinutes\" : \"1\"},{	\"SfmcEnterpriseAccountId\" : \"10460681\",	\"SfmcWebServiceEndpointUrl\" : \"https://webservice-microsoft.exacttarget.com/service.asmx\",	\"SfmcWebServiceUsername\" : \"API_10460681\",	\"SfmcWebServicePassword\" : \"welcome@1\",	\"SfmcEnterpriseAccountName\" : \"Microsoft Gmo Onboarding\",	\"WebServiceCloseTimeoutFromMinutes\" : \"1\",	\"WebServiceSendTimeoutFromMinutes\" : \"1\",	\"WebServiceOpenTimeoutFromMinutes\" : \"1\",	\"WebServiceReceiveTimeoutFromMinutes\" : \"1\"},{	\"SfmcEnterpriseAccountId\" : \"96540\",	\"SfmcWebServiceEndpointUrl\" : \"https://webservice-microsoft.exacttarget.com/service.asmx\",	\"SfmcWebServiceUsername\" : \"Api_96540\",	\"SfmcWebServicePassword\" : \"welcome@1\",	\"SfmcEnterpriseAccountName\" : \"Gmo v1\",	\"WebServiceCloseTimeoutFromMinutes\" : \"1\",	\"WebServiceSendTimeoutFromMinutes\" : \"1\",	\"WebServiceOpenTimeoutFromMinutes\" : \"1\",	\"WebServiceReceiveTimeoutFromMinutes\" : \"1\"}]");
            Console.WriteLine(cache4.StringGet("SalesforceEnterpriseAccountDetails"));
            //cache4.StringSet("PostTBNRequestToTopic", "{  \"workFlow\": \"PostTBNRequestToTopic\",  \"context\": {    \"runImmediate\": true  },  \"trigger\": {    \"type\": \"http\",    \"properties\": {      \"activityTrackingEnabled\": \"@Variable(ActivityTrackingEnabled_PostTBNRequestToTopic)\",      \"activityTrackingProperties\": {        \"TrackingServiceBusConnectionString\": \"@Variable(ActivityTrackingConnectionString_PostTBNRequestToTopic)\",        \"GroupingMessage\": \"False\",        \"MipContract\": \"True\",        \"TrackingTopic\": \"tracking\"      }    }  },  \"tasks\": [    {      \"name\": \"GenerateMessageIdForTBNRequest\",      \"sequence\": 0,      \"type\": \"GenerateUniqueIdAndSetInContext\",      \"properties\": {        \"contextVariableName\": \"messageId\"      }    },    {      \"name\": \"GenerateCorrelationIdForForTBNRequest\",      \"sequence\": 0,      \"type\": \"GenerateUniqueIdAndSetInContext\",      \"properties\": {        \"contextVariableName\": \"correlationId\"      }    },    {      \"name\": \"SendMessageToTBNRequestTopic\",      \"sequence\": 1,      \"type\": \"SendtoServiceBus\",      \"trace\": true,      \"headers\": {        \"MessageId\": \"$.MifContext.messageId\",        \"MessageCorrelationId\": \"$.MifContext.correlationId\",        \"ServiceName\": \"TBNRequests\",        \"SourceSystemName\": \"TBNTenant\",        \"SourceSystemReferenceId\": \"$.MifContext.Request.ID\",        \"SchemaName\": \"TBN\",        \"PublisherBridgeName\": \"PostTBNRequestToTopic\",        \"OriginSystemName\": \"TBNTenant\",        \"OriginSystemReferenceId\": \"$.MifContext.Request.ID\",        \"OriginatingEventName\": \"TBNRequestToSFMC\",        \"ActivityType\": \"Publish\"      },      \"properties\": {        \"client\": \"Topic\",        \"to\": \"@Variable(TBNRequestsTopic)\",        \"connectionString\": \"@Variable(EIvNextServiceBusConnectionString)\",        \"handleLargeMessageSize\": \"true\",        \"mipStorageAccountName\": \"@Variable(EIvNextStorageName)\",        \"mipStorageAccountKey\": \"@Variable(EIvNextStorageKey)\",        \"mipReferenceContainerName\": \"largemessagecontainer\",        \"maxMessageSizeInKb\": \"256\"      }    }  ]}");
        }

        public string GetAccessToken(string oauthUrlForMsAad, string grant_type, string client_id, string client_secret, string resource)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(oauthUrlForMsAad);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/form-data"));
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials" ),
                new KeyValuePair<string, string>("client_id", client_id),
                new KeyValuePair<string, string>("client_secret",client_secret),
                new KeyValuePair<string, string>("resource",resource)
            };

            var content = new FormUrlEncodedContent(parameters);
            var response = client.PostAsync(client.BaseAddress, content).Result;
            var access_token = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                var responseText = response.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(responseText);
                access_token = (string)jObject.SelectToken("access_token");
            }
            return access_token;
        }

        public void AddMessagesToTopicSubscription(string svcBusConnectionString, string sourceTopicName)
        {
            var sourceTopicClient = TopicClient.CreateFromConnectionString(svcBusConnectionString, sourceTopicName);
            BrokeredMessage brokeredMessage = null;

            var messageTemplate = "{  \"header\": {    \"MessageId\": \"d29d8241-49c8-4eb7-b06e-d72638bed83b\",    \"MessageCorrelationId\": \"9a87d86f-7ea6-4da9-8f0e-a7d69263b8e1\",    \"ServiceName\": \"PlaningActivity\",    \"SourceSystemName\": \"Allocadia\",    \"SourceSystemReferenceId\": \"DhrTst069\",    \"EventCaptureTimeStamp\": \"8/21/2016 6:07:34 PM\",    \"MessageBornTimeStamp\": \"8/21/2016 6:07:34 PM\",    \"SchemaName\": \"MIPEventMessage\",    \"PublisherBridgeName\" : \"PublishToPlaningActivity\",    \"OriginSystemName\": \"Allocadia\",    \"OriginSystemReferenceId\": \"DhrTst069\",    \"OriginatingEventName\": \"Allocadia\",    \"TargetSystemName\": \"SFMC\"  },  \"MessageBody\": {	\"ActivityID\" : \"99999\",	\"ActivityName\" : \"ActivityName999999\",	\"ActivityOwner\" : \"ActivityOwner999999\",	\"ExecutionTeam\" : \"ExecutionTeam999999\"}}";
                //\"MessageBody\": {	\"ActivityID\" : \"539121\",	\"ActivityName\" : \"USCMO - Affordable Innovation - Sales Enablement eBook\",	\"ActivityOwner\" : \"https://info.microsoft.com/AffordableInnovationSalesEnablementeBook-Register.html                      CO-Devices-CNTNT-FY16-08Aug-21-Affordable Innovation-Sales Enablement\",	\"ExecutionTeam\" : \"M&O - Windows\"}}";

            for (int i = 0; i < 1; i++)
            {
                System.Threading.Tasks.Task[] sends = new System.Threading.Tasks.Task[1];
                for (int idx = 0; idx < sends.Length; idx++)
                {
                    brokeredMessage = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(messageTemplate.Replace("{Id}", idx.ToString("0000")).Replace("{Datetime}", DateTime.Now.ToString("yyyyMMddHHmmssffff")))));
                    //brokeredMessage.Properties["MipMessageType"] = "TriggeredSendNotification_Generic";
                    sends[idx] = sourceTopicClient.SendAsync(brokeredMessage);
                }

                System.Threading.Tasks.Task.WaitAll(sends);
            }
        }

        public void BatchMessagesToTopicSubscription(string svcBusConnectionString, string sourceTopicName, string sourceSubscriptionName, string targetTopicName)
        {
            SubscriptionClient sourceSubscriptionClient = SubscriptionClient.CreateFromConnectionString(svcBusConnectionString, sourceTopicName, sourceSubscriptionName);
            var targetTopicClient = TopicClient.CreateFromConnectionString(svcBusConnectionString, targetTopicName);

            var tasks = sourceSubscriptionClient.ReceiveBatchAsync(5, TimeSpan.FromSeconds(1)).ContinueWith(ProcessReceivedMessages);

            System.Threading.Tasks.Task.WaitAll(tasks);
        }

        async void ProcessReceivedMessages(Task<IEnumerable<BrokeredMessage>> brokeredMessages)
        {
            foreach (var brokeredMessage in brokeredMessages.Result)
            {
                Console.WriteLine("{0} received", brokeredMessage.ToString());
                await brokeredMessage.CompleteAsync();
                Console.WriteLine("{0} complete", brokeredMessage.ToString());
            }
        }

        public void IsPrimaryEngine()
        {
            var higherPriorityEngines = new Dictionary<object, object> { { "PollingInterval", "30" } };
            Console.WriteLine(DateTime.UtcNow.Subtract(DateTime.Parse("2016-09-29 14:20:05.267")).TotalSeconds < 5 * 30);
        }

        public void ExptOut(out string param01)
        {
            param01 = "orange";
        }

        public void PrepareAppDriverSettingsModel()
        {
            ApplicationDriverSettings ads = new ApplicationDriverSettings()
            {
                emailProviders = new List<EmailProvider>()
                {
                    new EmailProvider
                    {
                        name = "Marketo",
                        webService = new WebService
                        {
                            useCertificate = "0",
                            wsBusinessUnitAccountId = "681",
                            wsClientSideCertificateFindBy = "FindBy",
                            wsClientSideCertificateStoreLocation = "My",
                            wsClientSideCertificateStoreName = "Personal",
                            wsClientSideCertificateSubjectName = "Sub",
                            wsPassword = "Pwd",
                            wsServerSideCertificateFindBy = "FindBy",
                            wsServerSideCertificateStoreLocation = "My",
                            wsServerSideCertificateStoreName = "Personal",
                            wsServerSideCertificateSubjectName = "Sub",
                            wsUri = "Uri",
                            wsUsername = "User"
                        }
                    }
                },
                smokeTests = new List<SmokeTest>()
                {
                    new SmokeTest
                    {
                        canSkipEmailInterchangeApplication=true,
                        isEnabled=false,
                        name="Name1",
                        requestDataFilePath="/red",
                        requestType="Type1",
                        salesforceFtoTemplateFilePath="/green",
                        triggeredBy="By"
                    },
                    new SmokeTest
                    {
                        canSkipEmailInterchangeApplication=true,
                        isEnabled=false,
                        name="Name2",
                        requestDataFilePath="/red",
                        requestType="Type2",
                        salesforceFtoTemplateFilePath="/green",
                        triggeredBy="By"
                    },
                    new SmokeTest
                    {
                        canSkipEmailInterchangeApplication=true,
                        isEnabled=false,
                        name="Name3",
                        requestDataFilePath="/red",
                        requestType="Type3",
                        triggeredBy="By"
                    }
                }
            };

            var adsXml = Serialize<ApplicationDriverSettings>(ads);
            Console.WriteLine(adsXml);

            var adsObj001 = Deserialize<ApplicationDriverSettings>(File.ReadAllText(@"D:\OneItVso\Email Interchange\Application\Main\Automation\AutomatedPulseSvc\DriverFiles\ApplicationDriverSettings.xml"));
            Console.WriteLine(adsObj001);

            var adsObj002 = Deserialize<InterchangeFileRequest>(File.ReadAllText(@"D:\OneItVso\Email Interchange\Application\Main\Automation\AutomatedPulseSvc\DriverFiles\BulksendDataAzure.xml"));
            Console.WriteLine(adsObj002);

            var adsObj003 = Deserialize<TriggeredRequest>(File.ReadAllText(@"D:\OneItVso\Email Interchange\Application\Main\Automation\AutomatedPulseSvc\DriverFiles\GenericTbnData.xml"));
            Console.WriteLine(adsObj003);
        }

        public void SqlAsyncProcessingPoc(string sqlConnectionString, string sqlCommandText)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(sqlConnectionString);
                command = new SqlCommand(sqlCommandText, connection);
                command.CommandTimeout = 1;
                connection.Open();
                command.BeginExecuteReader(new AsyncCallback(QueryExecutorCallback), command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | " + "Exception - " + ex.Message.Replace("\n", " ").Replace("\r", " "));
            }
        }

        private void QueryExecutorCallback(IAsyncResult result)
        {
            try
            {
                SqlCommand cmd = (SqlCommand)result.AsyncState;
                SqlDataReader reader = cmd.EndExecuteReader(result);
                while (reader != null && reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }

                Console.WriteLine("Alright!");

                if (cmd.Connection.State.Equals(ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | " + "Exception - " + ex.Message.Replace("\n", " ").Replace("\r", " "));
            }
        }

        public void SqlConnectionPoc(string sqlConnectionString)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(sqlConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | " + "Exception - " + ex.Message.Replace("\n", " ").Replace("\r", " "));
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// It converts an image as base64 string
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <returns></returns>
        public string ConvertImageAsBase64String(string imageFilePath)
        {
            string base64String = string.Empty;
            using (var image = Image.FromFile(imageFilePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    var imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudblobcontainer"></param>
        /// <param name="filePath"></param>
        public void UploadFileToBlob(CloudBlobContainer cloudblobcontainer, string filePath)
        {
            CloudBlockBlob cloudblockblob = cloudblobcontainer.GetBlockBlobReference(Path.GetFileName(filePath));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                cloudblockblob.UploadFromStream(fs);
            }
        }

        /// <summary>
        /// Transfer content from blob to SFTP server
        /// </summary>
        /// <param name="blobContainer"></param>
        /// <param name="blobName"></param>
        /// <param name="sftpServer"></param>
        /// <param name="sftpUser"></param>
        /// <param name="sftpPassword"></param>
        /// <param name="licenseKey"></param>
        /// <param name="ftpDirectory"></param>
        /// <returns></returns>
        private bool TransferBlobToLocalFileSystem(CloudBlobContainer blobContainer, string blobName, string localPathForFile)
        {
            Console.WriteLine("Transfer a blob to local file server is started..");

            var blobToUpload = blobContainer.GetBlockBlobReference(blobName);

            TimeSpan ts = new TimeSpan(10, 30, 0);
            blobToUpload.ServiceClient.Timeout = ts;
            blobToUpload.DownloadToFile(localPathForFile);

            Console.WriteLine("Transfer a blob to local file server is successful..");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maxFileSize"></param>
        public void CreateDynamicBulksendFile(string path, int maxFileSize)
        {
            long fileSize = 0;
            var alias = string.Empty;
            using (var sw = new StreamWriter(path))
            {
                sw.AutoFlush = true;
                sw.WriteLine("SubscriberKey\tEmailAddress\tUserSubscriptionId\tScheduledStartDate\tScheduledEndDate\tBatchGUID\tBulkSendGUID");
                while (fileSize <= maxFileSize)
                {
                    for (var idx = 0; idx < 1000; idx += 1)
                    {
                        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string SubscriberKey = Convert.ToBase64String(bytes);

                        bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string UserSubscriptionId = Convert.ToBase64String(bytes);

                        bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string ScheduledStartDate = Convert.ToBase64String(bytes);

                        bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string ScheduledEndDate = Convert.ToBase64String(bytes);

                        bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string BatchGUID = Convert.ToBase64String(bytes);

                        bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string BulkSendGUID = Convert.ToBase64String(bytes);

                        sw.WriteLine(SubscriberKey + "\tv-assiva@microsoft.com\t" + UserSubscriptionId + "\t" + ScheduledStartDate + "\t" + ScheduledEndDate + "\t" + "1" + "\t" + "1");
                    }

                    var fio = new FileInfo(path);
                    fileSize = fio.Length / (1024 * 1024);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maxFileSize"></param>
        public void CreatePlFile(string path, int maxFileSize)
        {
            long fileSize = 0;
            var alias = string.Empty;
            using (var sw = new StreamWriter(path))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Ind Email Name	Name");
                while (fileSize <= maxFileSize)
                {
                    for (var idx = 0; idx < 14000000; idx += 1)
                    {
                        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string id1 = Convert.ToBase64String(bytes)
                                                .Replace('+', '_')
                                                .Replace('/', '-')
                                                .TrimEnd('=');

                        bytes = System.Text.Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", string.Empty));
                        string id2 = Convert.ToBase64String(bytes)
                                                .Replace('+', '_')
                                                .Replace('/', '-')
                                                .TrimEnd('=');

                        alias = id1 + "" + id2;
                        sw.WriteLine(alias + "@gmail.com" + "	" + alias);
                    }
                    
                    var fio = new FileInfo(path);
                    fileSize = fio.Length / (1024 * 1024);
                }
                
            }
        }

        /// <summary>
        /// Gets text body of the specific email from MS Exchange Server by Email Subject
        /// </summary>
        /// <param name="microsoftUsername"></param>
        /// <param name="microsoftPassword"></param>
        /// <param name="microsoftDomainName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="emailSubject"></param>
        public string GetAllEmailFolders(string microsoftUsername, string microsoftPassword, string microsoftDomainName, string emailAddress, string emailFolderName, string emailSubject)
        {
            var textEmailBody = string.Empty;
            ExchangeService exchangeService = null;
            HtmlDocument htmlDocument = new HtmlDocument();

            try
            {
                //Instead of pushing ExchangeService to use specific version of ExchangeServer,
                //let ExchangeService choose the latest available version of Exchange Server
                exchangeService = new ExchangeService();
                exchangeService.Credentials = new NetworkCredential(microsoftUsername, microsoftPassword, microsoftDomainName);
                exchangeService.AutodiscoverUrl(emailAddress);

                Console.WriteLine("Exchange version available for your account: '" + exchangeService.RequestedServerVersion + "'");

                var folderView = new FolderView(1);
                var itemView = new ItemView(1);
                itemView.OrderBy.Add(ItemSchema.DateTimeReceived, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);
                itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);

                var folders = exchangeService.FindFolders(emailFolderName.ToLower().Equals("inbox") ? WellKnownFolderName.MsgFolderRoot : WellKnownFolderName.Inbox, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, emailFolderName)), folderView);
                var folderId = folders.FirstOrDefault().Id;
                var findResults = exchangeService.FindItems(folderId, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.ContainsSubstring(ItemSchema.Subject, emailSubject)), itemView);

                foreach (Item item in findResults.Items)
                {
                    if (exchangeService.RequestedServerVersion.ToString().Contains("2013"))
                    {
                        Console.WriteLine("Using native functionality in 'ExchangeService' to get text version of the email");
                        var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body, ItemSchema.TextBody);
                        var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                        textEmailBody = message.TextBody.Text;
                    }
                    else
                    {
                        Console.WriteLine("Using out-of-box functionalities in 'HtmlAgilityPack' to get text version of the email");
                        var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body);
                        var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                        htmlDocument.LoadHtml(message.Body);
                        textEmailBody = htmlDocument.DocumentNode.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return textEmailBody;
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateBulksendInputFile()
        {
            var line1 = "SubscriberKey	EmailAddress	BatchGUID	BulkSendGUID	FirstName	LastName	DateFor";
            var lineN = "{SubscriberKey}	EmailAddress	{BatchGUID}	{BulkSendGUID}	FirstName	LastName	{DateFor}";
            var batchGuid = Guid.NewGuid().ToString();
            var bulksendGuid = Guid.NewGuid().ToString();

            using (var sw = new StreamWriter(@"C:\temp\prasanna\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ffff")+".tsv"))
            {
                sw.WriteLine(line1);
                lineN = lineN.Replace("{BatchGUID}", batchGuid).Replace("{BulkSendGUID}", bulksendGuid).Replace("{DateFor}", DateTime.Now.ToString("yyyy-MM-dd"));
                for (var idx = 0; idx < 50000; idx += 1)
                    sw.WriteLine(lineN.Replace("{SubscriberKey}", Guid.NewGuid().ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="idx"></param>
        public void ResearchOnList(string filePath, int idx)
        {
            int bufferSize = (2 * 1024 * 1024);
            FileStream fileStream = File.OpenRead(filePath);
            int blockCount = (int)(fileStream.Length / bufferSize) + 1;

            Console.WriteLine(blockCount.ToString() + fileStream.ToString());

            List<string> blockIds = new List<string>();
            Parallel.For(0, 2, i =>
            {
                lock (this)
                {
                    string currentBlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                    blockIds.Add(i + "_" + currentBlockId);
                }
            });

            if (blockIds.Count < 2)
                Console.WriteLine();
            Console.WriteLine("idx - " + idx + " Length - " + blockIds.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appInsightsFile"></param>
        /// <param name="xmlNameSpace"></param>
        /// <param name="mappingsText"></param>
        /// <param name="regexPattern"></param>
        public void UpdateAppInsightsConfigFileByServiceConfigValues(string appInsightsFile, string xmlNameSpace, string mappingsText, string regexPattern)
        {            
            var xDoc = new XmlDocument();
            xDoc.Load(appInsightsFile);

            var ns = new XmlNamespaceManager(xDoc.NameTable);
            ns.AddNamespace("ns", xmlNameSpace);

            //var dictMappingsBetweenSvcConfigSettingsAndAppInsightsConfigSettings = new Dictionary<string, string>();
            var rgx = new Regex(regexPattern);
            var dictMappingsText = (from p in mappingsText.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                    let x = rgx.Match(p).Groups
                                    select x).ToDictionary(a => a[1].Value, a => a[2].Value);

            foreach (var key in dictMappingsText.Keys)
            {
                if (xDoc != null && !string.IsNullOrWhiteSpace(dictMappingsText[key]) && xDoc.SelectSingleNode(dictMappingsText[key], ns) != null && !string.IsNullOrWhiteSpace(xDoc.SelectSingleNode(dictMappingsText[key], ns).InnerText))
                    xDoc.SelectSingleNode(dictMappingsText[key], ns).InnerText = GetConfigurationSettingValue(key, string.Empty);
            }

            xDoc.Save(appInsightsFile);
            xDoc.RemoveAll();
            xDoc = null;
        }

        /// <summary>
        /// Use this method to return settings from App.Config or RoleEnvironment
        /// </summary>
        /// <param name="configurationString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetConfigurationSettingValue(string configurationString, string defaultValue)
        {
            if (string.IsNullOrEmpty(configurationString)) throw new ArgumentNullException("configurationString");

            string returnValue;
            try
            {
                if (RoleEnvironment.IsAvailable)
                    returnValue = RoleEnvironment.GetConfigurationSettingValue(configurationString);
                else
                    returnValue = ConfigurationManager.AppSettings[configurationString];
            }
            catch (RoleEnvironmentException)
            {
                return defaultValue;
            }
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delimitedFile"></param>
        /// <param name="delimiter"></param>
        public void GetDictionaryFromDelimitedFile(string delimitedFile, string delimiter)
        {
            string[] delimitedFileLines = File.ReadAllLines(delimitedFile);
            Dictionary<string, string> b = (from p in delimitedFileLines
                                            let x = p.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                                            select x).ToDictionary(a => a[0], a => a[1]);

            Console.WriteLine(b);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ImportDataFromCsvToDatatable()
        {
            var tsvFileContent = @"AttributeColumn	AttributeValue
Communication_Total	5537
Communications_Active	1112
CommunicationClass_Total	832
Subscription_Subscribed	46448766
Subscription_DistinctSubscribed	23551661
Subscription_DistinctSubscribedAndContactable	3527566
Standalone_Subscribed	142460455
Standalone_DistinctSubscribed	123855972
Standalone_DistinctSubscribedAndContactable	123766679
Answer_Total	264755006
CommID393_TechNetFlash_DistinctEmails	812609
CommID51_MSDNFlash_DistinctEmails	194189
";

            string[] tsvFileLines = tsvFileContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] splitFields;
            splitFields = tsvFileLines[0].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int Cols = splitFields.GetLength(0);
            DataTable datatable = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int columnIdx = 0; columnIdx < Cols; columnIdx++)
                datatable.Columns.Add(splitFields[columnIdx].ToLower(), typeof(string));
            DataRow Row;
            for (int RowIdx = 1; RowIdx < tsvFileLines.GetLength(0); RowIdx++)
            {
                splitFields = tsvFileLines[RowIdx].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Row = datatable.NewRow();
                for (int columnIdx = 0; columnIdx < Cols; columnIdx++)
                    Row[columnIdx] = splitFields[columnIdx];
                datatable.Rows.Add(Row);
            }

            Console.WriteLine(datatable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> PacketData()
        {
            var packets = new List<string>();
            var maxLengthOfPacket = 3980;
            var packet = string.Empty;
            var queuedContentTemplate = "Key_{0}#|#Value_{0}" + Environment.NewLine;
            var queuedContent = string.Empty;
            for (var idx = 0; idx < 2000; idx += 1)
            {
                queuedContent = string.Format(queuedContentTemplate, idx);
                if ((packet + queuedContent).Length > maxLengthOfPacket)
                {
                    packets.Add("<pack>" + packet + "</pack>");
                    packet = string.Empty;
                }
                packet += queuedContent;
            }

            return packets;
        }

        /// <summary>
        /// Parse values from raw text email body
        /// </summary>
        /// <param name="exactTargetDriverFile"></param>
        /// <param name="textEmailBody"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseValuesFromRawTextEmailBody(string exactTargetDriverFile, string textEmailBody)
        {
            #region INIT_VARIABLES
            var xDoc = new XmlDocument();
            xDoc.Load(exactTargetDriverFile);

            //To store the dynamic variables and values
            var dictionary = new Dictionary<string, string>();
            #endregion

            #region GET_PATTERNS_VARIABLES_AND_VALUES_TEXT_EMAIL_BODY
            //Get patterns and variables
            var xNodes = xDoc.SelectNodes("/DriverFile/RegexPatterns/Regex");
            foreach (XmlNode xNode in xNodes)
            {
                if (!dictionary.ContainsKey(xNode.Attributes["Variable"].InnerText))
                {
                    var regex = new Regex(xNode.Attributes["Pattern"].InnerText);
                    var match = regex.Match(textEmailBody);
                    dictionary[xNode.Attributes["Variable"].InnerText] = match.Groups[1].Value;
                }
            }
            #endregion

            return dictionary;
        }

        /// <summary>
        /// Parse values from xml document
        /// </summary>
        /// <param name="genesisDriverFile"></param>
        /// <param name="xmlText"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseValuesFromXmlDocument(string genesisDriverFile, string xmlText)
        {
            #region INIT_VARIABLES
            var xDocumentDriverFile = new XmlDocument();
            xDocumentDriverFile.Load(genesisDriverFile);
            var xDocumentRawContent = new XmlDocument();
            xDocumentRawContent.LoadXml(xmlText);

            //To store the dynamic variables and values
            var dictionary = new Dictionary<string, string>();
            #endregion

            #region GET_VARIABLES_AND_VALUES_FROM_XML_DOCUMENT
            //Get patterns and variables
            var xNodes = xDocumentDriverFile.SelectNodes("/DriverFile/Datasource/Data");
            foreach (XmlNode xNode in xNodes)
            {
                if (!dictionary.ContainsKey(xNode.Attributes["Variable"].InnerText))
                    dictionary[xNode.Attributes["Variable"].InnerText] = xDocumentRawContent.SelectSingleNode(xNode.Attributes["Xpath"].InnerText).InnerText;
            }
            #endregion

            return dictionary;
        }

        /// <summary>
        /// Pushes the values parsed from text email body to database server
        /// </summary>
        /// <param name="sqlConnectionString"></param>
        /// <param name="driverFile"></param>
        /// <param name="textEmailBody"></param>
        public void PushValuesFromTextEmailBodyToSqlServer(string sqlConnectionString, string driverFile, Dictionary<string, string> dictionary)
        {
            #region INIT_VARIABLES
            var xDoc = new XmlDocument();
            xDoc.Load(driverFile);
            #endregion

            #region SUBSTITUTE_VALUES_TO_REPLACEMENT_STRINGS_IN_SQL_QUERY
            var queryText = xDoc.SelectSingleNode("/DriverFile/SqlCommands").InnerText;
            //Substitute the values to the replacement strings in sql query
            foreach (var variable in dictionary.Keys)
                queryText = queryText.Replace("{" + variable + "}", dictionary[variable]);
            #endregion

            #region RUN_QUERY_TEXT_ON_SQL_SERVER
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(sqlConnectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(queryText, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Open))
                    sqlConnection.Close();
            }
            #endregion
        }

        /// <summary>
        /// Gets text body of the specific email from MS Exchange Server by Email Subject
        /// </summary>
        /// <param name="microsoftUsername"></param>
        /// <param name="microsoftPassword"></param>
        /// <param name="microsoftDomainName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="emailSubject"></param>
        public string GetTextEmailBodyByEmailSubjectFromExchangeServer(string microsoftUsername, string microsoftPassword, string microsoftDomainName, string emailAddress, string emailFolderName, string emailSubject)
        {
            var textEmailBody = string.Empty;
            ExchangeService exchangeService = null;
            HtmlDocument htmlDocument = new HtmlDocument();

            try
            {
                //Instead of pushing ExchangeService to use specific version of ExchangeServer,
                //let ExchangeService choose the latest available version of Exchange Server
                if (emailAddress.ToLower().EndsWith("@outlook.com"))
                {
                    exchangeService = new ExchangeService
                    {
                        TraceEnabled = false,
                        Credentials = new WebCredentials(emailAddress, microsoftPassword),
                        Url = new Uri(microsoftDomainName) //"https://outlook.com/EWS/Exchange.asmx"
                    };
                }
                else
                {
                    exchangeService = new ExchangeService();
                    exchangeService.Credentials = new NetworkCredential(microsoftUsername, microsoftPassword, microsoftDomainName);
                    exchangeService.AutodiscoverUrl(emailAddress);
                }

                Console.WriteLine("Exchange version available for your account: '" + exchangeService.RequestedServerVersion + "'");

                var folderView = new FolderView(1);
                var itemView = new ItemView(1);
                itemView.OrderBy.Add(ItemSchema.DateTimeReceived, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);
                itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);

                var folders = exchangeService.FindFolders(emailFolderName.ToLower().Equals("inbox") ? WellKnownFolderName.MsgFolderRoot : WellKnownFolderName.Inbox, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, emailFolderName)), folderView);
                var folderId = folders.FirstOrDefault().Id;
                var findResults = exchangeService.FindItems(folderId, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.ContainsSubstring(ItemSchema.Subject, emailSubject)), itemView);

                foreach (Item item in findResults.Items)
                {
                    if (exchangeService.RequestedServerVersion.ToString().Contains("2013"))
                    {
                        Console.WriteLine("Using native functionality in 'ExchangeService' to get text version of the email");
                        var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body, ItemSchema.TextBody);
                        var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                        textEmailBody = message.TextBody.Text;
                    }
                    else
                    {
                        Console.WriteLine("Using out-of-box functionalities in 'HtmlAgilityPack' to get text version of the email");
                        var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body);
                        var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                        htmlDocument.LoadHtml(message.Body);
                        textEmailBody = htmlDocument.DocumentNode.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return textEmailBody;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Imap4Server()
        {
            // Create a folder named "inbox" under current directory
            // to store the email file retrieved.
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox", curpath);

            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
                Directory.CreateDirectory(mailbox);

            MailServer oServer = new MailServer("imap-mail.outlook.com", "v-assiva@microsoft.com", "moni!131514IImon", true, ServerAuthType.AuthLogin, ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");

            oServer = new MailServer("imap.gmail.com", "ashokansivapragasam@gmail.com", "moni131514i", true, ServerAuthType.AuthLogin, ServerProtocol.Imap4);
            oClient = new MailClient("TryIt");

            // If your POP3 server requires SSL connection,
            // Please add the following codes:
            //oServer.SSLConnection = true;
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                //infos.Where(i => i.)
                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];
                    Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                        info.Index, info.Size, info.UIDL);

                    // Receive email from IMAP4 server
                    Mail oMail = oClient.GetMail(info);

                    Console.WriteLine("From: {0}", oMail.From.ToString());
                    Console.WriteLine("Subject: {0}\r\n", oMail.Subject);

                    // Generate an email file name based on date time.
                    System.DateTime d = System.DateTime.Now;
                    System.Globalization.CultureInfo cur = new
                        System.Globalization.CultureInfo("en-US");
                    string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    string fileName = String.Format("{0}\\{1}{2}{3}.eml",
                        mailbox, sdate, d.Millisecond.ToString("d3"), i);

                    // Save email to local disk
                    oMail.SaveAs(fileName, true);

                    // Mark email as deleted from IMAP4 server.
                    oClient.Delete(info);
                }

                // Quit and pure emails marked as deleted from IMAP4 server.
                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlText"></param>
        public void XslTransformer(string xslFile, string xmlInputFile, string xmlOutputFile)
        {
            XslTransform xslt = new XslTransform();
            xslt.Load(xslFile);
            xslt.Transform(xmlInputFile, xmlOutputFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlText"></param>
        public string XslTransformerV2(string xslFile, string xmlInputFile)
        {
            using (XmlReader xReader = XmlReader.Create(xmlInputFile))
            using (StringWriter sWriter = new StringWriter())
            using (XmlWriter xWriter = XmlWriter.Create(sWriter))
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslFile, XsltSettings.TrustedXslt, new XmlUrlResolver());
                xslt.Transform(xReader, xWriter);
                return sWriter.ToString();
            }
        }

        public string XslTransformerV3(string xslFile, string xmlInputData)
        {
            using (XmlReader xReader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(xmlInputData))))
            using (StringWriter sWriter = new StringWriter())
            using (XmlWriter xWriter = XmlWriter.Create(sWriter))
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslFile, XsltSettings.TrustedXslt, new XmlUrlResolver());
                xslt.Transform(xReader, xWriter);
                return sWriter.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void IfThenSemantics()
        {
            #region IF_CLAUSE
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection("Server=tcp:w4k2434z3i.database.windows.net;Database=InterchangeDB1;User ID=bluradar@w4k2434z3i;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False"))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(@"SELECT 'localhost' [ServerIf],'localhost' [ServerThen],'MSIFileProcessorHost' [Service1],'MSIFileProcessorHost' [Service2],'MSIFileProcessorHost' [Service3]", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dataTable);
                }
            }
            #endregion

            #region THEN_CLAUSE
            string thenClause = @"SC ""\\[ServerThen]"" STOP ""[Service1]"" & SC ""\\[ServerThen]"" STOP ""[Service2]"" & SC ""\\[ServerThen]"" STOP ""[Service3]""  & TIMEOUT /t ""10"" & SC ""\\[ServerThen]"" START ""[Service1]"" & SC ""\\[ServerThen]"" START ""[Service2]"" & SC ""\\[ServerThen]"" START ""[Service3]""";
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var regex = new Regex(@"\[(\w+)\]", RegexOptions.Compiled);
                var matches = regex.Matches(thenClause);
                foreach (System.Text.RegularExpressions.Match match in matches)
                    thenClause = thenClause.Replace(match.Groups[0].Value, dataRow[match.Groups[1].Value].ToString());

                #region EXECUTOR
                var processInfo = new ProcessStartInfo("cmd")
                {
                    ErrorDialog = false,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Arguments = "/c " + thenClause
                };

                var proc = System.Diagnostics.Process.Start(processInfo);
                // You can pass any delegate that matches the appropriate 
                // signature to ErrorDataReceived and OutputDataReceived
                proc.ErrorDataReceived += (sender, errorLine) => { if (errorLine.Data != null) Console.WriteLine(errorLine.Data); };
                proc.OutputDataReceived += (sender, outputLine) => { if (outputLine.Data != null) Console.WriteLine(outputLine.Data); };
                proc.BeginErrorReadLine();
                proc.BeginOutputReadLine();
                proc.WaitForExit();
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawXmlDocument"></param>
        /// <param name="cSharpSerializedXmlDocument"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public string CsharpObjectToXmlDOcument(string rawXmlDocument, string cSharpSerializedXmlDocument, Dictionary<string, string> mappings)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(cSharpSerializedXmlDocument);

            foreach (var key in mappings.Keys)
                rawXmlDocument = rawXmlDocument.Replace("{" + key + "}", (xDoc.SelectSingleNode(mappings[key]) == null || string.IsNullOrWhiteSpace(xDoc.SelectSingleNode(mappings[key]).InnerText) ? string.Empty : xDoc.SelectSingleNode(mappings[key]).InnerText));

            return rawXmlDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InternalMethod()
        {
            try
            {
                throw new Exception("SshException");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public DataTable GetSqlXmlReport(string connectionString, string sqlCommandText)
        {
            var report = new DataTable();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(sqlCommandText, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(report);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Open))
                    sqlConnection.Close();
            }

            return report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public DataSet GetSqlMixedReport(string connectionString, string sqlCommandText)
        {
            var reports = new DataSet();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(sqlCommandText, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(reports);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Open))
                    sqlConnection.Close();
            }

            return reports;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public string SendEmail(string toEmailAddresses, string subject, string body)
        {
            string result = "Message Sent Successfully..!!";
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("\"Ben\" smsql@microsoft.com");
            string senderPassword = "Paaab!@#)@On2015";//SecretsManager.Decrypt("bMHC/ZFEH9yFb6Yo5ryA0njF9nufaYQV6KvSPp3EapY5I2BiJ0IZ01DCaoDXVZNq2JzzjXxAYQp3LFtZQv6RthypXWdYTozDR4xnUMqBejXfOx3g2A9DSPs2QgZiuzDW5W2NgC45pomWlJl6K4cMO6lgZ3PudF3rmr85VVIWMQQK/7ff8WPn8WmVKIPF26lf3wIqRoVXfCbZkMnENA6PnlqEa6odhJ3V2jaYWuAmbgkHBWTLmlNkutTOkaxgE3xodO3ATSa98zRU1d8RY8Ro0MGqfMkddrMv6A2taZqjbsqUp/NzZL9Ct2AmtrSQQ6tjUMZlxCR+bneQLfpTyfOmRQ==", "1B647BE314FBC0E8ECCD885C3442A9E63C7EB5B5");

            try
            {
                if (string.IsNullOrWhiteSpace(toEmailAddresses))
                {
                    throw new ArgumentException("toEmailAddresses");
                }

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtphost.redmond.corp.microsoft.com",//"internal.smtp.mscom.phx.gbl",//"smtphost.redmond.corp.microsoft.com",//"HKXPRD3002.prod.outlook.com",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(from.Address, senderPassword),
                    Timeout = 30000,
                };

                System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(toEmailAddresses.Split(new[] { ';' })[0]);
                MailMessage msg = new MailMessage(from, to)
                {
                    IsBodyHtml = true,
                    Priority = System.Net.Mail.MailPriority.High,
                    Subject = subject,
                    Body = body
                };

                var toList = toEmailAddresses.Split(new[] { ';' });
                if (toList.Count() > 0)
                    for (int idx = 0; idx < toList.Count(); idx++)
                        msg.To.Add(new System.Net.Mail.MailAddress(toList[idx]));

                msg.Headers.Add("From", 
                 string.Format("{0} <{1}>", 
                 "Mic Ben", "v-assiva@microsoft.com"));    

                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!! Exception:- " + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Trycatchfinally()
        {
            while (true)
            {
                try
                {
                    //break;
                    throw new Exception("try");
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    Console.WriteLine("finally");
                    throw new Exception("yeah");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public void CreateDatatable(string connectionString)
        {
            DataTable dataTable = new DataTable();

            #region ASSIGN_SCHEMA_TO_DATATABLE
            // Here we create a DataTable with four columns.
            dataTable.Columns.Add("UniqueID", typeof(long));
            dataTable.Columns.Add("TenantName", typeof(string));
            dataTable.Columns.Add("MsiFileName", typeof(string));
            dataTable.Columns.Add("FileSize", typeof(long));
            dataTable.Columns.Add("EpochTimestamp", typeof(long));
            dataTable.Columns.Add("FileDroppedDateTime", typeof(DateTime));
            dataTable.Columns.Add("FilePickedDateTime", typeof(DateTime));
            dataTable.Columns.Add("FileCompletedDateTime", typeof(DateTime));
            dataTable.Columns.Add("IsValid", typeof(bool));
            dataTable.Columns.Add("ProcessState", typeof(string));
            #endregion

            dataTable.Rows.Add(0, "MMO", "MMO1", 1024, 20150318, DateTime.Now, DateTime.Parse("1900-01-01 01:01:01"), DateTime.Parse("1900-01-01 01:01:01"), false, "new");
            dataTable.Rows.Add(0, "MMO", "MMO2", 1024, 20150318, DateTime.Now, DateTime.Parse("1900-01-01 01:01:01"), DateTime.Parse("1900-01-01 01:01:01"), false, "new");
            dataTable.Rows.Add(0, "MMO", "MMO3", 1024, 20150318, DateTime.Now, DateTime.Parse("1900-01-01 01:01:01"), DateTime.Parse("1900-01-01 01:01:01"), false, "new");

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = "dbo.PoolMsiFileSupervisor";
                bulkCopy.BatchSize = 1000;
                // Write from the source to the destination.
                bulkCopy.WriteToServer(dataTable);
            }

            Console.WriteLine(dataTable.Rows.Count);
        }

        int Do(object n)
        {
            for (int idx = 0; idx < (int)n; idx += 1)
                _list.Add(idx.ToString());
            return (int)n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        void DoSomething(object n)
        {
            /*if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = "Thread_" + n;
            }*/
            Console.WriteLine("Thread_" + Thread.CurrentThread.ManagedThreadId + " n: " + n);
            for (int idx = 0; (int)n % 4 == 0 && idx < 100000; idx += 1)
                File.AppendAllText(@"c:\temp\threadpool_" + n + ".txt", Thread.CurrentThread.ManagedThreadId + "_" + n + "This is a test line " + idx + Environment.NewLine);

            Console.WriteLine("WorkItem - " + n + " is set");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        void DoThisthing(object n)
        {
            Thread.CurrentThread.Name = "Thread_" + n;
            Console.WriteLine(Thread.CurrentThread.Name + "_" + n);

            var msiRequestList = new List<string>();

            for (int idx = 0; idx < 100000; idx += 1)
                msiRequestList.Add("This is a test line " + idx + Environment.NewLine);


            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2
            };

            Parallel.ForEach(msiRequestList, parallelOptions, msiRequest =>
            {
                File.AppendAllText(@"c:\temp\threadpool_" + n + ".txt", Thread.CurrentThread.Name + "_" + n + msiRequest);
            });

            Console.WriteLine("WorkItem - " + n + " is set");
            _doneEvent.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        private static void Worker(object num)
        {
            // Each worker thread begins by requesting the 
            // semaphore.
            Console.WriteLine("Thread {0} begins " +
                "and waits for the semaphore.", num);
            _pool.WaitOne();

            // A padding interval to make the output more orderly. 
            int padding = Interlocked.Add(ref _padding, 100);

            Console.WriteLine("Thread {0} enters the semaphore.", num);

            // The thread's "work" consists of sleeping for  
            // about a second. Each thread "works" a little 
            // longer, just to make the output more orderly. 
            //
            Thread.Sleep(1000 + padding);

            Console.WriteLine("Thread {0} releases the semaphore.", num);
            Console.WriteLine("Thread {0} previous semaphore count: {1}",
                num, _pool.Release());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> returnList()
        {
            List<string> list = new List<string>
            {
                "ashok",
                null,
                "siva",
                "bharathi"
            };

            return list;
        }

        /// <summary>
        /// Download a file from sourceDriectory of Exact targets Sftp location
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sourceDriectory"></param>
        /// <param name="destinationPath"></param>
        /// <param name="address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void DownloadFile(string fileName, string sourceDriectory, string destinationPath, string address, string username, string password)
        {
            Sftp fileTransfer = null;
            try
            {
                SshParameters connectionParameters = new SshParameters(address, username, password);
                using (fileTransfer = new Sftp(connectionParameters))
                {
                    fileTransfer.Connect();
                    fileTransfer.SetBinaryMode();
                    fileTransfer.RemoteDir = sourceDriectory;
                    fileTransfer.LocalDir = new DirectoryInfo(destinationPath);
                    fileTransfer.Download(fileName);
                }
                return;
            }
            finally
            {
                if (fileTransfer != null && fileTransfer.IsConnected)
                    fileTransfer.Disconnect();

                fileTransfer = null;
            }
        }

        /// <summary>
        /// Upload a file to destinationDirectory of Exact targets Sftp location
        /// </summary>
        /// <param name="pathOfFileToBeTransfered"></param>
        /// <param name="address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="destinationDirectory"></param>
        public void UploadFile(string pathOfFileToBeTransfered, string address, string username, string password, string destinationDirectory)
        {
            Sftp fileTransfer = null;
            if (string.IsNullOrWhiteSpace(pathOfFileToBeTransfered))
            {
                throw new ArgumentNullException("pathOfFileToBeTransfered");
            }

            try
            {
                SshParameters connectionParameters = new SshParameters(address, username, password);
                using (fileTransfer = new Sftp(connectionParameters))
                {
                    fileTransfer.Connect();
                    fileTransfer.SetBinaryMode();
                    fileTransfer.RemoteDir = destinationDirectory;
                    fileTransfer.Upload(pathOfFileToBeTransfered);
                }

                return;
            }
            finally
            {
                if (fileTransfer != null && fileTransfer.IsConnected)
                    fileTransfer.Disconnect();

                fileTransfer = null;
            }
        }

        /// <summary>
        /// Rename a file to destinationDirectory of Exact targets Sftp location
        /// </summary>
        /// <param name="pathOfFileToBeTransfered"></param>
        /// <param name="address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="destinationDirectory"></param>
        public void RenameFile(string address, string username, string password, string destinationDirectory)
        {
            Sftp fileTransfer = null;

            try
            {
                SshParameters connectionParameters = new SshParameters(address, username, password);
                using (fileTransfer = new Sftp(connectionParameters))
                {
                    fileTransfer.Connect();
                    fileTransfer.SetBinaryMode();
                    //fileTransfer.RenameFile("/export/inputMMOFile/MMOSUBUNSUB_20141117172762_2.tsv.aes", "/export/ArchivedMmoFiles/MMOSUBUNSUB_20141117172762_2.tsv.aes");
                    //fileTransfer.RenameFile(@"export\ArchivedYmrFiles\8cfbcf01-26bc-4088-83f9-7ab2ed41d9c0_YMRSUBUNSUB_20150306163315.tsv.aes", @"export\YmrFiles\YMRSUBUNSUB_20150306163315.tsv.aes");
                    //fileTransfer.DeleteFile("/export/inputMMOFile/MMOSUBUNSUB_20141117172761_2.tsv.aes");

                    if (fileTransfer.IsValidPath("/export/inputMMOFil"))
                        Console.WriteLine("File is available");
                }

                return;
            }
            catch (SshException sshException) { if (!sshException.Message.ToLower().Contains("not found")) throw; }
            finally
            {
                if (fileTransfer != null && fileTransfer.IsConnected)
                    fileTransfer.Disconnect();

                fileTransfer = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataTable GetMsiTenants(string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand("GetMSITenants", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                if (command != null)
                    command.Dispose();
            }
            return dataTable;
        }

        /// <summary>
        /// Creates folder with neccessary permissions
        /// </summary>
        /// <param name="folderPath"></param>
        public void CreateFolderWithNeccessaryPermissions(string folderPath, Dictionary<string, FileSystemRights> userPreferences, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType accessControlType)
        {
            var directorySecurity = new DirectorySecurity();
            directorySecurity.SetAccessRuleProtection(true, false);

            // Add specific permissions
            foreach (var userIdentity in userPreferences.Keys)
                directorySecurity.AddAccessRule(new FileSystemAccessRule(userIdentity, userPreferences[userIdentity], inheritanceFlags, propagationFlags, accessControlType));

            Directory.CreateDirectory(folderPath).SetAccessControl(directorySecurity);
        }

        /// <summary>
        /// GetChecksum by file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        /// <summary>
        /// GetChecksumBuffered by filestream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static string GetChecksumBuffered(Stream stream)
        {
            using (var bufferedStream = new BufferedStream(stream, 1024 * 32))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(bufferedStream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        /// <summary>
        /// This method decrypts a file
        /// </summary>
        /// <param name="request">Actual Request</param>        
        /// <param name="destinationFile">file path to where we are placing the decrypted file</param>
        public void DecryptFile(string sourceFile, string destinationFile, string decryptionKey, int maximumRetryAttempts = 1)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException("Input file is not valid");

            if (string.IsNullOrWhiteSpace(decryptionKey))
                throw new ArgumentNullException("DecryptionKey is not valid");

            try
            {
                using (FileStream fsCrypt = new FileStream(destinationFile, FileMode.Create))
                {
                    using (RijndaelManaged rmCrypto = new RijndaelManaged())
                    {
                        rmCrypto.Mode = CipherMode.CBC;
                        using (PasswordDeriveBytes secretKey = new PasswordDeriveBytes(decryptionKey, Encoding.ASCII.GetBytes(decryptionKey.Length.ToString())))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, rmCrypto.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)), CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(sourceFile, FileMode.OpenOrCreate))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                        cs.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This method encrypts a file
        /// </summary>
        /// <param name="encryptionKey">Key used to encrypt the file</param>
        /// <param name="sourceFileName">File name to be encrypted</param>
        /// <param name="targetFileName">Target file name to be created with encryption</param>
        /// <param name="deleteSourceFile">Boolean flag to delete Source file</param>
        public void EncryptFile(string sourceFileName, string targetFileName, string encryptionKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFileName))
                throw new ArgumentNullException(sourceFileName);

            if (string.IsNullOrWhiteSpace(targetFileName))
                throw new ArgumentNullException(targetFileName);

            using (FileStream fileStreamCrypt = new FileStream(targetFileName, FileMode.Create))
            {
                using (RijndaelManaged rijndaelManagedCrypto = new RijndaelManaged())
                {
                    rijndaelManagedCrypto.Mode = CipherMode.CBC;
                    using (PasswordDeriveBytes secretKey = new PasswordDeriveBytes(encryptionKey, Encoding.ASCII.GetBytes(encryptionKey.Length.ToString(CultureInfo.InvariantCulture))))
                    {
                        using (CryptoStream cs = new CryptoStream(fileStreamCrypt, rijndaelManagedCrypto.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)), CryptoStreamMode.Write))
                        {
                            using (FileStream fileStreamIn = new FileStream(sourceFileName, FileMode.Open))
                            {
                                int data;
                                while ((data = fileStreamIn.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }

                                fileStreamIn.Flush();
                                cs.FlushFinalBlock();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Serializes a live object as string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Serialize<T>(T data)
        {
            if (null == data) return string.Empty;

            MemoryStream mstream = null;
            try
            {
                mstream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(data.GetType());
                xs.Serialize(mstream, data);
                mstream.Flush();
                mstream.Position = 0;
                StreamReader sr = new StreamReader(mstream);
                return sr.ReadToEnd();
            }
            finally
            {
                if (mstream != null) mstream.Close();
            }
        }

        /// <summary>
        /// Serializes a live object as string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Serialize001(object data)
        {
            if (null == data) return string.Empty;

            MemoryStream mstream = null;
            try
            {
                mstream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(data.GetType());
                xs.Serialize(mstream, data);
                mstream.Flush();
                mstream.Position = 0;
                StreamReader sr = new StreamReader(mstream);
                return sr.ReadToEnd();
            }
            finally
            {
                if (mstream != null) mstream.Close();
            }
        }

        /// <summary>
        /// Deserializes an xml content as live object
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <param name="objectType"></param>
        public T Deserialize<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return default(T);
            XmlSerializer serializer = null;
            StreamWriter writer = null;
            Stream inStream = null;
            try
            {
                serializer = new XmlSerializer(typeof(T));
                inStream = new MemoryStream();
                writer = new StreamWriter(inStream);
                writer.Write(value);
                writer.Flush();//force write
                inStream.Position = 0;
                return (T)serializer.Deserialize(inStream);
            }
            finally
            {
                if (writer != null) writer.Dispose();
                if (inStream != null) inStream.Dispose();
            }
        }

        /// <summary>
        /// Deserializes an object to string of characters using the DataContractSerializer
        /// </summary>
        /// <typeparam name="T">The output type of the deserialized object</typeparam>
        /// <param name="inputXml">The serialized Xml string of the object</param>
        /// <returns>Deserialized object of the specified generic type</returns>
        public T DataContractDeserialize<T>(string inputXml)
        {
            object retValue = null;
            StringReader strReader = null;
            XmlTextReader reader = null;
            try
            {
                if (!string.IsNullOrEmpty(inputXml))
                {
                    strReader = new StringReader(inputXml);
                    reader = new XmlTextReader(strReader);

                    if (reader.IsStartElement() && reader.IsEmptyElement)
                    {
                        return default(T);
                    }

                    DataContractSerializer dc = new DataContractSerializer(typeof(T), null, Int32.MaxValue, false, false, null);
                    retValue = dc.ReadObject(reader);
                }

                return (T)retValue;
            }
            finally
            {
                if (strReader != null) strReader.Close();
                if (reader != null) reader.Close();
            }
        }

        /// <summary>
        /// Decrypts the encrypted content with secrets certificate
        /// </summary>
        /// <param name="encryptedBlob"></param>
        /// <param name="certificateThumbprint"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedBlob, string certificateThumbprint)
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
            X509Certificate2 Certificate = certificateCollection[0];

            byte[] cipherbytes = Convert.FromBase64String(encryptedBlob);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)Certificate.PrivateKey;
            byte[] plainbytes = rsa.Decrypt(cipherbytes, false);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(plainbytes);
        }

        /// <summary>
        /// Encrypts the plain text with secrets certificate
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="certificateThumbprint"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string certificateThumbprint)
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
            X509Certificate2 Certificate = certificateCollection[0];

            byte[] bytes = Encoding.ASCII.GetBytes(plainText);
            RSACryptoServiceProvider rSACryptoServiceProvider = (RSACryptoServiceProvider)Certificate.PublicKey.Key;
            return Convert.ToBase64String(rSACryptoServiceProvider.Encrypt(bytes, false));
        }

        /// <summary>
        /// Creates the connection to queue in Azure account
        /// </summary>
        /// <param name="azureQueueConnectionString"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public bool CreateConnection(string azureQueueConnectionString, string queueName, bool isCreateIfNotExists, out CloudQueue queue)
        {
            bool isConnected = false;
            CloudStorageAccount accountInfo = null;
            CloudQueueClient queueClient = null;
            queue = null;
            try
            {
                //Create service client for credentialed access to the Queue service.
                accountInfo = CloudStorageAccount.Parse(azureQueueConnectionString);

                //Create an instance to azure queues based on the account.
                queueClient = accountInfo.CreateCloudQueueClient();

                //Get a reference to a queue in this storage account.
                queue = queueClient.GetQueueReference(queueName);

                if (isCreateIfNotExists)
                {
                    //Create the queue if it does not already exist.
                    queue.CreateIfNotExist();
                }

                isConnected = true;
            }
            catch (Exception ex)
            {
                isConnected = false;
                Console.WriteLine("Error-CreateConnection(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
            }

            return isConnected;
        }

        /// <summary>
        /// Gets max of 32 messages and makes them invisible for 60 minutes
        /// </summary>
        /// <param name="sourceQueue"></param>
        /// <param name="targetQueue"></param>
        /// <param name="maxMessages"></param>
        /// <param name="invisibleTimeout"></param>
        /// <param name="totalPassedItems"></param>
        /// <param name="totalFailedItems"></param>
        /// <returns></returns>
        public bool MoveMessages(CloudQueue sourceQueue, CloudQueue targetQueue, int maxMessages, TimeSpan invisibleTimeout, out int totalPassedItems, out int totalFailedItems)
        {
            bool isCopySuccess = false;
            Console.WriteLine("# of messages in source queue: #" + sourceQueue.RetrieveApproximateMessageCount());
            Console.WriteLine("# of messages in target queue: #" + targetQueue.RetrieveApproximateMessageCount());
            totalPassedItems = 0;
            totalFailedItems = 0;
            while (sourceQueue.ApproximateMessageCount > 0)
            {
                int passedItems = 0;
                int failedItems = 0;
                try
                {
                    var messages = sourceQueue.GetMessages(maxMessages, invisibleTimeout);
                    if (messages != null)
                    {
                        foreach (var message in messages)
                        {
                            try
                            {
                                Console.Write("\r Passed: #" + totalPassedItems.ToString("0000") + " Failed: #" + totalFailedItems.ToString("0000"));
                                targetQueue.AddMessage(message);
                                sourceQueue.DeleteMessage(message.Id, message.PopReceipt);
                                totalPassedItems += 1;
                                passedItems += 1;
                            }
                            catch (Exception innerEx)
                            {
                                failedItems += 1;
                                totalFailedItems += 1;
                                Console.WriteLine("Error-AddMessage(): " + innerEx.Message.Replace("\r", " ").Replace("\n", " "));
                            }
                        }
                    }
                    isCopySuccess = true;
                }
                catch (Exception ex)
                {
                    isCopySuccess = false;
                    Console.WriteLine("Error-GetMessages(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
                }
            }

            return isCopySuccess;
        }

        /// <summary>
        /// Gets max of 32 messages and makes them invisible for 60 minutes
        /// </summary>
        /// <param name="sourceQueue"></param>
        /// <param name="targetQueue"></param>
        /// <param name="maxMessages"></param>
        /// <param name="invisibleTimeout"></param>
        /// <param name="totalPassedItems"></param>
        /// <param name="totalFailedItems"></param>
        /// <returns></returns>
        public bool PeekMessages(CloudQueue sourceQueue, CloudQueue targetQueue, int maxMessages, TimeSpan invisibleTimeout, out int totalPassedItems, out int totalFailedItems)
        {
            bool isCopySuccess = false;
            Console.WriteLine("# of messages in source queue: #" + sourceQueue.RetrieveApproximateMessageCount());
            Console.WriteLine("# of messages in target queue: #" + targetQueue.RetrieveApproximateMessageCount());
            totalPassedItems = 0;
            totalFailedItems = 0;
            while (sourceQueue.ApproximateMessageCount > 0)
            {
                int passedItems = 0;
                int failedItems = 0;
                try
                {
                    var messages = sourceQueue.PeekMessages(maxMessages);
                    if (messages != null)
                    {
                        foreach (var message in messages)
                        {
                            try
                            {
                                Console.Write("\r Passed: #" + totalPassedItems.ToString("0000") + " Failed: #" + totalFailedItems.ToString("0000"));
                                targetQueue.AddMessage(message);
                                sourceQueue.DeleteMessage(message.Id, message.PopReceipt);
                                totalPassedItems += 1;
                                passedItems += 1;
                            }
                            catch (Exception innerEx)
                            {
                                failedItems += 1;
                                totalFailedItems += 1;
                                Console.WriteLine("Error-AddMessage(): " + innerEx.Message.Replace("\r", " ").Replace("\n", " "));
                            }
                        }
                    }
                    isCopySuccess = true;
                }
                catch (Exception ex)
                {
                    isCopySuccess = false;
                    Console.WriteLine("Error-GetMessages(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
                }
            }

            return isCopySuccess;
        }

        /// <summary>
        /// Gets the total number of messages in queue in configured intervals
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="sleepInMinutes"></param>
        public void MonitorQueue(CloudQueue queue, int sleepInMinutes)
        {
            Console.WriteLine("Monitoring the queue, '" + queue.Name + "'. It will get #msg from queue for every '" + sleepInMinutes + "' minute(s)");

            var currentMessagesInQueue = -1;
            var totalMessagesInQueue = 0;
            while (true)
            {
                try
                {
                    totalMessagesInQueue = queue.RetrieveApproximateMessageCount();
                    if (totalMessagesInQueue != currentMessagesInQueue)
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | # of messages in queue: #" + totalMessagesInQueue);
                        currentMessagesInQueue = totalMessagesInQueue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error-MonitorQueue(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
                }

                Thread.Sleep(sleepInMinutes * 60 * 1000);
            }
        }

        /// <summary>
        /// Gets the total number of messages in queue in configured intervals
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="sleepInMinutes"></param>
        public void EnqueueMessage(CloudQueue queue, int messageId, string name)
        {
            Console.WriteLine("Monitoring the queue, '" + queue.Name + "'. It will add #msg to queue");

            try
            {
                string jsonContent = DataContractSerialize(new QueueMessage { Id = messageId, Name = name, });
                queue.AddMessage(new CloudQueueMessage(jsonContent));
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | # of messages in queue: #" + messageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Enqueue(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
            }
        }

        /// <summary>
        /// Method for updating the queue message
        /// </summary>
        /// <param name="originalCloudMessages">Message collection from where one message to be updated.</param>
        /// <param name="message">message that needs to be updated.</param>
        public void UpdateQueueMessage(CloudQueue queue, CloudQueueMessage originalCloudMessage)
        {
            var modifiedCloudMessage = originalCloudMessage.AsString.Replace("Ashok", "Vinoth");
            var json = DataContractSerialize(modifiedCloudMessage);
            originalCloudMessage.SetMessageContent(json);

            queue.UpdateMessage(originalCloudMessage, TimeSpan.FromSeconds(120), MessageUpdateFields.Content | MessageUpdateFields.Visibility);
        }

        /// <summary>
        /// Gets the total number of messages in queue in configured intervals
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="sleepInMinutes"></param>
        public void Enqueue(CloudQueue queue)
        {
            Console.WriteLine("Monitoring the queue, '" + queue.Name + "'. It will add #msg to queue");

            var qMessageCounter = 0;
            while (qMessageCounter < 5)
            {
                try
                {
                    string jsonContent = DataContractSerialize(new QueueMessage { Id = qMessageCounter, Name = "Message: '" + qMessageCounter + "'" });
                    queue.AddMessage(new CloudQueueMessage(jsonContent));
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | # of messages in queue: #" + qMessageCounter);
                    qMessageCounter += 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error-Enqueue(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
                }
            }
        }

        public void DeleteQueueMessages(CloudQueue queue)
        {
            //Clear any existing messages from the queue.
            queue.Clear();

            //Create some new messages and add them to the queue.
            for (int i = 0; i < 10; i++)
            {
                CloudQueueMessage msg = new CloudQueueMessage("message" + (i < 5 ? "0" : i.ToString()));
                queue.AddMessage(msg);
                Console.WriteLine("Adding message with content: " + msg.AsString);
            }

            //Delete five messages from the queue.
            foreach (CloudQueueMessage msg in queue.GetMessages(5))
            {
                queue.DeleteMessage(msg);
                Console.WriteLine("Deleting message: " + msg.Id);
            }

            Console.WriteLine();

            //Delete five more messages from the queue.
            foreach (CloudQueueMessage msg in queue.GetMessages(5))
            {
                queue.DeleteMessage(msg.Id, msg.PopReceipt);
                Console.WriteLine("Deleting message " + msg.Id);
            }
        }

        /// <summary>
        /// Gets the total number of messages in queue in configured intervals
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="sleepInMinutes"></param>
        public IEnumerable<CloudQueueMessage> Dequeue(CloudQueue queue)
        {
            Console.WriteLine("Dequeuing the queue, '" + queue.Name + "'. It will get #msg from queue");
            IEnumerable<CloudQueueMessage> originalMessages = null;
            var totalMessagesInQueue = queue.RetrieveApproximateMessageCount();
            try
            {
                totalMessagesInQueue = queue.RetrieveApproximateMessageCount();
                originalMessages = queue.GetMessages(1, TimeSpan.FromSeconds(60));
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | # of messages in queue: #" + totalMessagesInQueue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Dequeue(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
            }

            return originalMessages;
        }

        /// <summary>
        /// Deserializes an object to string of characters using the DataContractSerializer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataContractSerialize(object data)
        {
            if (null == data)
                throw new ArgumentNullException("data");
            using (MemoryStream mstream = new MemoryStream())
            {
                DataContractSerializer dcs = new DataContractSerializer(data.GetType());
                dcs.WriteObject(mstream, data);
                mstream.Flush();
                mstream.Position = 0;
                StreamReader sr = new StreamReader(mstream);
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Gets the total number of blobs in blob container in configured intervals
        /// </summary>
        /// <param name="cloudblobcontainer"></param>
        /// <param name="sleepInMinutes"></param>
        public void MonitorBlob(Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer cloudblobcontainer, int sleepInMinutes)
        {
            Console.WriteLine("Monitoring the blob container, '" + cloudblobcontainer.Name + "'. It will get #blobs from blob container for every '" + sleepInMinutes + "' minute(s)");

            var currentBlobsInBlobContainer = -1;
            var totalBlobsInBlobContainer = 0;
            while (true)
            {
                try
                {
                    totalBlobsInBlobContainer = cloudblobcontainer.ListBlobs().Count();
                    if (totalBlobsInBlobContainer != currentBlobsInBlobContainer)
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | # of blobs in blob container: #" + totalBlobsInBlobContainer);
                        currentBlobsInBlobContainer = totalBlobsInBlobContainer;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error-MonitorBlob(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
                }

                Thread.Sleep(sleepInMinutes * 60 * 1000);
            }
        }

        /// <summary>
        /// Get <c>CloudBlobClient</c> object after configuring <c>CloudStorageAccount</c>
        /// <param name="buffersize">Set the buffer size (Size of chunck sent per call</param>
        /// </summary>
        /// <returns>Returns the <c>CloudBlobContainer</c></returns>
        private Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer GetCloudBlobContainer(string storageAcConnectionString, string blobContainerName, double expBackoffTime, int maxRetryAttempts, int maxExecutionTime, int parallelOperationThreadCount, int buffersize = 1024 * 1024)
        {
            Microsoft.WindowsAzure.Storage.CloudStorageAccount cloudStorageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(storageAcConnectionString);
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            ExponentialRetry retrypolicy = new ExponentialRetry(TimeSpan.FromSeconds(expBackoffTime), maxRetryAttempts);

            // Set the buffer size (Size of chuncks sent per call)
            cloudBlobClient.SingleBlobUploadThresholdInBytes = buffersize;
            cloudBlobClient.RetryPolicy = retrypolicy;
            cloudBlobClient.MaximumExecutionTime = TimeSpan.FromMinutes(maxExecutionTime);
            cloudBlobClient.ParallelOperationThreadCount = parallelOperationThreadCount;

            // Get a reference to a container, which may or may not exist.
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainerName);

            // Create a new container, if it does not exist
            cloudBlobContainer.CreateIfNotExists();

            return cloudBlobContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageAcConnectionString"></param>
        /// <param name="blobContainerName"></param>
        /// <returns></returns>
        private CloudBlobContainer GetCloudBlobContainer(string storageAcConnectionString, string blobContainerName)
        {
            var storageAccount = CloudStorageAccount.Parse(storageAcConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(blobContainerName);

            return blobContainer;
        }

        /// <summary>
        /// Uploads the file to fileuploadstore container as a blob
        /// </summary>
        /// <param name="filePath">The file to be moved as a blob to fileuploadstore container</param>
        /// <param name="destinationFileName">The name with which the blob has to be uploaded</param>
        /// <returns>Returns if the file was uploaded to blob successfully or not</returns>
        public bool BackupBlobContents(Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer cloudblobcontainer, string storageAcConnectionString, string blobContainerName, string sourceBlobName, string destinationBlobName, double expBackoffTime, int maxRetryAttempts, int maxExecutionTime, int parallelOperationThreadCount, int buffersize = 1024 * 1024)
        {
            try
            {
                var srcCloudBlob = cloudblobcontainer.GetBlockBlobReference(sourceBlobName);
                var destCloudBlob = cloudblobcontainer.GetBlockBlobReference(destinationBlobName);
                cloudblobcontainer.CreateIfNotExists();

                //Copy all contents from source blob to destination blob..
                destCloudBlob.StartCopyFromBlob(srcCloudBlob);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-MonitorQueue(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Transfer content from blob to SFTP server
        /// </summary>
        /// <param name="blobContainer"></param>
        /// <param name="blobName"></param>
        /// <param name="sftpServer"></param>
        /// <param name="sftpUser"></param>
        /// <param name="sftpPassword"></param>
        /// <param name="licenseKey"></param>
        /// <param name="ftpDirectory"></param>
        /// <returns></returns>
        private bool TransferBlobToSFTP(CloudBlobContainer blobContainer, string blobName, string sftpServer, string sftpUser, string sftpPassword, string licenseKey, string ftpDirectory)
        {
            Console.WriteLine("Transfer a blob to sftp started..");

            var blobToUpload = blobContainer.GetBlockBlobReference(blobName);

            #region DEPRECATED
            /*byte[] mfileBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                blobToUpload.DownloadToStream(ms);
                mfileBytes = ms.ToArray();
            }*/
            #endregion

            TimeSpan ts = new TimeSpan(10, 30, 0);
            blobToUpload.ServiceClient.Timeout = ts;
            byte[] fileBytes = blobToUpload.DownloadByteArray();

            UploadBytesToSftp(sftpServer, sftpUser, sftpPassword, licenseKey, ftpDirectory, fileBytes, blobName);

            Console.WriteLine("Transfer a blob to sftp successful..");

            return true;
        }

        /// <summary>
        ///  Uploads bytes to SFTP server
        /// </summary>
        /// <param name="sftpServer"></param>
        /// <param name="sftpUser"></param>
        /// <param name="sftpPassword"></param>
        /// <param name="licenseKey"></param>
        /// <param name="ftpDirectory"></param>
        /// <param name="blobContent"></param>
        /// <param name="blobName"></param>
        public void UploadBytesToSftp(string sftpServer, string sftpUser, string sftpPassword, string licenseKey, string ftpDirectory, byte[] blobContent, string blobName)
        {
            Jscape.Ssh.SshParameters connectionParameters = new Jscape.Ssh.SshParameters(sftpServer, sftpUser, sftpPassword);
            Sftp fileTransfer = new Sftp(connectionParameters);
            fileTransfer.LicenseKey = licenseKey;

            fileTransfer.Connect();
            fileTransfer.SetBinaryMode();
            fileTransfer.RemoteDir = ftpDirectory;
            fileTransfer.Timeout = 1;
            fileTransfer.Upload(blobContent, blobName);
        }
    }

    public class Communication
    {
        public string HideFromProfileCenter;
        public string CommunicationTitle;
        public string CommunicationDescription;
        public string CountryCode;
        public string LCID;
    }

    [DataContract]
    public class QueueMessage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string numberOfAttempts { get; set; }
    }

    [XmlRoot("TriggeredRequest")]
    public class TriggeredRequest
    {
        [XmlAttribute(AttributeName = "Type")]
        public string type { get; set; }

        [XmlAttribute(AttributeName = "ApplicationName")]
        public string applicationName { get; set; }

        [XmlAttribute(AttributeName = "EmailAddress")]
        public string emailAddress { get; set; }

        [XmlAttribute(AttributeName = "CommunicationId")]
        public int communicationId { get; set; }

        [XmlAttribute(AttributeName = "CustomerKey")]
        public string customerKey { get; set; }

        [XmlArray(ElementName = "CustomDecors", IsNullable = true)]
        [XmlArrayItem(ElementName = "CustomDecor", IsNullable = true)]
        public List<CustomDecor> customDecors { get; set; }
    }

    public class CustomDecor
    {
        [XmlAttribute(AttributeName="Name")]
        public string name { get; set; }

        [XmlAttribute(AttributeName = "Value")]
        public string value { get; set; }
    }

    [XmlRoot("InterchangeFileRequest")]
    public class InterchangeFileRequest
    {
        [XmlIgnore]
        private int _contentId { get; set; }

        [XmlIgnore]
        public string _sourceFileRelativePath { get; set; }

        [XmlAttribute(AttributeName = "SourceFileRelativePath")]
        public string sourceFileRelativePath { get { return _sourceFileRelativePath; } set { _sourceFileRelativePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + value; } }

        [XmlAttribute(AttributeName = "BatchId")]
        public string batchId { get; set; }

        [XmlAttribute(AttributeName = "BulkSendId")]
        public string bulkSendId { get; set; }

        [XmlAttribute(AttributeName = "ContentId")]
        public int contentId { get { return _contentId; } set { _contentId = value > 0 ? value : 999; } }

        [XmlAttribute(AttributeName = "TenantAccountId")]
        public string tenantAccountId { get; set; }

        [XmlAttribute(AttributeName = "BulkSendEmailType")]
        public string bulkSendEmailType { get; set; } 

        [XmlAttribute(AttributeName = "IsSendInvoke")]
        public bool isSendInvoke { get; set; }

        [XmlAttribute(AttributeName = "IsDynamicDataExtension")]
        public bool isDynamicDataExtension { get; set; }

        [XmlAttribute(AttributeName = "IsOverrideConfiguration")]
        public bool isOverrideConfiguration { get; set; }

        [XmlAttribute(AttributeName = "DataImportType")]
        public string dataImportType { get; set; }

        [XmlAttribute(AttributeName = "DynamicDataExtensionTemplateName")]
        public string dynamicDataExtensionTemplateName { get; set; }
    }

    [XmlRoot("ApplicationDriverSettings")]
    public class ApplicationDriverSettings
    {
        [XmlArray(ElementName = "SmokeTestRepository", IsNullable = false)]
        [XmlArrayItem(ElementName = "SmokeTest", IsNullable = false)]
        public List<SmokeTest> smokeTests = new List<SmokeTest>();

        [XmlArray(ElementName = "EmailProviders", IsNullable = false)]
        [XmlArrayItem(ElementName = "EmailProvider", IsNullable = false)]
        public List<EmailProvider> emailProviders = new List<EmailProvider>();
    }

    public class EmailProvider
    {
        [XmlAttribute(AttributeName="Name")]
        public string name { get; set; }

        [XmlElement(ElementName = "WebService")]
        public WebService webService { get; set; }

    }

    public class WebService
    {
        [XmlAttribute(AttributeName = "WsUri")]
        public string wsUri {get; set;}

        [XmlAttribute(AttributeName = "WsUsername")]
        public string wsUsername {get; set;}

        [XmlAttribute(AttributeName = "WsPassword")]
        public string wsPassword {get; set;}

        [XmlAttribute(AttributeName = "WsBusinessUnitAccountId")]
        public string wsBusinessUnitAccountId {get; set;}

        [XmlAttribute(AttributeName = "WsServerSideCertificateSubjectName")]
        public string wsServerSideCertificateSubjectName {get; set;}

        [XmlAttribute(AttributeName = "WsServerSideCertificateStoreName")]
        public string wsServerSideCertificateStoreName {get; set;}

        [XmlAttribute(AttributeName = "WsServerSideCertificateStoreLocation")]
        public string wsServerSideCertificateStoreLocation {get; set;}

        [XmlAttribute(AttributeName = "WsServerSideCertificateFindBy")]
        public string wsServerSideCertificateFindBy {get; set;}

        [XmlAttribute(AttributeName = "WsClientSideCertificateSubjectName")]
        public string wsClientSideCertificateSubjectName {get; set;}

        [XmlAttribute(AttributeName = "WsClientSideCertificateStoreName")]
        public string wsClientSideCertificateStoreName {get; set;}

        [XmlAttribute(AttributeName = "WsClientSideCertificateStoreLocation")]
        public string wsClientSideCertificateStoreLocation {get; set;}

        [XmlAttribute(AttributeName = "WsClientSideCertificateFindBy")]
        public string wsClientSideCertificateFindBy {get; set;}

        [XmlAttribute(AttributeName = "UseCertificate")]
        public string useCertificate {get; set;}
    }

    public class SmokeTest
    {
        #region PUBILC_PROPERTIES
        [XmlIgnore]
        private string _name;
        [XmlIgnore]
        private string _triggeredBy;
        [XmlIgnore]
        private string _requestType;
        [XmlIgnore]
        private string _requestDataFilePath;
        [XmlIgnore]
        private string _salesforceFtoTemplateFilePath;
        [XmlIgnore]
        private string _targetDropLocation;
        [XmlIgnore]
        private bool _canSkipEmailInterchangeApplication;
        [XmlIgnore]
        private bool _isEnabled;
        #endregion

        #region PUBILC_PROPERTIES
        [XmlAttribute(AttributeName = "Name")]
        public string name { get { return _name; } set { _name = value; } }

        [XmlAttribute(AttributeName = "TriggeredBy")]
        public string triggeredBy { get { return _triggeredBy; } set { _triggeredBy = value; } }

        [XmlAttribute(AttributeName = "RequestType")]
        public string requestType { get { return _requestType; } set { _requestType = value; } }

        [XmlAttribute(AttributeName = "RequestDataFileRelativePath")]
        public string requestDataFilePath { get { return _requestDataFilePath; } set { _requestDataFilePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + value; } }

        [XmlAttribute(AttributeName = "SalesforceFtoTemplateFileRelativePath")]
        public string salesforceFtoTemplateFilePath { get { return _salesforceFtoTemplateFilePath; } set { _salesforceFtoTemplateFilePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + value; } }

        [XmlAttribute(AttributeName = "TargetDropLocation")]
        public string targetDropLocation { get { return _targetDropLocation; } set { _targetDropLocation = value; } }

        [XmlAttribute(AttributeName = "CanSkipEmailInterchangeApplication")]
        public bool canSkipEmailInterchangeApplication { get { return _canSkipEmailInterchangeApplication; } set { _canSkipEmailInterchangeApplication = value; } }

        [XmlAttribute(AttributeName = "IsEnabled")]
        public bool isEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        #endregion
    }
}
