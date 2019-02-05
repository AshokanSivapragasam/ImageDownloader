using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using com.bgt.lens;
using System.Configuration;
using log4net;
using System.IO;

namespace JobmineHealthMonitor
{
    public partial class JobmineHealthMonitor : ServiceBase
    {
        #region PRIVATE_STATIC_FIELDS
        static bool isPingSuccessful = false;
        static string jobminebase = string.Empty;
        static string testfile = string.Empty;
        static string host = string.Empty;
        static UInt32 port;
        static ulong sessiontimeout = 10000;
        static ulong commandtimeout = 10000;
        static string encoding = string.Empty;
        static string version = string.Empty;
        [ThreadStatic]
        private static LensSession mDESession = null;

        static string basedirectory_shutdown = string.Empty;
        static string commandfullpath_shutdown = string.Empty;
        static string optionalargs_shutdown = string.Empty;
        static bool createnowindow_shutdown = false;
        static string basedirectory_startup = string.Empty;
        static string commandfullpath_startup = string.Empty;
        static string optionalargs_startup = string.Empty;
        static bool createnowindow_startup = false;

        //Timer for the service
        private System.Timers.Timer tmrSplitPosting = new System.Timers.Timer();
        #endregion

        #region CONSTRUTOR
        public JobmineHealthMonitor()
        {
            InitializeComponent();
        }
        #endregion

        #region PROTECTED_METHODS
        protected override void OnStart(string[] args)
        {
            Utils utils = new Utils();

            tmrSplitPosting.Elapsed += TimerSplitPosting_Elapsed;
            try
            {
                #region INITIALISING_APPENDERS
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("logappendername")))
                {
                    utils.logappendername = ConfigurationManager.AppSettings.Get("logappendername");
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("mailappendername")))
                {
                    utils.mailappendername = ConfigurationManager.AppSettings.Get("mailappendername");
                }

                //Configuring the logger for all threads once.
                log4net.Config.XmlConfigurator.Configure();
                utils.logger = LogManager.GetLogger(utils.logappendername);

                //Configuring the logger for all threads once.
                log4net.Config.XmlConfigurator.Configure();
                utils.mailer = LogManager.GetLogger(utils.mailappendername);
                #endregion

                tmrSplitPosting.Interval = 10000;
                tmrSplitPosting.Enabled = true;

