using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace CustomizeCloudPackage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program prgm = new Program();
            string inputDir = args[0];
            //1. Get all cloud package file (*.cspkg)
            var csPkgFiles = Directory.GetFiles(inputDir, "*.cspkg", SearchOption.TopDirectoryOnly);
            foreach (var csPkgFile in csPkgFiles)
            {
                //2. Unzip the cloud package (*.cspkg)
                var destCsPkgDir = Path.Combine(Path.GetDirectoryName(csPkgFile),Path.GetFileNameWithoutExtension(csPkgFile));
                prgm.Unzip(csPkgFile, destCsPkgDir);

                //3. Find the file (*.cssx)
                var cssxFiles = Directory.GetFiles(destCsPkgDir, "*.cssx", SearchOption.TopDirectoryOnly);
                foreach (var cssxFile in cssxFiles)
                {
                    //4. Unzip file (*.cssx)
                    var destCssxDir = Path.Combine(Path.GetDirectoryName(cssxFile), Path.GetFileNameWithoutExtension(cssxFile));
                    prgm.Unzip(cssxFile, destCssxDir);

                    //5. Find the file (*.csman)
                    var csmanFiles = Directory.GetFiles(destCssxDir, "*.csman", SearchOption.TopDirectoryOnly);

                    //6. Read the driver file
                    var driverFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DriverFile.xml");
                    var xDoc = new XmlDocument();
                    xDoc.Load(driverFile);
                    var xNodes = xDoc.SelectNodes("/cloudpkgdriver/files/file");
                    var xDocCsman = new XmlDocument();
                    xDocCsman.Load(csmanFiles[1]);
                    foreach(XmlNode xNode in xNodes)
                    {
                        //7. Copy (or) replace the files in cssx role package directory
                        var srcPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + xNode.Attributes["srcpath"].Value;
                        var destPath = destCssxDir.TrimEnd(Path.DirectorySeparatorChar) + xNode.Attributes["destpath"].Value;
                        var isOverwrite = Convert.ToBoolean(xNode.Attributes["overwrite"].Value);
                        File.Copy(srcPath, destPath, isOverwrite);

                        var dupItemNode = xDocCsman.SelectSingleNode("/PackageManifest/Contents/Item[@name='" + xNode.Attributes["destpath"].Value + "']");
                        dupItemNode = dupItemNode != null ? dupItemNode : xDocCsman.SelectSingleNode("/PackageManifest/Contents/Item[@name='" + xNode.Attributes["destpath"].Value.Replace("/", "\\") + "']");
                        dupItemNode = dupItemNode != null ? dupItemNode : xDocCsman.SelectSingleNode("/PackageManifest/Contents/Item[@name='" + xNode.Attributes["destpath"].Value.Replace("\\", "/") + "']");
                        if (dupItemNode != null)
                        {
                            dupItemNode.Attributes["hash"].Value = prgm.GetChecksum(destPath);
                        }
                        else
                        {
                            var itemNode = xDocCsman.SelectSingleNode("/PackageManifest/Contents");
                            itemNode.InnerXml += "<Item name='" + xNode.Attributes["srcpath"].Value + "' hash='" + prgm.GetChecksum(destPath) + "' uri='" + xNode.Attributes["destpath"].Value + "' created='130360517395000380' modified='130326005104656392' />";
                        }
                    }

                    xDocCsman.Save(csmanFiles[1]);
                }

                //2. Unzip the cloud package (*.cspkg)
                var destCsPkgDir = Path.Combine(Path.GetDirectoryName(csPkgFile), Path.GetFileNameWithoutExtension(csPkgFile));
                prgm.Unzip(csPkgFile, destCsPkgDir);
            }
        }

        /// <summary>
        /// Uncompress the files to target directory
        /// </summary>
        /// <param name="srcFile"></param>
        /// <param name="destDir"></param>
        public void Unzip(string srcFile, string destDir)
        {
            try
            {
                var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                using (ZipFile zip = ZipFile.Read(srcFile, options))
                {
                    // This call to ExtractAll() assumes:
                    //   - none of the entries are password-protected.
                    //   - want to extract all entries to current working directory
                    //   - none of the files in the zip already exist in the directory;
                    //     if they do, the method will throw.
                    zip.ExtractAll(destDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unzip. Exception: " + ex.Message.Replace("\\r", " ").Replace("\\n", " "));
            }
        }

        /// <summary>
        /// GetChecksum by file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
    }
}
