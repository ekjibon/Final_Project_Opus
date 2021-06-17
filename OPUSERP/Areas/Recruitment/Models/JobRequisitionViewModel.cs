using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class JobRequisitionViewModel
    {
        public int? jobRequisitionId { get; set; }
        public int? departmentId { get; set; }
        public int? designationId { get; set; }
        public int? jobSourceId { get; set; }
        public int? jobPostId { get; set; }

        public string jobNo { get; set; }
        public string jobTitle { get; set; }        
        public DateTime requisitionDate { get; set; }
        public DateTime appointmentDate { get; set; }
        public string justification { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string jobSource { get; set; }
        public int? vacancy { get; set; }
        public string jobLocation { get; set; }
        public decimal? salary { get; set; }
        public string employmentStatus { get; set; }
        public string jobContext { get; set; }
        public string reqExperience { get; set; }
        public string reqEducationalQ { get; set; }
        public string jobResponsibility { get; set; }
        public string addRequirements { get; set; }
        public string compensationOthers { get; set; }

        public JobRequisition JobRequisition { get; set; }
        public IEnumerable<JobRequisition> jobRequisitions { get; set; }
        public IEnumerable<JobPost> jobPosts { get; set; }
        public IEnumerable<OPUSERP.HRPMS.Data.Entity.Master.Designation> designations { get; set; }
        public IEnumerable<OPUSERP.HRPMS.Data.Entity.Master.Department> departments { get; set; }
        public IEnumerable<JobSource> jobSources { get; set; }
        public IEnumerable<EmploymentType> employmentTypes { get; set; }
    }
}
