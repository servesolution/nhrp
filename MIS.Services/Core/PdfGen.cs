using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using ExceptionHandler;

namespace MIS.Services.Core
{
	public static class PdfGenerator
	{

        public static bool ConvertToPdf(string htmlFileName, string pdfFileName, string alignment = "Portrait")
        {
            try
            {
                string argHTMLFilename = htmlFileName, argPDFFilename = pdfFileName;
                string fName =HttpContext.Current.Request.PhysicalApplicationPath+"Files\\pdf\\wkhtmltopdf.exe" ;                          
                if (System.IO.File.Exists(pdfFileName))
                {
                    System.IO.File.Delete(pdfFileName);
                }
                if (!argHTMLFilename.StartsWith("\""))
                    argHTMLFilename = string.Concat("\"", argHTMLFilename);
                if (!argHTMLFilename.EndsWith("\""))
                    argHTMLFilename = string.Concat(argHTMLFilename, "\"");

                if (!argPDFFilename.StartsWith("\""))
                    argPDFFilename = string.Concat("\"", argPDFFilename);
                if (!argPDFFilename.EndsWith("\""))
                    argPDFFilename = string.Concat(argPDFFilename, "\"");
                Process pi = new Process();
                pi.StartInfo.FileName = fName;
                pi.StartInfo.Arguments = "-O " + alignment + "  -q " + argHTMLFilename + " " + argPDFFilename;
                pi.Start();
                //  wait n milliseconds for exit (as after exit, it can't read the output)
                pi.WaitForExit(5000);
                // read the exit code, close process
                int returnCode = pi.ExitCode;               
                pi.Close();
                if (System.IO.File.Exists(htmlFileName))
                {
                    System.IO.File.Delete(htmlFileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return false;
            }
        }
	}
}