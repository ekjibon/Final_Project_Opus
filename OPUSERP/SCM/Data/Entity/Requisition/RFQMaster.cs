using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RFQMaster", Schema = "SCM")]
    public class RFQMaster:Base
    {
        public string rfqNo { get; set; }
        public DateTime? rfqDate { get; set; }
        public string rfqSubject { get; set; }
        public string termscondition { get; set; }
        public int? isclose { get; set; }
        public int? status { get; set; }

    }
}
