using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Web.Routing;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Configuration;
using System.Globalization;
using MIS.Models.Core;
using EntityFramework;
using ExceptionHandler;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MIS.Services.Core;
using System.Data.Common;
using MIS.Models.Payment.Setup;
using System.Net;
namespace MIS.Services.Core
{
    public static partial class Utils
    {

        /// <summary>
        /// extention function that converts object to string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string ConvertToString(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";
            return obj.ToString().Trim();
        }

        /// <summary>
        /// Extention function that converts object to int32
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int number</returns>
        public static int ToInt32(this object obj)
        {
            int val = 0;
            int.TryParse(obj.ConvertToString(), out val);
            return val;
        }

        public static float ToFloat(this object obj)
        {
            float val = 0;
            float.TryParse(obj.ConvertToString(), out val);
            return val;
        }
        public static bool ToBooleanYN(this object obj)
        {
            bool val = false;
            if (obj.ConvertToString() == "Y")
            {
                val = true;
            };
            return val;
        }

        public static bool ToBoolean(this object obj)
        {
            bool val = false;
            bool.TryParse(obj.ConvertToString(), out val);
            return val;
        }

        /// <summary>
        /// Extention function that converts object to int64
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int number</returns>
        public static Int64 ToInt64(this object obj)
        {
            Int64 val = 0;
            Int64.TryParse(obj.ConvertToString(), out val);
            return val;
        }

