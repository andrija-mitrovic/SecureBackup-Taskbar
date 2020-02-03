using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Database
{
    public interface IConnection
    {
        void CreateBackup(string database, string backupFolder);
    }
}
