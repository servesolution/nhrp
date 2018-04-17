using EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using ExceptionHandler;

namespace MIS.Services.Inspection
{
    public class InspectionConflictedFileService
    {

        //grt duplicate import
        public DataTable GetConflictedInspection(string dist, string vdc, string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT IDD.REASON,IDD.ENTERED_DATE,IDD.FILE_BATCH_ID,IDD.FILE_NAME  ,IDD.IMPORT_DATE,IDD.NRA_DEFINED_CD,IDD.FORM_NUMBER,IDD.BENEFICIARY_NAME,IDD.DISTRICT_CD,IDD.VDC_MUN_CD,IDD.WARD_CD,IDD.FILE_NAME,"
                + "DIS.DESC_ENG DISTRICT_ENG, DIS.DESC_LOC DISTRICT_LOC,   VDC.DESC_ENG VDC_MUNICIPALITY_ENG,  VDC.DESC_LOC VDC_MUNICIPALITY_LOC "
                + "FROM NHRS_INSPECTION_DUPLICATE_DATA IDD "
                + "LEFT OUTER JOIN MIS_DISTRICT DIS ON IDD.DISTRICT_CD = DIS.DISTRICT_CD "
                + "LEFT OUTER JOIN MIS_VDC_MUNICIPALITY VDC ON IDD.VDC_MUN_CD = VDC.VDC_MUN_CD WHERE 1=1";

                if (dist != null && dist != "")
                {
                    cmd = cmd + "AND IDD.DISTRICT_CD='" + dist.ConvertToString() + "'";
                }
                if (dist != null && dist != "")
                {
                    cmd = cmd + "AND IDD.VDC_MUN_CD='" + vdc.ConvertToString() + "'";
                }
                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND IDD.FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY IDD.IMPORT_DATE";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
                        {

                        }
                    });
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


        //get inspection application list
        public DataTable GetInspectionApplicationList(string something)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "select * from nhrs_inspection_application WHERE 1=1";
 
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
                        {

                        }
                    });
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
