using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MIS.Models.Core;
using MIS.Models.Security;
using MIS.Services.Core;
using System.Data.OracleClient;
using MIS.Core;
using System.Web.Routing;
using ExceptionHandler;

namespace MIS
{
    public class BaseController : Controller
    {

        #region Pratik
        CommonFunction common = new CommonFunction();
        public static List<SelectListItem> Districts = new List<SelectListItem>();
        public static List<SelectListItem> Genders = new List<SelectListItem>();
        public static List<SelectListItem> Educations = new List<SelectListItem>();
        public static List<SelectListItem> ClassTypes = new List<SelectListItem>();
        public static List<SelectListItem> Castes = new List<SelectListItem>();
        public static List<SelectListItem> Religions = new List<SelectListItem>();
        public static List<SelectListItem> MaritalStatus = new List<SelectListItem>();
        public static List<SelectListItem> VdcMuns = new List<SelectListItem>();
        public static List<SelectListItem> Wards = new List<SelectListItem>();


        protected void GetMemberActionData(FormCollection fc)
        {
            try
            {
                Districts = common.GetDistricts("").ToList();
                Genders = common.GetGender("").ToList();
                Educations = common.GetEducation("").ToList();
                ClassTypes = common.GetClassType("").ToList();
                Castes = common.GetCaste("").ToList();
                Religions = common.GetReligion("").ToList();
                MaritalStatus = common.GetMaritalStatus("").ToList();
                VdcMuns = common.GetVDCMunByDistrict("", "").ToList();
                Wards = common.GetWardByVDCMun("", "").ToList();
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }


        #endregion

        /// <summary>
        /// Property that returns User profile
        /// </summary>
        public UserProfile Current
        {
            get
            {
                return ComAuthentication.CurrentUser;
            }
        }

        /// <summary>
        /// Event which is overrided while any action of controller is executing
        /// </summary>
        /// <param name="filterContext">ActionExecutingContext</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            RouteValueDictionary rvd = new RouteValueDictionary();
            if (HttpContext.Request.QueryString["p"] != null)
            {
                rvd = QueryStringEncrypt.DecryptString(HttpContext.Request.QueryString["p"]);
                if (rvd != null)
                {
                    if (rvd["menuCode"] != null)
                    {
                        Session["menuCode"] = rvd["menuCode"].ToString();
                    }
                }
            }
            if (!SessionCheck.CheckSession())
            {
                Session["SessionExpired"] = true;
                filterContext.Result = new RedirectResult("~/Home/");
            }
            else
            {
                Session["SessionExpired"] = false;
            }
            try
            {
                string strController = this.RouteData.Values["routeController"].ConvertToString();
                if (string.IsNullOrEmpty(strController))
                {
                    strController = this.RouteData.Values["controller"].ConvertToString();
                }
                string action = this.RouteData.Values["action"].ConvertToString();
                string menuUrl = string.Format("/{0}/{1}/", strController, action);
                Users SessionUser = new Users();
                string UserCd = "";
                if (Session["comuser"] != null)
                {
                    SessionUser = (Users)Session["comuser"];
                    UserCd = SessionUser.usrCd;

                }
                // UserProfile up= (Session["comuser"] == null) ? "" : (UserProfile)Session["comuser"];
                
                  var menu = new MenuBuilder().GetUserWiseMenu(menuUrl, UserCd);

                if (menu.Count == 1)
                {
                    menu = (MISMenu)Session["MenuTree"];
                }
                else
                {
                    ViewBag.MenuCount = (menu.Count) / 2;
                }
                Session["MenuTree"] = menu;

                if (Session["PreviiousUrl"] == null)
                {
                    Session["PreviiousUrl"] = "Home/Index";
                }
                else
                {
                    Session["PreviiousUrl"] = menuUrl;
                }

                ViewBag.MenuItems = menu;
                //ViewData["smenu"] = menu;
                ViewBag.CountryHead = Utils.GetLabel("Search Country");
                ViewBag.RegionHead = Utils.GetLabel("Search Region/State");
                ViewBag.ZoneHead = Utils.GetLabel("Search Zone");
                ViewBag.DistrictHead = Utils.GetLabel("Search District");
                ViewBag.VDCMunHead = Utils.GetLabel("Search VDC/Municipality");
                ViewBag.GroupHead = Utils.GetLabel("Search Group");
                ViewBag.MaritalStatusHead = Utils.GetLabel("Search Marital Status");
                ViewBag.PositionHead = Utils.GetLabel("Search Position");
                ViewBag.GenderHead = Utils.GetLabel("Search Gender");
                ViewBag.OfficeHead = Utils.GetLabel("Search Office");
                ViewBag.BankAccountTypeHead = Utils.GetLabel("Search Bank Account Type");
                ViewBag.ServiceProviderTypeHead = Utils.GetLabel("Search Service Provider Type");
                ViewBag.SocialAssistanceTypeHead = Utils.GetLabel("Search Social Security Type");
                ViewBag.FeedFormatTypeHead = Utils.GetLabel("Search Feed Format Type");
                ViewBag.CasteHead = Utils.GetLabel("Search Caste");
                ViewBag.RelationHead = Utils.GetLabel("Search Relation");
                ViewBag.AllowanceSourceHead = Utils.GetLabel("Search Allowance Type");
                ViewBag.AllowanceTypeHead = Utils.GetLabel("Search Allowance Source");
                ViewBag.HouseholdHead = Utils.GetLabel("Search Household");
                ViewBag.EducationHead = Utils.GetLabel("Search Education");
                ViewBag.ReligionHead = Utils.GetLabel("Search Religion");
                ViewBag.LightSourceHead = Utils.GetLabel("Search Light Source");
                ViewBag.ToiletTypeHead = Utils.GetLabel("Search Toilet Type");
                ViewBag.SocialIndicatorIHead = Utils.GetLabel("Search Social Indicator I");
                ViewBag.WaterSourceHead = Utils.GetLabel("Search Water Source");
                ViewBag.WallMaterialHead = Utils.GetLabel("Search Wall Material");
                ViewBag.CookingFuelSourceHead = Utils.GetLabel("Search Cooking Fuel Source");
                ViewBag.RoofMaterialHead = Utils.GetLabel("Search Roof Material");
                ViewBag.BudgetHead = Utils.GetLabel("Search Budget Head");
                ViewBag.LineItemTypeHead = Utils.GetLabel("Search Line Item Type");
                ViewBag.ServiceProviderHead = Utils.GetLabel("Search Service Provider");
                ViewBag.PeriodTypeHead = Utils.GetLabel("Search Period Type");
                ViewBag.OccupationHead = Utils.GetLabel("Search Occupation");
                ViewBag.SectionHead = Utils.GetLabel("Search Section");
                ViewBag.PositionSubClassHead = Utils.GetLabel("Search Position Sub-Class");
                ViewBag.DesignationHead = Utils.GetLabel("Search Designation");
               // ViewBag.MenuCount = ViewBag.MenuCount - 1;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                //ViewData["err_page_error"] = ErrorMessages.Get(oe.GetCode());
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                // throw new HttpException(Constants.NOT_FOUND, "Error.");

            }


        }

        /// <summary>
        /// Event which is overrided after any action of controller is executed 
        /// sends emmail, sms and so on
        /// </summary>
        /// <param name="filterContext">ActionExecutedContext</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                ExceptionHandler.ExceptionManager.AppendLog(filterContext.Exception);
            }
        }

    }
}
