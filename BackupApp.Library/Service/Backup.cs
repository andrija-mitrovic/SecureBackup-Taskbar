using BackupApp.Library.Database;
using BackupApp.Library.Models;
using BackupApp.Library.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupApp.Library.Service
{
    public class Backup
    {
        private ICompression _compression;

        public Backup(ICompression compression)
        {
            _compression = compression;
        }

        public bool Create(string currentDirectory, BackupModel model)
        {
            try
            {
                GlobalConfig.Database = model.Database;

                string fullFileInfoPath = $"{currentDirectory}\\{GlobalConfig.BackupAppFileInfo}";
                if (File.Exists(fullFileInfoPath))
                {
                    int nextBackup = model.CurrentFile + 1;
                    TextProcessor.UpdateCurrentFile(nextBackup, fullFileInfoPath);

                    string backupFolder = model.SourceDir + "\\";
                    GlobalConfig.Connection.CreateBackup(model.Database, backupFolder);

                    _compression.Create(model, GlobalConfig.BackupPassword);

                    string filePath = $@"{model.DestinationDir}\{model.Name}_{model.CurrentFile}.{model.CompressionType}";

                    if (!_compression.CheckCompressedFile(filePath))
                    {
                        throw new Exception("Compressed file corrupted!");
                    }

                    return true;
                }
                else
                {
                    throw new Exception($"{GlobalConfig.BackupAppFileInfo} file doesn't exist!");
                }        
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
