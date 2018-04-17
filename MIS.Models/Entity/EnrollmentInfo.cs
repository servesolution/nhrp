using System;
using EntityFramework;

[Table(Name = "NHRS_ENROLLMENT_MOU")]
    public class enrollmentInfoclass:EntityBase
    {
        private string MODE =null;
        private Decimal? targetingind =null;
        private Decimal? targetbatchid =null;
        private Decimal? enrollmentid =null;
        private string mouid =null;
       private string enrollmentmoudt =null;
       private string enrollmentmoudtloc =null;
       private string houseownerid =null;
       private string homemberid =null;
       private string buildingstructureno =null;
       private string householdid =null;
       private string householddefinedcd =null;
       private string hhmemberid =null;
       private string firstnameeng =null;
       private string middlenameeng =null;
       private string lastnameeng =null;
       private string fullnameeng =null;
       private string firstnameloc =null;
       private string middlenameloc =null;
       private string lastnameloc =null;
       private string fullnameloc =null;
       private string memberphotopath =null;
       private string identificationtype =null;
       private string identificationno =null;
       private string identificationdocument =null;
       private string identificationissuedistrict =null;
       private Decimal? countrycd =null;
       private Decimal? regionstatecd = null;
       private Decimal? zonecd = null;
       private Decimal? districtcd = null;
       private string districteng =null;
       private string vdcmuncd =null;
       private string vdcmuneng =null;
       private Decimal? wardno = null;
       private string enumerationarea =null;
       private string areaeng =null;
       private string arealoc =null;
       private Decimal? bankcd = null;
       private Decimal?  bankbranchcd =null;
       private string bankaccno =null;
       private string bankacctypecd =null;
       private string ispaymentreceiverchanged =null;
       private string changedreasoneng =null;
       private string changedreasonloc =null;
       private string beneficiarymemberid =null;
       private string beneficiaryfnameeng =null;
       private string beneficiarymnameeng =null;
       private string beneficiarylnameeng =null;
       private string beneficiaryfullnameeng =null;
       private string beneficiaryfnameloc =null;
       private string beneficiarymnameloc =null;
       private string beneficiarylnameloc =null;
       private string beneficiaryfullnameloc =null;
       private  Decimal? beneficiaryrelationtypecd =null;
       private string proxymemberid =null;
       private string proxyfnameeng =null;
       private string proxymnameeng =null;
       private string proxylnameeng =null;
       private string proxyfullnameeng =null;
       private string proxyfnameloc=null;
       private string proxymnameloc=null;
       private string proxylnameloc =null;
       private string proxyfullnameloc =null;
       private  Decimal? proxyrelationtypecd =null;
       private string employeecd =null;
       private string approved =null;
       private string approvedby =null;
       private string approveddt =null;
       private string approveddtloc =null;
       private string updatedby =null;
       private string updateddt =null;
       private string updateddtloc =null;
       private string enteredby=null;
       private string entereddt =null;
       private string entereddtloc =null;
       private string ipaddress =null;

       [Column(Name = "TARGETING_ID", IsKey = true, SequenceName = "")]
       public Decimal? TARGETING_ID
       {
           get { return targetingind; }
           set { targetingind = value; }
       }

      [Column(Name = "TARGET_BATCH_ID", IsKey = true, SequenceName = "")]
	  public Decimal? TARGET_BATCH_ID
	  {
		get{return targetbatchid;}
          set { targetbatchid = value; }
	  }
       [Column(Name = "ENROLLMENT_ID", IsKey = true, SequenceName = "")]
      public Decimal? ENROLLMENT_ID
	  {
		get{return enrollmentid;}
          set { enrollmentid = value; }
	  }
       [Column(Name = "MOU_ID ", IsKey = true, SequenceName = "")]
       public String MOU_ID 
       {
           get { return mouid; }
           set { mouid = value; }
       }
       [Column(Name = "ENROLLMENT_MOU_DT", IsKey = true, SequenceName = "")]
       public String ENROLLMENT_MOU_DT
       {
           get { return enrollmentmoudt; }
           set { enrollmentmoudt = value; }
       }
       [Column(Name = "ENROLLMENT_MOU_DT_LOC", IsKey = true, SequenceName = "")]
       public String ENROLLMENT_MOU_DT_LOC
       {
           get { return enrollmentmoudtloc; }
           set { enrollmentmoudtloc = value; }
       }
       [Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
       public String HOUSE_OWNER_ID
       {
           get { return houseownerid; }
           set { houseownerid = value; }
       }
       [Column(Name = "HO_MEMBER_ID", IsKey = true, SequenceName = "")]
       public String HO_MEMBER_ID
       {
           get { return homemberid; }
           set { homemberid = value; }
       }
       [Column(Name = "BUILDING_STRUCTURE_NO", IsKey = true, SequenceName = "")]
       public String BUILDING_STRUCTURE_NO
       {
           get { return buildingstructureno; }
           set { buildingstructureno = value; }
       }
       [Column(Name = "HOUSEHOLD_ID", IsKey = true, SequenceName = "")]
       public String HOUSEHOLD_ID
       {
           get { return householdid; }
           set { householdid = value; }
       }
       [Column(Name = "HOUSEHOLD_DEFINED_CD", IsKey = true, SequenceName = "")]
       public String HOUSEHOLD_DEFINED_CD
       {
           get { return householddefinedcd; }
           set { householddefinedcd = value; }
       }
       [Column(Name = "FIRST_NAME_ENG", IsKey = true, SequenceName = "")]
       public String FIRST_NAME_ENG
       {
           get { return firstnameeng; }
           set { firstnameeng = value; }
       }
       [Column(Name = "MIDDLE_NAME_ENG", IsKey = true, SequenceName = "")]
       public String MIDDLE_NAME_ENG
       {
           get { return middlenameeng; }
           set { middlenameeng = value; }
       }
       [Column(Name = "LAST_NAME_ENG", IsKey = true, SequenceName = "")]
       public String LAST_NAME_ENG
       {
           get { return lastnameeng; }
           set { lastnameeng = value; }
       }
       [Column(Name = "FULL_NAME_ENG", IsKey = true, SequenceName = "")]
       public String FULL_NAME_ENG
       {
           get { return fullnameeng; }
           set { fullnameeng = value; }
       }
       [Column(Name = "FIRST_NAME_LOC", IsKey = true, SequenceName = "")]
       public String FIRST_NAME_LOC
       {
           get { return firstnameloc; }
           set { firstnameloc = value; }
       }
       [Column(Name = "MIDDLE_NAME_LOC", IsKey = true, SequenceName = "")]
       public String MIDDLE_NAME_LOC
       {
           get { return middlenameloc; }
           set { middlenameloc = value; }
       }
       [Column(Name = "LAST_NAME_LOC", IsKey = true, SequenceName = "")]
       public String LAST_NAME_LOC
       {
           get { return lastnameloc; }
           set { lastnameloc = value; }
       }
       [Column(Name = "FULL_NAME_LOC", IsKey = true, SequenceName = "")]
       public String FULL_NAME_LOC
       {
           get { return fullnameloc; }
           set { fullnameloc = value; }
       }
       [Column(Name = "MEMBER_PHOTO_PATH", IsKey = true, SequenceName = "")]
       public String MEMBER_PHOTO_PATH
       {
           get { return memberphotopath; }
           set { memberphotopath = value; }
       }
       [Column(Name = "IDENTIFICATION_TYPE_CD", IsKey = true, SequenceName = "")]
       public String IDENTIFICATION_TYPE_CD
       {
           get { return identificationtype; }
           set { identificationtype = value; }
         }


      [Column(Name = "IDENTIFICATION_NO", IsKey = true, SequenceName = "")]
       public String IDENTIFICATION_NO
       {
           get { return identificationno; }
           set { identificationno = value; }
       }
   
      [Column(Name = "IDENTIFICATION_DOCUMENT", IsKey = true, SequenceName = "")]
      public String IDENTIFICATION_DOCUMENT
      {
          get { return identificationdocument; }
          set { identificationdocument = value; }
      }

      [Column(Name = "IDENTIFICATION_ISSUE_DIS_CD ", IsKey = true, SequenceName = "")]
      public String IDENTIFICATION_ISSUE_DIS_CD
      {
          get { return identificationissuedistrict; }
          set { identificationissuedistrict = value; }
      }

      [Column(Name = "COUNTRY_CD", IsKey = true, SequenceName = "")]
      public Decimal? COUNTRY_CD 
      {
          get { return countrycd; }
          set { countrycd = value; }
      }

      [Column(Name = "REGION_STATE_CD ", IsKey = true, SequenceName = "")]
      public Decimal? REGION_STATE_CD 
      {
          get { return regionstatecd; }
          set { regionstatecd = value; }
      }

      [Column(Name = "ZONE_CD ", IsKey = true, SequenceName = "")]
      public Decimal? ZONE_CD 
      {
          get { return zonecd; }
          set { zonecd = value; }
      }

      [Column(Name = "DISTRICT_CD", IsKey = true, SequenceName = "")]
      public Decimal? DISTRICT_CD
      {
          get { return districtcd; }
          set { districtcd = value; }
      }

      [Column(Name = "VDC_MUN_CD", IsKey = true, SequenceName = "")]
      public String VDC_MUN_CD
      {
          get { return vdcmuncd; }
          set { vdcmuncd = value; }
      }

      [Column(Name = "WARD_NO", IsKey = true, SequenceName = "")]
      public Decimal? WARD_NO
      {
          get { return wardno; }
          set { wardno = value; }
      }
      [Column(Name = "ENUMERATION_AREA", IsKey = true, SequenceName = "")]
      public String ENUMERATION_AREA
      {
          get { return enumerationarea; }
          set { enumerationarea = value; }
      }
      [Column(Name = "AREA_ENG", IsKey = true, SequenceName = "")]
      public String AREA_ENG
      {
          get { return areaeng; }
          set { areaeng = value; }
      }

      [Column(Name = "AREA_LOC", IsKey = true, SequenceName = "")]
      public String AREA_LOC
      {
          get { return arealoc; }
          set { arealoc = value; }
      }

      [Column(Name = "BANK_CD", IsKey = true, SequenceName = "")]
      public Decimal? BANK_CD
      {
          get { return bankcd; }
          set { bankcd = value; }
      }
      [Column(Name = "BANK_BRANCH_CD", IsKey = true, SequenceName = "")]
      public Decimal? BANK_BRANCH_CD
      {
          get { return bankbranchcd; }
          set { bankbranchcd = value; }
      }
      [Column(Name = "BANK_ACC_NO", IsKey = true, SequenceName = "")]
      public String BANK_ACC_NO
      {
          get { return bankaccno; }
          set { bankaccno = value; }
      }

      [Column(Name = "BANK_ACC_TYPE_CD", IsKey = true, SequenceName = "")]
      public String BANK_ACC_TYPE_CD
      {
          get { return bankacctypecd; }
          set { bankacctypecd = value; }
      }
      [Column(Name = "IS_PAYMENT_RECEIVER_CHANGED ", IsKey = true, SequenceName = "")]
      public String IS_PAYMENT_RECEIVER_CHANGED
      {
          get { return ispaymentreceiverchanged; }
          set { ispaymentreceiverchanged = value; }
      }
      [Column(Name = "CHANGED_REASON_ENG ", IsKey = true, SequenceName = "")]
      public String CHANGED_REASON_ENG 
      {
          get { return changedreasoneng; }
          set { changedreasoneng = value; }
      }
      [Column(Name = "CHANGED_REASON_LOC", IsKey = true, SequenceName = "")]
      public String CHANGED_REASON_LOC
      {
          get { return changedreasonloc; }
          set { changedreasonloc = value; }
      }
      [Column(Name = "BENEFICIARY_MEMBER_ID", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_MEMBER_ID
      {
          get { return beneficiarymemberid; }
          set { beneficiarymemberid = value; }
      }
      [Column(Name = "BENEFICIARY_FNAME_ENG", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_FNAME_ENG
      {
          get { return beneficiaryfnameeng; }
          set { beneficiaryfnameeng = value; }
      }
      [Column(Name = "BENEFICIARY_MNAME_ENG ", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_MNAME_ENG 
      {
          get { return beneficiarymnameeng; }
          set { beneficiarymnameeng = value; }
      }
      [Column(Name = "BENEFICIARY_LNAME_ENG", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_LNAME_ENG
      {
          get { return beneficiarylnameeng; }
          set { beneficiarylnameeng = value; }
      }
      [Column(Name = "BENEFICIARY_FULLNAME_ENG", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_FULLNAME_ENG
      {
          get { return beneficiaryfullnameeng; }
          set { beneficiaryfullnameeng = value; }
      }
      [Column(Name = "BENEFICIARY_FNAME_LOC", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_FNAME_LOC
      {
          get { return beneficiaryfnameloc; }
          set { beneficiaryfnameloc = value; }
      }
      [Column(Name = "BENEFICIARY_MNAME_LOC", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_MNAME_LOC
      {
          get { return beneficiarymnameloc; }
          set { beneficiarymnameloc = value; }
      }
      [Column(Name = "BENEFICIARY_LNAME_LOC", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_LNAME_LOC
      {
          get { return beneficiarylnameloc; }
          set { beneficiarylnameloc = value; }
      }
      [Column(Name = "BENEFICIARY_FULLNAME_LOC", IsKey = true, SequenceName = "")]
      public String BENEFICIARY_FULLNAME_LOC
      {
          get { return beneficiaryfullnameloc; }
          set { beneficiaryfullnameloc = value; }
      }
      [Column(Name = "BENEFICIARY_RELATION_TYPE_CD ", IsKey = true, SequenceName = "")]
      public Decimal? BENEFICIARY_RELATION_TYPE_CD
      {
          get { return beneficiaryrelationtypecd; }
          set { beneficiaryrelationtypecd = value; }
      }

      [Column(Name = "PROXY_MEMBER_ID", IsKey = true, SequenceName = "")]
      public String PROXY_MEMBER_ID
      {
          get { return proxymemberid; }
          set { proxymemberid = value; }
      }
      [Column(Name = "PROXY_FNAME_ENG", IsKey = true, SequenceName = "")]
      public String PROXY_FNAME_ENG
      {
          get { return proxyfnameeng; }
          set { proxyfnameeng = value; }
      }
      [Column(Name = "PROXY_MNAME_ENG ", IsKey = true, SequenceName = "")]
      public String PROXY_MNAME_ENG
      {
          get { return proxymnameeng; }
          set { proxymnameeng = value; }
      }
      [Column(Name = "PROXY_LNAME_ENG", IsKey = true, SequenceName = "")]
      public String PROXY_LNAME_ENG
      {
          get { return proxylnameeng; }
          set { proxylnameeng = value; }
      }
      [Column(Name = "PROXY_FULLNAME_ENG", IsKey = true, SequenceName = "")]
      public String PROXY_FULLNAME_ENG
      {
          get { return proxyfullnameeng; }
          set { proxyfullnameeng = value; }
      }
      [Column(Name = "PROXY_FNAME_LOC", IsKey = true, SequenceName = "")]
      public String PROXY_FNAME_LOC
      {
          get { return proxyfnameloc; }
          set { proxyfnameloc = value; }
      }
      [Column(Name = "PROXY_MNAME_LOC", IsKey = true, SequenceName = "")]
      public String PROXY_MNAME_LOC
      {
          get { return proxymnameloc; }
          set { proxymnameloc = value; }
      }
      [Column(Name = "PROXY_LNAME_LOC", IsKey = true, SequenceName = "")]
      public String PROXY_LNAME_LOC
      {
          get { return proxylnameloc; }
          set { proxylnameloc = value; }
      }
      [Column(Name = "PROXY_FULLNAME_LOC", IsKey = true, SequenceName = "")]
      public String PROXY_FULLNAME_LOC
      {
          get { return proxyfullnameloc; }
          set { proxyfullnameloc = value; }
      }
      [Column(Name = "PROXY_RELATION_TYPE_CD ", IsKey = true, SequenceName = "")]
      public Decimal? PROXY_RELATION_TYPE_CD
      {
          get { return proxyrelationtypecd; }
          set { proxyrelationtypecd = value; }
      }
      [Column(Name = "EMPLOYEE_CD", IsKey = true, SequenceName = "")]
      public String EMPLOYEE_CD
      {
          get { return employeecd; }
          set { employeecd = value; }
      }
      [Column(Name = "APPROVED", IsKey = true, SequenceName = "")]
      public String APPROVED
      {
          get { return approved; }
          set { approved = value; }
      }
      [Column(Name = "APPROVED_BY", IsKey = true, SequenceName = "")]
      public String APPROVED_BY
      {
          get { return approvedby; }
          set { approvedby = value; }
      }
      [Column(Name = "APPROVED_DT", IsKey = true, SequenceName = "")]
      public String APPROVED_DT
      {
          get { return approveddt; }
          set { approveddt = value; }
      }
      [Column(Name = "APPROVED_DT_LOC", IsKey = true, SequenceName = "")]
      public String APPROVED_DT_LOC
      {
          get { return approveddtloc; }
          set { approveddtloc = value; }
      }
      [Column(Name = "UPDATED_BY", IsKey = true, SequenceName = "")]
      public String UPDATED_BY
      {
          get { return updatedby; }
          set { updatedby = value; }
      }
      [Column(Name = "UPDATED_DT", IsKey = true, SequenceName = "")]
      public String UPDATED_DT
      {
          get { return updateddt; }
          set { updateddt = value; }
      }
      [Column(Name = "UPDATED_DT_LOC", IsKey = true, SequenceName = "")]
      public String UPDATED_DT_LOC
      {
          get { return updateddtloc; }
          set { updateddtloc = value; }
      }
      [Column(Name = "ENTERED_BY", IsKey = true, SequenceName = "")]
      public String ENTERED_BY
      {
          get { return enteredby; }
          set { enteredby = value; }
      }
      [Column(Name = "ENTERED_DT", IsKey = true, SequenceName = "")]
      public String ENTERED_DT
      {
          get { return entereddt; }
          set { entereddt = value; }
      }
      [Column(Name = "ENTERED_DT_LOC", IsKey = true, SequenceName = "")]
      public String ENTERED_DT_LOC
      {
          get { return entereddtloc; }
          set { entereddtloc = value; }
      }
      [Column(Name = "ip_address", IsKey = true, SequenceName = "")]
      public String ip_address 
      {
          get { return ipaddress; }
          set { ipaddress = value; }
      }
    }

