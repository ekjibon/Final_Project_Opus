using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRO.Models
{
    public class JobArchiveViewModel
    {
        public int? operationMasterId { get; set; }

        public DateTime? archiveDate { get; set; }
        public DateTime? reportPrintDate { get; set; }
        public DateTime? ratingDate { get; set; }
        public DateTime? ratingValidTillDate { get; set; }
        public string ratingValidDays { get; set; }
        public int? archieveId { get; set; }

        public int?[] softCopyId { get; set; }
        public int?[] hardCopyId { get; set; }

        public string archiveLocationSoftcopy { get; set; }
        public int? archiveShelfId { get; set; }
        public string hardCopyNewFileNo { get; set; }
        public string hardCopyOldFileNo { get; set; }
        public int? hardWorkingFile { get; set; }
        public int? softWorkingFile { get; set; }
        public int? archiveFloorId { get; set; }
        public string remarks { get; set; }


        public IEnumerable<OperationMaster> operationMasters { get; set; }
        public IEnumerable<IRCRating> iRCRatings { get; set; }
        public IEnumerable<DocumentPhotoAttachment>  documentPhotoAttachments { get; set; }
        public IEnumerable<ArchiveFloor> archiveFloors { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        

    }
}
