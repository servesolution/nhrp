using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Xml;
using System.Data;
using System.IO;

namespace MIS.Services.Core
{

    public enum ChartType
    {
        Pie, Bar2D, Bar3D
    }


    public static class ExtensionMethods
    {
        public static List<string> ChartColors = new List<string>()
                                                    {
                                                        "FDC12E",
                                                        "56B9F9",
                                                        "A73F4B",
                                                        "FF9966",
                                                        "FFFF33",
                                                        "3300CC",
                                                        "33CC66",
                                                        "CC6600",
                                                        "666666",
                                                        "CC0033",  
                                                        "000000","000033","000066","000099","0000CC","0000FF","003300","003333","003366","003399","0033CC","0033FF","006600","006633","006666","006699","0066CC","0066FF","009900","009933","009966","009999","0099CC","0099FF","00CC00","00CC33","00CC66","00CC99","00CCCC","00CCFF","00FF00","00FF33","00FF66","00FF99","00FFCC","00FFFF","330000","330033","330066","330099","3300CC","3300FF","333300","333333","333366","333399","3333CC","3333FF","336600","336633","336666","336699","3366CC","3366FF","339900","339933","339966","339999","3399CC","3399FF","33CC00","33CC33","33CC66","33CC99","33CCCC","33CCFF","33FF00","33FF33","33FF66","33FF99","33FFCC","33FFFF","660000","660033","660066","660099","6600CC","6600FF","663300","663333","663366","663399","6633CC","6633FF","666600","666633","666666","666699","6666CC","6666FF","669900","669933","669966","669999","6699CC","6699FF","66CC00","66CC33","66CC66","66CC99","66CCCC","66CCFF","66FF00","66FF33","66FF66","66FF99","66FFCC","66FFFF","990000","990033","990066","990099","9900CC","9900FF","993300","993333","993366","993399","9933CC","9933FF","996600","996633","996666","996699","9966CC","9966FF","999900","999933","999966","999999","9999CC","9999FF","99CC00","99CC33","99CC66","99CC99","99CCCC","99CCFF","99FF00","99FF33","99FF66","99FF99","99FFCC","99FFFF","CC0000","CC0033","CC0066","CC0099","CC00CC","CC00FF","CC3300","CC3333","CC3366","CC3399","CC33CC","CC33FF","CC6600","CC6633","CC6666","CC6699","CC66CC","CC66FF","CC9900","CC9933","CC9966","CC9999","CC99CC","CC99FF","CCCC00","CCCC33","CCCC66","CCCC99","CCCCCC","CCCCFF","CCFF00","CCFF33","CCFF66","CCFF99","CCFFCC","CCFFFF","FF0000","FF0033","FF0066","FF0099","FF00CC","FF00FF","FF3300","FF3333","FF3366","FF3399","FF33CC","FF33FF","FF6600","FF6633","FF6666","FF6699","FF66CC","FF66FF","FF9900","FF9933","FF9966","FF9999","FF99CC","FF99FF","FFCC00","FFCC33","FFCC66","FFCC99","FFCCCC","FFCCFF","FFFF00","FFFF33","FFFF66","FFFF99","FFFFCC","FFFFFF"
                                                    };



        public static List<T> RemoveList<T>(this List<T> actualList, List<T> lstToRemove)
        {
            List<T> filteredList = actualList;
            foreach (T item in lstToRemove)
            {
                if (filteredList.Contains(item))
                    filteredList.Remove(item);
            }
            return filteredList;
        }


        public static string GetDisplayName(this PropertyInfo prop)
        {
            return prop.Name.ToUpper().Replace("_", " ");
        }

        public static List<string> TranslateListItems(this List<string> list)
        {
            List<string> translatedList = new List<string>();
            foreach (string s in list)
            {
                translatedList.Add(Utils.GetLabel(s));
            }
            return translatedList;
        }

