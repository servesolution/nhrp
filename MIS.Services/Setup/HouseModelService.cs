using MIS.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using EntityFramework;
using System.Data;
using System.Web.WebPages.Html;
using MIS.Models.Core;
using System.Web;

namespace MIS.Services.Setup
{
    public class HouseModelService
    {
        public void saveHouseModel(HouseModel objHouseModel)
        {
            HouseModelInfo objHouseModelInfo = new HouseModelInfo();
            QueryResult qr = new QueryResult();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    objHouseModelInfo.Mode = objHouseModel.mode;
                    objHouseModelInfo.ModelId = objHouseModel.model_id;
                    if (objHouseModel.mode == "A")
                    {                        
                        objHouseModelInfo.Approved = objHouseModel.approved;
                        objHouseModelInfo.ApprovedBy = SessionCheck.getSessionUsername();
                        objHouseModelInfo.ApprovedDt = DateTime.Now;
                    }
                    else if (objHouseModel.mode == "D")
                    {
                        //mode and model_id already passed
                    }
                    else if (objHouseModel.mode == "I")
                    {
                        objHouseModelInfo.DefinedCd = objHouseModel.defined_cd;
                        objHouseModelInfo.NameEng = objHouseModel.name_eng;
                        objHouseModelInfo.NameLoc = objHouseModel.name_loc;
                        objHouseModelInfo.CodeEng = objHouseModel.code_loc;
                        objHouseModelInfo.CodeLoc = objHouseModel.code_loc;
                        objHouseModelInfo.DescEng = objHouseModel.desc_eng;
                        objHouseModelInfo.DescLoc = objHouseModel.desc_loc;
                        objHouseModelInfo.Approved = "N";
                        objHouseModelInfo.ApprovedBy = SessionCheck.getSessionUsername();
                        objHouseModelInfo.ApprovedDt = DateTime.Now;
                        objHouseModelInfo.approved_dt_loc = DateTime.Now.ConvertToString();
                        objHouseModelInfo.EnteredBy = SessionCheck.getSessionUsername();
                        objHouseModelInfo.EnteredDt = DateTime.Now;
                        objHouseModelInfo.entered_dt_loc = DateTime.Now.ConvertToString();
                        objHouseModelInfo.updated_by = SessionCheck.getSessionUsername();
                        objHouseModelInfo.updated_dt = DateTime.Now;
                        objHouseModelInfo.updated_dt_loc = DateTime.Now.ConvertToString();
                        objHouseModelInfo.IpAddress = CommonVariables.IPAddress;
                        objHouseModelInfo.design_image = objHouseModel.design_image;
                        objHouseModelInfo.Hierarchy_parent_id = objHouseModel.hierarchy_cd.ToDecimal();
                        objHouseModelInfo.Previous_design_id = objHouseModel.previous_design_cd.ToDecimal();
                        objHouseModelInfo.design_number = objHouseModel.design_number.ToDecimal();
                    }
                    else if (objHouseModel.mode == "U")
                    {
                        objHouseModelInfo.DefinedCd = objHouseModel.defined_cd;
                        objHouseModelInfo.NameEng = objHouseModel.name_eng;
                        objHouseModelInfo.NameLoc = objHouseModel.name_loc;
                        objHouseModelInfo.CodeEng = objHouseModel.code_eng;
                        objHouseModelInfo.CodeLoc = objHouseModel.code_loc;
                        objHouseModelInfo.DescEng = objHouseModel.desc_eng;
                        objHouseModelInfo.DescLoc = objHouseModel.desc_loc;
                        objHouseModelInfo.design_image = objHouseModel.design_image;
                        objHouseModelInfo.Hierarchy_parent_id = objHouseModel.hierarchy_cd.ToDecimal();
                        objHouseModelInfo.Previous_design_id = objHouseModel.previous_design_cd.ToDecimal();
                        objHouseModelInfo.design_number = objHouseModel.design_number.ToDecimal();
                        objHouseModelInfo.UpdatedBy = SessionCheck.getSessionUsername();
                        objHouseModelInfo.UpdatedDt = DateTime.Now;                        
                    }
                    sf.PackageName = "PKG_NHRS_INSPECTION";
                    sf.Begin();
                    qr = sf.SubmitChanges(objHouseModelInfo, true);
                }
                catch (OracleException oe)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
            }
        }

        public DataTable getHouseModelList()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    cmdText = "SELECT MODEL_ID, DEFINED_CD,DESIGN_NUMBER, " + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC") + " AS NAME, "
                            + Utils.ToggleLanguage("CODE_ENG", "CODE_LOC") + " AS CODE, "
                            + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCRIPTION, APPROVED FROM NHRS_HOUSE_MODEL ORDER BY NAME";
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
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
                    {
                        sf.End();
                    }
                }
            }
            return dt;
        }

        public HouseModel getHouseModelDataById(decimal? id)
        {
            HouseModel objHouseModel = new HouseModel();
            string cmd = "";
            DataTable dt = new DataTable();            
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    cmd = "SELECT MODEL_ID, DEFINED_CD,DESIGN_NUMBER, NAME_ENG, NAME_LOC, CODE_ENG, CODE_LOC, DESC_ENG, DESC_LOC,IMAGE_NAME,HIERARCHY_PARENT_ID,PREVIOUS_DESIGN_CD "
                            + " FROM NHRS_HOUSE_MODEL WHERE MODEL_ID = " + id;
                    sf.Begin();
                    dt = sf.GetDataTable(new
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
                    objHouseModel = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    objHouseModel.model_id = dt.Rows[0]["MODEL_ID"].ToDecimal();
                    objHouseModel.defined_cd = dt.Rows[0]["DEFINED_CD"].ToString();
                    objHouseModel.name_eng = dt.Rows[0]["NAME_ENG"].ToString();
                    objHouseModel.name_loc = dt.Rows[0]["NAME_LOC"].ToString();
                    objHouseModel.code_eng = dt.Rows[0]["CODE_ENG"].ToString();
                    objHouseModel.code_loc = dt.Rows[0]["CODE_LOC"].ToString();
                    objHouseModel.desc_eng = dt.Rows[0]["DESC_ENG"].ToString();
                    objHouseModel.desc_loc = dt.Rows[0]["DESC_LOC"].ToString();
                    objHouseModel.design_image = dt.Rows[0]["IMAGE_NAME"].ToString();
                    objHouseModel.hierarchy_cd = dt.Rows[0]["HIERARCHY_PARENT_ID"].ToString();
                    objHouseModel.previous_design_cd = dt.Rows[0]["PREVIOUS_DESIGN_CD"].ToString();
                    objHouseModel.design_number = dt.Rows[0]["DESIGN_NUMBER"].ToString();
                    objHouseModel.mode = "U";
                }
            }
            return objHouseModel;
        }

       
       

      

        public List<MISCommon> GetTreeHead(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select INSPECTION_CODE_ID, DESC_ENG, DESC_LOC "
                    + "FROM NHRS_INSPECTION_DESC_DTL where UPPER_INSPECTION_CODE_ID IS NULL";

                    cmdText += " ORDER BY TO_NUMBER(INSPECTION_CODE_ID)";
                    try
                    {
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

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["INSPECTION_CODE_ID"].ConvertToString(), DescriptionLoc = drow["DESC_LOC"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public string GetDescriptionEngFromCode(string code, string tableName, string ColumanName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT DESC_ENG from " + tableName + " WHERE " + ColumanName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                        return (dt.Rows[0][0].ConvertToString());
                }
            }
            return "";
        }
        public string GetDescriptionLocFromCode(string code, string tableName, string ColumanName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT DESC_LOC from " + tableName + " WHERE " + ColumanName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                        return (dt.Rows[0][0].ConvertToString());
                }
            }
            return "";
        }
        public List<MISCommon> GetPreviousDesign(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select MODEL_ID,"+Utils.ToggleLanguage(" NAME_ENG ", "NAME_LOC ")
                    + "AS DESCRIPTION FROM NHRS_HOUSE_MODEL where CODE_LOC='"+1+"'";

                    cmdText += " ORDER BY TO_NUMBER(MODEL_ID)";
                    try
                    {
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

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["MODEL_ID"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetPreviousDesignTwo(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select MODEL_ID," + Utils.ToggleLanguage(" NAME_ENG ", "NAME_LOC ")
                    + "AS DESCRIPTION FROM NHRS_HOUSE_MODEL where CODE_LOC='" + 2 + "'";

                    cmdText += " ORDER BY TO_NUMBER(MODEL_ID)";
                    try
                    {
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

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["MODEL_ID"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

    }
}
