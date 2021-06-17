using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("Belongings", Schema = "HR")]
    public class Belongings:Base
    {
        [MaxLength(70)]
        public string assetNo { get; set; }

        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public int? belongingItemId { get; set; }
        public BelongingItem belongingItem { get; set; }

        [MaxLength(400)]
        public string description { get; set; }

        public DateTime? issueDate { get; set; }

        public DateTime? returnDate { get; set; }

        public int? shortOrder { get; set; }
    }
}
