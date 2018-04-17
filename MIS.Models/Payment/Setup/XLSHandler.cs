using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MIS.Models.Payment.Setup
{
    /// <summary>
    /// Enumurated data type for xls format to upload the data from excel file
    /// /// ======================================
    /// Author:        SSITH Pvt. Ltd.
    /// Created Date:   Jan 09, 2013
    /// Modified Date: 
    /// ======================================
    /// </summary>
    public enum XLSFormats : int
    {
        Accounting = 0,
        Currency = 1,
        NUMCHAR = 2,
        CHARACTER = 3,
        DATE = 4,
        NUMBER = 5,
        Custom = 6
    }


    /// <summary>
    /// XLSHandler class that set the data type of the excel sheet columns
    /// Author:        SSITH Pvt. Ltd.
    /// Created Date:   Jan 09, 2013
    /// Modified Date: 
    /// ======================================
    /// </summary>

    public class XLSHandler
    {
        private char[] chars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        // Format String Equivalents
        string _acct = "_($* #,##0.00_);_($* (#,##0.00);_($* \"-\"??_);_(@_)",
        _currency = "$#,##0.00_);($#,##0.00)",
        _txt = "@",
        _date = "dd-MM-yyyy",
        _numchar = "",
        _custom = "Custom",
        _generalNumber = "#,##0";

        /// <summary>
        /// get the column name from index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ColumnName(int index)
        {
            index -= 1; //adjust so it matches 0-indexed array rather than 1-indexed column
            int quotient = index / 26;
            if (quotient > 0)
                return ColumnName(quotient) + chars[index % 26].ToString();
            else
                return chars[index % 26].ToString();
        }

        /// <summary>
        /// Padding as per the size
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="size">size</param>
        /// <returns></returns>
        public string Padding(string value, int? size)
        {
            string mvalue = value;
            for (int i = value.Length; i < size; i++)
            {
                mvalue = string.Concat("0", mvalue);
            }
            return mvalue;
        }

        /// <summary>
        /// Change the cell format as per the format type
        /// </summary>
        /// <param name="formatType">xls format type</param>
        /// <param name="size">size of the value</param>
        /// <returns></returns>
        public string ChangeCellFormat(XLSFormats formatType, int? size)
        {
            string _format = string.Empty;
            switch(formatType)
            {
                case XLSFormats.Accounting:
                    _format = _acct;
                    break;
                case XLSFormats.Currency:
                    _format = _currency;
                    break;
                case XLSFormats.DATE:
                    _format = _date;
                    break;
                case XLSFormats.NUMCHAR:
                    for (int i = 0; i < size; i++)
                        _format = string.Concat(_format, "0");
                    break;
                case XLSFormats.CHARACTER:
                    _format = _txt;
                    break;
                case XLSFormats.NUMBER:
                    _format = _generalNumber;
                    break;
                case XLSFormats.Custom:
                    _format = _custom;
                    break;
                default:
                    _format = string.Empty;
                    break;
            }
            return _format;
        }
    }
}
