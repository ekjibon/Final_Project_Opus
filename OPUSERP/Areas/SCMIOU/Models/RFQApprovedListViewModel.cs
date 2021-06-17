using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMIOU.Models
{
    public class RFQApprovedListViewModel
    {
       
        public int? Id { get; set; }
        public string rfqNo { get; set; }
        public string rfqSubject { get; set; }
        public DateTime? rfqDate { get; set; }
        public int? status { get; set; }
    }
}
