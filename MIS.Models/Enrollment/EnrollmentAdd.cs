using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MIS.Models.Enrollment
{
    public class EnrollmentAdd

    {
        public String FISCAL_YR { get; set; }
        public String TARGET_BATCH_ID { get; set; }
        public String TARGETING_ID { get; set; }
        public String ENROLLMENT_ID { get; set; }
        public String NRA_DEFINED_CD { get; set; }
        public String MOU_ID { get; set; }
        public String DEFINED_MOU_ID { get; set; }
        public String SURVEY_NO { get; set; }
        public String HOUSE_ID { get; set; }
        public String NISSA_NO { get; set; }
        public Decimal? ENROLLMENT_MOU_DAY { get; set; }
        public Decimal? ENROLLMENT_MOU_MONTH { get; set; }
        public Decimal? ENROLLMENT_MOU_YEAR { get; set; }
        public String ENROLLMENT_MOU_DT { get; set; }
        public String ENROLLMENT_MOU_DAY_LOC { get; set; }

        public String ENROLLMENT_MOU_MONTH_LOC { get; set; }
        public String ENROLLMENT_MOU_YEAR_LOC { get; set; }
        public String ENROLLMENT_MOU_DT_LOC { get; set; }
        public String HOUSE_OWNER_ID { get; set; }
        public String NO_OF_HOUSE_OWNER { get; set; }
        public String HO_MEMBER_ID { get; set; }
        public String HOUSEHOLD_ID { get; set; }
        public String HOUSEHOLD_DEFINED_CD { get; set; }
        public String BENEFICIARY_TYPE_CD { get; set; }
        public String MEMBER_ID { get; set; }
        public String BENEFICIARY_FNAME_ENG { get; set; }
        public String BENEFICIARY_MNAME_ENG { get; set; }
        public String BENEFICIARY_LNAME_ENG { get; set; }
        public String BENEFICIARY_FULLNAME_ENG { get; set; }
        public String BENEFICIARY_FNAME_LOC { get; set; }
        public String BENEFICIARY_MNAME_LOC { get; set; }
        public String BENEFICIARY_LNAME_LOC { get; set; }
        public String BENEFICIARY_FULLNAME_LOC { get; set; }
        public Decimal? BENEFICIARY_RELATION_TYPE_CD { get; set; }
        public String Beneficiary_Photo { get; set; }

        public List<string> DOCUMENT { get; set; }

        public string DOCUMENTS { get; set; }


        public HttpPostedFileBase BFile { get; set; }


        public Decimal? REGION_STATE_CD { get; set; }
        public Decimal? ZONE_CD { get; set; }
        public Decimal? DISTRICT_CD { get; set; }

        public String VDC_MUN_CD { get; set; }
        public String WARD_NO { get; set; }
        public String ENUMERATION_AREA { get; set; }
        public String AREA { get; set; }
        public String AREA_ENG { get; set; }
        public String AREA_LOC { get; set; }
        public String FATHER_MEMBER_ID { get; set; }
        public String FATHER_FNAME_ENG { get; set; }
        public String FATHER_MNAME_ENG { get; set; }
        public String FATHER_LNAME_ENG { get; set; }
        public String FATHER_FullNAME_ENG { get; set; }
        public String FATHER_FNAME_LOC { get; set; }
        public String FATHER_MNAME_LOC { get; set; }
        public String FATHER_LNAME_LOC { get; set; }
        public String FATHER_FullNAME_LOC { get; set; }
        public Decimal? FATHER_RELATION_TYPE_CD { get; set; }
        public String GFATHER_MEMBER_ID { get; set; }
        public String GFATHER_FNAME_ENG { get; set; }
        public String GFATHER_MNAME_ENG { get; set; }
        public String GFATHER_LNAME_ENG { get; set; }
        public String GFATHER_FullNAME_ENG { get; set; }
        public String GFATHER_FNAME_LOC { get; set; }
        public String GFATHER_MNAME_LOC { get; set; }
        public String GFATHER_LNAME_LOC { get; set; }
        public String GFATHER_FullNAME_LOC { get; set; }
        public Decimal? GFATHER_RELATION_TYPE_CD { get; set; }
        public String PHONE_NO { get; set; }
        public String BIRTH_DT { get; set; }
        public String BIRTH_DT_LOC { get; set; }
        public Decimal? IDENTIFICATION_TYPE_CD { get; set; }
        public String IDENTIFICATION_NO { get; set; }
        public String BENEFICIARY_CTZ_NO { get; set; }
        public String IDENTIFICATION_DOCUMENT { get; set; }
        public Decimal? IDENTIFICATION_ISSUE_DIS_CD { get; set; }
        public String IDENTIFICATION_ISSUE_DIS { get; set; }
        public Decimal? IDENTIFICATION_ISSUE_DAY { get; set; }
        public Decimal? IDENTIFICATION_ISSUE_MONTH { get; set; }
        public Decimal? IDENTIFICATION_ISSUE_YEAR { get; set; }
        public String IDENTIFICATION_ISSUE_DT { get; set; }
        public String IDENTIFICATION_ISS_DAY_LOC { get; set; }
        public String IDENTIFICATION_ISS_MNTH_LOC { get; set; }
        public String IDENTIFICATION_ISS_YEAR_LOC { get; set; }
        public String IDENTIFICATION_ISS_DT_LOC { get; set; }
        public String ENUMERATOR_ID { get; set; }
        public String BUILDING_KITTA_NUMBER { get; set; }
        public String BUILDING_AREA { get; set; }
        public String BUILDING_DISTRICT { get; set; }
        public String BUILDING_VDC { get; set; }
        public Decimal? BUILDING_DISTRICT_CD { get; set; }
        public String BUILDING_VDC_MUN_CD { get; set; }
        public String BUILDING_WARD_NO { get; set; }
        public String BUILDING_AREA_ENG { get; set; }
        public String BUILDING_AREA_LOC { get; set; }
        public String TYPE_OF_HOUSE_TO_BE_CONSTRUCTED { get; set; }
        public String IS_BUILDING_DESIGN_FROM_CATALOG { get; set; }
        public String BUILDING_DESIGN_CATALOG_CD { get; set; }
        public String BUILDING_HAVE_OWN_DESIGN { get; set; }
        public String BUILDING_WALL_OR_PILLAR_TYPE_NO { get; set; }
        public String BUILDING_FLOOR_OR_ROOF_TYPE_NO { get; set; }
        public String BUILDING_DESIGN_OTHER { get; set; }
        public String NOMINEE_MEMBER_ID { get; set; }
        public String NOMINEE_FNAME_ENG { get; set; }
        public String NOMINEE_MNAME_ENG { get; set; }
        public String NOMINEE_LNAME_ENG { get; set; }
        public String NOMINEE_FULLNAME_ENG { get; set; }
        public String NOMINEE_FNAME_LOC { get; set; }
        public String NOMINEE_MNAME_LOC { get; set; }
        public String NOMINEE_LNAME_LOC { get; set; }
        public String NOMINEE_FULLNAME_LOC { get; set; }
        public String NOMINEE_RELATION { get; set; }
        public Decimal? NOMINEE_RELATION_TYPE_CD { get; set; }
        public String EMPLOYEE_CD { get; set; }
        public String OFFICE_CD { get; set; }
        public String REMARKS { get; set; }
        public String REMARKS_LOC { get; set; }
        public String BANK_CD { get; set; }
        public String BANK_BRANCH_CD { get; set; }
        public String BANK_ACC_NO { get; set; }

        public String ACC_HOLDER_FNAME_ENG { get; set; }
        public String ACC_HOLDER_MNAME_ENG { get; set; }
        public String ACC_HOLDER_LNAME_ENG { get; set; }
        public String BANK_ACC_TYPE_CD { get; set; }
        public String IS_PAYMENT_RECEIVER_CHANGED { get; set; }
        public String CHANGED_REASON_ENG { get; set; }
        public String CHANGED_REASON_LOC { get; set; }
        public String APPROVED { get; set; }
        public String APPROVED_BY { get; set; }
        public String APPROVED_DT { get; set; }
        public String APPROVED_DT_LOC { get; set; }
        public String UPDATED_BY { get; set; }
        public String UPDATED_DT { get; set; }
        public String UPDATED_DT_LOC { get; set; }
        public String ENTERED_BY { get; set; }
        public String ENTERED_DT_LOC { get; set; }
        public String ENTERED_DT { get; set; }
        public String PAYROLL_GENERATION_FLAG { get; set; }
        public String PAYROLL_BATCH_ID { get; set; }
        public String PAYROLL_DTL_ID { get; set; }
        public String IS_MANJURINAMA_AVAILABLE { get; set; }
        public String PROXY_MEMBER_ID { get; set; }
        public String PROXY_FNAME_ENG { get; set; }
        public String PROXY_MNAME_ENG { get; set; }
        public String PROXY_LNAME_ENG { get; set; }
        public String PROXY_FULLNAME_ENG { get; set; }
        public String PROXY_FNAME_LOC { get; set; }
        public String PROXY_MNAME_LOC { get; set; }
        public String PROXY_LNAME_LOC { get; set; }
        public String PROXY_FULLNAME_LOC { get; set; }
        public String PROXY_FATHERS_FNAME_ENG { get; set; }
        public String PROXY_FATHERS_MNAME_ENG { get; set; }
        public String PROXY_FATHERS_LNAME_ENG { get; set; }
        public String PROXY_FATHERSNAME_ENG { get; set; }
        public String PROXY_FATHERS_FNAME_LOC { get; set; }
        public String PROXY_FATHERS_MNAME_LOC { get; set; }
        public String PROXY_FATHERS_LNAME_LOC { get; set; }
        public String PROXY_FATHERSNAME_LOC { get; set; }
        public String PROXY_GFATHERS_FNAME_ENG { get; set; }
        public String PROXY_GFATHERS_MNAME_ENG { get; set; }
        public String PROXY_GFATHERS_LNAME_ENG { get; set; }
        public String PROXY_GRANDFATHERNAME_ENG { get; set; }
        public String PROXY_GFATHERS_FNAME_LOC { get; set; }
        public String PROXY_GFATHERS_MNAME_LOC { get; set; }
        public String PROXY_GFATHERS_LNAME_LOC { get; set; }
        public String PROXY_GRANDFATHERNAME_LOC { get; set; }
        public Decimal? PROXY_DISTRICT_CD { get; set; }
        public String PROXY_VDC_MUN_CD { get; set; }
        public String PROXY_WARD_NO { get; set; }
        public String PROXY_AREA_ENG { get; set; }
        public String PROXY_AREA_LOC { get; set; }
        public Decimal? PROXY_IDENTIFICATION_TYPE_CD { get; set; }
        public String PROXY_IDENTIFICATION_NO { get; set; }
        public Decimal? PROXY_IDENTIFICATION_ISSUE_DIS_CD { get; set; }
        public String PROXY_IDENTIFICATION_ISSUE_DT { get; set; }
        public String PROXY_IDENTIFICATION_ISS_DT_LOC { get; set; }
        public String PROXY_BIRTH_DT { get; set; }
        public String PROXY_BIRTH_DT_LOC { get; set; }
        public String PROXY_PHONE { get; set; }
        public String PROXY_RELATION { get; set; }
        public Decimal? PROXY_RELATION_TYPE_CD { get; set; }
       
        public String RELN_WID_BENEFICIARY { get; set; }
        public String NOMINEE_ADDRESS { get; set; }
        public String EMPLOYEE_ENG { get; set; }
        public String EMPLOYEE_LOC { get; set; }
        public Decimal? POSITION_CD { get; set; }
        public String POSITION_ENG { get; set; }
        public String POSITION_LOC { get; set; }
        public String OFFICE_ENG { get; set; }
        public String OFFICE_LOC { get; set; }
        public String OFFICE_ADDRESS_ENG { get; set; }
        public String OFICCE_ADDRESS_LOC { get; set; }
        public String OFFICE_DISTRICT_CD { get; set; }
        public String OFFICE_DISTRICT_NAME { get; set; }
        public String OFFICE_VDC_MUN_CD { get; set; }
        public String OFFICE_VDC_MUN_NAME { get; set; }
        public String OFFICE_WARD { get; set; }
        public String HOUSE_OWNER_FIRST_NAME_ENG { get; set; }
        public String HOUSE_OWNER_MIDDLE_NAME_ENG { get; set; }
        public String HOUSE_OWNER_LAST_NAME_ENG { get; set; }
        public String HOUSE_OWNER_NAME_ENG { get; set; }
        public String HOUSE_OWNER_FIRST_NAME_LOC { get; set; }
        public String HOUSE_OWNER_MIDDLE_NAME_LOC { get; set; }
        public String HOUSE_OWNER_LAST_NAME_LOC { get; set; }
        public String HOUSE_OWNER_NAME_LOC { get; set; }
        public String DISTRICT_NAME { get; set; }
        public String VDC_MUN_NAME { get; set; }
        public String IsMigrated { get; set; }
        public String ddlmigration { get; set; }
        public String ddlmarriage { get; set; }
        public String checkmarried { get; set; }
        public String checkunmarried { get; set; }
        public String FinlawFnameEng { get; set; }
        public String FinlawFnameLoc { get; set; }
        public String FinlawMnameEng { get; set; }
        public String FinlawMnameLoc { get; set; }
        public String FinlawLnameEng { get; set; }
        public String FinlawLnameLoc { get; set; }
        public String FinLawFullNameEng { get; set; }
        public String FinLawFullNameLoc { get; set; }
        public String husbandmemberid { get; set; }
        public String husbandfnameeng { get; set; }
        public String husbandfnameloc { get; set; }
        public String husbandMnameeng { get; set; }
        public String husbandMnameloc { get; set; }
        public String husbandLnameeng { get; set; }
        public String husbandLnameloc { get; set; }
        public String husbandFullnameEng { get; set; }
        public String husbandFullnameLoc { get; set; }
        public Decimal? migrationno { get; set; }
        public String migrationdate { get; set; }
        public String migrationdateloc { get; set; }

        public String Sakshi_FName_ENG { get; set; }
        public String Sakshi_MName_ENG { get; set; }
        public String Sakshi_LName_ENG { get; set; }
        public String Sakshi_FName_LOC { get; set; }
        public String Sakshi_MName_LOC { get; set; }
        public String Sakshi_LName_LOC { get; set; }

        public String CTZN_PIC_NAME { get; set; }

        public String DocType { get; set; }

        public String Docs { get; set; }

        public String BUILDING_STRUCTURE_NO { get; set; }

        public String ModeType { get; set; }


        #region new added field

        public int BUILDING_DEG_CAT_NO { get; set; }
        public string BUILDING_PILER_TYPE { get; set; }
        public string BUILDING_FLOOR_ROOF_TYPE { get; set; }
        public string BUILDING_OTHER_DEG { get; set; }

        public string IS_BUILDING_OWN_DESIGN { get; set; }


        public string designedNo { get; set; }

        public HttpPostedFileBase[] files { get; set; } 

        public string IS_BUILDING_DEG_FROM_CAT { get; set; }
        public string MAP_APROVED_NO { get; set; }
        #endregion


    }
}
