using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Models
{
    public class EmailModel
    {
        public string Host { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
