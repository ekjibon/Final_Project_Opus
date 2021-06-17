using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RequisitionDetails", Schema = "SCM")]
    public class RequisitionDetail:Base
    {
        public int requisitionMasterId { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public int? teamMemberId { get; set; }
        public TeamMember teamMember { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? reqQty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? reqRate { get; set; }

        public DateTime? targetDate { get; set; }

        public string justification { get; set; }
        public string budgetCode { get; set; } 
        public DateTime? jobAssignDate { get; set; }
        public string deliveryLocation { get; set; }
    }
}
