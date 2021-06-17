using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class CandidateInfoViewModel
    {
        //[Required]
        [Display(Name = "Candidate ID")]
        public int? candidateId { get; set; }

        //[Required]
        [Display(Name = "Reference Job No")]
        public string referenceJobNo { get; set; }

        [Display(Name = "National ID")]
        public string nationalID { get; set; }
        
        [Required]
        [Display(Name = "Candidate Name")]
        public string candidateName { get; set; }

        [Display(Name = "Nationality")]
        public string nationality { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? dateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Place of Birth")]
        public string placeOfBirth { get; set; }

        [Display(Name = "Marital Status")]
        public string maritalStatus { get; set; }

        [Display(Name = "Religion")]
        public string religion { get; set; }
        
        public int? department { get; set; }
        
        [Display(Name = "Blood Group")]
        public string bloodGroup { get; set; }


        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Mobile No")]
        public string mobile { get; set; }

        public string homeDistrict { get; set; }

        public string designation { get; set; }

        public string action { get; set; }
        public string status { get; set; }
        public int? cvId { get; set; }
        public int? jobReqId { get; set; }

        public string candidateNameCode { get; set; }

        public JobRequisition jobReq { get; set; }
        public CandidateInfo candidateInfo { get; set; }
        public IEnumerable<CandidateInfo> candidateInfos { get; set; }
        public IEnumerable<JobRequisition> jobRequisitions { get; set; }
        //public IEnumerable<Religion> religions { get; set; }
        //public IEnumerable<EmployeeType> employeeTypes { get; set; }
        //public IEnumerable<OrganoOrganization> organoOrganizations { get; set; }
        //public IEnumerable<Designation> designations { get; set; }
        //public IEnumerable<District> districts { get; set; }
        //public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        //public IEnumerable<Department> departments { get; set; }
    }
}
