using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Setup;
using System.Data;
using System.Web;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;

namespace MIS.Services.Setup
{
    public class EducationService
    {
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmdText = "select nvl(max(to_number(class_type_cd)),0)+1 from mis_class_type";
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
                if (dt != null && dt.Rows.Count>0)
                {
                    result = dt.Rows[0][0].ToString();
                }
            }
            return result;
        }

        public void ManageEducation(MISEducation Education,out string exc)
        {
            MisEducationInfo obj = new MisEducationInfo();
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                if (Education.Mode == "D")
                {
                    obj.EducationCd = Education.EducationCd;                  
                    obj.Mode = Education.Mode;
                    obj.IPAddress = CommonVariables.IPAddress;
                }
                else if (Education.Mode == "A")
                {
                    obj.EducationCd = Education.EducationCd;
                    obj.Approved = Education.Approved;
                    obj.ApprovedBy = Education.ApprovedBy;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.Mode = Education.Mode;
                }
                else
                {
                    obj.EducationCd = Education.EducationCd;
                    obj.DefinedCd = Utils.ConvertToString(Education.DefinedCd);
                    obj.DescEng = Utils.ConvertToString(Education.DescEng.ToUpper());
                    obj.DescLoc = Utils.ConvertToString(Education.DescLoc);
                    if (Education.ShortName == null)
                    {
                        obj.ShortName = Education.ShortName;
                    }
                    else
                    {
                        obj.ShortName = Education.ShortName.ToUpper();
                    }
                    obj.ShortNameLoc = Utils.ConvertToString(Education.ShortNameLoc);
                    obj.Approved = Education.Approved;
                    obj.Disabled = Education.Disabled;
                    obj.ApprovedBy = Education.ApprovedBy;
                    obj.ApprovedDt = Education.ApprovedDt.ToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = Education.ApprovedDtLoc;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.EnteredDt = Education.EnteredDt.ToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = Education.EnteredDtLoc;
                    obj.IPAddress = CommonVariables.IPAddress;
                    obj.Mode = Education.Mode;
                }
                service.PackageName = "PKG_SETUP";
                
                try
                {
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
            }
        }

        public DataTable Education_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            using (ServiceFactory service = new ServiceFactory())
            {
               
                if (sessionLanguage == "English")
                {
                    cmdText = "select class_type_cd,DEFINED_CD,DESC_ENG,SHORT_NAME,ORDER_NO,DISABLED,APPROVED,APPROVED_BY,APPROVED_DT,ENTERED_BY,ENTERED_DT from mis_class_type";
                }
                else
                {
                    cmdText = "select class_type_cd,DEFINED_CD,DESC_LOC DESC_ENG,SHORT_NAME_LOC SHORT_NAME,ORDER_NO,DISABLED,APPROVED,APPROVED_BY,APPROVED_DT,ENTERED_BY,ENTERED_DT from mis_class_type";
                }
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    if (sessionLanguage == "English")
                    {
                        cmdText += String.Format(" where Upper(DESC_ENG) like '{0}%'", initialStr);
                    }
                    else
                    {
                        cmdText += String.Format(" where Upper(DESC_LOC) like '{0}%'", initialStr);
                    }
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "DEFINED_CD")
                    {
                        //cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
                        cmdText += String.Format(" order by {0} {1}", orderby, order);
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }

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
            }
            return dtbl;
        }

        public bool CheckDuplicateDefinedCode(string DefinedCd, string code)
        {
            string cmdText = string.Empty;
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
               
                if (code == string.Empty)
                {
                    cmdText = "select * from mis_class_type where DEFINED_CD ='" + DefinedCd + "'";
                }
                else
                {
                    cmdText = "select * from mis_class_type where DEFINED_CD ='" + DefinedCd + "' and class_type_cd !='" + code + "'";
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

        public MISEducation FillEducation(string EducationCd)
        {
            MISEducation obj = new MISEducation();           
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                string CmdText = "select class_type_cd, DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC,DEFINED_CD from mis_class_type where class_type_cd = '" + EducationCd + "'";
                try
                {
                    service.Begin();
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
                    obj.EducationCd = Convert.ToInt32(dt.Rows[0]["CLASS_TYPE_CD"]);
                    obj.DescEng = dt.Rows[0]["DESC_ENG"].ToString();
                    obj.DescLoc = dt.Rows[0]["DESC_LOC"].ToString();
                    obj.ShortName = dt.Rows[0]["SHORT_NAME"].ToString();
                    obj.ShortNameLoc = dt.Rows[0]["SHORT_NAME_LOC"].ToString();
                    obj.DefinedCd = dt.Rows[0]["DEFINED_CD"].ToString();
                    obj.Mode = "U";
                }

            }
            return obj;
        }
    }
}
