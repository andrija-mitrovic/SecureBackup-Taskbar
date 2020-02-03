using BackupApp.Library;
using BackupApp.Library.Models;
using BackupApp.Library.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupApp.UI
{
    public partial class DisplayInfo : Form
    {
        private string _currentDirectory = Application.StartupPath;
        private readonly Dictionary<BackupIconType, string> _backupIcon;

        public DisplayInfo()
        {
            InitializeComponent();

            _backupIcon = new Dictionary<BackupIconType, string>();
            this._backupIcon.Add(BackupIconType.PROCESSING, $"{_currentDirectory}\\BackupBlue.ico");
            this._backupIcon.Add(BackupIconType.SUCCESSFUL, $"{_currentDirectory}\\BackupGreen.ico");
            this._backupIcon.Add(BackupIconType.ERROR, $"{_currentDirectory}\\BackupRed.ico");

            this.notifyIcon.Icon = new Icon(_backupIcon[BackupIconType.PROCESSING]);
            this.notifyIcon.Text = "Backup working...";

            this.notifyIcon.Visible = true;
            this.Load += DisplayInfo_Load;
        }

        private void DisplayInfo_Load(object sender, EventArgs e)
        {
            string filePath = $"{_currentDirectory}\\{GlobalConfig.BackupAppFileInfo}";
            BackupModel backupModel = filePath.LoadFile().ConvertToBackUpModel();

            Backup backup = new Backup(new SharpCompression());
            bool status = backup.Create(_currentDirectory, backupModel);

            //this.notifyIcon.Icon = (status == true) ?
            //    new Icon(_backupIcon[BackupIconType.SUCCESSFUL]) :
            //    new Icon(_backupIcon[BackupIconType.ERROR]);

            //this.notifyIcon.Text = (status == true) ? 
            //    $"Backup OK - {DateTime.Now}" : 
            //    $"Backup Error - {DateTime.Now}";
            string messageContent = "";

            if (status == true)
            {
                this.notifyIcon.Icon = new Icon(_backupIcon[BackupIconType.SUCCESSFUL]);
                this.notifyIcon.Text = $"Backup OK - {DateTime.Now}";
                messageContent = $"Backup successful - Date: {DateTime.Now}";
            }
            else
            {
                this.notifyIcon.Icon = new Icon(_backupIcon[BackupIconType.ERROR]);
                this.notifyIcon.Text = $"Backup Error - {DateTime.Now}";
                messageContent = $"Backup unsuccessful - Date: {DateTime.Now}";
            }

            string emailConfigFilePath = $"{_currentDirectory}\\{GlobalConfig.EmailConfig}";
            string notificationFilePath = $"{_currentDirectory}\\{GlobalConfig.BackupAppNotifFileInfo}";

            Notification notification = new Notification();
            
            if (File.Exists(emailConfigFilePath))
            {
                EmailModel emailModel = emailConfigFilePath.LoadFile().ConvertToEmailModel();
                emailModel.Body = messageContent;
                notification.Add(new Email(emailModel));
            }

            if (File.Exists(emailConfigFilePath))
            {
                notification.Add(new Text(messageContent, notificationFilePath));
            }

            notification.Send();

            Thread.Sleep(60*1000);
            this.Close();
        }
    }
}
