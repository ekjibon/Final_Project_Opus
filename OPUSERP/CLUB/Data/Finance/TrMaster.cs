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
    [Table("TrMaster", Schema = "Club")]
    public class TrMaster:Base
    {
        //Foreign Relation
        public int? employeeId { get; set; }
        public MemberInfo employee { get; set; }

        public decimal? amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public string url { get; set; } // Attatchemnts URLS

        public string adminFeedBack { get; set; }

        public string paymentType { get; set; } // paymentType="Cash", paymentType="MobileBanking", paymentType="Bank".

        public string trNumber { get; set; }

        public int status { get; set; } // 1= Confirm, 0=pending, 2=Reject

        public string remarks { get; set; }
    }
}
