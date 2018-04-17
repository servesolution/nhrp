using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using ExceptionHandler;
namespace MIS.Services.Core
{
    public class Encryption64
    {
        private byte[] key = { };
        private byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

        public string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {

                //key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8));
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.ToCharArray(), 0, 8);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return ex.Message;
            }
        }

        public string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.ToCharArray(), 0, 8);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return ex.Message;
            }
        }

    }
}