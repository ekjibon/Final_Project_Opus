
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSOPUSERP.Areas.MemberInfo.Models.NotMappedEntity;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class FeesViewModel
    {
        //Foreign Relation
        public int? employeeId { get; set; }

        public int? Id { get; set; }

        public int? paymentHead { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public double amount { get; set; }

        public double due { get; set; }

        public string remarks { get; set; }

        public string type { get; set; }

        public string actionType { get; set; }

        public string transectionId { get; set; }

        public string hiddenFile { get; set; }

        public int?[] selectPaymentHeadIds { get; set; }

        public decimal?[] selectPaymentHeadAmounts { get; set; }

        public decimal total { get; set; }

        [Display(Name = "Bank Receipt")]
        public IFormFile bankReceipt { get; set; }

        public SingleFees singleFees { get; set; }

        public FeeLn fLang { get; set; }

        public List<SingleFees> singleFeeses { get; set; }

        public IEnumerable<OPUSERP.CLUB.Data.Member.MemberInfo> employeeInfos { get; set; }

        public PaymentLog paymentLog { get; set; }

        public IEnumerable<PaymentLog> paymentLogs { get; set; }

        public IEnumerable<PaymentHead> paymentHeads { get; set; }

        public IEnumerable<Balance> balances { get; set; }

        public IEnumerable<TrMaster> trMasters { get; set; }

        public TrMaster trMaster { get; set; }

        public IEnumerable<Collection> collections { get; set; }

        public IEnumerable<PaymentHeadWithDue> monthlyPaymentHeadWithDues { get; set; }

        public IEnumerable<PaymentHeadWithDue> onetimePaymentHeadWithDues { get; set; }

        public PaymentReport paymentReports { get; set; }
    }
}
