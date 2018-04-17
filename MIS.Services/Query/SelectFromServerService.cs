using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;
namespace MIS.Services.Query
{
    public class SelectFromServerService
    {
        public DataTable getQuerryResult(string Query)
        {
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {


                try
                {


                    string cmd = "Select " + Query.ConvertToString();
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        args = new { }
                    });


                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
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
            return dt;
        }
    }
}

