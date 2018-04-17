using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using EntityFramework;
using System.Linq;
using System.Web;
namespace MIS.Services.Core
{
    public class Details
    {
        public string Cd { get; set; }
        public string DefinedCd { get; set; }
        public string Description { get; set; }
        public string DescriptionLoc { get; set; }
    }

    public enum DataType
    {
        Region,
        Zone,
        AllDistrict,
        District,
        VdcMun,
        Ward,
        Gender,
        Caste,
        CasteGroup,
        Religion,
        Education,
        MaritalStatus,
        Office,
        Position,
        Designation,
        SchoolType,
        ClassType,
        SubClass,
        Country,
        Currency,
        ColumnName,
        NewVDCMun
    }


    public static class GetData
    {
        #region MainCode
        public static int databaseHitCounter = 0;


        #region VariableDeclaration
        static Dictionary<string, Details> dictCurrency = new Dictionary<string, Details>();
        static List<SelectListItem> _currencyEng = new List<SelectListItem>();
        static List<SelectListItem> _currencyLoc = new List<SelectListItem>();
        static List<Details> currencyList = new List<Details>();

        static Dictionary<string, Details> dictCountry = new Dictionary<string, Details>();
        static List<SelectListItem> _countryEng = new List<SelectListItem>();
        static List<SelectListItem> _countryLoc = new List<SelectListItem>();
        static List<Details> countryList = new List<Details>();

        static Dictionary<string, Details> dictRegions = new Dictionary<string, Details>();
        static List<SelectListItem> _regionsEng = new List<SelectListItem>();
        static List<SelectListItem> _regionsLoc = new List<SelectListItem>();
        static List<Details> regionList = new List<Details>();



        static Dictionary<string, Details> dictZones = new Dictionary<string, Details>();
        static Dictionary<string, List<Details>> dictZoneByRegion = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _zonesByRegionEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _zonesByRegionLoc = new Dictionary<string, List<SelectListItem>>();
        static List<SelectListItem> _zonesEng = new List<SelectListItem>();
        static List<SelectListItem> _zonesLoc = new List<SelectListItem>();
        static List<Details> zoneList = new List<Details>();



        static Dictionary<string, Details> dictDistricts = new Dictionary<string, Details>();
        static Dictionary<string, List<Details>> dictDistrictsByZone = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _districtsByZoneEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _districtsByZoneLoc = new Dictionary<string, List<SelectListItem>>();
        static List<SelectListItem> _districtsEng = new List<SelectListItem>();
        static List<SelectListItem> _districtsLoc = new List<SelectListItem>();
        static List<Details> districtList = new List<Details>();

        static Dictionary<string, List<Details>> dictVdcByDistrict = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _vdcByDistrictEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _vdcByDistrictLoc = new Dictionary<string, List<SelectListItem>>();
        static List<Details> vdcList = new List<Details>();

        static Dictionary<string, Details> dictAllDistricts = new Dictionary<string, Details>();
        static Dictionary<string, List<Details>> dictDistrictsByAllZone = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _districtsByAllZoneEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _districtsByAllZoneLoc = new Dictionary<string, List<SelectListItem>>();
        static List<SelectListItem> _districtsAllEng = new List<SelectListItem>();
        static List<SelectListItem> _districtsAllLoc = new List<SelectListItem>();
        static List<Details> districtAllList = new List<Details>();



        static Dictionary<string, List<Details>> dictVdcByAllDistrict = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _vdcByAllDistrictEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _vdcByAllDistrictLoc = new Dictionary<string, List<SelectListItem>>();
        static List<Details> AllvdcList = new List<Details>();



        static Dictionary<string, List<Details>> dictWardByVdc = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _wards = new Dictionary<string, List<SelectListItem>>();
        static List<Details> wardList = new List<Details>();


        static Dictionary<string, Details> dictGenders = new Dictionary<string, Details>();
        static List<SelectListItem> _gendersEng = new List<SelectListItem>();
        static List<SelectListItem> _gendersLoc = new List<SelectListItem>();
        static List<Details> genderList = new List<Details>();



        static Dictionary<string, Details> dictCastes = new Dictionary<string, Details>();
        static Dictionary<string, List<Details>> dictCasteByCasteGrp = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _casteByCasteGrpEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _casteByCasteGrpLoc = new Dictionary<string, List<SelectListItem>>();

        static List<SelectListItem> _castesEng = new List<SelectListItem>();
        static List<SelectListItem> _castesLoc = new List<SelectListItem>();
        static List<Details> casteList = new List<Details>();



        static Dictionary<string, Details> dictCasteGroups = new Dictionary<string, Details>();
        static List<SelectListItem> _casteGroupsEng = new List<SelectListItem>();
        static List<SelectListItem> _casteGroupsLoc = new List<SelectListItem>();
        static List<Details> casteGroupList = new List<Details>();



        static Dictionary<string, Details> dictEducations = new Dictionary<string, Details>();
        static List<SelectListItem> _educationsEng = new List<SelectListItem>();
        static List<SelectListItem> _educationsLoc = new List<SelectListItem>();
        static List<Details> educationList = new List<Details>();



        static Dictionary<string, Details> dictReligions = new Dictionary<string, Details>();
        static List<SelectListItem> _religionsEng = new List<SelectListItem>();
        static List<SelectListItem> _religionsLoc = new List<SelectListItem>();
        static List<Details> religionList = new List<Details>();


        static Dictionary<string, Details> dictMaritalStatuses = new Dictionary<string, Details>();
        static List<SelectListItem> _maritalStatusEng = new List<SelectListItem>();
        static List<SelectListItem> _maritalStatusLoc = new List<SelectListItem>();
        static List<Details> maritalStatusList = new List<Details>();


        static Dictionary<string, Details> dictOffices = new Dictionary<string, Details>();
        static List<SelectListItem> _officesEng = new List<SelectListItem>();
        static List<SelectListItem> _officesLoc = new List<SelectListItem>();
        static List<Details> officeList = new List<Details>();


        static Dictionary<string, Details> dictPositions = new Dictionary<string, Details>();
        static List<SelectListItem> _positionsEng = new List<SelectListItem>();
        static List<SelectListItem> _positionsLoc = new List<SelectListItem>();
        static List<Details> positionList = new List<Details>();


