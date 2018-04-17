using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIS.Services.Core
{
    public  static class NepaliUnicode
    {
        public static IList<SelectListItem> NepaliCharacters()
        {
            IList<SelectListItem> listUnicode = new List<SelectListItem>();
            addListItem("अ", "%E0%A4%85", listUnicode);
            addListItem("आ", "%E0%A4%86", listUnicode);
            addListItem("इ", "%E0%A4%87", listUnicode);
            addListItem("ई", "%E0%A4%88", listUnicode);
            addListItem("उ", "%E0%A4%89", listUnicode);
            addListItem("ऊ", "%E0%A4%8A", listUnicode);
            addListItem("ऋ", "%E0%A4%8B", listUnicode);
            addListItem("ऌ", "%E0%A4%8C", listUnicode);
            addListItem("ऍ", "%E0%A4%8D", listUnicode);
            addListItem("ऎ", "%E0%A4%8E", listUnicode);
            addListItem("ए", "%E0%A4%8F", listUnicode);
            addListItem("ऐ", "%E0%A4%90", listUnicode);
            addListItem("ऑ", "%E0%A4%91", listUnicode);
            addListItem("ऒ", "%E0%A4%92", listUnicode);
            addListItem("ओ", "%E0%A4%93", listUnicode);
            addListItem("औ", "%E0%A4%94", listUnicode);
            addListItem("क", "%E0%A4%95", listUnicode);
            addListItem("ख", "%E0%A4%96", listUnicode);
            addListItem("ग", "%E0%A4%97", listUnicode);
            addListItem("घ", "%E0%A4%98", listUnicode);
            addListItem("ङ", "%E0%A4%99", listUnicode);
            addListItem("च", "%E0%A4%9A", listUnicode);
            addListItem("छ", "%E0%A4%9B", listUnicode);
            addListItem("ज", "%E0%A4%9C", listUnicode);
            addListItem("झ", "%E0%A4%9D", listUnicode);
            addListItem("ञ", "%E0%A4%9E", listUnicode);
            addListItem("ट", "%E0%A4%9F", listUnicode);
            addListItem("ठ", "%E0%A4%A0", listUnicode);
            addListItem("ड", "%E0%A4%A1", listUnicode);
            addListItem("ढ", "%E0%A4%A2", listUnicode);
            addListItem("ण", "%E0%A4%A3", listUnicode);
            addListItem("त", "%E0%A4%A4", listUnicode);
            addListItem("थ", "%E0%A4%A5", listUnicode);
            addListItem("द", "%E0%A4%A6", listUnicode);
            addListItem("ध", "%E0%A4%A7", listUnicode);
            addListItem("न", "%E0%A4%A8", listUnicode);
            addListItem("प", "%E0%A4%AA", listUnicode);
            addListItem("फ", "%E0%A4%AB", listUnicode);
            addListItem("ब", "%E0%A4%AC", listUnicode);
            addListItem("भ", "%E0%A4%AD", listUnicode);
            addListItem("म", "%E0%A4%AE", listUnicode);
            addListItem("य", "%E0%A4%AF", listUnicode);
            addListItem("र", "%E0%A4%B0", listUnicode);
            addListItem("ल", "%E0%A4%B2", listUnicode);
            addListItem("व", "%E0%A4%B5", listUnicode);
            addListItem("श", "%E0%A4%B6", listUnicode);
            addListItem("ष", "%E0%A4%B7", listUnicode);
            addListItem("स", "%E0%A4%B8", listUnicode);
            addListItem("ह", "%E0%A4%B9", listUnicode);
            addListItem("क्ष", "%E0%A4%95%E0%A5%8D%E0%A4%B7", listUnicode);
            addListItem("त्र", "%E0%A4%A4%E0%A5%8D%E0%A4%B0", listUnicode);
            addListItem("ज्ञ", "%E0%A4%9C%E0%A5%8D%E0%A4%9E", listUnicode);
            return listUnicode;
        }
        public static void addListItem(string value, string text, IList<SelectListItem> list)
        {
            SelectListItem li = new SelectListItem();
            li.Value = value;
            li.Text = text;
            list.Add(li);
        }
        public static string getValue(string text, IList<SelectListItem> list)
        {
            string strKey = "";
           // SelectListItem li = list.Select(   (x => new SelectListItem
           // {
           //     Text = text.ToString()                
           //});
            if (list.Count > 0)
            {
                for (int i=0;i<list.Count;i++)
                {
                    if (list[i].Text == text)
                    {
                        strKey = list[i].Value;
                        break;
                    }
                }
            }
            return strKey;
        }
        
    }
}