                utils.logger.Info("JobMineHealthMonitor service started at - " + DateTime.Now);
                
            }
            catch
            {
            }
        }

        protected override void OnStop()
        {
            //Stop the timer            
            tmrSplitPosting.Enabled = false;
            tmrSplitPosting.Stop();
            Utils utils = new Utils();
            utils.logger.Info("JobMineHealthMonitor service stopped at - " + DateTime.Now);
        }
        #endregion

        #region EVENT_HANDLERS
        /// <summary>
        /// Timer Event Handler elapsed method
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void TimerSplitPosting_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            Utils utils = new Utils();

            try
            {
                var frequency = Convert.ToInt32(ConfigurationManager.AppSettings.Get("frequency").Trim());
                tmrSplitPosting.Enabled = false;
                run();
                tmrSplitPosting.Enabled = true;
                tmrSplitPosting.Interval = frequency;
                utils.logger.Info("it's sleeptime. " + tmrSplitPosting.Interval + " ms");
            }
            catch (Exception ex)
            {
                //Stop the timer            
                tmrSplitPosting.Enabled = false;
                tmrSplitPosting.Stop();
                utils.logger.Info("Error while initializing the service. Error Message: " + ex.Message);
            }
        }
        #endregion

        #region RUN

        /// <summary>
        /// It tends to be triggered periodically to check the health of the jobmine server, starts if it is not responsive.
        /// </summary>
        public void run()
        {
            Utils utils = new Utils();
            ConfigurationManager.RefreshSection("AppSettings");
            var configfile = ConfigurationManager.AppSettings.Get("configfile").Trim();
            commandtimeout = Convert.ToUInt64(ConfigurationManager.AppSettings.Get("commandtimeout").Trim());
            var frequency = Convert.ToInt32(ConfigurationManager.AppSettings.Get("frequency").Trim());
            var configfilerawtext = File.ReadAllText(configfile, Utils.dtctEncoding(configfile));
            var splitservercommands = configfilerawtext.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var command in splitservercommands)
            {
                if (command.Trim().Equals(string.Empty))
                {
                    continue;
                }

                var cmdlets = command.Split(new[] { ',' });

                if (!cmdlets.Length.Equals(15) || command.TrimStart().StartsWith("[") || command.TrimEnd().EndsWith("]"))
                {
                    utils.logger.Debug("command not executed. " + command);
                    continue;
                }

                jobminebase = cmdlets[0].Trim();
                testfile = cmdlets[1].Trim();
                host = cmdlets[2].Trim();
                port = Convert.ToUInt32(cmdlets[3].Trim());
                encoding = cmdlets[4].Trim();
                version = cmdlets[5].Trim();
                sessiontimeout = Convert.ToUInt64(cmdlets[6].Trim());

                basedirectory_shutdown = cmdlets[7].Trim();
                commandfullpath_shutdown = cmdlets[8].Trim();
                optionalargs_shutdown = cmdlets[9].Trim();
                createnowindow_shutdown = Convert.ToBoolean(cmdlets[10]);
                basedirectory_startup = cmdlets[11].Trim();
                commandfullpath_startup = cmdlets[12].Trim();
                optionalargs_startup = cmdlets[13].Trim();
                createnowindow_startup = Convert.ToBoolean(cmdlets[14]);

                var xraylogfile = jobminebase.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "log\\xray.log";
                FileInfo fio = new FileInfo(xraylogfile);

                utils.logger.Info("checking health of the jobmine; server - " + host + " port - " + port + " sessiontimeout - " + sessiontimeout + " ms testfile - " + Path.GetFileName(testfile));

                if (fio.LastWriteTime.AddMilliseconds(frequency) <= DateTime.Now)
                {
                    utils.logger.Info("jobmine is idle for " + Math.Abs(fio.LastWriteTime.Subtract(DateTime.Now).TotalMinutes) + " minutes");
                    if (!buildingaction(testfile, host, port, encoding, version, sessiontimeout, commandtimeout))
                    {
                        utils.logger.Info("stopping unresponsive jobmine. server - " + host + " port - " + port + " sessiontimeout - " + sessiontimeout + " ms ");
                        Process Proc = new Process();
                        Proc.StartInfo.WorkingDirectory = basedirectory_shutdown;
                        Proc.StartInfo.FileName = commandfullpath_shutdown;
                        Proc.StartInfo.Arguments = optionalargs_shutdown;
                        Proc.StartInfo.CreateNoWindow = createnowindow_shutdown;
                        Proc.Start();

                        while (!Proc.HasExited)
                        {
                        }

                        utils.logger.Info("jobmine stopped successfully");

                        utils.logger.Info("restarting jobmine. server - " + host + " port - " + port + " sessiontimeout - " + sessiontimeout + " ms ");
                        Proc.StartInfo.WorkingDirectory = basedirectory_startup;
                        Proc.StartInfo.FileName = commandfullpath_startup;
                        Proc.StartInfo.Arguments = optionalargs_startup;
                        Proc.StartInfo.CreateNoWindow = createnowindow_startup;
                        Proc.Start();

                        while (!Proc.HasExited)
                        {
                        }

                        utils.logger.Info("jobmine restarted successfully");
                    }
                }
            }
        }
        #endregion
        
        #region PUBLIC_METHODS

        /// <summary>
        /// A trigger for sending the 'tag' command to the jobmine server with proposed time-out
        /// It returns to caller as soon as time-out expired. It does not care if the method completes.
        /// </summary>
        /// <param name="inputfile"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="encoding"></param>
        /// <param name="version"></param>
        /// <param name="sessiontimeout"></param>
        /// <param name="commandtimeout"></param>
        /// <returns></returns>
        public static bool buildingaction(string inputfile, string host, UInt32 port, string encoding, string version, ulong sessiontimeout, ulong commandtimeout)
        {
            Utils utils = new Utils();

            utils.logger.Debug("building asnyc caller for 'sendcmdtojobmine' module");
            try
            {
                Action action = new Action(() => sendcmdtojobmine(inputfile, host, port, encoding, version, sessiontimeout));
                CallModuleWithTimeout(action, (int)commandtimeout);
            }
            catch (Exception ex)
            {
                utils.logger.Error("Program aborted. Exception - " + ex.Message);
                isPingSuccessful = false;
            }
            finally
            {
                utils.logger.Info("buildingaction module was " + (isPingSuccessful ? "successful" : "unsuccessful") + ". server - " + host + " port - " + port);
            }
            return isPingSuccessful;
        }

        /// <summary>
        /// Send &lt;tag&gt; command to jobmine and assess response from jobmine server.
        /// </summary>
        /// <param name="inputfile"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="encoding"></param>
        /// <param name="version"></param>
        /// <param name="sessiontimeout"></param>
        public static void sendcmdtojobmine(string inputfile, string host, UInt32 port, string encoding, string version, ulong sessiontimeout)
        {
            Utils utils = new Utils();
            utils.logger.Info("sending <tag> command");
            try
            {
                if (OpenJobMineSrvrSession(host, port, encoding, version, sessiontimeout))
                {
                    utils.logger.Info("session created");
                    //We read file with its native encoding charset
                    //Thus, it may fix the broken characters while reading the file..
                    var inputDoc = File.ReadAllText(inputfile, Utils.dtctEncoding(inputfile));
                    var outputDoc = string.Empty;
                    TagDocumentWithJobMine(inputDoc, encoding, out outputDoc);

                    if (isPingSuccessful && (outputDoc == null || outputDoc.Equals(string.Empty)))
                    {
                        utils.logger.Warn("<tag> command looks successful but output text is empty");
                        isPingSuccessful = false;
                    }
                }
            }
            catch (Exception ex)
            {
                utils.logger.Error("err_tag_command. err_msg - " + ex.Message);
                isPingSuccessful = false;
            }
            finally
            {
                utils.logger.Info("<tag> command was " + (isPingSuccessful ? "successful" : "unsuccessful") + ". server - " + host + " port - " + port);
            }
        }

        /// <summary>
        /// Tag the document with jobmine server
        /// (It is the exact replica of the BgtWorkerNodeAgent)
        /// </summary>
        /// <param name="inputDoc"></param>
        /// <param name="encoding"></param>
        /// <param name="outputDoc"></param>
        public static void TagDocumentWithJobMine(string inputDoc, string encoding, out string outputDoc)
        {
            outputDoc = string.Empty;

            Utils utils = new Utils();
            try
            {
                #region INITIALISING
                var mInputText = "<?xml version='1.0' encoding='" + encoding + "'?>";
                mInputText += "<bgtcmd><tag type='posting'><![CDATA[" + inputDoc.Replace("<row>", "<Row>").Replace("</row>", "</Row>") + "]]></tag></bgtcmd>";
                LensMessage inMessage;
                LensMessage outMessage;
                #endregion

                try
                {
                    #region TRY_TAGGING_DOCUMENT
                    inMessage = LensMessage.Create(mInputText, LensMessage.XML_TYPE);
                    outMessage = mDESession.SendMessage(inMessage);
                    outputDoc = outMessage.GetMessageData();
                    #endregion

                    isPingSuccessful = true;
                }
                catch (LensException lex)
                {
                    #region EXCEPTION_IS_VOID_OR_NULL
                    if (lex == null || lex.Message == null)
                    {
                        throw new Exception("LensException is VOID or NULL");
                    }
                    #endregion

                    isPingSuccessful = false;
                    utils.logger.Error("lensexception. err_msg - " + lex.Message);
                }
                catch (ServerBusyException sbe)
                {
                    #region EXCEPTION_IS_VOID_OR_NULL
                    if (sbe == null || sbe.Message == null)
                    {
                        throw new Exception("ServerBusyException is VOID or NULL");
                    }
                    #endregion

                    isPingSuccessful = false;
                    utils.logger.Error("serverbusyexception. err_msg - " + sbe.Message);
                }
                finally
                {
                    #region RELEASING_RESOURCES
                    inMessage = null;
                    outMessage = null;
                    mInputText = null;
                    #endregion
                }

                outputDoc = outputDoc.Replace("<bgtres>", "").Replace("</bgtres>", "");

            }
            catch (Exception ex)
            {
                utils.logger.Error("err_tag_command. err_msg - " + ex.Message);
                isPingSuccessful = false;
            }
            finally
            {
                #region RELEASING_RESOURCES
                inputDoc = null;
                #endregion
            }
        }

        /// <summary>
        /// It starts/ creates new session for Lens(JobMine).
        /// If lens(DEXRay) seesion is created, returns true  
        /// Else, returns false  
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="encodingValue"></param>
        /// <param name="version"></param>
        /// <param name="sessiontimeout"></param>
        /// <returns></returns>
        private static bool OpenJobMineSrvrSession(string host, UInt32 port, string encodingValue, string version, ulong sessiontimeout)
        {
            Utils utils = new Utils();
            utils.logger.Debug("Opening a session for JobMine V" + version + "...");
            mDESession = MSLens.CreateSession(host, port, Encoding.GetEncoding(encodingValue));
            mDESession.SetEnableTransactionTimeout(true);
            mDESession.SetTransactionTimeout(sessiontimeout); // 1 minute
            mDESession.Open();
            utils.logger.Debug(("created") + " a session for JobMine V" + version + " currently.");
            return true;
        }

        #endregion

        #region PRIVATE_METHODS
        /// <summary>
        /// It is the asynchronous method which escapes from the method as soon
        /// as timeout expires
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        private static void CallModuleWithTimeout(Action action, int timeout)
        {
            Utils utils = new Utils();
            utils.logger.Debug("caller initiated with timeout - " + timeout + " milliseconds");
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            AsyncCallback callback = ar => waitHandle.Set();
            action.BeginInvoke(callback, null);

            if (!waitHandle.WaitOne(timeout))
                throw new System.TimeoutException("Timeout expired. Failed to complete in the timeout specified.");

            utils.logger.Debug("caller exiting successfully");
        }
        #endregion
    }
}
