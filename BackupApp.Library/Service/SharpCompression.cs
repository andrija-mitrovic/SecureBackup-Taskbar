using BackupApp.Library.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Service
{
    public class SharpCompression:ICompression
    {
        private string _password;
        private string _format;
        private string _destinationFile;

        public void Create(BackupModel model)
        {
            InitializeCompression(model);
        }

        public void Create(BackupModel model, string password)
        {
            _password = password;
            InitializeCompression(model);
        }

        public bool CheckCompressedFile(string filePath)
        {
            return ZipFile.CheckZip(filePath);
        }

        private void InitializeCompression(BackupModel model)
        {
            _format = GetCompressionExtension(model.CompressionType);

            if (!String.IsNullOrEmpty(_format))
            {
                _destinationFile = $@"{model.DestinationDir}\{model.Name}_{model.CurrentFile}{_format}";
                Compress(_destinationFile, model);
            }
            else
            {
                throw new Exception("Please insert correct compression type format!");
            }
        }

        private void Compress(string destinationFile, BackupModel model)
        {
            using (ZipFile zip = new ZipFile(destinationFile))
            {
                if(!String.IsNullOrEmpty(_password))
                    zip.Password = _password;

                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.AddSelectedFiles(SelectedFileQuery(model.BackupFiles), model.SourceDir, model.Name, true);
                zip.Save();
            }
        }

        private string SelectedFileQuery(List<string> backupFiles)
        {
            string query = "";

            for(int i = 0; i < backupFiles.Count; i++)
            {
                if (backupFiles[i].ToUpper() == "BAK")
                    query += $"(name=*.{backupFiles[i]} AND mtime>={DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)})";
                else
                    query += $"(name=*.{backupFiles[i]})";
                query += (i < backupFiles.Count-1) ? " OR " : "";
            }

            return query;
        }

        private string GetCompressionExtension(string CompressionType)
        {
            string format = CompressionType.ToUpper();

            switch (format)
            {
                case "RAR":
                    format = ".rar";
                    break;
                case "ZIP":
                    format = ".zip";
                    break;
                case "GZIP":
                    format = ".gzip";
                    break;
                case "BZIP":
                    format = ".bzip2";
                    break;
                case "7ZAP":
                    format = ".7zap";
                    break;
                case "TAR":
                    format = ".tar";
                    break;
                default:
                    format = "";
                    break;
            }

            return format;
        }
    }
}
