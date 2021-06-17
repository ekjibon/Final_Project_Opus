using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("CRMDepartment", Schema = "CRM")]
    public class CRMDepartment : Base
    {
        public string deptCode { get; set; }

        [Required]
        public string deptName { get; set; }
        public string deptNameBn { get; set; }

        public string shortName { get; set; }
        public DateTime? startDate { get; set; }

    }
}
