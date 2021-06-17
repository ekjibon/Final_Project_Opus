using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("AssignDocument", Schema = "Doc")]
    public class AssignDocument : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? documentMasterId { get; set; }
        public DocumentMaster documentMaster { get; set; }

        public int? documentPhotoAttachmentId { get; set; }
        public DocumentPhotoAttachment documentPhotoAttachment { get; set; }
        
        public int? isViewed { get; set; }
        [MaxLength(10)]
        public string hasEmployee { get; set; }
    }
}
