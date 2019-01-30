using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
namespace Microsoft.Windows.Services.AuthN.Client
{
	[DataContract]
	public class AppTicket
	{
		private const int FiveMinutesInSeconds = 300;
		[DataMember(Name = "access_token")]
		public string AccessToken
		{
			get;
			set;
		}
		[DataMember(Name = "token_type")]
		public string TokenType
		{
			get;
			set;
		}
		[DataMember(Name = "expires_in")]
		private int ExpiresIn
		{
			get;
			set;
		}
		[DataMember(Name = "error")]
		public string Error
		{
			get;
			set;
		}
		[DataMember(Name = "error_description")]
		public string ErrorMessage
		{
			get;
			set;
		}
		public DateTimeOffset TokenIssueTimeUtc
		{
			get;
			set;
		}
		public TimeSpan ValidFor
		{
			get
			{
				return TimeSpan.FromSeconds((double)(this.ExpiresIn - 300));
			}
		}
		public static AppTicket FromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			AppTicket result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(AppTicket));
				AppTicket appTicket = (AppTicket)dataContractJsonSerializer.ReadObject(memoryStream);
				appTicket.TokenIssueTimeUtc = DateTimeOffset.UtcNow;
				result = appTicket;
			}
			return result;
		}
	}
}
