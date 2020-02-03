
using System.Collections.Generic;

namespace BackupApp.Library.Models
{
    public class BackupModel
    {
        private List<string> _backupFiles;
        public string SourceDir { get; set; }
        public string DestinationDir { get; set; }
        public string Database { get; set; }
        public string Name { get; set; }
        public string CompressionType { get; set; }
        public List<string> BackupFiles {
            get
            {
                return _backupFiles;
            }
            set
            {
                _backupFiles = value;

                for(int i = 0; i < _backupFiles.Count; i++)
                {
                    _backupFiles[i] = _backupFiles[i].Trim(' ');
                }
            }
        }
        public int CurrentFile { get; set; }
    }
}
