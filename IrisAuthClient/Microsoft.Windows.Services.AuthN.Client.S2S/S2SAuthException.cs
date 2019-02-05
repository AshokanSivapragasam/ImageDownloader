using System;
using System.Runtime.Serialization;
namespace Microsoft.Windows.Services.AuthN.Client.S2S
{
	[Serializable]
	public class S2SAuthException : Exception
	{
		public S2SAuthErrorCode ErrorCode
		{
			get;
			set;
		}
		public S2SAuthException()
		{
		}
		public S2SAuthException(string message) : base(message)
		{
		}
		public S2SAuthException(string message, Exception ex) : base(message, ex)
		{
		}
		protected S2SAuthException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
		public S2SAuthException(S2SAuthErrorCode errorCode) : this(errorCode, string.Empty, null)
		{
		}
		public S2SAuthException(S2SAuthErrorCode errorCode, string message) : this(errorCode, message, null)
		{
		}
		public S2SAuthException(S2SAuthErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
