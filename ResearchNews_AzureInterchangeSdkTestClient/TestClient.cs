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
            try
            {
                /*
                 * MSI Subscribe version 1
                 */
                Console.WriteLine(Msi_Subscribe_v1("casey.anderson@microsoft.com", 51, 0));

                /*
                 * MSI Subscribe version 2
                 */
                Console.WriteLine(Msi_Subscribe_v2("rrogahn@exchange.microsoft.com", 51, 1));

                /*
                 * MSI Unsubscribe version 1
                 */
                Console.WriteLine(Msi_Unsubscribe_v1("casey.anderson@microsoft.com", 51, 0));

                /*
                 * MSI Unsubscribe version 2
                 */
                Console.WriteLine(Msi_Unsubscribe_v2("rrogahn@exchange.microsoft.com", 51, 1));

                /*
                 * Create TBN Generic Against EI API
                 */
                try { Console.WriteLine(" SendTriggeredWelcomeEmail() -- " + SendTriggeredWelcomeEmail("casey.anderson@microsoft.com")); }
                catch (Exception ex) { Console.WriteLine(" SendTriggeredWelcomeEmail() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

                Console.WriteLine("Press any key to close this window");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region SUBSCRIPTION_METHODS
        /// <summary>
        /// This method will subscribe an email address to a communication with preferred delivery format.
        /// </summary>
        /// <param name="emailAddress">It must be a valid email address</param>
        /// <param name="communicationId">It must be an integer. It must be created from Subscription Management Portal. Contact, eiengind@microsoft.com</param>
        /// <param name="deliveryFormatId">It must be an integer. 0 - Html email content type; 1 - Text email content type. Default will be html type</param>
        /// <returns></returns>
        private static bool Msi_Subscribe_v1(string emailAddress, int communicationId, int deliveryFormatId = 0)
        {
            #region INIT
            EmailInterchangeResult subcriptionResult = EmailInterchangeResult.None;
            #endregion

            #region INSTANTIATING
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            #endregion

            #region SEND_REQUEST_TO_EMAIL_INTERCHANGE_APPLICATION
            subcriptionResult = client.Subscribe(emailAddress, communicationId, deliveryFormatId);
            #endregion

            #region ASSERT
            return (subcriptionResult.Equals(EmailInterchangeResult.None)) ? true : false;
            #endregion
        }

        /// <summary>
        /// This method will subscribe an email address to a communication with preferred delivery format.
        /// </summary>
        /// <param name="emailAddress">It must be a valid email address</param>
        /// <param name="communicationId">It must be an integer. It must be created from Subscription Management Portal. Contact, eiengind@microsoft.com</param>
        /// <param name="deliveryFormatId">It must be an integer. 0 - Html email content type; 1 - Text email content type. Default will be html type</param>
        /// <returns></returns>
        private static bool Msi_Subscribe_v2(string emailAddress, int communicationId, int deliveryFormatId = 0)
        {
            #region INIT
            EmailInterchangeResponseToken subcriptionResult = new EmailInterchangeResponseToken();
            #endregion

            #region INSTANTIATING
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            #endregion

            #region SEND_REQUEST_TO_EMAIL_INTERCHANGE_APPLICATION
            subcriptionResult = client.SubscribeV2(emailAddress, communicationId, deliveryFormatId);
            #endregion

            #region ASSERT
            Console.WriteLine(subcriptionResult.EmailInterchangeId);
            return (subcriptionResult.Result.Equals(EmailInterchangeResult.None)) ? true : false;
            #endregion
        }

        /// <summary>
        /// This method will unsubscribe an email address to a communication with preferred delivery format.
        /// </summary>
        /// <param name="emailAddress">It must be a valid email address</param>
        /// <param name="communicationId">It must be an integer. It must be created from Subscription Management Portal. Contact, eiengind@microsoft.com</param>
        /// <param name="deliveryFormatId">It must be an integer. 0 - Html email content type; 1 - Text email content type. Default will be html type</param>
        /// <returns></returns>
        private static bool Msi_Unsubscribe_v1(string emailAddress, int communicationId, int deliveryFormatId = 0)
        {
            #region INIT
            EmailInterchangeResult subcriptionResult = EmailInterchangeResult.None;
            #endregion

            #region INSTANTIATING
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            #endregion

            #region SEND_REQUEST_TO_EMAIL_INTERCHANGE_APPLICATION
            subcriptionResult = client.Unsubscribe(emailAddress, communicationId, deliveryFormatId);
            #endregion

            #region ASSERT
            return (subcriptionResult.Equals(EmailInterchangeResult.None)) ? true : false;
            #endregion
        }

        /// <summary>
        /// This method will unsubscribe an email address to a communication with preferred delivery format.
        /// </summary>
        /// <param name="emailAddress">It must be a valid email address</param>
        /// <param name="communicationId">It must be an integer. It must be created from Subscription Management Portal. Contact, eiengind@microsoft.com</param>
        /// <param name="deliveryFormatId">It must be an integer. 0 - Html email content type; 1 - Text email content type. Default will be html type</param>
        /// <returns></returns>
        private static bool Msi_Unsubscribe_v2(string emailAddress, int communicationId, int deliveryFormatId = 0)
        {
            #region INIT
            EmailInterchangeResponseToken subcriptionResult = new EmailInterchangeResponseToken();
            #endregion

            #region INSTANTIATING
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            #endregion

            #region SEND_REQUEST_TO_EMAIL_INTERCHANGE_APPLICATION
            subcriptionResult = client.UnsubscribeV2(emailAddress, communicationId, deliveryFormatId);
            #endregion

            #region ASSERT
            Console.WriteLine(subcriptionResult.EmailInterchangeId);
            return (subcriptionResult.Result.Equals(EmailInterchangeResult.None)) ? true : false;
            #endregion
        }
        #endregion

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
            request.ApplicationName = "ResearchNews";
            request.CommunicationId = 12345;
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
    }
}
