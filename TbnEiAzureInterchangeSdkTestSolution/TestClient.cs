using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureTBNClientSDK = Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.AzureServiceReference;
using System.IO;
using System.Threading;

namespace Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.TestClient
{
    public class TestClient
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        public static void Main()
        {
            /*
             * TBN(Generic)
             */
            /*Console.WriteLine(Create_TBN_Generic_UNITTEST_3("v-kukhus@microsoft.com", "Khushboo", "", "CustomData001"));
            Console.WriteLine(Create_TBN_Generic_UNITTEST_3("v-assiva@microsoft.com", "Ashok", "Sivapragasam", "CustomData002"));
            Console.WriteLine(Create_TBN_Generic_UNITTEST_3("v-savole@microsoft.com", "Satish", "Voleti", "CustomData003"));*/

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
             * Get tracking status of emails by AccountId, SubscriberKey & TrackingField
             */
            /*
            try { Console.WriteLine(" GetEmailTrackingTriggerSendSummaryData() -- " + GetEmailTrackingTriggerSendSummaryData()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingTriggerSendSummaryData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
            try { Console.WriteLine(" GetEmailTrackingTriggerSendSummaryDataV2() -- " + GetEmailTrackingTriggerSendSummaryDataV2()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingTriggerSendSummaryDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

            Console.WriteLine("Press any key to close this window");
            Console.ReadLine();
        }

        #region EMAILTRACKING_METHODS
        private static string GetEmailTrackingTriggerSendSummaryData()
        {
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            EmailTrackingOutputOfTriggerSendSummaryDetails triggerSummary;

            //Construct Email Tracking Trigger send Summary input object.
            EmailTrackingInputOfTriggerSendSummaryInput inputSummary = new EmailTrackingInputOfTriggerSendSummaryInput
            {
                EmailTrackingCriteria = new TriggerSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = 10460681,
                    //Assign Trigger Send Customer Key.
                    TriggerSendCustomerKey = "<<Your_Triggeredsend_Defn_External_Key>>"
                }
            };

            //Retrieve call.
            triggerSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfTriggerSendSummaryInput, EmailTrackingOutputOfTriggerSendSummaryDetails>(inputSummary);

            return StringResources.EMAIL_TRACKING_TRIGGER_SEND_SUMMARY_STATUS;
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
                    ClientId = 10460681,
                    //Assign Trigger Send Customer Key.
                    TriggerSendCustomerKey = "<<Your_Triggeredsend_Defn_External_Key>>"
                }
            };

            //Retrieve call.
            triggerSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfTriggerSendSummaryInput, EmailTrackingOutputOfTriggerSendSummaryDetails>(inputSummary, out requestID);

            return StringResources.EMAIL_TRACKING_TRIGGER_SEND_SUMMARY_STATUS + "EI ID: " + requestID.ToString();
        }
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
            TriggeredRequestBase newRequest = Create_TBN_TAGM_Request_Packet(emailAddress);
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

        private static string Create_TBN_Generic_UNITTEST_3(string emailAddress, string firstName, string lastName, string customData)
        {
            TriggeredRequestBase newRequest = Create_TBN_Generic_Request_PacketV2(emailAddress, firstName, lastName, customData);
            return CallSend(newRequest);
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

            newSubscriber.FirstName = "<<FirstName>>";
            newSubscriber.MiddleName = "<<MiddleName>>";
            newSubscriber.LastName1 = "<<LastName1>>";
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();

            newSubscriber.WizardIds = WizardIDs; //{ "00000000-0000-0000-0000-000000010018" };
            newSubscriber.QuestionIds = QuestionIDs; // { "00000000-0000-0000-0000-000000003174", "00000000-0000-0000-0000-000000003175" };
            newSubscriber.RegistrationDate = DateTime.UtcNow;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "<<FirstName>>";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName";
            attribute.Value = "<<LastName1>>";
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

            newSubscriber.FirstName = "<<FirstName>>";
            newSubscriber.MiddleName = "<<LastName>>";
            newSubscriber.LastName1 = "<<LastName1>>";
            newSubscriber.SubscriberKey = Guid.NewGuid().ToString();
            newSubscriber.RegistrationDate = DateTime.UtcNow.AddDays(5);

            //addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "<<FirstName>>";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName";
            attribute.Value = "<<LastName>>";
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
            request.CommunicationId = 126723;
            request.ConversationId = "ConversationId";//Guid.NewGuid().ToString();

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
            attribute.Value = "<<LastName>>";
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
                LastName1 = "<<LastName>>",
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
        /// <returns></returns>
        private static TriggeredRequestBase Create_TBN_Partner_Request_Packet(string emailAddress)
        {
            /* Instantiating a new Subscriber (Generic TBN)*/
            GenericSubscriber newSubscriber = new GenericSubscriber();
            //newSubscriber.EmailAddress = "am<<LastName1>>@microsoft.com";
            newSubscriber.EmailAddress = emailAddress;
            newSubscriber.SubscriberKey = System.Guid.NewGuid().ToString();
            //addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            AzureTBNClientSDK.AzureServiceReference.Attribute attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();

            attribute.Name = "FirstName";
            attribute.Value = "<<FirstName>>";
            newSubscriber.Attributes[0] = attribute;

            attribute.Name = "LastName";
            attribute.Value = "<<LastName>>";
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
            attribute.Value = "<<FirstName>>";
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName";
            attribute.Value = "<<LastName>>";
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            EventRequestData newRequest = new EventRequestData();
            newRequest.ApplicationName = "World Wide Events";

            newRequest.BatchId = System.Guid.NewGuid();
            newRequest.TemplateId = "WWE_Prague_1";
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
            attribute.Value = "<<FirstName>>";
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "LastName1";
            attribute.Value = "<<LastName>>";
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
    }
}
