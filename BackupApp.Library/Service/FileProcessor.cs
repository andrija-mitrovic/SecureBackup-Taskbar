using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Service
{
    public static class FileProcessor
    {
        public static void Copy(string file, string sourceDirectory, string targetDirectory, List<string> backupFileTypes)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            for (int i = 0; i < backupFileTypes.Count; i++)
            {
                backupFileTypes[i] = "." + backupFileTypes[i].ToLower();
            }

            foreach (FileInfo fi in diSource.GetFiles())
            {
                if (backupFileTypes.Contains(fi.Extension.ToLower()))
                {
                    fi.CopyTo(Path.Combine(diTarget.FullName, fi.Name), true);
                }
            }
        }
        public static void CopyAll(string file, string sourceDirectory, string targetDirectory, List<string> backupFileTypes)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            for (int i = 0; i < backupFileTypes.Count; i++)
            {
                backupFileTypes[i] = "." + backupFileTypes[i].ToLower();
            }

            CopyAllSub(diSource, diTarget, backupFileTypes);
        }

        private static void CopyAllSub(DirectoryInfo source, DirectoryInfo target, List<string> backupFileTypes)
        {
            foreach (FileInfo fi in source.GetFiles())
            {
                if (backupFileTypes.Contains(fi.Extension.ToLower()))
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAllSub(diSourceSubDir, nextTargetSubDir, backupFileTypes);
            }
        }
    }
}