        public static string GetBarChartXml(this DataTable dt, ChartType cType, string xLegend, string yLegend, string yMaxValue, string caption, string subCaption = "", string yPrefix = "", string decimalPrecision = "000")
        {
            //chartType = "../../FusionCharts/MSColumn3D.swf";
            string finalXml = "";

            string headerGraph = @"<graph xaxisname='" + xLegend + "' yaxisname='" + yLegend + "' rotateNames='1' hovercapbg='DEDEBE' hovercapborder='889E6D' yAxisMaxValue='" + yMaxValue + "' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='" + decimalPrecision + "' showAlternateHGridColor='1' AlternateHGridAlpha='30' AlternateHGridColor='CCCCCC' caption='" + caption + "' subcaption='" + subCaption + "'>";
            finalXml = headerGraph;
            var categories =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<category name='{0}' />", Utils.GetLabel(r[1].ToString().Replace("'", "`")));
            string categoryString = "<categories>" + String.Join("", categories.ToList()) + "</categories>";
            string datasetString = "";
            for (int c = 2; c < dt.Columns.Count - 2; c++)
            {
                datasetString += String.Format("<dataset seriesname='{0}' color='{1}'>", Utils.GetLabel(dt.Columns[c].ToString().Replace("'", "`")), ChartColors[c]);

                for (int cols = 0; cols < dt.Rows.Count; cols++)
                    datasetString += String.Format("<set value='{0}'/>", dt.Rows[cols][c].ToString().Replace("'", "`"));
                datasetString += "</dataset>";
            }
            finalXml += categoryString + datasetString + "</graph>";
            return finalXml;
        }

        public static string GetBarDataChartXml(this DataTable dt, ChartType cType, string xLegend, string yLegend, string yMaxValue, string caption, string subCaption = "", string yPrefix = "", string decimalPrecision = "000000")
        {
            //chartType = "../../FusionCharts/MSColumn3D.swf";
            string finalXml = "";
           
            //formatNumberScale='0' 
            string headerGraph = @"<graph xaxisname='" + xLegend + "' yaxisname='" + yLegend + "' rotateNames='1' baseFontSize='14'  hovercapbg='DEDEBE' hovercapborder='889E6D' yAxisMaxValue='" + yMaxValue + "' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='" + decimalPrecision + "' showAlternateHGridColor='1' AlternateHGridAlpha='30' AlternateHGridColor='CCCCCC' caption='" + caption + "' subcaption='" + subCaption + "'>";
            finalXml = headerGraph;
            var categories =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<category name='{0}' />", (r[0].ToString().Replace("'", "`")));
            string categoryString = "<categories>" + String.Join("", categories.ToList()) + "</categories>";
            string datasetString = "";
            for (int c = 1; c < dt.Columns.Count; c++)
            {
                datasetString += String.Format("<dataset seriesname='{0}' color='{1}'>", Utils.GetLabel(dt.Columns[c].ToString().Replace("'", "`").Trim()), ChartColors[c]);
                for (int cols = 0; cols < dt.Rows.Count; cols++)
                    datasetString += String.Format("<set value='{0}'/>", dt.Rows[cols][c].ToString().Replace("'", "`"));
                datasetString += "</dataset>";
            }
            finalXml += categoryString + datasetString + "</graph>";
            return finalXml;
        }