        static Dictionary<string, List<Details>> dictDesignationsByPosition = new Dictionary<string, List<Details>>();
        static Dictionary<string, List<SelectListItem>> _designationsByPositionEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _designationsByPositionLoc = new Dictionary<string, List<SelectListItem>>();
        static List<Details> designationList = new List<Details>();



        static Dictionary<string, Details> dictSchoolTypes = new Dictionary<string, Details>();
        static List<SelectListItem> _schoolTypesEng = new List<SelectListItem>();
        static List<SelectListItem> _schoolTypesLoc = new List<SelectListItem>();
        static List<Details> schoolTypeList = new List<Details>();


        static Dictionary<string, Details> dictClassTypes = new Dictionary<string, Details>();
        static List<SelectListItem> _classTypesEng = new List<SelectListItem>();
        static List<SelectListItem> _classTypesLoc = new List<SelectListItem>();
        static List<Details> classTypeList = new List<Details>();



        static Dictionary<string, Details> dictSubClasses = new Dictionary<string, Details>();
        static List<SelectListItem> _subClassesEng = new List<SelectListItem>();
        static List<SelectListItem> _subClassesLoc = new List<SelectListItem>();
        static List<Details> subClassesList = new List<Details>();

        static List<Details> NewvdcList = new List<Details>();
        static Dictionary<string, List<SelectListItem>> _NewvdcByDistrictEng = new Dictionary<string, List<SelectListItem>>();
        static Dictionary<string, List<SelectListItem>> _NewvdcByDistrictLoc = new Dictionary<string, List<SelectListItem>>();

        #endregion

        private static void RetrieveDataFromDatabase()
        {
            DataTable dt = new DataTable();
            try
            {

                if (HttpContext.Current.Session["UpdatedType"] != null)
                {
                    DataType dataType = (DataType)HttpContext.Current.Session["UpdatedType"];
                    switch (dataType)
                    {
                        case DataType.Caste:
                            LoadCaste();
                            break;
                        case DataType.CasteGroup:
                            LoadCasteGroups();
                            break;
                        case DataType.Designation:
                            LoadDesignations();
                            break;
                        case DataType.Country:
                            LoadCountry();
                            break;
                        //case DataType.Currency:
                        //    LoadCurrency();
                        //    break;
                        case DataType.District:
                            LoadDistricts();
                            break;
                        case DataType.AllDistrict:
                            LoadAllDistricts();
                            break;
                        case DataType.ClassType:
                            LoadClassType();
                            break;
                        case DataType.Education:
                            LoadEducations();
                            break;
                        case DataType.Gender:
                            LoadGender();
                            break;
                        case DataType.MaritalStatus:
                            LoadMaritialStatuses();
                            break;
                        case DataType.Office:
                            LoadOffices();
                            break;
                        case DataType.Position:
                            LoadPositions();
                            break;
                        case DataType.Region:
                            LoadRegions();
                            break;
                        case DataType.Religion:
                            LoadReligions();
                            break;
                        case DataType.SchoolType:
                            LoadSchoolTypes();
                            break;
                        //case DataType.SubClass:
                        //    LoadSubClass();
                        //    break;
                        case DataType.VdcMun:
                            LoadVdcMun();
                            break;
                        case DataType.Ward:
                            LoadWards();
                            break;
                        case DataType.Zone:
                            LoadZones();
                            break;
                        case DataType.NewVDCMun:
                            LoadNewVdcMun();
                            break;
                    }
                    HttpContext.Current.Session["UpdatedType"] = null;
                }
                else
                {

                    #region Reset all dictionaries and lists before populating
                    LoadCaste();
                    LoadCasteGroups();
                    LoadClassType();
                    LoadDesignations();
                    LoadCountry();
                    LoadAllDistricts();
                    //LoadCurrency();
                    LoadDistricts();
                    LoadEducations();
                    LoadGender();
                    LoadMaritialStatuses();
                    LoadOffices();
                    LoadPositions();
                    LoadRegions();
                    LoadReligions();
                    LoadSchoolTypes();
                    //LoadSubClass();
                    LoadVdcMun();
                    //LoadWards();
                    LoadZones();
                    LoadNewVdcMun(); 
                    #endregion
                }
                string fileName = HttpContext.Current.Server.MapPath("/Files") + "performance.txt";
                System.IO.File.WriteAllLines(fileName, new string[] { " Total no. of times database has been hit through GetData file (since last application restart): ", databaseHitCounter.ToString() });

                HttpContext.Current.Session["UpdateGlobalData"] = "false";

            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionManager.AppendLog(exc);
            }
            finally
            {

            }

        }

