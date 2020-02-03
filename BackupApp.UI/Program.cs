using BackupApp.Library;
using BackupApp.Library.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupApp.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobalConfig.InitializeDatabase(DatabaseType.SqlServer);
            Application.Run(new DisplayInfo());
        }
    }
}