        public static string GetLineChartXml(this DataTable dt, ChartType cType, string xLegend, string yLegend, string yMaxValue, string caption, string subCaption , string yPrefix = "", string decimalPrecision = "000000")
        {
            //chartType = "../../FusionCharts/MSColumn3D.swf";
            string finalXml = "";

            //formatNumberScale='0' 
            string headerGraph = @"<graph xaxisname='" + xLegend + "' yaxisname='" + yLegend + "' rotateNames='1' baseFontSize='14'  hovercapbg='DEDEBE' showvalues='1' hovercapborder='889E6D' yAxisMaxValue='" + yMaxValue + "' numdivlines='4' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='" + decimalPrecision + "' showAlternateHGridColor='1' AlternateHGridAlpha='30' AlternateHGridColor='CCCCCC' caption='" + caption + "' subcaption='" + subCaption + "'  >";
            finalXml = headerGraph;
            var categories =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<category name='{0}' />", (r[0].ConvertToString()));
            //var categories = String.Format("<category name='MON'/>");
            //categories = categories+ String.Format("<category name='TUE'/>");
            //categories = categories + String.Format("<category name='WED'/>");
            //categories = categories + String.Format("<category name='THU'/>");
            //categories = categories + String.Format("<category name='FRI'/>");
            //categories = categories + String.Format("<categories name='SAT'/>");
            //categories = categories + string.Format("<categories name='SUN'/>");
            string categoryString = "<categories>" + String.Join("", categories.ToList()) + "</categories>";
            string datasetString = "";
            for (int c = 1; c < dt.Columns.Count; c++)
            {
                datasetString += String.Format("<dataset seriesname='{0}' color='{1}'>", Utils.GetLabel(dt.Columns[c].ToString().Replace("'", "`").Trim()), ChartColors[c]);
                for (int cols = 0; cols < dt.Rows.Count; cols++)
                    datasetString += String.Format("<set value='{0}'/>", dt.Rows[cols][c].ToString().Replace("'", "`"));
                datasetString += "</dataset>";
            }
            finalXml += categoryString + datasetString + "</graph>";
            return finalXml;
        }
        public static string GetBarDataChartXmlNew(this DataTable dt, ChartType cType, string xLegend, string yLegend, string yMaxValue, string caption, string subCaption = "", string yPrefix = "", string decimalPrecision = "000000")
        {
            //chartType = "../../FusionCharts/MSColumn3D.swf";
            string finalXml = "";

            string headerGraph = @"<graph xaxisname='" + xLegend + "' yaxisname='" + yLegend + "' rotateNames='1' hovercapbg='DEDEBE' hovercapborder='889E6D' yAxisMaxValue='" + yMaxValue + "' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='" + decimalPrecision + "' showAlternateHGridColor='1' AlternateHGridAlpha='30' AlternateHGridColor='CCCCCC' caption='" + caption + "' subcaption='" + subCaption + "'>";
            finalXml = headerGraph;
            var categories =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<category name='{0}' />", r[0].ToString().Replace("'", "`"));
            string categoryString = "<categories>" + String.Join("", categories.ToList()) + "</categories>";
            string datasetString = "";
            for (int c = 1; c < dt.Columns.Count; c++)
            {
                datasetString += String.Format("<dataset seriesname='{0}' color='{1}'>", Utils.GetLabel(dt.Columns[c].ToString().Replace("'", "`").Trim()), ChartColors[c]);

                for (int cols = 0; cols < dt.Rows.Count; cols++)
                    datasetString += String.Format("<set value='{0}'/>", dt.Rows[cols][c].ToString().Replace("'", "`"));
                datasetString += "</dataset>";
            }
            finalXml += categoryString + datasetString + "</graph>";
            return finalXml;
        }

        public static string GetPieChartXml(this Dictionary<string, string> dataDictionary, out string chartType, string decimalPrecision = "000")
        {
            chartType = "../../FusionCharts/Pie3D.swf";
            return "<graph shownames='1' decimalPrecision='" + decimalPrecision + "'>" +
                String.Join("", dataDictionary.Select((d) => String.Format("<set name='{0}' value='{1}' />", d.Key.Replace("'", "`"), d.Value.Replace("'", "`"))).ToArray<string>()) + "</graph>";
        }
        public static string GetPieChartXml1(this DataTable dtbl, string decimalPrecision = "000")
        {
            //chartType = "../../FusionCharts/Pie3D.swf";
            string graphHeader = string.Empty;
            string xml = string.Empty;
            string caption=Utils.GetLabel("Grade wise Damage Assessment");
            if (dtbl != null)
            {
                string dataset = string.Empty;//string.Format("<dataset color='{0}'>", "538dd5");
                int i = 1;
                foreach (DataRow drow in dtbl.Rows)
                {

                    dataset += string.Format("\n <set name='{0}' value='{1}' color='{2}'/>", drow[0].ConvertToString(),
                        drow[1].ConvertToString(), i == 1 ? "#4BACC6" : i == 2 ? "#8064A2" : i == 3 ? "#9BBB59" : i == 4 ? "#4F81BD" : i == 5 ? "#96114D" : i == 6 ? "CCCC00" : i == 7 ? "#ABD0BC" : i == 8 ? "#4F81BD" : "#9BBB59");
                    i += 1;
                }
                //  graphHeader = " <graph shownames='1' decimalPrecision='000' bgColor='dde3d5' caption=''>";
                graphHeader = " <graph shownames='1'  formatNumber='0' showpercentvalues='1' showpercentintooltip='0' formatNumberScale='0' showlegend='1' sFormatNumber='0' valueBgColor='#A73F4B' decimalPrecision='000' thousandSeparator='.' forceDecimals='1' decimals='2'  decimalSeparator=',' sFormatNumberScale='0' bgColor='dde3d5'caption='" + caption + "' baseFontSize='16'>";
                xml = string.Format("{0}\n{1}\n{2}", graphHeader, dataset, "</graph>");

            }
            return xml;
        }

