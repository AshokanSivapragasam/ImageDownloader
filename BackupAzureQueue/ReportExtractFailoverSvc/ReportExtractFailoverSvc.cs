using ReportExtractFailover;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractFailoverSvc
{
    public partial class ReportExtractFailoverSvc : ServiceBase
    {
        public ReportExtractFailoverSvc()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var failoverProcessor = new FailoverProcessor();
            failoverProcessor.DoWork();
        }

        protected override void OnStop()
        {
        }
    }
}
