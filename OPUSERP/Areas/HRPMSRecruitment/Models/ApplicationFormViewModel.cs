using OPUSERP.HRPMS.Data.Entity.Recruitment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class ApplicationFormViewModel
    {
        [Display(Name = "Applicants No")]
        public string applicantsNo { get; set; }

        [Display(Name = "Date")]
        public string date { get; set; }

        [Display(Name = "Name Bangla")]
        public string nameBn { get; set; }

        [Display(Name = "Name English")]
        public string nameEn { get; set; }

        [Display(Name = "National ID")]
        public string nid { get; set; }

        [Display(Name = "Birth Identification No")]
        public string bin { get; set; }

        [Display(Name = "Date Of Birth")]
        public string dateOfBirth { get; set; }

        [Display(Name = "Location Of Birth")]
        public string locationOfBirth { get; set; }

        [Display(Name = "Payment Ref. no")]
        public string paymentRefno { get; set; }

        [Display(Name = "Father Name Bangla")]
        public string fnameBn { get; set; }

        [Display(Name = "Father Name English")]
        public string fnameEn { get; set; }

        [Display(Name = "Mother Name Bangla")]
        public string mnameBn { get; set; }

        [Display(Name = "Mother Name English")]
        public string mnameEn { get; set; }
        
        [Display(Name = "Spouse Name Bangla")]
        public string spouseNameBn { get; set; }
        
        [Display(Name = "Spouse Name English")]
        public string spouseNameEn { get; set; }
        
        [Display(Name = "Present Division")]
        public string presentDivision { get; set; }
        
        [Display(Name = "Permanent Division")]
        public string PermanentDivision { get; set; }
        
        [Display(Name = "Present Post Office")]
        public string presentPostOffice { get; set; }
        
        [Display(Name = "Permanent Post Office")]
        public string PermanentPostOffice { get; set; }
        
        [Display(Name = "Present House No")]
        public string presentHouseNo { get; set; }
        
        [Display(Name = "Permanent House No")]
        public string PermanentHouseNo { get; set; }
        
        [Display(Name = "Mobile")]
        public string mobile { get; set; }
        
        [Display(Name = "Email")]
        public string email { get; set; }
        
        [Display(Name = "Nationality")]
        public string nationality { get; set; }
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [Display(Name = "Religion")]
        public string religion { get; set; }

        [Display(Name = "Occupation")]
        public string occupation { get; set; }
        
        [Display(Name = "Name Of Office")]
        public string nameOfOffice { get; set; }

        [Display(Name = "Position")]
        public string position { get; set; }

        [Display(Name = "Join Date")]
        public string joinDate { get; set; }

        [Display(Name = "Resign Date")]
        public string resignDate { get; set; }
        
        public IEnumerable<ApplicationForm> applicationForms { get; set; }
    }
}
