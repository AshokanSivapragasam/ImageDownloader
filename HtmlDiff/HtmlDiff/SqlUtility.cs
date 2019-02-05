using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace HtmlDiff
{
    public class SqlUtility
    {
        static string logFile = string.Empty;

        static void Main(string[] args)
        {
            SqlUtility p = new SqlUtility();

            var choice = args[0];
            Console.WriteLine("'" + choice + "'");

            try
            {
                switch (choice)
                {
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
                                SqlConnection sql = new SqlConnection(@"Server=" + dbServer + ";Database=" + dbName + ";User ID=" + dbUser + ";Password=" + dbPassword + ";Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Timeout=30");
                                sql.Open();
                                SqlCommand sqlCmd = new SqlCommand(cmdText, sql);
                                sqlCmd.CommandTimeout = 0;
                                //Wire up an event handler to the connection.InfoMessage event
                                sql.InfoMessage += connection_InfoMessage;
                                sqlCmd.ExecuteScalar();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            #endregion

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, ex.Message);
            }
        }

        static void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            File.AppendAllText(logFile, e.Message + Environment.NewLine);
        }
    }
}
