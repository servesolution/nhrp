using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace MIS.Services.Core
{
    public class FileEncryptionHelper
    {
        public FileEncryptionHelper()
        { }

        ///<summary>
        /// Encrypts a file.
        ///</summary>
        ///<param name="sInputFile">Source file to encrypt</param>
        ///<param name="sOutputFile">Destination file after encryption</param>
        ///<param name="sKey">The key used to encrypt</param>
        ///<returns>Returns a boolean value that represents whether the encryption of file has been successful or not.</returns>
        public bool EncryptFile(string sInputFile, string sOutputFile, string sKey)
        {
            bool success = false;
            Rfc2898DeriveBytes pdb = null;
            FileStream fsCrypt = null;
            RijndaelManaged RMCrypto = null;
            CryptoStream cs = null;
            FileStream fsIn = null;
            try
            {
                string password = sKey; // Your Key Here

                byte[] salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
                pdb = new Rfc2898DeriveBytes(password, salt);

                //UnicodeEncoding UE = new UnicodeEncoding();
                //byte[] key = UE.GetBytes(password);

                string cryptFile = sOutputFile;
                fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RMCrypto = new RijndaelManaged();

                cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(pdb.GetBytes(32), pdb.GetBytes(16)),
                    CryptoStreamMode.Write);

                fsIn = new FileStream(sInputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                success = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (fsIn != null)
                {
                    fsIn.Close();
                }
                if (cs != null)
                {
                    cs.Close();
                }
                if (RMCrypto != null)
                {
                    RMCrypto.Clear();
                }
                if (fsCrypt != null)
                {
                    fsCrypt.Close();
                }
                if (pdb != null)
                {
                    pdb = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return success;
        }
        ///<summary>
        /// Decrypts a file.
        ///</summary>
        ///<param name="sInputFile">Source file to decrypt</param>
        ///<param name="sOutputFile">Destination file after decryption</param>
        ///<param name="sKey">The key used to decrypt</param>
        ///<returns>Returns a boolean value that represents whether the decryption of file has been successful or not.</returns>
        public bool DecryptFile(string sInputFile, string sOutputFile, string sKey)
        {
            bool success = false;
            Rfc2898DeriveBytes pdb = null;
            FileStream fsCrypt = null;
            RijndaelManaged RMCrypto = null;
            CryptoStream cs = null;
            FileStream fsOut = null;
            try
            {
                string password = sKey; // Your Key Here

                byte[] salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
                pdb = new Rfc2898DeriveBytes(password, salt);

                //UnicodeEncoding UE = new UnicodeEncoding();
                //byte[] key = UE.GetBytes(password);

                fsCrypt = new FileStream(sInputFile, FileMode.Open);

                RMCrypto = new RijndaelManaged();

                cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(pdb.GetBytes(32), pdb.GetBytes(16)),
                    CryptoStreamMode.Read);

                fsOut = new FileStream(sOutputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                success = true;
            }
            catch (Exception ex)
            {
                //Log exception only when the key for padding is valid and the exception occurred
                if (ex.Message != "Padding is invalid and cannot be removed.")
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (fsOut != null)
                {
                    fsOut.Close();
                    //fsOut = null;
                }
                if (cs != null)
                {
                    //CyptoStream is not closed as it will throw exception when the key for padding is not valid
                    //cs.Close();
                    cs = null;
                }
                if (RMCrypto != null)
                {
                    RMCrypto.Clear();
                    //RMCrypto = null;
                }
                if (fsCrypt != null)
                {
                    fsCrypt.Close();
                    //fsCrypt = null;
                }
                if (pdb != null)
                {
                    pdb = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return success;
        }

    }
}
