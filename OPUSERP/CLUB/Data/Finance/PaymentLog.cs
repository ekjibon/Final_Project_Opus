using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Finance
{
    [Table("PaymentLog", Schema = "Club")]
    public class PaymentLog : Base
    {
        //Foreign Relation
        public int? employeeId { get; set; }
        public MemberInfo employee { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public double amount { get; set; }

        public string url { get; set; }

        public string remarks { get; set; }

        public string adminFeedBack { get; set; }

        public string paymentType { get; set; } // paymentType="Cash", paymentType="MobileBanking", paymentType="Bank".

        public string trNumber { get; set; }

        public int status { get; set; } // 1= Confirm, 0=pending, 2=Reject

    }
}
