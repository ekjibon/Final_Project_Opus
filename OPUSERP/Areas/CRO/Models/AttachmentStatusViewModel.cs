using Microsoft.AspNetCore.Http;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OPUSERP.Areas.CRO.Models
{
    public class AttachmentStatusViewModel
    {
        public int attachmentStatusId { get; set; }
        public int? operationMasterId { get; set; }
        public int? attachmentTypeId { get; set; }
        public string fileTypeName { get; set; }
        public string subjectName { get; set; }

        public int? receiveDocumentId { get; set; }
        public int? operationMasterIdDoc { get; set; }
        public int? documentChartId { get; set; }
        public int? archieveId { get; set; }

        public int? operationMasterIdJob { get; set; }
        public int? jobStatusId { get; set; }
        public string remarks { get; set; }
        public string reportNo { get; set; }
        public DateTime? reportDate { get; set; }
        public DateTime? convertDate { get; set; }
        public DateTime? archiveDate { get; set; }
        public DateTime? reportPrintDate { get; set; }
        public DateTime? ratingDate { get; set; }
        public DateTime? ratingValidTillDate { get; set; }
        // public IFormFile imagePath { get; set; }
        [MaxLength(150)]
        public string ratingValidDays { get; set; }
        public string proposedIRCRatingTypeName { get; set; }       
        public string proposedIRCRatingValue { get; set; }        
        public string proposedIRCShortRatingName { get; set; }        
        public string proposedIRCOutlook { get; set; }        
        public string proposedEntityRatingName { get; set; }
        public string description { get; set; }
        public DateTime? receiveDate { get; set; }

        public IEnumerable<OperationMaster> operationMasters { get; set; }
        public IEnumerable<GetOMByassignToReviewerViewModel> getOMByassignToReviewerViewModels { get; set; }
        public IEnumerable<AttachmentType> attachmentTypes { get; set; }
        public IEnumerable<DocumentChart> documentCharts { get; set; }
        public IEnumerable<JobStatus> jobStatuses { get; set; }
        public IEnumerable<RatingValue> ratingValues { get; set; }
        public IEnumerable<Archive> archives { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        public IEnumerable<GetOMAssignToConverterViewModel> getOMAssignToConverterViewModels { get; set; }
        public IEnumerable<GetOMAssignToConverterdViewModel> getOMAssignToConverterdViewModels { get; set; }
        public IEnumerable<GetOMByassignToClosedViewModel> getOMByassignToClosedViewModels { get; set; }
        public IEnumerable<GetOMByassignToDeclaredViewModel> getOMByassignToDeclareViewModels { get; set; }
        public IEnumerable<GetOMReviewerListViewModel> getOMReviewerListViewModels { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
    }
}
