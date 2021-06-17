using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class AddressViewModel
    {
        public int presentAddressID { get; set; }

        public int permanentAddressID { get; set; }

        public int employeeID { get; set; }

        public string presentRoadNo { get; set; }

        public string permanentRoadNo { get; set; }


        [Display(Name = "Present Division")]
        public int? presentDivision { get; set; }


        [Display(Name = "Present District")]
        public int? presentDistrict { get; set; }


        [Display(Name = "Present Upazila")]
        public int? presentUpazila { get; set; }

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


        [Display(Name = "Permanent Division")]
        public int? permanentDivision { get; set; }


        [Display(Name = "Permanent District")]
        public int? permanentDistrict { get; set; }


        [Display(Name = "Permanent Upazila")]
        public int? permanentUpazila { get; set; }

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

        public Address fLang { get; set; }

        public string employeeNameCode { get; set; }

        public MemberAddress present { get; set; }
        public MemberAddress permanent { get; set; }
    }
}
