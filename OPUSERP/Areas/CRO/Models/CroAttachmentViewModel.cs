using OPUSERP.CRO.Data.Entity.DistributeJob;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class CroAttachmentViewModel
    {
        public int? attachmentStatusId { get; set; }
        public int? attachmentTypeId { get; set; }
        public int? operationMasterId { get; set; }
        public int? actionTypeId { get; set; }
        public int? Id { get; set; }
        public string subjectName { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string createdBy { get; set; }
        public string createdAt { get; set; }
        public string fileTypeName { get; set; }
        public string attachmentTypeName { get; set; }
        public string archivedStatus { get; set; }
        

    }
}
