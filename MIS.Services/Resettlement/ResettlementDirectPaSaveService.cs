using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using MIS.Models.ManageResettlement;

namespace MIS.Services.Resettlement
{
    public class ResettlementDirectPaSaveService
    {

        public bool saveResettlementWithPA(ResettlementModelClass objModel)
        {
            bool result = false;
            QueryResult qrResetlement = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                    service.Begin();


                    string respondantFullName = null;
                    string FathersFullName = null;
                    string GrandFatherFullName = null;
                    string BeneficiaryFullName = null;
                    if (objModel.ResMiddleName.ConvertToString().Trim(' ') == "")
                    {
                        respondantFullName = objModel.ResFirstName.ConvertToString().Trim() + " " + objModel.ResLastName.ConvertToString().Trim();
                    }
                    else
                    {
                        respondantFullName = objModel.ResFirstName.ConvertToString().Trim() + " " + objModel.ResMiddleName.ConvertToString().Trim() + " " + objModel.ResLastName.ConvertToString().Trim();
                    }


                    if (objModel.ResFathersMiddleName.ConvertToString().Trim(' ') == "")
                    {
                        FathersFullName = objModel.ResFathersFirstName.ConvertToString().Trim() + " " + objModel.ResFathersLastName.ConvertToString().Trim();
                    }
                    else
                    {
                        FathersFullName = objModel.ResFathersFirstName.ConvertToString().Trim() + " " + objModel.ResFathersMiddleName.ConvertToString().Trim() + " " + objModel.ResFathersLastName.ConvertToString().Trim();
                    }


                    if (objModel.ResFathersMiddleName.ConvertToString().Trim(' ') == "")
                    {
                        GrandFatherFullName = objModel.ResGFathersFirstName.ConvertToString().Trim() + " " + objModel.ResGFathersLastName.ConvertToString().Trim();
                    }
                    else
                    {
                        GrandFatherFullName = objModel.ResGFathersFirstName.ConvertToString().Trim() + " " + objModel.ResGFathersMiddleName.ConvertToString().Trim() + " " + objModel.ResGFathersLastName.ConvertToString().Trim();
                    }

                    if (objModel.ResFathersMiddleName.ConvertToString().Trim(' ') == "")
                    {
                        BeneficiaryFullName = objModel.ResBeneficairyFName.ConvertToString().Trim() + " " + objModel.ResBeneficairyLName.ConvertToString().Trim();
                    }
                    else
                    {
                        BeneficiaryFullName = objModel.ResBeneficairyFName.ConvertToString().Trim() + " " + objModel.ResBeneficairyMName.ConvertToString().Trim() + " " + objModel.ResBeneficairyLName.ConvertToString().Trim();
                    }

                    if (objModel.Mode == "I")
                    {
                        qrResetlement = service.SubmitChanges("PR_NHRS_RESET_ENROLL_MST",
                                               "I",
                                                DBNull.Value,
                                                DBNull.Value,
                                                objModel.ResFirstName.ConvertToString().Trim(),
                                                objModel.ResMiddleName.ConvertToString().Trim(),
                                                objModel.ResLastName.ConvertToString().Trim(),
                                                respondantFullName,

                                                objModel.ResFathersFirstName.ConvertToString().Trim(),
                                                objModel.ResFathersMiddleName.ConvertToString().Trim(),
                                                objModel.ResFathersLastName.ConvertToString().Trim(),
                                                FathersFullName,

                                                objModel.ResGFathersFirstName.ConvertToString().Trim(),
                                                objModel.ResGFathersMiddleName.ConvertToString().Trim(),
                                                objModel.ResGFathersLastName.ConvertToString().Trim(),
                                                GrandFatherFullName,

                                                objModel.ResBeneficairyFName.ConvertToString().Trim(),
                                                objModel.ResBeneficairyMName.ConvertToString().Trim(),
                                                objModel.ResBeneficairyLName.ConvertToString().Trim(),
                                                BeneficiaryFullName,

                                                objModel.ResAge.ToDecimal(),//age
                                                objModel.ResFmc.ToDecimal(),//family member count
                                                objModel.ResDistrict.ToDecimal(),
                                                objModel.ResVDCMUN.ToDecimal(),
                                                objModel.ResWard.ToDecimal(),
                                                objModel.ResTole.ConvertToString(),//tole
                                                objModel.ResCtzNo.ConvertToString(),//citizenship number
                                                objModel.ResPaNo.ConvertToString(),//pa number

                                                DBNull.Value, //new pa
                                                objModel.ResEa.ToDecimal(),//ea
                                                objModel.ResHhSn.ToDecimal(),//hh_number
                                                objModel.ResSlipNo.ToDecimal(),//slip number
                                                objModel.ResMisReview.ConvertToString(),//mis review
                                                objModel.ResPhone.ConvertToString(),//phone
                                                objModel.ResRemarks.ConvertToString(),//review by engineer

                                                DBNull.Value,//status
                                                DBNull.Value,//approved
                                                DBNull.Value,//approved by
                                                DBNull.Value,//approved date
                                                DBNull.Value,//approved date loc  
                                                SessionCheck.getSessionUsername(),//entered by
                                                DateTime.Now,//entered date
                                                System.DateTime.Now.ConvertToString(),//entered date loc
                                                DBNull.Value,//updated by
                                                DBNull.Value,//updated date
                                                DBNull.Value,//updated date loc 
                                                DBNull.Value,//file batch id
                                                DBNull.Value //mis reviewed

                       );
                    }
                    else
                    {
                        qrResetlement = service.SubmitChanges("PR_NHRS_RESET_ENROLL_MST",
                                                objModel.Mode,
                                                objModel.ResettlementId.ToDecimal(),
                                                 DBNull.Value,
                                                objModel.ResFirstName.ConvertToString().Trim(),
                                                objModel.ResMiddleName.ConvertToString().Trim(),
                                                objModel.ResLastName.ConvertToString().Trim(),
                                                respondantFullName,

                                                objModel.ResFathersFirstName.ConvertToString().Trim(),
                                                objModel.ResFathersMiddleName.ConvertToString().Trim(),
                                                objModel.ResFathersLastName.ConvertToString().Trim(),
                                                FathersFullName,

                                                objModel.ResGFathersFirstName.ConvertToString().Trim(),
                                                objModel.ResGFathersMiddleName.ConvertToString().Trim(),
                                                objModel.ResGFathersLastName.ConvertToString().Trim(),
                                                GrandFatherFullName,

                                                objModel.ResBeneficairyFName.ConvertToString().Trim(),
                                                objModel.ResBeneficairyMName.ConvertToString().Trim(),
                                                objModel.ResBeneficairyLName.ConvertToString().Trim(),
                                                BeneficiaryFullName,

                                                objModel.ResAge.ToDecimal(),//age
                                                objModel.ResFmc.ToDecimal(),//family member count
                                                objModel.ResDistrict.ToDecimal(),
                                                objModel.ResVDCMUN.ToDecimal(),
                                                objModel.ResWard.ToDecimal(),
                                                objModel.ResTole.ConvertToString(),//tole
                                                objModel.ResCtzNo.ConvertToString(),//citizenship number
                                                objModel.ResPaNo.ConvertToString(),//pa number

                                                DBNull.Value, //new pa
                                                objModel.ResEa.ToDecimal(),//ea
                                                objModel.ResHhSn.ToDecimal(),//hh_number
                                                objModel.ResSlipNo.ToDecimal(),//slip number
                                                objModel.ResMisReview.ConvertToString(),//mis review
                                                objModel.ResPhone.ConvertToString(),//phone
                                                objModel.ResRemarks.ConvertToString(),//review by engineer

                                                DBNull.Value,//status
                                                DBNull.Value,//approved
                                                DBNull.Value,//approved by
                                                DBNull.Value,//approved date
                                                DBNull.Value,//approved date loc  
                                                SessionCheck.getSessionUsername(),//entered by
                                                DateTime.Now,//entered date
                                                System.DateTime.Now.ConvertToString(),//entered date loc
                                                SessionCheck.getSessionUsername(),//updated by
                                                DateTime.Now,//updated date
                                                System.DateTime.Now.ConvertToString(),//updated date loc
                                                DBNull.Value,//file batch id
                                                DBNull.Value //mis reviewed


                       );
                    }


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
                if (qrResetlement != null)
                {
                    result = qrResetlement.IsSuccess;
                }
                return result;
            }
        }



