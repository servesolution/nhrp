using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Services.Core
{
    public static class ConstantVariables
    {
        public const string EducationGrantSATID = "1";
        public const string EducationGrantPeriodTypeID = "1";
        public const string GeneralGroupID = "4";
        public const decimal HouseHoldHeadRelationTypeID = 1;
        public const String LocalCountryCode = "1";
    }

    public static class PermissionCode
    {
        public const string Edit = "1";
        public const string List = "2";
        public const string Add = "3";
        public const string Delete = "4";
        public const string Approve = "7";
        public const string Print = "10";
    }


    public static class HouseHoldHead
    {
        public const decimal HouseHoldHeadRelationID = 1;
    }

    public static class GeneralUser
    {
        public const string GeneralUserID = "4";
    }

    public static class MISLiteTables
    {
        public const string FRESH_HOUSEHOLD = "MISLITE_HOUSEHOLD_INFO_DUMP";
        public const string FRESH_HOUSEHOLD_MEMBER = "MISLITE_MEMBER_DUMP";
        public const string FRESH_HOUSEHOLD_FAMILY_DTL = "MISLITE_HH_FAMILY_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_MARRIAGE_DTL = "MISLITE_HH_MARRIAGE_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_WIDOW_DTL = "MISLITE_HH_WIDOW_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_DIVORCE_DTL = "MISLITE_HH_DIVORCE_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_DEATH_DTL = "MISLITE_HH_DEATH_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_SCHOOL_DTL = "MISLITE_HH_SCHOOL_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_ALLOWANCE_DTL = "MISLITE_HH_ALLOWANCE_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_DOCUMENT_DTL = "MISLITE_HH_DOC_DTL_DUMP";
        public const string FRESH_HOUSEHOLD_MEMBER_DOC_DTL = "MISLITE_MEMBER_DOC_DUMP";
    }

    public static class FileUploadValidation
    {
        public const int FileSize = 1024 * 1024;
        public const string SizeMessage = "Image Size should be smaller than 1MB !!";
        public const string FileFormat = "jpg";
        public const string FormatMessage = "File other than jpg is not allowed to upload !!";

    }

    public static class EncryptionConstant
    {
        public const string IMPORT_ENCRYPTION_KEY = "spinepal";
        public const string MASTER_ENCRYPTION_KEY = "spinepal";
        public const string USER_ENCRYPTION_KEY = "spinepal";
        public const string HDSP_ENCRYPTION_KEY = "spinepal";
        public const string SSNP_ENCRYPTION_KEY = "spinepal";
    }

    public static class RegistrationType
    {
        public const string Birth = "1";
        public const string Death = "2";
        public const string Divorce = "3";
        public const string Marriage = "4";
        public const string Migration = "5";
    }

    public static class MISMessage
    {
        public const string InsertSuccess = "Record inserted Successfully";
        public const string InsertFail = "Cannot insert record";
        public const string UpdateSucess = "Record updated Successfully";
        public const string UpdateFail = "Cannot update record";
        public const string DeleteSucess = "Record deleted Successfully";
        public const string DeleteFail = "Cannot delete record being used";
        public const string DuplicateCode = "Code already exists!!";
        public const string DuplicateRegCode = "Registration Code already exists!!";
        
    }
}
