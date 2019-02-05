

namespace Microsoft.IT.RelationshipManagement.Interchange.ExactTarget
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core;
    using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
    using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;
    using Microsoft.IT.RelationshipManagement.Interchange.Platform.Azure.Services.Common;
    using Microsoft.Web.Services3;
    using Microsoft.Web.Services3.Design;
    using Microsoft.Web.Services3.Security;
    using Microsoft.Web.Services3.Security.Tokens;
    using CoreRequest = Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core;
    using EmailInterchangeCommon = Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core;
    using requestEnum = Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
    using System.Text;
    using System.Xml.Schema;
    using System.Reflection;

    /// <summary>
    /// Contains the methods that are used to communicate (Create, Retrieve, Update) with ET.
    /// </summary>
    public static class Communicator
    {
        public const string CONSTANT_ZIP = "ZIP";
        
        #region CERTIFICATE AUTHENTICATION METHOD

        /// <summary>
        /// Creates the username for certificate with client signature cert proxy.
        /// </summary>
        /// <returns></returns>
        private static WebServicesClientProtocol ApplyCertificateAuthentication(InterchangeConfiguration configuration, InterchangeUser user)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            //Username Token
            UsernameTokenProvider clientTokenProvider = new UsernameTokenProvider(user.ExactTargetUsername, user.ExactTargetPassword);

            //Service Token (X509 Cert)
            //This the ExactTarget public key that will need to be retrieved and stored locally.
            //Change these settings to match your environment.
            X509TokenProvider serviceTokenProvider =
               new X509TokenProvider((StoreLocation)Enum.Parse(typeof(StoreLocation), configuration.ExactTargetStoreLocation),
                                     (StoreName)Enum.Parse(typeof(StoreName), configuration.ExactTargetStoreName),
                //configuration.ExactTargetCertificateName,
                                     user.ExactTargetCertificateName,
                                     (X509FindType)Enum.Parse(typeof(X509FindType), configuration.ExactTargetX509FindType));

            //Client Signature Token (X509 Cert)
            //This is the client provided X509Cert used to sign the message in order to verify message source.
            //The ASPNET account must have read permission to this certs private key in order to sign the message.
            //Change these settings to match your environment.
            X509TokenProvider clientSignatureTokenProvider =
               new X509TokenProvider((StoreLocation)Enum.Parse(typeof(StoreLocation), configuration.EmailInterchangeStoreLocation),
                                     (StoreName)Enum.Parse(typeof(StoreName), configuration.EmailInterchangeStoreName),
                                     configuration.EmailInterchangeCertificateName,
                                     (X509FindType)Enum.Parse(typeof(X509FindType), configuration.EmailInterchangeX509FindType));

            //Security Assertion Config
            UserNameForCertificateAssertion2 usernameForCertAssertion = new UserNameForCertificateAssertion2();

            //client signature
            usernameForCertAssertion.ClientSignatureOptions = SignatureOptions.IncludeAddressing |
                                                              SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.ClientSignatureTokenProvider = clientSignatureTokenProvider;

            //request protection
            usernameForCertAssertion.Protection.Request.SignatureOptions = SignatureOptions.IncludeAddressing |
                                                                           SignatureOptions.IncludeTimestamp |
                                                                           SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.Protection.Request.EncryptBody = false;

            //response protection
            usernameForCertAssertion.Protection.Response.SignatureOptions = SignatureOptions.IncludeAddressing |
                                                                            SignatureOptions.IncludeTimestamp |
                                                                            SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.Protection.Response.EncryptBody = false;

            //fault protection
            usernameForCertAssertion.Protection.Fault.SignatureOptions = SignatureOptions.IncludeAddressing |
                                                                         SignatureOptions.IncludeTimestamp |
                                                                         SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.Protection.Fault.EncryptBody = false;

            usernameForCertAssertion.RequireDerivedKeys = true;
            // SecurityConfiguration SecurityConfig = new SecurityConfiguration();

            //Policy Config
            Policy usernameForCertificatePolicy = new Policy(usernameForCertAssertion);

            ExactTarget.PartnerAPI proxy = null;
            ExactTarget.PartnerAPI tempProxy = new ExactTarget.PartnerAPI();
            //bool succeed = false;
            try
            {
                tempProxy.SetClientCredential<UsernameToken>(clientTokenProvider.GetToken());
                tempProxy.SetServiceCredential<X509SecurityToken>(serviceTokenProvider.GetToken());
                tempProxy.SetPolicy(usernameForCertificatePolicy);
                tempProxy.Url = user.ExactTargetUrl;
                tempProxy.Timeout = 240000;
                proxy = tempProxy;
                tempProxy = null;
            }
            finally
            {
                if (tempProxy != null)
                {
                    tempProxy.Dispose();
                }
            }
            return proxy;
        }

        private static WebServicesClientProtocol ApplyCertificateAuthentication(InterchangeConfiguration configuration, ETConnectionConfig configValues)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            //Username Token
            UsernameTokenProvider clientTokenProvider = new UsernameTokenProvider(configValues.ExactTargetUserName, configValues.ExactTargetAccountPassword);

            //Service Token (X509 Cert)
            //This the ExactTarget public key that will need to be retrieved and stored locally.
            //Change these settings to match your environment.
            X509TokenProvider serviceTokenProvider =
               new X509TokenProvider((StoreLocation)Enum.Parse(typeof(StoreLocation), configuration.ExactTargetStoreLocation),
                                     (StoreName)Enum.Parse(typeof(StoreName), configuration.ExactTargetStoreName),
                //configuration.ExactTargetCertificateName,
                                     configValues.ExactTargetCertificateName,
                                     (X509FindType)Enum.Parse(typeof(X509FindType), configuration.ExactTargetX509FindType));

            //Client Signature Token (X509 Cert)
            //This is the client provided X509Cert used to sign the message in order to verify message source.
            //The ASPNET account must have read permission to this certs private key in order to sign the message.
            //Change these settings to match your environment.
            X509TokenProvider clientSignatureTokenProvider =
               new X509TokenProvider((StoreLocation)Enum.Parse(typeof(StoreLocation), configuration.EmailInterchangeStoreLocation),
                                     (StoreName)Enum.Parse(typeof(StoreName), configuration.EmailInterchangeStoreName),
                                     configuration.EmailInterchangeCertificateName,
                                     (X509FindType)Enum.Parse(typeof(X509FindType), configuration.EmailInterchangeX509FindType));

            //Security Assertion Config
            UserNameForCertificateAssertion2 usernameForCertAssertion = new UserNameForCertificateAssertion2();

            //client signature
            usernameForCertAssertion.ClientSignatureOptions = SignatureOptions.IncludeAddressing |
                                                              SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.ClientSignatureTokenProvider = clientSignatureTokenProvider;

            //request protection
            usernameForCertAssertion.Protection.Request.SignatureOptions = SignatureOptions.IncludeAddressing |
                                                                           SignatureOptions.IncludeTimestamp |
                                                                           SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.Protection.Request.EncryptBody = false;

            //response protection
            usernameForCertAssertion.Protection.Response.SignatureOptions = SignatureOptions.IncludeAddressing |
                                                                            SignatureOptions.IncludeTimestamp |
                                                                            SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.Protection.Response.EncryptBody = false;

            //fault protection
            usernameForCertAssertion.Protection.Fault.SignatureOptions = SignatureOptions.IncludeAddressing |
                                                                         SignatureOptions.IncludeTimestamp |
                                                                         SignatureOptions.IncludeSoapBody;
            usernameForCertAssertion.Protection.Fault.EncryptBody = false;

            usernameForCertAssertion.RequireDerivedKeys = true;
            // SecurityConfiguration SecurityConfig = new SecurityConfiguration();

            //Policy Config
            Policy usernameForCertificatePolicy = new Policy(usernameForCertAssertion);

            ExactTarget.PartnerAPI proxy = null;
            ExactTarget.PartnerAPI tempProxy = new ExactTarget.PartnerAPI();
            //bool succeed = false;
            try
            {
                tempProxy.SetClientCredential<UsernameToken>(clientTokenProvider.GetToken());
                tempProxy.SetServiceCredential<X509SecurityToken>(serviceTokenProvider.GetToken());
                tempProxy.SetPolicy(usernameForCertificatePolicy);
                tempProxy.Url = configValues.ExactTargetURL;
                tempProxy.Timeout = 240000;
                proxy = tempProxy;
                tempProxy = null;
            }
            finally
            {
                if (tempProxy != null)
                {
                    tempProxy.Dispose();
                }
            }
            return proxy;
        }

        #endregion

        #region TRANSFORM TO ET OBJECT METHOD
        public static APIObject TransformToExactTargetObject(IRequest requestBase, InterchangeConfiguration configuration, InterchangeUser user)
        {
            if (null == configuration)
                throw new ArgumentNullException("configuration");

            if (null == requestBase)
                throw new ArgumentNullException("triggeredRequest");

            var newRequest = requestBase as EmailInterchangeCommon.Request;
            var triggeredRequest = requestBase as TriggeredRequest;

            if (null != newRequest)
            {
                var fileTrigger = new ExactTarget.FileTrigger();
                fileTrigger.Status = EmailInterchangeCommon.Enumerators.RequestStatus.ExternalNew.ToString();
                fileTrigger.Type = Enum.GetName(typeof(EmailInterchangeCommon.Enumerators.RequestType), EmailInterchangeCommon.Enumerators.RequestType.BulkSendData);

                fileTrigger.Client = new ExactTarget.ClientID();
                var bulkSendConfiguration = DataAccess.GetBulkSendConfiguration(user.InterchangeUserId, configuration.InterchangeDBConnectionString);

                if (bulkSendConfiguration == null)
                    throw new Exception(StringResources.FILE_REQUEST_NOTIFICATION_MISSING_CONFIGURATION_DATA);

                fileTrigger.Client.ID = bulkSendConfiguration.TenantAccountId;
                fileTrigger.Client.IDSpecified = true;
                return fileTrigger;
            }

            if (null != triggeredRequest)
            {
                var triggeredSend = new TriggeredSend();

                #region TAGM_SUBSCRIBER
                var tagmTriggerSubscriber = triggeredRequest.Subscribers[0] as TagmTriggerSubscriber;
                if (null != tagmTriggerSubscriber)
                    getTriggeredSendForTagmTriggerSubscriber(triggeredRequest, configuration, triggeredSend, tagmTriggerSubscriber, user);
                #endregion

                #region LIMITED_PROGRAM_SUBSCRIBER
                var limitedProgramSubscriber = triggeredRequest.Subscribers[0] as LimitedProgramSubscriber;
                if (null != limitedProgramSubscriber)
                    getTriggeredSendForLimitedProgram(triggeredRequest, configuration, triggeredSend, limitedProgramSubscriber, user);
                #endregion

                #region GENERIC_SUBSCRIBER
                var genericSubscriber = triggeredRequest.Subscribers[0] as GenericSubscriber;
                if (null != genericSubscriber)
                    getTriggeredSendForGenericSubscriber(triggeredRequest, configuration, triggeredSend, genericSubscriber, user);
                #endregion

                #region EVENT_SUBSCRIBER
                EventSubscriber eventSubscriber = triggeredRequest.Subscribers[0] as EventSubscriber;
                if (null != eventSubscriber)
                    getTriggeredSendForEventSubscriber(triggeredRequest, configuration, triggeredSend, user);
                #endregion

                return triggeredSend;
            }

            return null;
        }

        /// <summary>
        /// Transform EI object toExact Target Request to
        /// </summary>
        /// <param name="requestBase">Request object</param>
        /// <param name="configValues">Configuration values to contact ET</param>
        /// <returns>returns TransformToExactTargetObject</returns>
        public static ExactTarget.APIObject TransformToExactTargetObject(IRequest requestBase, InterchangeConfiguration configValues)
        {
            if (configValues == null)
            {
                throw new ArgumentNullException("requestBase");
            }

            if (configValues == null)
            {
                throw new ArgumentNullException("configValues");
            }

            CoreRequest.Request newRequest = requestBase as CoreRequest.Request;
            TriggeredRequest triggeredRequest = requestBase as TriggeredRequest;

            int accountID = Convert.ToInt32(newRequest.AccountId);

            //For RequestTypes that are not Triggered Requests.
            if (newRequest != null)
            {
                int subsidiaryAccountID;

                //Setting the properties of the new FTO to be created.
                ExactTarget.FileTrigger fileTrigger = new ExactTarget.FileTrigger();
                fileTrigger.Status = RequestStatus.ExternalNew.ToString();
                fileTrigger.Type = Enum.GetName(typeof(requestEnum.RequestType), newRequest.RequestType);

                //Set the ClientID property of FTO.C:\Email Interchange\RXD_GMP_ExactTarget\Main\UnitTests\ExactTargetCommunicator.Tests\ETConnectionConfigTest.cs
                fileTrigger.Client = new ExactTarget.ClientID();

                if (newRequest.RequestType == requestEnum.RequestType.PromotionalListData)
                {
                    if (Int32.TryParse(newRequest.AccountId, out subsidiaryAccountID))
                    {
                        fileTrigger.Client.ID = subsidiaryAccountID;
                    }

                    else
                    {
                        throw new FormatException("Request.Account Id is of Invalid Format");
                    }
                }
                else
                {
                    fileTrigger.Client.ID = accountID;
                }

                fileTrigger.Client.IDSpecified = true;

                return fileTrigger;
            }

            return null;
        }

        /// <summary>
        /// It prepares the triggered send definition for TAGM Subscriber
        /// </summary>
        /// <param name="triggeredRequest"></param>
        /// <param name="configuration"></param>
        /// <param name="ts"></param>
        /// <param name="tagmSubscriber"></param>
        /// <param name="user"></param>
        private static void getTriggeredSendForTagmTriggerSubscriber(TriggeredRequest triggeredRequest, InterchangeConfiguration configuration, TriggeredSend ts, TagmTriggerSubscriber tagmSubscriber, InterchangeUser user)
        {
            // FY13.1 Requirement.
            if (!string.IsNullOrEmpty(triggeredRequest.CustomerKey))
            {
                ts.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = triggeredRequest.CustomerKey,
                    Priority = "High"
                };
            }
            else
            {
                ts.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = string.Format(CultureInfo.InvariantCulture, "TBN_{0}", triggeredRequest.CommunicationId),
                    Priority = "High"
                };
            }

            ts.Client = new ExactTarget.ClientID();
            // FY13.1 Requirement.
            ts.Client.ID = (!string.IsNullOrEmpty(triggeredRequest.AccountId) && triggeredRequest.AccountId != "0" ? Convert.ToInt32(triggeredRequest.AccountId) : Convert.ToInt32(RetrieveAccountIdForTriggeredSend(ts.TriggeredSendDefinition.CustomerKey, configuration, user)));
            ts.Client.IDSpecified = true;

            ts.Subscribers = new ExactTarget.Subscriber[1];
            ts.Subscribers[0] = new ExactTarget.Subscriber();

            ts.Subscribers[0].EmailAddress = tagmSubscriber.EmailAddress;
            ts.Subscribers[0].SubscriberKey = tagmSubscriber.SubscriberKey;

            if (tagmSubscriber.Attributes != null)
                ts.Subscribers[0].Attributes = new ExactTarget.Attribute[9 + tagmSubscriber.Attributes.Count];
            else
                ts.Subscribers[0].Attributes = new ExactTarget.Attribute[9];

            ts.Subscribers[0].Attributes[0] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[0].Name = "CommID";
            ts.Subscribers[0].Attributes[0].Value = triggeredRequest.CommunicationId.ToString(CultureInfo.InvariantCulture);

            ts.Subscribers[0].Attributes[1] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[1].Name = "FirstName";
            ts.Subscribers[0].Attributes[1].Value = tagmSubscriber.FirstName;

            ts.Subscribers[0].Attributes[2] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[2].Name = "MiddleName";
            ts.Subscribers[0].Attributes[2].Value = tagmSubscriber.MiddleName;

            ts.Subscribers[0].Attributes[3] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[3].Name = "LastName1";
            ts.Subscribers[0].Attributes[3].Value = tagmSubscriber.LastName1;

            ts.Subscribers[0].Attributes[4] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[4].Name = "LastName2";
            ts.Subscribers[0].Attributes[4].Value = tagmSubscriber.LastName2;

            ts.Subscribers[0].Attributes[5] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[5].Name = "NamePrefix";
            ts.Subscribers[0].Attributes[5].Value = tagmSubscriber.NamePrefix;

            ts.Subscribers[0].Attributes[6] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[6].Name = "NameSuffix";
            ts.Subscribers[0].Attributes[6].Value = tagmSubscriber.NameSuffix;

            ts.Subscribers[0].Attributes[7] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[7].Name = "RegistrationDate";
            ts.Subscribers[0].Attributes[7].Value = tagmSubscriber.RegistrationDate.ToString();

            ts.Subscribers[0].Attributes[8] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[8].Name = "CustomerAnswerXML";
            ts.Subscribers[0].Attributes[8].Value = tagmSubscriber.WizardData;

            if (tagmSubscriber.Attributes != null)
            {
                for (int count = 9; count < ts.Subscribers[0].Attributes.Length; count++)
                {
                    ts.Subscribers[0].Attributes[count] = new ExactTarget.Attribute();
                    ts.Subscribers[0].Attributes[count].Name = tagmSubscriber.Attributes[count - 9].Name;
                    ts.Subscribers[0].Attributes[count].Value = tagmSubscriber.Attributes[count - 9].Value;
                }
            }
        }

        /// <summary>
        /// It prepares the triggered send definition for LP Subscriber
        /// </summary>
        /// <param name="triggeredRequest"></param>
        /// <param name="configuration"></param>
        /// <param name="ts"></param>
        /// <param name="limitedSubscriber"></param>
        /// <param name="user"></param>
        private static void getTriggeredSendForLimitedProgram(TriggeredRequest triggeredRequest, InterchangeConfiguration configuration, TriggeredSend ts, LimitedProgramSubscriber limitedSubscriber, InterchangeUser user)
        {
            //CR# 570611 (of On-Premise)
            // FY13.1 Requirement.
            if (!string.IsNullOrEmpty(triggeredRequest.CustomerKey))
            {
                ts.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = triggeredRequest.CustomerKey,
                    Priority = "High"
                };
            }
            else
            {
                ts.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = string.Format(CultureInfo.InvariantCulture, "LP_{0}", triggeredRequest.CommunicationId),
                    Priority = "High"
                };
            }

            ts.Client = new ExactTarget.ClientID();
            // FY13.1 Requirement.
            ts.Client.ID = (!string.IsNullOrEmpty(triggeredRequest.AccountId) && triggeredRequest.AccountId != "0" ? Convert.ToInt32(triggeredRequest.AccountId) : Convert.ToInt32(RetrieveAccountIdForTriggeredSend(ts.TriggeredSendDefinition.CustomerKey, configuration, user)));

            ts.Client.IDSpecified = true;

            ts.Subscribers = new ExactTarget.Subscriber[1];
            ts.Subscribers[0] = new ExactTarget.Subscriber();

            ts.Subscribers[0].EmailAddress = limitedSubscriber.EmailAddress;
            ts.Subscribers[0].SubscriberKey = limitedSubscriber.SubscriberKey;
            ts.Subscribers[0].Attributes = new ExactTarget.Attribute[10 + (limitedSubscriber.Attributes != null ? limitedSubscriber.Attributes.Count : 0)];

            ts.Subscribers[0].Attributes[0] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[0].Name = "CommID";
            ts.Subscribers[0].Attributes[0].Value = triggeredRequest.CommunicationId.ToString(CultureInfo.InvariantCulture);

            ts.Subscribers[0].Attributes[1] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[1].Name = "FirstName";
            ts.Subscribers[0].Attributes[1].Value = limitedSubscriber.FirstName;

            ts.Subscribers[0].Attributes[2] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[2].Name = "MiddleName";
            ts.Subscribers[0].Attributes[2].Value = limitedSubscriber.MiddleName;

            ts.Subscribers[0].Attributes[3] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[3].Name = "LastName1";
            ts.Subscribers[0].Attributes[3].Value = limitedSubscriber.LastName1;

            ts.Subscribers[0].Attributes[4] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[4].Name = "LastName2";
            ts.Subscribers[0].Attributes[4].Value = limitedSubscriber.LastName2;

            ts.Subscribers[0].Attributes[5] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[5].Name = "NamePrefix";
            ts.Subscribers[0].Attributes[5].Value = limitedSubscriber.NamePrefix;

            ts.Subscribers[0].Attributes[6] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[6].Name = "NameSuffix";
            ts.Subscribers[0].Attributes[6].Value = limitedSubscriber.NameSuffix;

            ts.Subscribers[0].Attributes[7] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[7].Name = "LimitedProgramID";
            ts.Subscribers[0].Attributes[7].Value = triggeredRequest.LimitedProgramId.ToString(CultureInfo.InvariantCulture);

            ts.Subscribers[0].Attributes[8] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[8].Name = "RegistrationDate";
            ts.Subscribers[0].Attributes[8].Value = limitedSubscriber.RegistrationDate.ToString(CultureInfo.InvariantCulture);

            ts.Subscribers[0].Attributes[9] = new ExactTarget.Attribute();
            ts.Subscribers[0].Attributes[9].Name = "CustomerAnswerXML";
            ts.Subscribers[0].Attributes[9].Value = limitedSubscriber.WizardData;

            if (limitedSubscriber.Attributes != null)
            {
                for (int count = 10; count < ts.Subscribers[0].Attributes.Length; count++)
                {
                    ts.Subscribers[0].Attributes[count] = new ExactTarget.Attribute();
                    ts.Subscribers[0].Attributes[count].Name = limitedSubscriber.Attributes[count - 10].Name;
                    ts.Subscribers[0].Attributes[count].Value = limitedSubscriber.Attributes[count - 10].Value;
                }
            }
        }

        /// <summary>
        /// It prepares the triggered send definition for Generic Subscriber
        /// </summary>
        /// <param name="triggeredRequest"></param>
        /// <param name="configuration"></param>
        /// <param name="ts"></param>
        /// <param name="genericSubscriber"></param>
        /// <param name="user"></param>
        private static void getTriggeredSendForGenericSubscriber(TriggeredRequest triggeredRequest, InterchangeConfiguration configuration, TriggeredSend ts, GenericSubscriber genericSubscriber, InterchangeUser user)
        {
            // FY13.1 Requirement.
            if (!string.IsNullOrEmpty(triggeredRequest.CustomerKey))
            {
                ts.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = triggeredRequest.CustomerKey,
                    Priority = "High"
                };
            }
            else
            {
                ts.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = string.Format(CultureInfo.InvariantCulture, "TBN_{0}", triggeredRequest.CommunicationId),
                    Priority = "High"
                };
            }

            ts.Client = new ExactTarget.ClientID();
            // FY13.1 Requirement.
            ts.Client.ID = (!string.IsNullOrEmpty(triggeredRequest.AccountId) && triggeredRequest.AccountId != "0" ? Convert.ToInt32(triggeredRequest.AccountId) : Convert.ToInt32(RetrieveAccountIdForTriggeredSend(ts.TriggeredSendDefinition.CustomerKey, configuration, user)));
            ts.Client.IDSpecified = true;

            ts.Subscribers = new ExactTarget.Subscriber[1];
            ts.Subscribers[0] = new ExactTarget.Subscriber();

            ts.Subscribers[0].EmailAddress = genericSubscriber.EmailAddress;
            ts.Subscribers[0].SubscriberKey = genericSubscriber.SubscriberKey;
            if (genericSubscriber.Attributes != null)
            {
                ts.Subscribers[0].Attributes = new ExactTarget.Attribute[genericSubscriber.Attributes.Count];

                for (int count = 0; count < genericSubscriber.Attributes.Count; count++)
                {
                    ts.Subscribers[0].Attributes[count] = new ExactTarget.Attribute();
                    ts.Subscribers[0].Attributes[count].Name = genericSubscriber.Attributes[count].Name;
                    ts.Subscribers[0].Attributes[count].Value = genericSubscriber.Attributes[count].Value;
                }
            }
        }

        /// <summary>
        /// It prepares the triggered send definition for Event Subscriber
        /// </summary>
        /// <param name="triggeredRequest"></param>
        /// <param name="configuration"></param>
        /// <param name="triggeredSend"></param>
        /// <param name="user"></param>
        private static void getTriggeredSendForEventSubscriber(TriggeredRequest triggeredRequest, InterchangeConfiguration configuration, TriggeredSend triggeredSend, InterchangeUser user)
        {

            // FY13.1 Requirement.
            if (!string.IsNullOrEmpty(triggeredRequest.CustomerKey))
            {
                triggeredSend.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = triggeredRequest.CustomerKey,
                    Priority = "High"
                };
            }
            else
            {
                triggeredSend.TriggeredSendDefinition = new ExactTarget.TriggeredSendDefinition()
                {
                    CustomerKey = string.Format(CultureInfo.InvariantCulture, "TBN_{0}", triggeredRequest.TemplateId),
                    Priority = "High"
                };
            }

            triggeredSend.Client = new ExactTarget.ClientID();
            // FY13.1 Requirement.
            triggeredSend.Client.ID = (!string.IsNullOrEmpty(triggeredRequest.AccountId) && triggeredRequest.AccountId != "0" ? Convert.ToInt32(triggeredRequest.AccountId) : Convert.ToInt32(RetrieveAccountIdForTriggeredSend(triggeredSend.TriggeredSendDefinition.CustomerKey, configuration, user)));

            triggeredSend.Client.IDSpecified = true;

            triggeredSend.Subscribers = new Subscriber[triggeredRequest.Subscribers.Count];

            int i = 0;

            foreach (var subscriber in triggeredRequest.Subscribers)
            {

                EventSubscriber eventSubscriber = subscriber as EventSubscriber;

                triggeredSend.Subscribers[i] = new ExactTarget.Subscriber();

                triggeredSend.Subscribers[i].SubscriberKey = eventSubscriber.SubscriberKey;
                triggeredSend.Subscribers[i].EmailAddress = eventSubscriber.EmailAddress;

                int numberOfAttributes = 13 + (eventSubscriber.Attributes != null ? eventSubscriber.Attributes.Count : 0);
                triggeredSend.Subscribers[i].Attributes = new ExactTarget.Attribute[numberOfAttributes];

                triggeredSend.Subscribers[i].Attributes[0] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[0].Name = "LocaleID";
                triggeredSend.Subscribers[i].Attributes[0].Value = eventSubscriber.LocaleId.ToString(CultureInfo.InvariantCulture);

                triggeredSend.Subscribers[i].Attributes[1] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[1].Name = "EventID";
                triggeredSend.Subscribers[i].Attributes[1].Value = eventSubscriber.EventId.ToString(CultureInfo.InvariantCulture);

                triggeredSend.Subscribers[i].Attributes[2] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[2].Name = "EmailTypeID";
                triggeredSend.Subscribers[i].Attributes[2].Value = eventSubscriber.EmailTypeId.ToString(CultureInfo.InvariantCulture);

                triggeredSend.Subscribers[i].Attributes[3] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[3].Name = "Subject";
                triggeredSend.Subscribers[i].Attributes[3].Value = eventSubscriber.Subject;

                triggeredSend.Subscribers[i].Attributes[4] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[4].Name = "From";
                triggeredSend.Subscribers[i].Attributes[4].Value = eventSubscriber.From;

                triggeredSend.Subscribers[i].Attributes[5] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[5].Name = "Body";
                triggeredSend.Subscribers[i].Attributes[5].Value = eventSubscriber.Body;

                triggeredSend.Subscribers[i].Attributes[6] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[6].Name = "CampaignCode";
                triggeredSend.Subscribers[i].Attributes[6].Value = eventSubscriber.CampaignCode;

                triggeredSend.Subscribers[i].Attributes[7] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[7].Name = "ReplyTo";
                triggeredSend.Subscribers[i].Attributes[7].Value = eventSubscriber.ReplyTo;

                triggeredSend.Subscribers[i].Attributes[8] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[8].Name = "BatchID";
                triggeredSend.Subscribers[i].Attributes[8].Value = triggeredRequest.BatchId.ToString();

                triggeredSend.Subscribers[i].Attributes[9] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[9].Name = "WWERequestSendDateTime";
                triggeredSend.Subscribers[i].Attributes[9].Value = eventSubscriber.WweRequestSendDateTime.ToString();

                triggeredSend.Subscribers[i].Attributes[10] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[10].Name = "PIRequestSendDateTime";
                triggeredSend.Subscribers[i].Attributes[10].Value = DateTime.UtcNow.ToString();

                triggeredSend.Subscribers[i].Attributes[11] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[11].Name = "TargetAudience";
                triggeredSend.Subscribers[i].Attributes[11].Value = eventSubscriber.TargetAudience;

                triggeredSend.Subscribers[i].Attributes[12] = new ExactTarget.Attribute();
                triggeredSend.Subscribers[i].Attributes[12].Name = "TargetProduct";
                triggeredSend.Subscribers[i].Attributes[12].Value = eventSubscriber.TargetProduct;

                if (eventSubscriber.Attributes != null)
                {
                    for (int count = 13; count < triggeredSend.Subscribers[i].Attributes.Length; count++)
                    {
                        triggeredSend.Subscribers[i].Attributes[count] = new ExactTarget.Attribute();
                        triggeredSend.Subscribers[i].Attributes[count].Name = eventSubscriber.Attributes[count - 13].Name;
                        triggeredSend.Subscribers[i].Attributes[count].Value = eventSubscriber.Attributes[count - 13].Value;
                    }
                }

                i++;
            }
        }

        /// <summary>
        /// It gets account id for triggered send definition
        /// </summary>
        /// <param name="customerKey"></param>
        /// <param name="configuration"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int RetrieveAccountIdForTriggeredSend(string customerKey, InterchangeConfiguration configuration, InterchangeUser user)
        {
            if (string.IsNullOrWhiteSpace(customerKey))
                throw new ArgumentNullException("customerKey");

            var retrieveRequest = new RetrieveRequest();
            retrieveRequest.ObjectType = "TriggeredSendDefinition";
            retrieveRequest.Properties = new string[] { "Client.ID", "CustomerKey" };

            var simpleFilterPart = new SimpleFilterPart();
            simpleFilterPart.Property = "CustomerKey";
            simpleFilterPart.SimpleOperator = SimpleOperators.equals;
            simpleFilterPart.Value = new string[] { customerKey };

            retrieveRequest.Filter = simpleFilterPart;
            retrieveRequest.QueryAllAccounts = true;
            retrieveRequest.QueryAllAccountsSpecified = true;

            var partnerAPI = getPartnerAPIObject(configuration, user);

            APIObject[] results = null;
            string requestID = string.Empty;
            string status = partnerAPI.Retrieve(retrieveRequest, out requestID, out results);

            DataAccess.AddXmlSoapRequestResponse(
                connectionString: configuration.InterchangeDBConnectionString,
                apiMethod: "Retrieve", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: null,
                apiObjects: XmlHelper.SerializeIt(retrieveRequest),
                additionalInformation: "<Dimensions><RequestId>" + requestID + "</RequestId><OverallStatus>" + status + "</OverallStatus></Dimensions>",
                apiResults: XmlHelper.SerializeIt(results));

            if (null == results || 0 == results.Length || status.Equals("Error"))
            {
                string ErrorMessage = (results != null && results.Length > 0 ? "ET Returned Error while trying to retrieve Account Id for the given Customer Key: " + customerKey : "Unable to find Triggered Send of name: " + customerKey);
                throw new InvalidOperationException(ErrorMessage);
            }

            var triggeredSendDefinition = results[0] as TriggeredSendDefinition;
            // verbose loggin here.
            return triggeredSendDefinition.Client.ID;
        }
        #endregion

        #region CREATEREQUEST METHOD
        /// <summary>
        /// Creates a request at Exact target
        /// </summary>
        /// <param name="request">The Promo/SP/ST request object</param>
        /// <param name="user">The Interchange User object</param>
        /// <returns></returns>
        public static bool CreateRequestAtExactTarget(CoreRequest.Request request, InterchangeUser user)
        {
            if (null == request)
                throw new ArgumentNullException("Request");

            if (null == user)
                throw new ArgumentNullException("user");

            var etConfiguration = new InterchangeConfiguration();
            var transformedRequests = new ExactTarget.APIObject[1];
            transformedRequests[0] = TransformToExactTargetObject(request, etConfiguration);

            var ConversationId = string.Empty;
            var createdExactTargetIds = CreateRequestsAtExactTarget(transformedRequests, etConfiguration, user, ConversationId);

            if (createdExactTargetIds != null && createdExactTargetIds.Length > 0)
                request.ExactTargetRequestId = createdExactTargetIds[0];

            return true;
        }

        /// <summary>
        /// Creates requests at Exact target
        /// </summary>
        /// <param name="apiObjects"></param>
        /// <param name="configuration"></param>
        /// <param name="user"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public static Guid[] CreateRequestsAtExactTarget(ExactTarget.APIObject[] apiObjects, InterchangeConfiguration configuration, InterchangeUser user, string conversationId)
        {
            if (null == apiObjects) throw new ArgumentNullException("apiObjects");
            if (null == configuration) throw new ArgumentNullException("configuration");

            var APIReference = getPartnerAPIObject(configuration, user);
            var requestID = string.Empty;
            var status = string.Empty;
            ExactTarget.CreateResult[] results = null;
            var createOptions = new ExactTarget.CreateOptions();
            var shouldPerformAsync = true;

            foreach (ExactTarget.APIObject item in apiObjects)
            {
                if (!(item is ExactTarget.TriggeredSend))
                {
                    shouldPerformAsync = false;
                    break;
                }
            }

            if (shouldPerformAsync)
            {
                createOptions.RequestType = ExactTarget.RequestType.Asynchronous;
                createOptions.RequestTypeSpecified = true;
            }

            createOptions.ConversationID = conversationId;
            
            results = APIReference.Create(createOptions, apiObjects, out requestID, out status);

            DataAccess.AddXmlSoapRequestResponse(
                connectionString: configuration.InterchangeDBConnectionString,
                apiMethod: "Create", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: XmlHelper.SerializeIt(createOptions),
                apiObjects: XmlHelper.SerializeIt(apiObjects),
                additionalInformation: "<Dimensions><RequestId>" + requestID + "</RequestId><OverallStatus>" + status + "</OverallStatus></Dimensions>",
                apiResults: XmlHelper.SerializeIt(results));

            var Guids = new Guid[apiObjects.Length];

            if (results == null || results.Length.Equals(0) || results.Length < apiObjects.Length || status.Equals("Error"))
            {
                string ErrorMessage = (results != null && results.Length > 0 ? string.Format(CultureInfo.CurrentCulture, "Error Code:{0}, Status Code:{1}, Status Message:{2}", results[0].ErrorCode.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), results[0].StatusCode, results[0].StatusMessage) : "Results received from ET with Error / NULL");
                if (results[0].StatusMessage.Equals("Invalid ConversationID. ConversationID has already been used.") || results[0].StatusMessage.Equals("Conversation is closed and cannot accept more parts."))
                    throw new InvalidConversationException(ErrorMessage);
                else
                    throw new InvalidOperationException(ErrorMessage);
            }

            Parallel.For(0, apiObjects.Length, delegate(int i)
            {
                Guid resultGuid = Guid.Empty;
                if (Guid.TryParse(results[i].NewObjectID, out resultGuid))
                    Guids[i] = resultGuid;
            });

            return Guids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ExactTarget.PartnerAPI getPartnerAPIObject(InterchangeConfiguration configuration, InterchangeUser user)
        {
            if (null == configuration) throw new ArgumentNullException("configuration");

            ExactTarget.PartnerAPI ApiReference = null;
            ExactTarget.PartnerAPI tempApiReference = null;
            try
            {
                if (user.UseETCertificate)
                {
                    WebServicesClientProtocol proxy = null;
                    proxy = ApplyCertificateAuthentication(configuration, user);
                    tempApiReference = proxy as ExactTarget.PartnerAPI;
                }
                else
                {
                    tempApiReference = new PartnerAPI();
                    tempApiReference.Url = user.ExactTargetUrl;

                    //Set the username/password.  This is using the Username token of WS-Security 1.0
                    UsernameTokenProvider utp = new UsernameTokenProvider(user.ExactTargetUsername, user.ExactTargetPassword);
                    tempApiReference.SetClientCredential<UsernameToken>(utp.GetToken());
                    Policy policy = new Policy(new UsernameOverTransportAssertion());
                    tempApiReference.SetPolicy(policy);
                    tempApiReference.Timeout = 240000;
                }
                ApiReference = tempApiReference;
                tempApiReference = null;
            }
            finally
            {
                if (tempApiReference != null)
                    tempApiReference.Dispose();
            }
            return ApiReference;
        }

        public static ExactTarget.FileTrigger RetrieveFileTriggerByObjectID(string fileTriggerObjectId, bool isDeltaProvided, InterchangeConfiguration configValues, InterchangeUser user)
        {
            string requestId;

            //Create the request object
            ExactTarget.RetrieveRequest retriveRequest = new ExactTarget.RetrieveRequest();

            //Giving Filter properties to filter the search.
            ExactTarget.SimpleFilterPart sfp = createSimpleFilter("ObjectID", fileTriggerObjectId);

            //Specifting the object type and the properties to be retrieved.
            retriveRequest.ObjectType = "FileTrigger";
            retriveRequest.Properties = new string[] { "ObjectID", "FileName", "ResponseControlManifest", "Status", "Type", "Client.ID" };
            retriveRequest.Filter = sfp;

            retriveRequest.QueryAllAccounts = true;
            retriveRequest.QueryAllAccountsSpecified = true;

            //Initialize the web service proxy.
            ExactTarget.PartnerAPI partnerAPI = Communicator.getPartnerAPIObject(configValues, user);

            //Invoke the Web Service.
            ExactTarget.APIObject[] results;
            string status = partnerAPI.Retrieve(retriveRequest, out requestId, out results);

            DataAccess.AddXmlSoapRequestResponse(
                connectionString: configValues.InterchangeDBConnectionString,
                apiMethod: "Retrieve", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: null,
                apiObjects: XmlHelper.SerializeIt(retriveRequest),
                additionalInformation: "<Dimensions><RequestId>" + requestId + "</RequestId><OverallStatus>" + status + "</OverallStatus></Dimensions>",
                apiResults: XmlHelper.SerializeIt(results));

            //If the retrieve failed, log an error and return NULL.

            if (status == "Error" || null == results || results.Length == 0)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.InvariantCulture, StringResources.FILE_PROCESSOR_ET_FTO_RETRIEVE_FAILED, fileTriggerObjectId, requestId));
            }

            //Transfer the results into a FTO.
            ExactTarget.FileTrigger filetrigger = results[0] as ExactTarget.FileTrigger;

            //If the ResponseControlManifest is not yet created.
            if (string.IsNullOrEmpty(filetrigger.ResponseControlManifest))
            {
                EmailInterchangeCommon.Enumerators.RequestType fileTriggerType = (EmailInterchangeCommon.Enumerators.RequestType)Enum.Parse(typeof(EmailInterchangeCommon.Enumerators.RequestType), filetrigger.Type);

                switch (fileTriggerType)
                {
                    case EmailInterchangeCommon.Enumerators.RequestType.BulkSendData:
                    case EmailInterchangeCommon.Enumerators.RequestType.PromotionalListData:
                    case EmailInterchangeCommon.Enumerators.RequestType.SuppressionPromotionalData:
                    case EmailInterchangeCommon.Enumerators.RequestType.SuppressionTransactionalData:
                    case EmailInterchangeCommon.Enumerators.RequestType.CampaignMetaData:
                        filetrigger.ResponseControlManifest = RetrieveProgramManifestTemplate(filetrigger.Type, isDeltaProvided, configValues, user);
                        break;

                    default:
                        break;
                }
            }

            return filetrigger;
        }

        private static ExactTarget.SimpleFilterPart createSimpleFilter(string filterName, string filterValue)
        {
            ExactTarget.SimpleFilterPart statusFilter = new ExactTarget.SimpleFilterPart();
            statusFilter.Property = filterName;
            statusFilter.SimpleOperator = ExactTarget.SimpleOperators.equals;
            statusFilter.Value = new string[] { filterValue };
            return statusFilter;
        }

        private static ExactTarget.ComplexFilterPart CreateComplexFilter(ExactTarget.SimpleFilterPart leftFilter, ExactTarget.SimpleFilterPart rightFilter)
        {
            ExactTarget.ComplexFilterPart filterPart = new ExactTarget.ComplexFilterPart();
            filterPart.LeftOperand = leftFilter;
            filterPart.LogicalOperator = ExactTarget.LogicalOperators.AND;
            filterPart.RightOperand = rightFilter;
            return filterPart;
        }

        public static string RetrieveProgramManifestTemplate(string fileTriggerType, bool isDeltaProvided, InterchangeConfiguration configValues, InterchangeUser user)
        {
            ExactTarget.PartnerAPI partnerAPI = null;
            try
            {
                partnerAPI = new ExactTarget.PartnerAPI();
                ExactTarget.RetrieveRequest retrieveRequest = new ExactTarget.RetrieveRequest();

                //Specifting the object type and the properties to be retrieved.
                retrieveRequest.ObjectType = "ProgramManifestTemplate";
                retrieveRequest.Properties = new string[] { "Content" };

                ExactTarget.SimpleFilterPart simpleFilterPart1 = createSimpleFilter("Type", fileTriggerType);

                //For Delta/Full make the ComplexFilterPart
                if ((fileTriggerType == requestEnum.RequestType.SuppressionPromotionalData.ToString())
                    || (fileTriggerType == requestEnum.RequestType.SuppressionTransactionalData.ToString()))
                {
                    ExactTarget.SimpleFilterPart simpleFilterPart2 = createSimpleFilter("OperationType", isDeltaProvided ? "Delta" : "Full");

                    ExactTarget.ComplexFilterPart complexFilterPart = CreateComplexFilter(simpleFilterPart1, simpleFilterPart2);

                    retrieveRequest.Filter = complexFilterPart;
                }
                else
                {
                    retrieveRequest.Filter = simpleFilterPart1;
                }

                partnerAPI = Communicator.getPartnerAPIObject(configValues, user);
                string requestId;
                //Retrieving the results.
                ExactTarget.APIObject[] results;
                string status = partnerAPI.Retrieve(retrieveRequest, out requestId, out results);

                DataAccess.AddXmlSoapRequestResponse(
                connectionString: configValues.InterchangeDBConnectionString,
                apiMethod: "Retrieve", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: null,
                apiObjects: XmlHelper.SerializeIt(retrieveRequest),
                additionalInformation: "<Dimensions><RequestId>" + requestId + "</RequestId><OverallStatus>" + status + "</OverallStatus></Dimensions>",
                apiResults: XmlHelper.SerializeIt(results));

                if (status == "Error" || null == results || results.Length == 0)
                {
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.InvariantCulture, StringResources.FILE_PROCESSOR_ET_FTO_MANIFEST_RETRIEVE_FAILED, fileTriggerType, requestId));
                }

                return ((ExactTarget.ProgramManifestTemplate)results[results.Length - 1]).Content;
            }
            finally
            {
                if (partnerAPI != null) partnerAPI.Dispose();
            }
        }
        #endregion

        public static bool UpdateFileTriggerObject(IRequest iRequest, InterchangeConfiguration configValues, string ftoStatus, string ftoStatusMessage, InterchangeUser user, string dataExtensionName)
        {
            if (iRequest == null) throw new ArgumentNullException("iRequest");
            if (configValues == null) throw new ArgumentNullException("configValues");

            dynamic request = iRequest;

            //Retrieve the FTO of the given RequestObject and set the status of the FTO as the Status of the Request Object.
            bool isDeltaProvided = false;
            if (request.DataEndpoints != null && request.DataEndpoints.Count > 0)
                isDeltaProvided = request.DataEndpoints[0].IsDeltaProvided;

            ExactTarget.FileTrigger fileTrigger = RetrieveFileTriggerByObjectID(iRequest.ExactTargetRequestId.ToString(), isDeltaProvided, configValues, user);

            if (fileTrigger == null) throw new InvalidOperationException(StringResources.FILE_PROCESSOR_CREATE_ET_REQUEST_FTO_NULL_ERROR);

            fileTrigger.Status = ftoStatus;

            string file = string.Format("{0}.{1}", request.ExactTargetRequestId, "aes");//Always File Name as "ETRequestId.aes".
            fileTrigger.FileName = System.IO.Path.GetFileName(file);

            bool status = UpdateManifestBulkSendData(ref fileTrigger, request, dataExtensionName);
            if (!status)
                throw new InvalidOperationException(StringResources.FILE_PROCESSOR_ET_FTO_MANIFEST_UPDATE_FAILED);

            fileTrigger.StatusMessage = ftoStatusMessage;

            #region Updating in ET

            string returnStatus = null;
            string returnRequestID = null;

            //Initialize the web service proxy
            PartnerAPI partnerAPI = Communicator.getPartnerAPIObject(configValues, user);

            ExactTarget.UpdateResult[] Updresults = null;
            Updresults = partnerAPI.Update(new ExactTarget.UpdateOptions(), new ExactTarget.APIObject[] { fileTrigger }, out returnRequestID, out returnStatus);

            DataAccess.AddXmlSoapRequestResponse(
                        connectionString: configValues.InterchangeDBConnectionString,
                        apiMethod: "Update", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: XmlHelper.SerializeIt(new UpdateOptions()),
                        apiObjects: XmlHelper.SerializeIt(new APIObject[] { fileTrigger }),
                        additionalInformation: "<Dimensions><RequestId>" + returnRequestID + "</RequestId><OverallStatus>" + returnStatus + "</OverallStatus></Dimensions>",
                        apiResults: XmlHelper.SerializeIt(Updresults));

            if (returnStatus == "Error")
            {
                string results = string.Empty;
                if (Updresults != null)
                {
                    foreach (ExactTarget.UpdateResult eachResult in Updresults)
                    {
                        results += string.Format(CultureInfo.CurrentCulture, StringResources.DetailXMLStscodeStsmessageErrorcode, eachResult.ResultDetailXML, eachResult.StatusCode, eachResult.StatusMessage, eachResult.ErrorCode);
                    }
                }

                throw new InvalidOperationException(StringResources.ReturnStatusfromETonUpdateErrorWithResults + results);
            }

            #endregion

            return true;
        }

        private static bool UpdateManifestBulkSendData(ref ExactTarget.FileTrigger fileTrigger, dynamic request, string dataExtensionName)
        {
            //replacing the objectid tag with the actual value ETID
            string manifestStringRef = fileTrigger.ResponseControlManifest.Replace("{FileTriggerID}", request.ExactTargetRequestId.ToString());

            //Load the string from ResponseControlManifest into XmlDocument 'manifest'.
			var settings = new XmlReaderSettings();
			settings.DtdProcessing = DtdProcessing.Prohibit;
            settings.Schemas.Add(null, Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath) + Path.DirectorySeparatorChar + "ProgramManifestSchema.xsd");
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);  
			settings.XmlResolver = null;
			var reader = XmlReader.Create(new MemoryStream(Encoding.Unicode.GetBytes(manifestStringRef)), settings);
			var manifest = new XmlDocument();
			manifest.Load(reader);

            manifest.SelectSingleNode("./ProgramManifest/Client/ID").InnerText = request.TenantAccountId;
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/OutputFile/Filename").InnerText = request.SourceFileName;
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/FileSpec").InnerText = request.SourceFileName;
            if (string.Compare(dataExtensionName, string.Empty, System.StringComparison.OrdinalIgnoreCase) == 0)
            {
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/DestinationObject/DataExtension/CustomerKey").InnerText = request.DataEndpoints[0].FilterList[0].Value;
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/UpdateType").InnerText = ((InterchangeFileRequest)request).DataImportType.ToString();
            }
            else
            {
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/DestinationObject/DataExtension/CustomerKey").InnerText = dataExtensionName;
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/UpdateType").InnerText = ((InterchangeFileRequest)request).DataImportType.ToString();
            }
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/FileType").InnerText = request.DataEndpoints[0].FilterList[1].Value;
            //Add Notification Email Address.   
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/ImportNotificationAddress").InnerText = request.NotificationEmailAddress;
            string fileName = System.IO.Path.GetFileName(request.FilePath);
            string fileExtension = System.IO.Path.GetExtension(fileName);
            if (string.Compare(fileExtension, ".zip", System.StringComparison.OrdinalIgnoreCase) != 0)
            {
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Filename").InnerText = string.Format("{0}{1}", request.ExactTargetRequestId.ToString(), ".gz");
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/OutputFile/Filename").InnerText = string.Format("{0}{1}", request.ExactTargetRequestId.ToString(), ".gz");
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Type").InnerText = "GZIP";
            }
            else
            {
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Filename").InnerText = string.Format("{0}{1}", request.ExactTargetRequestId.ToString(), fileExtension);
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/OutputFile/Filename").InnerText = string.Format("{0}{1}", request.ExactTargetRequestId.ToString(), fileExtension);
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Type").InnerText = "ZIP";
            }
            //Transfer the manifest back into the ResponseControlManifest.-- Anil-21022012
            fileTrigger.ResponseControlManifest = manifest.OuterXml.ToString(System.Globalization.CultureInfo.InvariantCulture);

            return true;

        }

        /// <summary>
        /// Retrieves all the FileTrigger objects with specified Status from Exact Target
        /// </summary>
        /// <param name="fileTriggerStatus">Specifies the Status filter based on which FileTrigger objects must be retrieved</param>
        /// <param name="configValues">Gives the data required for contacting Exact Target</param>
        /// <param name="InterchangeConfiguration">Gives the metadata from .cscfg file</param>
        /// <returns>Collection of FileTrigger objects which have the specified Status</returns>
        public static IEnumerable<ExactTarget.FileTrigger> RetrieveFileTriggerByStatus(string fileTriggerStatus, ETConnectionConfig configValues, InterchangeConfiguration interchangeConfiguration)
        {
            string requestId;
            List<ExactTarget.FileTrigger> filetrigCollection;

            ExactTarget.RetrieveRequest retrieveRequest = new ExactTarget.RetrieveRequest();

            //Specifting the object type and the properties to be retrieved.
            retrieveRequest.ObjectType = "FileTrigger";
            retrieveRequest.Properties = new string[] { "FileName"
                    , "ResponseControlManifest"
                    , "ObjectID"
                    , "RequestParameterDetail"
                    , "Type"
                    , "ScheduledDate" };

            retrieveRequest.Filter = PrepareFilterCriteria(fileTriggerStatus);

            retrieveRequest.QueryAllAccounts = true;
            retrieveRequest.QueryAllAccountsSpecified = true;

            InterchangeUser user = GetUserDetailsFromETConfigValues(configValues);

            //Initialize the web service proxy
            ExactTarget.PartnerAPI partnerAPIWse = getPartnerAPIObject(interchangeConfiguration, user);

            //Invoke the Web Service
            ExactTarget.APIObject[] results;
            string status = partnerAPIWse.Retrieve(retrieveRequest, out requestId, out results);

            DataAccess.AddXmlSoapRequestResponse(
                connectionString: interchangeConfiguration.InterchangeDBConnectionString,
                apiMethod: "Retrieve", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: null,
                apiObjects: XmlHelper.SerializeIt(retrieveRequest),
                additionalInformation: "<Dimensions><RequestId>" + requestId + "</RequestId><OverallStatus>" + status + "</OverallStatus></Dimensions>",
                apiResults: XmlHelper.SerializeIt(results));

            if (status == "Error")
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.InvariantCulture, StringResources.WorkerRole_ETRetrieveByStatusFailed, fileTriggerStatus, requestId));
            }

            if (results != null)
            {
                filetrigCollection = new List<FileTrigger>();

                foreach (ExactTarget.APIObject filetrigg in results)
                {
                    if (!string.IsNullOrEmpty((filetrigg as ExactTarget.FileTrigger).Type.Trim()))
                    {
                        filetrigCollection.Add(filetrigg as ExactTarget.FileTrigger);
                    }

                }

                return filetrigCollection;
            }

            else
                return new List<ExactTarget.FileTrigger>();
        }

        /// <summary>
        /// Updates the FileTrigger in ET corresponding to the specified RequestObject. 
        /// </summary>
        /// <param name="iRequest">The RequestObject for which the FileTrigger in ET has to be modified.</param>
        /// <param name="partnerAPIUrl">URL.</param>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="clientId">ClinetID.</param>
        /// <param name="typeForLogging">The ComponentType for logging.</param>
        /// <returns>True for success and False for failure.</returns>
        public static bool UpdateFileTriggerStatus(IRequest iRequest, InterchangeConfiguration configuration, int maximumRetryAttempts, ETConnectionConfig configValues)
        {
            if (iRequest == null) throw new ArgumentNullException("iRequest");
            if (configValues == null) throw new ArgumentNullException("configValues");

            Log.Add(iRequest.EmailInterchangeRequestId, iRequest.ExactTargetRequestId, iRequest.RequestType.ToString(),
                        Component.InterchangeETProcessor, String.Format(CultureInfo.InvariantCulture, StringResources.INTERCHANGE_ET_PROCESSOR),
                        211003001, "InterchangeEtProcessor.UpdateFileTriggerStatus", requestEnum.SeverityType.Information,
                        String.Format(CultureInfo.InvariantCulture, StringResources.ETProcessor_UpdatingFTOstatusStarted),
                        string.Empty,
                        XmlHelper.DataContractSerialize(iRequest),
                        LogType.Verbose,
                        LogClassification.Transaction);

            for (int iterator = 1; iterator <= maximumRetryAttempts; iterator++)
            {
                try
                {
                    dynamic request = iRequest;
                    //Retrieve the FTO of the given RequestObject and set the status of the FTO as the Status of the Request Object.
                    bool isDeltaProvided = false;
                    if (request.DataEndpoints != null && request.DataEndpoints.Count > 0)
                        isDeltaProvided = request.DataEndpoints[0].IsDeltaProvided;

                    InterchangeUser user = GetUserDetailsFromETConfigValues(configValues);

                    ExactTarget.FileTrigger fileTrigger = RetrieveFileTriggerByObjectID(iRequest.ExactTargetRequestId.ToString(), isDeltaProvided, configuration, user);

                    if (fileTrigger == null)
                    {
                        throw new InvalidOperationException(StringResources.FILE_TRIGGER_CANNOT_BE_NULL);
                    }

                    if (iRequest.ProcessStatus == requestEnum.ProcessStatus.RequestAssemble)
                    {
                        fileTrigger.Status = requestEnum.RequestStatus.ExternalReceived.ToString();
                    }
                    else if (iRequest.ProcessStatus == requestEnum.ProcessStatus.ETUpdateSuccess)
                    {
                        fileTrigger.Status = requestEnum.RequestStatus.Complete.ToString();
                        fileTrigger.StatusMessage = StringResources.EMAIL_RESPONSE_FTO_STATUS_COMPLETE_MESSAGE;
                    }
                    else
                    {
                        fileTrigger.Status = RequestStatus.ExternalError.ToString();
                        fileTrigger.StatusMessage = StringResources.EMAIL_RESPONSE_FTO_STATUS_ERROR_MESSAGE;
                    }

                    #region Updating in ET

                    string returnStatus = null;
                    string returnRequestID = null;

                    //Initialize the web service proxy
                    ExactTarget.PartnerAPI partnerAPI = getPartnerAPIObject(configuration, GetUserDetailsFromETConfigValues(configValues));

                    ExactTarget.UpdateResult[] Updresults = null;
                    Updresults = partnerAPI.Update(new ExactTarget.UpdateOptions(), new ExactTarget.APIObject[] { fileTrigger }, out returnRequestID, out returnStatus);

                    DataAccess.AddXmlSoapRequestResponse(
                        connectionString: configuration.InterchangeDBConnectionString,
                        apiMethod: "Update", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: XmlHelper.SerializeIt(new UpdateOptions()),
                        apiObjects: XmlHelper.SerializeIt(new APIObject[] { fileTrigger }),
                        additionalInformation: "<Dimensions><RequestId>" + returnRequestID + "</RequestId><OverallStatus>" + returnStatus + "</OverallStatus></Dimensions>",
                        apiResults: XmlHelper.SerializeIt(Updresults));

                    if (returnStatus == "Error")
                    {
                        string results = string.Empty;
                        if (Updresults != null)
                        {
                            foreach (ExactTarget.UpdateResult eachResult in Updresults)
                            {
                                results += string.Format(CultureInfo.CurrentCulture, StringResources.DetailXMLStscodeStsmessageErrorcode, eachResult.ResultDetailXML, eachResult.StatusCode, eachResult.StatusMessage, eachResult.ErrorCode);
                            }
                        }

                        throw new InvalidOperationException(StringResources.ReturnStatusfromETonUpdateErrorWithResults + results);
                    }

                    #endregion
                    break;
                }
                catch (Exception ex)
                {
                    if (iterator == maximumRetryAttempts)
                    {
                        Log.Add(iRequest.EmailInterchangeRequestId, iRequest.ExactTargetRequestId, iRequest.RequestType.ToString(),
                                 Component.InterchangeETProcessor, String.Format(CultureInfo.InvariantCulture, StringResources.INTERCHANGE_ET_PROCESSOR),
                                 211003002, "InterchangeEtProcessor.UpdateFileTriggerStatus", requestEnum.SeverityType.Exception,
                                 String.Format(CultureInfo.InvariantCulture, StringResources.ETProcessor_UpdatingFTOstatusFailed, ex.Message),
                                 ex.StackTrace,
                                 string.Empty,
                                 LogType.Business,
                                 LogClassification.Transaction);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Updates the FileTrigger in ET corresponding to the specified RIO QueueMessage. 
        /// </summary>
        /// <param name="message">InterchangeRIOProcessedQueueMessage</param>
        /// <param name="configuration">interchangeConfiguration</param>
        /// <param name="maximumRetryAttempts">no of ReqtryAttempts</param>
        /// <param name="configValues">ETConnectionConfigValues</param>
        /// <returns>True for success and False for failure.</returns>
        public static bool UpdateFileTriggerStatus(BaseQueueMessage message, InterchangeConfiguration configuration, int maximumRetryAttempts, ETConnectionConfig configValues)
        {
            if (configValues == null)
            {
                throw new ArgumentNullException("configValues");
            }
            InterchangeRIOProcessedQueueMessage rioProcessedMessage = (InterchangeRIOProcessedQueueMessage)message;

            Log.Add(rioProcessedMessage.Id, rioProcessedMessage.Id, requestEnum.RequestType.ReportExtractData.ToString(),
                        Component.InterchangeETProcessor, String.Format(CultureInfo.InvariantCulture, StringResources.INTERCHANGE_ET_PROCESSOR),
                        211009001, "InterchangeEtProcessor.UpdateFileTriggerStatus", requestEnum.SeverityType.Information,
                        String.Format(CultureInfo.InvariantCulture, StringResources.ETProcessor_UpdatingFTOstatusStarted),
                        string.Empty,
                        string.Empty,
                        LogType.Verbose,
                        LogClassification.Transaction);

            for (int iterator = 1; iterator <= maximumRetryAttempts; iterator++)
            {
                try
                {
                    //Retrieve the FTO of the given RequestObject and set the status of the FTO as the Status of the Request Object.
                    bool isDeltaProvided = true;

                    InterchangeUser interchangeUser = GetUserDetailsFromETConfigValues(configValues);

                    ExactTarget.FileTrigger fileTrigger = RetrieveFileTriggerByObjectID(rioProcessedMessage.Id.ToString(), isDeltaProvided, configuration, interchangeUser);

                    if (fileTrigger == null)
                    {
                        throw new InvalidOperationException(StringResources.FILE_TRIGGER_CANNOT_BE_NULL);
                    }

                    if (rioProcessedMessage.RequestProcessingStatus == RequestProcessingStatus.Completed_OnPremise.ToString())
                    {
                        fileTrigger.Status = requestEnum.RequestStatus.Complete.ToString();
                        fileTrigger.StatusMessage = StringResources.EMAIL_RESPONSE_FTO_STATUS_COMPLETE_MESSAGE;
                    }
                    else if (rioProcessedMessage.RequestProcessingStatus == RequestProcessingStatus.Failed_OnPremise.ToString())
                    {
                        fileTrigger.Status = requestEnum.RequestStatus.ExternalError.ToString();
                        fileTrigger.StatusMessage = StringResources.RIO_RESPONSE_FTO_STATUS_ERROR_MESSAGE;
                    }

                    #region Updating in ET

                    string returnStatus = null;
                    string returnRequestID = null;

                    //Initialize the web service proxy
                    ExactTarget.PartnerAPI partnerAPI = getPartnerAPIObject(configuration, GetUserDetailsFromETConfigValues(configValues));

                    ExactTarget.UpdateResult[] updResults = null;
                    updResults = partnerAPI.Update(new ExactTarget.UpdateOptions(), new ExactTarget.APIObject[] { fileTrigger }, out returnRequestID, out returnStatus);

                    DataAccess.AddXmlSoapRequestResponse(
                        connectionString: configuration.InterchangeDBConnectionString,
                        apiMethod: "Update", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: XmlHelper.SerializeIt(new UpdateOptions()),
                        apiObjects: XmlHelper.SerializeIt(new APIObject[] { fileTrigger }),
                        additionalInformation: "<Dimensions><RequestId>" + returnRequestID + "</RequestId><OverallStatus>" + returnStatus + "</OverallStatus></Dimensions>",
                        apiResults: XmlHelper.SerializeIt(updResults));

                    if (returnStatus == "Error")
                    {
                        string results = string.Empty;
                        if (updResults != null)
                        {
                            foreach (ExactTarget.UpdateResult eachResult in updResults)
                            {
                                results += string.Format(CultureInfo.CurrentCulture, StringResources.DetailXMLStscodeStsmessageErrorcode, eachResult.ResultDetailXML, eachResult.StatusCode, eachResult.StatusMessage, eachResult.ErrorCode);
                            }
                        }

                        throw new InvalidOperationException(StringResources.ReturnStatusfromETonUpdateErrorWithResults + results);
                    }

                    #endregion
                    break;
                }
                catch (Exception ex)
                {
                    if (iterator == maximumRetryAttempts)
                    {
                        Log.Add(message.Id, message.Id, requestEnum.RequestType.ReportExtractData.ToString(),
                                 Component.InterchangeETProcessor, String.Format(CultureInfo.InvariantCulture, StringResources.INTERCHANGE_ET_PROCESSOR),
                                 211009002, "InterchangeEtProcessor.UpdateFileTriggerStatus", requestEnum.SeverityType.Exception,
                                 String.Format(CultureInfo.InvariantCulture, StringResources.ETProcessor_UpdatingFTOstatusFailed, ex.Message),
                                 ex.StackTrace,
                                 string.Empty,
                                 LogType.Business,
                                 LogClassification.Transaction);
                        return false;
                    }
                }

            }
            return true;
        }

        public static InterchangeUser GetUserDetailsFromETConfigValues(ETConnectionConfig configvalues)
        {
            InterchangeUser user = new InterchangeUser()
            {
                ExactTargetCertificateName = configvalues.ExactTargetCertificateName,
                ExactTargetPassword = configvalues.ExactTargetAccountPassword,
                ExactTargetUsername = configvalues.ExactTargetUserName,
                ExactTargetUrl = configvalues.ExactTargetURL,
                UseETCertificate = configvalues.UseETCertificate
            };
            return user;
        }

        /// <summary>
        /// Updates the Manifest and FTO in case of ExternalCompleteStatus.
        /// </summary>
        /// <param name="request">RequestObject corresponding to the FTO.</param>
        /// <param name="fileTrigger">FTO for the particular Request.</param>
        /// <param name="clientId">ClientID</param>
        /// <param name="typeForLogging">ComponentType for logging.</param>
        /// <returns>True on Success and False on Failure.</returns>
        private static bool UpdateStatusExternalComplete(IRequest request, ref ExactTarget.FileTrigger fileTrigger, ETConnectionConfig configValues)
        {
            try
            {
                bool statusCheck = false;
                switch (request.RequestType)
                {
                    case requestEnum.RequestType.EmailResponseData:
                        //For EmailResponseData set the Status to Complete
                        fileTrigger.Status = requestEnum.RequestStatus.Complete.ToString();
                        //There is no Manifest Updation in the case of EmailResponseData
                        statusCheck = true;
                        break;
                    default:
                        statusCheck = false;
                        break;
                }
                if (!statusCheck)
                {
                    //If manifest update failed, then  return false.                    
                    return false;
                }
                return true;
            }
            finally
            {

            }
        }

        /// <summary>
        ///  Preparing the FilterCriteria with FileTriggerStatus
        /// </summary>
        /// <param name="fileTriggerStatus">fileTriggerStatus value</param>
        /// <returns>ComplexFilter</returns>
        private static ExactTarget.FilterPart PrepareFilterCriteria(string fileTriggerStatus)
        {
            //Giving Status Filter properties to filter the search.
            ExactTarget.SimpleFilterPart statusFilter = createSimpleFilter("Status", fileTriggerStatus);

            //Giving IsActive Filter properties to filter the search.
            ExactTarget.SimpleFilterPart isActiveFilter = createSimpleFilter("IsActive", "True");

            // Associating the simple filters into complex
            ExactTarget.ComplexFilterPart filterPart = CreateComplexFilter(statusFilter, isActiveFilter);

            return filterPart;
        }

        /// <summary>
        /// 1.Gets the response manifest from ET absed on the Exact Target ID
        /// 2.Updates the manifest based on the type of request.
        /// 3.Sends the updated manifest to ET for updation can be refleted at ET
        /// </summary>
        /// <param name="iRequest">Request object</param>
        /// <param name="configValues">Config values required to contact ET</param>
        /// <param name="user">user details needed to contact ET</param>
        /// <returns>Returns true if all goes well</returns>
        public static bool UpdateManifestResult(IRequest iRequest, InterchangeConfiguration configValues, InterchangeUser user, string[] fileNames = null)
        {
            if (iRequest == null) throw new ArgumentNullException("iRequest");
            if (configValues == null) throw new ArgumentNullException("configValues");

            EmailInterchangeCommon.Request request = (EmailInterchangeCommon.Request)iRequest;

            //Retrieve the FTO of the given RequestObject and set the status of the FTO as the Status of the Request Object.
            bool isDeltaProvided = false;
            if (request.DataEndpoints != null && request.DataEndpoints.Count > 0)
                isDeltaProvided = request.DataEndpoints[0].IsDeltaProvided;

            ExactTarget.FileTrigger fileTrigger = Communicator.RetrieveFileTriggerByObjectID(iRequest.ExactTargetRequestId.ToString(), isDeltaProvided, configValues, user);

            if (fileTrigger == null) throw new InvalidOperationException();

            fileTrigger.Status = RequestStatus.ExternalComplete.ToString();

            //If the new status is ExternalComplete, then populate the Filename Property in the FTO
            // and update the manifest.  
            bool status = ManifestHandler(request, ref fileTrigger, fileNames);

            if (!status)
                throw new InvalidOperationException("Response Manifest updation failed");

            // Set the complete status message when request is executed successfully
            if (fileTrigger.Status == RequestStatus.Complete.ToString() || fileTrigger.Status == RequestStatus.ExternalComplete.ToString())
            {

                fileTrigger.StatusMessage = "Success";
            }

            #region Updating in ET

            string returnStatus = null;
            string returnRequestID = null;
            //Initialize the web service proxy
            ExactTarget.PartnerAPI partnerAPI = getPartnerAPIObject(configValues, user);
            ExactTarget.UpdateResult[] Updresults = null;
            Updresults = partnerAPI.Update(new ExactTarget.UpdateOptions(), new ExactTarget.APIObject[] { fileTrigger }, out returnRequestID, out returnStatus);

            DataAccess.AddXmlSoapRequestResponse(
                        connectionString: configValues.InterchangeDBConnectionString,
                        apiMethod: "Update", stackMethod: System.Reflection.MethodBase.GetCurrentMethod().Name, apiAction: "", apiOptions: XmlHelper.SerializeIt(new UpdateOptions()),
                        apiObjects: XmlHelper.SerializeIt(new APIObject[] { fileTrigger }),
                        additionalInformation: "<Dimensions><RequestId>" + returnRequestID + "</RequestId><OverallStatus>" + returnStatus + "</OverallStatus></Dimensions>",
                        apiResults: XmlHelper.SerializeIt(Updresults));

            if (returnStatus == "Error")
            {
                string results = string.Empty;
                if (Updresults != null)
                {
                    foreach (ExactTarget.UpdateResult eachResult in Updresults)
                    {
                        results += string.Format(CultureInfo.CurrentCulture, StringResources.DetailXMLStscodeStsmessageErrorcode, eachResult.ResultDetailXML, eachResult.StatusCode, eachResult.StatusMessage, eachResult.ErrorCode);
                    }
                }

                throw new InvalidOperationException(StringResources.ReturnStatusfromETonUpdateErrorWithResults + results);
            }

            #endregion

            return true;
        }

        /// <summary>
        /// Based ont he request type, the respective method is called to update the manifest
        /// </summary>
        /// <param name="request">The request object needed to update the manifest</param>
        /// <param name="fileTrigger">The Exact Target object that contains the manifest</param>        
        /// <returns>Returns true is the manifest was updated </returns>
        private static bool ManifestHandler(EmailInterchangeCommon.Request request, ref FileTrigger fileTrigger, string[] fileNames = null)
        {
            int clientId = Convert.ToInt32(request.AccountId);
            bool statusCheck = false;

            switch (request.RequestType)
            {
                case requestEnum.RequestType.PromotionalListData:
                    //Update the Manifest
                    statusCheck = UpdateManifestPromotionalListData(ref fileTrigger, request);
                    break;

                case requestEnum.RequestType.SuppressionPromotionalData:
                case requestEnum.RequestType.SuppressionTransactionalData:
                    //Update the manifest
                    statusCheck = UpdateManifestSuppressionData(ref fileTrigger, request);
                    break;

                case requestEnum.RequestType.CampaignMetaData:
                    statusCheck = UpdateManifestCampaignMetaData(ref fileTrigger, request, fileNames);
                    break;
            }

            if (!statusCheck)
            {
                return false;
            }

            return true;
        }

        private static bool UpdateManifestCampaignMetaData(ref FileTrigger fileTrigger, EmailInterchangeCommon.Request request, string[] fileNames = null)
        {
            try
            {
                int clientID = 0;

                if (!int.TryParse(request.AccountId, out clientID))
                {
                    return false;
                }

                if (request.LastPullDateTime != DateTime.MinValue)
                {
                    fileTrigger.LastPullDate = request.LastPullDateTime;
                    fileTrigger.LastPullDateSpecified = true;
                }

                //replacing the objectid tag with the actual value ETID
                string manifestStringRef = fileTrigger.ResponseControlManifest.Replace("{FileTriggerID}", request.ExactTargetRequestId.ToString());

                //Load the string from ResponseControlManifest into XmlDocument 'manifest'.
				var settings = new XmlReaderSettings();
				settings.DtdProcessing = DtdProcessing.Prohibit;
                settings.Schemas.Add(null, Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath) + Path.DirectorySeparatorChar + "ProgramManifestSchema.xsd");
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);  
				settings.XmlResolver = null;
				var reader = XmlReader.Create(new MemoryStream(Encoding.Unicode.GetBytes(manifestStringRef)), settings);
				var manifest = new XmlDocument();
				manifest.Load(reader);

                //Do the necessary modifications in the manifest.
                manifest.SelectSingleNode("./ProgramManifest/Client/ID").InnerText = clientID.ToString(System.Globalization.CultureInfo.InvariantCulture);
                //manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/OutputFile/Filename").InnerText = Path.GetFileName(request.ProcessingFileNameSpec);

                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/InputFile/Filename").InnerText = request.ExactTargetRequestId + ".aes";
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Filename").InnerText = string.Format("{0}{1}", request.EmailInterchangeRequestId.ToString(), ".zip");
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/OutputFile/Filename").InnerText = string.Format("{0}{1}", request.EmailInterchangeRequestId.ToString(), ".zip");
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Type").InnerText = CONSTANT_ZIP;


                XmlNodeList activity = null;
                activity = manifest.SelectNodes("./ProgramManifest/Tasks/Task/Activities/Activity");

                int counter = 0;
                string[] campaignMetaDataFileNames = fileNames;

                DataEndpoint dt = request.DataEndpoints[0];

                foreach (XmlNode node in activity)
                {
                    if ((node.FirstChild.Name == "ImportActivity") && (counter < campaignMetaDataFileNames.Length))
                    {
                        for (int count = 0; count < campaignMetaDataFileNames.Length; count++)
                        {
                            string[] fileName = campaignMetaDataFileNames[count].Split('_');

                            if (fileName[0].Equals(node.SelectSingleNode("./ImportActivity/DestinationObject/DataExtension/CustomerKey").InnerText))
                            {
                                node.SelectSingleNode("./ImportActivity/FileSpec").InnerText = campaignMetaDataFileNames[count].Replace("EmailInterchangeID", request.EmailInterchangeRequestId.ToString());
                                if (dt.IsDeltaProvided == true)
                                {
                                    node.SelectSingleNode("./ImportActivity/UpdateType").InnerText = "ColumnBased";
                                }
                                else
                                {
                                    node.SelectSingleNode("./ImportActivity/UpdateType").InnerText = "Overwrite";
                                }
                                break;
                            }
                        }
                        counter++;
                    }
                }

                //Transfer the manifest back into the ResponseControlManifest.
                fileTrigger.ResponseControlManifest = manifest.OuterXml.ToString(System.Globalization.CultureInfo.InvariantCulture);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An Exception occured while updating the manifest for CampaignMetaData with message: " + ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Update the response manifest for PromotionalListData type of requests
        /// </summary>
        /// <param name="fileTrigger">The Exact Target object that contains the manifest</param>
        /// <param name="request">The PromotionalListData type of request object needed to update the manifest</param>
        /// <param name="ClientID"></param>
        /// <returns>Retuens ture if manifest is updated successfully</returns>
        private static bool UpdateManifestPromotionalListData(ref ExactTarget.FileTrigger fileTrigger, EmailInterchangeCommon.Request request)
        {
            int clientID = 0;

            //replacing the objectid tag with the actual value ETID
            string manifestStringRef = fileTrigger.ResponseControlManifest.Replace("{FileTriggerID}", request.ExactTargetRequestId.ToString());

            //Load the string from ResponseControlManifest into XmlDocument 'manifest'.
			var settings = new XmlReaderSettings();
			settings.DtdProcessing = DtdProcessing.Prohibit;
            settings.Schemas.Add(null, Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath) + Path.DirectorySeparatorChar + "ProgramManifestSchema.xsd");
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);  
			settings.XmlResolver = null;
			var reader = XmlReader.Create(new MemoryStream(Encoding.Unicode.GetBytes(manifestStringRef)), settings);
			var manifest = new XmlDocument();
			manifest.Load(reader);

            if (!int.TryParse(request.AccountId, out clientID))
            {
                return false;
            }

            //Do the necessary modifications in the manifest.
            manifest.SelectSingleNode("./ProgramManifest/Client/ID").InnerText = clientID.ToString();
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/OutputFile/Filename").InnerText = Path.GetFileName(request.ProcessingFileNameSpec);
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/FileSpec").InnerText = Path.GetFileName(request.ProcessingFileNameSpec);
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/DestinationObject/DataExtension/CustomerKey").InnerText = request.DataEndpoints[0].Name;
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/ImportNotificationAddress").InnerText = request.NotificationEmailAddress;

            DataEndpoint dt = request.DataEndpoints[0];
            if (dt.IsDeltaProvided == true)
            {
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/UpdateType").InnerText = "AddAndUpdate";
            }

            else
            {
                manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/UpdateType").InnerText = "Overwrite";
            }

            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/InputFile/Filename").InnerText = request.ExactTargetRequestId + ".aes";//request.ProcessingFileNameSpec;//request.ProcessingFileName;

            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Filename").InnerText = string.Format("{0}{1}", request.EmailInterchangeRequestId.ToString(), ".zip");
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/OutputFile/Filename").InnerText = string.Format("{0}{1}", request.EmailInterchangeRequestId.ToString(), ".zip");
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Type").InnerText = CONSTANT_ZIP;

            //Transfer the manifest back into the ResponseControlManifest.
            fileTrigger.ResponseControlManifest = manifest.OuterXml.ToString(CultureInfo.InvariantCulture);

            return true;
        }

        /// <summary>
        /// Update the response manifest for PromotionalListData type of requests
        /// </summary>
        /// <param name="fileTrigger">The Exact Target object that contains the manifest</param>
        /// <param name="request">The SupressuinalPromotional/SupressuinalTransactional type of request object 
        /// needed to update the manifest</param>
        /// <param name="ClientID"></param>
        /// <returns>Retuens ture if manifest is updated successfully</returns>
        private static bool UpdateManifestSuppressionData(ref ExactTarget.FileTrigger fileTrigger, EmailInterchangeCommon.Request request)
        {
            int clientID = 0;

            //replacing the objectid tag with the actual value ETID
            string manifestStringRef = fileTrigger.ResponseControlManifest.Replace("{FileTriggerID}", request.ExactTargetRequestId.ToString());

            //Load the string from ResponseControlManifest into XmlDocument 'manifest'.
			var settings = new XmlReaderSettings();
			settings.DtdProcessing = DtdProcessing.Prohibit;
            settings.Schemas.Add(null, Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath) + Path.DirectorySeparatorChar + "ProgramManifestSchema.xsd");
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);  
			settings.XmlResolver = null;
			var reader = XmlReader.Create(new MemoryStream(Encoding.Unicode.GetBytes(manifestStringRef)), settings);
			var manifest = new XmlDocument();
			manifest.Load(reader);

            if (!int.TryParse(request.AccountId, out clientID))
            {
                return false;
            }

            //Do the necessary modifications in the manifest.            
            manifest.SelectSingleNode("./ProgramManifest/Client/ID").InnerText = clientID.ToString();
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/OutputFile/Filename").InnerText = Path.GetFileNameWithoutExtension(request.ProcessingFileNameSpec);
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/ImportActivity/FileSpec").InnerText = Path.GetFileNameWithoutExtension(request.ProcessingFileNameSpec);

            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/InputFile/Filename").InnerText = request.ExactTargetRequestId + ".aes";

            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Filename").InnerText = string.Format("{0}{1}", request.EmailInterchangeRequestId, ".zip");
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecryptActivity/Resources/OutputFile/Filename").InnerText = string.Format("{0}{1}", request.EmailInterchangeRequestId, ".zip");
            manifest.SelectSingleNode("./ProgramManifest/Tasks/Task/Activities/Activity/DecompressActivity/Resources/InputFile/Type").InnerText = CONSTANT_ZIP;

            //Transfer the manifest back into the ResponseControlManifest.
            fileTrigger.ResponseControlManifest = manifest.OuterXml.ToString(System.Globalization.CultureInfo.InvariantCulture);

            return true;
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
        }
    }
}
