using System;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
namespace Microsoft.Windows.Services.AuthN.Client
{
	public class S2SWebRequestHandler : WebRequestHandler
	{
		public S2SWebRequestHandler(X509Certificate2 clientCertificate)
		{
			base.AuthenticationLevel = AuthenticationLevel.MutualAuthRequired;
			base.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			if (clientCertificate != null)
			{
				base.ClientCertificateOptions = ClientCertificateOption.Manual;
				base.ClientCertificates.Add(clientCertificate);
			}
		}
	}
}
