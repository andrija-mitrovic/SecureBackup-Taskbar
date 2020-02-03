using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace BackupApp.Library.Database
{
    public class SqlManagement:IConnection
    {
        public void CreateBackup(string database, string backupFolder)
        {
            if (Directory.Exists(backupFolder))
            {
                try
                {
                    SqlConnection connection = GlobalConfig.GetSqlConnection();        
                    connection.Open();

                    string backupPath = backupFolder + database + "-" + DateTime.Now.ToString("dd.MM.yyyy").Replace(".", "") + ".BAK";
                    string query = "BACKUP DATABASE [" + database + "] TO  DISK = N'" + backupPath
                        + "' WITH NOFORMAT, NOINIT,  NAME = N'data-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Backup folder doesn't exist!");
            }
        }
    }
}
