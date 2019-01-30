using Microsoft.Windows.Services.AuthN.Client.S2S;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
namespace Microsoft.Windows.Services.AuthN.Client
{
	public static class CertificateFinder
	{
		public static X509Certificate2 FindCertificateByThumbprint(string thumbprint, bool validOnly = true, bool throwIfMultipleOrNoMatch = false)
		{
			if (string.IsNullOrEmpty(thumbprint))
			{
				throw new ArgumentException("Serial number cannot be null or empty");
			}
			return CertificateFinder.Find<string>(thumbprint, validOnly, new Func<StoreName, StoreLocation, bool, string, bool, X509Certificate2>(CertificateFinder.FindCertificateByThumbprint), throwIfMultipleOrNoMatch);
		}
		public static X509Certificate2 FindCertificateBySerialNumber(string serialNumber, bool validOnly = true, bool throwIfMultipleOrNoMatch = false)
		{
			if (string.IsNullOrEmpty(serialNumber))
			{
				throw new ArgumentException("Serial number cannot be null or empty");
			}
			return CertificateFinder.Find<string>(serialNumber, validOnly, new Func<StoreName, StoreLocation, bool, string, bool, X509Certificate2>(CertificateFinder.FindCertificateBySerialNumber), throwIfMultipleOrNoMatch);
		}
		public static X509Certificate2 FindCertificateByName(string name, bool validOnly = true, bool throwIfMultipleOrNoMatch = false)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name cannot be null or empty");
			}
			return CertificateFinder.Find<string>(name, validOnly, new Func<StoreName, StoreLocation, bool, string, bool, X509Certificate2>(CertificateFinder.FindCertificateByName), throwIfMultipleOrNoMatch);
		}
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.String.Equals(System.String,System.StringComparison)")]
		public static X509Certificate2 FindCertificateByThumbprint(StoreName storeName, StoreLocation storeLocation, bool validOnly, string thumbprint, bool throwIfMultipleOrNoMatch = false)
		{
			if (string.IsNullOrEmpty(thumbprint))
			{
				throw new ArgumentException("Serial number cannot be null or empty");
			}
			return CertificateFinder.Find(storeName, storeLocation, X509FindType.FindByThumbprint, validOnly, thumbprint, throwIfMultipleOrNoMatch);
		}
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.String.Equals(System.String,System.StringComparison)")]
		public static X509Certificate2 FindCertificateBySerialNumber(StoreName storeName, StoreLocation storeLocation, bool validOnly, string serialNumber, bool throwIfMultipleOrNoMatch = false)
		{
			if (string.IsNullOrEmpty(serialNumber))
			{
				throw new ArgumentException("Serial number cannot be null or empty");
			}
			return CertificateFinder.Find(storeName, storeLocation, X509FindType.FindBySerialNumber, validOnly, serialNumber, throwIfMultipleOrNoMatch);
		}
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.String.IndexOf(System.String,System.StringComparison)")]
		public static X509Certificate2 FindCertificateByName(StoreName storeName, StoreLocation storeLocation, bool validOnly, string name, bool throwIfMultipleOrNoMatch = false)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty");
			}
			return CertificateFinder.Find(storeName, storeLocation, X509FindType.FindBySubjectName, validOnly, name, throwIfMultipleOrNoMatch);
		}
		public static X509Certificate2 Find(StoreName storeName, StoreLocation storeLocation, X509FindType findType, bool validOnly, object findValue, bool throwIfMultipleOrNoMatch = false)
		{
			X509Certificate2 x509Certificate = null;
			X509Store x509Store = null;
			try
			{
				x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(findType, findValue, validOnly);
				x509Certificate = ((x509Certificate2Collection.Count == 1) ? x509Certificate2Collection[0] : null);
				if (throwIfMultipleOrNoMatch && x509Certificate == null)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "Could not find the client certificate in storeName: {0}, storeLocation: {1}, findType : {2}, validOnly: {3}, findValue: {4}", new object[]
					{
						storeName,
						storeLocation,
						findType,
						validOnly,
						findValue
					});
					throw new S2SAuthException(S2SAuthErrorCode.CannotFindCertificate, message);
				}
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return x509Certificate;
		}
		private static X509Certificate2 Find<T>(T value, bool validOnly, Func<StoreName, StoreLocation, bool, T, bool, X509Certificate2> func, bool throwIfMultipleOrNoMatch = false)
		{
			X509Certificate2 x509Certificate = func(StoreName.My, StoreLocation.LocalMachine, true, value, false) ?? func(StoreName.My, StoreLocation.CurrentUser, true, value, false);
			if (x509Certificate != null || validOnly)
			{
				return x509Certificate;
			}
			X509Certificate2 x509Certificate2 = func(StoreName.My, StoreLocation.LocalMachine, false, value, false) ?? func(StoreName.My, StoreLocation.CurrentUser, false, value, false);
			if (throwIfMultipleOrNoMatch && x509Certificate2 == null)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "Could not find the client certificate in StoreName.My and location 'LocalMachine' nor 'CurrentUser' findValue: {0}, validOnly: {1}", new object[]
				{
					value,
					validOnly
				});
				throw new S2SAuthException(S2SAuthErrorCode.CannotFindCertificate, message);
			}
			return x509Certificate2;
		}
	}
}
