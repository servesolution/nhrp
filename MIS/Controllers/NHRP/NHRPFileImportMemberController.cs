using ExceptionHandler;
using MIS.Models.NHRP;
using MIS.Services.Core;
using MIS.Services.NHRP.FileImport;
using MIS.Services.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
//using System.Windows.Forms;


namespace MIS.Controllers.NHRP
{
    public class NHRPFileImportMemberController : BaseController
    {
        DataImportReportService objReportService = new DataImportReportService();
        CommonFunction common = new CommonFunction();
        # region File Import

        public ActionResult FileImport(NhrsHouseDetail mdl)
        {


            DataTable dtFileStatusReport = new DataTable();
            List<string> jsonFiles = Directory.GetDirectories(Server.MapPath("~/Files/CSV/"), "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();
            //List<string> jsonFiles = Directory.GetFiles(Server.MapPath("~/Files/CSV/"), "*.json")
            //                         .Select(path => Path.GetFileName(path))
            //                         .ToList();
            dtFileStatusReport = objReportService.GetDataImportReportForDashboard(jsonFiles);
            string id = null;
            id = mdl.DISTRICT_CD_Defined.ConvertToString();
            ViewData["dtFileStatusReport"] = dtFileStatusReport;

            ViewData["ddldistrict"] = common.GetDistrictsinEnglish("");
            ViewData["dtFileImportRslt"] = dtFileStatusReport;
            return View();
        }

        public ActionResult GetFileImport(FormCollection fc, string SearchValue)
        {
            NhrsHouseDetail model = new NhrsHouseDetail();
            string id = string.Empty;
            // DataTable dt = new DataTable();
            try
            {
                //model.DISTRICT_CD_Defined = fc["DISTRICT_CD_Defined"].ToString();
                //id = model.DISTRICT_CD_Defined.ConvertToString();
                DataTable dtFileStatusReport = new DataTable();
                string filepath = Server.MapPath("~/Files/CSV/" + SearchValue + "/");
                if (Directory.Exists(filepath))
                {
                    List<string> jsonFiles = Directory.GetDirectories(filepath, "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();

                    dtFileStatusReport = objReportService.GetDataImportReportForDashboard1(jsonFiles);
                }
                //  dt = objReportService.GetFileImportFilter(id);
                // ViewData["ddldistrict"] = common.GetDistricts(id);
                ViewData["dtFileImportRslt"] = dtFileStatusReport;
                ViewData["dtPostToMainData"] = objReportService.GetPostToMainDataByDistrict(SearchValue);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_FileImport");
            //  return View("FileImport");

        }

        [HttpPost]
        public ActionResult FileImport1(IEnumerable<HttpPostedFileBase> file)
        {
            FileImportService fileImpService = new FileImportService();
            string exc = string.Empty;
            
            List<string> fileListInDB = new List<string>();
            fileListInDB = fileImpService.JSONFileListInDB();

            List<SortedDictionary<dynamic, dynamic>> totalSurveys = new List<SortedDictionary<dynamic, dynamic>>();
            int num_rows = 0;
            int num_cols = 0; 
            SortedDictionary<dynamic, dynamic> indSurvey = new SortedDictionary<dynamic, dynamic>();
            List<SortedDictionary<dynamic, dynamic>> lstHouseOwners = new List<SortedDictionary<dynamic, dynamic>>();

            if (file.Count() == 9)
            {
                foreach (var fl in file)
                {
                    //if (fl.ContentType == "application/octet-stream")
                    //{
                    if (fl != null && fl.ContentLength > 0)
                    {
                        if (fl.FileName == "Main.csv")
                        {
                            // Get the data for House Owners
                            string[,] valuesOwners = LoadCsv(fl.FileName);
                            num_rows = valuesOwners.GetUpperBound(0) + 1;
                            num_cols = valuesOwners.GetUpperBound(1) + 1;
                            DataTable tableHouseOwners = new DataTable();
                            for (int c = 0; c < num_cols; c++)
                            {
                                tableHouseOwners.Columns.Add(valuesOwners[0, c].Replace("\"", ""));
                                //tableHouseOwners.Columns.Add(valuesOwners[0, c]);
                            }
                            for (int r = 1; r < num_rows; r++)
                            {
                                tableHouseOwners.Rows.Add();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableHouseOwners.Rows[r - 1][c] = valuesOwners[r, c].Replace("\"", "");
                                    //tableHouseOwners.Rows[r - 1][c] = valuesOwners[r, c];
                                }
                            }
                        }
                    }
                    //}
                }
            }


            return View();
        }

        private void InsertMigrationData(string initialPath, string hidJsonFileName)
        {
            FileImportService fileImpService = new FileImportService();
            FormCollection fc = new FormCollection();
            string id = string.Empty;
            string exc = string.Empty;
            bool success = false;
            int surveyCount = 0;
            int houseCount = 0;
            int householdCount = 0;
            int memberCount = 0;
            try
            {
                if (Directory.Exists(initialPath))
                {

                    List<string> fileListInDB = new List<string>();
                    fileListInDB = fileImpService.JSONFileListInDB();
                    if (hidJsonFileName != null && hidJsonFileName.Length > 0)
                    {
                        ViewData["fileNameBeingImported"] = hidJsonFileName;
                        if (!fileListInDB.Contains(hidJsonFileName))
                        {
                            if (Directory.GetFiles(initialPath).Length == 9)
                            {
                                List<SortedDictionary<dynamic, dynamic>> totalSurveys = new List<SortedDictionary<dynamic, dynamic>>();
                                int num_rows = 0;
                                int num_cols = 0;

                                int houseOwnerCnt = 0;
                                int ownerOtherHouseCnt = 0;
                                int buildingsCnt = 0;


                                SortedDictionary<dynamic, dynamic> indSurvey = new SortedDictionary<dynamic, dynamic>();
                                List<SortedDictionary<dynamic, dynamic>> lstHouseOwners = new List<SortedDictionary<dynamic, dynamic>>();

                                //DataTable dtExcelHouseOwners = new DataTable();
                                //dtExcelHouseOwners = Import_To_Grid(initialPath + "/House_Owner.xlsx"), ".xlsx", "Yes", "House_Owner");

                                #region House Owners

                                // Get the data for House Owners

                                string[,] valuesOwners = LoadCsv(initialPath + "/Main_Table.csv");
                                num_rows = valuesOwners.GetUpperBound(0) + 1;
                                num_cols = valuesOwners.GetUpperBound(1) + 1;
                                DataTable tableHouseOwners = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableHouseOwners.Columns.Add(valuesOwners[0, c].Replace("\"", ""));
                                    //tableHouseOwners.Columns.Add(valuesOwners[0, c]);
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableHouseOwners.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableHouseOwners.Rows[r - 1][c] = valuesOwners[r, c].Replace("\"", "");
                                        //tableHouseOwners.Rows[r - 1][c] = valuesOwners[r, c];
                                    }
                                }

                                // Get the data for Meta
                                string[,] valuesOwnersMeta = LoadCsv(initialPath + "/Meta.csv");
                                num_rows = valuesOwnersMeta.GetUpperBound(0) + 1;
                                num_cols = valuesOwnersMeta.GetUpperBound(1) + 1;
                                DataTable tableHouseOwnersMeta = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableHouseOwnersMeta.Columns.Add(valuesOwnersMeta[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableHouseOwnersMeta.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableHouseOwnersMeta.Rows[r - 1][c] = valuesOwnersMeta[r, c].Replace("\"", "");
                                    }
                                }

                                var collection = from t1 in tableHouseOwners.AsEnumerable()
                                                 join t2 in tableHouseOwnersMeta.AsEnumerable()
                                                    on t1["ona_id"] equals t2["ona_id"]
                                                 select new
                                                 {
                                                     ona_id = t1["ona_id"],
                                                     district = t1["dist"],
                                                     vdc = t1["vdcmun"],
                                                     ward = t1["ward"],
                                                     enum_ar = t1["EA"],
                                                     house_no = t1["howner_sn"],
                                                     enum_id = t1["enum_id"],
                                                     tole = t1["tole"],
                                                     own_num = t1["howner_num"],
                                                     own_res = t1["own_res_sme"],
                                                     name_fst = t1["name_fst"],
                                                     name_mid = t1["name_mid"],
                                                     name_lst = t1["name_lst"],
                                                     gender = t1["gender"],
                                                     relation = t1["relation"],
                                                     relatnod = t1["relatnod"],
                                                     reason = t1["reason"],
                                                     reasonod = t1["reasonod"],
                                                     photo = t1["photo"],
                                                     house_sa = t1["rhouse_sa"],
                                                     house_di = t1["rhouse_da"],
                                                     ndam_c = t1["ndam_c"],
                                                     pdam_c = t1["pdam_c"],
                                                     cdam_c = t1["cdam_c"],
                                                     tel = t1["tel"],
                                                     soc_mob = t1["soc_mob"],
                                                     comment = t1["comment"],
                                                     uuid = t2["uuid"],
                                                     st_time = t2["st_time"],
                                                     en_time = t2["en_time"],
                                                     today = t2["today"],
                                                     imei = t2["imei"],
                                                     imsi = t2["imsi"],
                                                     sim = t2["sim"],
                                                     sim_tel = t2["sim_tel"],
                                                     sub_time = t2["sub_time"]
                                                 };

                                DataTable result = new DataTable("tableHouseOwnersFinal");
                                result.Columns.Add("ona_id", typeof(string));
                                result.Columns.Add("district", typeof(string));
                                result.Columns.Add("vdc", typeof(string));
                                result.Columns.Add("ward", typeof(string));
                                result.Columns.Add("enum_ar", typeof(string));
                                result.Columns.Add("house_no", typeof(string));
                                result.Columns.Add("enum_id", typeof(string));
                                result.Columns.Add("tole", typeof(string));
                                result.Columns.Add("own_num", typeof(string));
                                result.Columns.Add("own_res", typeof(string));
                                result.Columns.Add("name_fst", typeof(string));
                                result.Columns.Add("name_mid", typeof(string));
                                result.Columns.Add("name_lst", typeof(string));
                                result.Columns.Add("gender", typeof(string));
                                result.Columns.Add("relation", typeof(string));
                                result.Columns.Add("relatnod", typeof(string));
                                result.Columns.Add("reason", typeof(string));
                                result.Columns.Add("reasonod", typeof(string));
                                result.Columns.Add("photo", typeof(string));
                                result.Columns.Add("house_sa", typeof(string));
                                result.Columns.Add("house_di", typeof(string));
                                result.Columns.Add("ndam_c", typeof(string));
                                result.Columns.Add("pdam_c", typeof(string));
                                result.Columns.Add("cdam_c", typeof(string));
                                result.Columns.Add("tel", typeof(string));
                                result.Columns.Add("soc_mob", typeof(string));
                                result.Columns.Add("comment", typeof(string));
                                result.Columns.Add("uuid", typeof(string));
                                result.Columns.Add("st_time", typeof(string));
                                result.Columns.Add("en_time", typeof(string));
                                result.Columns.Add("today", typeof(string));
                                result.Columns.Add("imei", typeof(string));
                                result.Columns.Add("imsi", typeof(string));
                                result.Columns.Add("sim", typeof(string));
                                result.Columns.Add("sim_tel", typeof(string));
                                result.Columns.Add("sub_time", typeof(string));

                                foreach (var item in collection)
                                {
                                    result.Rows.Add(item.ona_id, item.district, item.vdc, item.ward,
                                        item.enum_ar, item.house_no, item.enum_id, item.tole, item.own_num
                                        , item.own_res, item.name_fst, item.name_mid, item.name_lst, item.gender, item.relation, item.relatnod
                                        , item.reason, item.reasonod, item.photo, item.house_sa, item.house_di, item.ndam_c, item.pdam_c, item.cdam_c, item.tel
                                        , item.soc_mob, item.comment, item.uuid, item.st_time, item.en_time, item.today, item.imei, item.imsi, item.sim, item.sim_tel, item.sub_time);
                                }

                                houseOwnerCnt = result.Rows.Count;
                                #region Code for Excel
                                //DataTable dtExcelHouseOwners = new DataTable();
                                //dtExcelHouseOwners = Import_To_Grid(initialPath + "/House_Owner.xlsx"), ".xlsx", "Yes", "House_Owner");

                                //DataTable tableHouseOwnersDetail = new DataTable();

                                //tableHouseOwnersDetail = dtExcelHouseOwners;
                                #endregion

                                #region Code for CSV
                                string[,] valuesOwnersDetail = LoadCsv(initialPath + "/House_Owner.csv");
                                //string[,] valuesOwnersDetail = LoadCsvTab(initialPath + "/House_Owner.csv"));
                                num_rows = valuesOwnersDetail.GetUpperBound(0) + 1;
                                num_cols = valuesOwnersDetail.GetUpperBound(1) + 1;
                                DataTable tableHouseOwnersDetail = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableHouseOwnersDetail.Columns.Add(valuesOwnersDetail[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableHouseOwnersDetail.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableHouseOwnersDetail.Rows[r - 1][c] = valuesOwnersDetail[r, c].Replace("\"", "");
                                    }
                                }
                                #endregion




                                string[,] valuesOtherHouseCount = LoadCsv(initialPath + "/House_Other_Place.csv");
                                num_rows = valuesOtherHouseCount.GetUpperBound(0) + 1;
                                num_cols = valuesOtherHouseCount.GetUpperBound(1) + 1;
                                DataTable tableOtherHouseCount = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableOtherHouseCount.Columns.Add(valuesOtherHouseCount[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableOtherHouseCount.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableOtherHouseCount.Rows[r - 1][c] = valuesOtherHouseCount[r, c].Replace("\"", "");
                                    }
                                }

                                // Get the data for Building Count
                                string[,] valuesBuildingCount = LoadCsv(initialPath + "/Building.csv");
                                num_rows = valuesBuildingCount.GetUpperBound(0) + 1;
                                num_cols = valuesBuildingCount.GetUpperBound(1) + 1;
                                DataTable tableBuildingCount = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableBuildingCount.Columns.Add(valuesBuildingCount[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableBuildingCount.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableBuildingCount.Rows[r - 1][c] = valuesBuildingCount[r, c].Replace("\"", "");
                                    }
                                }

                                // Get the data for Family Count
                                string[,] valuesFamilyCount = LoadCsv(initialPath + "/Household.csv");
                                num_rows = valuesFamilyCount.GetUpperBound(0) + 1;
                                num_cols = valuesFamilyCount.GetUpperBound(1) + 1;
                                DataTable tableFamilyCount = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableFamilyCount.Columns.Add(valuesFamilyCount[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableFamilyCount.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableFamilyCount.Rows[r - 1][c] = valuesFamilyCount[r, c].Replace("\"", "");
                                    }
                                }

                                // Get the data for Individual Count
                                string[,] valuesIndividualCount = LoadCsv(initialPath + "/Individual.csv");
                                num_rows = valuesIndividualCount.GetUpperBound(0) + 1;
                                num_cols = valuesIndividualCount.GetUpperBound(1) + 1;
                                DataTable tableIndividualCount = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableIndividualCount.Columns.Add(valuesIndividualCount[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableIndividualCount.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableIndividualCount.Rows[r - 1][c] = valuesIndividualCount[r, c].Replace("\"", "");
                                    }
                                }

                                // Get the data for Death Count
                                string[,] valuesDeathCount = LoadCsv(initialPath + "/Death.csv");
                                num_rows = valuesDeathCount.GetUpperBound(0) + 1;
                                num_cols = valuesDeathCount.GetUpperBound(1) + 1;
                                DataTable tableDeathCount = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableDeathCount.Columns.Add(valuesDeathCount[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableDeathCount.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableDeathCount.Rows[r - 1][c] = valuesDeathCount[r, c].Replace("\"", "");
                                    }
                                }
                                // Get the data for Destructed Count
                                string[,] valuesDestructedCount = LoadCsv(initialPath + "/Injured_Missed.csv");
                                num_rows = valuesDestructedCount.GetUpperBound(0) + 1;
                                num_cols = valuesDestructedCount.GetUpperBound(1) + 1;
                                DataTable tableDestructedCount = new DataTable();
                                for (int c = 0; c < num_cols; c++)
                                {
                                    tableDestructedCount.Columns.Add(valuesDestructedCount[0, c].Replace("\"", ""));
                                }
                                for (int r = 1; r < num_rows; r++)
                                {
                                    tableDestructedCount.Rows.Add();
                                    for (int c = 0; c < num_cols; c++)
                                    {
                                        tableDestructedCount.Rows[r - 1][c] = valuesDestructedCount[r, c].Replace("\"", "");
                                    }
                                }

                                //Start of filtering and pushing in Dictionary
                                foreach (DataRow dr in result.Rows)
                                {
                                    indSurvey = new SortedDictionary<dynamic, dynamic>();
                                    foreach (DataColumn dc in result.Columns)
                                    {
                                        indSurvey.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                                    }
                                    //lstHouseOwners.Add(indOwner);
                                    #region House Owners Detail
                                    List<SortedDictionary<dynamic, dynamic>> lstHouseOwnersDetail = new List<SortedDictionary<dynamic, dynamic>>();


                                    DataRow[] drSelectOwnerDetail = tableHouseOwnersDetail.Select("ona_id='" + dr["ona_id"] + "'");
                                    foreach (DataRow dr1 in drSelectOwnerDetail)
                                    {
                                        SortedDictionary<dynamic, dynamic> indOwner = new SortedDictionary<dynamic, dynamic>();
                                        foreach (DataColumn dc in tableHouseOwnersDetail.Columns)
                                        {
                                            indOwner.Add(dc.ColumnName, dr1[dc.ColumnName].ToString());
                                        }
                                        lstHouseOwnersDetail.Add(indOwner);
                                    }

                                    #endregion

                                    #region Other House
                                    List<SortedDictionary<dynamic, dynamic>> lstOwnerOtherHouses = new List<SortedDictionary<dynamic, dynamic>>();

                                    DataRow[] drSelectOtherHouse = tableOtherHouseCount.Select("ona_id='" + dr["ona_id"] + "'");
                                    ownerOtherHouseCnt = tableOtherHouseCount.Rows.Count;
                                    foreach (DataRow dr2 in drSelectOtherHouse)
                                    {
                                        SortedDictionary<dynamic, dynamic> indOwner = new SortedDictionary<dynamic, dynamic>();
                                        foreach (DataColumn dc in tableOtherHouseCount.Columns)
                                        {
                                            indOwner.Add(dc.ColumnName, dr2[dc.ColumnName].ToString());
                                        }
                                        lstOwnerOtherHouses.Add(indOwner);
                                    }

                                    #endregion

                                    #region Building
                                    List<SortedDictionary<dynamic, dynamic>> lstBuildings = new List<SortedDictionary<dynamic, dynamic>>();
                                    buildingsCnt = tableBuildingCount.Rows.Count;
                                    //List<SortedDictionary<dynamic, dynamic>> lstFamilies = new List<SortedDictionary<dynamic, dynamic>>();
                                    DataRow[] drSelectBuilding = tableBuildingCount.Select("ona_id='" + dr["ona_id"] + "'");
                                    foreach (DataRow dr3 in drSelectBuilding)
                                    {
                                        List<SortedDictionary<dynamic, dynamic>> lstFamilies = new List<SortedDictionary<dynamic, dynamic>>();
                                        SortedDictionary<dynamic, dynamic> indBuilding = new SortedDictionary<dynamic, dynamic>();
                                        foreach (DataColumn dc in tableBuildingCount.Columns)
                                        {
                                            indBuilding.Add(dc.ColumnName, dr3[dc.ColumnName].ToString());
                                        }

                                        #region Family

                                        //List<SortedDictionary<dynamic, dynamic>> lstMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                        //List<SortedDictionary<dynamic, dynamic>> lstDeadMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                        //List<SortedDictionary<dynamic, dynamic>> lstDestructedMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                        SortedDictionary<dynamic, dynamic> indFamily = new SortedDictionary<dynamic, dynamic>();
                                        DataRow[] drSelectFamily = tableFamilyCount.Select("ona_id='" + dr3["ona_id"] + "' and house_sn='" + dr3["house_sn"] + "'");//house_sn=building structure number
                                        foreach (DataRow dr4 in drSelectFamily)
                                        {
                                            List<SortedDictionary<dynamic, dynamic>> lstMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                            List<SortedDictionary<dynamic, dynamic>> lstDeadMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                            List<SortedDictionary<dynamic, dynamic>> lstDestructedMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                            if (dr4["ona_id"].ToString() == "88288")
                                            {

                                            }
                                            foreach (DataColumn dc in tableFamilyCount.Columns)
                                            {
                                                indFamily.Add(dc.ColumnName, dr4[dc.ColumnName].ToString());
                                            }

                                            #region Individual Member

                                            SortedDictionary<dynamic, dynamic> indIndividual = new SortedDictionary<dynamic, dynamic>();
                                            DataRow[] drIndividual = tableIndividualCount.Select("ona_id='" + dr4["ona_id"] + "' and house_sn='" + dr4["house_sn"] + "' and hhd_sn='" + dr4["hhd_sn"] + "'");//howner_sn=Family serial num
                                            foreach (DataRow dr5 in drIndividual)
                                            {
                                                foreach (DataColumn dc in tableIndividualCount.Columns)
                                                {
                                                    indIndividual.Add(dc.ColumnName, dr5[dc.ColumnName].ToString());
                                                }
                                                lstMembers.Add(indIndividual);
                                                indIndividual = new SortedDictionary<dynamic, dynamic>();
                                            }
                                            indFamily.Add("Members", lstMembers);
                                            #endregion

                                            #region Death Member

                                            SortedDictionary<dynamic, dynamic> indDeath = new SortedDictionary<dynamic, dynamic>();
                                            DataRow[] drDeath = tableDeathCount.Select("ona_id='" + dr4["ona_id"] + "' and house_sn='" + dr4["house_sn"] + "' and hhd_sn='" + dr4["hhd_sn"] + "'");
                                            foreach (DataRow dr6 in drDeath)
                                            {
                                                foreach (DataColumn dc in tableDeathCount.Columns)
                                                {
                                                    indDeath.Add(dc.ColumnName, dr6[dc.ColumnName].ToString());
                                                }
                                                lstDeadMembers.Add(indDeath);
                                                indDeath = new SortedDictionary<dynamic, dynamic>();
                                            }
                                            indFamily.Add("DeadMembers", lstDeadMembers);
                                            #endregion

                                            #region Destructed Members

                                            SortedDictionary<dynamic, dynamic> indDestructed = new SortedDictionary<dynamic, dynamic>();
                                            DataRow[] drDestructed = tableDestructedCount.Select("ona_id='" + dr4["ona_id"] + "' and house_sn='" + dr4["house_sn"] + "' and hhd_sn='" + dr4["hhd_sn"] + "'");
                                            foreach (DataRow dr7 in drDestructed)
                                            {
                                                foreach (DataColumn dc in tableDestructedCount.Columns)
                                                {
                                                    indDestructed.Add(dc.ColumnName, dr7[dc.ColumnName].ToString());
                                                }
                                                lstDestructedMembers.Add(indDestructed);
                                                indDestructed = new SortedDictionary<dynamic, dynamic>();
                                            }
                                            indFamily.Add("DestructedMembers", lstDestructedMembers);
                                            #endregion

                                            lstFamilies.Add(indFamily);
                                            indFamily = new SortedDictionary<dynamic, dynamic>();
                                        }
                                        indBuilding.Add("Families", lstFamilies);
                                        #endregion

                                        lstBuildings.Add(indBuilding);
                                        indBuilding = new SortedDictionary<dynamic, dynamic>();
                                    }
                                    #endregion

                                    indSurvey.Add("Buildings", lstBuildings);
                                    indSurvey.Add("Owners", lstHouseOwnersDetail);
                                    indSurvey.Add("OtherHouses", lstOwnerOtherHouses);


                                    totalSurveys.Add(indSurvey);
                                }
                                #endregion
                                fileListInDB = new List<string>();

                                List<SortedDictionary<dynamic, dynamic>> filteredSurveyList = new List<SortedDictionary<dynamic, dynamic>>();
                                List<string> houseIDListDB = new List<string>();
                                houseIDListDB = new List<string>();
                                houseIDListDB = fileImpService.HouseIDListCSV();
                                filteredSurveyList = totalSurveys.Where(m => !houseIDListDB.Contains(m["ona_id"])).ToList();

                                if (filteredSurveyList.Count == 0)
                                {
                                    ViewData["ErrorMessage"] = "The House ID in the file is already exist in the database.Data import failed." + initialPath;
                                    ViewData["Status"] = "FAILED";
                                }
                                else
                                {

                                    fileListInDB = fileImpService.JSONErrorFileListInDB();
                                    if (fileListInDB.Contains(hidJsonFileName))
                                    {
                                        string batchID = fileImpService.GetBatchIDFromFileName(hidJsonFileName);
                                        success = fileImpService.SaveDataCSV(hidJsonFileName, totalSurveys, "I", batchID, out surveyCount, out houseCount, out householdCount, out memberCount, out exc);
                                    }
                                    else
                                    {
                                        success = fileImpService.SaveDataCSV(hidJsonFileName, totalSurveys, "I", "", out surveyCount, out houseCount, out householdCount, out memberCount, out exc);
                                    }
                                    //success = fileImpService.SaveDataCSV(hidJsonFileName, totalSurveys, "I", out surveyCount, out houseCount, out householdCount, out memberCount, out exc);
                                    ViewData["surveyCnt"] = surveyCount;
                                    ViewData["houseCnt"] = houseCount;
                                    ViewData["familyCnt"] = householdCount;//familyCount;
                                    ViewData["memberCnt"] = memberCount;
                                    if (success)
                                    {
                                        ViewData["SuccessMessage"] = "Data imported successfully.";
                                        ViewData["Status"] = "COMPLETED";
                                    }
                                    else
                                    {
                                        ViewData["ErrorMessage"] = "Data import failed." + initialPath + " Error: " + exc;
                                        ViewData["Status"] = "FAILED";


                                    }

                                }

                            }
                        }
                        else
                        {
                            //ViewData["ErrorMessage"] = "File with name '" + hidJsonFileName + "' has already been used for data import. Please choose another file!!";
                            //ViewData["Status"] = "FAILED";
                        }
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "File Not Found";
                    ViewData["Status"] = "FAILED";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Exception Occured. Error:" + ex.Message.ConvertToString();
                ViewData["Status"] = "FAILED";
            }

        }

        [HttpPost]
        public ActionResult FileImport(string hidJsonFileName, string hidDistrictName)
        {
            FileImportService fileImpService = new FileImportService();
            FormCollection fc = new FormCollection();
            string id = string.Empty;
            string exc = string.Empty; 
            string initialPath1 = Server.MapPath("~/Files/CSV/" + hidDistrictName);
            try
            {
                string initialPath = string.Empty;// Server.MapPath("~/Files/CSV/" + hidDistrictName + "/" + hidJsonFileName);
                if (hidJsonFileName == "All")
                {
                    DataTable dtFileStatusReport1 = new DataTable();
                    string filepath = Server.MapPath("~/Files/CSV/" + hidDistrictName + "/");
                    if (Directory.Exists(filepath))
                    {
                        List<string> jsonFiles1 = Directory.GetDirectories(filepath, "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();
                        foreach (string s in jsonFiles1)
                        {

                            initialPath = Server.MapPath("~/Files/CSV/" + hidDistrictName + "/" + s);
                            InsertMigrationData(initialPath, s);
                        }
                        // dtFileStatusReport1 = objReportService.GetDataImportReportForDashboard1(jsonFiles);
                    }

                }
                else
                {
                    int value;
                    if (int.TryParse(hidJsonFileName, out value))
                    {

                    }
                    else
                    {
                        initialPath = Server.MapPath("~/Files/CSV/" + hidDistrictName + "/" + hidJsonFileName);
                        InsertMigrationData(initialPath, hidJsonFileName);
                    }
                }
            }
            catch (OracleException oe)
            {
                ViewData["ErrorMessage"] = "Data import failed.";
                ViewData["Status"] = "FAILED";
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Data import failed.";
                ViewData["Status"] = "FAILED";
                ExceptionManager.AppendLog(ex);
            }
            // redirect back to the index action to show the form once again
            DataTable dtFileStatusReport = new DataTable();
            //List<string> jsonFiles = Directory.GetFiles(Server.MapPath("~/Files/Json/"), "*.json")
            //                         .Select(path => Path.GetFileName(path))
            //                         .ToList();
            List<string> jsonFiles = Directory.GetDirectories(initialPath1, "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();
            dtFileStatusReport = objReportService.GetDataImportReportForDashboard(jsonFiles);
            ViewData["dtFileStatusReport"] = dtFileStatusReport;
            ViewData["ddldistrict"] = common.GetDistrictsinEnglish("");
            ViewData["dtPostToMainData"] = objReportService.GetPostToMainData();
            return View();
        }
        private string[,] LoadCsvTab(string filename)
        {
            string whole_file = System.IO.File.ReadAllText(filename);

            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            int num_rows = lines.Length;
            int num_cols = lines[0].Split('\t').Length;

            string[,] values = new string[num_rows, num_cols];
            //string[] values = Regex.Split(value, "\r\n");
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            for (int r = 0; r < num_rows; r++)
            {
                
                string[] line_r = SplitCSVTab(lines[r]); //lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }

            return values;
        }
        public static string[] SplitCSVTab(string input)
        {
            Regex csvSplit = new Regex("(?:^|\t)(\"(?:[^\"]+|\"\")*\"|[^\t]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart('\t'));
            }

            return list.ToArray<string>();
        }

        private string[,] LoadCsv(string filename)
        {
            string whole_file = System.IO.File.ReadAllText(filename);

            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            int num_rows = lines.Length;
            int num_cols = lines[0].Split(',').Length;

            string[,] values = new string[num_rows, num_cols];
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            for (int r = 0; r < num_rows; r++)
            {
                //if(csvSplit.Match(lines[r]))
                //{

                //}
                string[] line_r = SplitCSV(lines[r]); //lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }

            return values;
        }
        public static string[] SplitCSV(string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray<string>();
        }

        private DataTable Import_To_Grid(string FilePath, string Extension, string isHDR, string sheetName)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //string SheetName = dtExcelSchema.Rows[0]["House_Owner"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "$]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            return dt;
        }


        [HttpPost]
        //public ActionResult FileImport(HttpPostedFileBase file)
        public ActionResult FileImportOld(string hidJsonFileName)
        {
            FileImportService fileImpService = new FileImportService();
            string exc = string.Empty;
            bool success = false;
            int surveyCount = 0;
            int houseCount = 0;
            int householdCount = 0;
            int memberCount = 0;
            try
            {
                // Verify that the user selected a file
                List<string> fileListInDB = new List<string>();
                fileListInDB = fileImpService.JSONFileListInDB();
                //if (file.ContentType == "application/octet-stream")
                //{
                //if (file != null && file.ContentLength > 0)
                //{
                if (hidJsonFileName != null && hidJsonFileName.Length > 0)
                {
                    //ViewData["fileNameBeingImported"] = file.FileName;
                    ViewData["fileNameBeingImported"] = hidJsonFileName;
                    if (!fileListInDB.Contains(hidJsonFileName))
                    {
                        using (StreamReader r = new StreamReader(Server.MapPath("~/Files/Json/") + hidJsonFileName))
                        {
                            List<SortedDictionary<dynamic, dynamic>> totalSurveys = new List<SortedDictionary<dynamic, dynamic>>();
                            string json = r.ReadToEnd();
                            dynamic array = JsonConvert.DeserializeObject(json);
                            foreach (var item in array)
                            {
                                SortedDictionary<dynamic, dynamic> indSurvey = new SortedDictionary<dynamic, dynamic>();
                                List<SortedDictionary<dynamic, dynamic>> lstHouseOwners = new List<SortedDictionary<dynamic, dynamic>>();
                                List<SortedDictionary<dynamic, dynamic>> lstOwnerOtherHouses = new List<SortedDictionary<dynamic, dynamic>>();
                                List<SortedDictionary<dynamic, dynamic>> lstBuildings = new List<SortedDictionary<dynamic, dynamic>>();
                                //Counts
                                int houseOwnerCnt = 0;
                                int ownerOtherHouseCnt = 0;
                                int buildingsCnt = 0;
                                foreach (var itemKVPair in item)
                                {
                                    if (itemKVPair.Name != "building_damage_assessment/hh_data/house_owner_info" && itemKVPair.Name != "building_damage_assessment/hh_data/hh_owner_other_house_detail" && itemKVPair.Name != "building_damage_assessment/hh_data/hh_building")
                                    {
                                        if (!indSurvey.ContainsKey(itemKVPair.Name))
                                        {
                                            indSurvey.Add(itemKVPair.Name, itemKVPair.Value);
                                        }
                                    }
                                    else
                                    {
                                        if (itemKVPair.Name == "building_damage_assessment/hh_data/house_owner_info")
                                        {
                                            dynamic arrayHouseOwners = JsonConvert.DeserializeObject((itemKVPair.Value).ToString());
                                            houseOwnerCnt = arrayHouseOwners.Count;
                                            foreach (var owner in arrayHouseOwners)
                                            {
                                                SortedDictionary<dynamic, dynamic> indOwner = new SortedDictionary<dynamic, dynamic>();
                                                foreach (var ownerDetail in owner)
                                                {
                                                    indOwner.Add(ownerDetail.Name, ownerDetail.Value);
                                                }
                                                lstHouseOwners.Add(indOwner);
                                            }

                                        }
                                        else if (itemKVPair.Name == "building_damage_assessment/hh_data/hh_owner_other_house_detail")
                                        {
                                            dynamic arrayOwnerOtherHouses = JsonConvert.DeserializeObject((itemKVPair.Value).ToString());
                                            ownerOtherHouseCnt = arrayOwnerOtherHouses.Count;
                                            foreach (var ownerOthHouse in arrayOwnerOtherHouses)
                                            {
                                                SortedDictionary<dynamic, dynamic> indOwnerOtherHouse = new SortedDictionary<dynamic, dynamic>();
                                                foreach (var ownerOtherHouseDetail in ownerOthHouse)
                                                {
                                                    indOwnerOtherHouse.Add(ownerOtherHouseDetail.Name, ownerOtherHouseDetail.Value);
                                                }
                                                lstOwnerOtherHouses.Add(indOwnerOtherHouse);
                                            }

                                        }
                                        else if (itemKVPair.Name == "building_damage_assessment/hh_data/hh_building")
                                        {
                                            List<SortedDictionary<dynamic, dynamic>> lstFamilies = new List<SortedDictionary<dynamic, dynamic>>();
                                            dynamic arrayBuildings = JsonConvert.DeserializeObject((itemKVPair.Value).ToString());
                                            //Counts
                                            buildingsCnt = arrayBuildings.Count;
                                            foreach (var building in arrayBuildings)
                                            {
                                                SortedDictionary<dynamic, dynamic> indBuilding = new SortedDictionary<dynamic, dynamic>();
                                                foreach (var buildingDetail in building)
                                                {
                                                    if (buildingDetail.Name != "building_damage_assessment/hh_data/hh_building/hh_building_group/family")
                                                    {
                                                        indBuilding.Add(buildingDetail.Name, buildingDetail.Value);
                                                    }
                                                    else
                                                    {
                                                        dynamic arrayfamily = JsonConvert.DeserializeObject((buildingDetail.Value).ToString());
                                                        foreach (var family in arrayfamily)
                                                        {
                                                            List<SortedDictionary<dynamic, dynamic>> lstMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                                            List<SortedDictionary<dynamic, dynamic>> lstDeadMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                                            List<SortedDictionary<dynamic, dynamic>> lstDestructedMembers = new List<SortedDictionary<dynamic, dynamic>>();
                                                            SortedDictionary<dynamic, dynamic> indFamily = new SortedDictionary<dynamic, dynamic>();
                                                            foreach (var familyDetail in family)
                                                            {
                                                                if (familyDetail.Name != "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members" && familyDetail.Name != "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death" && familyDetail.Name != "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions")
                                                                {
                                                                    indFamily.Add(familyDetail.Name, familyDetail.Value);
                                                                }
                                                                else
                                                                {
                                                                    if (familyDetail.Name == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members")
                                                                    {
                                                                        dynamic arrayMembers = JsonConvert.DeserializeObject((familyDetail.Value).ToString());
                                                                        SortedDictionary<dynamic, dynamic> indMember = new SortedDictionary<dynamic, dynamic>();
                                                                        foreach (var memberDetail in arrayMembers)
                                                                        {
                                                                            dynamic arrayMember = JsonConvert.DeserializeObject((memberDetail).ToString());
                                                                            foreach (var memberDet in arrayMember)
                                                                            {
                                                                                indMember.Add(memberDet.Name, memberDet.Value);
                                                                            }
                                                                            lstMembers.Add(indMember);
                                                                            indMember = new SortedDictionary<dynamic, dynamic>();
                                                                        }
                                                                        indFamily.Add("Members", lstMembers);
                                                                    }
                                                                    else if (familyDetail.Name == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death")
                                                                    {
                                                                        dynamic arrayDeadMembers = JsonConvert.DeserializeObject((familyDetail.Value).ToString());
                                                                        SortedDictionary<dynamic, dynamic> indDeadMember = new SortedDictionary<dynamic, dynamic>();
                                                                        foreach (var deadMemberDetail in arrayDeadMembers)
                                                                        {
                                                                            dynamic arrayDeadMember = JsonConvert.DeserializeObject((deadMemberDetail).ToString());
                                                                            foreach (var deadMemberDet in arrayDeadMember)
                                                                            {
                                                                                indDeadMember.Add(deadMemberDet.Name, deadMemberDet.Value);
                                                                            }
                                                                            lstDeadMembers.Add(indDeadMember);
                                                                            indDeadMember = new SortedDictionary<dynamic, dynamic>();
                                                                        }
                                                                        indFamily.Add("DeadMembers", lstDeadMembers);
                                                                    }
                                                                    else if (familyDetail.Name == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions")
                                                                    {
                                                                        dynamic arrayDestructedMembers = JsonConvert.DeserializeObject((familyDetail.Value).ToString());
                                                                        SortedDictionary<dynamic, dynamic> indDestructedMember = new SortedDictionary<dynamic, dynamic>();
                                                                        foreach (var destructedMemberDetail in arrayDestructedMembers)
                                                                        {
                                                                            dynamic arrayDestructedMember = JsonConvert.DeserializeObject((destructedMemberDetail).ToString());
                                                                            foreach (var destructedMemberDet in arrayDestructedMember)
                                                                            {
                                                                                indDestructedMember.Add(destructedMemberDet.Name, destructedMemberDet.Value);
                                                                            }
                                                                            lstDestructedMembers.Add(indDestructedMember);
                                                                            indDestructedMember = new SortedDictionary<dynamic, dynamic>();
                                                                        }
                                                                        indFamily.Add("DestructedMembers", lstDestructedMembers);
                                                                    }
                                                                }
                                                            }
                                                            lstFamilies.Add(indFamily);
                                                            indFamily = new SortedDictionary<dynamic, dynamic>();
                                                        }
                                                        indBuilding.Add("Families", lstFamilies);
                                                    }
                                                }
                                                lstBuildings.Add(indBuilding);
                                                indBuilding = new SortedDictionary<dynamic, dynamic>();
                                            }
                                            indSurvey.Add("Buildings", lstBuildings);
                                            indSurvey.Add("Owners", lstHouseOwners);
                                            indSurvey.Add("OtherHouses", lstOwnerOtherHouses);
                                        }
                                    }
                                }
                                totalSurveys.Add(indSurvey);
                                indSurvey = new SortedDictionary<dynamic, dynamic>();
                            }
                            success = fileImpService.SaveData(hidJsonFileName, totalSurveys, "I", out surveyCount, out houseCount, out householdCount, out memberCount, out exc);
                            ViewData["surveyCnt"] = surveyCount;
                            ViewData["houseCnt"] = houseCount;
                            ViewData["familyCnt"] = householdCount;//familyCount;
                            ViewData["memberCnt"] = memberCount;
                            if (success)
                            {
                                ViewData["SuccessMessage"] = "Data imported successfully.";
                                ViewData["Status"] = "COMPLETED";
                            }
                            else
                            {
                                List<SortedDictionary<dynamic, dynamic>> filteredSurveyList = new List<SortedDictionary<dynamic, dynamic>>();
                                List<string> houseIDListDB = new List<string>();
                                houseIDListDB = new List<string>();
                                houseIDListDB = fileImpService.HouseIDList();
                                filteredSurveyList = totalSurveys.Where(m => !houseIDListDB.Contains(m["building_damage_assessment/hh_data/hh_address/hh_address_ward/house_ID"].Value)).ToList();
                                if (filteredSurveyList.Count == 0)
                                {
                                    ViewData["ErrorMessage"] = "The House ID in the file is missing in the database.Data import failed.";
                                    ViewData["Status"] = "FAILED";
                                }
                                else
                                {
                                    ViewData["ErrorMessage"] = "Data import failed.";
                                    ViewData["Status"] = "FAILED";
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "File with name '" + hidJsonFileName + "' has already been used for data import. Please choose another file!!";
                        ViewData["Status"] = "FAILED";
                    }
                }
                //}
                //else
                //{
                //    ViewData["ErrorMessage"] = "Please input JSON file format only!!";
                //    ViewData["Status"] = "FAILED";
                //}
            }
            catch (OracleException oe)
            {
                ViewData["ErrorMessage"] = "Data import failed.";
                ViewData["Status"] = "FAILED";
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Data import failed.";
                ViewData["Status"] = "FAILED";
                ExceptionManager.AppendLog(ex);
            }
            // redirect back to the index action to show the form once again
            DataTable dtFileStatusReport = new DataTable();
            List<string> jsonFiles = Directory.GetFiles(Server.MapPath("~/Files/Json/"), "*.json")
                                     .Select(path => Path.GetFileName(path))
                                     .ToList();
            dtFileStatusReport = objReportService.GetDataImportReportForDashboard(jsonFiles);
            ViewData["dtFileStatusReport"] = dtFileStatusReport;
            return View();
        }
        #endregion
        public JsonResult PostToMain()
        {
            bool success = false;
            string message = "";
            FileImportService fileImpService = new FileImportService();
            success = fileImpService.PostToMain();
            if (success)
            {
                message = "Data posted to main tables from the corresponding migration tables!!";
            }
            else
            {
                message = "Data posting to main tables failed!!";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
