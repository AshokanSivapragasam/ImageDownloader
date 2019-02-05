using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace OnPremiseHealthMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ImpersonateUserAndStartService("fareast", "etintdev", "microsoft@FAREAST8910", "Service1", "HYDPCM266761D");
        }

        /// <summary>
        /// Impersonates session with specified user and starts the service.
        /// </summary>
        /// <param name="remoteMachineName"></param>
        /// <param name="domainName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="serviceName"></param>
        private static void ImpersonateUserAndStartService(string remoteMachineName, string domainName, string username, string password, string serviceName)
        {
            #region CREATES_IMPERSONATION
            var impersonateUser = new ImpersonateUser();
            impersonateUser.Impersonate(domainName, username, password);
            #endregion

            #region CODE_EXECUTES_AS_AN_IMPERSONATED_USER
            var serviceController = new ServiceController(serviceName, remoteMachineName);
            if (serviceController.CanStop)
                serviceController.Stop();
            else
                serviceController.Start();
            Console.WriteLine(serviceController.CanStop);
            #endregion

            #region DROPS_IMPERSONATION
            impersonateUser.Undo();
            #endregion
        }
    }
}
