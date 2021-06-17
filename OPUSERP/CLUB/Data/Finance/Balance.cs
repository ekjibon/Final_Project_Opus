using OPUSERP.CLUB.Data.Member;
using System;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Payroll.Data.Entity.Salary;

namespace OPUSERP.CLUB.Data.Finance
{
    [Table("Balance", Schema = "Club")]
    public class Balance: Base
    {
        //Foreign Relation
        public int? employeeId { get; set; }
        public MemberInfo employee { get; set; }

        //Foreign Relation
        public int? paymentHeadId { get; set; }
        public PaymentHead paymentHead { get; set; }

        public double paid { get; set; }
        public double due { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime?  date { get; set; }
    }
}
