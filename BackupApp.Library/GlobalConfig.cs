using BackupApp.Library.Database;
using BackupApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library
{
    public static class GlobalConfig
    {
        public const string BackupPassword= "123456";
        public const string BackupAppFileInfo= "BackupApp.txt";
        public const string BackupAppNotifFileInfo = "Backup notifications.txt";
        public const string EmailConfig = "Email configuration.txt";
        public static string Database { get; set; }

        public static IConnection Connection { get; set; }

        public static void InitializeDatabase(DatabaseType databaseType)
        {
            if (databaseType == DatabaseType.SqlServer)
            {
                Connection = new SqlManagement();
            }
            else if (databaseType == DatabaseType.MySql)
            {
                Connection = new MySqlManagment();
            }
        }

        private static string connectionMaster = ConfigurationManager.ConnectionStrings["SqlServer"]
                .ConnectionString.Replace("test", "master");
        private static string connection = ConfigurationManager.ConnectionStrings["SqlServer"]
                .ConnectionString.Replace("test", GlobalConfig.Database);

        public static SqlConnection GetSqlMasterConnection()
        {
            SqlConnection con = new SqlConnection(connectionMaster);
            return con;
        }

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection con = new SqlConnection(connection);
            return con;
        }
    }
}
