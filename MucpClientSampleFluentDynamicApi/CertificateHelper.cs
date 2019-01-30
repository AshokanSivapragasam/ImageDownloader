// <copyright>
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using System;
using System.Security.Cryptography.X509Certificates;

namespace MucpClientSample
{
    public static class CertificateHelper
    {
        private const string MyStore = "My";

        /// <summary>
        /// Gets the certificate by thumbprint.
        /// </summary>
        /// <param name="thumbprint">The certificate thumbprint.</param>
        /// <param name="location">The location.</param>
        /// <returns>The certificate</returns>
        public static X509Certificate2 GetCertificateByThumbprint(string thumbprint, StoreLocation location)
        {
            return GetCertificate(X509FindType.FindByThumbprint, thumbprint, location);
        }

        /// <summary>
        /// Gets the certificate by subject name
        /// </summary>
        /// <param name="subjectName">The certificate subject name.</param>
        /// <param name="location">The location.</param>
        /// <returns>The certificate</returns>
        public static X509Certificate2 GetCertificateBySubjectName(string subjectName, StoreLocation location)
        {
            return GetCertificate(X509FindType.FindBySubjectName, subjectName, location);
        }

        private static X509Certificate2 GetCertificate(X509FindType findType, string findValue, StoreLocation location)
        {
            DateTime furthestExpirey = DateTime.MinValue;
            X509Certificate2 certificate = null;
            X509Store certStore = new X509Store(MyStore, location);
            try
            {
                certStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                X509CertificateCollection certs = certStore.Certificates.Find(findType, findValue, false);
                foreach (X509Certificate2 cert in certs)
                {
                    if (cert != null)
                    {
                        if (cert.NotAfter >= furthestExpirey)
                        {
                            certificate = cert;
                            furthestExpirey = cert.NotAfter;
                        }
                    }
                }
            }
            finally
            {
                certStore.Close();
            }

            return certificate;
        }


    }
}
