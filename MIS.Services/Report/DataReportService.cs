using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Security;
using System.Data;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;
namespace MIS.Services.Report
{
    public class DataImportReportService
    { 
        public DataTable GetFileImportFilter(string districtcd)
        {
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmdText = "select TO_CHAR(BATCH_ID) BATCH_ID,TO_CHAR(HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT, TO_CHAR(HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(MEMBER_CNT) MEMBER_CNT,FILE_NAME, CASE WHEN ERROR_MSG IS NULL THEN 'COMPLETED' ELSE 'FAILED' END STATUS,IS_POSTED , MVM.DISTRICT_CD from MIGNHRS.MIG_BATCH_INFO mbi INNER JOIN MIS_VDC_MUNICIPALITY mvm ON mbi.FILE_NAME=mvm.DESC_ENG WHERE DISTRICT_CD='" + districtcd + "' order by BATCH_ID ";
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                }
                catch (Exception)
                {
                    dtbl = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();

                    }
                }
                return dtbl;
            }
        }

        public DataTable GetDataImportReportForDashboard(List<string> jsonFiles)
        {
            DataRow row;
            DataTable dtbl = new DataTable();
            string cmdTxt = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    //cmdTxt = "select TO_CHAR(BATCH_ID) BATCH_ID,TO_CHAR(HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT, TO_CHAR(HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(MEMBER_CNT) MEMBER_CNT,FILE_NAME, CASE WHEN ERROR_MSG IS NULL THEN 'COMPLETED' ELSE 'FAILED' END STATUS,IS_POSTED from MIGNHRS.MIG_BATCH_INFO order by BATCH_ID";
                    cmdTxt = "select TO_CHAR(BATCH_ID) BATCH_ID,TO_CHAR(HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT, TO_CHAR(HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(MEMBER_CNT) MEMBER_CNT,FILE_NAME, 'FAILED' AS STATUS,IS_POSTED from MIGNHRS.MIG_BATCH_INFO order by BATCH_ID";
                    //dtbl = service.GetDataTable(true, "PR_MEM_GENDERWISE_DASHBOARD", P_Session_ID, P_Zone, P_EnterBy, DBNull.Value);
                    dtbl = service.GetDataTable(cmdTxt, null);
                    List<string> result = new List<string>();
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        List<string> fileNamesInDB = dtbl.AsEnumerable().Select(r => r.Field<string>("FILE_NAME")).ToList();
                        result = jsonFiles.Except(fileNamesInDB).ToList();
                    }
                    else
                    {
                        dtbl = new DataTable();
                        result = jsonFiles;
                        dtbl.Columns.Add("BATCH_ID", typeof(String));
                        dtbl.Columns.Add("HOUSE_OWNER_CNT", typeof(String));
                        dtbl.Columns.Add("BUILDING_STRUCTURE_CNT", typeof(String));
                        dtbl.Columns.Add("HOUSEHOLD_CNT", typeof(String));
                        dtbl.Columns.Add("FILE_NAME", typeof(String));
                        dtbl.Columns.Add("STATUS", typeof(String));
                    }
                    foreach (var i in result)
                    {
                        row = dtbl.NewRow();
                        row["BATCH_ID"] = "";
                        row["HOUSE_OWNER_CNT"] = "";
                        row["BUILDING_STRUCTURE_CNT"] = "";
                        row["HOUSEHOLD_CNT"] = "";
                        row["FILE_NAME"] = i.ToString();
                        row["STATUS"] = "PENDING";
                        dtbl.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }
        public DataTable GetDataImportReportForDashboard1(List<string> jsonFiles)
        {
            DataRow row;
            DataTable dtbl = new DataTable();

            string cmdTxt = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    cmdTxt = "select TO_CHAR(BATCH_ID) BATCH_ID,TO_CHAR(HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT, TO_CHAR(HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(MEMBER_CNT) MEMBER_CNT,FILE_NAME, CASE WHEN ERROR_MSG IS NULL THEN 'COMPLETED' ELSE 'FAILED' END STATUS,IS_POSTED from MIGNHRS.MIG_BATCH_INFO order by BATCH_ID";
                    // dtbl = service.GetDataTable(true, "PR_MEM_GENDERWISE_DASHBOARD", P_Session_ID, P_Zone, P_EnterBy, DBNull.Value);
                    dtbl = service.GetDataTable(cmdTxt, null);
                    List<string> result = new List<string>();
                    List<string> result1 = new List<string>();
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtbl.Rows)
                        {
                            if(jsonFiles.Contains(dr["FILE_NAME"].ConvertToString()))
                            {
                                result1.Add(dr["FILE_NAME"].ConvertToString());

                            }
                        }
                        List<string> fileNamesInDB = dtbl.AsEnumerable().Select(r => r.Field<string>("FILE_NAME")).ToList();
                        result = jsonFiles.Except(fileNamesInDB).ToList();
                       // result1 = fileNamesInDB;
                    }
                    else
                    {
                        result = jsonFiles;

                    }

                    dtbl = new DataTable();
                    // result = jsonFiles;
                    dtbl.Columns.Add("BATCH_ID", typeof(String));
                    dtbl.Columns.Add("HOUSE_OWNER_CNT", typeof(String));
                    dtbl.Columns.Add("BUILDING_STRUCTURE_CNT", typeof(String));
                    dtbl.Columns.Add("HOUSEHOLD_CNT", typeof(String));
                    dtbl.Columns.Add("FILE_NAME", typeof(String));
                    dtbl.Columns.Add("STATUS", typeof(String));
                    foreach (var i in result1)
                    {
                        row = dtbl.NewRow();
                        row["BATCH_ID"] = "";
                        row["HOUSE_OWNER_CNT"] = "";
                        row["BUILDING_STRUCTURE_CNT"] = "";
                        row["HOUSEHOLD_CNT"] = "";
                        row["FILE_NAME"] = i.ToString();
                        row["STATUS"] = "Completed";
                        dtbl.Rows.Add(row);
                    }
                    foreach (var i in result)
                    {
                        row = dtbl.NewRow();
                        row["BATCH_ID"] = "";
                        row["HOUSE_OWNER_CNT"] = "";
                        row["BUILDING_STRUCTURE_CNT"] = "";
                        row["HOUSEHOLD_CNT"] = "";
                        row["FILE_NAME"] = i.ToString();
                        row["STATUS"] = "PENDING";
                        dtbl.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }
        public DataTable GetPostToMainData()
        {
            DataTable dtbl = new DataTable();
            string cmdTxt = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    cmdTxt = "select TO_CHAR(BATCH_ID) BATCH_ID,TO_CHAR(HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT, TO_CHAR(HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(MEMBER_CNT) MEMBER_CNT,FILE_NAME, CASE WHEN ERROR_MSG IS NULL THEN 'COMPLETED' ELSE 'FAILED' END STATUS,IS_POSTED from MIGNHRS.MIG_BATCH_INFO where IS_POSTED='Y' order by BATCH_ID ";
                    //dtbl = service.GetDataTable(true, "PR_MEM_GENDERWISE_DASHBOARD", P_Session_ID, P_Zone, P_EnterBy, DBNull.Value);
                    dtbl = service.GetDataTable(cmdTxt, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }
        public DataTable GetPostToMainDataByDistrict(string District)
        {
            DataTable dtbl = new DataTable();
            string cmdTxt = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    cmdTxt = "select Distinct TO_CHAR(BI.BATCH_ID) BATCH_ID,TO_CHAR(BI.HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BI.BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT,TO_CHAR(BI.HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(BI.MEMBER_CNT) MEMBER_CNT,BI.FILE_NAME, CASE WHEN BI.ERROR_MSG IS NULL THEN 'COMPLETED'ELSE 'FAILED' END STATUS,IS_POSTED from MIGNHRS.MIG_BATCH_INFO BI INNER JOIN NHRS_HOUSE_OWNER_MST HOM ON HOM.BATCH_ID=BI.BATCH_ID INNER JOIN NHRS.MIS_DISTRICT MD ON MD.DISTRICT_CD=HOM.DISTRICT_CD where MD.DESC_ENG='"+District+"' AND ((BI.IS_POSTED='Y')OR(BI.IS_POSTED='M')) order by BI.BATCH_ID";
                    //dtbl = service.GetDataTable(true, "PR_MEM_GENDERWISE_DASHBOARD", P_Session_ID, P_Zone, P_EnterBy, DBNull.Value);
                    dtbl = service.GetDataTable(cmdTxt, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }

        public void SaveMOUDData(NameValueCollection paramValues)
        {
            ServiceFactory service = new ServiceFactory();
           
            Object District = DBNull.Value;
            Object Total_identified_households = DBNull.Value;
            Object Beneficiaries_with_grant_agreement = DBNull.Value;
            Object Beneficiaries_who_receieved_first_installment = DBNull.Value;
            Object Beneficiaries_who_have_started_construction = DBNull.Value;
            //Object Forms_submitted_for_second_installment = DBNull.Value;
            Object Approved_for_second_installment = DBNull.Value;
            Object Not_approved_for_second_installment = DBNull.Value;
            Object Houses_Not_able_to_Verified_for_Second_installment = DBNull.Value;
            //Object Forms_submitted_for_third_installment = DBNull.Value;
            Object Approved_for_third_installment = DBNull.Value;
            Object Not_Approved_for_third_installment = DBNull.Value;
            Object Houses_Not_able_to_Verified_for_third_installment=DBNull.Value;
            Object Construction_completed = DBNull.Value;
            //Object Selfconstructed_houses = DBNull.Value;
            if (paramValues["District"].ConvertToString() != string.Empty)
                District = paramValues["District"].ConvertToString();

            if (paramValues["totalHH"].ConvertToString() != string.Empty)
                Total_identified_households = paramValues["totalHH"].ConvertToString();

            if (paramValues["nohhwithgrantagreement"].ConvertToString() != string.Empty)
                Beneficiaries_with_grant_agreement = paramValues["nohhwithgrantagreement"].ConvertToString();

            if (paramValues["nohhwithfirstinstallment"].ConvertToString() != string.Empty)
                Beneficiaries_who_receieved_first_installment = paramValues["nohhwithfirstinstallment"].ConvertToString();

            if (paramValues["sitelayout"].ConvertToString() != string.Empty)
                Beneficiaries_who_have_started_construction = paramValues["sitelayout"].ConvertToString();
         
           
            //if (paramValues["Forms_submitted_for_second_installment"].ConvertToString() != string.Empty)
            //    Forms_submitted_for_second_installment = paramValues["Forms_submitted_for_second_installment"].ConvertToString();

            if (paramValues["housesvarifiedforsecondinstallment"].ConvertToString() != string.Empty)
                Approved_for_second_installment = paramValues["housesvarifiedforsecondinstallment"].ConvertToString();

            if (paramValues["housesnotvarifiedforsecondinstallment"].ConvertToString() != string.Empty)
                Not_approved_for_second_installment = paramValues["housesnotvarifiedforsecondinstallment"].ConvertToString();


            if (paramValues["housesnotabletovarifiedforsecinstal"].ConvertToString() != string.Empty)
                Houses_Not_able_to_Verified_for_Second_installment = paramValues["housesnotabletovarifiedforsecinstal"].ConvertToString();

            //if (paramValues["Forms_submitted_for_third_installment"].ConvertToString() != string.Empty)
            //    Forms_submitted_for_third_installment = paramValues["Forms_submitted_for_third_installment"].ConvertToString();

            if (paramValues["housesvarifiedforthirdinstallment"].ConvertToString() != string.Empty)
                Approved_for_third_installment = paramValues["housesvarifiedforthirdinstallment"].ConvertToString();

            if (paramValues["housesnotvarifiedforthirdinstallment"].ConvertToString() != string.Empty)
                Not_Approved_for_third_installment = paramValues["housesnotvarifiedforthirdinstallment"].ConvertToString();

            if (paramValues["housesnotabletoverifiedforthirdinstal"].ConvertToString() != string.Empty)
                Houses_Not_able_to_Verified_for_third_installment = paramValues["housesnotabletoverifiedforthirdinstal"].ConvertToString();


            if (paramValues["Construction_completed"].ConvertToString() != string.Empty)
                Construction_completed = paramValues["Construction_completed"].ConvertToString();

            //if (paramValues["Selfconstructed_houses"].ConvertToString() != string.Empty)
            //    Selfconstructed_houses = paramValues["Selfconstructed_houses"].ConvertToString();
            try
            {
                service.Begin();
                service.SubmitChanges("PR_MOUD_DATA",
                    District,
                    District,
                    Total_identified_households,
                    Beneficiaries_with_grant_agreement,
                    Beneficiaries_who_receieved_first_installment,
                     Beneficiaries_who_have_started_construction,
                    Approved_for_second_installment,
                    Not_approved_for_second_installment,
                    Houses_Not_able_to_Verified_for_Second_installment,
                    Approved_for_third_installment,
                    Not_Approved_for_third_installment,
                    Houses_Not_able_to_Verified_for_third_installment,
                    Construction_completed,
                    DateTime.Now.ConvertToString(),
                    DateTime.Now.ConvertToString()
                    );
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
    }
}
