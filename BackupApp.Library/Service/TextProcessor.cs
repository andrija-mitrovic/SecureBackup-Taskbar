using BackupApp.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Service
{
    public static class TextProcessor
    {
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static BackupModel ConvertToBackUpModel(this List<string> lines)
        {
            BackupModel model = new BackupModel();

            model.SourceDir = ValueExtraction(lines[0]);
            model.DestinationDir = ValueExtraction(lines[1]);
            model.Database = ValueExtraction(lines[2]); 
            model.Name = ValueExtraction(lines[3]); 
            model.CompressionType = ValueExtraction(lines[4]);
            model.BackupFiles = ValueExtraction(lines[5]).Split(',').ToList();
            model.CurrentFile = Convert.ToInt32(ValueExtraction(lines[6]));

            return model;
        }

        public static EmailModel ConvertToEmailModel(this List<string> lines)
        {
            EmailModel model = new EmailModel();

            model.Host = ValueExtraction(lines[0]);
            model.EmailFrom = ValueExtraction(lines[1]);
            model.EmailTo = ValueExtraction(lines[2]);
            model.Subject = ValueExtraction(lines[3]);
            model.DisplayName = ValueExtraction(lines[4]);
            model.Port = Convert.ToInt32(ValueExtraction(lines[5]));
            model.Username = ValueExtraction(lines[6]);
            model.Password = ValueExtraction(lines[7]);
            model.EnableSsl = Convert.ToBoolean(ValueExtraction(lines[8]));

            return model;
        }

        private static string ValueExtraction(string value)
        {
            int equalIndex = value.IndexOf("=");
            int valueLength = value.Length;

            return value.Substring(equalIndex+1, valueLength-equalIndex-1).Trim(' ');
        }

        public static void UpdateCurrentFile(int currentFile, string destinationFile)
        {
            int editLine = 7;

            string lineToWrite = $"CURRENT_FILE={currentFile}";

            string[] lines = File.ReadAllLines(destinationFile);

            using (StreamWriter writer = new StreamWriter(destinationFile))
            {
                for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
                {
                    if (currentLine == editLine)
                    {
                        writer.WriteLine(lineToWrite);
                    }
                    else
                    {
                        writer.WriteLine(lines[currentLine - 1]);
                    }
                }
            }
        }
    }
}