        #region get resettlement data by id
        public ResettlementModelClass getResettlementDataById(string id)
        {
            DataTable dt = new DataTable();
            string cmdText = null;
            ResettlementModelClass objModel = new ResettlementModelClass();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_RESETTLEMENT_ENROLL_MST where RESET_ENROLL_ID='" + id.ConvertToString() + "'";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

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
            if (dt.Rows.Count > 0)
            {
                objModel.ResettlementId = dt.Rows[0]["RESET_ENROLL_ID"].ConvertToString();

                objModel.ResFirstName = dt.Rows[0]["RESPONDANT_F_NAME"].ConvertToString();
                objModel.ResMiddleName = dt.Rows[0]["RESPONDANT_M_NAME"].ConvertToString();
                objModel.ResLastName = dt.Rows[0]["RESPONDANT_L_NAME"].ConvertToString();

                objModel.ResFathersFirstName = dt.Rows[0]["FATHERS_F_NAME"].ConvertToString();
                objModel.ResFathersMiddleName = dt.Rows[0]["FATHERS_M_NAME"].ConvertToString();
                objModel.ResFathersLastName = dt.Rows[0]["FATHERS_L_NAME"].ConvertToString();

                objModel.ResGFathersFirstName = dt.Rows[0]["GRAND_FF_NAME"].ConvertToString();
                objModel.ResGFathersMiddleName = dt.Rows[0]["GRAND_FM_NAME"].ConvertToString();
                objModel.ResGFathersLastName = dt.Rows[0]["GRAND_FL_NAME"].ConvertToString();

                objModel.ResBeneficairyFName = dt.Rows[0]["BENEFICIARY_F_NAME"].ConvertToString();
                objModel.ResBeneficairyMName = dt.Rows[0]["BENEFICIARY_M_NAME"].ConvertToString();
                objModel.ResBeneficairyLName = dt.Rows[0]["BENEFICIARY_L_NAME"].ConvertToString();


                objModel.ResAge = dt.Rows[0]["AGE"].ConvertToString();
                objModel.ResFmc = dt.Rows[0]["FAMILY_MEMBER_CNT"].ConvertToString();
                objModel.ResDistrict = dt.Rows[0]["DISTRICT_CD"].ConvertToString();
                objModel.ResVDCMUN = dt.Rows[0]["VDC_MUN_CD"].ConvertToString();

                objModel.ResWard = dt.Rows[0]["WARD"].ConvertToString();
                objModel.ResTole = dt.Rows[0]["TOLE"].ConvertToString();
                objModel.ResCtzNo = dt.Rows[0]["CITIZENSHIP_NO"].ConvertToString();
                objModel.ResPaNo = dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                objModel.NewPa = dt.Rows[0]["NEW_NRA_DEFINED_CD"].ConvertToString();

                objModel.ResEa = dt.Rows[0]["ENUMERATION_AREA"].ConvertToString();
                objModel.ResHhSn = dt.Rows[0]["HH_SN"].ConvertToString();
                objModel.ResSlipNo = dt.Rows[0]["SLIP_NO"].ConvertToString();
                objModel.ResMisReview = dt.Rows[0]["MIS_REVIEW"].ConvertToString();

                objModel.ResPhone = dt.Rows[0]["PHONE_NUMBER"].ConvertToString();
                objModel.ResRemarks = dt.Rows[0]["ENGINEERS_REMARKS"].ConvertToString();

            }
            return objModel;
        }
        #endregion



        #region get all resettlement data
        public DataTable GetAllResettlement(ResettlementModelClass objModel)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                try
                {
                    dt = service.GetDataTable(true, "GET_RESETTLEMENT_ENROLL_SEARCH",
                                        objModel.ResDistrict.ConvertToString(),
                                        objModel.ResVDCMUN.ConvertToString(),
                                        objModel.ResWard.ConvertToString(),
                                        objModel.ResPaNo.ConvertToString(),
                                        objModel.ResMisReview.ConvertToString(),
                                        DBNull.Value,
                                        DBNull.Value
                                        );
                }
                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }

            }
            return dt;

        }
        #endregion
    }
}
