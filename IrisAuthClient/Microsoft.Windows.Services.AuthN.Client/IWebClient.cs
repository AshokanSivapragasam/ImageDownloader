using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
namespace Microsoft.Windows.Services.AuthN.Client
{
	public interface IWebClient
	{
		Task<AppTicket> GetAppTicketAsync(Uri url, string body, X509Certificate2 clientCertificate, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback = null);
		Task<AppTicket> GetAppTicketAsync(Uri url, string body, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback = null);
	}
}
