using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Web.Routing;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Configuration;
using System.Globalization;
using MIS.Models.Core;
using EntityFramework;
using ExceptionHandler;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MIS.Services.Core;
using MIS.Services.AuditTrail;

namespace MIS.Services.Core
{    
    public static class CommonVariables
    {
        public static string CurrencyCode { get { return HttpContext.Current.Session["CurrencyCode"].ConvertToString(); } set { HttpContext.Current.Session["CurrencyCode"] = value; } }
        public static string HouseholdId { get { return HttpContext.Current.Session["HouseholdId"].ConvertToString(); } set { HttpContext.Current.Session["HouseholdId"] = value; } }
        public static string HosueholdDefinedCode { get { return HttpContext.Current.Session["HouseholdDefincedId"].ConvertToString(); } set { HttpContext.Current.Session["HouseholdDefincedId"] = value; } }
        public static string MemberID { get { return HttpContext.Current.Session["MemberId"].ConvertToString(); } set { HttpContext.Current.Session["MemberId"] = value; } }
        public static string MembershipID { get { return HttpContext.Current.Session["MembershipID"].ConvertToString(); } set { HttpContext.Current.Session["MembershipID"] = value; } }
        public static string MemberDefinedCode { get { return HttpContext.Current.Session["MemberDefinedCode"].ConvertToString(); } set { HttpContext.Current.Session["MemberDefinedCode"] = value; } }
        public static string MPISNo { get { return HttpContext.Current.Session["MPISNo"].ConvertToString(); } set { HttpContext.Current.Session["MPISNo"] = value; } }
        public static string PMTSNo { get { return HttpContext.Current.Session["PMTSNo"].ConvertToString(); } set { HttpContext.Current.Session["PMTSNo"] = value; } }
        public static string RptSessionId { get { return HttpContext.Current.Session["RptSessionId"].ConvertToString(); } set { HttpContext.Current.Session["RptSessionId"] = value; } }
        public static string RptGenderFlag { get { return HttpContext.Current.Session["RptGenderFlag"].ConvertToString(); } set { HttpContext.Current.Session["RptGenderFlag"] = value; } }
        public static string UserCode { get { return HttpContext.Current.Session["UserCode"].ConvertToString(); } set { HttpContext.Current.Session["UserCode"] = value; } }
        public static string UserName { get { return HttpContext.Current.Session["UserName"].ConvertToString(); } set { HttpContext.Current.Session["UserName"] = value; } }
        public static string EmpCode { get { return HttpContext.Current.Session["EmpCode"].ConvertToString(); } set { HttpContext.Current.Session["EmpCode"] = value; } }
        public static string ReturnFromApproveDelete { get { return HttpContext.Current.Session["ReturnFromApproveDelete"].ConvertToString(); } set { HttpContext.Current.Session["ReturnFromApproveDelete"] = value; } }
        public static string currentController { get { return HttpContext.Current.Session["currentController"].ConvertToString(); } set { HttpContext.Current.Session["currentController"] = value; } }
        public static string currentAction { get { return HttpContext.Current.Session["currentAction"].ConvertToString(); } set { HttpContext.Current.Session["currentAction"] = value; } }

