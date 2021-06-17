using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RequisitionMasters", Schema = "SCM")]
    public class RequisitionMaster:Base
    {
        [MaxLength(150)]
        public string reqNo { get; set; }
        public DateTime? reqDate { get; set; }

        public DateTime? targetDate { get; set; }

        public string justification { get; set; }

        public string subject { get; set; }
        public string deliveryaddress { get; set; }

        public int? reqBy { get; set; }

        [MaxLength(200)]
        public string reqDept { get; set; }

        public int? projectId { get; set; }
        public Project project { get; set; }

        [MaxLength(50)]
        public string reqType { get; set; }

        public int? teamMasterId { get; set; }
        public TeamMaster teamMaster { get; set; }

        public DateTime? assignDate { get; set; }

        public DateTime? approveDate { get; set; }

        [MaxLength(50)]
        public string isPostPR { get; set; }

        public int isActive { get; set; }

        public int? statusInfoId { get; set; }
        public StatusInfo statusInfo { get; set; }
        public int? fundSourceId { get; set; }
        public FundSource fundSource { get; set; }

        public int? employeeinfoId { get; set; }
        public EmployeeInfo employeeinfo { get; set; }

       
    }
}
