using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Models.CaseGrievance;
using System.Data.OracleClient;
using MIS.Models.Entity;
using MIS.Services.Core;

namespace MIS.Services.CaseGrievance
{
    public class CaseGrievanceHandlingService
    {
        public DataTable HouseOwnerDetail(string a)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT DISTINCT FIRST_NAME_ENG,MIDDLE_NAME_ENG,LAST_NAME_ENG,FULL_NAME_ENG,FIRST_NAME_LOC,MIDDLE_NAME_LOC,LAST_NAME_LOC,FULL_NAME_LOC,GENDER_ENG,GENDER_LOC,DISTRICT_ENG,DISTRICT_LOC,VDC_LOC,VDC_ENG,WARD_NO,AREA_LOC,AREA_ENG,HOUSE_OWNER_ID,INSTANCE_UNIQUE_SNO,HOUSE_SNO,NRA_DEFINED_CD,SNO FROM VW_HOUSE_OWNER_DTL WHERE HOUSE_OWNER_ID='" + a + "' order by SNO";
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
            }
            return dt;
        }

        public DataTable GetGrievanceHandledData(string a)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * from nhrs_grievance_handled where HOUSE_OWNER_ID='" + a + "' order by case_registration_id,building_structure_no";
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
            }
            return dt;
        }
        public DataTable GetGrievanceOwnerName(string CaseRegistrationId)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT DISTINCT HOUSE_OWNER_NAME_ENG from nhrs_grievance_handled where CASE_REGISTRATION_ID='" + CaseRegistrationId + "'";
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
            }
            return dt;
        }

        //Other house Detail
        public DataTable getOthHouseDetail(string a)
        {
            DataTable dt = null;
            string Id = a;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_SEARCH";
                    service.Begin();
                    dt = service.GetDataTable(true, "PR_GET_GRIEVANCE_OTH_DETAIL",
                        Id,
                        DBNull.Value
                        );
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
                return dt;
            }
        }
        public void svaeGrievance(CaseGrievanceHandled obj)
        {
            CaseGrievanceHandling objentity = new CaseGrievanceHandling();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    objentity.Mode = obj.Mode;
                    objentity.GrievanceId = null;
                    objentity.HouseOwnerId = obj.houseOwnerId.ToString();
                    objentity.HouseOWnerNameEng = obj.HouseOwnerName.ConvertToString();
                    objentity.BuildingStructureNo = obj.houseStructureNo;
                    objentity.CaseRegistrationId = obj.caseRegistrationId.ToDecimal();
                    objentity.SurveyedDamageGradeCd = obj.SurveyDamageGradeCd.ToDecimal();
                    objentity.MatrixGradeCode = obj.MATRIX_GRADE_CD.ToDecimal();
                    objentity.OfficerGradeCd = obj.gradeAfterGrievance.ToDecimal();
                    objentity.PhotoGradeCd = obj.PHOTO_GRADE_CD.ToDecimal();
                    objentity.SurveytechSolution = obj.SurveyTechnicalSolution.ConvertToString();
                    objentity.OfficerstechSolution = obj.OfficersTechnicalSolution.ConvertToString();
                    objentity.GrievanceOfficerCd = SessionCheck.getSessionUserCode().ConvertToString();
                    //objentity.GrievanceOfficerCd = 1;
                    objentity.OfficerRecommendation = obj.REMARKS;
                    objentity.RecommendationCode = obj.RecommendationCode;
                    objentity.ClarificationCode = obj.ClarificationCode;
                    objentity.Status = obj.FieldObservation;
                    objentity.Retargeted = "N";
                    objentity.Remarks = obj.REMARKS;
                    objentity.RemarksLoc = obj.REMARKS;
                    objentity.Active = "Y";
                    //string datetoPass = DateTime.Now.ConvertToString();
                    objentity.EnteredBy = SessionCheck.getSessionUsername();
                    objentity.EnteredDt = DateTime.Now.ToString();
                    objentity.LastUpdatedBy = SessionCheck.getSessionUsername();
                    objentity.LastUpdatedDt = DateTime.Now.ToString();
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objentity, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
        public void DeleteGrievanceHandled(string CaseRegistrationID)
        {
            CaseGrievanceHandling objentity = new CaseGrievanceHandling();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    objentity.Mode = "D";
                    objentity.CaseRegistrationId = CaseRegistrationID.ToDecimal();
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objentity, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
        public bool CheckDuplicateRegistrationID(string code)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select * from NHRS_GRIEVANCE_HANDLED  where CASE_REGISTRATION_ID ='" + code + "'";


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

        public string getRegistrationId(string InstanceUniqueSno)
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select CASE_REGISTRATION_ID from NHRS_GRIEVANCE_REGISTRATION WHERE BENEFICIARY_ID='" + InstanceUniqueSno + "'";
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

        public DataTable CheckNissaNo(string nissano, string district, string vdc, string ward)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select * from MIS_HOUSEHOLD_INFO  where 1=1";
                    if (nissano != "")
                    {
                        cmdText += " and upper(INSTANCE_UNIQUE_SNO)='" + nissano.ToUpper() + "'";
                    }
                    if (district != "")
                    {
                        cmdText += " and upper(CUR_DISTRICT_CD)='" + district.ToUpper() + "'";
                    }
                    if (vdc != "")
                    {
                        cmdText += " and upper(CUR_VDC_MUN_CD)='" + vdc.ToUpper() + "'";
                    }
                    if (ward != "")
                    {
                        cmdText += " and upper(CUR_WARD_NO)='" + ward.ToUpper() + "'";
                    }
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
                    return dtbl;
                }
                return dtbl;
            }
        }

        public DataTable CheckHouseHandled(string HouseOwnerID, string CaseRegistrationId)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select * from NHRS_GRIEVANCE_HANDLED  where HOUSE_OWNER_ID='" + HouseOwnerID + "' OR CASE_REGISTRATION_ID='" + CaseRegistrationId + "'";
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
                    return dtbl;
                }
                return dtbl;
            }
        }
    }
}
