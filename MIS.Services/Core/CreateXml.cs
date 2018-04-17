using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace MIS.Services.Core
{
    public class CreateXml
    {
        public void createMSCombi2D(DataTable dtResult,string Caption, string xAxis, string yAxis)
        {

            XmlDocument docConfig = new XmlDocument();
            XmlNode xmlNode = docConfig.CreateNode(XmlNodeType.XmlDeclaration, "", "");

            string Header = "<chart caption='" + Caption + "' xAxisName='" + xAxis + "' yAxisName='" + yAxis + "' " +
                            "showValues='0' numberPrefix='$' decimalPrecision='0' bgcolor='F3f3f3' bgAlpha='70' " +
                            "showColumnShadow='1' divlinecolor='c5c5c5' divLineAlpha='60' showAlternateHGridColor='1' " +
                            "alternateHGridColor='f8f8f8' alternateHGridAlpha='60'></chart>";
            docConfig.LoadXml(Header);
            XmlElement rootElement = docConfig.DocumentElement;
         // XmlElement rootElement = docConfig.CreateElement("chart");            
            docConfig.AppendChild(rootElement);
            XmlElement hedder = docConfig.CreateElement("categories");
            docConfig.DocumentElement.PrependChild(hedder);
            docConfig.ChildNodes.Item(0).AppendChild(hedder);
            foreach (DataRow dr in dtResult.Rows)
            {
                string Categories = dr[0].ConvertToString();

                XmlElement hedderr = docConfig.CreateElement("category");
                hedder.PrependChild(hedderr);
               // docConfig.ChildNodes.Item(0).AppendChild(hedderr);
                // Create <installationid> Node
                //XmlElement installationElement = docConfig.CreateElement("ID");
                //XmlText installationIdText = docConfig.CreateTextNode(Convert.ToString(1));
                //installationElement.AppendChild(installationIdText);
                //hedder.PrependChild(installationElement);
                // Create <environment> Node
                // <category name='Jan' />
                XmlElement environmentElement = docConfig.CreateElement("NAME");
                XmlText environText = docConfig.CreateTextNode(Categories);
                environmentElement.AppendChild(environText);
                hedderr.PrependChild(environmentElement);
            }
            int dataSetCount = dtResult.Columns.Count - 1;
            for (int i = 0; i < dataSetCount; i++)
            {
               // XmlDocument docConfig1 = new XmlDocument();
                string HeadName = dtResult.Columns[i + 1].ConvertToString();
                string Header1 = "<dataset seriesName='" + HeadName + "' color='c4e3f7' ></dataset>";
               // docConfig1.LoadXml(Header1);
                docConfig.LoadXml(Header1);
               // XmlElement hedder1 = docConfig.DocumentElement.
                //docConfig.DocumentElement.PrependChild(hedder1);
                //docConfig.ChildNodes.Item(0).AppendChild(hedder1);
                foreach (DataRow dr in dtResult.Rows)
                {
                    XmlElement hedderr1 = docConfig.CreateElement("set");
                    //hedder1.PrependChild(hedderr1);
                    XmlElement environmentElement = docConfig.CreateElement("value");
                    XmlText environText = docConfig.CreateTextNode(dr[i+1].ConvertToString());
                    environmentElement.AppendChild(environText);
                    hedderr1.PrependChild(environmentElement);
                    //<set value='27400' />
                }
            }

            // Save xml document to the specified folder path.
            docConfig.Save("D:\\Sample.xml");
        }
    }
}
