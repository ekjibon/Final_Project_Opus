using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Master;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class MemberInfoViewModel
    {
        
        [Display(Name = "Emp ID")]
        public string employeeID { get; set; }

        [Display(Name = "Employee Code")]
        public string employeeCode { get; set; }

        [Display(Name = "National ID")]
        public string nationalID { get; set; }

        [Display(Name = "Birth Identification No")]
        public string birthIdentificationNo { get; set; }

        [Display(Name = "Govt. ID")]
        public string govtID { get; set; }

        [Display(Name = "GPF Nominee Name")]
        public string gpfNomineeName { get; set; }

        [Display(Name = "GPF Acc No")]
        public string gpfAcNo { get; set; }

        [Display(Name = "Name In English")]
        public string nameEnglish { get; set; }
        
        [Display(Name = "Name In Bangla")]
        public string nameBangla { get; set; }

        [Display(Name = "Mother Name In English")]
        public string motherNameEnglish { get; set; }
        
        [Display(Name = "Mother Name In Bangla")]
        public string motherNameBangla { get; set; }

        [Display(Name = "Father Name In English")]
        public string fatherNameEnglish { get; set; }
        
        [Display(Name = "Father Name In Bangla")]
        public string fatherNameBangla { get; set; }

        [Display(Name = "Nationality")]
        public string nationality { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? dateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Place of Birth")]
        public string birthPlace { get; set; }

        [Display(Name = "Marital Status")]
        public string maritalStatus { get; set; }

        [Display(Name = "Religion")]
        public int? religion { get; set; }
        

        [Display(Name = "Employee Type")]
        public string employeeType { get; set; }
        
        [Display(Name = "Date Of Membership")]
        public DateTime? dateOfMembership { get; set; }

        [Display(Name = "Blood Group")]
        public string bloodGroup { get; set; }

        [Display(Name = "Freedom Fighter")]
        public string freedomFighter { get; set; }

        [Display(Name = "Freedom Fighter No")]
        public string freedomFighterNo { get; set; }
        
        [Display(Name = "Telephone (Office)")]
        public string telephoneOffice { get; set; }

        [Display(Name = "Telephone (Residence)")]
        public string telephoneResidence { get; set; }

        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Display(Name = "Mobile Number (Office)")]
        public string mobileNumberOffice { get; set; }

        [Display(Name = "Mobile Number (Personal)")]
        public string mobileNumberPersonal { get; set; }

        public string emailAddressPersonal { get; set; }



        public int[] skills { get; set; }

        public string homeDistrict { get; set; }
        public string natureOfRequitment { get; set; }
        public string specialSkill { get; set; }
        public string seniorityNumber { get; set; }
        public string joiningDesignation { get; set; }
        public string designation { get; set; }

        public string action { get; set; }

        public EmployeeInfoLn fLang { get; set; }
        public ACRLn fLang4 { get; set; }

        public int? post { get; set; }//Hidden Field For Designation, Handle.
        public int? activityStatus { get; set; }
        public string employeeNameCode { get; set; }
        public OPUSERP.CLUB.Data.Member.MemberPhotograph photograph { get; set; }

        public int? CurrentPage { get; set; }
        public int? TotalPage { get; set; }
        public int? dataCount { get; set; }

        public OPUSERP.CLUB.Data.Member.MemberInfo employeeInfo { get; set; }
        public IEnumerable<OPUSERP.CLUB.Data.Member.MemberInfo> allEmployeeInfos { get; set; }
        public IEnumerable<Religion> religions { get; set; }
        public IEnumerable<MemberType> memberTypes { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<Degree> degrees { get; set; }
        public IEnumerable<Subject> subjects { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<SpecialSkill> specialSkills { get; set; }
        public IEnumerable<AllMemberJson> allMemberJsons { get; set; }
        public IEnumerable<MemberTransferLog> memberTransferLogs { get; set; }


        public IEnumerable<MemberInfoReportViewModel> memberInfoReportViewModels { get; set; }
    }
}
