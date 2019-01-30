using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
namespace Microsoft.Windows.Services.AuthN.Client.S2S
{
	public class S2SAuthClient : IS2SAuthClient
	{
		private const string ScopeFormat = "{0}::{1}";
		private const string OAuthRequestBodyFormat = "grant_type=client_credentials&client_id={0}&scope={1}";
		private const string GetAccessTokenFailedErrorMessageFormat = "Failed to get S2S Access Token for ticket scope '{0}' and site id '{1}'.";
		private static readonly MemoryCache ClientCache = new MemoryCache("ClientCache", null);
		private static readonly object TicketCacheLock = new object();
		private static readonly object ClientCacheLock = new object();
		private static MemoryCache ticketCache = new MemoryCache("InitialTicketCache", null);
		private readonly IWebClient webClient;
		private readonly Uri msaAuthenticationUrl;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public readonly long ClientSiteId;
		public X509Certificate2 ClientCertificate
		{
			get;
			private set;
		}
		public static TimeSpan? MaxTokenLifetime
		{
			get;
			set;
		}
		private S2SAuthClient(long clientSiteId, X509Certificate2 clientCertificate, Uri msaAuthenticationUrl, IWebClient webClient)
		{
			if (clientCertificate == null)
			{
				throw new ArgumentNullException("clientCertificate");
			}
			if (msaAuthenticationUrl == null)
			{
				throw new ArgumentNullException("msaAuthenticationUrl");
			}
			this.ClientSiteId = clientSiteId;
			this.ClientCertificate = clientCertificate;
			this.msaAuthenticationUrl = msaAuthenticationUrl;
			this.webClient = (webClient ?? new HttpWebClient());
		}
		private S2SAuthClient(long clientSiteId, X509Certificate2 clientCertificate, Uri msaAuthenticationUrl, HttpClient httpClient)
		{
			if (msaAuthenticationUrl == null)
			{
				throw new ArgumentNullException("msaAuthenticationUrl");
			}
			if (clientCertificate == null)
			{
				throw new ArgumentNullException("clientCertificate");
			}
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			this.ClientSiteId = clientSiteId;
			this.ClientCertificate = clientCertificate;
			this.msaAuthenticationUrl = msaAuthenticationUrl;
			this.webClient = new HttpWebClient(httpClient);
		}
		public static S2SAuthClient CreateWithCertificateByThumbprint(long clientSiteId, string clientCertificateThumbprint, Uri msaAuthenticationUrl, IWebClient webClient = null)
		{
			if (string.IsNullOrEmpty(clientCertificateThumbprint))
			{
				throw new ArgumentException("Client certificate thumbprint cannot be null");
			}
			X509Certificate2 certificate = CertificateFinder.FindCertificateByThumbprint(clientCertificateThumbprint, true, true);
			return S2SAuthClient.Create(clientSiteId, certificate, msaAuthenticationUrl, webClient);
		}
		public static S2SAuthClient CreateWithCertificateBySerialNumber(long clientSiteId, string clientCertificateSerialNumber, Uri msaAuthenticationUrl, IWebClient webClient = null)
		{
			if (string.IsNullOrEmpty(clientCertificateSerialNumber))
			{
				throw new ArgumentException("Client certificate serial number cannot be null");
			}
			X509Certificate2 certificate = CertificateFinder.FindCertificateBySerialNumber(clientCertificateSerialNumber, true, true);
			return S2SAuthClient.Create(clientSiteId, certificate, msaAuthenticationUrl, webClient);
		}
		public static S2SAuthClient CreateWithCertificateByName(long clientSiteId, string clientCertificateName, Uri msaAuthenticationUrl, IWebClient webClient = null)
		{
			if (string.IsNullOrEmpty(clientCertificateName))
			{
				throw new ArgumentException("Client certificate name cannot be null");
			}
			X509Certificate2 certificate = CertificateFinder.FindCertificateByName(clientCertificateName, true, true);
			return S2SAuthClient.Create(clientSiteId, certificate, msaAuthenticationUrl, webClient);
		}
		public static S2SAuthClient Create(long clientSiteId, X509Certificate2 certificate, Uri msaAuthenticationUrl, IWebClient webClient = null)
		{
			string key = clientSiteId + "-" + certificate.GetSerialNumberString();
			S2SAuthClient s2SAuthClient = S2SAuthClient.ClientCache.Get(key, null) as S2SAuthClient;
			if (s2SAuthClient != null)
			{
				return s2SAuthClient;
			}
			S2SAuthClient result;
			lock (S2SAuthClient.ClientCacheLock)
			{
				if (S2SAuthClient.ClientCache.Get(key, null) == null)
				{
					s2SAuthClient = new S2SAuthClient(clientSiteId, certificate, msaAuthenticationUrl, webClient);
					S2SAuthClient.ClientCache.Add(key, s2SAuthClient, new CacheItemPolicy(), null);
				}
				result = (S2SAuthClient.ClientCache.Get(key, null) as S2SAuthClient);
			}
			return result;
		}
		public static S2SAuthClient Create(long clientSiteId, X509Certificate2 certificate, Uri msaAuthenticationUrl, HttpClient httpClient)
		{
			return new S2SAuthClient(clientSiteId, certificate, msaAuthenticationUrl, httpClient);
		}
		public static void PurgeTicketCache()
		{
			S2SAuthClient.ticketCache = new MemoryCache("ticketCache:" + Guid.NewGuid(), null);
		}
		private async Task<AppTicket> GetAppTicketAsync(string ticketScope, Action<S2SClientEventInfo> instrumentationCallback, CancellationToken cancellationToken)
		{
			string text = HttpUtility.UrlEncode(this.ClientSiteId.ToString(CultureInfo.InvariantCulture));
			string text2 = HttpUtility.UrlEncode(ticketScope);
			string body = string.Format(CultureInfo.InvariantCulture, "grant_type=client_credentials&client_id={0}&scope={1}", new object[]
			{
				text,
				text2
			});
			return await webClient.GetAppTicketAsync(msaAuthenticationUrl, body, ClientCertificate, cancellationToken, instrumentationCallback).ConfigureAwait(false);
		}
		public async Task<string> GetAccessTokenAsync(string targetSite, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback = null)
		{
			return await GetAccessTokenAsync(targetSite, "S2S_24HOURS_MUTUALSSL", cancellationToken, instrumentationCallback).ConfigureAwait(false);
		}
		public async Task<string> GetAccessTokenAsync(string targetSite, string authPolicy, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback = null)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0}::{1}", new object[]
			{
				targetSite,
				authPolicy
			});
			string key = this.ClientSiteId + "-" + text;
			MemoryCache memoryCache = S2SAuthClient.ticketCache;
			AppTicket appTicket = memoryCache.Get(key, null) as AppTicket;
			string result;
			if (appTicket != null)
			{
				result = appTicket.AccessToken;
			}
			else
			{
				try
				{
					AppTicket appTicket2 = await this.GetAppTicketAsync(text, instrumentationCallback, cancellationToken).ConfigureAwait(false);
					lock (S2SAuthClient.TicketCacheLock)
					{
						appTicket = (memoryCache.Get(key, null) as AppTicket);
						if (appTicket == null)
						{
							DateTimeOffset dateTimeOffset = appTicket2.TokenIssueTimeUtc;
							TimeSpan? maxTokenLifetime = S2SAuthClient.MaxTokenLifetime;
							if (maxTokenLifetime.HasValue && maxTokenLifetime < appTicket2.ValidFor)
							{
								dateTimeOffset += maxTokenLifetime.Value;
							}
							else
							{
								dateTimeOffset += appTicket2.ValidFor;
							}
							memoryCache.Add(key, appTicket2, new CacheItemPolicy
							{
								AbsoluteExpiration = dateTimeOffset
							}, null);
						}
					}
					appTicket = (memoryCache.Get(key, null) as AppTicket);
					result = ((appTicket != null) ? appTicket.AccessToken : null);
				}
				catch (Exception innerException)
				{
					throw new S2SAuthException(S2SAuthErrorCode.GetAccessTokenFailed, string.Format("Failed to get S2S Access Token for ticket scope '{0}' and site id '{1}'.", text, this.ClientSiteId), innerException);
				}
			}
			return result;
		}
	}
}
