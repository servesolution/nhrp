using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using MIS.Services.Report;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using MIS.Services.Inspection;
using System.IO;
 
 
namespace MIS.Controllers.Inspection
{
    public class InspectionImportJsonController : BaseController
    {
        //
        // GET: /InspectionImportJson/

        public ActionResult SaveInspectionJson(string jsonCollection)
        {

            String paNumber ="";
            string FormNumber ="";

            InspectionImportJsonService objInspectionJsonService = new InspectionImportJsonService();
            using (StreamReader r = new StreamReader(Server.MapPath("~/Files/InspectionJson/") + "output.json"))
            {
                string json                     = r.ReadToEnd();
                dynamic array                   = JsonConvert.DeserializeObject(json);
                                 
                foreach (var item in array)
                {

                    List<SortedDictionary<dynamic, dynamic>> totalData = new List<SortedDictionary<dynamic, dynamic>>();
                    SortedDictionary<dynamic, dynamic> Total = new SortedDictionary<dynamic, dynamic>();

                     
                    SortedDictionary<dynamic, dynamic> PArecord             = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> ApplicationRecord    = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> InspectionRecord     = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> BeneficiaryReport    = new SortedDictionary<dynamic, dynamic>();

                    SortedDictionary<dynamic, dynamic> PartOne              = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> InspectionCommon     = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> AandBTypeRccHouseI1  = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> OwnDesign            = new SortedDictionary<dynamic, dynamic>();

                    SortedDictionary<dynamic, dynamic> AandBTypeRccHouseI2  = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> CTypeHouseI2         = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> CTypeRCCHouseI2      = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> AandBTypeRccHouseI3  = new SortedDictionary<dynamic, dynamic>();

                    SortedDictionary<dynamic, dynamic> CTypeHouseI3         = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> CTypeRCCHouseI3      = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> Photos               = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> InspectionProcess    = new SortedDictionary<dynamic, dynamic>();

                    SortedDictionary<dynamic, dynamic> CTypeHouseI1 = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> CTypeRCCHouseI1 = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> GPSData = new SortedDictionary<dynamic, dynamic>();
                    SortedDictionary<dynamic, dynamic> Notes = new SortedDictionary<dynamic, dynamic>();




                    List<SortedDictionary<dynamic, dynamic>> lstPArecord            = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstApplicationRecord   = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstInspectionRecord    = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstBeneficiaryReport   = new List<SortedDictionary<dynamic, dynamic>>();

                    List<SortedDictionary<dynamic, dynamic>> lstPartOne             = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstInspectionCommon    = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstAandBTypeRccHouseI1 = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstOwnDesign           = new List<SortedDictionary<dynamic, dynamic>>();

                    List<SortedDictionary<dynamic, dynamic>> lstAandBTypeRccHouseI2 = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstCTypeHouseI2        = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstCTypeRCCHouseI2     = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstAandBTypeRccHouseI3 = new List<SortedDictionary<dynamic, dynamic>>();

                    List<SortedDictionary<dynamic, dynamic>> lstCTypeHouseI3        = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstCTypeRCCHouseI3     = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstPhotos              = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstInspectionProcess   = new List<SortedDictionary<dynamic, dynamic>>();

                    List<SortedDictionary<dynamic, dynamic>> lstCTypeHouseI1 = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstCTypeRCCHouseI1 = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstGPSData             = new List<SortedDictionary<dynamic, dynamic>>();
                    List<SortedDictionary<dynamic, dynamic>> lstNotes = new List<SortedDictionary<dynamic, dynamic>>();




 

                    foreach (var pair in item)
                    {
                       if (pair.Name == "GRANT_AGREEMENT_NUMBER")
                        {
                           paNumber= pair.Value;
                       }
                        if (pair.Name == "FORM_NUMBER")
                        {
                           FormNumber= pair.Value;
                       }




                        if (pair.Name == "PA_Record")
                        {
                            if (!PArecord.ContainsKey(pair.Name))
                            {
                                PArecord.Add(pair.Name, pair.Value);
                            }
                            lstPArecord.Add(PArecord);
                        }
                        else
                        {
                            if (pair.Name == "APPLICATION_RECORD")
                            {
                                foreach (var pair1 in pair)
                                {
                                    foreach (var pair2 in pair1)
                                    {
                                        if (!ApplicationRecord.ContainsKey(pair2.Name))
                                        {
                                            ApplicationRecord.Add(pair2.Name, pair2.Value);
                                        }
                                        lstApplicationRecord.Add(ApplicationRecord);
                                    }  
                                }   
                            }

                             
                            if (pair.Name == "INSPECTION_RECORD")
                            {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair3 in pair1)
                                        {
                                            if (!InspectionRecord.ContainsKey(pair3.Name))
                                            {
                                                InspectionRecord.Add(pair3.Name, pair3.Value);
                                            }
                                            lstInspectionRecord.Add(InspectionRecord);
                                        } 
                                    } 
                              }



                             if (pair.Name == "BENEFICIARY_RECORD")
                             {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair4 in pair1)
                                        {
                                            if (!BeneficiaryReport.ContainsKey(pair4.Name))
                                            {
                                                BeneficiaryReport.Add(pair4.Name, pair4.Value);
                                            }
                                            lstBeneficiaryReport.Add(BeneficiaryReport);
                                        } 
                                    } 
                            }


                             // tech solution,org, soil and trained manson

                            if (pair.Name == "PART_1")
                             {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!PartOne.ContainsKey(pair5.Name))
                                            {
                                                PartOne.Add(pair5.Name, pair5.Value);
                                            }
                                            lstPartOne.Add(PartOne);
                                        } 
                                    } 
                              }


                            // Common part to all inspection except own design
                                if (pair.Name == "PART2_1")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!InspectionCommon.ContainsKey(pair5.Name))
                                            {
                                                InspectionCommon.Add(pair5.Name, pair5.Value);
                                            }
                                            lstInspectionCommon.Add(InspectionCommon);
                                        } 
                                    } 
                                }



                                // A and B  RCC type First Inspection 
                                if (pair.Name == "ANNEX_10.1")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!AandBTypeRccHouseI1.ContainsKey(pair5.Name))
                                            {
                                                AandBTypeRccHouseI1.Add(pair5.Name, pair5.Value);
                                            }
                                            lstAandBTypeRccHouseI1.Add(AandBTypeRccHouseI1);
                                        }
                                    }
                                }



                                // Own Design
                                if (pair.Name == "ANNEX_12")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!OwnDesign.ContainsKey(pair5.Name))
                                            {
                                                OwnDesign.Add(pair5.Name, pair5.Value);
                                            }
                                            lstOwnDesign.Add(OwnDesign);
                                        }
                                    }
                                }




                                // Rcc A and B Type Second
                                if (pair.Name == "ANNEX_13.1")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!AandBTypeRccHouseI2.ContainsKey(pair5.Name))
                                            {
                                                AandBTypeRccHouseI2.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeHouseI1.Add(AandBTypeRccHouseI2);
                                        }
                                    }
                                }




                                // C Type (smc,smm,bmc,bmm) First Inspection
                                if (pair.Name == "ANNEX_10.2AD")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!CTypeHouseI1.ContainsKey(pair5.Name))
                                            {
                                                CTypeHouseI1.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeHouseI1.Add(CTypeHouseI1);
                                        }
                                    }
                                }



                                // C Type (smc,smm,bmc,bmm) Second Inspection
                                if (pair.Name == "ANNEX_13.2AD")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!CTypeHouseI2.ContainsKey(pair5.Name))
                                            {
                                                CTypeHouseI2.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeHouseI2.Add(CTypeHouseI2);
                                        }
                                    }
                                }



                                // C Type RCC house First Inspection
                                if (pair.Name == "ANNEX_10.2E")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!CTypeRCCHouseI1.ContainsKey(pair5.Name))
                                            {
                                                CTypeRCCHouseI1.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeRCCHouseI1.Add(CTypeRCCHouseI1);
                                        }
                                    }
                                }



                                // C Type RCC house Second Inspection
                                if (pair.Name == "ANNEX_13.2E")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!CTypeRCCHouseI2.ContainsKey(pair5.Name))
                                            {
                                                CTypeRCCHouseI2.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeRCCHouseI2.Add(CTypeRCCHouseI2);
                                        }
                                    }
                                }



                            // A and B type RCC Third Inspection
                                if (pair.Name == "ANNEX_15.1")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!AandBTypeRccHouseI3.ContainsKey(pair5.Name))
                                            {
                                                AandBTypeRccHouseI3.Add(pair5.Name, pair5.Value);
                                            }
                                            lstAandBTypeRccHouseI3.Add(AandBTypeRccHouseI3);
                                        }
                                    }
                                }



                                // C type (smm,bmm,bmc,bmm) Third Inspection
                                if (pair.Name == "ANNEX_15.2AD.1")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!CTypeHouseI3.ContainsKey(pair5.Name))
                                            {
                                                CTypeHouseI3.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeHouseI3.Add(CTypeHouseI3);
                                        }
                                    }
                                }



                                // C type RCC Third Inspection
                                if (pair.Name == "ANNEX_15.2E")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!CTypeRCCHouseI3.ContainsKey(pair5.Name))
                                            {
                                                CTypeRCCHouseI3.Add(pair5.Name, pair5.Value);
                                            }
                                            lstCTypeRCCHouseI3.Add(CTypeRCCHouseI3);
                                        }
                                    }
                                }




                                // Photos
                                if (pair.Name == "PHOTOS")
                                {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!Photos.ContainsKey(pair5.Name))
                                            {
                                                Photos.Add(pair5.Name, pair5.Value);
                                            }
                                            lstPhotos.Add(Photos);
                                        }
                                    }
                                }





                            //inspection processign 
                            if (pair.Name == "FINAL_APPROVAL")
                            {
                                    foreach (var pair1 in pair)
                                    {
                                        foreach (var pair5 in pair1)
                                        {
                                            if (!InspectionProcess.ContainsKey(pair5.Name))
                                            {
                                                InspectionProcess.Add(pair5.Name, pair5.Value);
                                            }
                                            lstInspectionProcess.Add(InspectionProcess);
                                        }
                                    }
                            }




                            //GPS Information
                            if (pair.Name == "GPS_DATA")
                            {
                                foreach (var pair1 in pair)
                                {
                                    foreach (var pair5 in pair1)
                                    {
                                        if (!GPSData.ContainsKey(pair5.Name))
                                        {
                                            GPSData.Add(pair5.Name, pair5.Value);
                                        }
                                        lstGPSData.Add(GPSData);
                                    }
                                }
                            }

                            //Notes Information
                            if (pair.Name == "NOTES")
                            {
                                foreach (var pair1 in pair)
                                {
                                    foreach (var pair5 in pair1)
                                    {
                                        if (!Notes.ContainsKey(pair5.Name))
                                        {
                                            Notes.Add(pair5.Name, pair5.Value);
                                        }
                                        lstNotes.Add(Notes);
                                    }
                                }
                            }

                             

                        }
                          
                    }
                    Total.Add("GPSData", lstGPSData);
                    Total.Add("InspectionProcess", lstInspectionProcess);
                    Total.Add("Photos", lstPhotos);
                    Total.Add("CTypeRCCHouseI3", lstCTypeRCCHouseI3);

                    Total.Add("CTypeHouseI3", lstCTypeHouseI3);
                    Total.Add("AandBTypeRccHouseI3", lstAandBTypeRccHouseI3);
                    Total.Add("CTypeRCCHouseI2", lstCTypeRCCHouseI2);
                    Total.Add("CTypeHouseI2", lstCTypeHouseI2);

                    Total.Add("AandBTypeRccHouseI2", lstAandBTypeRccHouseI2);
                    Total.Add("OwnDesign", lstOwnDesign); 
                    Total.Add("AandBTypeRccHouseI1", lstAandBTypeRccHouseI1);
                    Total.Add("InspectionCommon", lstInspectionCommon);

                    Total.Add("PartOne", lstPartOne); 
                    Total.Add("BeneficiaryReport", lstBeneficiaryReport);
                    Total.Add("InspectionRecord", lstInspectionRecord);
                    Total.Add("ApplicationRecord", lstApplicationRecord);

                    Total.Add("PArecord", lstPArecord);

                    Total.Add("Notes", lstNotes);
                    Total.Add("CTypeHouseI1", lstCTypeHouseI1);
                    Total.Add("CTypeRCCHouseI1", lstCTypeRCCHouseI1);

                    Total.Add("PA_NUMBER",paNumber);
                    Total.Add("FORM_NUMBER",FormNumber);

                    totalData.Add(Total);

                    objInspectionJsonService.SaveInspectionData(totalData,"output.json");
                }

                
            }
            return View();
        }

    }
}
