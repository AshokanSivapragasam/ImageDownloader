using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
namespace Microsoft.Windows.Services.AuthN.Client.S2S
{
	public interface IS2SAuthClient
	{
		X509Certificate2 ClientCertificate
		{
			get;
		}
		Task<string> GetAccessTokenAsync(string targetSite, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback = null);
		Task<string> GetAccessTokenAsync(string targetSite, string authPolicy, CancellationToken cancellationToken, Action<S2SClientEventInfo> instrumentationCallback = null);
	}
}
