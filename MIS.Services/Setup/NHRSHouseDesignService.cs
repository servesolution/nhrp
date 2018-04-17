using EntityFramework;
using ExceptionHandler;
using MIS.Models.Entity;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace MIS.Services.Setup
{
   public class NHRSHouseDesignService
   {
  //Get the MaximumaValue for code 
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select nvl(max(to_number(HOUSE_DESIGN_CD)),0)+1 from NHRS_HOUSE_DESIGN";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    dt = null;
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

            return result;
        }

        // Edit,Update,Delete and Approve the Record
        public bool ManageNHRSHouseDesign(NHRSHouseDesignModelClass objhouseDesign, out string exc)
        {
            NHRSHouseDesignEntityClass obj = new NHRSHouseDesignEntityClass();
            exc = "";
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {


                service.PackageName = "PKG_NHRS_INSPECTION";
                obj.Housedesigncd = objhouseDesign.HOUSE_DESIGN_CD;
                obj.DefinedCd = objhouseDesign.DEFINED_CD.ToDecimal();
                obj.Mode = objhouseDesign.MODE;
                if (objhouseDesign.MODE == "I" || objhouseDesign.MODE == "U")
                {
                    obj.DescEng = Utils.ConvertToString(objhouseDesign.DESC_ENG);
                    obj.DescLoc = Utils.ConvertToString(objhouseDesign.DESC_LOC);
                    obj.ModelType = Utils.ConvertToString(objhouseDesign.MODEL_TYPE);
                    obj.Status = Utils.ConvertToString(objhouseDesign.STATUS);
                }
                obj.Approved = objhouseDesign.APPROVED.ConvertBoolean();
                obj.ApprovedBy = SessionCheck.getSessionUsername();
                //obj.ApprovedDt = objhouseDesign.APPROVED_DT;
                obj.ApprovedDt = System.DateTime.Now;
                obj.EnteredBy = SessionCheck.getSessionUsername();
                //obj.EnteredDt = objhouseDesign.ENTERED_DT;
                obj.EnteredDt = System.DateTime.Now;
                obj.IPAddress = CommonVariables.IPAddress;


                obj.ApprovedDtLoc = System.DateTime.Now.ConvertToString();
                obj.EnteredDtLoc = System.DateTime.Now.ConvertToString();
                obj.UpdatedBy = SessionCheck.getSessionUsername();
                obj.UpdatedDate = objhouseDesign.ENTERED_DT;
                obj.UpdatedDateLoc = System.DateTime.Now.ConvertToString();
                obj.Active = "Y";

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    exc = oe.Code.ConvertToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    res = false;
                    exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qr != null)
                {
                    res = qr.IsSuccess;
                }
                return res;
            }

        }

        // Get All Table Criteria
        public DataTable GetAllNHRSHouseDesign(string initialStr)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select HOUSE_DESIGN_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS NAMEENG," + "  APPROVED from NHRS_HOUSE_DESIGN";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '{0}%'", initialStr);
                }
                cmdText += String.Format(" order by DEFINED_CD");
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
            return dtbl;
        }

        // Fill The Not Interviewing Reason For Edit 
        public NHRSHouseDesignModelClass FillNHRSHouseDesign(string HouseDesignCode)
        {
            NHRSHouseDesignModelClass obj = new NHRSHouseDesignModelClass();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select HOUSE_DESIGN_CD,DEFINED_CD,DESC_ENG,DESC_LOC from NHRS_HOUSE_DESIGN where HOUSE_DESIGN_CD = '" + HouseDesignCode + "'";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        obj = null;
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.HOUSE_DESIGN_CD = Convert.ToInt32(dt.Rows[0]["HOUSE_DESIGN_CD"]);
                        obj.DEFINED_CD = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.DESC_ENG = dt.Rows[0]["DESC_ENG"].ToString();
                        obj.DESC_LOC = dt.Rows[0]["DESC_LOC"].ToString();
                     
                    }
                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            return obj;
        }

        public bool CheckDuplicateDefinedCode(string DefinedCode, string HouseDesignCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (HouseDesignCode == string.Empty)
                {
                    cmdText = "select * from NHRS_HOUSE_DESIGN where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from NHRS_HOUSE_DESIGN where DEFINED_CD ='" + DefinedCode + "' and HOUSE_DESIGN_CD !='" + HouseDesignCode + "'";
                }
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
                if (dtbl.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
