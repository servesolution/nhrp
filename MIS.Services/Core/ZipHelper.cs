using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;


namespace MIS.Services.Core
{
    public class ZipHelper
    {      

        public bool ZipFolder(string sDirectory, string sPassword)
        {
            bool success = false;
            string directoryPath = sDirectory;
            string directoryName = new DirectoryInfo(directoryPath).Name;
            string parentPath = Directory.GetParent(directoryPath).FullName;
            string zipFileName = directoryName + ".zip";
            string zipFilePath = Path.Combine(parentPath, zipFileName);

            ZipFile zip = new ZipFile(zipFileName, Encoding.Unicode);
            try
            {
                zip.TempFileFolder = Path.GetTempPath();
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = sPassword;
                zip.AddDirectory(directoryPath, directoryName);
                if (File.Exists(zipFilePath))
                    File.Delete(zipFilePath);
                zip.Save(zipFilePath);
                success = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (zip != null)
                {
                    zip = null;
                }
            }

            return success;
        }

        public bool UnZipFile(string sFileName, string sPassword, out string sError)
        {
            bool success = false;
            sError = string.Empty;
            string filePath = sFileName;
            string parentPath = Directory.GetParent(filePath).FullName;
            string extractDirectory = parentPath;
            ZipFile zip = new ZipFile(filePath);
            try
            {
                zip.TempFileFolder = Path.GetTempPath();
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = sPassword;
                zip.ExtractAll(extractDirectory, ExtractExistingFileAction.OverwriteSilently);
                success = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper()==("Bad Password").ToUpper())
                    sError = "Password is Invalid";
                else
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (zip != null)
                {
                    zip = null;
                }
            }
            return success;
        }

    }
}
