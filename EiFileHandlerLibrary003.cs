using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinSCP;
using Xceed.Zip;

namespace EiFileHandlerLibrary
{
    public class FileHandlerLibraryModules
    {
        public static void Main(string[] args)
        {
            var fileHandlerLibraryModules = new FileHandlerLibraryModules();
            var userPick = "MENU";

            do
            {
                var userArgs = userPick.Split(new[] { "\" \"", "\"" }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToArray();
                switch (userArgs[0])
                {
                    case "ENCRYPTFILEUSINGSHAREDKEY":
                        {
                            #region ENCRYPTFILEUSINGSHAREDKEY
                            Console.WriteLine("Encryption started");
                            fileHandlerLibraryModules.EncryptFileUsingSharedKey(sourceFile: userArgs[1], targetFile: userArgs[2], encryptionKey: userArgs[3]);
                            Console.WriteLine("Encryption completed");
                            userPick = "USERCHOICE";
                            #endregion

                            break;
                        }
                    case "DECRYPTFILEUSINGSHAREDKEY":
                        {
                            #region DECRYPTFILEUSINGSHAREDKEY
                            Console.WriteLine("Decryption started");
                            fileHandlerLibraryModules.DecryptFileUsingSharedKey(sourceFile: userArgs[1], destinationFile: userArgs[2], decryptionKey: userArgs[3]);
                            Console.WriteLine("Decryption completed");
                            userPick = "USERCHOICE";
                            #endregion

                            break;
                        }
                    case "DOWNLOADFROMSFTP":
                        {
                            #region DOWNLOADFROMSFTP
                            Console.WriteLine("Download started");
                            fileHandlerLibraryModules.DownloadFromSftp(sftpAddress: userArgs[1], sftpUserName: userArgs[2], sftpPassword: userArgs[3], remoteSourceFileRelativePath: userArgs[4], localDestinationFolder: userArgs[5], isSftpTraceEnabled: Convert.ToBoolean(userArgs[6]), sftpLogFolderPath: userArgs[7]);
                            Console.WriteLine("Download completed");
                            userPick = "USERCHOICE";
                            #endregion

                            break;
                        }
                    case "UPLOADFROMSFTP":
                        {
                            #region UPLOADFROMSFTP
                            Console.WriteLine("Upload started");
                            fileHandlerLibraryModules.UploadFromFileShareToSftp(sftpAddress: userArgs[1], sftpUserName: userArgs[2], sftpPassword: userArgs[3], localSourceFileFullPath: userArgs[4], remoteDestinationFolderRelativePath: userArgs[5], isSftpTraceEnabled: Convert.ToBoolean(userArgs[6]), sftpLogFolderPath: userArgs[7]);
                            Console.WriteLine("Upload completed");
                            userPick = "USERCHOICE";
                            #endregion

                            break;
                        }

                    case "COMPRESSFILEORFOLDER":
                        {
                            #region COMPRESSFILEORFOLDER
                            Console.WriteLine("Compression started");
                            fileHandlerLibraryModules.CompressFileOrFolder(sourceFolderOrFile: userArgs[1], destinationCompressFile: userArgs[2], isItAFolder: Convert.ToBoolean(userArgs[3]), xceedLicenseKey: userArgs[4]);
                            Console.WriteLine("Compression completed");
                            userPick = "USERCHOICE";
                            #endregion

                            break;
                        }

                    case "DECOMPRESSFILEORFOLDER":
                        {
                            #region DECOMPRESSFILEORFOLDER
                            Console.WriteLine("Decompression started");
                            fileHandlerLibraryModules.DecompressFileOrFolder(compressedFile: userArgs[1], destinationPath: userArgs[2], extractToFolder: userArgs[3], xceedLicenseKey: userArgs[4], isItAFolder: Convert.ToBoolean(userArgs[5]), canDeleteCompressedfile: Convert.ToBoolean(userArgs[6]));
                            Console.WriteLine("Decompression completed");
                            userPick = "USERCHOICE";
                            #endregion

                            break;
                        }
                    case "USERCHOICE":
                        {
                            Console.WriteLine(Environment.NewLine + "Your choice");
                            userPick = Console.ReadLine();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(Environment.NewLine);
                            FunConsole.WriteLine(@"USAGE

1. ENCRYPTFILEUSINGSHAREDKEY <sourceFile> <targetFile> <encryptionKey>
2. DECRYPTFILEUSINGSHAREDKEY <sourceFile> <targetFile> <encryptionKey>
3. DOWNLOADFROMSFTP <sftpAddress> <sftpUserName> <sftpPassword> <remoteSourceFileRelativePath> <localDestinationFolder> <isSftpTraceEnabled> <sftpLogFolderPath>
4. UPLOADFROMSFTP <sftpAddress> <sftpUserName> <sftpPassword> <remoteSourceFileRelativePath> <localDestinationFolder> <isSftpTraceEnabled> <sftpLogFolderPath>
5. COMPRESSFILEORFOLDER <sourceFolderOrFile> <destinationCompressFile> <isItAFolder> <xceedLicenseKey>
6. DECOMPRESSFILEORFOLDER <compressedFile> <destinationPath> <extractToFolder> <xceedLicenseKey> <isItAFolder> <canDeleteCompressedfile>");

                            userPick = "USERCHOICE";

                            break;
                        }
                }
            } while (!userPick.ToLower().Equals("exit") && !userPick.ToLower().Equals("bye"));
        }

        /// <summary>
        /// This method encrypts a file using a shared key. Key is public for both Salesforce & CPM.
        /// </summary>
        /// <param name="encryptionKey">Key used to encrypt the file</param>
        /// <param name="sourceFileName">File name to be encrypted</param>
        /// <param name="targetFileName">Target file name to be created with encryption</param>
        /// <param name="deleteSourceFile">Boolean flag to delete Source file</param>
        public void EncryptFileUsingSharedKey(string sourceFile, string targetFile, string encryptionKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException(sourceFile);

            if (string.IsNullOrWhiteSpace(targetFile))
                throw new ArgumentNullException(targetFile);

            using (FileStream fileStreamCrypt = new FileStream(targetFile, FileMode.Create))
            {
                using (RijndaelManaged rijndaelManagedCrypto = new RijndaelManaged())
                {
                    rijndaelManagedCrypto.Mode = CipherMode.CBC;
                    using (PasswordDeriveBytes secretKey = new PasswordDeriveBytes(encryptionKey, Encoding.ASCII.GetBytes(encryptionKey.Length.ToString(CultureInfo.InvariantCulture))))
                    {
                        using (CryptoStream cs = new CryptoStream(fileStreamCrypt, rijndaelManagedCrypto.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)), CryptoStreamMode.Write))
                        {
                            using (FileStream fileStreamIn = new FileStream(sourceFile, FileMode.Open))
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
        /// This method decrypts a file using a shared key. Key is public for both Salesforce & CPM.
        /// </summary>
        /// <param name="request">Actual Request</param>        
        /// <param name="destinationFile">file path to where we are placing the decrypted file</param>
        public void DecryptFileUsingSharedKey(string sourceFile, string destinationFile, string decryptionKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException("Input file is not valid");

            if (string.IsNullOrWhiteSpace(decryptionKey))
                throw new ArgumentNullException("DecryptionKey is not valid");

            try
            {
                using (FileStream fsCrypt = new FileStream(destinationFile, FileMode.Create))
                {
                    using (RijndaelManaged rmCrypto = new RijndaelManaged())
                    {
                        rmCrypto.Mode = CipherMode.CBC;
                        using (PasswordDeriveBytes secretKey = new PasswordDeriveBytes(decryptionKey, Encoding.ASCII.GetBytes(decryptionKey.Length.ToString())))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, rmCrypto.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)), CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(sourceFile, FileMode.OpenOrCreate))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                        cs.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts the encrypted content with *.pfx certificate
        /// </summary>
        /// <param name="encryptedBlob"></param>
        /// <param name="certificateThumbprint"></param>
        /// <returns></returns>
        public string DecryptStringUsingPfxCertficate(string encryptedBlob, string certificateThumbprint)
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
        /// Encrypts the plain text with *.pfx certificate
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="certificateThumbprint"></param>
        /// <returns></returns>
        public string EncryptUsingPfxCertficate(string plainText, string certificateThumbprint)
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
        /// Downloads the specified file from a Secured FTP location to the specified file path
        /// </summary>
        /// <param name="message">message that contain the information about the file to be downloaded.</param>
        /// <param name="request">Actual Request</param>
        /// <param name="destinationFilePath">Specifies the file path which the file has to be downloaded to</param>
        /// <param name="accountDetails">account Details</param>
        /// <returns>Returns the file name with path at the destination</returns>
        public void DownloadFromSftp(string sftpAddress, string sftpUserName, string sftpPassword, string remoteSourceFileRelativePath, string localDestinationFolder, bool isSftpTraceEnabled, string sftpLogFolderPath)
        {
            if (string.IsNullOrWhiteSpace(remoteSourceFileRelativePath))
                throw new ArgumentNullException(remoteSourceFileRelativePath);

            if (string.IsNullOrWhiteSpace(localDestinationFolder))
                throw new ArgumentNullException(localDestinationFolder);

            try
            {
                var sessionOptions = new SessionOptions
                {
                    FtpMode = FtpMode.Passive,
                    HostName = sftpAddress,
                    UserName = sftpUserName,
                    Password = sftpPassword,
                    Protocol = WinSCP.Protocol.Sftp
                };

                using (var session = new Session())
                {
                    sessionOptions.SshHostKeyFingerprint = session.ScanFingerprint(sessionOptions, "SHA-256");

                    if (isSftpTraceEnabled)
                    {
                        if (!Directory.Exists(sftpLogFolderPath))
                            Directory.CreateDirectory(sftpLogFolderPath);

                        var sftpLogFilename = "SftpTrace_Thread_" + Thread.CurrentThread.ManagedThreadId + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + ".txt";
                        session.SessionLogPath = Path.Combine(sftpLogFolderPath, sftpLogFilename);
                    }
                    // Connect
                    session.Open(sessionOptions);

                    var transferOptions = new TransferOptions
                    {
                        TransferMode = TransferMode.Binary
                    };

                    // download files
                    var transferResult = session.GetFiles(remoteSourceFileRelativePath + "*", localDestinationFolder, false, transferOptions);

                    Console.WriteLine(@"Is transfer success? " + transferResult.IsSuccess + @"
Any failures? " + transferResult.Failures.Select(r => r));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Uploads a file from the specified path to a secured FTP location
        /// </summary>
        /// <param name="sourceFilePath">Specifies the file name with full path which has to be uploaded to the secured FTP location</param>
        /// <param name="accountDetails">Object that contains SFTP connection details.</param>
        /// <param name="deleteSourceFile">Boolean flag to delete Source file</param>
        public void UploadFromFileShareToSftp(string sftpAddress, string sftpUserName, string sftpPassword, string localSourceFileFullPath, string remoteDestinationFolderRelativePath, bool isSftpTraceEnabled, string sftpLogFolderPath)
        {
            if (string.IsNullOrWhiteSpace(localSourceFileFullPath))
                throw new ArgumentNullException(localSourceFileFullPath);

            if (string.IsNullOrWhiteSpace(remoteDestinationFolderRelativePath))
                throw new ArgumentNullException(remoteDestinationFolderRelativePath);

            try
            {
                var sessionOptions = new SessionOptions
                {
                    FtpMode = FtpMode.Passive,
                    HostName = sftpAddress,
                    UserName = sftpUserName,
                    Password = sftpPassword,
                    Protocol = WinSCP.Protocol.Sftp
                };

                using (var session = new Session())
                {
                    sessionOptions.SshHostKeyFingerprint = session.ScanFingerprint(sessionOptions, "SHA-256");

                    if (isSftpTraceEnabled)
                    {
                        if (!Directory.Exists(sftpLogFolderPath))
                            Directory.CreateDirectory(sftpLogFolderPath);

                        var sftpLogFilename = "SftpTrace_Thread_" + Thread.CurrentThread.ManagedThreadId + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + ".txt";
                        session.SessionLogPath = Path.Combine(sftpLogFolderPath, sftpLogFilename);
                    }
                    // Connect
                    session.Open(sessionOptions);
                    // Upload files
                    var transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                    transferOptions.ResumeSupport = new TransferResumeSupport { State = TransferResumeSupportState.Off };

                    var transferResult = session.PutFiles(localSourceFileFullPath, remoteDestinationFolderRelativePath, false, transferOptions);
                    // Throw on any error
                    transferResult.Check();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This method decompresses a file
        /// </summary>
        /// <param name="sourceFolderOrFile">File or folder to be decompressed</param>
        /// <param name="destinationCompressFile">Resultant file or folder to created after decompression</param>
        /// <param name="isItAFolder">Used as <c>isItAFolder?</c> flag</param>
        /// <param name="xceedLicenseKey">License Key for Xceed Library</param>
        /// <returns>Returns if file or decompression was successful or not</returns>
        public bool CompressFileOrFolder(string sourceFolderOrFile, string destinationCompressFile, bool isItAFolder, string xceedLicenseKey)
        {
            if (string.IsNullOrWhiteSpace(sourceFolderOrFile)) throw new ArgumentNullException("sourceFolderOrFile");
            if (string.IsNullOrWhiteSpace(destinationCompressFile)) throw new ArgumentNullException("destinationCompressFile");
            Licenser.LicenseKey = xceedLicenseKey;

            try
            {
                if (isItAFolder)
                    QuickZip.Zip(destinationCompressFile, false, false, false, Directory.EnumerateFiles(sourceFolderOrFile).ToArray());
                else
                    QuickZip.Zip(destinationCompressFile, false, false, false, sourceFolderOrFile);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 
        /// This private method uses XCeed library to decompress the folder or file 
        /// </summary> 
        /// <param name="compressedFile">Name of the file to be decompressed</param> 
        /// <param name="destinationPath">Path where ExtractToFolder will be created</param> 
        /// <param name="extractToFolder">ExtractToFolder contains all the decompressed files</param>
        /// <param name="emailInterchangeId">Email Interchange ID</param>
        /// <param name="requestType">Request Type</param>
        /// <param name="isItAFolder">It indicate whether to decompress a folder or a file, since we made this as Optional parameter by default it will takes true</param> 
        /// <param name="canDeleteCompressedfile">It indicate whether to delete the compressed folder after the decompression or not, if true it will delete the compressed fodler</param> 
        /// <returns></returns>
        public bool DecompressFileOrFolder(string compressedFile, string destinationPath, string extractToFolder, string xceedLicenseKey, bool isItAFolder = true, bool canDeleteCompressedfile = false)
        {
            if (string.IsNullOrWhiteSpace(compressedFile)) throw new ArgumentNullException("compressedFile");
            Xceed.Zip.Licenser.LicenseKey = xceedLicenseKey;

            try
            {
                if (isItAFolder)
                {
                    if (string.IsNullOrWhiteSpace(destinationPath)) throw new ArgumentNullException("destinationPath");
                    if (string.IsNullOrWhiteSpace(extractToFolder)) throw new ArgumentNullException("extractToFolder");

                    destinationPath = Path.Combine(destinationPath, extractToFolder);
                    if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
                    QuickZip.Unzip(compressedFile, destinationPath, "*");
                }
                else
                {
                    var compressedFileInfo = new FileInfo(compressedFile);
                    QuickZip.Unzip(compressedFile, compressedFileInfo.Directory.FullName, "*");
                }

                if (canDeleteCompressedfile && File.Exists(compressedFile)) System.IO.File.Delete(compressedFile);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class FunConsole
    {
        public static void WriteLine(string message)
        {
            var type = typeof(ConsoleColor);
            var colorIndex = 0;
            foreach (var line in message.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                colorIndex = 2;
                foreach (var word in line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    colorIndex += 1;
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, Enum.GetNames(type)[colorIndex]);
                    Console.Write(word);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
