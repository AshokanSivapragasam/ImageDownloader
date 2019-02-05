using Jscape.Sftp;
using Jscape.Ssh;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xceed.Zip;

namespace Net35Projects
{
    class Program
    {
        /// <summary>
        /// <c>SqlConnection</c> object used to connect to database
        /// </summary>
        SqlConnection sqlConnection;

        /// <summary>
        /// <c>SqlCommand</c> object used to connect to database
        /// </summary>
        SqlCommand sqlCommand = null;

        static string logFile = string.Empty;

        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine(AppConstants.Default.GlobalNameOfApplication);
            ConfigurationManager.AppSettings["b"] = ConfigurationManager.AppSettings["a"];
            Console.WriteLine(ConfigurationManager.AppSettings["b"]);

            FileInfo fileDesc = new FileInfo(@"\\azeidevsql01\Backup\X16-49583VS2010Agents1.iso");

            try
            {
                    // try to open the file, to check it is available
                    fileDesc.OpenRead().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var choice = args[0];
            Console.WriteLine("'" + choice + "'");

            try
            {
                switch (choice)
                {
                    case "OBJCACHE":
                        {
                            #region OBJCACHE
                            string key = args[1];
                            ObjectCache cache = MemoryCache.Default;
                            DataRow userAttributes = null;
                            if (cache.Contains(key))
                            {
                                DataSet ds = new DataSet();
                                ds = (DataSet)cache.Get(key);

                                if (ds.Tables.Count > 0)
                                {
                                    userAttributes = (from r in ds.Tables[0].AsEnumerable()
                                                      where r.Field<Int32>("AccountId") == 39327
                                                      select r).FirstOrDefault();
                                    Console.WriteLine(p.Decrypt(userAttributes["SFTPUserName"].ToString(), "1B647BE314FBC0E8ECCD885C3442A9E63C7EB5B5"));
                                    Console.WriteLine(p.Decrypt(userAttributes["SFTPPassword"].ToString(), "1B647BE314FBC0E8ECCD885C3442A9E63C7EB5B5"));
                                }
                                Console.WriteLine("Gotcha");
                            }
                            else
                            {
                                CacheItemPolicy policy = new CacheItemPolicy();

                                policy.Priority = CacheItemPriority.Default;
                                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(Convert.ToDouble(300));

                                cache.Set(key, new DataSet(), policy);
                                Console.WriteLine("Added");
                            }
                            Console.WriteLine("Completed");
                            #endregion

                            break;
                        }

                    case "SQLTEST":
                        {
                            #region SQLTEST
                            try
                            {
                                string connectionString = args[1];
                                string cmdText = args[2];
                                SqlConnection sql = new SqlConnection(connectionString);
                                sql.Open();

                                SqlCommand sqlCmd = new SqlCommand(cmdText, sql);
                                var dataReader = sqlCmd.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    Console.WriteLine(dataReader.GetSqlString(0));
                                }

                                Console.WriteLine(sql.State.ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            #endregion

                            break;
                        }

                    case "RUNTHISSQLSCRIPT":
                        {
                            #region RUNTHISSQLSCRIPT
                            try
                            {
                                string dbServer = args[1];
                                string dbName = args[2];
                                string dbUser = args[3];
                                string dbPassword = args[4];
                                string cmdText = File.ReadAllText(args[5]);
                                logFile = args[6];
                                SqlConnection sql = new SqlConnection(@"Server=" + dbServer + ";Database=" + dbName + ";User ID=" + dbUser + ";Password=" + dbPassword + ";Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False");
                                sql.Open();

                                SqlCommand sqlCmd = new SqlCommand(cmdText, sql);
                                //Wire up an event handler to the connection.InfoMessage event
                                sql.InfoMessage += connection_InfoMessage;
                                var result = sqlCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            #endregion

                            break;
                        }

                    case "RUNSQLSCRIPT":
                        {
                            #region RUNSQLSCRIPT
                            try
                            {
                                string dbConnectionString = args[1];
                                string cmdText = File.ReadAllText(args[2]);
                                logFile = args[3];
                                SqlConnection sql = new SqlConnection(dbConnectionString);
                                sql.Open();

                                SqlCommand sqlCmd = new SqlCommand(cmdText, sql);
                                //Wire up an event handler to the connection.InfoMessage event
                                sql.InfoMessage += connection_InfoMessage;
                                var result = sqlCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            #endregion

                            break;
                        }

                    case "ET_SM_LOCALE_FIX":
                        {
                            #region ET_SM_LOCALE_FIX

                            #region prepare data
                            string connectionString = args[1];
                            string CommId = args[2];
                            string guid = Guid.NewGuid().ToString();
                            string XceedLicenseKey = args[3];
                            string encryptionKey = args[4];
                            string address = args[5];
                            string username = args[6];
                            string password = args[7];
                            string destinationDirectory_Comm = args[8];
                            string destinationDirectory_CommCategory = args[9];
                            #endregion

                            Console.WriteLine("Get data");
                            #region Get data
                            var fileContentCommunication = p.GetDataFromSqlDatabase(connectionString, @"
select 'CommunicationID	CommunicationClassID	CommunicationTypeID	AccountID	CommunicationTitle	ActiveInd	CustomerDiscoverableInd	OwnerEmailAlias	CommunicationDescription	LastUpdatedDateTime	LanguageID	HiddenInd	CustomerCanUnsubscribeInd	HideFromProfileCenter	CountryID	EmailPublisherID'
+char(13)+char(10)+
isnull(convert(varchar(max), CommunicationID),'')+ '	'+
isnull(convert(varchar(max), CommunicationClassID),'')+ '	'+
isnull(convert(varchar(max), CommunicationTypeID),'')+ '	'+
isnull(convert(varchar(max), AccountID),'')+ '	'+
isnull(convert(varchar(max), CommunicationTitle),'')+ '	'+
isnull(convert(varchar(max), ActiveInd),'')+ '	'+
isnull(convert(varchar(max), CustomerDiscoverableInd),'')+ '	'+
isnull(convert(varchar(max), OwnerEmailAlias),'')+ '	'+
isnull(convert(varchar(max), CommunicationDescription),'')+ '	'+
isnull(convert(varchar(max), LastUpdatedDateTime),'')+ '	'+
isnull(convert(varchar(max), LanguageID),'')+ '	'+
isnull(convert(varchar(max), HiddenInd),'')+ '	'+
isnull(convert(varchar(max), CustomerCanUnsubscribeInd),'')+ '	'+
isnull(convert(varchar(max), HideFromProfileCenter),'')+ '	'+
isnull(convert(varchar(max), CountryID),'')+ '	'+
isnull(convert(varchar(max), EmailPublisherID),'')
from [dbo].[communication] c 
where c.CommunicationID in ('" + CommId + "')");

                            var fileContentCommunicationCategory = p.GetDataFromSqlDatabase(connectionString, @"
select 'CommunicationClassID	CommunicationClassTitle	CommunicationClassDescription	ActiveInd'+char(13)+char(10)+isnull(convert(varchar(max), cc.CommunicationClassID),'')+ '	'+
isnull(convert(varchar(max), cc.CommunicationClassTitle),'')+ '	'+
isnull(convert(varchar(max), cc.CommunicationClassDescription),'')+ '	'+
isnull(convert(varchar(max), cc.ActiveInd),'')
from [dbo].[communicationclass] cc 
inner join [dbo].[communication] c on c.CommunicationClassID = cc.CommunicationClassID
where c.CommunicationID in ('" + CommId + "')");
                            #endregion

                            Console.WriteLine("Write data");
                            #region Write to data files
                            var communicationFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + guid + "_Communication.tsv";
                            var communicationCategoryFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + guid + "_CommunicationCategory.tsv";
                            File.WriteAllText(communicationFileName, fileContentCommunication);
                            File.WriteAllText(communicationCategoryFileName, fileContentCommunicationCategory);
                            #endregion

                            Console.WriteLine("Compress data file");
                            #region Compress the data files
                            p.CompressFile(communicationFileName, communicationFileName + ".zip", XceedLicenseKey);
                            p.CompressFile(communicationCategoryFileName, communicationCategoryFileName + ".zip", XceedLicenseKey);
                            #endregion

                            Console.WriteLine("Encrypt data file ");
                            #region Encrypt the data files
                            p.EncryptFile(communicationFileName + ".zip", communicationFileName + ".zip.aes", encryptionKey);
                            p.EncryptFile(communicationCategoryFileName + ".zip", communicationCategoryFileName + ".zip.aes", encryptionKey);
                            #endregion

                            Console.WriteLine("Upload data file ");
                            #region Upload the data files
                            p.UploadFile(communicationFileName + ".zip.aes", address, username, password, destinationDirectory_Comm);
                            p.UploadFile(communicationCategoryFileName + ".zip.aes", address, username, password, destinationDirectory_CommCategory);
                            #endregion

                            Console.WriteLine("Success!!");
                            #endregion

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            File.AppendAllText(logFile, e.Message + Environment.NewLine);
        }

        /// <summary>
        /// Decrypts the encrypted content with secrets certificate
        /// </summary>
        /// <param name="encryptedBlob"></param>
        /// <param name="certificateThumbprint"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedBlob, string certificateThumbprint)
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
            X509Certificate2 Certificate = certificateCollection[0];

            byte[] cipherbytes = Convert.FromBase64String(encryptedBlob);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)Certificate.PrivateKey;
            byte[] plainbytes = rsa.Decrypt(cipherbytes, false);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(plainbytes);
        }

        /// <summary>
        /// Encrypts the plain text with secrets certificate
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="certificateThumbprint"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string certificateThumbprint)
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
            X509Certificate2 Certificate = certificateCollection[0];

            byte[] bytes = Encoding.ASCII.GetBytes(plainText);
            RSACryptoServiceProvider rSACryptoServiceProvider = (RSACryptoServiceProvider)Certificate.PublicKey.Key;
            return Convert.ToBase64String(rSACryptoServiceProvider.Encrypt(bytes, false));
        }

        /// <summary>
        /// Will get connection string from configuration file and open connection
        /// </summary>
        /// <returns> Returns an open connection</returns>
        private SqlConnection GetSqlConnection(string connectionString)
        {
            SqlConnection tempConnection = null;
            tempConnection = new SqlConnection(connectionString);
            tempConnection.Open();
            return tempConnection;
        }

        /// <summary>
        /// Disposes the <c>SqlCommand</c> object and <c>SqlConnection</c> object
        /// </summary>
        /// <param name="tempSqlConnection">The <c>SqlConnection</c> object to be disposed</param>
        /// <param name="tempSqlCommand" >The <c>SqlCommand</c> object to be disposed</param> 
        private void Dispose(SqlConnection tempSqlConnection, SqlCommand tempSqlCommand)
        {
            if (tempSqlCommand != null)
                tempSqlCommand.Dispose();

            if (tempSqlConnection != null && tempSqlConnection.State != ConnectionState.Closed)
            {
                tempSqlConnection.Close();
                tempSqlConnection.Dispose();
            }
        }
        
        /// <summary>
        /// It gets the list of msi tenants preferred file-based approach
        /// </summary>
        /// <returns></returns>
        public string GetDataFromSqlDatabase(string connectionString, string cmdText)
        {
            string result = string.Empty;
            try
            {
                sqlConnection = GetSqlConnection(connectionString);
                sqlCommand = new SqlCommand(cmdText, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                result = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            finally
            {
                Dispose(sqlConnection, sqlCommand);
            }
            return result;
        }

        /// <summary>
        /// This method encrypts a file
        /// </summary>
        /// <param name="encryptionKey">Key used to encrypt the file</param>
        /// <param name="sourceFileName">File name to be encrypted</param>
        /// <param name="targetFileName">Target file name to be created with encryption</param>
        /// <param name="deleteSourceFile">Boolean flag to delete Source file</param>
        public void EncryptFile(string sourceFileName, string targetFileName, string encryptionKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFileName))
                throw new ArgumentNullException(sourceFileName);

            if (string.IsNullOrWhiteSpace(targetFileName))
                throw new ArgumentNullException(targetFileName);

            using (FileStream fileStreamCrypt = new FileStream(targetFileName, FileMode.Create))
            {
                using (RijndaelManaged rijndaelManagedCrypto = new RijndaelManaged())
                {
                    rijndaelManagedCrypto.Mode = CipherMode.CBC;
                    using (PasswordDeriveBytes secretKey = new PasswordDeriveBytes(encryptionKey, Encoding.ASCII.GetBytes(encryptionKey.Length.ToString(CultureInfo.InvariantCulture))))
                    {
                        using (CryptoStream cs = new CryptoStream(fileStreamCrypt, rijndaelManagedCrypto.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)), CryptoStreamMode.Write))
                        {
                            using (FileStream fileStreamIn = new FileStream(sourceFileName, FileMode.Open))
                            {
                                int data;
                                while ((data = fileStreamIn.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }

                                fileStreamIn.Flush();
                                cs.FlushFinalBlock();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method compresses a file
        /// </summary>
        /// <param name="sourceFile">File to be compressed</param>
        /// <param name="destinationFileName">Resultant file to created after compression</param>
        public void CompressFile(string sourceFile, string destinationFileName, string XceedLicenseKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFile)) throw new ArgumentNullException("sourceFile");
            if (string.IsNullOrWhiteSpace(destinationFileName)) throw new ArgumentNullException("destinationFileName");
            try
            {
                // create a temporary zip file locally within the parent folder
                FileInfo sourceInfo = new FileInfo(destinationFileName);
                string tempCompressedFile = Path.Combine(sourceInfo.Directory.FullName, Path.GetRandomFileName());

                // create the compressed file
                if (DoCompress(sourceFile, tempCompressedFile, false, XceedLicenseKey))
                {
                    // move the file to the target location with desired name
                    try
                    {
                        System.IO.File.Move(tempCompressedFile, destinationFileName);
                    }
                    catch
                    {
                        // delete the temporary file created
                        if (System.IO.File.Exists(tempCompressedFile))
                            System.IO.File.Delete(tempCompressedFile);
                        throw;
                    }
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// This private method uses XCeed library to compress the folder or file
        /// </summary>
        /// <param name="sourceFolderOrFile">Folder or file to be compressed</param>
        /// <param name="destinationCompressFile">Name of the resultant compressed file</param>
        /// <param name="blnCompressFolder">It indicate whether to compress a folder or a file</param>
        /// <returns></returns>
        private static bool DoCompress(string sourceFolderOrFile, string destinationCompressFile, bool blnCompressFolder, string XceedLicenseKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFolderOrFile)) throw new ArgumentNullException("sourceFolderOrFile");
            if (string.IsNullOrWhiteSpace(destinationCompressFile)) throw new ArgumentNullException("destinationCompressFile");

            Xceed.Zip.Licenser.LicenseKey = XceedLicenseKey;

            try
            {

                if (blnCompressFolder)
                {
                    QuickZip.Zip(destinationCompressFile,
                    false,
                    false,
                    false,
                    Directory.EnumerateFiles(sourceFolderOrFile).ToArray());
                }
                else
                {
                    QuickZip.Zip(destinationCompressFile, false, false, false, sourceFolderOrFile);
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Upload a file to destinationDirectory of Exact targets Sftp location
        /// </summary>
        /// <param name="pathOfFileToBeTransfered"></param>
        /// <param name="address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="destinationDirectory"></param>
        public void UploadFile(string pathOfFileToBeTransfered, string address, string username, string password, string destinationDirectory)
        {
            Sftp fileTransfer = null;
            if (string.IsNullOrWhiteSpace(pathOfFileToBeTransfered))
            {
                throw new ArgumentNullException("pathOfFileToBeTransfered");
            }

            try
            {
                SshParameters connectionParameters = new SshParameters(address, username, password);
                using (fileTransfer = new Sftp(connectionParameters))
                {
                    fileTransfer.Connect();
                    fileTransfer.SetBinaryMode();
                    fileTransfer.RemoteDir = destinationDirectory;
                    fileTransfer.Upload(pathOfFileToBeTransfered);
                }

                return;
            }
            finally
            {
                if (fileTransfer != null && fileTransfer.IsConnected)
                    fileTransfer.Disconnect();

                fileTransfer = null;
            }
        }
    }
}