        /// <summary>
        /// Extention function that finds out if the type is int type
        /// </summary>
        /// <param name="type">Type of object</param>
        /// <returns>True if onverted </returns>
        public static bool IsNumeric(this Type type)
        {
            return (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64) || type == typeof(Decimal) || type == typeof(Double));

        }

        public static string DbDateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DateFormat"].ToString();
            }
        }

        public static DateTime ToDateTime(this object obj, string format)
        {
            format = format ?? Utils.DbDateFormat;

            DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            dtf.ShortDatePattern = format;
            return Convert.ToDateTime(obj, dtf);
        }

        public static DateTime? ToDateTime(this object obj)
        {
            DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            dtf.ShortDatePattern = Utils.DbDateFormat;
            try
            {
                return Convert.ToDateTime(obj, dtf);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Extention function that check boolean and returns Y or N
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="inMode">True/fales</param>
        /// <returns>String</returns>
        public static string ToSTBoolean(this object obj, Boolean inMode)
        {
            return ((obj.ConvertToString().Trim() != string.Empty) &&
                (obj.ConvertToString().Trim() != "N") ? "Y" : (inMode ? "N" : null));
        }

        /// <summary>
        /// Extention function that check boolean and returns Y or N
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="inMode">True/fales</param>
        /// <returns>String</returns>
        public static string ToYNBoolean(this object obj, Boolean inMode)
        {
            return ((obj.ConvertToString().Trim() != string.Empty) &&
                (obj.ConvertToString().Trim().ToLower() != "false") ? "Y" : (inMode ? "N" : null));
        }

        /// <summary>
        /// Converts an object to decimal
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <returns>Returns decimal.</returns>
        public static Decimal? ToDecimal(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Converts an object to decimal with povided decimal place
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <param name="decimalPlace">Decimal place</param>
        /// <returns>Returns decimal with povided decimal place.</returns>
        public static Decimal? ToDecimal(this object obj, int decimalPlace)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            try
            {
                return Math.Round(Convert.ToDecimal(obj), decimalPlace);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// extention function that Get Unicode toggle words from excel sheet
        /// </summary> 
        /// <returns></returns>
        public static void GetLanguageList()
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string physicalPath = HttpContext.Current.Request.MapPath(appPath);
            string filepath = System.IO.Path.GetFullPath(physicalPath + @"\Unicode\unicodexml.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataTable dtRes = new DataTable();
          //  dtRes = ReadFromEXcel(filepath);
            if (ds.Tables[0] != null)
            {
                HttpContext.Current.Session["UnicodeWords"] = ds.Tables[0];
            }
            else
            {
                HttpContext.Current.Session["UnicodeWords"] = null;
            }

        }

        /// <summary>
        /// Get Label name according to language setting
        /// </summary> 
        ///<param name="LabelName"></param>
        /// <returns>string</returns>
        public static string GetLabel(string labelName)
        {
            //labelName = labelName.ToUpper();
            if (!String.IsNullOrEmpty(labelName))
            {
                string sessionLanguage = "";
                DataTable dtUnicode = null;
                if (HttpContext.Current.Session["LanguageSetting"] != null)
                {
                    sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                }
                if (HttpContext.Current.Session["UnicodeWords"] != null)
                {
                    dtUnicode = (DataTable)HttpContext.Current.Session["UnicodeWords"];
                }
                if (sessionLanguage != "English")
                {
                    if (dtUnicode != null)
                    {
                        var drNepaliLabel = dtUnicode.AsEnumerable().Where(s => s.Field<string>("A") == labelName.ToUpper());
                        foreach (var dr in drNepaliLabel)
                        {
                            labelName = (dr["B"] == null) ? labelName : dr["B"].ToString();
                        }
                        //labelName = labelName.Replace("0", "०").Replace("1", "१").Replace("2", "२").Replace("3", "३").Replace("4", "४").Replace("5", "५").Replace("6", "६").Replace("7", "७").Replace("8", "८").Replace("9", "९");
                    }
                }
            }
            return labelName;
        }
        /// <summary>
        /// Get number according to language setting
        /// </summary> 
        ///<param name="number"></param>
        /// <returns>string</returns>
        public static string GetNumber(string number)
        {
            string newVal = "";
            foreach (char c in number)
            {
                newVal += GetLabel(c.ToString());
            }
            if (newVal != "")
                number = newVal;
            return number;
        }

        /// <summary>
        /// Get Label with alternate language of session
        /// </summary>
        /// <param name="LabelName"></param>
        /// <returns>string</returns>
        public static string GetAlternateLabel(string labelName)
        {
            string sessionLanguage = "";
            DataTable dtUnicode = null;
            labelName = labelName.ToUpper();
            if (HttpContext.Current.Session["LanguageSetting"] != null)
            {
                sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            }
            if (HttpContext.Current.Session["UnicodeWords"] != null)
            {
                dtUnicode = (DataTable)HttpContext.Current.Session["UnicodeWords"];
            }
            if (sessionLanguage == "English")
            {
                var drNepaliLabel = dtUnicode.AsEnumerable().Where(s => s.Field<string>("A") == labelName);
                foreach (var dr in drNepaliLabel)
                {
                    labelName = (dr["B"] == null) ? labelName : dr["B"].ToString();
                }
            }
            return labelName;
        }

        /// <summary>
        /// Toggle data from database according to session language
        /// </summary>
        /// <param name="ENG"></param>
        /// <param name="LOC"></param>
        /// <returns>string</returns>
        public static string ToggleLanguage(string DESC_ENG, string DESC_LOC)
        {
            string sessionLanguage = "";
            if (HttpContext.Current.Session["LanguageSetting"] != null)
            {
                sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            }
            if (sessionLanguage == "English")
            {
                return DESC_ENG;
            }
            else
            {
                return DESC_LOC;
            }
        }

        /// <summary>
        /// Change Session Language
        /// </summary> 
        /// <returns>string</returns>
        public static string Changelanguage()
        {
            string strLabel = "";
            string sessionLanguage = "";
            if (HttpContext.Current.Session["LanguageSetting"] == null)
            {
                sessionLanguage = "English";
            }
            else
            {
                sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();

            }
            if (sessionLanguage == "English")
            {
                strLabel = GetAlternateLabel("In Nepali");

            }
            else
            {
                strLabel = "In English";
            }
            return strLabel;
        }

        public static bool ConvertBoolean(this object obj)
        {
            //string st = "";
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            else
            {
                if (obj.ConvertToString() == "N")
                    return false;
                else if (obj.ConvertToString() == "Y")
                    return true;
            }
            return false;
            //return obj.ToString().Trim();
            //return st; 
        }

        #region Unused Function
        public static string ConvertAsciiToUnicode(string asciiString)
        {
            // Create two different encodings.
            Encoding encAscii = Encoding.ASCII;
            Encoding encUnicode = Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] asciiBytes = encAscii.GetBytes(asciiString);

            // Perform the conversion from one encoding to the other.
            byte[] unicodeBytes = Encoding.Convert(encAscii, encUnicode, asciiBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] unicodeChars = new char[encUnicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
            encUnicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, unicodeChars, 0);
            string unicodeString = new string(unicodeChars);

            // Return the new unicode string
            return unicodeString;
        }
        public static string UTF8toASCII(string text)
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            Byte[] encodedBytes = utf8.GetBytes(text);
            Byte[] convertedBytes =
                    Encoding.Convert(Encoding.UTF8, Encoding.ASCII, encodedBytes);
            System.Text.Encoding ascii = System.Text.Encoding.ASCII;

            string str = ascii.GetString(convertedBytes);
            return str;
        }
        public static string ConvertUnicodeToAscii(string unicodeString)
        {
            //string test = "abc pi(\\u0915)";
            // Create two different encodings.
            Encoding encAscii = Encoding.ASCII;
            Encoding encUnicode = Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] unicodeBytes = encUnicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(encUnicode, encAscii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[encAscii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            encAscii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Return the new ascii string
            return asciiString;
        }
        #endregion

        /// <summary>
        /// Convert HTML entity to Unicode character
        /// </summary>
        /// <param name="Html entity"></param> 
        /// <returns>string</returns>
        public static string HTMLToUnicode(string text)
        {

            string[] strTotalword = text.Split(',');
            string unicodeString = "";
            StringBuilder str = new StringBuilder();
            foreach (string strPartial in strTotalword)
            {
                if (strPartial != "")
                {
                    string codePoint = (Convert.ToInt32(strPartial)).ToString("x4");
                    int code = int.Parse(codePoint, System.Globalization.NumberStyles.HexNumber);
                    unicodeString = char.ConvertFromUtf32(code).ToString();
                    str.Append(unicodeString);
                }
            }
            unicodeString = str.ToString();
            return unicodeString;
        }


        /// <summary>
        /// Set Url for current page
        /// </summary>
        /// <param name="Url Helper"></param> 
        /// <returns></returns>
        public static void setUrl(this UrlHelper urlHelper)
        {
            var routeValueDictionary = urlHelper.RequestContext.RouteData.Values;
            var rootUrl = urlHelper.Content("~/");
            string.Format("{0}{1}/{2}/", rootUrl, routeValueDictionary["controller"], routeValueDictionary["action"]);
            if (CommonVariables.currentAction != routeValueDictionary["action"].ToString() || CommonVariables.currentController != routeValueDictionary["controller"].ToString())
            {
                CommonVariables.currentAction = routeValueDictionary["action"].ToString();
                CommonVariables.currentController = routeValueDictionary["controller"].ToString();

                CommonVariables.SearchGroupCode = string.Empty;
                CommonVariables.SearchLoginName = string.Empty;
                CommonVariables.SearchUserName = string.Empty;
            }
        }

        /// <summary>
        /// Encrypt password 
        /// </summary>
        /// <param name="string Message"></param> 
        /// <returns>String</returns>
        public static string EncryptString(string Message)
        {
            string Passphrase = "Secure";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

            }
            finally
            {                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        /// <summary>
        /// Decrypt Password
        /// </summary>
        /// <param name="String Message"></param>
        /// <param name="LOC"></param>
        /// <returns>string</returns>
        public static string DecryptString(string Message)
        {
            string Passphrase = "Secure";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);

        }

        public static bool IsNumeric(this object obj)
        {
            decimal result = 0;
            return decimal.TryParse(obj.ConvertToString(), out result);
        }

        public static bool IsDate(this object obj)
        {
            DateTime dt;
            return DateTime.TryParse(obj.ConvertToString(), out dt);
        }

        public static bool CreateFile(String strData, string filePath)
        {
            try
            {
                // StringWriter stringWriter = new StringWriter(strData);
                if (!string.IsNullOrEmpty(strData))
                {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                System.IO.File.WriteAllText(filePath, strData.ToString(), Encoding.UTF8);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return false;
            }
        }
        public static bool CreateFile(StringBuilder strData, string filePath)
        {
            try
            {
               // StringWriter stringWriter = new StringWriter(strData);
                //if (!string.IsNullOrEmpty(strData))
                //{
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    System.IO.File.WriteAllText(filePath, strData.ToString(), Encoding.UTF8);
                //}
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return false;
            }
        }

        public static bool ExportToExcel(DataTable dataTable, String fileName, string sheetName = "Sheet1")
        {
            bool isBroken = false;
            if (dataTable != null)
            {

                object m_objOpt = null;
                Excel.Application m_objExcel = null;
                Excel.Workbooks m_objBooks = null;
                Excel._Workbook m_objBook = null;
                Excel.Sheets m_objSheets = null;
                Excel._Worksheet m_objSheet = null;
                Excel.Range m_objRange = null;
                Excel.Font m_objFont = null;
                try
                {
                    m_objOpt = System.Reflection.Missing.Value;
                    m_objExcel = new Excel.Application();
                    m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
                    m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
                    m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
                    m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
                    m_objExcel.DisplayAlerts = false;
                    //Creating Headers value
                    int columns = dataTable.Columns.Count;
                    if (columns <= 0)
                    {
                        ExceptionManager.AppendLog(new Exception("Datatable should contain at least one column."));
                    }

                    object[] objHeaders = new object[columns];
                    for (int i = 0; i < columns; i++)
                    { objHeaders[i] = (object)dataTable.Columns[i].ColumnName; }
                    m_objRange = m_objSheet.get_Range("A1", m_objOpt);
                    m_objRange = m_objRange.get_Resize(1, columns);
                    m_objRange.set_Value(m_objOpt, objHeaders);
                    m_objFont = m_objRange.Font;
                    m_objFont.Bold = true;
                    //Creates the data for data cell
                    int rows = dataTable.Rows.Count;
                    if (rows > 0)
                    {
                        object[,] objData = new object[rows, columns];
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                if (dataTable.Rows[i][j].IsNumeric())
                                {
                                    if (dataTable.Rows[i][j].ConvertToString().Length <= 10)
                                    {
                                        objData[i, j] = (object)GetNumber(dataTable.Rows[i][j].ConvertToString());
                                    }
                                    else
                                    {
                                        objData[i, j] = string.Concat("'", ((object)GetNumber((dataTable.Rows[i][j]).ConvertToString())).ConvertToString());
                                    }
                                }
                                else if (dataTable.Rows[i][j].IsDate())
                                {
                                    //objData[i, j] = (object)Convert.ToDateTime(dataTable.Rows[i][j]).ToShortDateString();
                                    objData[i, j] = (object)GetNumber(Convert.ToDateTime(dataTable.Rows[i][j]).ToString("dd/MMM/yyyy"));
                                }
                                else
                                {
                                    objData[i, j] = string.Concat("'", ((object)GetNumber((dataTable.Rows[i][j]).ConvertToString())).ConvertToString());
                                }
                            }
                            if (isBroken)
                                break;
                        }
                        m_objRange = m_objSheet.get_Range("A2", m_objOpt);
                        m_objRange = m_objRange.get_Resize(rows, columns);
                        m_objRange.set_Value(m_objOpt, objData);
                    }
                    m_objSheet.Name = sheetName;
                    m_objBook.SaveAs(fileName, m_objOpt, m_objOpt,
                        m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
                        m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
                    m_objBook.Close(false, m_objOpt, m_objOpt);
                    m_objExcel.Quit();
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    throw new Exception(ex.Message);
                }
                finally
                {

                    //Clean-up
                    if (m_objExcel != null)
                    {
                        m_objExcel.Quit();
                        Marshal.ReleaseComObject(m_objExcel);
                    }
                    m_objOpt = null;
                    m_objFont = null;
                    m_objRange = null;
                    m_objSheet = null;
                    m_objSheets = null;
                    m_objBooks = null;
                    m_objBook = null;
                    m_objExcel = null;
                    GC.Collect();
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ExportToExcel2(DataTable dataTable, String fileName)
        {
            bool isFileGenerated = false;
            StringBuilder strExcelBuilder = new StringBuilder();
            if (dataTable != null)
            {
                try
                {

                    int columns = dataTable.Columns.Count;
                    if (columns <= 0)
                    {
                        ExceptionManager.AppendLog(new Exception("Datatable should contain at least one column."));
                        return false;
                    }
                    else
                    {
                        strExcelBuilder.Append("<table>");
                        strExcelBuilder.Append("<tr>");
                        for (int i = 0; i < columns; i++)
                        {
                            strExcelBuilder.Append("<td>");
                            strExcelBuilder.Append(dataTable.Columns[i].ToString());
                            strExcelBuilder.Append("</td>");
                        }
                        strExcelBuilder.Append("</tr>");
                        if (dataTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                strExcelBuilder.Append("<tr>");
                                for (int j = 0; j < dataTable.Columns.Count; j++)
                                {
                                    strExcelBuilder.Append("<td>");
                                    strExcelBuilder.Append(dataTable.Rows[i][j].ToString());
                                    strExcelBuilder.Append("</td>");
                                }
                                strExcelBuilder.Append("</tr>");
                            }
                        }
                        strExcelBuilder.Append("</table>");
                    }
                    isFileGenerated = Utils.CreateFile(strExcelBuilder.ToString(), fileName);
                    return isFileGenerated;
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //For MISLITE master data 
        public static bool ExportToExcel(Dictionary<string, string> dicTableQuery, String fileName)
        {
            bool isBroken = false;
            object m_objOpt = null;
            Excel.Application m_objExcel = null;
            Excel.Workbooks m_objBooks = null;
            Excel._Workbook m_objBook = null;
            Excel.Sheets m_objSheets = null;
            Excel._Worksheet m_objSheet = null;
            Excel.Range m_objRange = null;
            Excel.Font m_objFont = null;
            DataTable dataTable = null;
            int k = 0;
            if (dicTableQuery != null && dicTableQuery.Count > 0)
            {

                try
                {
                    m_objOpt = System.Reflection.Missing.Value;
                    m_objExcel = new Excel.Application();
                    m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
                    m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
                    m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
                    m_objExcel.DisplayAlerts = false;
                    while (m_objBook.Sheets.Count > 1)
                    {
                        ((Excel._Worksheet)m_objSheets.get_Item(m_objBook.Sheets.Count - 1)).Delete();
                    }

                    int defSheetCount = m_objSheets.Count;
                    //Creating Headers value
                    foreach (var pair in dicTableQuery)
                    {

                        //GetData
                        if (k > 0)
                        {
                            m_objSheet = m_objBook.Sheets.Add(m_objOpt, m_objOpt, 1, m_objOpt) as Excel._Worksheet;
                        }
                        else
                        {
                            m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
                        }
                        k++;
                        string strQuery = pair.Value.ConvertToString();
                        using (ServiceFactory service = new ServiceFactory())
                        {
                            service.Begin();
                            try
                            {
                                dataTable = service.GetDataTable(strQuery, null);
                            }
                            catch (Exception ex)
                            {
                                dataTable = null;
                                ExceptionManager.AppendLog(ex);
                            }
                            finally
                            {

                                if (service.Transaction != null)
                                {
                                    service.End();
                                }
                            }
                        }
                        if (dataTable != null)
                        {
                            int columns = dataTable.Columns.Count;
                            if (columns <= 0)
                            {
                                ExceptionManager.AppendLog(new Exception("Datatable should contain at least one column."));
                            }

                            object[] objHeaders = new object[columns];
                            for (int i = 0; i < columns; i++)
                            {
                                if (dataTable.Columns[i].ColumnName.ToUpper().Contains("DATE") || dataTable.Columns[i].ColumnName.ToUpper().Contains("DT") || dataTable.Columns[i].ColumnName.ToUpper().Contains("FISCAL_YR"))
                                {
                                    Excel.Range cellRange = m_objSheet.get_Range(string.Concat(GetExcelColumnName(i + 1), "2"), string.Concat(GetExcelColumnName(i + 1), (dataTable.Rows.Count + 1).ToString()));
                                    //cellRange.NumberFormat = "dd-MM-yyyy";
                                    cellRange.NumberFormat = "@";
                                }
                                objHeaders[i] = (object)dataTable.Columns[i].ColumnName;
                            }
                            m_objRange = m_objSheet.get_Range("A1", m_objOpt);
                            m_objRange = m_objRange.get_Resize(1, columns);
                            m_objRange.set_Value(m_objOpt, objHeaders);
                            m_objFont = m_objRange.Font;
                            m_objFont.Bold = true;
                            //Creates the data for data cell
                            int rows = dataTable.Rows.Count;
                            if (rows > 0)
                            {
                                object[,] objData = new object[rows, columns];
                                for (int i = 0; i < rows; i++)
                                {
                                    for (int j = 0; j < columns; j++)
                                    {
                                        if (dataTable.Rows[i][j].IsNumeric())
                                        {
                                            if (dataTable.Rows[i][j].ConvertToString().Length <= 10)
                                            {
                                                objData[i, j] = (object)GetNumber(dataTable.Rows[i][j].ConvertToString());
                                            }
                                            else
                                            {
                                                objData[i, j] = string.Concat("'", ((object)GetNumber((dataTable.Rows[i][j]).ConvertToString())).ConvertToString());
                                            }
                                        }
                                        else if (dataTable.Rows[i][j].IsDate() && ((dataTable.Columns[j].ColumnName.ToUpper().Trim() != "NEPALI_ST_DATE") && (dataTable.Columns[j].ColumnName.ToUpper().Trim() != "FISCAL_YR")))
                                        {
                                            //objData[i, j] = (object)Convert.ToDateTime(dataTable.Rows[i][j]).ToShortDateString();
                                            objData[i, j] = (object)GetNumber(Convert.ToDateTime(dataTable.Rows[i][j]).ToString("dd-MMM-yyyy"));
                                        }
                                        else
                                        {
                                            objData[i, j] = string.Concat("'", ((object)GetNumber((dataTable.Rows[i][j]).ConvertToString())).ConvertToString());
                                        }
                                    }
                                    if (isBroken)
                                        break;
                                }
                                m_objRange = m_objSheet.get_Range("A2", m_objOpt);
                                m_objRange = m_objRange.get_Resize(rows, columns);
                                m_objRange.set_Value(m_objOpt, objData);
                            }
                            m_objSheet.Name = pair.Key.ConvertToString();
                        }
                    }
                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }
                    m_objBook.SaveAs(fileName, m_objOpt, m_objOpt,
                        m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
                        m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
                    m_objBook.Close(false, m_objOpt, m_objOpt);
                    m_objExcel.Quit();
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    return false;
                }
                finally
                {

                    //Clean-up
                    if (m_objExcel != null)
                    {
                        m_objExcel.Quit();
                        Marshal.ReleaseComObject(m_objExcel);
                    }
                    m_objOpt = null;
                    m_objFont = null;
                    m_objRange = null;
                    m_objSheet = null;
                    m_objSheets = null;
                    m_objBooks = null;
                    m_objBook = null;
                    m_objExcel = null;
                    GC.Collect();
                }
            }
            else
            {
                return false;
            }
        }

        public static string GetExcelColumnName(int columnNo)
        {
            int nCol = columnNo;
            string sChars = "0ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string sCol = string.Empty;
            while (nCol > 26)
            {
                int nChar = nCol % 26;
                if (nChar == 0)
                    nChar = 26;
                nCol = (nCol - nChar) / 26;
                sCol = sChars[nChar] + sCol;
            }
            if (nCol != 0)
                sCol = sChars[nCol] + sCol;

            return sCol;
        }
        public static DataTable ReadFromEXcel(string filepath, string sheetname = "Sheet1")
        {
            DataTable dtExcel = null;
            string extension = "";
            string connString = "";
            if (filepath != "")
            {
                if (filepath.Contains("."))
                {
                    extension = Path.GetExtension(filepath);
                }
                if (extension == ".xls")
                {
                    connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filepath + ";" + "Extended Properties=Excel 8.0;";
                }
                else
                {
                    connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filepath + ";" + "Extended Properties=Excel 12.0;";
                }
                OleDbConnection oledbConn = new OleDbConnection(connString);
                try
                {
                    oledbConn.Open();
                    string query = string.Format("SELECT * FROM [{0}$]", sheetname);
                    using (OleDbDataAdapter myCommand = new OleDbDataAdapter(query, oledbConn))
                    {
                        dtExcel = new DataTable();
                        myCommand.Fill(dtExcel);
                    }

                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    dtExcel = null;
                }
                finally
                {
                    oledbConn.Close();
                }
            }
            return dtExcel;
        }

        public static DataSet ReadDataSetFromExcel(string filepath)
        {
            DataSet dsExcel = null;
            string extension = "";
            string connString = "";
            if (filepath != "")
            {
                if (filepath.Contains("."))
                {
                    extension = Path.GetExtension(filepath);
                }
                if (extension == ".xls")
                {
                    connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filepath + ";" + "Extended Properties=Excel 8.0;";
                }
                else
                {
                    connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filepath + ";" + "Extended Properties=Excel 12.0;";
                }
                OleDbConnection oledbConn = new OleDbConnection(connString);
                try
                {
                    oledbConn.Open();
                    DataTable dtTables = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dtTables != null && dtTables.Rows.Count > 0)
                    {
                        dsExcel = new DataSet("dsExcel");
                        foreach (DataRow drIn in dtTables.Rows)
                        {
                            string tableName = drIn["TABLE_NAME"].ConvertToString();
                            string query = string.Format("SELECT * FROM [{0}]", tableName);

                            using (OleDbDataAdapter myCommand = new OleDbDataAdapter(query, oledbConn))
                            {
                                DataTable dt = new DataTable(tableName.Trim(new char[] { '$' }));
                                myCommand.Fill(dt);
                                dsExcel.Tables.Add(dt);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    dsExcel = null;
                }
                finally
                {
                    if (oledbConn.State == ConnectionState.Open) oledbConn.Close();
                }
            }
            return dsExcel;
        }


        #region "Import Excel"
        /// <summary>
        /// Makes the uploadformat list by datatable row and returns it
        /// </summary>
        /// <param name="dtbl">data table of uploadtype</param>
        /// <returns>List<UploadFormat></returns>
        public static List<UploadFormat> GetUploadFormat(DataTable dtbl)
        {
            List<UploadFormat> list = new List<UploadFormat>();
            foreach (DataRow row in dtbl.Rows)
            {
                string colSize = row["COL_SIZE"] == null || row["COL_SIZE"].ConvertToString() == "" ? "0" : row["COL_SIZE"].ConvertToString();
                list.Add(new UploadFormat()
                {
                    ColHeadingName = row["COL_HEADING_NAME"].ConvertToString(),
                    ColumnOrder = row["COL_ORDER"].ConvertToString(),
                    DbHeadingName = row["DB_COLUMN_NAME"].ConvertToString(),
                    DataType = row["DATA_TYPE"].ConvertToString(),
                    ColumnSize = colSize.ToInt32()
                });
            }
            return list;
        }

        /// <summary>
        /// Get scheama of uploaded xlsx file
        /// </summary>
        /// <param name="oleConn">Oledb connection</param>
        /// <param name="sheetName">Sheet name</param>
        /// <returns>data table</returns>
        public static DataTable GetExcelScheama(OleDbConnection oleConn, string sheetName)
        {
            DataTable schemaTable = null;
            if (!string.IsNullOrWhiteSpace(sheetName) && !sheetName.EndsWith("$")) sheetName += "$";
            try
            {
                OleDbCommand cmd = oleConn.CreateCommand();
                DataTable dt = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                schemaTable = cmd.ExecuteReader(CommandBehavior.KeyInfo).GetSchemaTable();
            }
            catch (Exception ex)
            {
                //Thread.Sleep(15000);
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }

            return schemaTable;
        }

        /// <summary>
        /// Gets oledb connection that opens the uploaded xlsx file
        /// </summary>
        /// <param name="fName">xlsx file name</param>
        /// <returns>Oledb object</returns>
        public static OleDbConnection GetExcelConnection(string fName)
        {
            OleDbConnection oleConn = new OleDbConnection();
            try
            {
                string strConn = string.Empty;
                if (Path.GetExtension(fName).ToUpper() == "XLS")
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=" + fName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fName + ";" + "Extended Properties=Excel 12.0;";

                oleConn.ConnectionString = strConn;
                oleConn.Open();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            return oleConn;
        }

        /// <summary>
        /// Maps the headers of uploaded file with database fields
        /// </summary>
        /// <param name="dtblXlsScheama">Uploaded xlsx DataTable</param>
        /// <param name="list">List of database fields</param>
        /// <returns>boolean</returns>
        public static string IsColumnsValid(DataTable dtblXlsSchema, List<UploadFormat> list, string tableName = "")
        {
            int column = 1;
            string msg = string.Empty;
            foreach (DataRow dr in dtblXlsSchema.Rows)
            {
                var cols = from col in list
                           where col.ColHeadingName.EqualsTo(dr["columnname"].ConvertToString())
                           select new { Order1 = col.ColumnOrder, Order2 = dr["columnordinal"].ConvertToString(), colheadingname = col.ColHeadingName };
                var obj1 = cols.FirstOrDefault();
                if (obj1 != null)
                {
                    if (Convert.ToInt32(obj1.Order1) != Convert.ToInt32(obj1.Order2) + 1)
                    {
                        msg += "<b>" + dr["columnname"].ConvertToString() + "</b> does not match with <b>" + obj1.colheadingname + "</b> Column No " + column + "";
                    }
                }
                else
                {
                    msg += "<b>" + dr["columnname"].ConvertToString() + "</b> does not match. Please check column no. " + column + "";
                };
                column++;
            }
            if (list.Count != dtblXlsSchema.Rows.Count)
            {
                msg += "Number of columns in template is <b>" + dtblXlsSchema.Rows.Count + "</b> and database is <b>" + list.Count + "</b>";

            }
            if (!String.IsNullOrEmpty(msg))
            {
                if (!String.IsNullOrEmpty(tableName))
                {
                    msg += " in <b>" + tableName.ConvertToString() + "</b>";
                }
                msg += "<br />";
            }
            return msg;

            #region Commented One By One Error Display
            //int column = 1;
            //foreach (DataRow dr in dtblXlsSchema.Rows)
            //{
            //    var cols = from col in list
            //               where col.ColHeadingName.EqualsTo(dr["columnname"].ConvertToString())
            //               select new { Order1 = col.ColumnOrder, Order2 = dr["columnordinal"].ConvertToString(), colheadingname = col.ColHeadingName };
            //    var obj1 = cols.FirstOrDefault();
            //    if (obj1 != null)
            //    {
            //        if (Convert.ToInt32(obj1.Order1) != Convert.ToInt32(obj1.Order2) + 1)
            //        {
            //            return "<b>" + dr["columnname"].ConvertToString() + "</b> does not match with <b>" + obj1.colheadingname + "</b> Column No " + column;
            //        }
            //    }
            //    else
            //    {
            //        return "<b>" + dr["columnname"].ConvertToString() + "</b> does not match. Please check column no. " + column;
            //    };
            //    column++;
            //}
            //if (list.Count != dtblXlsSchema.Rows.Count)
            //{
            //    return "Number of columns in template is <b>" + dtblXlsSchema.Rows.Count + "</b> and database is <b>" + list.Count + "<b>";
            //}
            //return ""; 
            #endregion
        }

        #endregion

        /// <summary>
        /// Fixes the string for Column name
        /// </summary>
        /// <param name="obj">String to be fixed</param>
        /// <returns>Returns the fixed string</returns>
        public static string FixToColumnName(this string obj)
        {
            if (obj == null)
                return string.Empty;

            string fixedName = string.Empty;
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["LanguageSetting"] != null && System.Web.HttpContext.Current.Session["LanguageSetting"].ConvertToString().ToUpper() == "ENGLISH")
            {
                fixedName = System.Text.RegularExpressions.Regex.Replace(obj, "[^a-zA-Z0-9_]", "_");
            }
            else
            {
                fixedName = obj.Replace(" ", "_").Replace("(", "_").Replace(")", "_").Replace("&", "_").Replace("@", "_").Replace("#", "_").Replace("%", "_").Replace("*", "_").Replace("/", "_").Replace("+", "_").Replace("-", "_");
            }

            fixedName = System.Text.RegularExpressions.Regex.Replace(fixedName, "(_)+", "_");
            return fixedName;
        }
        /// <summary>
        /// Converts an object to string in formatted form. 
        /// For example: ConvertToString("N2") returns an object to string
        /// with 2 decimal places. It formats an object only if they
        /// are is correct format and convertible to target type.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <param name="format">Format string i.e. N2, D2, dd/MM/yyyy etc.</param>
        /// <returns>Returns formatted string.</returns>
        public static string ConvertToString(this object obj, string format)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;

            return string.Format("{0:" + format + "}", obj);
        }

        /// <summary>
        /// Fixes the single quotes in the string
        /// </summary>
        /// <param name="obj">String to be fixed</param>
        /// <returns>Returns the fixed string</returns>
        public static string FixQuote(this string obj)
        {
            if (obj == null)
                return string.Empty;

            string fixedName = string.Empty;
            fixedName = obj.Trim().Replace("'", "''");
            return fixedName;
        }

        /// <summary>
        /// Compares two System.String objects, ignoring or honoring their case, and returns true or false that indicates their comparison.
        /// </summary>
        /// <param name="args1">First String object to be compared</param>
        /// <param name="args2">Second String object to be compared with.</param>
        /// <returns>Returns boolean value that indicates the comparison of the objects.</returns>
        public static bool EqualsTo(this string args1, string args2)
        {
            return string.Compare(args1.ConvertToString(), args2.ConvertToString(), true) == 0;
        }

        /// <summary>
        /// Splits string according to the delimiters provided.<para></para>
        /// For example: <para></para>
        /// ("Hello World").SplitString(" ") returns a string array containing two items as "Hello" and "World".<para></para>
        /// ("Yesterday,Today,Tommorow_is_holiday").SplitString(",_") returns a string array containing five items as "Yesterday", "Today", "Tommorow", "is" and "holiday".
        /// </summary>
        /// <param name="obj">String to split</param>
        /// <param name="delimiters">Delimiter used to split the string. This may be one or many.</param>
        /// <returns>Returns string array containing the splitted items.</returns>
        public static string[] SplitString(this string obj, string delimiters)
        {
            char[] delimitersArray = delimiters.ToArray();
            string[] splitted = obj.Split(delimitersArray, StringSplitOptions.RemoveEmptyEntries);
            return splitted;
        }

        /// <summary>
        /// Converts a string to database insertable text. It returns null if string is null, empty or contains only white spaces.
        /// </summary>
        /// <param name="obj">String to be converted</param>
        /// <returns>Returns converted string.</returns>
        public static string ToDbString(this String obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                return null;
            }
            try
            {
                return obj.ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts words into Title Case i.e first letter of word into Upper Case.
        /// If a word contains "_" character, it splits into words.
        /// </summary>
        /// <param name="str">string to changed into Title Case.</param>
        /// <returns></returns>
        public static string ToTitleCase(this string str)
        {
            string st = string.Empty;
            string[] words = str.Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0)
            {
                foreach (string word in words)
                {
                    if (word.Length > 0)
                    {
                        if (string.IsNullOrEmpty(st)) st = string.Concat(st, word.Substring(0, 1).ToUpper(), word.Substring(1).ToLower());
                        else st = string.Concat(st, " ", word.Substring(0, 1).ToUpper(), word.Substring(1).ToLower());
                    }
                }
            }
            return (!string.IsNullOrEmpty(st) ? st : str);
        }

        public static T ToEnum<T>(this string name)
        {
            T en = default(T);
            try
            {
                en = (T)Enum.Parse(typeof(T), name, true);
            }
            catch { }
            return en;
        }


        public static void RemoveEmptyRows(this DataTable table)
        {
            if (table == null) return;

            int cols = table.Columns.Count;
            for (int i = (table.Rows.Count - 1); i >= 0; i--)
            {
                if (IsBlankRow(table, i, cols))
                {
                    table.Rows[i].Delete();
                    table.AcceptChanges();
                }
            }
        }
        static bool IsBlankRow(DataTable table, int index, int columns)
        {
            if (table == null) return true;
            for (int i = 0; i < columns; i++)
            {
                var val = table.Rows[index][i].ConvertToString();
                if (val != null && val != string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public static QueryResult SubmitChanges(this ServiceFactory service, NameValueCollection args, string storeProcedureName)
        {
            storeProcedureName = service.CreateProcedureName(storeProcedureName);
            using (DbCommand cmd = service.Connection.CreateCommand())
            {
                cmd.CommandText = storeProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                service.DiscoverParameters(cmd);
                foreach (DbParameter param in cmd.Parameters)
                {
                    string key = param.ParameterName.Substring(2);
                    NameValuePair pair = args.Find(delegate(NameValuePair p)
                    {
                        return p.Name.EqualsTo(key);
                    });
                    if (pair != null)
                    {
                        if (param.DbType == DbType.DateTime)
                        {
                            if (pair.Value.ConvertToString() != null && pair.Value.ConvertToString() != "")
                            {
                                param.Value = pair.Value.ToDateTime(null);
                            }
                            else
                            {
                                param.Value = DBNull.Value;
                            }
                        }
                        else
                        {
                            param.Value = pair.Value;
                        }
                    }
                    else
                    {
                        param.Value = DBNull.Value;
                    }

                }
                return service.SubmitChanges(cmd);
            }
        }


        public static string SingleEngDate(string day, string month, string year)
        {
            if (!string.IsNullOrWhiteSpace(day) && !string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(year))
            {
                day = Convert.ToInt32(day).ToString("00");
                month = Convert.ToInt32(month).ToString("00");
                year = Convert.ToInt32(year).ToString("0000");
                return string.Format("{0}-{1}-{2}", day, month, year);
            }
            return string.Empty;
        }


        public static bool CheckIfFileExistsOnServer(string fileName, string servername, string username, string psdwrd)
        {
            var request = (FtpWebRequest)WebRequest.Create(servername + fileName);
            request.Credentials = new NetworkCredential(username, psdwrd);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }
    }


}


