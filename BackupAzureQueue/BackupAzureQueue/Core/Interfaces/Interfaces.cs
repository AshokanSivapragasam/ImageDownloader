using System;
using System.Collections.Generic;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
using System.Collections.ObjectModel;
using System.Threading;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces
{
    /// <summary>
    /// IResponseDispatcherClient Interface
    /// </summary>
    /// <typeparam name="TRequestObject"></typeparam>
    public interface IResponseDispatcherClient<TRequestObject>
    {
        /// <summary>
        /// SendResponse Method 
        /// </summary>
        /// <param name="newPacket">new Packet</param>
        void SendResponse(TRequestObject newPacket);
        
        /// <summary>
        /// GetRequest Method
        /// </summary>
        /// <returns>TRequestObject</returns>
        TRequestObject GetRequest();
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceManagement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionEngineId"></param>
        /// <param name="transactionType"></param>
        /// <param name="requestType"></param>
        void CommitResource(Guid executionEngineId, TransactionType transactionType, RequestType requestType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionEngineId"></param>
        /// <param name="transactionType"></param>
        /// <param name="requestType"></param>
        void FreeResource(Guid executionEngineId, TransactionType transactionType, RequestType requestType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionEngineId"></param>
        /// <param name="transactionType"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        bool CheckAvailability(Guid executionEngineId, TransactionType transactionType, RequestType requestType);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceManagementData
    {
        /// <summary>
        /// 
        /// </summary>
        Collection<RequestTypeSourceMapEntry> RequestTypeSourceMapEntryCollection
        {
            get;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //Collection<SourceMaxSessionEntry> SourceMaxSessionEntryCollection
        //{
        //    get;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //Collection<CurrentDataStoreSessionEntry> CurrentDataStoreSessionEntryCollection
        //{
        //    get;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //Collection<ThreadManagementLookupEntry> ThreadManagementEntryCollection
        //{
        //    get;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //Collection<CurrentThreadManagementEntry> CurrentThreadManagementEntryCollection
        //{
        //    get;
        //}
        /// <summary>
        /// UpdateResourceAllocation Method
        /// </summary>
        /// <param name="resourceInstance">ResourceInstance</param>
        void UpdateResourceAllocation(ResourceDetails resourceInstance);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequestObject"></typeparam>
    public interface IActivity<TRequestObject>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRequest"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        ActivityStatus ProcessRequest(ref TRequestObject newRequest, Dictionary<String, String> configuration);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRequests"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        ActivityStatus ProcessRequests(ref TRequestObject[] newRequests, Dictionary<String, String> configuration);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequestObject"></typeparam>
    public interface IExecutionAdapter<TRequestObject>
    {
        /// <summary>
        /// ProcessRequest Method
        /// </summary>
        /// <param name="newRequest"></param>
        void ProcessRequest(TRequestObject newRequest);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequestObject"></typeparam>
    public interface IRequestPacket<TRequestObject>
    {
        /// <summary>
        /// Gets the RequestId
        /// </summary>
        Guid RequestId
        {
            get;
        }
        /// <summary>
        /// Gets the RequestType
        /// </summary>
        RequestType RequestType
        {
            get;
        }
        /// <summary>
        /// Gets or Sets the TransactionType
        /// </summary>
        TransactionType TransactionType
        {
            get;
        }
        /// <summary>
        /// Gets or Sets the Request
        /// </summary>
        TRequestObject Request
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the ProcessStatus
        /// </summary>
        ProcessStatus ProcessStatus
        {
            get;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurationReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns></returns>
        Dictionary<String, String> GetConfigurationList(ComponentType componentName);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        String GetConfigurationValue(ComponentType componentName, String keyName);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IRequestStateManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Collection<RequestPacket> GetIncompleteRequests(ComponentType componentType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestPacket"></param>
        /// <param name="executionState"></param>
        /// <param name="currentComponentId"></param>
        void SaveState(RequestPacket requestPacket, RequestExecutionStatus executionState, Int32 currentComponentId);
    }

    /// <summary>
    /// Interface for all poll mechanisms in Email Interchange
    /// </summary>
    public interface IPollEngine
    {
        /// <summary>
        /// The invterval at which the Poll Engine should burst
        /// </summary>
        System.Timers.Timer PollTimer();

        /// <summary>
        /// Performs poll operations
        /// </summary>
        void Ping();

        /// <summary>
        /// Reloads the configuration values from the repository
        /// </summary>
        void RefreshConfigurations();

    }
}
