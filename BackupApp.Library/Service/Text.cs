using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Service
{
    public class Text : INotification
    {
        private string _message;
        private string _destinationFile;

        public Text(string message, string destinationFile)
        {
            _message = message;
            _destinationFile = destinationFile;
        }

        public void Send()
        {
            string lineToWrite = _message;

            using (StreamWriter writer = new StreamWriter(_destinationFile, true))
            {
                writer.WriteLine(lineToWrite);
            }
        }
    }
}
