using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
 public class BankPayrollDetail
    {
     public string Lang { get; set; }
           public string  Fiscalyr { get; set; }
           public string  transaction_id { get; set; }
           public string  transaction_dt { get; set; }
           public string  bank_cd { get; set; }
           public string  bank_branch_cd { get; set; }
           public string  account_type_cd { get; set; }
           public string nra_defined_cd { get; set; }
           public string house_owner_id { get; set; }
           public string house_hold_id { get; set; }
           public string ac_actiivated_dt { get; set; }
           public string ac_actiivated_dt_loc { get; set; }
           public string cr_amount { get; set; }
           public string deposited_dt { get; set; }
           public string dr_amount { get; set; }
           public string withdraw_dt { get; set; }
           public string tranch { get; set; }
           public string entered_by { get; set; }
           public string entered_dt { get; set; }
           public string  updated_by { get; set; }
           public string updated_dt { get; set; }
           public string remarks { get; set; }
           public string remarks_loc { get; set; }
           public string file_batch_id { get; set; }
           public string reporting_dt { get; set; }
           public string reporting_dt_loc { get; set; }
           public string account_number { get; set; }
           public string withdraw_dt_loc { get; set; }
           public string transaction_dt_loc { get; set; }
           public string deposite_dt_loc { get; set; }
           public string payroll_install_cd { get; set; }
           public string batch_lot { get; set; }
           public string payroll_dtl_id { get; set; }
           public string dis_cd { get; set; }
           public string vdc_mun_cd { get; set; }
           public string ward_num { get; set; }
           public string  bank_branch_name { get; set; }
           public string  bank_name { get; set; }
           public string  is_atm_card_issue { get; set; }
           public string  atm_card_issued_dt { get; set; }
           public string bank_claim { get; set; }
           public string bank_claim_dt { get; set; }
           public string bank_claim_dt_loc {  get; set; }
           public string Quarter { get; set; }
           public string isuploaded { get; set; }
           public string Approve { get; set; }
           public string bank_payroll_id { get; set; }
           public string APPROVED_BY { get; set; }
           public int BANK_NEW_PAYROLL_ID { get; set; }

           public string APPROVED_DT { get; set; }
           public string SecondTrancheApproved { get; set; }
           public string ThirdTrancheApproved { get; set; }
           public string MODE { get; set; }
           public string Status { get; set; }
           public String QUARTER
           {
               get { return Quarter; }
               set { Quarter = value; }
           }
           public String FISCAL_YR
           {
               get { return Fiscalyr; }
               set { Fiscalyr = value; }
           }
           public String Is_Uploaded
           {
               get { return isuploaded; }
               set { isuploaded = value; }
           }

           public String TRANSACTON_ID
           {
            get { return transaction_id; }
            set { transaction_id = value; }
           }
           public String TRANSACTION_DT
        {
            get { return transaction_dt; }
            set { transaction_dt = value; }
        }
           public String BANK_CD
        {
            get { return bank_cd; }
            set { bank_cd = value; }
        }
           public String BANK_BRANCH_CD
        {
            get { return bank_branch_cd; }
            set { bank_branch_cd = value; }
        }
           public String ACCOUNT_TYPE_CD
        {
            get { return account_type_cd; }
            set { account_type_cd = value; }
        }
           public String NRA_DEFINED_CD
        {
            get { return nra_defined_cd; }
            set { nra_defined_cd = value; }
        }
           public String HOUSE_OWNER_ID
        {
            get { return house_owner_id; }
            set { house_owner_id = value; }
        }
           public String HOUSE_HOLD_ID
        {
            get { return house_hold_id; }
            set { house_hold_id = value; }
        }
           public String AC_ACTIVATED_DT
        {
            get { return ac_actiivated_dt; }
            set { ac_actiivated_dt = value; }
        }
           public String AC_ACTIVATED_DT_LOC
        {
            get { return ac_actiivated_dt_loc; }
            set { ac_actiivated_dt_loc = value; }
        }
           public String CR_AMOUNT
        {
            get { return cr_amount; }
            set { cr_amount = value; }
        }
           public String DEPOSITED_DT
        {
            get { return deposited_dt; }
            set { deposited_dt = value; }
        }
           public String DR_AMOUNT
        {
            get { return dr_amount; }
            set { dr_amount = value; }
        }
           public String WITHDRAW_DT
        {
            get { return dr_amount; }
            set { dr_amount = value; }
        }
           public String TRANCH
        {
            get { return tranch; }
            set { tranch = value; }
        }
           public String ENTERED_BY
        {
            get { return entered_by; }
            set { entered_by = value; }
        }
           public String ENTERED_DT
        {
            get { return entered_dt; }
            set { entered_dt = value; }
        }
           public String UPDATED_BY
        {
            get { return updated_by; }
            set { updated_by = value; }
        }
           public String UPDATED_DT
        {
            get { return updated_dt; }
            set { updated_dt = value; }
        }
           public String REMARKS
        {
            get { return remarks; }
            set { remarks = value; }
        }
           public String REMARKS_LOC
        {
            get { return remarks_loc; }
            set { remarks_loc = value; }
        }
           public String FILE_BATCH_ID
        {
            get { return file_batch_id; }
            set { file_batch_id = value; }
        }

           public String REPORTING_DT
        {
            get { return reporting_dt; }
            set { reporting_dt = value; }
        }
           public String REPORTING_DATE_LOC
        {
            get { return reporting_dt_loc; }
            set { reporting_dt_loc = value; }
        }

           public String ACCOUNT_NUMBER
        {
            get { return account_number; }
            set { account_number = value; }
        }
           public String WITHDRAW_DT_LOC
        {
            get { return withdraw_dt_loc; }
            set { withdraw_dt_loc = value; }
        }
        //public String Payment_rec_mem_Id
        //{
        //    get { return PaymentrecmemId; }
        //    set { PaymentrecmemId = value; }
        //}
        public String TRANSACTION_DATE_LOC
        {
            get { return transaction_dt_loc; }
            set { transaction_dt_loc = value; }
        }
        public String DEPOSITED_DT_LOC
        {
            get { return deposite_dt_loc; }
            set { deposite_dt_loc = value; }
        }
        public String PAYROLL_INSTALL_CD
        {
            get { return payroll_install_cd; }
            set { payroll_install_cd = value; }
        }
        public String BATCH_LOT
        {
            get { return batch_lot; }
            set { batch_lot = value; }
        }
        public String PAYROLL_DTL_ID
        {
            get { return payroll_dtl_id; }
            set { payroll_dtl_id = value; }
        }
        public String DISTRICT_CD
        {
            get { return dis_cd; }
            set { dis_cd = value; }
        }
        public String VDC_MUN_CD
        {
            get { return vdc_mun_cd; }
            set { vdc_mun_cd = value; }
        }
        public String WARD_NO
        {
            get { return ward_num; }
            set { ward_num = value; }
        }
        public String BANK_BRANCH_NAME
        {
            get { return bank_branch_name; }
            set { bank_branch_name = value; }
        }
        public String BANK_NAME
        {
            get { return bank_name; }
            set { bank_name = value; }
        }
        public String IS_ATM_CARD_ISSUED
        {
            get { return is_atm_card_issue; }
            set { is_atm_card_issue = value; }
        }
        public string ATM_CARD_ISSUED_DATE
        {
            get { return atm_card_issued_dt; }
            set { atm_card_issued_dt = value; }
        }
        public string BANK_CLAIM
        {
            get { return bank_claim; }
            set { bank_claim = value; }
        }
        public string BANK_CLAIM_DT
        {
            get { return bank_claim_dt; }
            set { bank_claim_dt = value; }
        }
        public string BANK_CLAIM_DT_LOC
        {
            get { return bank_claim_dt_loc; }
            set { bank_claim_dt_loc = value; }
        }

        public string IsUploaded { get; set; }
    }
}