        private static void LoadCountry()
        {
            dictCountry.Clear();
            _countryEng.Clear();
            _countryLoc.Clear();
            countryList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Country
                string countryCmd = "select COUNTRY_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_COUNTRY where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = countryCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;

                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictCountry.Add(r["COUNTRY_CD"].ToString(), new Details
                    {
                        Cd = r["COUNTRY_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                countryList.AddRange(dictCountry.Values);
                _countryEng.AddRange(dictCountry.Select(dist => new SelectListItem { Text = dist.Value.Description, Value = dist.Value.DefinedCd }).ToList());
                _countryLoc.AddRange(dictCountry.Select(dist => new SelectListItem { Text = dist.Value.DescriptionLoc, Value = dist.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadCurrency()
        {
            dictCurrency.Clear();
            _currencyEng.Clear();
            _currencyLoc.Clear();
            currencyList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Currency
                string currencyCmd = "select CURRENCY_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_CURRENCY where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = currencyCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;

                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictCurrency.Add(r["CURRENCY_CD"].ToString(), new Details
                    {
                        Cd = r["CURRENCY_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                currencyList.AddRange(dictCurrency.Values);
                _currencyEng.AddRange(dictCurrency.Select(dist => new SelectListItem { Text = dist.Value.Description, Value = dist.Value.DefinedCd }).ToList());
                _currencyLoc.AddRange(dictCurrency.Select(dist => new SelectListItem { Text = dist.Value.DescriptionLoc, Value = dist.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadMaritialStatuses()
        {
            dictMaritalStatuses.Clear();
            _maritalStatusEng.Clear();
            _maritalStatusLoc.Clear();
            maritalStatusList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region MaritalStatuses
                string maritalStatusCmd = "select MARITAL_STATUS_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_MARITAL_STATUS where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = maritalStatusCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictMaritalStatuses.Add(r["MARITAL_STATUS_CD"].ToString(), new Details
                    {
                        Cd = r["MARITAL_STATUS_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                maritalStatusList.AddRange(dictMaritalStatuses.Values);
                _maritalStatusEng.AddRange(dictMaritalStatuses.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _maritalStatusLoc.AddRange(dictMaritalStatuses.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadOffices()
        {
            dictOffices.Clear();
            _officesEng.Clear();
            _officesLoc.Clear();
            officeList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Offices
                string officeCmd = "select OFFICE_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_OFFICE where 1=1  order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = officeCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictOffices.Add(r["OFFICE_CD"].ToString(), new Details
                    {
                        Cd = r["OFFICE_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                officeList.AddRange(dictOffices.Values);
                _officesEng.AddRange(dictOffices.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _officesLoc.AddRange(dictOffices.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadPositions()
        {
            dictPositions.Clear();
            _positionsEng.Clear();
            _positionsLoc.Clear();
            positionList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Positions
                string positionCmd = "select POSITION_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_POSITION where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = positionCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                try
                {
                    foreach (DataRow r in dt.Rows)
                        dictPositions.Add(r["POSITION_CD"].ToString(), new Details
                        {
                            Cd = r["POSITION_CD"].ToString(),
                            DefinedCd = r["DEFINED_CD"].ToString(),
                            Description = r["DESC_ENG"].ToString(),
                            DescriptionLoc = r["DESC_LOC"].ToString()
                        });
                }
                catch (Exception excInner)
                {
                    //throw new Exception("Duplicate Entry Found. Details : " + excInner.Message);
                    ExceptionHandler.ExceptionManager.AppendLog(excInner);
                }
                positionList.AddRange(dictPositions.Values);
                _positionsEng.AddRange(dictPositions.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _positionsLoc.AddRange(dictPositions.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadDesignations()
        {
            dictDesignationsByPosition.Clear();
            _designationsByPositionEng.Clear();
            _designationsByPositionLoc.Clear();
            designationList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Designations
                string designationCmd = "select T1.DESIGNATION_CD, T1.DESC_ENG, T1.DESC_LOC, T1.DEFINED_CD, T2.POSITION_CD FROM MIS_DESIGNATION T1 INNER JOIN  MIS_POSITION_DESIGNATION T2 ON T1.DESIGNATION_CD = T2.DESIGNATION_CD where 1=1 order by T1.DEFINED_CD";


                dt = service.GetDataTable(new
                {
                    query = designationCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // Get unique position codes
                List<string> positions = dt.AsEnumerable().Select(r => r["POSITION_CD"].ToString()).Distinct().ToList();

                foreach (string p in positions)
                {
                    dictDesignationsByPosition.Add(p,
                        dt.AsEnumerable().Where(row => row["POSITION_CD"].ToString() == p)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["DESIGNATION_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _designationsByPositionEng.Add(p, dictDesignationsByPosition[p].Select(v => new SelectListItem { Text = v.Description, Value = v.DefinedCd }).ToList());
                    _designationsByPositionLoc.Add(p, dictDesignationsByPosition[p].Select(v => new SelectListItem { Text = v.DescriptionLoc, Value = v.DefinedCd }).ToList());
                }


                foreach (string k in dictDesignationsByPosition.Keys)
                {
                    designationList.AddRange(dictDesignationsByPosition[k]);
                }

                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadSchoolTypes()
        {
            dictSchoolTypes.Clear();
            _schoolTypesEng.Clear();
            _schoolTypesLoc.Clear();
            schoolTypeList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region SchoolType
                string schoolTypeCmd = "select SCHOOL_TYPE_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_SCHOOL_TYPE where 1=1 order by to_number(nvl(DEFINED_CD,0))";
                dt = service.GetDataTable(new
                {
                    query = schoolTypeCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictSchoolTypes.Add(r["SCHOOL_TYPE_CD"].ToString(), new Details
                    {
                        Cd = r["SCHOOL_TYPE_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                schoolTypeList.AddRange(dictSchoolTypes.Values);
                _schoolTypesEng.AddRange(dictSchoolTypes.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _schoolTypesLoc.AddRange(dictSchoolTypes.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadClassType()
        {
            dictClassTypes.Clear();
            _classTypesEng.Clear();
            _classTypesLoc.Clear();
            classTypeList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region ClassType
                string classTypeCmd = "select CLASS_TYPE_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_CLASS_TYPE where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = classTypeCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictClassTypes.Add(r["CLASS_TYPE_CD"].ToString(), new Details
                    {
                        Cd = r["CLASS_TYPE_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                classTypeList.AddRange(dictClassTypes.Values);
                _classTypesEng.AddRange(dictClassTypes.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _classTypesLoc.AddRange(dictClassTypes.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadSubClass()
        {
            dictSubClasses.Clear();
            _subClassesEng.Clear();
            _subClassesLoc.Clear();
            subClassesList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region SubClass
                string subClassCmd = "select POS_SUB_CLASS_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_POSITION_SUB_CLASS_MST where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = subClassCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictSubClasses.Add(r["POS_SUB_CLASS_CD"].ToString(), new Details
                    {
                        Cd = r["POS_SUB_CLASS_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                subClassesList.AddRange(dictSubClasses.Values);
                _subClassesEng.AddRange(dictSubClasses.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _subClassesLoc.AddRange(dictSubClasses.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadRegions()
        {
            dictRegions.Clear();
            _regionsEng.Clear();
            _regionsLoc.Clear();
            regionList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Regions
                string regionCmd = "select REG_ST_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_REGION_STATE where 1=1 order by to_number(nvl(DEFINED_CD,0))";
                dt = service.GetDataTable(new
                {
                    query = regionCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictRegions.Add(r["REG_ST_CD"].ToString(), new Details
                    {
                        Cd = r["REG_ST_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                regionList.AddRange(dictRegions.Values);
                _regionsEng.AddRange(dictRegions.Select(region => new SelectListItem { Text = region.Value.Description, Value = region.Value.DefinedCd }).ToList());
                _regionsLoc.AddRange(dictRegions.Select(region => new SelectListItem { Text = region.Value.DescriptionLoc, Value = region.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadZones()
        {
            dictZones.Clear();
            dictZoneByRegion.Clear();
            _zonesByRegionEng.Clear();
            _zonesByRegionLoc.Clear();
            _zonesEng.Clear();
            _zonesLoc.Clear();
            zoneList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Zones
                string zoneCmd = "select ZONE_CD, DESC_ENG, DESC_LOC, DEFINED_CD, REG_ST_CD FROM MIS_ZONE where 1=1 order by to_number(nvl(DEFINED_CD,0))";
                dt = service.GetDataTable(new
                {
                    query = zoneCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictZones.Add(r["ZONE_CD"].ToString(), new Details
                    {
                        Cd = r["ZONE_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                zoneList.AddRange(dictZones.Values);
                _zonesEng.AddRange(dictZones.Select(zone => new SelectListItem { Text = zone.Value.Description, Value = zone.Value.DefinedCd }).ToList());
                _zonesLoc.AddRange(dictZones.Select(zone => new SelectListItem { Text = zone.Value.DescriptionLoc, Value = zone.Value.DefinedCd }).ToList());

                // Create mappings
                List<string> regions = dt.AsEnumerable().Select(r => r["REG_ST_CD"].ToString()).Distinct().ToList();

                foreach (string r in regions)
                {
                    dictZoneByRegion.Add(r,
                        dt.AsEnumerable().Where(row => row["REG_ST_CD"].ToString() == r)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["ZONE_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _zonesByRegionEng.Add(r, dictZoneByRegion[r].Select(z => new SelectListItem { Text = z.Description, Value = z.DefinedCd }).ToList());
                    _zonesByRegionLoc.Add(r, dictZoneByRegion[r].Select(z => new SelectListItem { Text = z.DescriptionLoc, Value = z.DefinedCd }).ToList());
                }
                //Mapping with district
                //  LoadDistricts();
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadDistricts()
        {
            dictDistricts.Clear();
            dictDistrictsByZone.Clear();
            _districtsByZoneEng.Clear();
            _districtsByZoneLoc.Clear();
            _districtsEng.Clear();
            _districtsLoc.Clear();
            districtList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Districts
                string districtCmd = "select DISTRICT_CD, DESC_ENG, DESC_LOC, DEFINED_CD, ZONE_CD FROM MIS_DISTRICT where 1=1 and IS_EARTHQUAKE_AFFECT_DISTRICT = 'Y' order by DESC_ENG ";
                dt = service.GetDataTable(new
                {
                    query = districtCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;

                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictDistricts.Add(r["DISTRICT_CD"].ToString(), new Details
                    {
                        Cd = r["DISTRICT_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                districtList.AddRange(dictDistricts.Values);
                _districtsEng.AddRange(dictDistricts.Select(dist => new SelectListItem { Text = dist.Value.Description, Value = dist.Value.DefinedCd }).ToList());
                _districtsLoc.AddRange(dictDistricts.Select(dist => new SelectListItem { Text = dist.Value.DescriptionLoc, Value = dist.Value.DefinedCd }).ToList());

                // Create mappings
                List<string> zones = dt.AsEnumerable().Select(r => r["ZONE_CD"].ToString()).Distinct().ToList();

                foreach (string z in zones)
                {
                    dictDistrictsByZone.Add(z,
                        dt.AsEnumerable().Where(row => row["ZONE_CD"].ToString() == z)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["DISTRICT_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _districtsByZoneEng.Add(z, dictDistrictsByZone[z].Select(d => new SelectListItem { Text = d.Description, Value = d.DefinedCd }).ToList());
                    _districtsByZoneLoc.Add(z, dictDistrictsByZone[z].Select(d => new SelectListItem { Text = d.DescriptionLoc, Value = d.DefinedCd }).ToList());
                }

                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadAllDistricts()
        {
            dictAllDistricts.Clear();
            dictDistrictsByAllZone.Clear();
            _districtsByAllZoneEng.Clear();
            _districtsByAllZoneLoc.Clear();
            _districtsAllEng.Clear();
            _districtsAllLoc.Clear();
            districtAllList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region AllDistricts
                string districtCmd = "select DISTRICT_CD, DESC_ENG, DESC_LOC, DEFINED_CD, ZONE_CD FROM MIS_DISTRICT  where 1=1 order by DEFINED_CD ";
                dt = service.GetDataTable(new
                {
                    query = districtCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;

                // convert to dictionary
                
                foreach (DataRow r in dt.Rows)
                    dictAllDistricts.Add(r["DISTRICT_CD"].ToString(), new Details
                    {
                        Cd = r["DISTRICT_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                districtAllList.AddRange(dictAllDistricts.Values);
                _districtsAllEng.AddRange(dictAllDistricts.Select(dist => new SelectListItem { Text = dist.Value.Description, Value = dist.Value.DefinedCd }).ToList());
                _districtsAllLoc.AddRange(dictAllDistricts.Select(dist => new SelectListItem { Text = dist.Value.DescriptionLoc, Value = dist.Value.DefinedCd }).ToList());

                // Create mappings
                List<string> zones = dt.AsEnumerable().Select(r => r["ZONE_CD"].ToString()).Distinct().ToList();

                foreach (string z in zones)
                {
                    dictDistrictsByAllZone.Add(z,
                        dt.AsEnumerable().Where(row => row["ZONE_CD"].ToString() == z)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["DISTRICT_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _districtsByAllZoneEng.Add(z, dictDistrictsByAllZone[z].Select(d => new SelectListItem { Text = d.Description, Value = d.DefinedCd }).ToList());
                    _districtsByAllZoneLoc.Add(z, dictDistrictsByAllZone[z].Select(d => new SelectListItem { Text = d.DescriptionLoc, Value = d.DefinedCd }).ToList());
                }

                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadGender()
        {
            dictGenders.Clear();
            _gendersEng.Clear();
            _gendersLoc.Clear();
            genderList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Genders
                string genderCmd = "select GENDER_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_GENDER where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = genderCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictGenders.Add(r["GENDER_CD"].ToString(), new Details
                    {
                        Cd = r["GENDER_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                genderList.AddRange(dictGenders.Values);
                _gendersEng.AddRange(dictGenders.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _gendersLoc.AddRange(dictGenders.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadVdcMun()
        {
            dictVdcByDistrict.Clear();
            _vdcByDistrictEng.Clear();
            _vdcByDistrictLoc.Clear();
            vdcList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region VdcMun
                string vdcCmd = "select VDC_MUN_CD, DESC_ENG, DESC_LOC, DEFINED_CD, DISTRICT_CD FROM MIS_VDC_MUNICIPALITY where 1=1 and APPROVED='Y' order by DESC_ENG asc";
                dt = service.GetDataTable(new
                {
                    query = vdcCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // Get unique district codes
                List<string> dists = dt.AsEnumerable().Select(r => r["DISTRICT_CD"].ToString()).Distinct().ToList();

                foreach (string d in dists)
                {
                    dictVdcByDistrict.Add(d,
                        dt.AsEnumerable().Where(row => row["DISTRICT_CD"].ToString() == d)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["VDC_MUN_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _vdcByDistrictEng.Add(d, dictVdcByDistrict[d].Select(v => new SelectListItem { Text = v.Description, Value = v.DefinedCd }).ToList());
                    _vdcByDistrictLoc.Add(d, dictVdcByDistrict[d].Select(v => new SelectListItem { Text = v.DescriptionLoc, Value = v.DefinedCd }).ToList());
                }


                foreach (string k in dictVdcByDistrict.Keys)
                {
                    vdcList.AddRange(dictVdcByDistrict[k]);
                }

                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadNewVdcMun()
        {
            dictVdcByDistrict.Clear();
            _NewvdcByDistrictEng.Clear();
            _NewvdcByDistrictLoc.Clear();
            NewvdcList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region VdcMun
                string vdcCmd = "select NMUNICIPALITY_CD, DESC_ENG, DESC_LOC, DEFINED_CD, DISTRICT_CD FROM NHRS_NMUNICIPALITY where 1=1 and APPROVED='Y' order by DESC_ENG asc";
                dt = service.GetDataTable(new
                {
                    query = vdcCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // Get unique district codes
                List<string> dists = dt.AsEnumerable().Select(r => r["DISTRICT_CD"].ToString()).Distinct().ToList();

                foreach (string d in dists)
                {
                    dictVdcByDistrict.Add(d,
                        dt.AsEnumerable().Where(row => row["DISTRICT_CD"].ToString() == d)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["NMUNICIPALITY_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _NewvdcByDistrictEng.Add(d, dictVdcByDistrict[d].Select(v => new SelectListItem { Text = v.Description, Value = v.DefinedCd }).ToList());
                    _NewvdcByDistrictLoc.Add(d, dictVdcByDistrict[d].Select(v => new SelectListItem { Text = v.DescriptionLoc, Value = v.DefinedCd }).ToList());
                }


                foreach (string k in dictVdcByDistrict.Keys)
                {
                    NewvdcList.AddRange(dictVdcByDistrict[k]);
                }

                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }


        private static void LoadWards()
        {
            dictWardByVdc.Clear();
            _wards.Clear();
            wardList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Wards - Commented for now
                //string wardCmd = "select WARD_NO, VDC_MUN_CD FROM MIS_WARD where 1=1 ";
                //dt = service.GetDataTable(new
                //{
                //    query = wardCmd,
                //    args = new
                //    {

                //    }
                //});

                //databaseHitCounter++;
                //// Get unique vdc codes
                //List<string> vdcs = dt.AsEnumerable().Select(r => r["VDC_MUN_CD"].ToString()).Distinct().ToList();
                //DateTime dat = DateTime.Now;
                //foreach (string w in vdcs)
                //{
                //    dictWardByVdc.Add(w,
                //        dt.AsEnumerable().Where(row => row["VDC_MUN_CD"].ToString() == w)
                //        .Select(cRow => new Details
                //        {
                //            Cd = cRow["WARD_NO"].ToString(),
                //            DefinedCd = cRow["WARD_NO"].ToString(),
                //            Description = cRow["WARD_NO"].ToString(),
                //            DescriptionLoc = cRow["WARD_NO"].ToString()
                //        }).ToList()
                //        );
                //    _wards.Add(w, dictWardByVdc[w].Select(v => new SelectListItem { Text = v.Description, Value = v.DefinedCd }).ToList());
                //}

                //TimeSpan ts = DateTime.Now - dat;

                //foreach (string k in dictWardByVdc.Keys)
                //{
                //    wardList.AddRange(dictWardByVdc[k]);
                //}
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadCaste()
        {
            dictCastes.Clear();
            dictCasteByCasteGrp.Clear();
            _casteByCasteGrpEng.Clear();
            _casteByCasteGrpLoc.Clear();
            _castesEng.Clear();
            _castesLoc.Clear();
            casteList.Clear();
             
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Castes
                string casteCmd = "select CASTE_CD, DESC_ENG, DESC_LOC, DEFINED_CD, CASTE_GROUP_CD FROM MIS_CASTE where 1=1 order by to_number(nvl(DEFINED_CD,0))";
                dt = service.GetDataTable(new
                {
                    query = casteCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictCastes.Add(r["CASTE_CD"].ToString(), new Details
                    {
                        Cd = r["CASTE_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });

                casteList.AddRange(dictCastes.Values);
                _castesEng.AddRange(dictCastes.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _castesLoc.AddRange(dictCastes.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());

                // Create mappings
                List<string> casteGrps = dt.AsEnumerable().Select(r => r["Caste_Group_cd"].ToString()).Distinct().ToList();

                foreach (string cg in casteGrps)
                {
                    dictCasteByCasteGrp.Add(cg,
                        dt.AsEnumerable().Where(row => row["Caste_Group_cd"].ToString() == cg)
                        .Select(cRow => new Details
                        {
                            Cd = cRow["CASTE_CD"].ToString(),
                            DefinedCd = cRow["DEFINED_CD"].ToString(),
                            Description = cRow["DESC_ENG"].ToString(),
                            DescriptionLoc = cRow["DESC_LOC"].ToString()
                        }).ToList()
                        );
                    _casteByCasteGrpEng.Add(cg, dictCasteByCasteGrp[cg].Select(d => new SelectListItem { Text = d.Description, Value = d.DefinedCd }).ToList());
                    _casteByCasteGrpLoc.Add(cg, dictCasteByCasteGrp[cg].Select(d => new SelectListItem { Text = d.DescriptionLoc, Value = d.DefinedCd }).ToList());
                }
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadCasteGroups()
        {
            dictCasteGroups.Clear();
            _casteGroupsEng.Clear();
            _casteGroupsLoc.Clear();
            casteGroupList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region CasteGroups
                string casteGroupCmd = "select CASTE_GROUP_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_CASTE_GROUP where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = casteGroupCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictCasteGroups.Add(r["CASTE_GROUP_CD"].ToString(), new Details
                    {
                        Cd = r["CASTE_GROUP_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                casteGroupList.AddRange(dictCasteGroups.Values);
                _casteGroupsEng.AddRange(dictCasteGroups.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _casteGroupsLoc.AddRange(dictCasteGroups.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadEducations()
        {
            dictEducations.Clear();
            _educationsEng.Clear();
            _educationsLoc.Clear();
            educationList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Educations
                string educationCmd = "select class_type_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_Class_Type where 1=1 order by DEFINED_CD";
                dt = service.GetDataTable(new
                {
                    query = educationCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictEducations.Add(r["class_type_cd"].ToString(), new Details
                    {
                        Cd = r["class_type_cd"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                educationList.AddRange(dictEducations.Values);
                _educationsEng.AddRange(dictEducations.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.Cd }).ToList());
                _educationsLoc.AddRange(dictEducations.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.Cd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        private static void LoadReligions()
        {
            dictReligions.Clear();
            _religionsEng.Clear();
            _religionsLoc.Clear();
            religionList.Clear();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                #region Religions
                string religionCmd = "select RELIGION_CD, DESC_ENG, DESC_LOC, DEFINED_CD FROM MIS_RELIGION where 1=1 order by to_number(nvl(DEFINED_CD,0))";
                dt = service.GetDataTable(new
                {
                    query = religionCmd,
                    args = new
                    {

                    }
                });

                databaseHitCounter++;
                // convert to dictionary
                foreach (DataRow r in dt.Rows)
                    dictReligions.Add(r["RELIGION_CD"].ToString(), new Details
                    {
                        Cd = r["RELIGION_CD"].ToString(),
                        DefinedCd = r["DEFINED_CD"].ToString(),
                        Description = r["DESC_ENG"].ToString(),
                        DescriptionLoc = r["DESC_LOC"].ToString()
                    });
                religionList.AddRange(dictReligions.Values);

                _religionsEng.AddRange(dictReligions.Select(gen => new SelectListItem { Text = gen.Value.Description, Value = gen.Value.DefinedCd }).ToList());
                _religionsLoc.AddRange(dictReligions.Select(gen => new SelectListItem { Text = gen.Value.DescriptionLoc, Value = gen.Value.DefinedCd }).ToList());
                #endregion
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
        }

        public static List<SelectListItem> AllRegions(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _regionsEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _regionsLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _regionsEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }

        public static List<SelectListItem> AllZones(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _zonesEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _zonesLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _zonesEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }
        public static List<SelectListItem> ZoneByRegion(string regionCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _zonesByRegionEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
            {
                if (_zonesByRegionLoc.ContainsKey(regionCd))
                    foreach (SelectListItem li in _zonesByRegionLoc[regionCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            else
            {
                if (_zonesByRegionEng.ContainsKey(regionCd))
                    foreach (SelectListItem li in _zonesByRegionEng[regionCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            return lst;
        }


        public static List<SelectListItem> AllDistricts(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _districtsEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _districtsLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _districtsEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }

        public static List<SelectListItem> Districts(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _districtsAllEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _districtsAllLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _districtsAllEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }

        public static List<SelectListItem> DistrictbyZone(string zoneCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _districtsByZoneEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
            {
                if (_districtsByZoneLoc.ContainsKey(zoneCd))
                    foreach (SelectListItem li in _districtsByZoneLoc[zoneCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            else
            {
                if (_districtsByZoneEng.ContainsKey(zoneCd))
                    foreach (SelectListItem li in _districtsByZoneEng[zoneCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            return lst;
        }

        public static List<SelectListItem> ZonebyRegState(string regStateCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _zonesByRegionEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
            {
                if (_zonesByRegionLoc.ContainsKey(regStateCd))
                    foreach (SelectListItem li in _zonesByRegionLoc[regStateCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            else
            {
                if (_zonesByRegionEng.ContainsKey(regStateCd))
                    foreach (SelectListItem li in _zonesByRegionEng[regStateCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            return lst;
        }

        public static List<SelectListItem> VdcByDistrict(string districtCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _vdcByDistrictEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
            {
                if (_vdcByDistrictLoc.ContainsKey(districtCd))
                    foreach (SelectListItem li in _vdcByDistrictLoc[districtCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            else
            {
                if (_vdcByDistrictEng.ContainsKey(districtCd))
                    foreach (SelectListItem li in _vdcByDistrictEng[districtCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            return lst;
        }

        public static List<SelectListItem> NewVdcByDistrict(string districtCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _NewvdcByDistrictEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
            {
                if (_NewvdcByDistrictLoc.ContainsKey(districtCd))
                    foreach (SelectListItem li in _NewvdcByDistrictLoc[districtCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            else
            {
                if (_NewvdcByDistrictEng.ContainsKey(districtCd))
                    foreach (SelectListItem li in _NewvdcByDistrictEng[districtCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            return lst;
        }

        public static List<SelectListItem> WardByVdc(string vdcMunCd)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _wards.Count == 0)
                RetrieveDataFromDatabase();

            if (_wards.ContainsKey(vdcMunCd))
                foreach (SelectListItem li in _wards[vdcMunCd])
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst;
        }

        public static List<SelectListItem> CasteByCasteGrp(string casteGrpCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _districtsByZoneEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
            {
                if (_casteByCasteGrpLoc.ContainsKey(casteGrpCd))
                    foreach (SelectListItem li in _casteByCasteGrpLoc[casteGrpCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            else
            {
                if (_casteByCasteGrpEng.ContainsKey(casteGrpCd))
                    foreach (SelectListItem li in _casteByCasteGrpEng[casteGrpCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
            }
            return lst;
        }


        public static List<SelectListItem> AllGenders(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _gendersEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _gendersLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _gendersEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }


        public static List<SelectListItem> AllCastes(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _castesEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _castesLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _castesEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }


        public static List<SelectListItem> AllCasteGroups(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _casteGroupsEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _casteGroupsLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _casteGroupsEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }


        public static List<SelectListItem> AllEducations(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _educationsEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _educationsLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _educationsEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }

        public static List<SelectListItem> AllReligions(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _religionsEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _religionsLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _religionsEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }



        public static List<SelectListItem> AllMaritalStatus(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _maritalStatusEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _maritalStatusLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _maritalStatusEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }

        public static List<SelectListItem> AllOffices(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _officesEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _officesLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _officesEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }


        public static List<SelectListItem> AllPositions(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _positionsEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _positionsLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _positionsEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }

        public static List<SelectListItem> AllDesignationByPosition(string positionCd, string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _designationsByPositionEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _designationsByPositionLoc[positionCd])
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
            {
                if (_designationsByPositionEng.ContainsKey(positionCd))
                {
                    foreach (SelectListItem li in _designationsByPositionEng[positionCd])
                    {
                        SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                        lst.Add(sli);
                    }
                }
            }
            return lst;
        }


        public static List<SelectListItem> AllSchoolTypes(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _schoolTypesEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _schoolTypesLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _schoolTypesEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }


        public static List<SelectListItem> AllClassTypes(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _classTypesEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _classTypesLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _classTypesEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }


        public static List<SelectListItem> AllSubClasses(string language)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (HttpContext.Current.Session["UpdatedType"] != null || _subClassesEng.Count == 0)
                RetrieveDataFromDatabase();
            if (language.ToLower() == "nepali")
                foreach (SelectListItem li in _subClassesLoc)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            else
                foreach (SelectListItem li in _subClassesEng)
                {
                    SelectListItem sli = new SelectListItem { Text = li.Text, Value = li.Value };
                    lst.Add(sli);
                }
            return lst.ToList();
        }
        #endregion


        public static string GetCodeFor(DataType dataType, string definedCd, string parentDefCd = "")
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            if (string.IsNullOrWhiteSpace(definedCd))
                return definedCd;
            switch (dataType)
            {

                case DataType.NewVDCMun:
                    return NewvdcList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Currency:
                    return currencyList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Country:
                    return countryList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Region:
                    return regionList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Zone:
                    return zoneList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.District:
                    return districtList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.AllDistrict:
                    return districtAllList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.VdcMun:
                    return vdcList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Ward:
                    return wardList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Gender:
                    return genderList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Caste:
                    return casteList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.CasteGroup:
                    return casteGroupList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Religion:
                    return religionList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Education:
                    return educationList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.MaritalStatus:
                    return maritalStatusList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Office:
                    return officeList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Position:
                    return positionList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.Designation:
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, parentDefCd)].Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.SchoolType:
                    return schoolTypeList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.ClassType:
                    return classTypeList.Single(x => x.DefinedCd == definedCd).Cd;
                case DataType.SubClass:
                    return subClassesList.Single(x => x.DefinedCd == definedCd).Cd;
            }

            throw new Exception("Provided DEFINED CD not found in the data type specified.");

        }
        public static string GetDefinedCodeFor(DataType dataType, string cd, string parentDefCd = "")
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            try
            {
                if (string.IsNullOrWhiteSpace(cd))
                    return cd;
                switch (dataType)
                {
                    case DataType.Currency:
                        return currencyList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Country:
                        return countryList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Region:
                        return regionList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Zone:
                        return zoneList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.District:
                        return districtList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.AllDistrict:
                        return districtAllList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.VdcMun:
                        return vdcList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Ward:
                        return wardList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Gender:
                        return genderList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Caste:
                        return casteList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.CasteGroup:
                        return casteGroupList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Religion:
                        return religionList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Education:
                        return educationList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.MaritalStatus:
                        return maritalStatusList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Office:
                        return officeList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Position:
                        return positionList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.Designation:
                        //return designationList.Where(x => x.Cd == cd).Distinct().Single().DefinedCd;
                        return dictDesignationsByPosition[GetCodeFor(DataType.Position, parentDefCd)].Single(x => x.Cd == cd).Cd;
                    case DataType.SchoolType:
                        return schoolTypeList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.ClassType:
                        return classTypeList.Single(x => x.Cd == cd).DefinedCd;
                    case DataType.SubClass:
                        return subClassesList.Single(x => x.Cd == cd).DefinedCd;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            return "";
            //throw new Exception("Provided CD not found in the data type specified.");

        }

        public static string GetDescEngFor(DataType dataType, string cd, string parentDefCd = "")
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            try
            {
                if (string.IsNullOrWhiteSpace(cd))
                    return cd;
                switch (dataType)
                {
                    case DataType.Currency:
                        return currencyList.Single(x => x.Cd == cd).Description;
                    case DataType.Country:
                        return countryList.Single(x => x.Cd == cd).Description;
                    case DataType.Region:
                        return regionList.Single(x => x.Cd == cd).Description;
                    case DataType.Zone:
                        return zoneList.Single(x => x.Cd == cd).Description;
                    case DataType.District:
                        return districtList.Single(x => x.Cd == cd).Description;
                    case DataType.VdcMun:
                        return vdcList.Single(x => x.Cd == cd).Description;
                    case DataType.Ward:
                        return wardList.Single(x => x.Cd == cd).Description;
                    case DataType.Gender:
                        return genderList.Single(x => x.Cd == cd).Description;
                    case DataType.Caste:
                        return casteList.Single(x => x.Cd == cd).Description;
                    case DataType.CasteGroup:
                        return casteGroupList.Single(x => x.Cd == cd).Description;
                    case DataType.Religion:
                        return religionList.Single(x => x.Cd == cd).Description;
                    case DataType.Education:
                        return educationList.Single(x => x.Cd == cd).Description;
                    case DataType.MaritalStatus:
                        return maritalStatusList.Single(x => x.Cd == cd).Description;
                    case DataType.Office:
                        return officeList.Single(x => x.Cd == cd).Description;
                    case DataType.Position:
                        return positionList.Single(x => x.Cd == cd).Description;
                    case DataType.Designation:
                        //return designationList.Where(x => x.Cd == cd).Distinct().Single().DefinedCd;
                        return dictDesignationsByPosition[GetCodeFor(DataType.Position, parentDefCd)].Single(x => x.Cd == cd).Description;
                    case DataType.SchoolType:
                        return schoolTypeList.Single(x => x.Cd == cd).Description;
                    case DataType.ClassType:
                        return classTypeList.Single(x => x.Cd == cd).Description;
                    case DataType.SubClass:
                        return subClassesList.Single(x => x.Cd == cd).Description;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            return "";
            //throw new Exception("Provided CD not found in the data type specified.");

        }
        public static string GetDescLocFor(DataType dataType, string cd, string parentDefCd = "")
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            try
            {
                if (string.IsNullOrWhiteSpace(cd))
                    return cd;
                switch (dataType)
                {
                    case DataType.Currency:
                        return currencyList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Country:
                        return countryList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Region:
                        return regionList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Zone:
                        return zoneList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.District:
                        return districtList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.VdcMun:
                        return vdcList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Ward:
                        return wardList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Gender:
                        return genderList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Caste:
                        return casteList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.CasteGroup:
                        return casteGroupList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Religion:
                        return religionList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Education:
                        return educationList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.MaritalStatus:
                        return maritalStatusList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Office:
                        return officeList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Position:
                        return positionList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.Designation:
                        //return designationList.Where(x => x.Cd == cd).Distinct().Single().DefinedCd;
                        return dictDesignationsByPosition[GetCodeFor(DataType.Position, parentDefCd)].Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.SchoolType:
                        return schoolTypeList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.ClassType:
                        return classTypeList.Single(x => x.Cd == cd).DescriptionLoc;
                    case DataType.SubClass:
                        return subClassesList.Single(x => x.Cd == cd).DescriptionLoc;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            return "";
            //throw new Exception("Provided CD not found in the data type specified.");

        }
        public static List<SelectListItem> GetSelectedList(List<SelectListItem> list, string selectedValue)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            if (!string.IsNullOrWhiteSpace(selectedValue))
                list.First(s => s.Value == selectedValue).Selected = true;
            //else
            //{
            //    foreach (SelectListItem li in list)
            //    {
            //        li.Selected = false;
            //    }
            //}
            return list;
        }


        #region Filters
        public static List<SelectListItem> FilterCurrency(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return currencyList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return currencyList.Where(d => d.DescriptionLoc.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return currencyList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return currencyList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return currencyList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return currencyList.Where(d => d.Description.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return currencyList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return currencyList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }

        public static List<SelectListItem> FilterCountry(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return countryList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return countryList.Where(d => d.DescriptionLoc.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return countryList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return countryList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return countryList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return countryList.Where(d => d.Description.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return countryList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return countryList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }

        public static List<SelectListItem> FilterDistrict(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return districtList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return districtList.Where(d => d.DescriptionLoc.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return districtList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return districtList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return districtList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return districtList.Where(d => d.Description.ToLower().Contains(descL.ToLower())).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return districtList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return districtList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }
        public static List<SelectListItem> FilterDistrictbyZone(string code, string desc, string zoneDefCd, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                //return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                //new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                //).ToList();
                else if (!isDescEmpty)
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return (zoneDefCd.ConvertToString() == "" ? districtList : dictDistrictsByZone[GetCodeFor(DataType.Zone, zoneDefCd)]).Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }

        public static List<SelectListItem> FilterZonebyRegState(string code, string desc, string regStateDefCd, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return (regStateDefCd.ConvertToString() == "" ? zoneList : dictZoneByRegion[GetCodeFor(DataType.Region, regStateDefCd)]).Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }

        public static List<SelectListItem> FilterZone(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return zoneList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return zoneList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return zoneList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();

                return zoneList.Select(d1 =>
                        new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                        ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return zoneList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return zoneList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return zoneList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return zoneList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }
        public static List<SelectListItem> FilterRegion(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return regionList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return regionList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return regionList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();

                return regionList.Select(d1 =>
                        new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                        ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return regionList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return regionList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return regionList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return regionList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }


        }
        public static List<SelectListItem> FilterVdc(string code, string desc, string districtDefCd, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            if (districtDefCd.ConvertToString()=="")
            {
                return new List<SelectListItem>();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return dictVdcByDistrict[GetCodeFor(DataType.District, districtDefCd)].Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
            }

        }
        public static List<SelectListItem> FilterOffice(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return officeList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return officeList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return officeList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return officeList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return officeList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return officeList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return officeList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return officeList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }
        public static List<SelectListItem> FilterCaste(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return casteList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return casteList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return casteList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return casteList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return casteList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return casteList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return casteList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return casteList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }

        public static List<SelectListItem> FilterEducation(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return educationList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return educationList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return educationList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return educationList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return educationList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return educationList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return educationList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return educationList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }

        public static List<SelectListItem> FilterClassType(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return classTypeList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return classTypeList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return classTypeList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return classTypeList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return classTypeList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return classTypeList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return classTypeList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return classTypeList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }

        public static List<SelectListItem> FilterReligion(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return religionList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return religionList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return religionList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return religionList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return religionList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return religionList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return religionList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return religionList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }

        public static List<SelectListItem> FilterMaritalStatus(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return maritalStatusList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return maritalStatusList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return maritalStatusList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return maritalStatusList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return maritalStatusList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return maritalStatusList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return maritalStatusList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return maritalStatusList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }
        public static List<SelectListItem> FilterPosition(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString().ToLower();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return positionList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return positionList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return positionList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return positionList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return positionList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return positionList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return positionList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return positionList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }
        public static List<SelectListItem> FilterSubClass(string code, string desc, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString().ToLower();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return subClassesList.Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return subClassesList.Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return subClassesList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return subClassesList.Select(d1 =>
                            new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                            ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return subClassesList.Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return subClassesList.Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return subClassesList.Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return subClassesList.Select(d1 =>
                            new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                            ).ToList();
            }

        }
        public static List<SelectListItem> FilterDesignation(string code, string desc, string posDefCd, string language)
        {
            if (HttpContext.Current.Session["UpdatedType"] != null)
            {
                RetrieveDataFromDatabase();
            }
            bool isCodeEmpty = string.IsNullOrWhiteSpace(code);
            bool isDescEmpty = string.IsNullOrWhiteSpace(desc);
            string descL = desc.ConvertToString();
            if (language == "nepali")
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Where(d => d.DefinedCd.Contains(code) && d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Where(d => d.DescriptionLoc.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Select(d1 =>
                    new SelectListItem { Text = d1.DescriptionLoc, Value = d1.DefinedCd }
                    ).ToList();
            }
            else
            {
                if (!isCodeEmpty && !isDescEmpty)
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Where(d => d.DefinedCd.Contains(code) && d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isDescEmpty)
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Where(d => d.Description.ToLower().Contains(descL)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else if (!isCodeEmpty)
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Where(d => d.DefinedCd.Contains(code)).Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
                else
                    return dictDesignationsByPosition[GetCodeFor(DataType.Position, posDefCd)].Select(d1 =>
                    new SelectListItem { Text = d1.Description, Value = d1.DefinedCd }
                    ).ToList();
            }

        }
        #endregion
    }
}