using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MIS.Services.Enrollment
{
    public class EnrollmentFileImportService
    {
        CommonFunction common = new CommonFunction();
        public static string GetDateFormatString(string inputdate)
        {
            if (inputdate == "99999999" || inputdate == "")
            {
                return "";
            }
            else
            {
                string[] splitString = inputdate.Split(' ');
                if (splitString.Length > 1)
                {
                    if (splitString.Length == 3)
                    {
                        return splitString[0] + "-0" + splitString[1] + "-0" + splitString[2];
                    }
                    else if (splitString.Length == 2)
                    {
                        if (splitString[1].Length == 3)
                        {
                            return splitString[0] + "-0" + splitString[1].Substring(0, 1) + "-" + splitString[1].Substring(1, 2);
                        }
                        else if (splitString[1].Length == 1)
                        {
                            return splitString[0].Substring(0, 4) + "-" + splitString[0].Substring(4, 2) + "-0" + splitString[1];
                        }
                    }
                }
                else
                {
                    return inputdate.Substring(0, 4) + "-" + inputdate.Substring(4, 2) + "-" + inputdate.Substring(6, 2);
                }

            }
            return "";
        }

        //public string ConvertDtToXML(DataTable dt)
        //{
        //    string result = "";
        //    //Converting List to XML using LINQ to XML    
        //    // the xml doc will get stored into OrderDetails object of XDocument    
        //    XDocument paramTable = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
        //    new XElement("CustomerOrder",
        //    from OrderDet in dt.OrderDetails
        //    select new XElement("OrderDetails",
        //    new XElement("ItemCode", OrderDet.ItemCode),
        //    new XElement("ProductName", OrderDet.ProductName),
        //    new XElement("Qty", OrderDet.Qty),
        //    new XElement("Price", OrderDet.Price),
        //    new XElement("TotalAmount", OrderDet.TotalAmount))));    

        //    return result;
        //}
        public string SaveData(DataTable paramTable, string fileName, out string ExceptionMessage)
        {
            QueryResult qr = null;
            QueryResult qr1 = null;
            string  res = "False";
            string exc = string.Empty;
            ExceptionMessage = string.Empty;
            string batchID = "";
            string PA_NO = "";
            int loopIndexValue = 0;

            string BeneficiaryFNameEng = "";
            string BeneficiaryMNameEng = "";
            string BeneficiaryLNameEng = "";


            string ProxyFNameEng = "";
            string ProxyMNameEng = "";
            string ProxyLNameEng = "";


            string ProxyFFNameEng = "";
            string ProxyFMNameEng = "";
            string ProxyFLNameEng = "";


            string ProxyGFFNameEng = "";
            string ProxyGFMNameEng = "";
            string ProxyGFLNameEng = "";


            string AccountholderFNameEng = "";
            string AccountholderMNameEng = "";
            string AccountholderLNameEng = "";



            string NomineeFNameEng = "";
            string NomineeMNameEng = "";
            string NomineeLNameEng = "";


            string WitnessFNameEng = "";
            string WitnessMNameEng = "";
            string WitnessLNameEng = "";

            string DistCd = null;
            string VdcCd = null;
            string Ward = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_NHRS_ENROLLMENT_CSV";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_ENROLLMENT_CSV_FILE_BATCH",
                                                "I",
                                                 paramTable.Rows[0]["DIST"].ToString(),//District
                                                 paramTable.Rows[0]["VDCMUN"].ToString(),//VDC
                        //paramTable.Rows[0]["WARD"].ToString(),//WARD
                                                 "Completed",
                                                fileName,//filename                                                 
                                                 CurrentDate,
                                                 SessionCheck.getSessionUsername(),
                                                 DBNull.Value,
                                                 DBNull.Value);

                    //Main Table 
                    batchID = qr["p_BATCH_ID"].ConvertToString();
               
                    //foreach (DataRow row in paramTable.Rows)
                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                       

                        #region split Beneficiary full name
                        if (paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                BeneficiaryFNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[0];
                                BeneficiaryMNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[1];
                                BeneficiaryLNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                BeneficiaryFNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[0];
                                BeneficiaryMNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[1];
                                BeneficiaryLNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                BeneficiaryFNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[0];
                                BeneficiaryMNameEng = " ";
                                BeneficiaryLNameEng = paramTable.Rows[i][14].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion


                        #region split proxy full name
                        if (paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                ProxyFNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyMNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[1];
                                ProxyLNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                ProxyFNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyMNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[1];
                                ProxyLNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                ProxyFNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyMNameEng = " ";
                                ProxyLNameEng = paramTable.Rows[i][27].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion


                        #region split proxy  father full name
                        if (paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                ProxyFFNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyFMNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[1];
                                ProxyFLNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                ProxyFFNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyFMNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[1];
                                ProxyFLNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                ProxyFFNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyFMNameEng = " ";
                                ProxyFLNameEng = paramTable.Rows[i][37].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion

                        #region split proxy grand father full name
                        if (paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                ProxyGFFNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyGFMNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[1];
                                ProxyGFLNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                ProxyGFFNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyGFMNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[1];
                                ProxyGFLNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                ProxyGFFNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[0];
                                ProxyGFMNameEng = " ";
                                ProxyGFLNameEng = paramTable.Rows[i][36].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion


                        #region split proxy Account Holder full name
                        if (paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                AccountholderFNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[0];
                                AccountholderMNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[1];
                                AccountholderLNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                AccountholderFNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[0];
                                AccountholderMNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[1];
                                AccountholderLNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                AccountholderFNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[0];
                                AccountholderMNameEng = " ";
                                AccountholderLNameEng = paramTable.Rows[i][41].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion


                        #region split  nominee full name
                        if (paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                NomineeFNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[0];
                                NomineeMNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[1];
                                NomineeLNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                NomineeFNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[0];
                                NomineeMNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[1];
                                NomineeLNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                NomineeFNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[0];
                                NomineeMNameEng = " ";
                                NomineeLNameEng = paramTable.Rows[i][58].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion


                        #region split witness full name
                        if (paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                WitnessFNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[0];
                                WitnessMNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[1];
                                WitnessLNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                WitnessFNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[0];
                                WitnessMNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[1];
                                WitnessLNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                WitnessFNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[0];
                                WitnessMNameEng = " ";
                                WitnessLNameEng = paramTable.Rows[i][60].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        #endregion


                        string agrementDate = paramTable.Rows[i]["AGREEMENT_DATE"].ConvertToString();
                        if (agrementDate != "")
                        {
                            agrementDate = agrementDate.Insert(4, "/");
                            agrementDate = agrementDate.Insert(7, "/");

                        }

                         string BenfCtzIssueDate = paramTable.Rows[i]["BENEF_CTZ_ISUE_DATE"].ConvertToString();
                         if (BenfCtzIssueDate != "")
                        {
                            BenfCtzIssueDate = BenfCtzIssueDate.Insert(4, "/");
                            BenfCtzIssueDate = BenfCtzIssueDate.Insert(7, "/");

                        }

                         string BenfBirthDate = paramTable.Rows[i]["BENEF_BRTH_DATE"].ConvertToString();
                         if (BenfBirthDate != "")
                        {
                            BenfBirthDate = BenfBirthDate.Insert(4, "/");
                            BenfBirthDate = BenfBirthDate.Insert(7, "/");

                        }

                         string MigrationDate = paramTable.Rows[i]["MIGRATN_DATE"].ConvertToString();
                         string MigrationDate_eng = "";

                         if (MigrationDate != "")
                         {
                             MigrationDate = MigrationDate.Insert(4, "/");
                             MigrationDate = MigrationDate.Insert(7, "/");
                             MigrationDate_eng=NepaliDate.getEnglishDate(MigrationDate);
                         }

                         string ProxyCtzIssueDate = paramTable.Rows[i]["PRXY_CTZ_ISUE_DATE"].ConvertToString();
                         if (ProxyCtzIssueDate != "")
                         {
                             ProxyCtzIssueDate = ProxyCtzIssueDate.Insert(4, "/");
                             ProxyCtzIssueDate = ProxyCtzIssueDate.Insert(7, "/");

                         }

                         string ProxyBirtDate = paramTable.Rows[i]["PRXY_BRTH_DATE"].ConvertToString();
                         string ProxyBirtDate_eng = "";
                         if (ProxyBirtDate != "")
                         {
                             ProxyBirtDate = ProxyBirtDate.Insert(4, "/");
                             ProxyBirtDate = ProxyBirtDate.Insert(7, "/");
                             ProxyBirtDate_eng = NepaliDate.getEnglishDate(ProxyBirtDate);


                         }
                        

                         loopIndexValue = i;

                        
                          PA_NO = paramTable.Rows[i][0].ConvertToString() + '-' + paramTable.Rows[i][1].ConvertToString() + '-' + paramTable.Rows[i][2].ConvertToString() + '-' + paramTable.Rows[i][3].ConvertToString() + '-' + paramTable.Rows[i][4].ConvertToString();
                          string[] SplitPa = PA_NO.Split('-');
                          if (SplitPa.Length == 5)
                          {
                              DistCd = SplitPa[0];
                              VdcCd = SplitPa[1];
                              Ward = SplitPa[2];
                          }
                          if (SplitPa.Length == 6)
                          {
                              DistCd = SplitPa[1];
                              VdcCd = SplitPa[2];
                              Ward = SplitPa[3];
                          }
                        
                        qr1 = service.SubmitChanges("PR_ENROLLMENT_MOU_CSVFILE",
                                    DBNull.Value,//,
                                    DistCd.ToDecimal(),
                                    VdcCd.ToDecimal(),
                                    Ward.ToDecimal(),
                                    paramTable.Rows[i]["EA"].ConvertToString(),
                                    PA_NO,
                                    paramTable.Rows[i]["BENEF_TOLE"].ConvertToString(), //   row["BENEF_TOLE"],//p_AREA_ENG IN NHRS_ENROLLMENT_MOU.AREA_ENG%TYPE,
                                    paramTable.Rows[i]["BENEF_GFATHR_NME"].ConvertToString(),//   row["BENEF_GFATHR_NME"],//p_GFATHER_FULLNAME_ENG IN NHRS_ENROLLMENT_MOU.GFATHER_FULLNAME_ENG%TYPE,
                                    paramTable.Rows[i]["BENEF_FATHR_NME"].ConvertToString(),//   row["BENEF_FATHR_NME"],//p_FATHER_FULLNAME_ENG IN NHRS_ENROLLMENT_MOU.FATHER_FULLNAME_ENG%TYPE,
                                    paramTable.Rows[i]["BENEF_HUSBND_NME"].ConvertToString(),
                                    paramTable.Rows[i]["BENEF_AGE"].ToDecimal(), //   row["BENEF_AGE"],//p_BENEFICIARY_AGE IN NHRS_ENROLLMENT_MOU.BENEFICIARY_AGE%TYPE,
                                    paramTable.Rows[i]["BENEF_SEX"].ToDecimal(),
                                    paramTable.Rows[i]["BENEF_NME_NPL"].ConvertToString(),//   row["BENEF_NME_NPL"],
                                    paramTable.Rows[i]["BENEF_NME_ENG"].ConvertToString(), //   row["BENEF_NME_ENG"],//p_BENEFICIARY_FULLNAME_ENG IN NHRS_ENROLLMENT_MOU.BENEFICIARY_FULLNAME_ENG%TYPE,
                                    BeneficiaryFNameEng,
                                    BeneficiaryMNameEng,
                                    BeneficiaryLNameEng,
                                    agrementDate,           //new
                                    paramTable.Rows[i]["BENEF_CTZ_NO"].ConvertToString(),
                                    paramTable.Rows[i]["BENEF_CTZ_ISUE_DST"].ToDecimal(),//   row["BENEF_CTZ_ISUE_DST"],//p_BEN_CTZ_ISSUE_DISTRICT_CD IN NHRS_ENROLLMENT_MOU.BEN_CTZ_ISSUE_DISTRICT_CD%TYPE,
                                    BenfCtzIssueDate,//   GetDateFormatString(row["BENEF_CTZ_ISUE_DATE"].ConvertToString()),//p_BENEFICIARY_CTZ_ISSUE_DT IN NHRS_ENROLLMENT_MOU.BENEFICIARY_CTZ_ISSUE_DT%TYPE,
                                    BenfBirthDate, //   GetDateFormatString(row["BENEF_BRTH_DATE"].ConvertToString()),//p_BENEFICIARY_DOB_loc IN NHRS_ENROLLMENT_MOU.BENEFICIARY_DOB_loc%TYPE,
                                    (paramTable.Rows[i]["MIGRATION_NO"].ToDecimal()), //   row["MIGRATION_NO"],
                                    MigrationDate_eng.ToDateTime(), //   GetDateFormatString(row["MIGRATN_DATE"].ConvertToString()),
                                    paramTable.Rows[i]["BENEF_TEL_NO"].ConvertToString(), //   row["BENEF_TEL_NO"],//p_BENEFICIARY_PHONE IN NHRS_ENROLLMENT_MOU.BENEFICIARY_PHONE%TYPE,
                                    paramTable.Rows[i]["BENEF_CTZ_DST"].ToDecimal(),
                                    paramTable.Rows[i]["BENEF_CTZ_VDC"].ToDecimal(),
                                    paramTable.Rows[i]["BENEF_CTZ_WRD"].ToDecimal(),
                                    paramTable.Rows[i]["PRXY_NAME"].ConvertToString(), //   row["PRXY_NAME"],
                                    ProxyFNameEng,
                                    ProxyMNameEng,
                                    ProxyLNameEng,
                                    paramTable.Rows[i]["PRXY_DST"].ConvertToString().ToDecimal(), //   row["PRXY_DST"],
                                    paramTable.Rows[i]["PRXY_VDC"].ConvertToString().ToDecimal(),//   row["PRXY_VDC"],
                                    paramTable.Rows[i]["PRXY_WRD"].ConvertToString().ToDecimal(), //   row["PRXY_WRD"],
                                    paramTable.Rows[i]["PRXY_TOLE"].ConvertToString(), //   row["PRXY_TOLE"],
                                    paramTable.Rows[i]["PRXY_CTZ_NO"].ConvertToString(), //   row["PRXY_CTZ_NO"],
                                    paramTable.Rows[i]["PRXY_CTZ_ISUE_DST"].ToDecimal(), //   row["PRXY_CTZ_ISUE_DST"],
                                    ProxyCtzIssueDate,  //   GetDateFormatString(row["PRXY_CTZ_ISUE_DATE"].ConvertToString()),
                                    ProxyBirtDate_eng.ToDateTime(),//   GetDateFormatString(row["PRXY_BRTH_DATE"].ConvertToString()),
                                    paramTable.Rows[i]["PRXY_GFATHR"].ConvertToString(),//   GetDateFormatString(row["PRXY_GFATHR"].ConvertToString()),
                                    ProxyGFFNameEng,
                                    ProxyGFMNameEng,
                                    ProxyGFLNameEng,
                                    paramTable.Rows[i]["PRXY_FATHR"].ConvertToString(),//   row["PRXY_FATHR"],
                                    ProxyFFNameEng,
                                    ProxyFMNameEng,
                                    ProxyFLNameEng,
                                    paramTable.Rows[i]["PRXY_RELATN"].ToDecimal(), //   row["PRXY_RELATN"],
                                    paramTable.Rows[i]["PRXY_TEL_NO"].ConvertToString(), //   row["PRXY_TEL_NO"],
                                    paramTable.Rows[i]["BANK_ACCOUNT_NO"].ConvertToString(), //   row["BANK_ACCOUNT_NO"],
                                    paramTable.Rows[i]["ACCNT_HOLDR_NME"].ConvertToString(),//   row["ACCNT_HOLDR_NME"],
                                    AccountholderFNameEng,
                                    AccountholderMNameEng,
                                    AccountholderLNameEng,
                                    paramTable.Rows[i]["BANK_NAME"].ConvertToString(),
                                    paramTable.Rows[i]["BANK_BRANCH"].ToDecimal(), //   row["BANK_BRANCH"],
                                    paramTable.Rows[i]["PLOTNO"].ToDecimal(), //   row["PLOTNO"],
                                    paramTable.Rows[i]["LAND_AREA_ROPANI"].ToDecimal(), //   row["LAND_AREA_ROPANI"],
                                    paramTable.Rows[i]["LAND_AREA_ANA"].ToDecimal(),//   row["LAND_AREA_ANA"],
                                    paramTable.Rows[i]["LAND_AREA_PAISA"].ToDecimal(),//   row["LAND_AREA_PAISA"],
                                    paramTable.Rows[i]["LAND_DIST"].ToDecimal(), //   row["LAND_DIST"],
                                    paramTable.Rows[i]["LAND_VDC"].ToDecimal(), //   row["LAND_VDC"],
                                    paramTable.Rows[i]["LAND_WRD"].ToDecimal(), //   row["LAND_WRD"],
                                    paramTable.Rows[i]["HOUSE_DESIGN"].ToDecimal(),//   row["HOUSE_DESIGN"],
                                    paramTable.Rows[i]["WALL_TYPE"].ToDecimal(),//   row["WALL_TYPE"],
                                    paramTable.Rows[i]["ROOF_TYPE"].ToDecimal(), //   row["ROOF_TYPE"],
                                    paramTable.Rows[i]["MAP_APRVD_NO"].ToDecimal(), //   row["MAP_APRVD_NO"],
                                    paramTable.Rows[i]["HOWNER"].ConvertToString(), //   row["HOWNER"],
                                    paramTable.Rows[i]["HOWNR_SN"].ToDecimal(),//   row["HOWNR_SN"],
                                    paramTable.Rows[i]["NOMINEE_NME"].ConvertToString(),//   row["NOMINEE_NME"],//p_NOMINEE_FULLNAME_ENG IN NHRS_ENROLLMENT_MOU.NOMINEE_FULLNAME_ENG%TYPE,
                                    NomineeFNameEng,
                                    NomineeMNameEng,
                                    NomineeLNameEng,
                                    paramTable.Rows[i]["NOMINEE_RELATN"].ToDecimal(),
                                    paramTable.Rows[i]["WITNESS"].ConvertToString(),//   row["WITNESS"],
                                    WitnessFNameEng,
                                    WitnessMNameEng,
                                    WitnessLNameEng,
                                    paramTable.Rows[i]["OFFICIAL"].ConvertToString(), //   row["OFFICIAL"],
                                    paramTable.Rows[i]["POSITION"].ConvertToString(),//   row["POSITION"],
                                    paramTable.Rows[i]["OFFICE_NAME"].ConvertToString(),//   row["OFFICE_NAME"],
                                    paramTable.Rows[i]["BEN_PHOTO"].ToString(),//   row["BEN_PHOTO"],
                                    paramTable.Rows[i]["BEN_PHOTO_NME"].ConvertToString(),
                                    paramTable.Rows[i]["CTZ_PHOTO_FRNT"].ConvertToString(), //   row["CTZ_PHOTO_FRNT"],
                                    paramTable.Rows[i]["CTZ_PHOTO_NME_FRNT"].ConvertToString(),  //   row["CTZ_PHOTO_NME_FRNT"],//p_CTZ_PHOTO_NME_FRNT IN NHRS_ENROLLMENT_MOU.CTZ_PHOTO_NME_FRNT%TYPE,
                                    paramTable.Rows[i]["CTZ_PHOTO_BACK"].ConvertToString(),  //   row["CTZ_PHOTO_BACK"],
                                    paramTable.Rows[i]["CTZ_PHOTO_NME_BACK"].ConvertToString(), //   row["CTZ_PHOTO_NME_BACK"],//p_CTZ_PHOTO_NME_BACK IN NHRS_ENROLLMENT_MOU.CTZ_PHOTO_NME_BACK%TYPE,
                                    paramTable.Rows[i]["PA_PHOTO_FRNT"].ConvertToString(), //   row["PA_PHOTO_FRNT"],
                                    paramTable.Rows[i]["PA_PHOTO_NME_FRNT"].ConvertToString(), //   row["PA_PHOTO_NME_FRNT"],//p_PA_PHOTO_NME_FRNT IN NHRS_ENROLLMENT_MOU.PA_PHOTO_NME_FRNT%TYPE,
                                    paramTable.Rows[i]["PA_PHOTO_BACK"].ConvertToString(),//   row["PA_PHOTO_BACK"],
                                    paramTable.Rows[i]["PA_PHOTO_NME_BACK"].ConvertToString(),//   row["PA_PHOTO_NME_BACK"],//p_PA_PHOTO_NME_BACK IN NHRS_ENROLLMENT_MOU.PA_PHOTO_NME_BACK%TYPE
                                    paramTable.Rows[i]["DEVICE_NAME"].ConvertToString(),//   row["DEVICE_NAME"],
                                    batchID.ConvertToString(),
                                     paramTable.Rows[i]["OTHERS"].ConvertToString(),
                                      paramTable.Rows[i]["GSON_GDAUGHTR"].ToDecimal(),
                                       paramTable.Rows[i]["SON_DAUGH"].ToDecimal(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                     SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString()
                                              );
                    }

                }
                catch (OracleException oe)
                {
                    res = "True";
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                    string cmdText = String.Format("update NHRS_ENROLLMENT_CSVFILE_ set STATUS='Error',ERROR_MESSAGE='" + oe.Message + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_ENROLLMENT_PA where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

                    ExceptionMessage ="PA Number: "+ PA_NO + "- " + oe.Message;

                    string reason = oe.Message;
                    saveDuplicateEnrollmentPAData(PA_NO,
                               paramTable.Rows[ loopIndexValue][14].ConvertToString(),
                               paramTable.Rows[loopIndexValue][0].ConvertToString(),
                               paramTable.Rows[loopIndexValue][1].ConvertToString(),
                               paramTable.Rows[loopIndexValue][2].ConvertToString(),
                               paramTable.Rows[loopIndexValue][3].ConvertToString(),
                               paramTable.Rows[loopIndexValue][4].ConvertToString(),
                               paramTable.Rows[loopIndexValue][8].ConvertToString(), fileName, reason);
               
                }
                catch (Exception ex)
                {
                    res = "True";
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);

                    string cmdText = String.Format("update NHRS_ENROLLMENT_CSVFILE_ set STATUS='Error',ERROR_MESSAGE='" + ex.Message + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_ENROLLMENT_PA where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    ExceptionMessage = PA_NO +"-"+ ex.Message;

                    string reason = ex.Message;
                    saveDuplicateEnrollmentPAData(PA_NO,
                               paramTable.Rows[loopIndexValue][14].ConvertToString(),
                               paramTable.Rows[loopIndexValue][0].ConvertToString(),
                               paramTable.Rows[loopIndexValue][1].ConvertToString(),
                               paramTable.Rows[loopIndexValue][2].ConvertToString(),
                               paramTable.Rows[loopIndexValue][3].ConvertToString(),
                               paramTable.Rows[loopIndexValue][4].ConvertToString(),
                               paramTable.Rows[loopIndexValue][8].ConvertToString(), fileName, reason);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res=="True")
                {
                    res = ExceptionMessage;
                   
                }
                else
                {
                    if (qr != null)
                    {
                        res = qr.IsSuccess.ConvertToString();
                        ExceptionMessage = res;
                    }
                }
                
            return ExceptionMessage;

            }

        }

        public Boolean SaveDataFromFileBrowse(DataTable paramTable, string District, string VDC, string fileName, out string exc)
        {
            QueryResult qr = null;
            QueryResult qr1 = null;
            bool res = false;
            int i;
            exc = string.Empty;
            string batchID = "";


            //string fileExtension = Path.GetExtension(fileName);
            //for (int i = 2; i < paramTable.Rows.Count; i++)
            //{ }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_ENROLLMENT_FILE";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_ENROLLMENT_FILE_BATCH",
                                               "I",
                                               District,
                                               VDC,
                                               "Completed",
                                               fileName,//filename                                                 
                                               CurrentDate,
                                               SessionCheck.getSessionUsername(),
                                               DBNull.Value,
                                               DBNull.Value);

                    //Main Table 
                    batchID = qr["p_BATCH_ID"].ConvertToString();

                    //foreach (DataRow row in paramTable.Rows)

                    //SAVE FILE IN ENROLLMENT_MOU

                    for (i = 1; i < paramTable.Rows.Count; i++)
                    {

                        string ctzissuedate = "";
                        string benefdob = "";
                        string benefFrstrname = " ";
                        string benefMdlname = "";
                        string benefLstname = "";
                        string FatherFirstName = "";
                        string FatherMiddleName = "";
                        string FatherLastName = "";
                        string GFatherFirstName = "";
                        string GFatherMiddleName = "";
                        string GFatherLastName = "";
                        string SpouseFirstName = "";
                        string SpouseMiddleName = "";
                        string SpouseLastName = "";
                        string InlawFirstName = "";
                        string InlawMiddleName = "";
                        string InlawLastName = "";
                        string InsertBy = SessionCheck.getSessionUsername();
                        var currentDate = DateTime.Now;
                        //  DateTime issuedate = DateTime.Parse(paramTable.Rows[i][14].ToString());
                        ctzissuedate = paramTable.Rows[i][12].ToString();
                        // DateTime DOB = DateTime.Parse(paramTable.Rows[i][15].ToString());
                        benefdob = paramTable.Rows[i][13].ToString();

                        #region split full name
                        if (paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for beneficiary name
                        {
                            if (paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                benefFrstrname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[0];
                                benefMdlname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[1];
                                benefLstname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                benefFrstrname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[0];
                                benefMdlname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[1];
                                benefLstname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                benefFrstrname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[0];
                                benefMdlname = " ";
                                benefLstname = paramTable.Rows[i][1].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }
                        if (paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[0] != "")  //for father name
                        {
                            if (paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {
                                FatherFirstName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[0];
                                FatherMiddleName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[1];
                                FatherLastName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][7].ToString().Split(' ')[3];
                            }
                            if (paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                FatherFirstName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[0];
                                FatherMiddleName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[1];
                                FatherLastName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                FatherFirstName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[0];
                                FatherMiddleName = " ";
                                FatherLastName = paramTable.Rows[i][7].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }
                        if (paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[0] != "")    //for Grand father name
                        {
                            if (paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {
                                GFatherFirstName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[0];
                                GFatherMiddleName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[1];
                                GFatherLastName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][6].ToString().Split(' ')[3];
                            }
                            if (paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                GFatherFirstName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[0];
                                GFatherMiddleName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[1];
                                GFatherLastName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                GFatherFirstName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[0];
                                GFatherMiddleName = " ";
                                GFatherLastName = paramTable.Rows[i][6].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        if (paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[0] != "")    //Spouse name
                        {
                            if (paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {
                                SpouseFirstName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[0];
                                SpouseMiddleName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[1];
                                SpouseLastName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][9].ToString().Split(' ')[3];
                            }
                            if (paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                SpouseFirstName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[0];
                                SpouseMiddleName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[1];
                                SpouseLastName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                SpouseFirstName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[0];
                                SpouseMiddleName = " ";
                                SpouseLastName = paramTable.Rows[i][9].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }
                        if (paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[0] != "")   //Inlaw Name
                        {
                            if (paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {
                                InlawFirstName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[0];
                                InlawMiddleName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[1];
                                InlawLastName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][8].ToString().Split(' ')[3];
                            }
                            if (paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                InlawFirstName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[0];
                                InlawMiddleName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[1];
                                InlawLastName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                InlawFirstName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[0];
                                InlawMiddleName = " ";
                                InlawLastName = paramTable.Rows[i][8].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }
                        #endregion



                        qr1 = service.SubmitChanges("PR_ENROLLMENT_MOU_FILE",
                                           "I",
                                           District.ToDecimal(),//DIS 
                                           VDC.ToDecimal(),//VDC
                                           Convert.ToDecimal(paramTable.Rows[i][4].ToString()),//Ward
                                           paramTable.Rows[i][5].ToString(), //NRA DEFINED CODE                                       
                                           paramTable.Rows[i][6].ToString(),//bajeko name
                                            GFatherFirstName.ToString(),
                                             GFatherMiddleName.ToString(),
                                            GFatherLastName.ToString(),
                                           paramTable.Rows[i][7].ToString(),//father name
                                            FatherFirstName.ToString(),
                                             FatherMiddleName.ToString(),
                                             FatherLastName.ToString(),
                                           paramTable.Rows[i][9].ToString(),//husband name
                                             SpouseFirstName.ToString(),
                                            SpouseMiddleName.ToString(),
                                           SpouseLastName.ToString(),

                                           paramTable.Rows[i][1].ToString(),//bname    
                                           benefFrstrname.ToString(),
                                           benefMdlname.ToString(),
                                           benefLstname.ToString(),
                                           paramTable.Rows[i][10].ToString(),//na pra pa
                            //DBNull.Value,//dis_cd
                                           paramTable.Rows[i][11].ToString(),//issue dis
                                           ctzissuedate.ToString(),//jari miti
                                            DBNull.Value,//   ctzissuedate.ToString().Split('-')[2],
                                            DBNull.Value,//    (ctzissuedate.ToString().Split('-')[1]),
                                             DBNull.Value,//   (ctzissuedate.ToString().Split('-')[0]),
                                            paramTable.Rows[i][8].ToString(),//inlaw name
                                            InlawFirstName.ToString(),
                                            InlawMiddleName.ToString(),
                                            InlawLastName.ToString(),
                                            DBNull.Value,
                                            benefdob.ToString(),//DOB 
                                            DBNull.Value,//   (benefdob.ToString().Split('-')[2]),
                                            DBNull.Value,//    benefdob.ToString().Split('-')[1],
                                            DBNull.Value,//  (benefdob.ToString().Split('-')[0]),
                                            InsertBy,
                                            currentDate,
                                           batchID.ToDecimal()

                                           );
                    }
                }

                catch (OracleException oe)
                {
                    res = true;
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                    string cmdText = String.Format("update NHRS_ENROLLMENT_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_ENROLLMENT_MOU where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("update NHRS_ENROLLMENT_MST set  MOU_SIGNED_STATUS='N',SMS_SEND_STATUS='N',PRINT_STATUS='N' where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                }
                catch (Exception ex)
                {
                    res = true;
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);

                    string cmdText = String.Format("update NHRS_ENROLLMENT_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_ENROLLMENT_MOU where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("update NHRS_ENROLLMENT_MST set  MOU_SIGNED_STATUS='N',SMS_SEND_STATUS='N',PRINT_STATUS='N' where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }

                return res;

            }



        }
        #region DeleteEnrollDetailForShowingZeroRecord
        //public Boolean deleteEnrollmentDetail(string batchid)
        //{
        //    QueryResult qr = null;

        //    bool res = false;

        //    string exc = string.Empty;

        //    string cmdText = "";

        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        try
        //        {
        //            service.PackageName = "NHRS.PKG_ENROLLMENT_FILE";
        //            service.Begin();
        //            var CurrentDate = DateTime.Now;
        //            qr = service.SubmitChanges("PR_ENROLLMENT_FILE_BATCH",
        //                                       "I",
        //                                       District,
        //                                       VDC,
        //                                       "Completed",
        //                                       fileName,//filename                                                 
        //                                       CurrentDate,
        //                                       SessionCheck.getSessionUsername(),
        //                                       DBNull.Value,
        //                                       DBNull.Value);

        //        }

        //        catch (Exception ex)
        //        {
        //            res = true;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }
        //        if (res)
        //        {
        //            res = false;
        //        }
        //        else
        //        {
        //            if (qr != null)
        //            {
        //                res = qr.IsSuccess;
        //            }
        //        }

        //        return res;
        //    }
        //}
        #endregion
        public bool SaveErrorMessgae(string errorMsg)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(errorMsg, null);

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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }
        public string GetBatchIDFromFileName(string fileName)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            string lstStr = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT BATCH_ID FROM NHRS_ENROLLMENT_FILE_BATCH WHERE FILENAME=" + fileName.Trim();
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
                lstStr = dt.Rows[0]["BATCH_ID"].ToString();
            }
            return lstStr;
        }

        public bool CheckDuplicate(string NRADefinedCode)
        {
            DataTable dt = new DataTable();

            string cmdText = "";
            string cmdText1 = "";

            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ENROLLMENT_MOU WHERE NRA_DEFINED_CD='" + NRADefinedCode + "'";


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
                res = true;

            }

            return res;
        }


           

        public Boolean saveDuplicateEnrollmentPAData(string nraDefinedcd, string benfNameeng, string district, string vdc, string ward, string enumerationArea, string agreeemntNo, string fatherFullnameEng ,string filename, string reason)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qrSaveDuplicateInspection = null;
            bool res = false;
            string duplicateId = "";
            string definedId = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_NHRS_ENROLLMENT_CSV";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qrSaveDuplicateInspection = service.SubmitChanges("PR_NHRS_ENRLMNTDUPLT_PA",
                                               "I",
                                               duplicateId.ToDecimal(),
                                               nraDefinedcd.ConvertToString(),
                                               benfNameeng.ConvertToString(),
                                               district.ToDecimal(),                     // district
                                               vdc.ToDecimal(),             // vdc mun cd
                                               ward.ToDecimal(),                        // ward TOLE
                                               enumerationArea.ConvertToString(),
                                               agreeemntNo.ConvertToString(),
                                               fatherFullnameEng.ConvertToString(),
                                               filename.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               System.DateTime.Now.ConvertToString() ,
                                               reason
                                                  



                                         );









                }


                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrSaveDuplicateInspection != null)
                    {
                        res = qrSaveDuplicateInspection.IsSuccess;
                    }
                }

                return res;

            }
        }



        //save duplicate enrolled data 
        public Boolean saveDuplicateEnrollmentData(string paNumber, string benfName, string district, string vdc, string ward, string citizenNo, string fileName)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qrSaveDuplicateInspection = null;
            bool res = false;
            string duplicateId = "";
            string definedId = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_ENROLLMENT_FILE";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qrSaveDuplicateInspection = service.SubmitChanges("PR_ENROLL_DUPLICATE_FILE",
                                               "I",
                                               duplicateId.ToDecimal(),
                                               paNumber.ConvertToString(),
                                               benfName.ConvertToString(),
                                               district.ConvertToString(),                     // district
                                               vdc.ConvertToString(),             // vdc mun cd
                                               ward.ConvertToString(),                         // ward TOLE

                                               citizenNo.ConvertToString(),
                                               fileName.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               System.DateTime.Now.ConvertToString()

                                         );









                }


                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrSaveDuplicateInspection != null)
                    {
                        res = qrSaveDuplicateInspection.IsSuccess;
                    }
                }

                return res;

            }
        }


          

        public DataTable GetDuplicateEnrollemntPAImport(string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * FROM NHRS_ENRLMNTDUPLT_PA where 1=1 ";


                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY DUPLICATE_DATA_ID";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
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

        //grt duplicate import
        public DataTable GetDuplicateImport(string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * FROM NHRS_ENROLLMENT_DUPLICATE where 1=1 ";


                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY DUPLICATE_DATA_ID";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
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
        public bool CheckCSVPADuplicate(string NRADefinedCode)
        {
            DataTable dt = new DataTable();

            string cmdText = "";
            string cmdText1 = "";

            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ENROLLMENT_PA WHERE NRA_DEFINED_CD='" + NRADefinedCode + "'";


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
                res = true;

            }

            return res;
        }
        public bool CheckIfPAExists(string NRADefinedCode)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ENROLLMENT_MST WHERE NRA_DEFINED_CD='" + NRADefinedCode + "'";
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
                res = true;

            }
            return res;
        }
         public System.Data.DataTable GetDataImportRecordByDistrict(string district, string vdc)
        {
             DataTable dtbl = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_ENROLLMENT_FILE";
                    dtbl = service.GetDataTable(true, "PR_GET_ENROLLMENT_FILE_BATCH",
                         district.ToDecimal(),//DistrictID
                        vdc.ToDecimal(),//VDC
                        //DBNull.Value,//WARD
                        DBNull.Value);
                    // dtbl = service.GetDataTable(cmdTxt, null);

                }
                catch (Exception ex)
                {
                    dtbl = null;
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

            return dtbl;
        }
        public System.Data.DataTable GetDataImportRecordPAByDistrict()
        {
             DataTable dtbl = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_NHRS_ENROLLMENT_CSV";
                    dtbl = service.GetDataTable(true, "PR_ENROLLMENT_PA_FILE_BATCH",
                        DBNull.Value);

                }
                catch (Exception ex)
                {
                    dtbl = null;
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

            return dtbl;
        }
        public System.Data.DataTable GetDataImportRecordByVdc(List<string> jsonFiles)
        {
            DataRow row;
            DataTable dtbl = new DataTable();
             using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_ENROLLMENT_FILE";
                    dtbl = service.GetDataTable(true, "PR_GET_ENROLLMENT_FILE_BATCH",
                         DBNull.Value,//DistrictID
                         DBNull.Value,//VDC
                         DBNull.Value,//WARD
                        DBNull.Value);
                    // dtbl = service.GetDataTable(cmdTxt, null);
                    List<string> result = new List<string>();
                    List<string> result1 = new List<string>();
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            if (jsonFiles.Contains(dr["FILENAME"].ConvertToString()))
                            {
                                result1.Add(dr["FILENAME"].ConvertToString());
                            }
                        }
                        List<string> fileNamesInDB = dtbl.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
                        result = jsonFiles.Except(fileNamesInDB).ToList();
                    }
                    else
                    {
                        result = jsonFiles;

                    }

                    dtbl = new DataTable();
                    dtbl.Columns.Add("BATCH_ID", typeof(String));
                    dtbl.Columns.Add("DISCTRICT_CD", typeof(String));
                    dtbl.Columns.Add("VDC_CD", typeof(String));
                    dtbl.Columns.Add("WARD_NO", typeof(String));
                    dtbl.Columns.Add("FILENAME", typeof(String));
                    dtbl.Columns.Add("STATUS", typeof(String));
                    foreach (var i in result1)
                    {
                        string[] spliData = i.ConvertToString().Split('_');
                        row = dtbl.NewRow();
                        row["BATCH_ID"] = "";
                        row["DISCTRICT_CD"] = "";
                        row["VDC_CD"] = "";
                        row["WARD_NO"] = "";
                        row["FILENAME"] = spliData[2].ToString();
                        row["STATUS"] = "COMPLETED";
                        dtbl.Rows.Add(row);
                    }
                    foreach (var i in result)
                    {
                        string[] spliData = i.ConvertToString().Split('_');
                        row = dtbl.NewRow();
                        row["BATCH_ID"] = "";
                        row["DISCTRICT_CD"] = "";
                        row["VDC_CD"] = "";
                        row["WARD_NO"] = "";
                        row["FILENAME"] = spliData[2].ToString();
                        row["STATUS"] = "PENDING";
                        dtbl.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    dtbl = null;
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

            return dtbl;
        }


        public DataTable getTable(string text)
        {
             DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
           
            try
            {
               
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = text,
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

            return dtbl;
        }

        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_ENROLLMENT_FILE_BATCH WHERE STATUS='Completed'";
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
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
            }
            return lstStr;
        }
        public List<string> JSONFileListInDBPA()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_ENROLLMENT_CSVFILE_ WHERE STATUS='Completed'";
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
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
            }
            return lstStr;
        }

        //public System.Data.DataTable GetDataImportRecord(List<string> jsonFiles)
        //{
        //    DataRow row;
        //    DataTable dtbl = new DataTable();
        //    string cmdTxt = "";
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        try
        //        {
        //            service.PackageName = "PKG_ENROLLMENT_FILE";
        //            dtbl = service.GetDataTable(true, "PR_GET_ENROLLMENT_FILE_BATCH", 
        //                 DBNull.Value,//DistrictID
        //                 DBNull.Value,//VDC
        //                 DBNull.Value,//WARD
        //                DBNull.Value);
        //           // dtbl = service.GetDataTable(cmdTxt, null);
        //            List<string> result = new List<string>();
        //            List<string> result1 = new List<string>();
        //            if (dtbl != null && dtbl.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dtbl.Rows)
        //                {
        //                    if (jsonFiles.Contains(dr["FILENAME"].ConvertToString()))
        //                    {
        //                        result1.Add(dr["FILENAME"].ConvertToString());
        //                    }
        //                }
        //                List<string> fileNamesInDB = dtbl.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
        //                result = jsonFiles.Except(fileNamesInDB).ToList();
        //            }
        //            else
        //            {
        //                result = jsonFiles;

        //            }

        //            dtbl = new DataTable();                    
        //            dtbl.Columns.Add("BATCH_ID", typeof(String));
        //            dtbl.Columns.Add("DISCTRICT_CD", typeof(String));
        //            dtbl.Columns.Add("VDC_CD", typeof(String));
        //            dtbl.Columns.Add("WARD_NO", typeof(String));
        //            dtbl.Columns.Add("FILENAME", typeof(String));
        //            dtbl.Columns.Add("STATUS", typeof(String));
        //            foreach (var i in result1)
        //            {
        //                row = dtbl.NewRow();
        //                row["BATCH_ID"] = "";
        //                row["DISCTRICT_CD"] = "";
        //                row["VDC_CD"] = "";
        //                row["WARD_NO"] = "";
        //                row["FILENAME"] = i.ToString();
        //                row["STATUS"] = "COMPLETED";
        //                dtbl.Rows.Add(row);
        //            }
        //            foreach (var i in result)
        //            {
        //                row = dtbl.NewRow();
        //                row["BATCH_ID"] = "";
        //                row["DISCTRICT_CD"] = "";
        //                row["VDC_CD"] = "";
        //                row["WARD_NO"] = "";
        //                row["FILENAME"] = i.ToString();
        //                row["STATUS"] = "PENDING";
        //                dtbl.Rows.Add(row);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            dtbl = null;
        //            ExceptionHandler.ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }
        //    }

        //    return dtbl;
        //}

        public DataTable GetPostToMainDataByDistrictEnrollment(string District)
        {
            DataTable dtbl = new DataTable();
            string cmdTxt = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    cmdTxt = "select Distinct TO_CHAR(BI.BATCH_ID) BATCH_ID,TO_CHAR(BI.HOUSE_OWNER_CNT) HOUSE_OWNER_CNT, TO_CHAR(BI.BUILDING_STRUCTURE_CNT) BUILDING_STRUCTURE_CNT,TO_CHAR(BI.HOUSEHOLD_CNT) HOUSEHOLD_CNT, TO_CHAR(BI.MEMBER_CNT) MEMBER_CNT,BI.FILE_NAME, CASE WHEN BI.ERROR_MSG IS NULL THEN 'COMPLETED'ELSE 'FAILED' END STATUS,IS_POSTED from MIGNHRS.MIG_BATCH_INFO BI INNER JOIN NHRS_HOUSE_OWNER_MST HOM ON HOM.BATCH_ID=BI.BATCH_ID INNER JOIN NHRS.MIS_DISTRICT MD ON MD.DISTRICT_CD=HOM.DISTRICT_CD where MD.DESC_ENG='" + District + "' AND ((BI.IS_POSTED='Y')OR(BI.IS_POSTED='M')) order by BI.BATCH_ID";
                    //dtbl = service.GetDataTable(true, "PR_MEM_GENDERWISE_DASHBOARD", P_Session_ID, P_Zone, P_EnterBy, DBNull.Value);
                    dtbl = service.GetDataTable(cmdTxt, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
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

            return dtbl;
        }


        public List<string> EnrollmentPaFile()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_ENROLLMENT_CSVFILE_ WHERE STATUS='Completed'";
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
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
            }
            return lstStr;
        }

    }
}
//qr = service.SubmitChanges("PR_ENROLLMENT_FILE_BATCH",
//                           "I",
//                             paramTable.Rows[0]["DIST"].ToString(),//District
//                             paramTable.Rows[0]["VDCMUN"].ToString(),//VDC
//                             paramTable.Rows[0]["WARD"].ToString(),//WARD
//                             "Completed",
//                            fileName,//filename                                                 
//                             CurrentDate,
//                             SessionCheck.getSessionUsername(),
//                             DBNull.Value,
//                             DBNull.Value);

////Main Table 
//batchID = qr["p_BATCH_ID"].ConvertToString();