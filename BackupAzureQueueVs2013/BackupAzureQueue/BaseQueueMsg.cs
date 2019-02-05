using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupAzureQueue
{
    class BaseQueueMsg
    {
        public string prop01 { get; set; }
        public string prop02 { get; set; }
        public string prop03 { get; set; }
    }

    class Inherit001 : BaseQueueMsg
    {
        public string prop04 { get; set; }
        public string prop05 { get; set; }
    }

    class Inherit002 : BaseQueueMsg
    {
        public string prop06 { get; set; }
        public string prop07 { get; set; }
    }
}
