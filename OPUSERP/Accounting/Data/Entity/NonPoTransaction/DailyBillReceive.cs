using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
//using OPUSERP.HRPMS.Data.Entity.Employee;
//using OPUSERP.HRPMS.Data.Entity.Master;
//using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Data.Entity.NonPoTransaction
{
    [Table("DailyBillReceive", Schema = "ACCOUNT")]
    public class DailyBillReceive:Base
    {
        public string SLNo { get; set; }
        public DateTime? BillReceiveDate { get; set; }
        public string ProcessNo { get; set; }
        public int? supplierId { get; set; }
        public SCM.Data.Entity.Supplier.Organization supplier { get; set; }
        public string InvoiceNo { get; set; }
        public int? IsPo { get; set; }
        public int? IsClaimed { get; set; }
        public int? isClaimable { get; set; }
        public int? ischallan { get; set; }
        public decimal? GBamount { get; set; }
        public DateTime? ChalanDate { get; set; }
        public string vatNumber { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Other { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PO { get; set; }
       
        public string Challan { get; set; }
        public string PoNo { get; set; }
        public string PoDate { get; set; }
        public int? BillStatusId { get; set; }//0 BILL RECEIVE 1 bILL PAYMENT
        public int? subid { get; set; }
        public int? AccountID { get; set; }
        public string VoucherNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime? ApvDate { get; set; }
        public decimal? POAMOUNT { get; set; }


        public DateTime? SentToDepDate { get; set; }
        public DateTime? ReceiveDepDate { get; set; }
        public int? ResposneemployeeInfoId { get; set; }
        public EmployeeInfo ResposneemployeeInfo { get; set; }
        public DateTime? CheckReceiveDate { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? VendorPayReceiveDate { get; set; }
        public DateTime? BillReturnDate { get; set; }
        public string ReturnComments { get; set; }
        public string ReturnProcessNo { get; set; }
        public decimal? VoucherAmount { get; set; }
        //public int? DepartmentId { get; set; }
        //public Department Department { get; set; }
        public string DivisionName { get; set; }
        public string ReqDept { get; set; }
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; } 
        public DateTime? VoucherDate { get; set; }
      

   

        public string paidStatus { get; set; }
        public int? CreditPeriod { get; set; }
        public string ExpectedFindate { get; set; }
        public string UserDeptCode { get; set; }

        public int? ItemId { get; set; }
        public Item Item { get; set; }

        public decimal? VATPayable { get; set; }
        public decimal? TAXPayable { get; set; }
        public decimal? VATCurrent { get; set; }
        public string vendorstatus { get; set; }
        public int? NonDepartmentId { get; set; }
        public int? NoVat { get; set; }
        public int? NoReturn { get; set; }
        public Department NonDepartment { get; set; }

        public int? voucherMasterId { get; set; }
        public VoucherMaster voucherMaster { get; set; }
    }
}
