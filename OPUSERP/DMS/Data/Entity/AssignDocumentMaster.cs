using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("AssignDocumentMaster", Schema = "Doc")]
    public class AssignDocumentMaster : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? documentMasterId { get; set; }
        public DocumentMaster documentMaster { get; set; }       
        
        public int? isViewed { get; set; }
        [MaxLength(10)]
        public string hasEmployee { get; set; }
    }
}
