using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class BelongingsViewModel
    {
        public int? employeeID { get; set; }
        public int? belongingsItemID { get; set; }
        public int? itemID { get; set; }
        public string assetNo { get; set; }
        public string brandName { get; set; }
        public string modelNo { get; set; }
        public DateTime? issueDate { get; set; }
        public DateTime? returnDate { get; set; }
        public int? specificationID { get; set; }
        public int? belongingId { get; set; }
        public string remarks { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<Belongings> belongings { get; set; }
        public IEnumerable<BelongingItem> belongingItems { get; set; }
    }
}
