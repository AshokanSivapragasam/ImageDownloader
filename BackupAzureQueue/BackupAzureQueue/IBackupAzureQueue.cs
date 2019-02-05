using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupAzureQueue
{
    public interface IBackupAzureQueue
    {
        int Add(int number1, int number2);
    }
}
