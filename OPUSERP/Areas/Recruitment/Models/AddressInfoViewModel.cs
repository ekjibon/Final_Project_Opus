using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class AddressInfoViewModel
    {
        public int presentAddressID { get; set; }

        public int permanentAddressID { get; set; }

        public int candidateId { get; set; }

        public string presentRoadNo { get; set; }

        public string permanentRoadNo { get; set; }

        [Required]
        [Display(Name = "Present Division")]
        public string presentDivision { get; set; }

        [Required]
        [Display(Name = "Present District")]
        public string presentDistrict { get; set; }

        [Required]
        [Display(Name = "Present Upazila")]
        public string presentUpazila { get; set; }

        [Display(Name = "Present Union")]
        public string presentUnion { get; set; }

        [Display(Name = "Present Post Office")]
        public string presentPostOffice { get; set; }

        [StringLength(50, ErrorMessage = "The {0} Must be at least {2} and at most {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Present Post Code")]
        public string presentPostCode { get; set; }

        [Display(Name = "Present Block/Sector")]
        public string presentBlockSector { get; set; }

        [Display(Name = "Present House/Village")]
        public string presentHouseVillage { get; set; }

        [Required]
        [Display(Name = "Permanent Division")]
        public string permanentDivision { get; set; }

        [Required]
        [Display(Name = "Permanent District")]
        public string permanentDistrict { get; set; }

        [Required]
        [Display(Name = "Permanent Upazila")]
        public string permanentUpazila { get; set; }

        [Display(Name = "Permanent Union")]
        public string permanentUnion { get; set; }

        [Display(Name = "Permanent Post Office")]
        public string permanentPostOffice { get; set; }

        [StringLength(50, ErrorMessage = "The {0} Must be at least {2} and at most {1} characters long.", MinimumLength = 3)]
        public string permanentPostCode { get; set; }

        [Display(Name = "Permanent Block/Sector")]
        public string permanentBlockSector { get; set; }

        [Display(Name = "Permanent House/Village")]
        public string permanentHouseVillage { get; set; }

        public string sameAddress { get; set; }

        public string employeeNameCode { get; set; }

        public RCRTAddress present { get; set; }
        public RCRTAddress permanent { get; set; }
        public CandidateInfo candidateInfo { get; set; }
    }
}
