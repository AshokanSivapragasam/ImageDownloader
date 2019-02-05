using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using log4net;
using System.Threading;

namespace JobmineHealthMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			{ 
				new JobmineHealthMonitor() 
			};
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Utils utils = new Utils();

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

                utils.logger.Info("JobMineHealthMonitor service started at - " + DateTime.Now);

                JobmineHealthMonitor jobminehealthmonitor = new JobmineHealthMonitor();
                jobminehealthmonitor.run();

                utils.logger.Info("JobMineHealthMonitor service stopped at - " + DateTime.Now);
            }
        }
    }
}
