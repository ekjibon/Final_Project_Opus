using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("AttachmentStatus", Schema = "CRO")]
    public class AttachmentStatus : Base
    {        
        public int? operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        public int? attachmentTypeId { get; set; }
        public AttachmentType attachmentType { get; set; }

        [MaxLength(250)]
        public string fileTypeName { get; set; }

        [MaxLength(250)]
        public string subjectName { get; set; }

        public int? archieveId { get; set; }
    }
}
