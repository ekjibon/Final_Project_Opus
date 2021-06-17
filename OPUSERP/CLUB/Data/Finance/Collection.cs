using OPUSERP.CLUB.Data.Member;
using System;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Payroll.Data.Entity.Salary;

namespace OPUSERP.CLUB.Data.Finance
{
    [Table("Collection", Schema = "Club")]
    public class Collection:Base
    {
        //Foreign Relation
        public int? employeeId { get; set; }
        public MemberInfo employee { get; set; }

        //Foreign Relation
        public int? paymentHeadId { get; set; }
        public PaymentHead paymentHead { get; set; }
        
        //Foreign Relation
        public int? trMasterId { get; set; }
        public TrMaster trMaster { get; set; }

        public string period { get; set; }

        public decimal? amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? paymentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? verifyDate { get; set; }

        public string status { get; set; } // paid, pending, rejected.

        public string remarks { get; set; }


    }
}
