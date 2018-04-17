using System;
using System.Web.Mvc;
using System.IO;

namespace MIS.Services.Core
{    
    public class DownloadResult : ActionResult
    {

        /// <summary>
        /// Creates a new instance of the MIS.Controllers.DownloadResult class. 
        /// </summary>
        public DownloadResult()
        {
        }

        /// <summary>
        /// Creates a new instance of the MIS.Contollers.DownloadResult class with the specified file path. 
        /// </summary>
        /// <param name="FilePath">Path where the download is to be saved.</param>
        public DownloadResult(string FilePath)
        {
            this.FilePath = FilePath;
        }

        /// <summary>
        /// Creates a new instance of the MIS.Contollers.DownloadResult class with the specified memory stream. 
        /// </summary>
        public DownloadResult(MemoryStream downloadStream)
        {
            this.DownloadStream = downloadStream;
        }

        /// <summary>
        /// Gets or sets the file path. 
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the bool value that determines whether to keep file or not. 
        /// </summary>
        public bool KeepFile { get; set; }

        /// <summary>
        /// Gets or sets the download name of the file. 
        /// </summary>
        public string FileDownloadName { get; set; }

        /// <summary>
        /// Gets or sets the content type. 
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the download stream. 
        /// </summary>
        public MemoryStream DownloadStream { get; set; }

        /// <summary>
        /// Executes the result and performs the download task as per the information provided. It overrides the method ExecuteResult. 
        /// </summary>
        /// <param name="context">ControllerContext object</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (this.DownloadStream != null)
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.ClearHeaders();
                context.HttpContext.Response.Buffer = false;
                context.HttpContext.Response.ContentType = this.ContentType ?? "application/xls";
                context.HttpContext.Response.AppendHeader("content-disposition", string.Format("attachment;filename={0}", this.FileDownloadName));
                context.HttpContext.Response.BinaryWrite(this.DownloadStream.ToArray());
                this.DownloadStream.Close();
                this.DownloadStream.Dispose();
                this.DownloadStream = null;
                context.HttpContext.Response.Flush();
            }
            else
            {
                if (!String.IsNullOrEmpty(FileDownloadName))
                {
                    context.HttpContext.Response.ContentType = this.ContentType;
                    context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + this.FileDownloadName);
                }
                context.HttpContext.Response.TransmitFile(this.FilePath);
                context.HttpContext.Response.Flush();
                if (this.KeepFile == false)
                {
                    if (System.IO.File.Exists(this.FilePath))
                    {
                        System.IO.File.Delete(this.FilePath);
                    }
                }
            }
        }
    }
}
