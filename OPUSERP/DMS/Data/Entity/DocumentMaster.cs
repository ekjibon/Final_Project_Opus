using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("DocumentMaster", Schema = "Doc")]
    public class DocumentMaster : Base
    {
        public int? documentCategoryId { get; set; }
        public DocumentCategory documentCategory { get; set; }

        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        [MaxLength(400)]
        public string documentName { get; set; }
        [MaxLength(100)]
        public string documentNo { get; set; }           
        public DateTime? docCreateDate { get; set; }
        [MaxLength(200)]
        public string docCreateBy { get; set; }
    }
}
