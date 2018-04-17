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
    public class NHRSConstructionMaterialService
    {
        //Get the MaximumaValue for code 
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select nvl(max(to_number(CONSTRUCTION_MAT_CD)),0)+1 from NHRS_CONSTRUCTION_MATERIAL";
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
        public bool ManageNHRSConstructionMaterial(NHRSConstructionMaterialModelClass objconstructionMaterial, out string exc)
        {
            NHRSConstructionMaterialEntityClass obj = new NHRSConstructionMaterialEntityClass();
            exc = "";
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {


                service.PackageName = "PKG_NHRS_INSPECTION";
                obj.Constructionmaterialcd = objconstructionMaterial.CONSTRUCTION_MAT_CD;
                obj.DefinedCd = objconstructionMaterial.DEFINED_CD.ToDecimal();
                obj.Mode = objconstructionMaterial.MODE;
                if (objconstructionMaterial.MODE == "I" || objconstructionMaterial.MODE == "U")
                {
                    obj.NameEng = Utils.ConvertToString(objconstructionMaterial.NAME_ENG);
                    obj.NameLoc = Utils.ConvertToString(objconstructionMaterial.NAME_LOC);
                    obj.DescEng = Utils.ConvertToString(objconstructionMaterial.DESCRIPTION_ENG);
                    obj.DescLoc = Utils.ConvertToString(objconstructionMaterial.DESCRIPTION_LOC);
                    obj.ModelType = Utils.ConvertToString(objconstructionMaterial.MODEL_TYPE);
                    obj.Status = Utils.ConvertToString(objconstructionMaterial.STATUS);
                }
                obj.Approved = objconstructionMaterial.APPROVED.ConvertBoolean();
                obj.ApprovedBy = SessionCheck.getSessionUsername();
                //obj.ApprovedDt = objconstructionMaterial.APPROVED_DT;
                obj.ApprovedDt = System.DateTime.Now;
                obj.EnteredBy = SessionCheck.getSessionUsername();
                //obj.EnteredDt = objconstructionMaterial.ENTERED_DT;
                obj.EnteredDt = System.DateTime.Now;
                obj.IPAddress = CommonVariables.IPAddress;


                obj.ApprovedDtLoc = System.DateTime.Now.ConvertToString();
                obj.EnteredDtLoc = System.DateTime.Now.ConvertToString();
                obj.UpdatedBy = SessionCheck.getSessionUsername();
                obj.UpdatedDate = objconstructionMaterial.ENTERED_DT;
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
                cmdText = "select CONSTRUCTION_MAT_CD,DEFINED_CD," + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC") + " AS NAMEENG," + "  APPROVED from NHRS_CONSTRUCTION_MATERIAL";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(" + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC") + ") like '{0}%'", initialStr);
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
        public NHRSConstructionMaterialModelClass FillNHRSConstructionMaterial(string ConstructionMaterialCode)
        {
            NHRSConstructionMaterialModelClass obj = new NHRSConstructionMaterialModelClass();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select CONSTRUCTION_MAT_CD,DEFINED_CD,NAME_ENG,NAME_LOC,DESCRIPTION_ENG,DESCRIPTION_LOC from NHRS_CONSTRUCTION_MATERIAL where CONSTRUCTION_MAT_CD = '" + ConstructionMaterialCode + "'";
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
                        obj.CONSTRUCTION_MAT_CD = Convert.ToInt32(dt.Rows[0]["CONSTRUCTION_MAT_CD"]);
                        obj.DEFINED_CD = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.NAME_ENG = dt.Rows[0]["NAME_ENG"].ToString();
                        obj.NAME_LOC = dt.Rows[0]["NAME_LOC"].ToString();
                        obj.DESCRIPTION_ENG = dt.Rows[0]["DESCRIPTION_ENG"].ToString();
                        obj.DESCRIPTION_LOC = dt.Rows[0]["DESCRIPTION_LOC"].ToString();

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

        public bool CheckDuplicateDefinedCode(string DefinedCode, string ConstructionMaterialCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (ConstructionMaterialCode == string.Empty)
                {
                    cmdText = "select * from NHRS_CONSTRUCTION_MATERIAL where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from NHRS_CONSTRUCTION_MATERIAL where DEFINED_CD ='" + DefinedCode + "' and CONSTRUCTION_MAT_CD !='" + ConstructionMaterialCode + "'";
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
