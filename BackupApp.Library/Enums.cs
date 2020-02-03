using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library
{
    public enum CompressionFormat
    {
        RAR, 
        SEVEN_ZAP,
        ZIP,
        TAR,
        GZIP,
        BZIP_TWO
    }

    public enum BackupIconType
    {
        PROCESSING,
        SUCCESSFUL,
        ERROR
    }

    public enum DatabaseType
    {
        SqlServer,
        MySql
    }
}
