using MIS.Models.Inspection;
using MIS.Services.Core;
using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Inspection
{
    public class MiniInspectionController : BaseController
    {
        //
        // GET: /MiniInspection/
        CommonFunction com = new CommonFunction();
        public ActionResult InspectionSearch(string p)
        {

            //ViewData["ddl_Districts"] = com.GetAllDistricts("");
            ViewData["ddl_Districts"] = com.GetDistricts("");
            ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");

            return View();
        }

        [HttpPost]
        public ActionResult InspectionSearch(FormCollection Fc)
        {

            string districtCd = "";
            string vdcMunCd = "";
            string ward = "";
            string beneficiaryname = "";
            string paNumber = "";
            MiniInspectionServiceClass objServ = new MiniInspectionServiceClass();
            districtCd = com.GetCodeFromDataBase(Fc["ddl_Districts"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            vdcMunCd = com.GetCodeFromDataBase(Fc["vdc"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            ward = Fc["ward"].ConvertToString();
            beneficiaryname = Fc["nameBenf"].ConvertToString();
            paNumber = Fc["TB1"].ConvertToString() + "-" + Fc["TB2"].ConvertToString() + "-" + Fc["TB3"].ConvertToString() + "-" + Fc["TB4"].ConvertToString() + "-" + Fc["TB5"].ConvertToString();
            if (paNumber == "----")
            {
                paNumber = "";
            }

            DataTable results = objServ.InspectionSearch(districtCd, vdcMunCd, ward, beneficiaryname, paNumber);
            ViewData["results"] = results;

            return PartialView("_MiniInspectionSearchResults");
        }


        //manage mini inspection
        public ActionResult ManageMiniInspection(string p)
        {
            string paNumber = "";
            string Tranche = "";
            string Status = "";
            DataTable dtInspInfo = new DataTable();
            DataTable dtOwnerDtl = new DataTable();
            MiniInspectionServiceClass objServ = new MiniInspectionServiceClass();
            MiniInspectionModelClass objInspectn = new MiniInspectionModelClass();

            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {

                

                rvd = QueryStringEncrypt.DecryptString(p);
                if (p != null)
                { 
                    if (rvd != null)
                    {
                        if (rvd["id1"] != null)
                        {
                            paNumber = rvd["id1"].ToString();
                        }
                        if (rvd["id2"] != null)
                        {
                            Tranche = rvd["id2"].ToString();
                        }
                        if (rvd["id3"] != null)
                        {
                            Status = rvd["id3"].ToString();
                        }

                        if (Status == "P")
                        {
                            if (Tranche == "2")
                            {
                                objInspectn.MODE = "I";
                            }
                            else
                            {
                                objInspectn.MODE = "U";
                            } 
                        }
                        
                        else
                        {
                            objInspectn.MODE = "U";
                        }


                        dtOwnerDtl = objServ.InspectionSearch(null, null, null, null, paNumber);
                        if (dtOwnerDtl != null && dtOwnerDtl.Rows.Count > 0)
                        {
                            List<InspectionOwnerDetailModelClass> objOwnList = new List<InspectionOwnerDetailModelClass>();
                            foreach (DataRow drOwn in dtOwnerDtl.Rows)
                            {
                                InspectionOwnerDetailModelClass objOwn = new InspectionOwnerDetailModelClass();
                                objInspectn.DISTRICT_CD = drOwn["DISTRICT_CD"].ConvertToString();
                                objInspectn.VDC_MUN_CD = drOwn["VDC_MUN_CD"].ConvertToString();
                                objInspectn.WARD_NO = drOwn["WARD_NO"].ConvertToString();


                                objOwn.OwnerFNameE = Utils.ToggleLanguage(drOwn["BENEFICIARY_NAME"].ConvertToString(), drOwn["BENEFICIARY_NAME_LOC"].ConvertToString());
                                objOwn.ReceipentName = Utils.ToggleLanguage(drOwn["RECIPIENT_NAME"].ConvertToString(), drOwn["RECIPIENT_NAME_LOC"].ConvertToString());
                                objOwn.OwnerDistCd = Utils.ToggleLanguage(drOwn["DISTRICT_ENG"].ConvertToString(), drOwn["DISTRICT_LOC"].ConvertToString());
                                objOwn.OwnVdcCd = Utils.ToggleLanguage(drOwn["VDC_MUNICIPALITY_ENG"].ConvertToString(), drOwn["VDC_MUNICIPALITY_LOC"].ConvertToString());
                                objOwn.OwnWaard = drOwn["WARD_NO"].ConvertToString();
                                objOwn.paNumber = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                objOwnList.Add(objOwn);
                            }
                            objInspectn.ListOwner = objOwnList;
                        }



                        string beneficiaryFullName = "";
                        objInspectn.RECIEPENT_NAME = Utils.ToggleLanguage(dtOwnerDtl.Rows[0]["RECIPIENT_NAME"].ConvertToString(), dtOwnerDtl.Rows[0]["RECIPIENT_NAME_LOC"].ConvertToString());
                        objInspectn.BENEFICIARY_NAME = Utils.ToggleLanguage(dtOwnerDtl.Rows[0]["BENEFICIARY_NAME"].ConvertToString(), dtOwnerDtl.Rows[0]["BENEFICIARY_NAME"].ConvertToString());

                        beneficiaryFullName = Utils.ToggleLanguage(dtOwnerDtl.Rows[0]["RECIPIENT_NAME"].ConvertToString(), dtOwnerDtl.Rows[0]["RECIPIENT_NAME_LOC"].ConvertToString());
                        string[] name = beneficiaryFullName.Split(' ');
                        if (name.Length == 3)
                        {
                            objInspectn.BENEFICIARY_FNAME = name[0];
                            objInspectn.BENEFICIARY_FNAME = name[1];
                            objInspectn.BENEFICIARY_FNAME = name[2];
                        }
                        else if (name.Length == 2)
                        {
                            objInspectn.BENEFICIARY_FNAME = name[0];
                            objInspectn.BENEFICIARY_FNAME = name[1];
                        }
                        else
                        {
                            objInspectn.BENEFICIARY_FNAME = "";
                        }
                        objInspectn.NRA_DEFINED_CD = dtOwnerDtl.Rows[0]["NRA_DEFINED_CD"].ConvertToString();

                        dtInspInfo = objServ.getInspectionData(paNumber); 
                        if(dtInspInfo!= null && dtInspInfo.Rows.Count>0)
                        {
                            foreach(DataRow dr in dtInspInfo.Rows)
                            {
                                 if(objInspectn.MODE=="I")
                                {
                                    beneficiaryFullName = Utils.ToggleLanguage(dtOwnerDtl.Rows[0]["RECIPIENT_NAME"].ConvertToString(), dtOwnerDtl.Rows[0]["RECIPIENT_NAME_LOC"].ConvertToString()); 

                                }
                                else
                                {
                                      beneficiaryFullName = dr["BENEFICIARY_NAME"].ConvertToString();

                                }
                                  name = beneficiaryFullName.Split(' ');
                                if(name.Length==3)
                                {
                                    objInspectn.BENEFICIARY_FNAME = name[0];
                                    objInspectn.BENEFICIARY_MNAME = name[1];
                                    objInspectn.BENEFICIARY_LNAME = name[2];
                                }
                                else if (name.Length == 2)
                                {
                                    objInspectn.BENEFICIARY_FNAME = name[0];
                                     objInspectn.BENEFICIARY_LNAME = name[2];
                                }
                                else
                                {
                                    objInspectn.BENEFICIARY_FNAME = "";
                                }

                                if(Tranche=="2")
                                {
                                    objInspectn.REMARKS = dr["REMARKS_TWO"].ConvertToString();
                                }
                                else if (Tranche == "3")
                                {
                                    objInspectn.REMARKS = dr["REMARKS_THREE"].ConvertToString();
                                }
                                else
                                {
                                    objInspectn.REMARKS = dr["REMARKS"].ConvertToString();

                                }
                                objInspectn.BANK_ACC_NO = dr["BANK_ACC_NO"].ConvertToString();
                                objInspectn.MOBILE_NO = dr["MOBILE_NO"].ConvertToString();
                                objInspectn.BANK_CD = dr["BANK_CD"].ConvertToString();
                                 ViewData["ddl_bankname"] = com.GetBankName(dr["BANK_CD"].ConvertToString(), "", "");
                                 objInspectn.SECOND_TRANCHE = dr["SECOND_TRANCHE"].ConvertToString();
                                 objInspectn.THIRD_TRANCHE = dr["THIRD_TRANCHE"].ConvertToString();
                                 objInspectn.TRANCHE = Tranche;

                                ViewData["ddl_design"] = com.GetallHouseModelForInspectionOne(dr["HOUSE_DESIGN_NO"].ConvertToString());
                                ViewData["ddl_tranche"] = (List<SelectListItem>)objServ.getinspectionTranche(Tranche).Data;
                                return View(objInspectn);
                            }
                        }

                    }
                }
                ViewData["ddl_tranche"] = (List<SelectListItem>)objServ.getinspectionTranche(Tranche).Data;
                ViewData["ddl_design"] = com.GetallHouseModelForInspectionOne("");
                ViewData["ddl_bankname"] = com.GetBankName("");

                
            }
            catch
            {

            }
            return View(objInspectn);
        }


        [HttpPost]
        public ActionResult ManageMiniInspection(MiniInspectionModelClass objInspectn, FormCollection fc)
        {
            if (fc["ddl_tranche"].ConvertToString() == "1")
            {
                 objInspectn.FIRST_TRANCHE= "1";
            }
            if (fc["ddl_tranche"].ConvertToString() == "2")
            {
                objInspectn.REMARKS_TWO = objInspectn.REMARKS;
                objInspectn.REMARKS = null;
                objInspectn.SECOND_TRANCHE = "2";
            }
            if (fc["ddl_tranche"].ConvertToString() == "3")
            {
                objInspectn.REMARKS_THREE = objInspectn.REMARKS;
                objInspectn.REMARKS = null;
                objInspectn.THIRD_TRANCHE = "3";
            }

            if(objInspectn.BENEFICIARY_MNAME=="")
            {
                objInspectn.FILLED_RECIEPENT = objInspectn.BENEFICIARY_FNAME + " " + objInspectn.BENEFICIARY_LNAME;
            }
            else
            {
                objInspectn.FILLED_RECIEPENT = objInspectn.BENEFICIARY_FNAME + " " + objInspectn.BENEFICIARY_MNAME + " " + objInspectn.BENEFICIARY_LNAME;
            }
            objInspectn.HOUSE_DESIGN_NO= fc["ddl_design"].ConvertToString();
             MiniInspectionServiceClass objServ = new MiniInspectionServiceClass();
            bool result = objServ.saveMiniInspection( objInspectn);
             return RedirectToAction("InspectionSearch");
         }

    }
}
