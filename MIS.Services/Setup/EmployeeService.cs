using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Setup;
using System.Data;
using System.Web;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Models.Core;

namespace MIS.Services.Setup
{
    public class EmployeeService
    {

        public string GetUserCode(string employeeCode)
        {
            string result = "";
            DataTable MaxCode = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                
                string cmdText = "select USR_CD from COM_WEB_USR where EMP_CD='" + employeeCode + "'";
                try
                {
                    service.Begin();
                    MaxCode = service.GetDataTable(new
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
                if (MaxCode != null)
                {
                    if (MaxCode.Rows.Count > 0)
                    {
                        result = MaxCode.Rows[0][0].ToString();
                    }
                }
                
            }
            return result;
        }
        public void ManageEmployee(MISEmployee Employee)
        {
            ComWebUsrGrpInfo objGrp = new ComWebUsrGrpInfo();
            ComWebUsrInfo objWebUser = new ComWebUsrInfo();
            ComEmployeeInfo obj = new ComEmployeeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                   
                    if (Employee.Mode == "D")
                    {
                        obj.EmployeeCd = Employee.EmployeeCd;
                        obj.DefEmployeeCd = Employee.DefinedCd;
                        obj.EnteredBy = SessionCheck.getSessionUsername(); ;
                        obj.Mode = Employee.Mode;
                        obj.IPAddress = CommonVariables.IPAddress;
                    }
                    else if (Employee.Mode == "A")
                    {
                        obj.EmployeeCd = Employee.EmployeeCd;
                        obj.Approved = Employee.Approved;
                        obj.ApprovedBy = SessionCheck.getSessionUsername();
                        obj.EnteredBy = SessionCheck.getSessionUsername();                      
                        obj.Mode = Employee.Mode;
                    }
                    else
                    {
                        obj.EmployeeCd = Employee.EmployeeCd;
                        obj.DefEmployeeCd = Employee.DefinedCd;
                        obj.FirstNameEng = Employee.FirstNameEng.ConvertToString();
                        obj.MiddleNameEng = Employee.MiddleNameEng.ConvertToString();
                        obj.LastNameEng = Employee.LastNameEng.ConvertToString();
                        if (obj.MiddleNameEng == "")
                        {
                            obj.FullNameEng = obj.FirstNameEng + " " + obj.LastNameEng;
                            Employee.FullNameEng = obj.FirstNameEng + " " + obj.LastNameEng;
                        }
                        else
                        {
                            obj.FullNameEng = obj.FirstNameEng + " " + obj.MiddleNameEng + " " + obj.LastNameEng;
                            Employee.FullNameEng = obj.FirstNameEng + " " + obj.MiddleNameEng + " " + obj.LastNameEng;
                        }
                        
                        obj.FirstNameLoc = Employee.FirstNameLoc.ConvertToString();
                        obj.MiddleNameLoc = Employee.MiddleNameLoc.ConvertToString();
                        obj.LastNameLoc = Employee.LastNameLoc.ConvertToString();
                        if (obj.MiddleNameLoc == "")
                        {
                            obj.FullNameLoc = obj.FirstNameLoc + " " + obj.LastNameLoc;
                        }
                        else
                        {
                            obj.FullNameLoc = obj.FirstNameLoc + " " + obj.MiddleNameLoc + " " + obj.LastNameLoc;
                        }
                        obj.BirthDt = Employee.BirthDate.ToString("dd-MM-yyyy");
                        obj.BirthDtLoc = Employee.BirthDateLoc;
                        obj.MaritalStatusCd = GetData.GetCodeFor(DataType.MaritalStatus, Employee.MaritalStatusCd);
                        obj.GenderCd = GetData.GetCodeFor(DataType.Gender, Employee.GenderCd);

                        obj.FatherFnameEng = Employee.FatherFirstaNameEng;
                        obj.FatherMnameEng = Employee.FatherMiddleNameEng;
                        obj.FatherLnameEng = Employee.FatherLastNameEng;
                        obj.FatherFullNameEng = Employee.FatherFullNameEng;
                        obj.FatherFnameLoc = Employee.FatherFirstaNameLoc;
                        obj.FatherMnameLoc = Employee.FatherMiddleNameLoc;
                        obj.FatherLnameLoc = Employee.FatherLastNameLoc;
                        obj.FatherFullnameLoc = Employee.FatherFullNameLoc;

                        obj.MotherFnameEng = Employee.MotherFirstaNameEng;
                        obj.MotherMnameEng = Employee.MotherMiddleNameEng;
                        obj.MotherLnameEng = Employee.MotherLastNameEng;
                        obj.MotherFullNameEng = Employee.MotherFullNameEng;
                        obj.MotherFnameLoc = Employee.MotherFirstaNameLoc;
                        obj.MotherMnameLoc = Employee.MotherMiddleNameLoc;
                        obj.MotherLnameLoc = Employee.MotherLastNameLoc;
                        obj.MotherFullnameLoc = Employee.MotherFullNameLoc;

                        obj.GfatherFnameEng = Employee.GFatherFirstaNameEng;
                        obj.GfatherMnameEng = Employee.GFatherMiddleNameEng;
                        obj.GfatherLnameEng = Employee.GFatherLastNameEng;
                        obj.GfatherFullNameEng = Employee.GFatherFullNameEng;
                        obj.GfatherFnameLoc = Employee.GFatherFirstaNameLoc;
                        obj.GfatherMnameLoc = Employee.GFatherMiddleNameLoc;
                        obj.GfatherLnameLoc = Employee.GFatherLastNameLoc;
                        obj.GfatherFullnameLoc = Employee.GFatherFullNameLoc;

                        obj.SpouseFnameEng = Employee.SpouseFirstaNameEng;
                        obj.SpouseMnameEng = Employee.SpouseMiddleNameEng;
                        obj.SpouseLnameEng = Employee.SpouseLastNameEng;
                        obj.SpouseFullNameEng = Employee.SpouseFullNameEng;
                        obj.SpouseFnameLoc = Employee.SpouseFirstaNameLoc;
                        obj.SpouseMnameLoc = Employee.SpouseMiddleNameLoc;
                        obj.SpouseLnameLoc = Employee.SpouseLastNameLoc;
                        obj.SpouseFullnameLoc = Employee.SpouseFullNameLoc;

                        obj.CitizenshipNo = Employee.CitizenshipNo;
                        obj.CitizenIssueDistrict = GetData.GetCodeFor(DataType.District, Employee.CitizenshipDistrictCode);
                        obj.CitizenIssueDt = null;
                        obj.CitizenIssueDtLoc = Employee.CitizenshipIssueDateLoc;

                        obj.PassportNo = Employee.PassportNo;
                        obj.PassportIssueDistrict = GetData.GetCodeFor(DataType.District, Employee.PassportDistrictCode);
                        obj.PassportIssueDt = null;
                        obj.PassportIssueDtLoc = Employee.PassportIssueDateLoc;

                        obj.ProFundNo = Employee.ProvidentFundNo;
                        obj.CitNo = Employee.CitNo;
                        obj.PanNo = Employee.PanNo;
                        obj.DrivingLicenseNo = Employee.DrivingLicenseNo;

                        obj.PerCountryCd = Employee.PerCountryCd;
                        obj.PerRegStCd = GetData.GetCodeFor(DataType.Region, Employee.PerRegStCd);
                        obj.PerZoneCd = GetData.GetCodeFor(DataType.Zone, Employee.PerZoneCd);
                        if (!String.IsNullOrWhiteSpace(Employee.PerDistrictCd.ConvertToString()))
                        {
                            obj.PerDistrictCd = GetData.GetCodeFor(DataType.District, Employee.PerDistrictCd);
                            obj.PerZoneCd = CommonService.GetZone(obj.PerDistrictCd);
                            obj.PerRegStCd = CommonService.GetRegion(obj.PerZoneCd);
                            obj.PerCountryCd = CommonService.GetCountry(obj.PerRegStCd);
                        }
                        obj.PerVdcMunCd = GetData.GetCodeFor(DataType.VdcMun, Employee.PerVdcMunCd);
                        obj.PerWardNo = Employee.PerWardNo;
                        obj.PerStreet = Employee.PerStreet;
                        obj.PerStreetLoc = Employee.PerStreetLoc;

                        obj.TmpCountryCd = Employee.TempCountryCd;
                        obj.TmpRegStCd = GetData.GetCodeFor(DataType.Region, Employee.TempRegStCd);
                        obj.TmpZoneCd = GetData.GetCodeFor(DataType.Zone, Employee.TempZoneCd);
                        if (!String.IsNullOrWhiteSpace(Employee.TempDistrictCd.ConvertToString()))
                        {
                            obj.TmpDistrictCd = GetData.GetCodeFor(DataType.District, Employee.TempDistrictCd);
                            obj.TmpZoneCd = CommonService.GetZone(obj.TmpDistrictCd);
                            obj.TmpRegStCd = CommonService.GetRegion(obj.TmpZoneCd);
                            obj.TmpCountryCd = CommonService.GetCountry(obj.TmpRegStCd);
                        }
                        obj.TmpVdcMunCd = GetData.GetCodeFor(DataType.VdcMun, Employee.TempVdcMunCd);
                        obj.TmpWardNo = Employee.TempWardNo;
                        obj.TmpStreet = Employee.TempStreet;
                        obj.TmpStreetLoc = Employee.TempStreetLoc;

                        obj.OfficeCd = GetData.GetCodeFor(DataType.Office, Employee.OfficeCd);
                        obj.SectionCd = Employee.SectionCd;
                        if (Employee.OfficeJoinedDate.ToString() != "" && Employee.OfficeJoinedDate != null)
                        {
                            obj.OfficeJoinDt = Employee.OfficeJoinedDate.ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            obj.OfficeJoinDt = null;
                        }
                        obj.OfficeJoinDtLoc = Employee.OfficeJoinedDateLoc;
                        obj.PositionCd = GetData.GetCodeFor(DataType.Position, Employee.PositionCd);
                        obj.PosSubClassCd = GetData.GetCodeFor(DataType.SubClass, Employee.PositionSubClCd);
                        obj.DesignationCd = GetData.GetCodeFor(DataType.Designation, Employee.DesignationCd, Employee.PositionCd);
                        obj.RetiredFlag = Employee.Retired;
                        obj.RetiredDt = null;
                        obj.RetiredDtLoc = Employee.RetiredDateLoc;
                        obj.MobileNumber = Employee.MobileNumber;
                        obj.TelephoneNo = Employee.TelephoneNum;
                        obj.FaxNumber = Employee.FaxNum;
                        obj.PoBox = Employee.PoBox;
                        obj.Email = Employee.Email;
                        obj.OtherContactInfo = Employee.OtherConInfo;
                        obj.OtherContactInfoLoc = Employee.OtherConInfoLoc;
                        obj.Remarks = Employee.Remarks;
                        obj.RemarksLoc = Employee.RemarksLoc;
                        obj.EnteredBy = SessionCheck.getSessionUsername();
                        obj.EnteredDt = Employee.EnteredDate.ToString("dd-MM-yyyy");
                        string email = Employee.Email;
                        string password = Employee.Password;
                        string groupCode = Employee.GroupCode;
                        //if (Employee.Mode == "I")
                        //{
                        //    if (email == "" || password == "" || groupCode == "" || email == null || password == null || groupCode == null)
                        //    {
                        //        obj.Approved = Employee.Approved;
                        //    }
                        //    else
                        //    {
                        //        obj.Approved = true;
                        //    }
                        //}
                        //else
                        //{
                        //    obj.Approved = false;
                        //}
                        obj.Approved = false;
                        obj.ApprovedBy = SessionCheck.getSessionUsername();
                        obj.ApprovedDt = Employee.ApprovedDate.ToString("dd-MM-yyyy");
                        obj.ApprovedDtLoc = Employee.ApprovedDateLoc;
                        obj.ImageURL = Employee.ImageURL;
                        obj.Mode = Employee.Mode;
                        obj.IPAddress = CommonVariables.IPAddress;
                    }

                    if (Employee.Mode == "D")
                    {
                        service.PackageName = "PKG_COM_WEB_SECURITY";
                        service.Begin();
                        if (GetUserCode(Employee.EmployeeCd) != "" && GetUserCode(Employee.EmployeeCd) != null)
                        {
                            objWebUser.UsrCd = GetUserCode(Employee.EmployeeCd);
                            objWebUser.EnteredBy = SessionCheck.getSessionUsername(); ;
                            objWebUser.IPAddress = CommonVariables.IPAddress;
                            objWebUser.Mode = Employee.Mode;
                            QueryResult qrUser = service.SubmitChanges(objWebUser, true);
                            if (qrUser.IsSuccess == true)
                            {
                                service.PackageName = "PKG_SETUP";
                                service.Begin();
                                QueryResult qr = service.SubmitChanges(obj, true);
                            }
                        }
                        else
                        {
                            service.PackageName = "PKG_SETUP";
                            service.Begin();
                            QueryResult qr = service.SubmitChanges(obj, true);
                        }
                    }
                    else if (Employee.Mode == "A")
                    {
                        obj.EnteredBy = SessionCheck.getSessionUsername();
                        service.PackageName = "PKG_SETUP";
                        service.Begin();
                        QueryResult qr = service.SubmitChanges(obj, true);
                    }
                    else
                    {
                        service.PackageName = "PKG_SETUP";
                        service.Begin();
                        QueryResult qr = service.SubmitChanges(obj, true);
                        if (qr.IsSuccess == true)
                        {
                            string email = Employee.Email;
                            string password = Employee.Password;
                            string groupCode = Employee.GroupCode;
                            if (Employee.Mode == "I")
                            {
                                if (email == "" || password == "" || groupCode == "" || email == null || password == null || groupCode == null)
                                {
                                }
                                else
                                {
                                    service.PackageName = "PKG_COM_WEB_SECURITY";
                                    objWebUser.UsrCd = null;
                                    objWebUser.UsrName = Employee.FullNameEng;
                                    if (password != null && password != "")
                                    {
                                        objWebUser.Password = Utils.EncryptString(Employee.Password.Trim());
                                    }
                                    objWebUser.EmpCd = qr["V_EMPLOYEE_CD"].ToString();
                                    objWebUser.Status = "E";
                                    objWebUser.ExpiryDt = DateTime.Now.AddMonths(6).ToString("dd-MMM-yyyy").ToLower();
                                    objWebUser.EnteredBy = SessionCheck.getSessionUsername();
                                    objWebUser.IPAddress = CommonVariables.IPAddress;
                                    objWebUser.Email = Employee.Email;
                                    //if (Employee.Mode == "I")
                                    //{
                                    //    if (email == "" || password == "" || groupCode == "" || email == null || password == null || groupCode == null)
                                    //    {
                                    //        objWebUser.Approved = Employee.Approved;
                                    //    }
                                    //    else
                                    //    {
                                    //        objWebUser.Approved = true;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    objWebUser.Approved = false;
                                    //}
                                    objWebUser.Approved = false;
                                    objWebUser.ApprovedBy = SessionCheck.getSessionUsername();
                                    objWebUser.ApprovedDt = Employee.ApprovedDate.ToString("dd-MM-yyyy");
                                    if (Employee.Verification_Required==true)
                                    {
                                        objWebUser.Verification_Required = "Y";
                                    }
                                    else
                                    {
                                        objWebUser.Verification_Required = "N";
                                    }
                                   
                                    objWebUser.Verification_Code = null;
                                    objWebUser.Mobile_No = Employee.MobileNumber;
                                    objWebUser.Verification_Validity = null;
                                    //objWebUser.LastUpdatedBy = SessionCheck.getSessionUsername();
                             
                                    objWebUser.Mode = Employee.Mode;
                                    QueryResult qrUser = service.SubmitChanges(objWebUser, true);

                                    if (qrUser.IsSuccess == true)
                                    {
                                        string toName = "";
                                        if (Employee.MiddleNameEng == "" || Employee.MiddleNameEng == null)
                                        {
                                            toName = Employee.FirstNameEng + " " + Employee.LastNameEng;
                                        }
                                        else
                                        {
                                            toName = Employee.FirstNameEng + " " + Employee.MiddleNameEng + " " + Employee.LastNameEng;
                                        }
                                        EmailMessage emMessage = new EmailMessage();
                                        emMessage.From = "";
                                        emMessage.To = objWebUser.Email;
                                        emMessage.Subject = "User Created";
                                        emMessage.Body = "Dear " + toName + ",<br> Your user is created as username: " + objWebUser.Email + " password: " + Employee.Password.Trim() + ". Your account is still not verified please contact administrator for account verification.<br><br> Thank You";
                                        Core.MailSend.SendMail(emMessage);
                                        objGrp.UsrCd = qrUser["v_usr_cd"].ToString();
                                        objGrp.GrpCd = Employee.GroupCode;
                                        objGrp.EnteredBy = Employee.EnteredBy;
                                        objGrp.LastUpdatedBy = Employee.EnteredBy;
                                        objGrp.Ipaddress = CommonVariables.IPAddress;
                                        objGrp.Mode = Employee.Mode;
                                        QueryResult qrGrp = service.SubmitChanges(objGrp, true);
                                    }
                                }
                            }                          
                            
                        }
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

            }
        }

        public DataTable Employee_GetAllCriteriaToTable(string initialStr, string distCode, string vdcMunCode, string ward, string group, string fullname, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            using (ServiceFactory service = new ServiceFactory())
            {
               
                cmdText = @"select T1.EMPLOYEE_CD,T1.DEF_EMPLOYEE_CD," + Utils.ToggleLanguage("T1.FULL_NAME_ENG", "T1.FULL_NAME_LOC") + " as EMPLOYEE_FULL_NAME," + Utils.ToggleLanguage("T2.DESC_ENG", "T2.DESC_LOC") + " AS OFFICE_NAME,T5.GRP_NAME," + Utils.ToggleLanguage("T7.DESC_ENG", "T7.DESC_LOC") + " AS DISTRICT_NAME," + Utils.ToggleLanguage("T6.DESC_ENG", "T6.DESC_LOC") + " AS VDC_MUN_NAME,T1.PER_WARD_NO,T1.APPROVED,T1.APPROVED_BY,T1.PER_DISTRICT_CD from com_Employee T1 inner join MIS_Office T2 on T1.OFFICE_CD=T2.OFFICE_CD left join com_web_usr T3 on T1.EMPLOYEE_CD=T3.EMP_CD left join com_web_usr_grp T4 on T4.USR_CD=T3.USR_CD left join com_web_grp T5 on T5.GRP_CD=T4.GRP_CD left join mis_vdc_municipality T6 on T1.PER_VDC_MUN_CD=T6.VDC_MUN_CD left join mis_district T7 on T1.PER_DISTRICT_CD=T7.DISTRICT_CD WHERE 1=1";

                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {

                    cmdText += String.Format(" AND Upper(" + Utils.ToggleLanguage("T1.FULL_NAME_ENG", "T1.FULL_NAME_LOC") + ") like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby == "DEF_EMPLOYEE_CD")
                    {
                        cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
                    }
                    else
                    {
                        cmdText += String.Format(" order by {0} {1}", orderby, order);
                    }
                }
                else
                {

                    if (distCode != "" && distCode != null && distCode != "0")
                    {
                        cmdText += String.Format(" AND T1.PER_DISTRICT_CD='" + distCode + "'");
                    }
                    if (vdcMunCode != "" && vdcMunCode != null && vdcMunCode != "0")
                    {
                        cmdText += String.Format(" AND T1.PER_VDC_MUN_CD='" + vdcMunCode + "'");
                    }
                    if (ward != "" && ward != null && ward != "0")
                    {
                        cmdText += String.Format(" AND T1.PER_WARD_NO='" + ward + "'");
                    }
                    if (group != "" && group != null && group != "0")
                    {
                        cmdText += String.Format(" AND T5.GRP_CD='" + group + "'");
                    }


                    if (fullname != "" && fullname != "null" && fullname != null)
                    {

                        if (sessionLanguage == "English")
                        {
                            cmdText += String.Format(" AND Upper(T1.FULL_NAME_ENG) like '%{0}%'", fullname.ToUpper());
                        }
                        else
                        {
                            cmdText += String.Format(" AND T1.FULL_NAME_LOC like '%{0}%'", fullname);
                        }
                    }

                }
                try
                {
                    if (orderby == "T7.DESC_ENG%2CT6.DESC_ENG%2CT1.PER_WARD_NO")
                    {
                        cmdText = cmdText.Replace("%2C", " " + order + ",");
                    }
                    else if (orderby == "T7.DESC_LOC%2CT6.DESC_LOC%2CT1.PER_WARD_NO")
                    {
                        cmdText = cmdText.Replace("%2C", " " + order + ",");
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
                    dtbl = null;
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
            return dtbl;
        }

        public bool CheckDuplicateDefinedCode(string DefinedCd, string code)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {                
                try
                {                   
                    if (code == string.Empty)
                    {
                        cmdText = "select * from COM_EMPLOYEE where DEF_EMPLOYEE_CD ='" + DefinedCd + "'";
                    }
                    else
                    {
                        cmdText = "select * from COM_EMPLOYEE where DEF_EMPLOYEE_CD ='" + DefinedCd + "' and EMPLOYEE_CD !='" + code + "'";
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
                    return false;
                }
                return true;
            }
        }

        public bool CheckDuplicateUserCode(string UserCodeCd, string Employeecode)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
               
                try
                {
                    if (Employeecode == string.Empty)
                    {
                        cmdText = "select * from COM_WEB_USR where EMAIL ='" + UserCodeCd + "'";
                    }
                    else
                    {
                        cmdText = "select * from COM_WEB_USR where EMAIL ='" + UserCodeCd + "' and EMP_CD !='" + Employeecode + "'";
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
                    return false;
                }
                return true;
            }
        }

        public MISEmployee FillEmployee(string EmployeeCd)
        {
            MISEmployee obj = new MISEmployee();
            DataTable dTable = new DataTable();
            dTable = null;
            using (ServiceFactory EmployeeService = new ServiceFactory())
            {
                try
                {
                    EmployeeService.Begin();
                    string CmdText = "select T1.EMPLOYEE_CD,T1.DEF_EMPLOYEE_CD,T1.MARITAL_STATUS_CD,T1.EMP_PHOTO_ID,T1.FIRST_NAME_ENG,T1.MIDDLE_NAME_ENG,T1.LAST_NAME_ENG,T1.FIRST_NAME_LOC,T1.MIDDLE_NAME_LOC,T1.LAST_NAME_LOC,T1.GENDER_CD,T1.BIRTH_DT,T1.BIRTH_DT_LOC,T1.PER_DISTRICT_CD,T1.PER_VDC_MUN_CD,T1.PER_STREET,T1.PER_STREET_LOC,T1.PER_WARD_NO,T2.DESC_ENG,T5.GRP_NAME,T1.PER_DISTRICT_CD,T1.PER_VDC_MUN_CD,T1.PER_WARD_NO,T1.OFFICE_CD,T1.POSITION_CD,T1.DESIGNATION_CD,T1.OFFICE_JOIN_DT,T1.OFFICE_JOIN_DT_LOC,T3.USR_CD,T5.GRP_CD,T3.EMAIL,T3.VERIFICATION_REQUIRED from com_Employee T1 inner join MIS_Office T2 on T1.OFFICE_CD=T2.OFFICE_CD left join com_web_usr T3 on T1.EMPLOYEE_CD=T3.EMP_CD left join com_web_usr_grp T4 on T4.USR_CD=T3.USR_CD left join com_web_grp T5 on T5.GRP_CD=T4.GRP_CD where T1.EMPLOYEE_CD='" + EmployeeCd + "'";
                    dTable = EmployeeService.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }
                    });                  

                    if (dTable.Rows.Count > 0)
                    {
                        obj.EmployeeCd = dTable.Rows[0]["EMPLOYEE_CD"].ConvertToString();
                        obj.DefinedCd = dTable.Rows[0]["DEF_EMPLOYEE_CD"].ConvertToString();
                        obj.FirstNameEng = dTable.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                        obj.MiddleNameEng = dTable.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                        obj.LastNameEng = dTable.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                        obj.FirstNameLoc = dTable.Rows[0]["FIRST_NAME_LOC"].ConvertToString();
                        obj.MiddleNameLoc = dTable.Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                        obj.LastNameLoc = dTable.Rows[0]["LAST_NAME_LOC"].ConvertToString();
                        obj.GenderCd = dTable.Rows[0]["GENDER_CD"].ConvertToString();
                        obj.BirthDate = Convert.ToDateTime(dTable.Rows[0]["BIRTH_DT"]);
                        obj.BirthDateLoc = dTable.Rows[0]["BIRTH_DT_LOC"].ConvertToString();
                        if (dTable.Rows[0]["PER_DISTRICT_CD"].ConvertToString() != "")
                        {
                            obj.PerDistrictCd = dTable.Rows[0]["PER_DISTRICT_CD"].ConvertToString();
                        }
                        if (dTable.Rows[0]["PER_VDC_MUN_CD"].ToString() != "")
                        {
                            obj.PerVdcMunCd = dTable.Rows[0]["PER_VDC_MUN_CD"].ConvertToString();
                        }
                        obj.PerStreet = dTable.Rows[0]["PER_STREET"].ToString();
                        obj.PerStreetLoc = dTable.Rows[0]["PER_STREET_LOC"].ConvertToString();
                        if (dTable.Rows[0]["PER_WARD_NO"].ToString() != "")
                        {
                            obj.PerWardNo = dTable.Rows[0]["PER_WARD_NO"].ConvertToString();
                        }
                        obj.OfficeCd = dTable.Rows[0]["OFFICE_CD"].ConvertToString();
                        obj.PositionCd = dTable.Rows[0]["POSITION_CD"].ConvertToString();
                        obj.DesignationCd = dTable.Rows[0]["DESIGNATION_CD"].ConvertToString();
                        obj.MaritalStatusCd = dTable.Rows[0]["MARITAL_STATUS_CD"].ConvertToString();
                        if (dTable.Rows[0]["OFFICE_JOIN_DT"].ToString() != "")
                        {
                            obj.OfficeJoinedDate = Convert.ToDateTime(dTable.Rows[0]["OFFICE_JOIN_DT"]);
                        }
                        obj.OfficeJoinedDateLoc = dTable.Rows[0]["OFFICE_JOIN_DT_LOC"].ConvertToString();
                        obj.UserCode = dTable.Rows[0]["EMAIL"].ConvertToString();
                        obj.GroupCode = dTable.Rows[0]["GRP_CD"].ConvertToString();
                        obj.Email = dTable.Rows[0]["EMAIL"].ConvertToString();
                        obj.ImageURL = dTable.Rows[0]["EMP_PHOTO_ID"].ConvertToString();
                        if (obj.ImageURL == null || obj.ImageURL == "")
                        {
                            obj.ImageURL = "../../Files/images/employee/anon1.jpg";
                        }
                        if (dTable.Rows[0]["VERIFICATION_REQUIRED"].ConvertToString() == "Y")
                        {
                            obj.Verification_Required = true;
                        }
                        else
                        {
                            obj.Verification_Required = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (EmployeeService.Transaction.Connection != null)
                    {
                        EmployeeService.End();
                    }
                }
            }
            return obj;

        }

        public List<MISEmployee> GetGenderbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISEmployee> lstEmployee = new List<MISEmployee>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();                   
                    cmdText = "select GENDER_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_GENDER where 1=1";
                    if (code != "")
                    {
                        cmdText += " and GENDER_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstEmployee = new List<MISEmployee>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstEmployee.Add(new MISEmployee { GenderCd = drow["GENDER_CD"].ConvertToString(), GenderName = drow["DESCRIPTION"].ConvertToString() });
            }

            return lstEmployee;
        }

        public List<MISEmployee> GetDesignationbyCodeandDesc(string code, string desc, string posid)
        {
            DataTable dtbl = null;
            List<MISEmployee> lstEmployee = new List<MISEmployee>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select T1.DESIGNATION_CD,T1.DEFINED_CD," + Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC") + " as DESCRIPTION FROM MIS_DESIGNATION T1 inner join MIS_POSITION_DESIGNATION T2 on T1.DESIGNATION_CD=T2.DESIGNATION_CD where 1 = 1 and T2.POSITION_CD='" + posid + "' ";
                    
                    if (code != "")
                    {
                        cmdText += " and T1.DESIGNATION_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(T1.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and T1.DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstEmployee = new List<MISEmployee>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstEmployee.Add(new MISEmployee { DesignationCd = drow["DESIGNATION_CD"].ConvertToString(), DefinedCd = drow["DEFINED_CD"].ConvertToString(), DesignationName = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstEmployee;
        }

        public List<MISEmployee> GetMarriedbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISEmployee> lstEmployee = new List<MISEmployee>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();                   
                    cmdText = "select MARITAL_STATUS_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_MARITAL_STATUS where 1=1";
                    if (code != "")
                    {
                        cmdText += " and MARITAL_STATUS_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstEmployee = new List<MISEmployee>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstEmployee.Add(new MISEmployee { MaritalStatusCd = drow["MARITAL_STATUS_CD"].ConvertToString(), MaritalName = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstEmployee;
        }

        public void ManageUser(MISUserRegistration Employee, string Step, out string ErrMsg)
        {
            ErrMsg = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ComWebUsrGrpInfo objGrp = new ComWebUsrGrpInfo();
                    ComWebUsrInfo objWebUser = new ComWebUsrInfo();
                    //ComEmployeeInfo obj = new ComEmployeeInfo();
                    ComEmployeeInfo objPosted = null;

                    if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["PostedComEmployeeInfo"] != null)
                        objPosted = (ComEmployeeInfo)System.Web.HttpContext.Current.Session["PostedComEmployeeInfo"];
                    else
                        objPosted = new ComEmployeeInfo();

                    if (Step == "1")
                    {
                        objPosted.EmployeeCd = Employee.EmployeeCd;
                        objPosted.FirstNameEng = Employee.FirstNameEng.ConvertToString();
                        objPosted.MiddleNameEng = Employee.MiddleNameEng.ConvertToString();
                        objPosted.LastNameEng = Employee.LastNameEng.ConvertToString();
                        if (objPosted.MiddleNameEng == "")
                        {
                            objPosted.FullNameEng = objPosted.FirstNameEng + " " + objPosted.LastNameEng;
                        }
                        else
                        {
                            objPosted.FullNameEng = objPosted.FirstNameEng + " " + objPosted.MiddleNameEng + " " + objPosted.LastNameEng;
                        }
                        objPosted.FirstNameLoc = Employee.FirstNameLoc.ConvertToString();
                        objPosted.MiddleNameLoc = Employee.MiddleNameLoc.ConvertToString();
                        objPosted.LastNameLoc = Employee.LastNameLoc.ConvertToString();
                        if (objPosted.MiddleNameLoc == "")
                        {
                            objPosted.FullNameLoc = objPosted.FirstNameLoc + " " + objPosted.LastNameLoc;
                        }
                        else
                        {
                            objPosted.FullNameLoc = objPosted.FirstNameLoc + " " + objPosted.MiddleNameLoc + " " + objPosted.LastNameLoc;
                        }
                        objPosted.BirthDt = Employee.BirthDate.ToString("dd-MM-yyyy");
                        objPosted.BirthDtLoc = Employee.BirthDateLoc;
                        objPosted.MaritalStatusCd = GetData.GetCodeFor(DataType.MaritalStatus, Employee.MaritalStatusCd);
                        objPosted.GenderCd = GetData.GetCodeFor(DataType.Gender, Employee.GenderCd);

                        objPosted.FatherFnameEng = Employee.FatherFirstaNameEng;
                        objPosted.FatherMnameEng = Employee.FatherMiddleNameEng;
                        objPosted.FatherLnameEng = Employee.FatherLastNameEng;
                        objPosted.FatherFullNameEng = Employee.FatherFullNameEng;
                        objPosted.FatherFnameLoc = Employee.FatherFirstaNameLoc;
                        objPosted.FatherMnameLoc = Employee.FatherMiddleNameLoc;
                        objPosted.FatherLnameLoc = Employee.FatherLastNameLoc;
                        objPosted.FatherFullnameLoc = Employee.FatherFullNameLoc;

                        objPosted.MotherFnameEng = Employee.MotherFirstaNameEng;
                        objPosted.MotherMnameEng = Employee.MotherMiddleNameEng;
                        objPosted.MotherLnameEng = Employee.MotherLastNameEng;
                        objPosted.MotherFullNameEng = Employee.MotherFullNameEng;
                        objPosted.MotherFnameLoc = Employee.MotherFirstaNameLoc;
                        objPosted.MotherMnameLoc = Employee.MotherMiddleNameLoc;
                        objPosted.MotherLnameLoc = Employee.MotherLastNameLoc;
                        objPosted.MotherFullnameLoc = Employee.MotherFullNameLoc;

                        objPosted.GfatherFnameEng = Employee.GFatherFirstaNameEng;
                        objPosted.GfatherMnameEng = Employee.GFatherMiddleNameEng;
                        objPosted.GfatherLnameEng = Employee.GFatherLastNameEng;
                        objPosted.GfatherFullNameEng = Employee.GFatherFullNameEng;
                        objPosted.GfatherFnameLoc = Employee.GFatherFirstaNameLoc;
                        objPosted.GfatherMnameLoc = Employee.GFatherMiddleNameLoc;
                        objPosted.GfatherLnameLoc = Employee.GFatherLastNameLoc;
                        objPosted.GfatherFullnameLoc = Employee.GFatherFullNameLoc;

                        objPosted.SpouseFnameEng = Employee.SpouseFirstaNameEng;
                        objPosted.SpouseMnameEng = Employee.SpouseMiddleNameEng;
                        objPosted.SpouseLnameEng = Employee.SpouseLastNameEng;
                        objPosted.SpouseFullNameEng = Employee.SpouseFullNameEng;
                        objPosted.SpouseFnameLoc = Employee.SpouseFirstaNameLoc;
                        objPosted.SpouseMnameLoc = Employee.SpouseMiddleNameLoc;
                        objPosted.SpouseLnameLoc = Employee.SpouseLastNameLoc;
                        objPosted.SpouseFullnameLoc = Employee.SpouseFullNameLoc;

                        objPosted.CitizenshipNo = Employee.CitizenshipNo;
                        objPosted.CitizenIssueDistrict = GetData.GetCodeFor(DataType.District, Employee.CitizenshipDistrictCode);
                        objPosted.CitizenIssueDt = null;
                        objPosted.CitizenIssueDtLoc = Employee.CitizenshipIssueDateLoc;

                        objPosted.PassportNo = Employee.PassportNo;
                        objPosted.PassportIssueDistrict = GetData.GetCodeFor(DataType.District, Employee.PassportDistrictCode);
                        objPosted.PassportIssueDt = null;
                        objPosted.PassportIssueDtLoc = Employee.PassportIssueDateLoc;

                        objPosted.ProFundNo = Employee.ProvidentFundNo;
                        objPosted.CitNo = Employee.CitNo;
                        objPosted.PanNo = Employee.PanNo;
                        objPosted.DrivingLicenseNo = Employee.DrivingLicenseNo;

                        objPosted.ImageURL = Employee.ImageURL;

                        objPosted.PerCountryCd = Employee.PerCountryCd;
                        objPosted.PerRegStCd = GetData.GetCodeFor(DataType.Region, Employee.PerRegStCd);
                        objPosted.PerZoneCd = GetData.GetCodeFor(DataType.Zone, Employee.PerZoneCd);
                        objPosted.PerDistrictCd = GetData.GetCodeFor(DataType.District, Employee.PerDistrictCd);
                        objPosted.PerVdcMunCd = GetData.GetCodeFor(DataType.VdcMun, Employee.PerVdcMunCd);
                        objPosted.PerWardNo = Employee.PerWardNo;
                        objPosted.PerStreet = Employee.PerStreet;
                        objPosted.PerStreetLoc = Employee.PerStreetLoc;

                        objPosted.TmpCountryCd = Employee.TempCountryCd;
                        objPosted.TmpRegStCd = GetData.GetCodeFor(DataType.Region, Employee.TempRegStCd);
                        objPosted.TmpZoneCd = GetData.GetCodeFor(DataType.Zone, Employee.TempZoneCd);
                        objPosted.TmpDistrictCd = GetData.GetCodeFor(DataType.District, Employee.TempDistrictCd);
                        objPosted.TmpVdcMunCd = GetData.GetCodeFor(DataType.VdcMun, Employee.TempVdcMunCd);
                        objPosted.TmpWardNo = Employee.TempWardNo;
                        objPosted.TmpStreet = Employee.TempStreet;
                        objPosted.TmpStreetLoc = Employee.TempStreetLoc;

                        objPosted.DefEmployeeCd = Employee.DefinedCd;
                        objPosted.OfficeCd = GetData.GetCodeFor(DataType.Office, Employee.OfficeCd);
                        objPosted.SectionCd = Employee.SectionCd;
                        if (Employee.OfficeJoinedDate.ToString() != "" && Employee.OfficeJoinedDate != null)
                        {
                            objPosted.OfficeJoinDt = Employee.OfficeJoinedDate.ConvertToString("dd-MM-yyyy");
                        }
                        else
                        {
                            objPosted.OfficeJoinDt = null;
                        }
                        objPosted.OfficeJoinDtLoc = Employee.OfficeJoinedDateLoc;
                        objPosted.PositionCd = GetData.GetCodeFor(DataType.Position, Employee.PositionCd);
                        objPosted.PosSubClassCd = GetData.GetCodeFor(DataType.SubClass, Employee.PositionSubClCd);
                        objPosted.DesignationCd = GetData.GetCodeFor(DataType.Designation, Employee.DesignationCd, Employee.PositionCd);
                        objPosted.RetiredFlag = Employee.Retired;
                        objPosted.RetiredDt = null;
                        objPosted.RetiredDtLoc = Employee.RetiredDateLoc;
                        objPosted.MobileNumber = Employee.MobileNumber;
                        objPosted.TelephoneNo = Employee.TelephoneNum;
                        objPosted.FaxNumber = Employee.FaxNum;
                        objPosted.PoBox = Employee.PoBox;
                        objPosted.OtherContactInfo = Employee.OtherConInfo;
                        objPosted.OtherContactInfoLoc = Employee.OtherConInfoLoc;
                        objPosted.Remarks = Employee.Remarks;
                        objPosted.RemarksLoc = Employee.RemarksLoc;
                        objPosted.EnteredBy = SessionCheck.getSessionUsername();
                        objPosted.EnteredDt = Employee.EnteredDate.ToString("dd-MM-yyyy");
                    }
                    else if (Step == "2" && objPosted != null)
                    {
                        objPosted.Email = Employee.Email;
                        string email = Employee.Email;
                        string password = Employee.Password;
                        string groupCode = Employee.GroupCode;
                        //if (Employee.Mode == "I")
                        //{
                        //    if (email == "" || password == "" || groupCode == "" || email == null || password == null || groupCode == null)
                        //    {
                        //        objPosted.Approved = Employee.Approved;
                        //    }
                        //    else
                        //    {
                        //        objPosted.Approved = true;
                        //    }
                        //}
                        //else
                        //{
                        //    objPosted.Approved = false;
                        //}
                        objPosted.Approved = false;
                        objPosted.ApprovedBy = SessionCheck.getSessionUsername();
                        objPosted.ApprovedDt = Employee.ApprovedDate.ToString("dd-MM-yyyy");
                        objPosted.ApprovedDtLoc = Employee.ApprovedDateLoc;
                        objPosted.ImageURL = Employee.ImageURL;
                        objPosted.Mode = Employee.Mode;
                        objPosted.IPAddress = CommonVariables.IPAddress;
                    }

                    if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
                        System.Web.HttpContext.Current.Session["PostedComEmployeeInfo"] = objPosted;
                    
                    if (Step == "2" && objPosted != null)
                    {
                        service.PackageName = "PKG_SETUP";
                        service.Begin();
                        QueryResult qr = service.SubmitChanges(objPosted, true);
                        //QueryResult qr = new QueryResult();
                        if (qr.IsSuccess == true)
                        {
                            string email = Employee.Email;
                            string password = Employee.Password;
                            string groupCode = ConstantVariables.GeneralGroupID; //Employee.GroupCode;
                            if (Employee.Mode == "I")
                            {
                                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(groupCode))
                                {
                                    ErrMsg = "The User details was not specified";
                                }
                                else
                                {
                                    service.PackageName = "PKG_COM_WEB_SECURITY";
                                    objWebUser.UsrCd = null;
                                    objWebUser.UsrName = objPosted.FullNameEng;
                                    if (password != null && password != "")
                                    {
                                        objWebUser.Password = Utils.EncryptString(Employee.Password.Trim());
                                    }
                                    objWebUser.EmpCd = qr["V_EMPLOYEE_CD"].ToString();
                                    objWebUser.Status = "E";
                                    objWebUser.ExpiryDt = DateTime.Now.AddMonths(6).ToString("dd-MMM-yyyy").ToLower();
                                    objWebUser.EnteredBy = SessionCheck.getSessionUsername();
                                    objWebUser.LastUpdatedBy = SessionCheck.getSessionUsername();
                                    objWebUser.Email = objPosted.Email;
                                    objWebUser.IPAddress = CommonVariables.IPAddress;
                                    objWebUser.Mode = objPosted.Mode;
                                    QueryResult qrUser = service.SubmitChanges(objWebUser, true);

                                    if (qrUser.IsSuccess == true)
                                    {
                                        objGrp.UsrCd = qrUser["v_usr_cd"].ToString();
                                        objGrp.GrpCd = ConstantVariables.GeneralGroupID;//Employee.GroupCode;
                                        objGrp.EnteredBy = SessionCheck.getSessionUsername();
                                        objGrp.LastUpdatedBy = SessionCheck.getSessionUsername();
                                        objGrp.Ipaddress = CommonVariables.IPAddress;
                                        objGrp.Mode = objPosted.Mode;
                                        QueryResult qrGrp = service.SubmitChanges(objGrp, true);
                                        if(!qrGrp.IsSuccess)
                                            ErrMsg = "Could not assign User Group.";
                                    }
                                    else
                                        ErrMsg = "Could not create User.";
                                }
                            }

                        }
                        else
                            ErrMsg = "Could not create Employee.";
                    }
                }
                catch (OracleException oe)
                {
                    ErrMsg = "There was error while creating User";
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ErrMsg = "There was error while creating User";
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null && service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
        }
    }
}
