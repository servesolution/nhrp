using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Enrollment.EnrollmentPopup
{
   public class benificiary
    {
        public String HOUSE_OWNER_ID { get; set; }
        public Decimal? BENEFICIARY_TYPE_CD { get; set; }
        public String BENEFICIARY_TYPE_CD_ENG { get; set; }
        public String BENEFICIARY_TYPE_CD_LOC { get; set; }
        public String BENEFICIARY_FNAME_ENG { get; set; }
        public String BENEFICIARY_MNAME_ENG { get; set; }
        public String BENEFICIARY_LNAME_ENG { get; set; }
        public String BENEFICIARY_FULLNAME_ENG { get; set; }
        public String BENEFICIARY_FNAME_LOC { get; set; }
        public String BENEFICIARY_MNAME_LOC { get; set; }
        public String BENEFICIARY_LNAME_LOC { get; set; }
        public String BENEFICIARY_FULLNAME_LOC { get; set; }
        public String BENEFICIARY_RELATION_TYPE_CD{ get; set; }
        public String BENEFICIARY_RELATION_TYPE_ENG { get; set; }
        public String BENEFICIARY_RELATION_TYPE_LOC { get; set; }
        public String FATHER_FULLNAME_ENG { get; set; }
        public String FATHER_FULLNAME_LOC { get; set; }
        public String FATHER_RELATION_TYPE_CD { get; set; }
        public String GFATHER_FULLNAME_ENG { get; set; }
        public String GFATHER_FULLNAME_LOC { get; set; }
        public String BUILDING_AREA_ENG { get; set; }
        public String BUILDING_AREA_LOC { get; set; }
        public String BUILDING_AREA { get; set; }
        public Decimal? ENUMERATOR_ID { get; set; }
        public Decimal? IDENTIFICATION_TYPE_CD { get; set; }
        public String IDENTIFICATION_TYPE_CD_ENG { get; set; }
        public String IDENTIFICATION_TYPE_CD_LOC { get; set; }
        public String IDENTIFICATION_NO { get; set; }
        public String IDENTIFICATION_DOCUMENT { get; set; }
        public String IDENTIFICATION_ISSUE_DIS_CD { get; set; }
        public String IDENTIFICATION_ISSUE_DIS_ENG { get; set; }
        public String IDENTIFICATION_ISSUE_DIS_LOC { get; set; }
        public String IDENTIFICATION_ISSUE_DAY { get; set; }
        public String IDENTIFICATION_ISSUE_MONTH { get; set; }

        public String IDENTIFICATION_ISSUE_YEAR { get; set; }
        public String IDENTIFICATION_ISSUE_DT { get; set; }
        public String IDENTIFICATION_ISS_DAY_LOC { get; set; }
        public String IDENTIFICATION_ISS_MNTH_LOC { get; set; }
        public String IDENTIFICATION_ISS_YEAR_LOC { get; set; }
        public String IDENTIFICATION_ISS_DT_LOC { get; set; }
        public String NOMINEE_FULLNAME_ENG { get; set; }
        public String NOMINEE_FULLNAME_LOC { get; set; }
        public String NOMINEE_RELATION_TYPE_CD { get; set; }

        public List<HouseOwner> houseownerList = new List<HouseOwner>();

        public List<benificiary> beneficiaryList = new List<benificiary>();

        public List<proxy> proxyList = new List<proxy>();

        public List<nominee> nomineeList = new List<nominee>();

        public List<enrollmentclass> enrollmentList = new List<enrollmentclass>();

        public List<bank> bankList = new List<bank>();

        public List<employee> employeeList = new List<employee>();

    }
}
