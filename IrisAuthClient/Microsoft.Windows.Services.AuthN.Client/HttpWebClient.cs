using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
namespace Microsoft.Windows.Services.AuthN.Client
{
	internal class HttpWebClient : IWebClient
	{
		private readonly HttpClient httpClient;
		internal HttpWebClient()
		{
		}
		internal HttpWebClient(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}
		public async Task<AppTicket> GetAppTicketAsync(Uri url, string body, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback)
		{
			return await this.GetAppTicketAsync(url, body, null, cancellationToken, instrumentationCallback);
		}
		public async Task<AppTicket> GetAppTicketAsync(Uri url, string body, X509Certificate2 clientCertificate, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback)
		{
			StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
			DateTime now = DateTime.Now;
			HttpResponseMessage httpResponseMessage;
			if (this.httpClient == null)
			{
				using (HttpClient httpClient = new HttpClient(new S2SWebRequestHandler(clientCertificate)))
				{
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    httpResponseMessage = await httpClient.PostAsync(url, stringContent, cancellationToken).ConfigureAwait(false);
					goto IL_1B9;
				}
			}
			httpResponseMessage = await this.httpClient.PostAsync(url, stringContent, cancellationToken).ConfigureAwait(false);
			IL_1B9:
			TimeSpan duration = DateTime.Now - DateTime.Now.AddSeconds(-2);
			AppTicket appTicket = new AppTicket();
			if (httpResponseMessage.Content != null)
			{
				appTicket = AppTicket.FromJson(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
			}
            
			if (instrumentationCallback != null)
			{
				instrumentationCallback(new S2SClientEventInfo
				{
					Succeeded = httpResponseMessage.IsSuccessStatusCode,
					Duration = duration,
					Message = appTicket.ErrorMessage,
					ErrorCode = appTicket.Error,
					TargetUri = url.AbsoluteUri,
					RequestMethod = "POST",
					ResponseContentType = (httpResponseMessage.Content != null) ? httpResponseMessage.Content.Headers.ContentType.ToString() : string.Empty,
					DependencyType = "MSA",
					DependencyOperationVersion = httpResponseMessage.Version.ToString(),
					ResponseSizeBytes = (httpResponseMessage.Content != null && httpResponseMessage.Content.Headers.ContentLength.HasValue) ? ((int)httpResponseMessage.Content.Headers.ContentLength.Value) : 0,
					Protocol = "HTTP",
					ProtocolStatusCode = httpResponseMessage.StatusCode.ToString()
				});
			}
			if (!httpResponseMessage.IsSuccessStatusCode && !string.IsNullOrEmpty(appTicket.Error))
			{
				string arg = string.Format("App ticket error: {0}. Message: {1}.", appTicket.Error, appTicket.ErrorMessage);
				throw new HttpRequestException(string.Format("{0}:{1}. {2}", (int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, arg));
			}
			httpResponseMessage.EnsureSuccessStatusCode();
			return appTicket;
		}
	}
}
