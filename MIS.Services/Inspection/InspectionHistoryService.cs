using EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using ExceptionHandler;


namespace MIS.Services.Inspection
{
    public class InspectionHistoryService
    {

        public DataTable getHistory(string district_Cd,string  vdc_mun_cd,string  ward_no,string  InspLevel,string  Id)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory(); 
            string cmd = "";
            DataTable dt = new DataTable();

            try
            {
                cmd = "select *   FROM NHRS_INSPECTION_UPDATE_HISTORY WHERE 1= 1 ";
                if(district_Cd!="" && district_Cd != null)
                {
                   cmd= cmd  +  "and (DEG_DISTRICT_CD ='"+district_Cd+"' or PPR_DISTRICT_CD='"+district_Cd+"')";
                }

                if(vdc_mun_cd!="" && vdc_mun_cd != null)
                {
                   cmd= cmd  +  "and (DEG_VDC_MUN_CD ='"+vdc_mun_cd+"' or PPR_VDC_MUN_CD='"+vdc_mun_cd+"')";
                }

                 if(ward_no!="" && ward_no != null)
                {
                   cmd= cmd  +  "and (DEG_WARD ='"+ward_no+"' or PPR_WARD_CD='"+ward_no+"')";
                }

                 if (InspLevel != "" && InspLevel != null)
                 {
                     if (InspLevel=="1")
                     {
                         cmd = cmd + "and (PPR_INSPECTION_LEVEL ='" + InspLevel + "' or MST_INSPECTION_LEVEL1='" + InspLevel + "')";
                     }
                     if (InspLevel == "2")
                     {
                         cmd = cmd + "and (PPR_INSPECTION_LEVEL ='" + InspLevel + "' or MST_INSPECTION_LEVEL2='" + InspLevel + "')";
                     }
                     if (InspLevel == "3")
                     {
                         cmd = cmd + "and (PPR_INSPECTION_LEVEL ='" + InspLevel + "' or MST_INSPECTION_LEVEL3='" + InspLevel + "')";
                     }
                     
                 }
                 if (Id != "" && Id != null)
                 {
                     cmd = cmd + "and (MST_NRA_DEFINED_CD ='" + Id + "' or DEG_NRA_DEFINED_CD='" + Id + "' or PPR_NRA_DEFINED_CD='" + Id + "')";
                 }
                  ;
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });


                dt = sf.GetDataTable(true, "GET_INSPECTION_DATA",
                                                   district_Cd,
                                                   vdc_mun_cd,
                                                   ward_no,
                                                   InspLevel,
                                                   Id, 
                                                   DBNull.Value );

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

                foreach(DataRow dr in dt.Rows)
                {
                    dtbl.ImportRow(dr);
                }

                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }

        public DataTable getUpdatedata(string cmd, string district_Cd, string vdc_mun_cd, string ward_no, string InspLevel, string Id)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
               

            try
            {
                
                cmd = cmd+ "  FROM NHRS_INSPECTION_UPDATE_HISTORY WHERE 1= 1 ";
                if (district_Cd != "" && district_Cd != null)
                {
                    cmd = cmd + "and (DEG_DISTRICT_CD ='" + district_Cd + "' or PPR_DISTRICT_CD='" + district_Cd + "')";
                }

                if (vdc_mun_cd != "" && vdc_mun_cd != null)
                {
                    cmd = cmd + "and (DEG_VDC_MUN_CD ='" + vdc_mun_cd + "' or PPR_VDC_MUN_CD='" + vdc_mun_cd + "')";
                }

                if (ward_no != "" && ward_no != null)
                {
                    cmd = cmd + "and (DEG_WARD ='" + ward_no + "' or PPR_WARD_CD='" + ward_no + "')";
                }

                if (InspLevel != "" && InspLevel != null)
                {
                    if (InspLevel == "1")
                    {
                        cmd = cmd + "and (PPR_INSPECTION_LEVEL ='" + InspLevel + "' or MST_INSPECTION_LEVEL1='" + InspLevel + "')";
                    }
                    if (InspLevel == "2")
                    {
                        cmd = cmd + "and (PPR_INSPECTION_LEVEL ='" + InspLevel + "' or MST_INSPECTION_LEVEL2='" + InspLevel + "')";
                    }
                    if (InspLevel == "3")
                    {
                        cmd = cmd + "and (PPR_INSPECTION_LEVEL ='" + InspLevel + "' or MST_INSPECTION_LEVEL3='" + InspLevel + "')";
                    }
                     
                }
                if (Id != "" && Id != null)
                {
                    cmd = cmd + "and (MST_NRA_DEFINED_CD ='" + Id + "' or DEG_NRA_DEFINED_CD='" + Id + "' or PPR_NRA_DEFINED_CD='" + Id + "')";
                }
                cmd = cmd + " ORDER BY INSPECTION_HISTORY_ID";
                sf.Begin();
                dtbl = sf.GetDataTable(new
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
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }
    }
}





                   