        //For Feed Forward Pending
        public static string SocialAssistanceType { get { return HttpContext.Current.Session["SocialAssistanceType"].ConvertToString(); } set { HttpContext.Current.Session["SocialAssistanceType"] = value; } }
        public static string ServiceProvider { get { return HttpContext.Current.Session["ServiceProvider"].ConvertToString(); } set { HttpContext.Current.Session["ServiceProvider"] = value; } }
        public static string FiscalYear { get { return HttpContext.Current.Session["FiscalYear"].ConvertToString(); } set { HttpContext.Current.Session["FiscalYear"] = value; } }
        public static string BatchIdFrom { get { return HttpContext.Current.Session["BatchIdFrom"].ConvertToString(); } set { HttpContext.Current.Session["BatchIdFrom"] = value; } }
        public static string BatchIdTo { get { return HttpContext.Current.Session["BatchIdTo"].ConvertToString(); } set { HttpContext.Current.Session["BatchIdTo"] = value; } }
        public static string BatchID { get { return HttpContext.Current.Session["BatchID"].ConvertToString(); } set { HttpContext.Current.Session["BatchID"] = value; } }
        public static string BankAccountType { get { return HttpContext.Current.Session["BankAccountType"].ConvertToString(); } set { HttpContext.Current.Session["BankAccountType"] = value; } }
        public static string AccountNumber { get { return HttpContext.Current.Session["AccountNumber"].ConvertToString(); } set { HttpContext.Current.Session["AccountNumber"] = value; } }
        public static string TransactionDate { get { return HttpContext.Current.Session["TransactionDate"].ConvertToString(); } set { HttpContext.Current.Session["TransactionDate"] = value; } }
        public static string TransactionDateLoc { get { return HttpContext.Current.Session["TransactionDateLoc"].ConvertToString(); } set { HttpContext.Current.Session["TransactionDateLoc"] = value; } }
        public static string StartDTFrom = string.Empty;
        public static string EndDTTo = string.Empty;
        public static string SearchUserName
        {
            get { return HttpContext.Current.Session["SearchUserName"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchUserName"] = value; }
        }
        public static string START_DT_FROM
        {
            get { return StartDTFrom; }
            set { StartDTFrom = value; }
        }
        public static string END_DT_TO
        {
            get { return EndDTTo; }
            set { EndDTTo = value; }
        }
        public static string SearchLoginName
        {
            get { return HttpContext.Current.Session["SearchLoginName"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchLoginName"] = value; }
        }
        public static string SearchGroupCode
        {
            get { return HttpContext.Current.Session["SearchGroupCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchGroupCode"] = value; }
        }

        public static string SearchStartDate
        {
            get { return HttpContext.Current.Session["SearchStartDate"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchStartDate"] = value; }
        }
        public static string SearchEndDate
        {
            get { return HttpContext.Current.Session["SearchEndDate"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchEndDate"] = value; }
        }
        public static string SearchStartDateLoc
        {
            get { return HttpContext.Current.Session["SearchStartDateLoc"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchStartDateLoc"] = value; }
        }
        public static string SearchEndDateLoc
        {
            get { return HttpContext.Current.Session["SearchEndDateLoc"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchEndDateLoc"] = value; }
        }
        public static string SearchTableName
        {
            get { return HttpContext.Current.Session["SearchTableName"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchTableName"] = value; }
        }
        public static string SearchTableType
        {
            get { return HttpContext.Current.Session["SearchTableType"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchTableType"] = value; }
        }
        public static string SearchOperation
        {
            get { return HttpContext.Current.Session["SearchOperation"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchOperation"] = value; }
        }

        public static string LayerName { get { return HttpContext.Current.Session["LayerName"].ConvertToString(); } set { HttpContext.Current.Session["LayerName"] = value; } }

        //For Selecting Position in Toilet Type Tree
        public static string ToiletTypeCD { get { return HttpContext.Current.Session["ToiletTypeCD"].ConvertToString(); } set { HttpContext.Current.Session["ToiletTypeCD"] = value; } }

        //For Selecting Position in Source Tree
        public static string SourceCD { get { return HttpContext.Current.Session["SourceCD"].ConvertToString(); } set { HttpContext.Current.Session["SourceCD"] = value; } }

        //For Search Operation In Upload log 
        public static bool SearchFalg { get { return HttpContext.Current.Session["SearchFalg"].ToBoolean(); } set { HttpContext.Current.Session["SearchFalg"] = value; } }

        //For Selecting Position in Position Tree
        public static string PossitionCD { get { return HttpContext.Current.Session["PossitionCD"].ConvertToString(); } set { HttpContext.Current.Session["PossitionCD"] = value; } }
        public static string SelectedCode { get { return HttpContext.Current.Session["SelectedCode"].ConvertToString(); } set { HttpContext.Current.Session["SelectedCode"] = value; } }
        public static bool ApprovedCode { get { return HttpContext.Current.Session["ApprovedCode"].ToBoolean(); } set { HttpContext.Current.Session["ApprovedCode"] = value; } }

        //Poverty Calculation
        public static string BusinessRule { get { return HttpContext.Current.Session["BusinessRule"].ConvertToString(); } set { HttpContext.Current.Session["BusinessRule"] = value; } }
        public static string RuleCalculated { get { return HttpContext.Current.Session["RuleCalculated"].ConvertToString(); } set { HttpContext.Current.Session["RuleCalculated"] = value; } }

        public static string SearchDistrictDefinedCode { get { return HttpContext.Current.Session["SearchDistrictDefinedCode"].ConvertToString(); } set { HttpContext.Current.Session["SearchDistrictDefinedCode"] = value; } }

        //Poverty Ranking
        public static string RangeFrom { get { return HttpContext.Current.Session["RangeFrom"].ConvertToString(); } set { HttpContext.Current.Session["RangeFrom"] = value; } }
        public static string RangeTo { get { return HttpContext.Current.Session["RangeTo"].ConvertToString(); } set { HttpContext.Current.Session["RangeTo"] = value; } }
        public static string RecordDisplay { get { return HttpContext.Current.Session["RecordDisplay"].ConvertToString(); } set { HttpContext.Current.Session["RecordDisplay"] = value; } }
        public static string OrderBy { get { return HttpContext.Current.Session["OrderBy"].ConvertToString(); } set { HttpContext.Current.Session["OrderBy"] = value; } }

        public static string EnteredDateFromBS { get { return HttpContext.Current.Session["EnteredDateFromBS"].ConvertToString(); } set { HttpContext.Current.Session["EnteredDateFromBS"] = value; } }
        public static string EnteredDateFromAD { get { return HttpContext.Current.Session["EnteredDateFromAD"].ConvertToString(); } set { HttpContext.Current.Session["EnteredDateFromAD"] = value; } }
        public static string EnteredDateToBS { get { return HttpContext.Current.Session["EnteredDateToBS"].ConvertToString(); } set { HttpContext.Current.Session["EnteredDateToBS"] = value; } }
        public static string EnteredDateToAD { get { return HttpContext.Current.Session["EnteredDateToAD"].ConvertToString(); } set { HttpContext.Current.Session["EnteredDateToAD"] = value; } }

        public static string ApprovedDateFromBS { get { return HttpContext.Current.Session["ApprovedDateFromBS"].ConvertToString(); } set { HttpContext.Current.Session["ApprovedDateFromBS"] = value; } }
        public static string ApprovedDateFromAD { get { return HttpContext.Current.Session["ApprovedDateFromAD"].ConvertToString(); } set { HttpContext.Current.Session["ApprovedDateFromAD"] = value; } }
        public static string ApprovedDateToBS { get { return HttpContext.Current.Session["ApprovedDateToBS"].ConvertToString(); } set { HttpContext.Current.Session["ApprovedDateToBS"] = value; } }
        public static string ApprovedDateToAD { get { return HttpContext.Current.Session["ApprovedDateToAD"].ConvertToString(); } set { HttpContext.Current.Session["ApprovedDateToAD"] = value; } }
        //End of Poverty Calculation


        public static int SearchedItem
        {
            get { return HttpContext.Current.Session["SearchedItem"].ToInt32(); }
            set { HttpContext.Current.Session["SearchedItem"] = value; }
        }

        public static string SearchZoneCode
        {
            get { return HttpContext.Current.Session["SearchZoneCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchZoneCode"] = value; }
        }


        public static string SearchZoneDefCode
        {
            get { return HttpContext.Current.Session["SearchZoneDefCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchZoneDefCode"] = value; }
        }

        public static string SearchDistrictCode
        {
            get { return HttpContext.Current.Session["SearchDistrictCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchDistrictCode"] = value; }
        }

        public static string SearchSATCode
        {
            get { return HttpContext.Current.Session["SearchSATCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchSATCode"] = value; }
        }

        public static string SearchPeriodType
        {
            get { return HttpContext.Current.Session["SearchPeriodType"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchPeriodType"] = value; }
        }

        public static string SearchPeriodCount
        {
            get { return HttpContext.Current.Session["SearchPeriodCount"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchPeriodCount"] = value; }
        }

        public static string SearchInstallment
        {
            get { return HttpContext.Current.Session["SearchInstallment"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchInstallment"] = value; }
        }

        public static string SearchDistrictDefCode
        {
            get { return HttpContext.Current.Session["SearchDistrictDefCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchDistrictDefCode"] = value; }
        }


        public static string SearchVDCMunTypeCode
        {
            get { return HttpContext.Current.Session["SearchVDCMunTypeCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchVDCMunTypeCode"] = value; }
        }


        public static string SearchVDCMunName
        {
            get { return HttpContext.Current.Session["SearchVDCMunName"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchVDCMunName"] = value; }
        }


        public static string SearchVDCMunNameLoc
        {
            get { return HttpContext.Current.Session["SearchVDCMunNameLoc"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchVDCMunNameLoc"] = value; }
        }


        public static string SearchVDCMunCode
        {
            get { return HttpContext.Current.Session["SearchVDCMunCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchVDCMunCode"] = value; }
        }


        public static string SearchVDCMunDefCode
        {
            get { return HttpContext.Current.Session["SearchVDCMunDefCode"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchVDCMunDefCode"] = value; }
        }


        public static string SearchWard
        {
            get { return HttpContext.Current.Session["SearchWard"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchWard"] = value; }
        }


        public static string SearchFullName
        {
            get { return HttpContext.Current.Session["SearchFullName"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchFullName"] = value; }
        }

        public static string SearchRegState
        {
            get { return HttpContext.Current.Session["SearchRegState"].ConvertToString(); }
            set { HttpContext.Current.Session["SearchRegState"] = value; }
        }


        public static string Upperdescription
        {
            get { return HttpContext.Current.Session["Upperdescription"].ConvertToString(); }
            set { HttpContext.Current.Session["Upperdescription"] = value; }
        }

        //Added By Roman For Passing IP Address
        public static string IPAddress
        {
            get { return HttpContext.Current.Session["IPAddress"].ConvertToString(); }
            set { HttpContext.Current.Session["IPAddress"] = value; }
        }


        public static string HouseholdDistrictENG
        {
            get { return HttpContext.Current.Session["HouseholdDistrictENG"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdDistrictENG"] = value; }
        }

        public static string HouseholdDistrictLOC
        {
            get { return HttpContext.Current.Session["HouseholdDistrictLOC"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdDistrictLOC"] = value; }
        }

        public static string HouseholdVDCENG
        {
            get { return HttpContext.Current.Session["HouseholdVDCENG"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdVDCENG"] = value; }
        }
        public static string HouseholdVDCLOC
        {
            get { return HttpContext.Current.Session["HouseholdVDCLOC"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdVDCLOC"] = value; }
        }

        public static string HouseholdWard
        {
            get { return HttpContext.Current.Session["HouseholdWard"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdWard"] = value; }
        }
        public static string HouseholdAreaEng
        {
            get { return HttpContext.Current.Session["HouseholdAreaEng"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdAreaEng"] = value; }
        }
        public static string HouseholdAreaLoc
        {
            get { return HttpContext.Current.Session["HouseholdAreaLoc"].ConvertToString(); }
            set { HttpContext.Current.Session["HouseholdAreaLoc"] = value; }
        }

        //NHRS Project Added

        public static string GroupName
        {
            get { return HttpContext.Current.Session["GroupName"].ConvertToString(); }
            set { HttpContext.Current.Session["GroupName"] = value; }
        }
        public static string GroupCD
        {
            get { return HttpContext.Current.Session["GroupCD"].ConvertToString(); }
            set { HttpContext.Current.Session["GroupCD"] = value; }
        }
        public static string OfficeCD
        {
            get { return HttpContext.Current.Session["OfficeCD"].ConvertToString(); }
            set { HttpContext.Current.Session["OfficeCD"] = value; }
        }
    }
}