        public static string GetMultiLineChartXml(this DataTable dt,string chartCaption,string chartSubCaption,string xLegendTitle,string showValues)
        {
            string finalXml = "";

            //finalXml = @"<chart palette='2' caption='" + chartCaption + "' subcaption='" + chartSubCaption + "' xaxisname='" + xLegendTitle + "' showvalues='0' divlinealpha='100' numvdivlines='4' vdivlinealpha='0' showalternatevgridcolor='1' alternatevgridalpha='5' canvaspadding='0' labeldisplay='" + xLegendTitle + "' showborder='0'>";

            finalXml=@"<chart caption='" + chartCaption + "' subCaption='" + chartSubCaption + "' xAxisName='" + xLegendTitle + "' yAxisName='No. of Beneficiaries'"+
                " paletteColors='#008ee4,#6baa01,#e44a00' bgAlpha = '0' borderAlpha ='20' canvasBorderAlpha = '0' LegendShadow = '0' legendBorderAlpha ='0'"+                
                " showXAxisLine= '1' showValues = '0' showBorder = '0' showAlternateHgridColor ='0' base = '10' axisLineAlpha ='10' divLineAlpha ='10' toolTipColor = '#ffffff'"+
                " toolTipBorderThickness = '0' toolTipBgColor ='#000000' toolTipBgAlpha ='80' toolTipBorderRadius ='2' toolTipPadding ='5' "+
                " formatNumberScale= '0'"+
            //Defining custom decimal separator
            " decimalSeparator=','"+
            " yaxismaxvalue='799000'>";
            finalXml=finalXml;

            var categories =
                from r in dt.Rows.OfType<DataRow>()
                //select String.Format("<category label='{0}' {1} {2} />", (r[1].ConvertToString()), "stepskipped='false'", "appliedsmartlabel='true'");   
                select String.Format("<category label='{0}'/>", (r[0].ConvertToString()));   
      
            string categoryString = "<categories>" + String.Join("", categories.Distinct().ToList()) + "</categories>";
            finalXml=finalXml+categoryString;

            //finalXml = finalXml + @"<axis title='1st Tranch' titlepos='left'  tickwidth='10' divlineisdashed='1' formatnumberscale='1' numberscalevalue='1' numdivlines='5'>";
                  var datasets1 =
                from r in dt.Rows.OfType<DataRow>()
                //select String.Format("<set value='{0}' />", (r[2].ConvertToString()));
                select String.Format("<data value='{0}' />", (r[1].ConvertToString()));
                  string dataset1String = "<dataset seriesname='Installment 1st Tranch'>" + String.Join("", datasets1.ToList()) + "</dataset>";
                  finalXml = finalXml + dataset1String;
                //+"</axis>";

          //  finalXml = finalXml + @"<axis title='2st tranch' titlepos='left'  tickwidth='10' divlineisdashed='1' formatnumberscale='1' numberscalevalue='1'   numdivlines='5'>";
                  var datasets2 =
                from r in dt.Rows.OfType<DataRow>()
                //select String.Format("<set value='{0}' />", (r[3].ConvertToString()));
                select String.Format("<data value='{0}' />", (r[2].ConvertToString()));
                  string dataset2String = "<dataset seriesname='Installment 2nd Tranch'>" + String.Join("", datasets2.ToList()) + "</dataset>";
                  finalXml = finalXml + dataset2String;
                //+"</axis>";

            //finalXml = finalXml + @"<axis title='3st tranch' axisonleft='0'  titlepos='RIGHT'  tickwidth='10' divlineisdashed='1' formatnumberscale='1' numberscalevalue='1'  numdivlines='5'>";
                  var datasets3 =
                from r in dt.Rows.OfType<DataRow>()
            //    select String.Format("<set value='{0}' />", (r[4].ConvertToString()));  
                select String.Format("<data value='{0}' />", (r[3].ConvertToString()));  
            string dataset3String = "<dataset seriesname='Installment 3rd Tranch'>" + String.Join("", datasets3.ToList()) + "</dataset>";
            finalXml = finalXml + dataset3String;
                //+"</axis>";
             finalXml=finalXml+"</chart>";
            return finalXml;
        }
        public static string GetBarChartXml(this DataTable dt,string chartCaption,string chartSubCaption,string xLegendTitle,string yLegendTitle)
        {

             string finalXml = "";
            finalXml=@"<chart caption='"+chartCaption+"' subcaption='"+chartSubCaption+"' xaxisname='"+xLegendTitle+"' yaxisname='"+yLegendTitle+"' theme='fint'>";
          
            var setValue =
            from r in dt.Rows.OfType<DataRow>()
            select String.Format("<set label='{0}' value='{1}' />", (r[0].ConvertToString()), (r[2].ConvertToString()));

            finalXml = finalXml + String.Join("", setValue.ToList()) + "</chart>";
                return finalXml;
        }
        public static string GetMultiSeriesBarChartXml(this DataTable dt, string chartCaption, string chartSubCaption, string xLegendTitle, string yLegendTitle)
        {

            string finalXml = "";
            finalXml = @"<chart caption='" + chartCaption + "' xAxisname='" + xLegendTitle + "' yAxisName='"+ yLegendTitle +"'" +            
            " theme='fint'"+            
            " subcaption = '"+chartSubCaption+"'"+            
            //" numberPrefix='Rs. '"+
            " rotateValues='0'"+
            " placeValuesInside='0'"+
            " valueFontColor='#000000'"+
            " valueBgColor='#FFFFFF'"+
            " valueBgAlpha= '80'"+
            //Disabling number scale compression
            " formatNumberScale= '0'"+
            //Defining custom decimal separator
            " decimalSeparator=','"+
            " yaxismaxvalue='799000'>";


            var categories =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<category label='{0}'/>", (r[0].ConvertToString()));

            string categoryString = "<categories>" + String.Join("", categories.Distinct().ToList()) + "</categories>";
            finalXml = finalXml + categoryString;

            var datasets1 =
               from r in dt.Rows.OfType<DataRow>()
               select String.Format("<set value='{0}' />", (r[1].ConvertToString()));
            string dataset1String = "<dataset seriesname='1st Installment'>" + String.Join("", datasets1.ToList()) + "</dataset>";
            finalXml = finalXml + dataset1String;
            var datasets2 =
               from r in dt.Rows.OfType<DataRow>()
               select String.Format("<set value='{0}' />", (r[2].ConvertToString()));
            string dataset2String = "<dataset seriesname='2nd Installment'>" + String.Join("", datasets2.ToList()) + "</dataset>";
            finalXml = finalXml + dataset2String;

            var datasets3 =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<set value='{0}' />", (r[3].ConvertToString()));
            string dataset3String = "<dataset seriesname='3rd Installment'>" + String.Join("", datasets3.ToList()) + "</dataset>";
            finalXml = finalXml + dataset3String ;
            finalXml = finalXml +  "</chart>";
            return finalXml;
        }
        public static string GetMultiSeriasChartXml(this DataTable dt, string chartCaption, string chartSubCaption, string xLegendTitle, string yLegendTitle)
        {

            string finalXml = "";
            finalXml = @"<chart caption='" + chartCaption + "' subcaption='" + chartSubCaption + "' xaxisname='" + xLegendTitle + "' yaxisname='" + yLegendTitle + "' numberprefix='Rs' palettecolors='#0075c2,#1aaf5d,#f2c500' bgcolor='#ffffff' showborder='0' showcanvasborder='0' useplotgradientcolor='0' plotborderalpha='10' legendborderalpha='0' legendbgalpha='0' legendshadow='0' showhovereffect='1' valuefontcolor='#ffffff' rotatevalues='1' placevaluesinside='1' divlinecolor='#999999' divlinedashed='1' divlinedashlen='1' canvasbgcolor='#ffffff' captionfontsize='14' subcaptionfontsize='14' subcaptionfontbold='0'> ";
             var categories =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<category label='{0}'/>", (r[0].ConvertToString()));   
      
            string categoryString = "<categories>" + String.Join("", categories.Distinct().ToList()) + "</categories>";
            finalXml=finalXml+categoryString;


             var datasets1 =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<set value='{0}' />", (r[1].ConvertToString()));
                  string dataset1String = "<dataset seriesname='Installment 1st Tranch'>" + String.Join("", datasets1.ToList()) + "</dataset>";
            finalXml=finalXml+dataset1String;
             var datasets2 =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<set value='{0}' />", (r[2].ConvertToString()));
                  string dataset2String = "<dataset seriesname='Installment 1st Tranch'>" + String.Join("", datasets2.ToList()) + "</dataset>";
            finalXml=finalXml+dataset2String;

            var datasets3 =
                from r in dt.Rows.OfType<DataRow>()
                select String.Format("<set value='{0}' />", (r[3].ConvertToString()));
                  string dataset3String = "<dataset seriesname='Installment 1st Tranch'>" + String.Join("", datasets3.ToList()) + "</dataset>";
            finalXml=finalXml+dataset3String+ "</chart>";           
            return finalXml;
        }
    }
}