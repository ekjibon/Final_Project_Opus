//using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class DailyBillReceiveViewModel
    {
        public int? billReceiveId { get; set; }
        public string SLNo { get; set; }
        public DateTime? BillReceiveDate { get; set; }
        public string ProcessNo { get; set; }
        public int? supplierId { get; set; }

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
        public int? BillStatusId { get; set; }
        public int? subid { get; set; }
        public int? AccountID { get; set; }
        public string VoucherNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime? ApvDate { get; set; }
        public decimal? POAMOUNT { get; set; }


        public DateTime? SentToDepDate { get; set; }
        public DateTime? ReceiveDepDate { get; set; }
        public int? ResposneemployeeInfoId { get; set; }
        public int? employeeInfoId { get; set; }
        public DateTime? CheckReceiveDate { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? VendorPayReceiveDate { get; set; }
        public DateTime? BillReturnDate { get; set; }
        public string ReturnComments { get; set; }
        public string ReturnProcessNo { get; set; }
        public decimal? VoucherAmount { get; set; }
        public int? DepartmentId { get; set; }

        public string DivisionName { get; set; }
        public string ReqDept { get; set; }
        public string EmpName { get; set; }
        public DateTime? VoucherDate { get; set; }




        public string paidStatus { get; set; }
        public int? CreditPeriod { get; set; }
        public string ExpectedFindate { get; set; }
        public string UserDeptCode { get; set; }

        public int? itemId { get; set; }


        public decimal? VATPayable { get; set; }
        public decimal? TAXPayable { get; set; }
        public decimal? VATCurrent { get; set; }
        public string vendorstatus { get; set; }
        public int? NonDepartmentId { get; set; }
        public int? NoVat { get; set; }
        public int? NoReturn { get; set; }
        public int? hdnPaymentAccId { get; set; }
        public string payFrom { get; set; }
        public string narration { get; set; }
        public decimal? vatCurrentAmount { get; set; }
        public int? rebatable { get; set; }
        public int? deductable { get; set; }
        public int? taxExempted { get; set; }
        public int? taxInclusive { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public int? subLedgerTaxId { get; set; }
        public int? subLedgerVatId { get; set; }
        
        //@isRebate=0 AND @VATDEDUCTABLE = 1 AND @TAXEXAMTED = 0 AND @TAXINLUCSIVE = 1
        public IEnumerable<Item> items { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public SLNoViewModel SLNoViewModel { get; set; }
        public AutoProcessNumberViewModel AutoProcessNumberViewModel { get; set; }
        public IEnumerable<DailyBillReceive> dailyBillReceives { get; set; }
        public DailyBillReceive dailyBillReceive { get; set; }
        public IEnumerable<VatTaxRate> VatTaxRates { get; set; }
        public IEnumerable<TaxYear> taxYears { get; set; }
        public IEnumerable<CostCentre> costCentres { get; set; }
        public IEnumerable<VoucherMaster> GetVoucherMasters { get; set; }
    }
}
