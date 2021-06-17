using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("JobRequisition", Schema = "RCRT")]
    public class JobRequisition : Base
    {
        public string jobNo { get; set; }
        public string jobTitle { get; set; }
        public DateTime? requisitionDate { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string justification { get; set; }
       
        public int? departmentId { get; set; }
        public Department department { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? jobSourceId { get; set; }
        public JobSource jobSource { get; set; }

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
        [NotMapped]
        public DateTime? deadline { get; set; }
    }
}
