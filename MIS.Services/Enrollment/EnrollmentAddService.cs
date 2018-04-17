using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Models.Enrollment;
using MIS.Services.Core;

namespace MIS.Services.Enrollment
{
    public class EnrollmentAddService
    {
        public DataTable GetEnrollmentAdd(string Houseownerid, string Pa)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                
                try
                {
                    string[] PaSplit = Pa.Split('-');
                    service.Begin();
                    
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    if (PaSplit[0]=="R")
                    {
                        dt = service.GetDataTable(true, "PR_NHRS_ENROL_RETRO_PRINT_VIEW",
                                               Houseownerid,
                                               Pa,
                                               DBNull.Value
                                              );
                    }
                    else if (PaSplit[0] == "G")
                    {
                        dt = service.GetDataTable(true, "PR_NHRS_ENROL_GRIE_PRINT_VIEW",
                                               Houseownerid,
                                               Pa,
                                               DBNull.Value
                                              );
                    }
                    else
                    {
                        dt = service.GetDataTable(true, "PR_NHRS_ENROLLMENT_PRINT_VIEW",
                                               Houseownerid.ToDecimal(),
                                               Pa.ConvertToString(),
                                               DBNull.Value
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
                    dt = null;
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

            return dt;
        }
        public void SaveEnrollmentAdd(EnrollmentAdd model)
        {
            EnrollmentMou mouenroll = new EnrollmentMou();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    mouenroll.TargtBatchId = model.TARGET_BATCH_ID == "" ? null : model.TARGET_BATCH_ID.ToDecimal();//model.TARGET_BATCH_ID.ToDecimal();
                    mouenroll.EnrollmntId = model.ENROLLMENT_ID == "" ? null : model.ENROLLMENT_ID.ToDecimal();//model.ENROLLMENT_ID.ToDecimal();
                    mouenroll.TargetingId = model.TARGETING_ID == "" ? null : model.TARGETING_ID.ToDecimal();//model.TARGETING_ID.ToDecimal();
                    //mouenroll.DistrictCD = model.DISTRICT_CD;
                    //mouenroll.VdcMunCD = model.VDC_MUN_CD.ConvertToString();
                    mouenroll.WardNO = model.WARD_NO == "" ? null : model.WARD_NO.ToDecimal();//model.WARD_NO.ToDecimal();
                    mouenroll.HouseOwnerID = model.HOUSE_OWNER_ID == "" ? null : model.HOUSE_OWNER_ID.ConvertToString();//model.HOUSE_OWNER_ID;
                    mouenroll.HoHldID = model.HOUSEHOLD_ID == "" ? null : model.HOUSEHOLD_ID.ConvertToString();//model.HOUSEHOLD_ID;
                    mouenroll.FiscalYr = model.FISCAL_YR == "" ? null : model.FISCAL_YR.ConvertToString();//model.FISCAL_YR;
                    mouenroll.EnumeratorID = model.ENUMERATOR_ID == "" ? null : model.ENUMERATOR_ID.ConvertToString();//model.ENUMERATOR_ID;                    
                    mouenroll.MOUId = model.MOU_ID == "" ? null : model.MOU_ID.ConvertToString();//model.MOU_ID;
                    mouenroll.SurveyNO = model.SURVEY_NO == "" ? null : model.SURVEY_NO.ConvertToString();//model.SURVEY_NO;
                    mouenroll.NRA_DEFINED_CD = model.NRA_DEFINED_CD == "" ? null : model.NRA_DEFINED_CD.ConvertToString();

                    mouenroll.BeneficiaryFirstNmEng = model.BENEFICIARY_FNAME_ENG == "" ? null : model.BENEFICIARY_FNAME_ENG.ConvertToString();//model.BENEFICIARY_FNAME_ENG;
                    mouenroll.BeneficiaryMiddleNmEng = model.BENEFICIARY_MNAME_ENG == "" ? null : model.BENEFICIARY_MNAME_ENG.ConvertToString();//model.BENEFICIARY_MNAME_ENG;
                    mouenroll.BeneficiaryLastNmEng = model.BENEFICIARY_LNAME_ENG == "" ? null : model.BENEFICIARY_LNAME_ENG.ConvertToString();//model.BENEFICIARY_LNAME_ENG;
                    mouenroll.BeneficiaryFullNameEng = model.BENEFICIARY_FULLNAME_ENG == "" ? null : model.BENEFICIARY_FULLNAME_ENG.ConvertToString();//model.BENEFICIARY_FULLNAME_ENG;
                    mouenroll.BeneficiaryFistNmLoc = model.BENEFICIARY_FNAME_LOC == "" ? null : model.BENEFICIARY_FNAME_LOC.ConvertToString();//model.BENEFICIARY_FNAME_LOC;
                    mouenroll.BeneficiaryMiddleNmLoc = model.BENEFICIARY_MNAME_LOC == "" ? null : model.BENEFICIARY_MNAME_LOC.ConvertToString();//model.BENEFICIARY_MNAME_LOC;
                    mouenroll.BeneficiaryLastNmLoc = model.BENEFICIARY_LNAME_LOC == "" ? null : model.BENEFICIARY_LNAME_LOC.ConvertToString();//model.BENEFICIARY_LNAME_LOC;
                    mouenroll.BeneficiaryFullNameLoc = model.BENEFICIARY_FULLNAME_LOC == "" ? null : model.BENEFICIARY_FULLNAME_LOC.ConvertToString();//model.BENEFICIARY_FULLNAME_LOC;
                    mouenroll.DistrictCD = model.DISTRICT_CD;
                    //mouenroll.VdcMunCD = model.VDC_MUN_CD.ConvertToString();
                    mouenroll.WardNO = model.WARD_NO == "" ? null : model.WARD_NO.ToDecimal();//model.WARD_NO.ToDecimal();
                    mouenroll.AreaENG = model.AREA_ENG == "" ? null : model.AREA_ENG.ConvertToString();//model.AREA_ENG;
                    mouenroll.AreaLOC = model.AREA_LOC == "" ? null : model.AREA_LOC.ConvertToString();//model.AREA_LOC;
                    mouenroll.FatherFirstnameEng = model.FATHER_FNAME_ENG == "" ? null : model.FATHER_FNAME_ENG.ConvertToString();//model.FATHER_FNAME_ENG;
                    mouenroll.FatherMiddlenameEng = model.FATHER_MNAME_ENG == "" ? null : model.FATHER_MNAME_ENG.ConvertToString();//model.FATHER_MNAME_ENG;
                    mouenroll.FatherLastnameEng = model.FATHER_LNAME_ENG == "" ? null : model.FATHER_LNAME_ENG.ConvertToString();//model.FATHER_LNAME_ENG;
                    mouenroll.FatherFullNameEng = model.FATHER_FullNAME_ENG == "" ? null : model.FATHER_FullNAME_ENG.ConvertToString();//model.FATHER_FullNAME_ENG;
                    mouenroll.FatherFirstnameLoc = model.FATHER_FNAME_LOC == "" ? null : model.FATHER_FNAME_LOC.ConvertToString();//model.FATHER_FNAME_LOC;
                    mouenroll.FatherMiddlenameLoc = model.FATHER_MNAME_LOC == "" ? null : model.FATHER_MNAME_LOC.ConvertToString();//model.FATHER_MNAME_LOC;
                    mouenroll.FatherLastnameLoc = model.FATHER_LNAME_LOC == "" ? null : model.FATHER_LNAME_LOC.ConvertToString();//model.FATHER_LNAME_LOC;
                    mouenroll.FatherFullNameLoc = model.FATHER_FullNAME_LOC == "" ? null : model.FATHER_FullNAME_LOC.ConvertToString();//model.FATHER_FullNAME_LOC;
                    mouenroll.GFatherFirstnameEng = model.GFATHER_FNAME_ENG == "" ? null : model.GFATHER_FNAME_ENG.ConvertToString();//model.GFATHER_FNAME_ENG;
                    mouenroll.GFatherMiddlenameEng = model.GFATHER_MNAME_ENG == "" ? null : model.GFATHER_MNAME_ENG.ConvertToString();//model.GFATHER_MNAME_ENG;
                    mouenroll.GFatherLastnameEng = model.GFATHER_LNAME_ENG == "" ? null : model.GFATHER_LNAME_ENG.ConvertToString();//model.GFATHER_LNAME_ENG;
                    mouenroll.GFatherFullNameEng = model.GFATHER_FullNAME_ENG == "" ? null : model.GFATHER_FullNAME_ENG.ConvertToString();//model.GFATHER_FullNAME_ENG;
                    mouenroll.GFatherFirstnameLoc = model.GFATHER_FNAME_LOC == "" ? null : model.GFATHER_FNAME_LOC.ConvertToString();//model.GFATHER_FNAME_LOC;
                    mouenroll.GFatherMiddlenameLoc = model.GFATHER_MNAME_LOC == "" ? null : model.GFATHER_MNAME_LOC.ConvertToString();//model.GFATHER_MNAME_LOC;
                    mouenroll.GFatherLastnameLoc = model.GFATHER_LNAME_LOC == "" ? null : model.GFATHER_LNAME_LOC.ConvertToString();//model.GFATHER_LNAME_LOC;
                    mouenroll.GFatherFullNameLoc = model.GFATHER_FullNAME_LOC == "" ? null : model.GFATHER_FullNAME_LOC.ConvertToString();//model.GFATHER_FullNAME_LOC;
                    mouenroll.FATHER_RELATION_TYPE_CD = model.FATHER_RELATION_TYPE_CD;
                    mouenroll.GFATHER_RELATION_TYPE_CD = model.GFATHER_RELATION_TYPE_CD;
                    mouenroll.IdentificationTypeCD = model.IDENTIFICATION_TYPE_CD;
                    mouenroll.IdentificationNO = model.IDENTIFICATION_NO == "" ? null : model.IDENTIFICATION_NO.ConvertToString();//model.IDENTIFICATION_NO;
                    mouenroll.IdentificationIssDisCD = model.IDENTIFICATION_ISSUE_DIS_CD;
                    if (model.IDENTIFICATION_ISSUE_DT == null)
                    {
                        mouenroll.IdentificationIssueDt = null;
                    }
                    else
                    {
                        mouenroll.IdentificationIssueDt =(model.IDENTIFICATION_ISSUE_DT).ConvertToString();
                    }
                    
                    mouenroll.IdentificationIssueDtLoc = model.IDENTIFICATION_ISS_DT_LOC == "" ? null : model.IDENTIFICATION_ISS_DT_LOC.ToString();//model.IDENTIFICATION_ISS_DT_LOC;
                    if (model.BIRTH_DT == null)
                    {
                        mouenroll.BENEFICIARY_DOB_ENG = null;
                    }
                    else
                    {
                        mouenroll.BENEFICIARY_DOB_ENG = (model.BIRTH_DT).ConvertToString();
                    }
                    //mouenroll.BENEFICIARY_DOB_ENG = model.BIRTH_DT.ToString() == "" ? null : model.BIRTH_DT.ToString("dd-MM-yyyy");//model.BIRTH_DT;
                    mouenroll.BENEFICIARY_DOB_LOC = model.BIRTH_DT_LOC == "" ? null : model.BIRTH_DT_LOC.ConvertToString();//model.BIRTH_DT_LOC;
                    mouenroll.FATHER_INLAW_FNAME_ENG = model.FinlawFnameEng == "" ? null : model.FinlawFnameEng.ConvertToString();
                    mouenroll.FATHER_INLAW_FNAME_LOC = model.FinlawFnameLoc == "" ? null : model.FinlawFnameLoc.ConvertToString();
                    mouenroll.FATHER_INLAW_MNAME_ENG = model.FinlawMnameEng == "" ? null : model.FinlawMnameEng.ConvertToString();
                    mouenroll.FATHER_INLAW_MNAME_LOC = model.FinlawMnameLoc == "" ? null : model.FinlawMnameLoc.ConvertToString();
                    mouenroll.FATHER_INLAW_LNAME_ENG = model.FinlawLnameEng == "" ? null : model.FinlawLnameEng.ConvertToString();
                    mouenroll.FATHER_INLAW_LNAME_LOC = model.FinlawLnameLoc == "" ? null : model.FinlawLnameLoc.ConvertToString();
                    mouenroll.FATHER_INLAW_FULLNAME_ENG = model.FinLawFullNameEng == "" ? null : model.FinLawFullNameEng.ConvertToString();
                    mouenroll.FATHER_INLAW_FULLNAME_LOC = model.FinLawFullNameLoc == "" ? null : model.FinLawFullNameLoc.ConvertToString();
                    mouenroll.SPOUSE_MEMBER_ID = model.husbandmemberid == "" ? null : model.husbandmemberid.ConvertToString();
                    mouenroll.SPOUSE_FIRST_NAME_ENG = model.husbandfnameeng == "" ? null : model.husbandfnameeng.ConvertToString();
                    mouenroll.SPOUSE_FIRST_NAME_LOC = model.husbandfnameloc == "" ? null : model.husbandfnameloc.ConvertToString();
                    mouenroll.SPOUSE_MIDDLE_NAME_ENG = model.husbandMnameeng == "" ? null : model.husbandMnameeng.ConvertToString();
                    mouenroll.SPOUSE_MIDDLE_NAME_LOC = model.husbandMnameloc == "" ? null : model.husbandMnameloc.ConvertToString();
                    mouenroll.SPOUSE_LAST_NAME_ENG = model.husbandLnameeng == "" ? null : model.husbandLnameeng.ConvertToString();
                    mouenroll.SPOUSE_LAST_NAME_LOC = model.husbandLnameloc == "" ? null : model.husbandLnameloc.ConvertToString();
                    mouenroll.SPOUSE_FULL_NAME_ENG = model.husbandFullnameEng == "" ? null : model.husbandFullnameEng.ConvertToString();
                    mouenroll.SPOUSE_FULL_NAME_LOC = model.husbandFullnameLoc == "" ? null : model.husbandFullnameLoc.ConvertToString();
                    //mouenroll.BENEFICIARY_PHONE = model.PHONE_NO == "" ? null : model.PHONE_NO.ConvertToString();//model.PHONE_NO;
                    //mouenroll.isBeneficiaryMigrated = model.IsMigrated == "" ? null : model.IsMigrated.ConvertToString();//model.IsMigrated;
                   //mouenroll.benfMigrationNo = model.migrationno == "" ? null : model.migrationno.ConvertToString();//model.migrationno;
                    //if (model.migrationdate == null)
                    //{
                    //    mouenroll.benfMigrationDate = null;
                    //}
                    //else
                    //{
                    //    mouenroll.benfMigrationDate = Convert.ToDateTime(model.migrationdate).ToString("MM-dd-yyyy");
                    //}
                    //mouenroll.benfMigrationDate = model.migrationdate.ToString() == "" ? null : model.migrationdate.ToString("dd-MM-yyyy");//model.migrationdate;
                    //mouenroll.benfMigratnDateLoc = model.migrationdateloc == "" ? null : model.migrationdateloc.ConvertToString();//model.migrationdateloc;
                    //mouenroll.BENEFICIARY_PHOTO = model.Beneficiary_Photo == "" ? null : model.Beneficiary_Photo.ConvertToString();//model.Beneficiary_Photo;
                    mouenroll.ISMANJURINAMA_AVAIL = model.IS_MANJURINAMA_AVAILABLE == "" ? null : model.IS_MANJURINAMA_AVAILABLE.ConvertToString();
                    mouenroll.MANJURINAMA_FNAME_ENG = model.PROXY_FNAME_ENG == "" ? null : model.PROXY_FNAME_ENG.ConvertToString();//model.PROXY_FNAME_ENG;
                    mouenroll.MANJURINAMA_MNAME_ENG = model.PROXY_MNAME_ENG == "" ? null : model.PROXY_MNAME_ENG.ConvertToString();//model.PROXY_MNAME_ENG;
                    mouenroll.MANJURINAMA_LNAME_ENG = model.PROXY_LNAME_ENG == "" ? null : model.PROXY_LNAME_ENG.ConvertToString();//model.PROXY_LNAME_ENG;
                    mouenroll.MANJURINAMA_FULLNAME_ENG = model.PROXY_FULLNAME_ENG == "" ? null : model.PROXY_FULLNAME_ENG.ConvertToString();//model.PROXY_FULLNAME_ENG;
                    mouenroll.MANJURINAMA_FNAME_LOC = model.PROXY_FNAME_LOC == "" ? null : model.PROXY_FNAME_LOC.ConvertToString();//model.PROXY_FNAME_LOC;
                    mouenroll.MANJURINAMA_MNAME_LOC = model.PROXY_MNAME_LOC == "" ? null : model.PROXY_MNAME_LOC.ConvertToString();//model.PROXY_MNAME_LOC;
                    mouenroll.MANJURINAMA_LNAME_LOC = model.PROXY_LNAME_LOC == "" ? null : model.PROXY_LNAME_LOC.ConvertToString();//model.PROXY_LNAME_LOC;
                    mouenroll.MANJURINAMA_FULLNAME_LOC = model.PROXY_FULLNAME_LOC == "" ? null : model.PROXY_FULLNAME_LOC.ConvertToString();//model.PROXY_FULLNAME_LOC;
                    //mouenroll.MANJURINAMA_DISTRICT_CD = model.PROXY_DISTRICT_CD;
                    //mouenroll.MANJURINAMA_VDC_CD = model.PROXY_VDC_MUN_CD.ToDecimal();
                    //mouenroll.MANJURINAMA_WARD_NO = model.PROXY_WARD_NO == "" ? null : model.PROXY_WARD_NO.ConvertToString();//model.PROXY_WARD_NO;
                    //mouenroll.MANJURINAMA_AREA_ENG = model.PROXY_AREA_ENG == "" ? null : model.PROXY_AREA_ENG.ConvertToString();//model.PROXY_AREA_ENG;
                    //mouenroll.MANJURINAMA_AREA_LOC = model.PROXY_AREA_LOC == "" ? null : model.PROXY_AREA_LOC.ConvertToString();//model.PROXY_AREA_LOC;
                    //mouenroll.MANJURINAMA_IDENTITY_TYPE_CD = model.PROXY_IDENTIFICATION_TYPE_CD;
                    //mouenroll.MANJURINAMA_IDENTITY_NO = model.PROXY_IDENTIFICATION_NO == "" ? null : model.PROXY_IDENTIFICATION_NO.ConvertToString();//model.PROXY_IDENTIFICATION_NO;
                    //mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DIS_CD = model.PROXY_IDENTIFICATION_ISSUE_DIS_CD;
                    //if (model.PROXY_IDENTIFICATION_ISSUE_DT == null)
                    //{
                    //    mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DT = null;
                    //}
                    //else
                    //{
                    //    mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DT = Convert.ToDateTime(model.PROXY_IDENTIFICATION_ISSUE_DT).ToString("MM-dd-yyyy");
                    //}
                    ////mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DT = model.PROXY_IDENTIFICATION_ISSUE_DT.ToString() == "" ? null : model.PROXY_IDENTIFICATION_ISSUE_DT.ToString("dd-MM-yyyy");//model.PROXY_IDENTIFICATION_ISSUE_DT;
                    //mouenroll.MANJURINAMA_IDENTIFICATION_ISS_DT_LOC = model.PROXY_IDENTIFICATION_ISS_DT_LOC == "" ? null : model.PROXY_IDENTIFICATION_ISS_DT_LOC.ConvertToString();//model.PROXY_IDENTIFICATION_ISS_DT_LOC;
                    //if (model.PROXY_BIRTH_DT == null)
                    //{
                    //    mouenroll.MANJURINAMA_BIRTH_DT = null;
                    //}
                    //else
                    //{
                    //    mouenroll.MANJURINAMA_BIRTH_DT = Convert.ToDateTime(model.PROXY_BIRTH_DT).ToString("MM-dd-yyyy");
                    //}
                    ////mouenroll.manjurinama_birth_dt = model.proxy_birth_dt.tostring() == "" ? null : model.proxy_birth_dt.tostring("dd-mm-yyyy");//model.proxy_birth_dt;
                    //mouenroll.manjurinama_birth_dt_loc = model.proxy_birth_dt_loc == "" ? null : model.proxy_birth_dt_loc.converttostring();//model.proxy_birth_dt_loc;
                    //mouenroll.MANJURINAMA_FATHER_FNAME_ENG = model.PROXY_FATHERS_FNAME_ENG == "" ? null : model.PROXY_FATHERS_FNAME_ENG.ConvertToString();//model.PROXY_FATHERS_FNAME_ENG;
                    //mouenroll.MANJURINAMA_FATHER_MNAME_ENG = model.PROXY_FATHERS_MNAME_ENG == "" ? null : model.PROXY_FATHERS_MNAME_ENG.ConvertToString();//model.PROXY_FATHERS_MNAME_ENG;
                    //mouenroll.MANJURINAMA_FATHER_LNAME_ENG = model.PROXY_FATHERS_LNAME_ENG == "" ? null : model.PROXY_FATHERS_LNAME_ENG.ConvertToString();//model.PROXY_FATHERS_LNAME_ENG;
                    //mouenroll.MANJURINAMA_FATHER_FNAME_LOC = model.PROXY_FATHERS_FNAME_LOC == "" ? null : model.PROXY_FATHERS_FNAME_LOC.ConvertToString();//model.PROXY_FATHERS_FNAME_LOC;
                    //mouenroll.MANJURINAMA_FATHER_MNAME_LOC = model.PROXY_FATHERS_MNAME_LOC == "" ? null : model.PROXY_FATHERS_MNAME_LOC.ConvertToString();//model.PROXY_FATHERS_MNAME_LOC;
                    //mouenroll.MANJURINAMA_FATHER_LNAME_LOC = model.PROXY_FATHERS_LNAME_LOC == "" ? null : model.PROXY_FATHERS_LNAME_LOC.ConvertToString();//model.PROXY_FATHERS_LNAME_LOC;
                    //mouenroll.MANJURINAMA_FATHER_FULLNAME_ENG = model.PROXY_FATHERSNAME_ENG == "" ? null : model.PROXY_FATHERSNAME_ENG.ConvertToString();//model.PROXY_FATHERSNAME_ENG;
                    //mouenroll.MANJURINAMA_FATHER_FULLNAME_LOC = model.PROXY_FATHERSNAME_LOC == "" ? null : model.PROXY_FATHERSNAME_LOC.ConvertToString();//model.PROXY_FATHERSNAME_LOC;
                    //mouenroll.MANJURINAMA_GFATHER_FNAME_ENG = model.PROXY_GFATHERS_FNAME_ENG == "" ? null : model.PROXY_GFATHERS_FNAME_ENG.ConvertToString();//model.PROXY_GFATHERS_FNAME_ENG;
                    //mouenroll.MANJURINAMA_GFATHER_MNAME_ENG = model.PROXY_GFATHERS_MNAME_ENG == "" ? null : model.PROXY_GFATHERS_MNAME_ENG.ConvertToString();//model.PROXY_GFATHERS_MNAME_ENG;
                    //mouenroll.MANJURINAMA_GFATHER_LNAME_ENG = model.PROXY_GFATHERS_LNAME_ENG == "" ? null : model.PROXY_GFATHERS_LNAME_ENG.ConvertToString();//model.PROXY_GFATHERS_LNAME_ENG;
                    //mouenroll.MANJURINAMA_GFATHER_FNAME_LOC = model.PROXY_GFATHERS_FNAME_LOC == "" ? null : model.PROXY_GFATHERS_FNAME_LOC.ConvertToString();//model.PROXY_GFATHERS_FNAME_LOC;
                    //mouenroll.MANJURINAMA_GFATHER_MNAME_LOC = model.PROXY_GFATHERS_MNAME_LOC == "" ? null : model.PROXY_GFATHERS_MNAME_LOC.ConvertToString();//model.PROXY_GFATHERS_MNAME_LOC;
                    //mouenroll.MANJURINAMA_GFATHER_LNAME_LOC = model.PROXY_GFATHERS_LNAME_LOC == "" ? null : model.PROXY_GFATHERS_LNAME_LOC.ConvertToString();//model.PROXY_GFATHERS_LNAME_LOC;
                    //mouenroll.MANJURINAMA_GFATHER_FULLNAME_ENG = model.PROXY_GRANDFATHERNAME_ENG == "" ? null : model.PROXY_GRANDFATHERNAME_ENG.ConvertToString();//model.PROXY_GRANDFATHERNAME_ENG;
                    //mouenroll.MANJURINAMA_GFATHER_FULLNAME_LOC = model.PROXY_GRANDFATHERNAME_LOC == "" ? null : model.PROXY_GRANDFATHERNAME_LOC.ConvertToString();//model.PROXY_GRANDFATHERNAME_LOC;
                    //mouenroll.MANJURINAMA_RELATION_TYPE_CD = model.PROXY_RELATION_TYPE_CD;
                    //mouenroll.MANJURINAMA_PHONE = model.PROXY_PHONE == "" ? null : model.PROXY_PHONE.ConvertToString();//model.PROXY_PHONE;
                    mouenroll.BankCD = model.BANK_CD == "" ? null : model.BANK_CD.ToDecimal();//model.BANK_CD;
                    mouenroll.BankBrnchCD = model.BANK_BRANCH_CD == "" ? null : model.BANK_BRANCH_CD.ToDecimal();//model.BANK_BRANCH_CD;
                    //mouenroll.BankAccNO = model.BANK_ACC_NO == "" ? null : model.BANK_ACC_NO.ConvertToString();//model.BANK_ACC_NO;
                   // mouenroll.BankAccTypCD = model.BANK_ACC_TYPE_CD == "" ? null : model.BANK_ACC_TYPE_CD.ToDecimal();//model.BANK_ACC_TYPE_CD;
                  
                   // mouenroll.BuildingKittaNO = model.BUILDING_KITTA_NUMBER == "" ? null : model.BUILDING_KITTA_NUMBER.ConvertToString();//model.BUILDING_KITTA_NUMBER;
                   // mouenroll.BuildingAREA = model.BUILDING_AREA == "" ? null : model.BUILDING_AREA.ConvertToString();//model.BUILDING_AREA;
                    //mouenroll.BuildingAreaENG = model.BUILDING_AREA_ENG == "" ? null : model.BUILDING_AREA_ENG.ConvertToString();//model.BUILDING_AREA_ENG;
                   // mouenroll.BuildingAreaLOC = model.BUILDING_AREA_LOC == "" ? null : model.BUILDING_AREA_LOC.ConvertToString();//model.BUILDING_AREA_LOC;
                   // mouenroll.IS_BUILDING_TOBE_CONSTRUCTED = model.TYPE_OF_HOUSE_TO_BE_CONSTRUCTED == "" ? null : model.TYPE_OF_HOUSE_TO_BE_CONSTRUCTED.ConvertToString();//model.TYPE_OF_HOUSE_TO_BE_CONSTRUCTED;
                   // mouenroll.isBulDesignFrmCatalog = model.IS_BUILDING_DESIGN_FROM_CATALOG == "" ? null : model.IS_BUILDING_DESIGN_FROM_CATALOG.ConvertToString();//model.IS_BUILDING_DESIGN_FROM_CATALOG;
                    //mouenroll.bulDesignCatNo = model.BUILDING_DESIGN_CATALOG_CD == "" ? null : model.BUILDING_DESIGN_CATALOG_CD.ToDecimal();//model.BUILDING_DESIGN_CATALOG_CD.ToDecimal();
                   // mouenroll.IS_BUILDING_OWN_DESIGN = model.BUILDING_HAVE_OWN_DESIGN == "" ? null : model.BUILDING_HAVE_OWN_DESIGN.ConvertToString();//model.BUILDING_HAVE_OWN_DESIGN;
                   // mouenroll.buildingPilarTyp = model.BUILDING_WALL_OR_PILLAR_TYPE_NO == "" ? null : model.BUILDING_WALL_OR_PILLAR_TYPE_NO.ConvertToString();//model.BUILDING_WALL_OR_PILLAR_TYPE_NO;
                    //mouenroll.bulFloorRoofTyp = model.BUILDING_FLOOR_OR_ROOF_TYPE_NO == "" ? null : model.BUILDING_FLOOR_OR_ROOF_TYPE_NO.ConvertToString();//model.BUILDING_FLOOR_OR_ROOF_TYPE_NO;
                    //mouenroll.bulOtherTyp = model.BUILDING_DESIGN_OTHER == "" ? null : model.BUILDING_DESIGN_OTHER.ConvertToString();//model.BUILDING_DESIGN_OTHER;
              
                    //mouenroll.NomineeFullnameEng = model.NOMINEE_FULLNAME_ENG == "" ? null : model.NOMINEE_FULLNAME_ENG.ConvertToString();//model.NOMINEE_FULLNAME_ENG;
                    //mouenroll.NomineeFirstnamLoc = model.NOMINEE_FNAME_LOC == "" ? null : model.NOMINEE_FNAME_LOC.ConvertToString();//model.NOMINEE_FNAME_LOC;
                    //mouenroll.NomineeMiddlenamLoc = model.NOMINEE_MNAME_LOC == "" ? null : model.NOMINEE_MNAME_LOC.ConvertToString();//model.NOMINEE_MNAME_LOC;
                   // mouenroll.NomineeLastnamLoc = model.NOMINEE_LNAME_LOC == "" ? null : model.NOMINEE_LNAME_LOC.ConvertToString();//model.NOMINEE_LNAME_LOC;
                   // mouenroll.NomineeFullnameLoc = model.NOMINEE_FULLNAME_LOC == "" ? null : model.NOMINEE_FULLNAME_LOC.ConvertToString();//model.NOMINEE_FULLNAME_LOC;
                  //  mouenroll.EmployCD = model.EMPLOYEE_CD == "" ? null : model.EMPLOYEE_CD.ConvertToString();//model.EMPLOYEE_CD;
                  //  mouenroll.OfficeCD = model.OFFICE_CD == "" ? null : model.OFFICE_CD.ConvertToString();// model.OFFICE_CD;
                    mouenroll.Remark = model.REMARKS == "" ? null : model.REMARKS.ConvertToString();//model.REMARKS;
                    mouenroll.RemarkLoc = model.REMARKS_LOC == "" ? null : model.REMARKS_LOC.ConvertToString();//model.REMARKS_LOC;
                   // mouenroll.BUILDING_STRUCTURE_NO = model.BUILDING_STRUCTURE_NO == "" ? null : model.BUILDING_STRUCTURE_NO.ConvertToString();
                    mouenroll.ApproveD = "Y";
                    //mouenroll.IsPaymentReceiverChanged = model.IS_PAYMENT_RECEIVER_CHANGED;
                    mouenroll.UpdatedBY = SessionCheck.getSessionUsername();
                    mouenroll.EnteredBY = SessionCheck.getSessionUsername();
                    mouenroll.EnteredDT = (DateTime.Now).ToDateTime("MM-dd-yyyy");// DateTime.Now.ToString();
                    if (model.ModeType=="I")
                    {
                        mouenroll.Mode = "I";
                    }
                    else if (model.ModeType == "U")
                    {
                        mouenroll.Mode = "U";
                    }
                    //mouenroll.Mode = model.ModeType;
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(mouenroll, true);

                    //string[] strdoc = model.Docs.Split(',');
                    //string[] strdocType = model.DocType.Split(',');
                    //int index = 0;
                    //foreach(string s in strdoc)
                    //{
                    //    MisHhDocDtlInfo mouDocument = new MisHhDocDtlInfo();
                    //    mouDocument.HOUSE_OWNER_ID = model.HOUSE_OWNER_ID;
                    //    mouDocument.HouseholdId = model.HOUSEHOLD_ID;
                    //    mouDocument.BUILDING_STRUCTURE_NO = mouenroll.BUILDING_STRUCTURE_NO;
                    //    mouDocument.DocTypeCd = strdocType[index].ToDecimal();
                    //    mouDocument.DocId = s;
                    //    mouDocument.ApprovedBy = SessionCheck.getSessionUsername();
                    //    mouDocument.ApprovedDt = DateTime.Now.ToString("MM-dd-yyyy");
                    //    mouDocument.UpdatedBy = SessionCheck.getSessionUsername();
                    //    mouDocument.UpdatedDt = DateTime.Now.ToString("MM-dd-yyyy");
                    //    mouDocument.EnteredBy = SessionCheck.getSessionUsername();
                    //    mouDocument.EnteredDt = DateTime.Now.ToString("MM-dd-yyyy");
                    //    mouDocument.BATCH_ID = model.TARGET_BATCH_ID;
                    //    mouDocument.Mode = "I"; //model.ModeType; //"I";
                    //    QueryResult qr1 = service.SubmitChanges(mouDocument, true);
                    //    index++;
                    //}                   

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
        public bool SaveEnrollmentAdd1(EnrollmentAdd model)
        {
            bool result = false;
            EnrollmentMou mouenroll = new EnrollmentMou();
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    mouenroll.TargetingId = model.TARGETING_ID == "" ? null : model.TARGETING_ID.ToDecimal();//model.TARGETING_ID.ToDecimal();
                    mouenroll.TargtBatchId = model.TARGET_BATCH_ID == "" ? null : model.TARGET_BATCH_ID.ToDecimal();//model.TARGET_BATCH_ID.ToDecimal();
                    mouenroll.AreaENG = model.AREA_ENG == "" ? null : model.AREA_ENG.ConvertToString();//model.AREA_ENG;
                    mouenroll.EnumeratorID = model.ENUMERATOR_ID == "" ? null : model.ENUMERATOR_ID.ConvertToString();//model.ENUMERATOR_ID;                    
                    mouenroll.MOUId = model.MOU_ID == "" ? null : model.MOU_ID.ConvertToString();//model.MOU_ID;
                    mouenroll.HouseOwnerID = model.HOUSE_OWNER_ID == "" ? null : model.HOUSE_OWNER_ID.ConvertToString();//model.HOUSE_OWNER_ID;
                    mouenroll.HoHldID = model.HOUSEHOLD_ID == "" ? null : model.HOUSEHOLD_ID.ConvertToString();//model.HOUSEHOLD_ID;
                    mouenroll.SurveyNO = model.SURVEY_NO == "" ? null : model.SURVEY_NO.ConvertToString();//model.SURVEY_NO;
                    mouenroll.FiscalYr = model.FISCAL_YR == "" ? null : model.FISCAL_YR.ConvertToString();//model.FISCAL_YR;
                    mouenroll.NRA_DEFINED_CD = model.NRA_DEFINED_CD.ConvertToString();
                    mouenroll.BeneficiaryFirstNmEng = model.BENEFICIARY_FNAME_ENG == "" ? null : model.BENEFICIARY_FNAME_ENG.ConvertToString();//model.BENEFICIARY_FNAME_ENG;
                    mouenroll.BeneficiaryMiddleNmEng = model.BENEFICIARY_MNAME_ENG == "" ? null : model.BENEFICIARY_MNAME_ENG.ConvertToString();//model.BENEFICIARY_MNAME_ENG;
                    mouenroll.BeneficiaryLastNmEng = model.BENEFICIARY_LNAME_ENG == "" ? null : model.BENEFICIARY_LNAME_ENG.ConvertToString();//model.BENEFICIARY_LNAME_ENG;

                    mouenroll.BeneficiaryFistNmLoc = model.BENEFICIARY_FNAME_LOC == "" ? null : model.BENEFICIARY_FNAME_LOC.ConvertToString();//model.BENEFICIARY_FNAME_LOC;
                    mouenroll.BeneficiaryMiddleNmLoc = model.BENEFICIARY_MNAME_LOC == "" ? null : model.BENEFICIARY_MNAME_LOC.ConvertToString();//model.BENEFICIARY_MNAME_LOC;
                    mouenroll.BeneficiaryLastNmLoc = model.BENEFICIARY_LNAME_LOC == "" ? null : model.BENEFICIARY_LNAME_LOC.ConvertToString();//model.BENEFICIARY_LNAME_LOC;

                    mouenroll.DistrictCD = model.DISTRICT_CD;//model.DISTRICT_CD;
                    mouenroll.VdcMunCD = model.VDC_MUN_CD.ConvertToString();
                    //mouenroll.VdcMunCD = model.VDC_MUN_CD == "" ? null : model.VDC_MUN_CD.ToDecimal();
                    mouenroll.WardNO = model.WARD_NO == "" ? null : model.WARD_NO.ToDecimal();//model.WARD_NO.ToDecimal();

                    mouenroll.IdentificationNO = model.IDENTIFICATION_NO == "" ? null : model.IDENTIFICATION_NO.ConvertToString();//model.IDENTIFICATION_NO;
                    mouenroll.IdentificationIssDisCD = model.IDENTIFICATION_ISSUE_DIS_CD;

                    if (model.IDENTIFICATION_ISSUE_DT == null)
                    {
                        mouenroll.IdentificationIssueDt = null;
                    }
                    else
                    {
                        mouenroll.IdentificationIssueDt = (model.IDENTIFICATION_ISSUE_DT).ConvertToString();
                    }
                    mouenroll.IdentificationIssueDtLoc = model.IDENTIFICATION_ISS_DT_LOC == "" ? null : model.IDENTIFICATION_ISS_DT_LOC.ConvertToString();//model.IDENTIFICATION_ISS_DT_LOC;

                    if (model.BIRTH_DT == null)
                    {
                        mouenroll.BENEFICIARY_DOB_ENG = null;
                    }
                    else
                    {
                        mouenroll.BENEFICIARY_DOB_ENG = (model.BIRTH_DT).ConvertToString();
                    }
                    mouenroll.ISMANJURINAMA_AVAIL = model.IS_MANJURINAMA_AVAILABLE == "" ? null : model.IS_MANJURINAMA_AVAILABLE.ConvertToString();
                    mouenroll.BENEFICIARY_DOB_LOC = model.BIRTH_DT_LOC == "" ? null : model.BIRTH_DT_LOC.ConvertToString();//model.BIRTH_DT_LOC;

                    mouenroll.BENEFICIARY_PHONE = model.PHONE_NO == "" ? null : model.PHONE_NO.ConvertToString();//model.PHONE_NO;

                    mouenroll.benfMigrationNo = model.migrationno.ToDecimal();//model.migrationno;

                    if (model.migrationdate == null)
                    {
                        mouenroll.benfMigrationDate = null;
                    }
                    else
                    {
                        mouenroll.benfMigrationDate = (model.migrationdate).ConvertToString();
                    }

                    //mouenroll.benfMigrationDate = model.migrationdate.ConvertToString();//model.migrationdate;
                    mouenroll.benfMigratnDateLoc = model.migrationdate.ConvertToString();
                    //mouenroll.benfMigratnDateLoc = model.migrationdateloc == "" ? null : model.migrationdateloc.ConvertToString();//model.migrationdate;
                        mouenroll.BENEFICIARY_PHOTO = model.Beneficiary_Photo == "" ? null : model.Beneficiary_Photo.ConvertToString();

                    mouenroll.MANJURINAMA_FNAME_ENG = model.PROXY_FNAME_ENG == "" ? null : model.PROXY_FNAME_ENG.ConvertToString();//model.PROXY_FNAME_ENG;
                    mouenroll.MANJURINAMA_MNAME_ENG = model.PROXY_MNAME_ENG == "" ? null : model.PROXY_MNAME_ENG.ConvertToString();//model.PROXY_MNAME_ENG;
                    mouenroll.MANJURINAMA_LNAME_ENG = model.PROXY_LNAME_ENG == "" ? null : model.PROXY_LNAME_ENG.ConvertToString();//model.PROXY_LNAME_ENG;
                    //mouenroll.MANJURINAMA_FULLNAME_ENG = model.PROXY_FULLNAME_ENG == "" ? null : model.PROXY_FULLNAME_ENG.ConvertToString();//model.PROXY_FULLNAME_ENG;
                    mouenroll.MANJURINAMA_FNAME_LOC = model.PROXY_FNAME_LOC == "" ? null : model.PROXY_FNAME_LOC.ConvertToString();//model.PROXY_FNAME_LOC;
                    mouenroll.MANJURINAMA_MNAME_LOC = model.PROXY_MNAME_LOC == "" ? null : model.PROXY_MNAME_LOC.ConvertToString();//model.PROXY_MNAME_LOC;
                    mouenroll.MANJURINAMA_LNAME_LOC = model.PROXY_LNAME_LOC == "" ? null : model.PROXY_LNAME_LOC.ConvertToString();//model.PROXY_LNAME_LOC;

                    mouenroll.MANJURINAMA_DISTRICT_CD = model.PROXY_DISTRICT_CD;

                    mouenroll.MANJURINAMA_VDC_CD = model.PROXY_VDC_MUN_CD.ConvertToString() == "" ? null : model.PROXY_VDC_MUN_CD.ConvertToString();//model.PROXY_MNAME_ENG;

                    //mouenroll.MANJURINAMA_VDC_CD = model.PROXY_VDC_MUN_CD.ToDecimal();
                    mouenroll.MANJURINAMA_WARD_NO = model.PROXY_WARD_NO;//model.PROXY_WARD_NO;
                    mouenroll.MANJURINAMA_AREA_ENG = model.PROXY_AREA_ENG == "" ? null : model.PROXY_AREA_ENG.ConvertToString();//model.proxy_area_eng;
                    mouenroll.MANJURINAMA_IDENTITY_NO = model.PROXY_IDENTIFICATION_NO == "" ? null : model.PROXY_IDENTIFICATION_NO.ConvertToString();//model.IDENTIFICATION_NO;

                    //mouenroll.MANJURINAMA_IDENTITY_NO = model.PROXY_IDENTIFICATION_NO == "" ? null : model.PROXY_IDENTIFICATION_NO.ConvertToString();
                    //mouenroll.MANJURINAMA_IDENTITY_NO = model.PROXY_IDENTIFICATION_NO == "" ? null : model.PROXY_IDENTIFICATION_NO.ConvertToString();//model.PROXY_IDENTIFICATION_NO;

                    mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DIS_CD = model.PROXY_IDENTIFICATION_ISSUE_DIS_CD;
                    if (model.PROXY_IDENTIFICATION_ISSUE_DT == null)
                    {
                        mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DT = null;
                    }
                    else
                    {
                        mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DT = (model.PROXY_IDENTIFICATION_ISSUE_DT).ConvertToString();
                    }

                    mouenroll.MANJURINAMA_IDENTIFICATION_ISS_DT_LOC = model.PROXY_IDENTIFICATION_ISS_DT_LOC == "" ? null : model.PROXY_IDENTIFICATION_ISS_DT_LOC.ConvertToString();

                    if (model.PROXY_BIRTH_DT == null)
                    {
                        mouenroll.MANJURINAMA_BIRTH_DT = null;
                    }
                    else
                    {
                        mouenroll.MANJURINAMA_BIRTH_DT = (model.PROXY_BIRTH_DT).ConvertToString();
                    }
                    mouenroll.MANJURINAMA_BIRTH_DT_LOC = model.PROXY_BIRTH_DT_LOC == "" ? null : model.PROXY_BIRTH_DT_LOC.ConvertToString();//model.PROXY_BIRTH_DT_LOC;

                    mouenroll.MANJURINAMA_GFATHER_FNAME_ENG = model.PROXY_GFATHERS_FNAME_ENG == "" ? null : model.PROXY_GFATHERS_FNAME_ENG.ConvertToString();//model.PROXY_GFATHERS_FNAME_ENG;
                    mouenroll.MANJURINAMA_GFATHER_MNAME_ENG = model.PROXY_GFATHERS_MNAME_ENG == "" ? null : model.PROXY_GFATHERS_MNAME_ENG.ConvertToString();//model.PROXY_GFATHERS_MNAME_ENG;
                    mouenroll.MANJURINAMA_GFATHER_LNAME_ENG = model.PROXY_GFATHERS_LNAME_ENG == "" ? null : model.PROXY_GFATHERS_LNAME_ENG.ConvertToString();//model.PROXY_GFATHERS_LNAME_ENG;
                    mouenroll.MANJURINAMA_GFATHER_FNAME_LOC = model.PROXY_GFATHERS_FNAME_LOC == "" ? null : model.PROXY_GFATHERS_FNAME_LOC.ConvertToString();//model.PROXY_GFATHERS_FNAME_LOC;
                    mouenroll.MANJURINAMA_GFATHER_MNAME_LOC = model.PROXY_GFATHERS_MNAME_LOC == "" ? null : model.PROXY_GFATHERS_MNAME_LOC.ConvertToString();//model.PROXY_GFATHERS_MNAME_LOC;
                    mouenroll.MANJURINAMA_GFATHER_LNAME_LOC = model.PROXY_GFATHERS_LNAME_LOC == "" ? null : model.PROXY_GFATHERS_LNAME_LOC.ConvertToString();//model.PROXY_GFATHERS_LNAME_LOC;
                    ////mouenroll.MANJURINAMA_GFATHER_FULLNAME_ENG = model.PROXY_GRANDFATHERNAME_ENG == "" ? null : model.PROXY_GRANDFATHERNAME_ENG.ConvertToString();//model.PROXY_GRANDFATHERNAME_ENG;
                    ////mouenroll.MANJURINAMA_GFATHER_FULLNAME_LOC = model.PROXY_GRANDFATHERNAME_LOC == "" ? null : model.PROXY_GRANDFATHERNAME_LOC.ConvertToString();//model.PROXY_GRANDFATHERNAME_LOC;
                    mouenroll.MANJURINAMA_FATHER_FNAME_ENG = model.PROXY_FATHERS_FNAME_ENG == "" ? null : model.PROXY_FATHERS_FNAME_ENG.ConvertToString();//model.PROXY_FATHERS_FNAME_ENG;
                    mouenroll.MANJURINAMA_FATHER_MNAME_ENG = model.PROXY_FATHERS_MNAME_ENG == "" ? null : model.PROXY_FATHERS_MNAME_ENG.ConvertToString();//model.PROXY_FATHERS_MNAME_ENG;
                    mouenroll.MANJURINAMA_FATHER_LNAME_ENG = model.PROXY_FATHERS_LNAME_ENG == "" ? null : model.PROXY_FATHERS_LNAME_ENG.ConvertToString();//model.PROXY_FATHERS_LNAME_ENG;
                    mouenroll.MANJURINAMA_FATHER_FNAME_LOC = model.PROXY_FATHERS_FNAME_LOC == "" ? null : model.PROXY_FATHERS_FNAME_LOC.ConvertToString();//model.PROXY_FATHERS_FNAME_LOC;
                    mouenroll.MANJURINAMA_FATHER_MNAME_LOC = model.PROXY_FATHERS_MNAME_LOC == "" ? null : model.PROXY_FATHERS_MNAME_LOC.ConvertToString();//model.PROXY_FATHERS_MNAME_LOC;
                    mouenroll.MANJURINAMA_FATHER_LNAME_LOC = model.PROXY_FATHERS_LNAME_LOC == "" ? null : model.PROXY_FATHERS_LNAME_LOC.ConvertToString();//model.PROXY_FATHERS_LNAME_LOC;
                    mouenroll.MANJURINAMA_RELATION_TYPE_CD = model.PROXY_RELATION_TYPE_CD;
                    mouenroll.MANJURINAMA_PHONE = model.PROXY_PHONE == "" ? null : model.PROXY_PHONE.ConvertToString();//model.PROXY_PHONE;

                    mouenroll.BankAccNO = model.BANK_ACC_NO == "" ? null : model.BANK_ACC_NO.ConvertToString();//model.BANK_ACC_NO;
                    mouenroll.BankCD = model.BANK_CD == "" ? null : model.BANK_CD.ToDecimal();//model.BANK_CD;
                    mouenroll.BankBrnchCD = model.BANK_BRANCH_CD == "" ? null : model.BANK_BRANCH_CD.ToDecimal();//model.BANK_BRANCH_CD;
                    mouenroll.ACC_HOLD_FNAME_ENG = model.ACC_HOLDER_FNAME_ENG == "" ? null : model.ACC_HOLDER_FNAME_ENG.ConvertToString();//model.PROXY_FATHERS_FNAME_ENG;
                    mouenroll.ACC_HOLD_MNAME_ENG = model.ACC_HOLDER_MNAME_ENG == "" ? null : model.ACC_HOLDER_MNAME_ENG.ConvertToString();//model.PROXY_FATHERS_MNAME_ENG;
                    mouenroll.ACC_HOLD_LNAME_ENG = model.ACC_HOLDER_LNAME_ENG == "" ? null : model.ACC_HOLDER_LNAME_ENG.ConvertToString();//model.PROXY_FATHERS_LNAME_ENG;


                    mouenroll.BuildingKittaNO = model.BUILDING_KITTA_NUMBER == "" ? null : model.BUILDING_KITTA_NUMBER.ConvertToString();//model.BUILDING_KITTA_NUMBER;
                    mouenroll.BuildingAreaENG = model.BUILDING_AREA_ENG == "" ? null : model.BUILDING_AREA_ENG.ConvertToString();//model.BUILDING_AREA_ENG;
                    mouenroll.BuildingAREA = model.BUILDING_AREA == "" ? null : model.BUILDING_AREA.ConvertToString();//model.BUILDING_AREA;
                    mouenroll.BuildingDistCD = model.BUILDING_DISTRICT_CD;
                    mouenroll.BuildingVdcMunCD =Convert.ToDecimal(model.BUILDING_VDC_MUN_CD.ConvertToString() == "" ? null : model.BUILDING_VDC_MUN_CD.ConvertToString());//model.PROXY_MNAME_ENG;

                    //mouenroll.BuildingVdcMunCD = model.BUILDING_VDC_MUN_CD.ConvertToString();
                    mouenroll.BuildingWardNO = model.BUILDING_WARD_NO == "" ? null : model.BUILDING_WARD_NO.ToDecimal();//model.BUILDING_WARD_NO.ToDecimal();


                    mouenroll.NomineeFirstnamEng = model.NOMINEE_FNAME_ENG == "" ? null : model.NOMINEE_FNAME_ENG.ConvertToString();//model.NOMINEE_FNAME_ENG;
                    mouenroll.NomineeMiddlenamEng = model.NOMINEE_MNAME_ENG == "" ? null : model.NOMINEE_MNAME_ENG.ConvertToString();//model.NOMINEE_MNAME_ENG;
                    mouenroll.NomineeLastnamEng = model.NOMINEE_LNAME_ENG == "" ? null : model.NOMINEE_LNAME_ENG.ConvertToString();//model.NOMINEE_LNAME_ENG;

                    mouenroll.NomineeRelnTypCD = model.NOMINEE_RELATION_TYPE_CD;

                    mouenroll.SHAKSHI_FNAME_ENG = model.Sakshi_FName_ENG == "" ? null : model.Sakshi_FName_ENG.ConvertToString();//model.PROXY_FATHERS_FNAME_ENG;
                    mouenroll.SHAKSHI_MNAME_ENG = model.Sakshi_MName_ENG == "" ? null : model.Sakshi_MName_ENG.ConvertToString();//model.PROXY_FATHERS_MNAME_ENG;
                    mouenroll.SHAKSHI_LNAME_ENG = model.Sakshi_LName_ENG == "" ? null : model.Sakshi_LName_ENG.ConvertToString();//model.PROXY_FATHERS_LNAME_ENG;

                    mouenroll.CTZN_PIC_NAME = model.CTZN_PIC_NAME == "" ? null : model.CTZN_PIC_NAME.ConvertToString();


                    //mouenroll.isBulDesignFrmCatalog = "Y";
                    //mouenroll.bulDesignCatNo = 24545;
              
                    //mouenroll.IS_BUILDING_OWN_DESIGN = "Y";
                    //mouenroll.buildingPilarTyp = "2442";
                    //mouenroll.bulFloorRoofTyp = "2422";
                    //mouenroll.bulOtherTyp = "24524";

                    mouenroll.isBulDesignFrmCatalog = model.IS_BUILDING_DEG_FROM_CAT;
                    //mouenroll.bulDesignCatNo = model.BUILDING_DEG_CAT_NO;
                    mouenroll.bulDesignCatNo = model.BUILDING_DEG_CAT_NO == 0 ? null : model.BUILDING_DEG_CAT_NO.ToDecimal();

                    mouenroll.IS_BUILDING_OWN_DESIGN = model.IS_BUILDING_OWN_DESIGN;
                    //mouenroll.buildingPilarTyp = model.BUILDING_PILER_TYPE;
                    mouenroll.buildingPilarTyp = model.BUILDING_PILER_TYPE == "" ? null : model.BUILDING_PILER_TYPE;

                    //mouenroll.bulFloorRoofTyp = model.BUILDING_FLOOR_ROOF_TYPE;
                    mouenroll.bulFloorRoofTyp = model.BUILDING_FLOOR_ROOF_TYPE == "" ? null : model.BUILDING_FLOOR_ROOF_TYPE;

                    mouenroll.bulOtherTyp = model.BUILDING_OTHER_DEG;

                    mouenroll.bulOtherTyp = model.BUILDING_OTHER_DEG;
                    mouenroll.MapapprovedNo = model.MAP_APROVED_NO;
















                    ////mouenroll.EnrollmntId = model.ENROLLMENT_ID == "" ? null : model.ENROLLMENT_ID.ToDecimal();//model.ENROLLMENT_ID.ToDecimal();
                    //mouenroll.EnrollmentMouDay = model.ENROLLMENT_MOU_DAY.ToString() == "" ? null : model.ENROLLMENT_MOU_DAY.ToDecimal();
                    //mouenroll.EnrollmentMouMnth = model.ENROLLMENT_MOU_MONTH.ToString() == "" ? null : model.ENROLLMENT_MOU_MONTH.ToDecimal();
                    //mouenroll.EnrollmentMouYr = model.ENROLLMENT_MOU_YEAR.ToString() == "" ? null : model.ENROLLMENT_MOU_YEAR.ToDecimal();
                    //mouenroll.EnrollmentMouDt = model.ENROLLMENT_MOU_DT== "" ? null : model.ENROLLMENT_MOU_DT.ConvertToString();

                    //mouenroll.EnrollmentMouDayLoc = model.ENROLLMENT_MOU_DAY_LOC == "" ? null : model.ENROLLMENT_MOU_DAY_LOC.ConvertToString();
                    //mouenroll.EnrollmentMouMnthLoc = model.ENROLLMENT_MOU_MONTH_LOC == "" ? null : model.ENROLLMENT_MOU_MONTH_LOC.ConvertToString();
                    //mouenroll.EnrollmentMouYrLoc = model.ENROLLMENT_MOU_YEAR_LOC == "" ? null : model.ENROLLMENT_MOU_YEAR_LOC.ConvertToString();
                    //mouenroll.EnrollmentMouDtLoc = model.ENROLLMENT_MOU_DT_LOC == "" ? null : model.ENROLLMENT_MOU_DT_LOC.ConvertToString();
                    //mouenroll.HoMemID = model.HO_MEMBER_ID == "" ? null : model.HO_MEMBER_ID.ConvertToString();//model.HOUSE_Member_ID;
                    //mouenroll.HoHldID = model.HOUSEHOLD_ID == "" ? null : model.HOUSEHOLD_ID.ConvertToString();//model.HOUSEHOLD_ID;

                    //mouenroll.HOHldDefCD = model.HOUSEHOLD_DEFINED_CD == "" ? null : model.HOUSEHOLD_DEFINED_CD.ConvertToString();//model.HOUSEHOLD_DEFINED_CD;
                    //mouenroll.MemID = model.MEMBER_ID == "" ? null : model.MEMBER_ID.ConvertToString();//model.MEMBER_ID;
                    //mouenroll.BeneficiaryTypCD = model.BENEFICIARY_TYPE_CD == "" ? null : model.BENEFICIARY_TYPE_CD.ConvertToString();//model.BENEFICIARY_TYPE_CD;

                    //mouenroll.BeneficiaryFullNameEng = model.BENEFICIARY_FULLNAME_ENG == "" ? null : model.BENEFICIARY_FULLNAME_ENG.ConvertToString();//model.BENEFICIARY_FULLNAME_ENG;

                    //mouenroll.BeneficiaryFullNameLoc = model.BENEFICIARY_FULLNAME_LOC == "" ? null : model.BENEFICIARY_FULLNAME_LOC.ConvertToString();//model.BENEFICIARY_FULLNAME_LOC;

                  
                    //mouenroll.isBeneficiaryMigrated = model.IsMigrated == "" ? null : model.IsMigrated.ConvertToString();//model.IsMigrated;

                    //mouenroll.RegnStateCD = model.REGION_STATE_CD.ToString() == "" ? null : model.REGION_STATE_CD.ToDecimal();//model.REGION_STATE_CD;
                    //mouenroll.ZoneCD = model.ZONE_CD.ToString() == "" ? null : model.ZONE_CD.ToDecimal();//model.ZONE_CD;


                    //mouenroll.EnumArea = model.ENUMERATION_AREA == "" ? null : model.ENUMERATION_AREA.ConvertToString();//model.ENUMERATION_AREA;

                    //mouenroll.AreaLOC = model.AREA_LOC == "" ? null : model.AREA_LOC.ConvertToString();//model.AREA_LOC;

                    //mouenroll.FATHER_RELATION_TYPE_CD = model.FATHER_RELATION_TYPE_CD;
                    //mouenroll.FatherMemID = model.FATHER_MEMBER_ID == "" ? null : model.FATHER_MEMBER_ID.ConvertToString();//model.AREA_LOC;

                  


                  
                    ////mouenroll.VdcMunCD = 11;
                    ////mouenroll.WardNO = model.WARD_NO == "" ? null : model.WARD_NO.ToDecimal();//model.WARD_NO.ToDecimal();
                   
                    ////mouenroll.NRA_DEFINED_CD = model.NRA_DEFINED_CD == "" ? null : model.NRA_DEFINED_CD.ConvertToString();

                   
                 
                    ////mouenroll.DistrictCD = 13;
                    ////mouenroll.VdcMunCD = 11;

                    //mouenroll.FatherFirstnameEng = model.FATHER_FNAME_ENG == "" ? null : model.FATHER_FNAME_ENG.ConvertToString();//model.FATHER_FNAME_ENG;
                    //mouenroll.FatherMiddlenameEng = model.FATHER_MNAME_ENG == "" ? null : model.FATHER_MNAME_ENG.ConvertToString();//model.FATHER_MNAME_ENG;
                    //mouenroll.FatherLastnameEng = model.FATHER_LNAME_ENG == "" ? null : model.FATHER_LNAME_ENG.ConvertToString();//model.FATHER_LNAME_ENG;
                    //mouenroll.FatherFullNameEng = model.FATHER_FullNAME_ENG == "" ? null : model.FATHER_FullNAME_ENG.ConvertToString();//model.FATHER_FullNAME_ENG;
                    //mouenroll.FatherFirstnameLoc = model.FATHER_FNAME_LOC == "" ? null : model.FATHER_FNAME_LOC.ConvertToString();//model.FATHER_FNAME_LOC;
                    //mouenroll.FatherMiddlenameLoc = model.FATHER_MNAME_LOC == "" ? null : model.FATHER_MNAME_LOC.ConvertToString();//model.FATHER_MNAME_LOC;
                    //mouenroll.FatherLastnameLoc = model.FATHER_LNAME_LOC == "" ? null : model.FATHER_LNAME_LOC.ConvertToString();//model.FATHER_LNAME_LOC;
                    //mouenroll.FatherFullNameLoc = model.FATHER_FullNAME_LOC == "" ? null : model.FATHER_FullNAME_LOC.ConvertToString();//model.FATHER_FullNAME_LOC;
                    //mouenroll.GFATHER_RELATION_TYPE_CD = model.GFATHER_RELATION_TYPE_CD;
                    //mouenroll.GFatherMemID = model.GFATHER_MEMBER_ID == "" ? null : model.GFATHER_MEMBER_ID.ConvertToString();//model.GFATHER_MEMBER_ID;

                    //mouenroll.GFatherFirstnameEng = model.GFATHER_FNAME_ENG == "" ? null : model.GFATHER_FNAME_ENG.ConvertToString();//model.GFATHER_FNAME_ENG;
                    //mouenroll.GFatherMiddlenameEng = model.GFATHER_MNAME_ENG == "" ? null : model.GFATHER_MNAME_ENG.ConvertToString();//model.GFATHER_MNAME_ENG;
                    //mouenroll.GFatherLastnameEng = model.GFATHER_LNAME_ENG == "" ? null : model.GFATHER_LNAME_ENG.ConvertToString();//model.GFATHER_LNAME_ENG;
                    //mouenroll.GFatherFullNameEng = model.GFATHER_FullNAME_ENG == "" ? null : model.GFATHER_FullNAME_ENG.ConvertToString();//model.GFATHER_FullNAME_ENG;
                    //mouenroll.GFatherFirstnameLoc = model.GFATHER_FNAME_LOC == "" ? null : model.GFATHER_FNAME_LOC.ConvertToString();//model.GFATHER_FNAME_LOC;
                    //mouenroll.GFatherMiddlenameLoc = model.GFATHER_MNAME_LOC == "" ? null : model.GFATHER_MNAME_LOC.ConvertToString();//model.GFATHER_MNAME_LOC;
                    //mouenroll.GFatherLastnameLoc = model.GFATHER_LNAME_LOC == "" ? null : model.GFATHER_LNAME_LOC.ConvertToString();//model.GFATHER_LNAME_LOC;
                    //mouenroll.GFatherFullNameLoc = model.GFATHER_FullNAME_LOC == "" ? null : model.GFATHER_FullNAME_LOC.ConvertToString();//model.GFATHER_FullNAME_LOC;


                    mouenroll.IdentificationTypeCD = model.IDENTIFICATION_TYPE_CD;
                    mouenroll.IdentificationDOC = model.IDENTIFICATION_DOCUMENT;





                    //mouenroll.IdentificationIssueDay = model.IDENTIFICATION_ISSUE_DAY.ToString() == "" ? null : model.ENROLLMENT_MOU_MONTH.ToDecimal();
                    //mouenroll.IdentificationIssueMnth = model.IDENTIFICATION_ISSUE_MONTH.ToString()== "" ? null : model.ENROLLMENT_MOU_YEAR.ToDecimal();
                    //mouenroll.IdentificationIssueYr = model.IDENTIFICATION_ISSUE_YEAR.ToString()== "" ? null : model.ENROLLMENT_MOU_DT.ToDecimal();

                    //mouenroll.IdentificationIssueDay = model.IDENTIFICATION_ISS_DAY_LOC == "" ? null : model.IDENTIFICATION_ISS_DAY_LOC.ToDecimal();
                    //mouenroll.IdentificationIssueMnth = model.IDENTIFICATION_ISS_MNTH_LOC == "" ? null : model.IDENTIFICATION_ISS_MNTH_LOC.ToDecimal();
                    //mouenroll.IdentificationIssueYr = model.IDENTIFICATION_ISS_YEAR_LOC == "" ? null : model.IDENTIFICATION_ISS_YEAR_LOC.ToDecimal();


                    //mouenroll.BuildingKittaNO = model.BUILDING_KITTA_NUMBER == "" ? null : model.BUILDING_KITTA_NUMBER.ConvertToString();//model.BUILDING_KITTA_NUMBER;
                    //mouenroll.BuildingDistCD = model.BUILDING_DISTRICT_CD;

                    //mouenroll.BuildingVdcMunCD = model.BUILDING_VDC_MUN_CD.ToDecimal();
                    //mouenroll.BuildingWardNO = model.BUILDING_WARD_NO == "" ? null : model.BUILDING_WARD_NO.ToDecimal();//model.BUILDING_WARD_NO.ToDecimal();

                    //mouenroll.BuildingAreaLOC = model.BUILDING_AREA_LOC == "" ? null : model.BUILDING_AREA_LOC.ConvertToString();//model.BUILDING_AREA_LOC;

                    //mouenroll.isBulDesignFrmCatalog = model.IS_BUILDING_DESIGN_FROM_CATALOG == "" ? null : model.IS_BUILDING_DESIGN_FROM_CATALOG.ConvertToString();//model.IS_BUILDING_DESIGN_FROM_CATALOG;
                    //mouenroll.bulDesignCatNo = model.BUILDING_DESIGN_CATALOG_CD.ToString() == "" ? null : model.BUILDING_DESIGN_CATALOG_CD.ToDecimal();//model.IS_BUILDING_DESIGN_FROM_CATALOG;
                    //mouenroll.IS_BUILDING_TOBE_CONSTRUCTED = model.TYPE_OF_HOUSE_TO_BE_CONSTRUCTED == "" ? null : model.TYPE_OF_HOUSE_TO_BE_CONSTRUCTED.ConvertToString();//model.TYPE_OF_HOUSE_TO_BE_CONSTRUCTED;

                    //mouenroll.IS_BUILDING_OWN_DESIGN = model.BUILDING_HAVE_OWN_DESIGN == "" ? null : model.BUILDING_HAVE_OWN_DESIGN.ConvertToString();//model.BUILDING_HAVE_OWN_DESIGN;
                    //mouenroll.buildingPilarTyp = model.BUILDING_WALL_OR_PILLAR_TYPE_NO == "" ? null : model.BUILDING_WALL_OR_PILLAR_TYPE_NO.ConvertToString();//model.BUILDING_WALL_OR_PILLAR_TYPE_NO;
                    //mouenroll.bulFloorRoofTyp = model.BUILDING_FLOOR_OR_ROOF_TYPE_NO == "" ? null : model.BUILDING_FLOOR_OR_ROOF_TYPE_NO.ConvertToString();//model.BUILDING_FLOOR_OR_ROOF_TYPE_NO;
                    //mouenroll.bulOtherTyp = model.BUILDING_DESIGN_OTHER == "" ? null : model.BUILDING_DESIGN_OTHER.ConvertToString();//model.BUILDING_DESIGN_OTHER;
                    
                    
                    
                    //mouenroll.FATHER_INLAW_FNAME_ENG = model.FinlawFnameEng == "" ? null : model.FinlawFnameEng.ConvertToString();
                    //mouenroll.FATHER_INLAW_FNAME_LOC = model.FinlawFnameLoc == "" ? null : model.FinlawFnameLoc.ConvertToString();
                    //mouenroll.FATHER_INLAW_MNAME_ENG = model.FinlawMnameEng == "" ? null : model.FinlawMnameEng.ConvertToString();
                    //mouenroll.FATHER_INLAW_MNAME_LOC = model.FinlawMnameLoc == "" ? null : model.FinlawMnameLoc.ConvertToString();
                    //mouenroll.FATHER_INLAW_LNAME_ENG = model.FinlawLnameEng == "" ? null : model.FinlawLnameEng.ConvertToString();
                    //mouenroll.FATHER_INLAW_LNAME_LOC = model.FinlawLnameLoc == "" ? null : model.FinlawLnameLoc.ConvertToString();
                    //mouenroll.FATHER_INLAW_FULLNAME_ENG = model.FinLawFullNameEng == "" ? null : model.FinLawFullNameEng.ConvertToString();
                    //mouenroll.FATHER_INLAW_FULLNAME_LOC = model.FinLawFullNameLoc == "" ? null : model.FinLawFullNameLoc.ConvertToString();
                    //mouenroll.SPOUSE_MEMBER_ID = model.husbandmemberid == "" ? null : model.husbandmemberid.ConvertToString();
                    //mouenroll.SPOUSE_FIRST_NAME_ENG = model.husbandfnameeng == "" ? null : model.husbandfnameeng.ConvertToString();
                    //mouenroll.SPOUSE_FIRST_NAME_LOC = model.husbandfnameloc == "" ? null : model.husbandfnameloc.ConvertToString();
                    //mouenroll.SPOUSE_MIDDLE_NAME_ENG = model.husbandMnameeng == "" ? null : model.husbandMnameeng.ConvertToString();
                    //mouenroll.SPOUSE_MIDDLE_NAME_LOC = model.husbandMnameloc == "" ? null : model.husbandMnameloc.ConvertToString();
                    //mouenroll.SPOUSE_LAST_NAME_ENG = model.husbandLnameeng == "" ? null : model.husbandLnameeng.ConvertToString();
                    //mouenroll.SPOUSE_LAST_NAME_LOC = model.husbandLnameloc == "" ? null : model.husbandLnameloc.ConvertToString();
                    //mouenroll.SPOUSE_FULL_NAME_ENG = model.husbandFullnameEng == "" ? null : model.husbandFullnameEng.ConvertToString();
                    //mouenroll.SPOUSE_FULL_NAME_LOC = model.husbandFullnameLoc == "" ? null : model.husbandFullnameLoc.ConvertToString();
                    //mouenroll.isBeneficiaryMigrated = model.IsMigrated == "" ? null : model.IsMigrated.ConvertToString();//model.IsMigrated;

                    //if (model.migrationdate == null)
                    //{
                    //    mouenroll.benfMigrationDate = null;
                    //}
                    //else
                    //{
                    //    mouenroll.benfMigrationDate = Convert.ToDateTime(model.migrationdate).ToString("MM-dd-yyyy");
                    //}
                    //mouenroll.benfMigrationDate = model.migrationdate.ToString() == "" ? null : model.migrationdate.ToString("dd-MM-yyyy");//model.migrationdate;
                    //mouenroll.benfMigratnDateLoc = model.migrationdateloc == "" ? null : model.migrationdateloc.ConvertToString();//model.migrationdateloc;

                    //mouenroll.ISMANJURINAMA_AVAIL = model.IS_MANJURINAMA_AVAILABLE == "" ? null : model.IS_MANJURINAMA_AVAILABLE.ConvertToString();//model.IS_MANJURINAMA_AVAILABLE;

                    //mouenroll.MANJURINAMA_FULLNAME_LOC = model.PROXY_FULLNAME_LOC == "" ? null : model.PROXY_FULLNAME_LOC.ConvertToString();//model.PROXY_FULLNAME_LOC;
                                      //mouenroll.MANJURINAMA_AREA_LOC = model.PROXY_AREA_LOC == "" ? null : model.PROXY_AREA_LOC.ConvertToString();//model.PROXY_AREA_LOC;
                    //mouenroll.MANJURINAMA_IDENTITY_TYPE_CD = model.PROXY_IDENTIFICATION_TYPE_CD;
                    //mouenroll.MANJURINAMA_IDENTIFICATION_ISSUE_DIS_CD = model.PROXY_IDENTIFICATION_ISSUE_DIS_CD;

                    ////mouenroll.manjurinama_identification_issue_dt = model.proxy_identification_issue_dt.tostring() == "" ? null : model.proxy_identification_issue_dt.tostring("dd-mm-yyyy");//model.proxy_identification_issue_dt;
                    //mouenroll.manjurinama_identification_iss_dt_loc = model.proxy_identification_iss_dt_loc == "" ? null : model.proxy_identification_iss_dt_loc.converttostring();//model.proxy_identification_iss_dt_loc;
                    //if (model.PROXY_BIRTH_DT == null)
                    //{
                    //    mouenroll.MANJURINAMA_BIRTH_DT = null;
                    //}
                    //else
                    //{
                    //    mouenroll.MANJURINAMA_BIRTH_DT = Convert.ToDateTime(model.PROXY_BIRTH_DT).ToString("MM-dd-yyyy");
                    //}

                   
                    //mouenroll.MANJURINAMA_FATHER_FULLNAME_ENG = model.PROXY_FATHERSNAME_ENG == "" ? null : model.PROXY_FATHERSNAME_ENG.ConvertToString();//model.PROXY_FATHERSNAME_ENG;
                    //mouenroll.MANJURINAMA_FATHER_FULLNAME_LOC = model.PROXY_FATHERSNAME_LOC == "" ? null : model.PROXY_FATHERSNAME_LOC.ConvertToString();//model.PROXY_FATHERSNAME_LOC;

                    //mouenroll.BankBrnchCD = model.BANK_BRANCH_CD == "" ? null : model.BANK_BRANCH_CD.ToDecimal();//model.BANK_BRANCH_CD;

                    // mouenroll.BankAccTypCD = model.BANK_ACC_TYPE_CD == "" ? null : model.BANK_ACC_TYPE_CD.ToDecimal();//model.BANK_ACC_TYPE_CD;



                    
                    // mouenroll.isBulDesignFrmCatalog = model.IS_BUILDING_DESIGN_FROM_CATALOG == "" ? null : model.IS_BUILDING_DESIGN_FROM_CATALOG.ConvertToString();//model.IS_BUILDING_DESIGN_FROM_CATALOG;
                    //mouenroll.bulDesignCatNo = model.BUILDING_DESIGN_CATALOG_CD == "" ? null : model.BUILDING_DESIGN_CATALOG_CD.ToDecimal();//model.BUILDING_DESIGN_CATALOG_CD.ToDecimal();
                    
                    // mouenroll.NomineeFirstnamEng = model.NOMINEE_FNAME_ENG == "" ? null : model.NOMINEE_FNAME_ENG.ConvertToString();//model.NOMINEE_FNAME_ENG;
                    //mouenroll.NomineeMiddlenamEng = model.NOMINEE_MNAME_ENG == "" ? null : model.NOMINEE_MNAME_ENG.ConvertToString();//model.NOMINEE_MNAME_ENG;
                    // mouenroll.NomineeLastnamEng = model.NOMINEE_LNAME_ENG == "" ? null : model.NOMINEE_LNAME_ENG.ConvertToString();//model.NOMINEE_LNAME_ENG;
                    //mouenroll.NomineeFullnameEng = model.NOMINEE_FULLNAME_ENG == "" ? null : model.NOMINEE_FULLNAME_ENG.ConvertToString();//model.NOMINEE_FULLNAME_ENG;
                    //mouenroll.NomineeFirstnamLoc = model.NOMINEE_FNAME_LOC == "" ? null : model.NOMINEE_FNAME_LOC.ConvertToString();//model.NOMINEE_FNAME_LOC;
                    //mouenroll.NomineeMiddlenamLoc = model.NOMINEE_MNAME_LOC == "" ? null : model.NOMINEE_MNAME_LOC.ConvertToString();//model.NOMINEE_MNAME_LOC;
                    // mouenroll.NomineeLastnamLoc = model.NOMINEE_LNAME_LOC == "" ? null : model.NOMINEE_LNAME_LOC.ConvertToString();//model.NOMINEE_LNAME_LOC;
                    // mouenroll.NomineeFullnameLoc = model.NOMINEE_FULLNAME_LOC == "" ? null : model.NOMINEE_FULLNAME_LOC.ConvertToString();//model.NOMINEE_FULLNAME_LOC;
                    // mouenroll.NomineeRelnTypCD = model.NOMINEE_RELATION_TYPE_CD;
                    //  mouenroll.EmployCD = model.EMPLOYEE_CD == "" ? null : model.EMPLOYEE_CD.ConvertToString();//model.EMPLOYEE_CD;
                    //  mouenroll.OfficeCD = model.OFFICE_CD == "" ? null : model.OFFICE_CD.ConvertToString();// model.OFFICE_CD;
                    //mouenroll.Remark = model.REMARKS == "" ? null : model.REMARKS.ConvertToString();//model.REMARKS;
                    //mouenroll.RemarkLoc = model.REMARKS_LOC == "" ? null : model.REMARKS_LOC.ConvertToString();//model.REMARKS_LOC;
                    //// mouenroll.BUILDING_STRUCTURE_NO = model.BUILDING_STRUCTURE_NO == "" ? null : model.BUILDING_STRUCTURE_NO.ConvertToString();
                    mouenroll.ApproveD = "Y";
                    //mouenroll.IsPaymentReceiverChanged = model.IS_PAYMENT_RECEIVER_CHANGED;
                    mouenroll.UpdatedBY = SessionCheck.getSessionUsername();
                    mouenroll.EnteredBY = SessionCheck.getSessionUsername();
                    mouenroll.EnteredDT = (DateTime.Now).ToDateTime("MM-dd-yyyy");// DateTime.Now.ToString();
                    if (model.ModeType == "I")
                    {
                        mouenroll.Mode = "I";
                    }
                    else if (model.ModeType == "U")
                    {
                        mouenroll.Mode = "U";
                    }
                    //mouenroll.Mode = model.ModeType;
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    service.Begin();
                      qr = service.SubmitChanges(mouenroll, true);

                    //string[] strdoc = model.Docs.Split(',');
                    //string[] strdocType = model.DocType.Split(',');
                    //int index = 0;
                    //foreach(string s in strdoc)
                    //{
                    //    MisHhDocDtlInfo mouDocument = new MisHhDocDtlInfo();
                    //    mouDocument.HOUSE_OWNER_ID = model.HOUSE_OWNER_ID;
                    //    mouDocument.HouseholdId = model.HOUSEHOLD_ID;
                    //    mouDocument.BUILDING_STRUCTURE_NO = mouenroll.BUILDING_STRUCTURE_NO;
                    //    mouDocument.DocTypeCd = strdocType[index].ToDecimal();
                    //    mouDocument.DocId = s;
                    //    mouDocument.ApprovedBy = SessionCheck.getSessionUsername();
                    //    mouDocument.ApprovedDt = DateTime.Now.ToString("MM-dd-yyyy");
                    //    mouDocument.UpdatedBy = SessionCheck.getSessionUsername();
                    //    mouDocument.UpdatedDt = DateTime.Now.ToString("MM-dd-yyyy");
                    //    mouDocument.EnteredBy = SessionCheck.getSessionUsername();
                    //    mouDocument.EnteredDt = DateTime.Now.ToString("MM-dd-yyyy");
                    //    mouDocument.BATCH_ID = model.TARGET_BATCH_ID;
                    //    mouDocument.Mode = "I"; //model.ModeType; //"I";
                    //    QueryResult qr1 = service.SubmitChanges(mouDocument, true);
                    //    index++;
                    //}                   

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
                if (qr != null)
                {
                    result = qr.IsSuccess;
                }


            }
            return result;
        }


        //public void UpdateEnrollmentAddDetails(EnrollmentAdd model)
        //{
        //    ENROLLMENT_MOU enroll = new ENROLLMENT_MOU();
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        try
        //        {
        //            enroll.EnrollmntId = model.ENROLLMENT_ID.ConvertToString();
        //            enroll.TargetingId = model.TARGETING_ID.ConvertToString();
        //            enroll.HouseOwnerID = model.HOUSE_OWNER_ID;
        //            enroll.TargtBatchId = model.TARGET_BATCH_ID.ConvertToString();

        //            //enroll.BUILDING_STRUCTURE_NO = model.BUILDING_STRUCTURE_NO;

        //            enroll.DistrictCD = model.DISTRICT_CD.ToDecimal();
        //            enroll.VdcMunCD = model.VDC_MUN_CD.ToDecimal();
        //           // enroll.FIRST_NAME_ENG = model.FIRST_NAME_ENG;
        //           // enroll.MIDDLE_NAME_ENG = model.MIDDLE_NAME_ENG;
        //            //enroll.LAST_NAME_ENG = model.LAST_NAME_ENG;
        //           // enroll.MEMBER_PHOTO_PATH = model.MEMBER_PHOTO_PATH;

        //            enroll.BeneficiaryFullNameEng = model.BENEFICIARY_FULLNAME_ENG;
        //            enroll.FatherFullNameEng = model.FATHER_FullNAME_ENG;
        //            enroll.GFatherFullNameEng = model.GFATHER_FullNAME_ENG;
        //            enroll.BeneficiaryRelnTypCD = model.BENEFICIARY_RELATION_TYPE_CD.ToDecimal();

        //            //enroll.PROXY_FULLNAME_ENG = model.PROXY_FULLNAME_ENG;
        //            //enroll.PROXY_FATHERSNAME_ENG = model.PROXY_FATHERSNAME_ENG;
        //            //enroll.PROXY_GRANDFATHERNAME_ENG = model.PROXY_GRANDFATHERNAME_ENG;
        //            //enroll.PROXY_RELATION_TYPE_CD = model.PROXY_RELATION_TYPE_CD.ToDecimal();

        //            enroll.BankCD = model.BANK_CD.ToDecimal();
        //            enroll.BankBrnchCD = model.BANK_BRANCH_CD.ToDecimal();
        //            enroll.BankAccNO = model.BANK_ACC_NO;
        //            enroll.BankAccTypCD = model.BANK_ACC_TYPE_CD;
        //            enroll.IdentificationTypeCD = model.IDENTIFICATION_TYPE_CD;
        //            enroll.IdentificationNO = model.IDENTIFICATION_NO;
        //            enroll.IdentificationDOC = model.IDENTIFICATION_DOCUMENT;
        //            enroll.IdentificationIssDisCD = model.IDENTIFICATION_ISSUE_DIS_CD;
        //            enroll.UpdatedBY = SessionCheck.getSessionUsername();
        //            enroll.Mode = model.MODE;
        //            service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
        //            service.Begin();
        //            QueryResult qr = service.SubmitChanges(enroll, true);

        //        }
        //        catch (OracleException oe)
        //        {
        //            service.RollBack();
        //            ExceptionManager.AppendLog(oe);
        //        }
        //        catch (Exception ex)
        //        {
        //            service.RollBack();
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }

        //    }
        //}



        public EnrollmentAdd GetEnrollmentAdd()
        {
            throw new NotImplementedException();
        }
        public DataTable GetEnrollmentData(string Houseownerid, string PA)
        {
            DataTable dtt = new DataTable();
            using (ServiceFactory sf=new ServiceFactory())
            {
                try
                {
                    string cmd = "SELECT * FROM NHRS_ENROLLMENT_PA WHERE HOUSE_OWNER_ID='" + Houseownerid + "' AND NRA_DEFINED_CD='" + PA + "' ";
                    sf.Begin();
                    dtt = sf.GetDataTable(new
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

            }


            return dtt;
        }

        public DataTable GetPAPrintView(string Houseownerid, string PA)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
          
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_PA_PRINT_VIEW",
                    Houseownerid,
                    PA,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
}