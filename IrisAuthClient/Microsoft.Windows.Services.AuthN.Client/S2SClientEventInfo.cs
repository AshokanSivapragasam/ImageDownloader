using System;
using System.Diagnostics.CodeAnalysis;
namespace Microsoft.Windows.Services.AuthN.Client
{
	public class S2SClientEventInfo
	{
		public bool Succeeded
		{
			get;
			set;
		}
		public string ErrorCode
		{
			get;
			set;
		}
		public string Message
		{
			get;
			set;
		}
		public TimeSpan Duration
		{
			get;
			set;
		}
		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Used for logging")]
		public string TargetUri
		{
			get;
			set;
		}
		public int ServiceErrorCode
		{
			get;
			set;
		}
		public string RequestMethod
		{
			get;
			set;
		}
		public string ResponseContentType
		{
			get;
			set;
		}
		public string Protocol
		{
			get;
			set;
		}
		public string ProtocolStatusCode
		{
			get;
			set;
		}
		public string DependencyOperationVersion
		{
			get;
			set;
		}
		public string DependencyType
		{
			get;
			set;
		}
		public int ResponseSizeBytes
		{
			get;
			set;
		}
	}
}
