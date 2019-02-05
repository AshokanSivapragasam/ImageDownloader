using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace BackupAzureQueue
{
    public static class SecretsManager
    {
        /// <summary>
        /// Gets or sets certificate that will be used to decrypt the cypher text
        /// </summary>
        private static X509Certificate2 Certificate { get; set; }

        /// <summary>
        /// Decrypt cypher test with secret certificate
        /// </summary>
        /// <param name="encryptedString">Cypher text</param>
        /// <param name="secretCertificateThumbprint"></param>
        /// <returns>Returns plain text</returns>
        public static string Decrypt(string encryptedString, string secretCertificateThumbprint)
        {
            if (null == Certificate)
            {
                X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                x509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, secretCertificateThumbprint, false);
                Certificate = certificateCollection[0];
            }

            byte[] cipherbytes = Convert.FromBase64String(encryptedString);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)Certificate.PrivateKey;
            byte[] plainbytes = rsa.Decrypt(cipherbytes, false);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(plainbytes);
        }
    }
}
