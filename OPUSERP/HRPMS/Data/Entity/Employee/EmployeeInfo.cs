using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeInfo", Schema = "HR")]
    public class EmployeeInfo : Base
    {
        [Required]
        [MaxLength(50)]
        public string employeeCode { get; set; }

        [MaxLength(100)]
        public string nationalID { get; set; }

        [MaxLength(100)]
        public string birthIdentificationNo { get; set; }

        [MaxLength(250)]
        public string govtID { get; set; }
        [MaxLength(250)]
        public string gpfNomineeName { get; set; }
        [MaxLength(50)]
        public string gpfAcNo { get; set; }
        [MaxLength(250)]
        public string nameEnglish { get; set; }
        [MaxLength(250)]
        public string nameBangla { get; set; }
        [MaxLength(250)]
        public string motherNameEnglish { get; set; }
        [MaxLength(250)]
        public string motherNameBangla { get; set; }
        [MaxLength(250)]
        public string fatherNameEnglish { get; set; }
        [MaxLength(250)]
        public string fatherNameBangla { get; set; }
        [MaxLength(100)]
        public string nationality { get; set; }
        [MaxLength(3)]
        public string disability { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? joiningDatePresentWorkstation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? joiningDateGovtService { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateofregularity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfPermanent { get; set; }
        
        public DateTime? LPRDate { get; set; } //calculative From Date of Birth

        public DateTime? PRLStartDate { get; set; } //calculative From Date of Birth
        public DateTime? PRLEndDate { get; set; } //calculative From Date of Birth
        [MaxLength(10)]
        public string gender { get; set; }
        [MaxLength(250)]
        public string birthPlace { get; set; }
        [MaxLength(20)]
        public string maritalStatus { get; set; }

        public int?  religionId { get; set; }
        public Religion religion { get; set; }

        public int? employeeTypeId { get; set; }
        public EmployeeType employeeType { get; set; }

        public int? activityStatus { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }
        [MaxLength(30)]
        public string tin { get; set; }

        public string batch { get; set; }
        [MaxLength(20)]
        public string bloodGroup { get; set; }

        public bool freedomFighter { get; set; }
        [MaxLength(150)]
        public string freedomFighterNo { get; set; }
        [MaxLength(50)]
        public string telephoneOffice { get; set; }
        [MaxLength(50)]
        public string telephoneResidence { get; set; }
        [MaxLength(50)]
        public string pabx { get; set; }
        [MaxLength(150)]
        public string emailAddress { get; set; }
        [MaxLength(150)]
        public string emailAddressPersonal { get; set; } // Next generated not planned

        [MaxLength(50)]
        public string mobileNumberOffice { get; set; }

        [MaxLength(50)]
        public string mobileNumberPersonal { get; set; }
        [MaxLength(250)]
        public string specialSkill { get; set; }
       
        [MaxLength(50)]
        public string seniorityNumber { get; set; }
        [MaxLength(250)]
        public string designation { get; set; }
        [MaxLength(150)]
        public string skypeId { get; set; }

        public int? post { get; set; } // Related PostID But Not FK Referenced 

        public int designationCheck { get; set; }//Current Charged Checked
        [MaxLength(250)]
        public string joiningDesignation { get; set; }
        
        [MaxLength(100)]
        public string natureOfRequitment { get; set; } // Direct Or Absorbed
        [MaxLength(100)]
        public string homeDistrict { get; set; }

        public int? branchId { get; set; }
        public SpecialBranchUnit branch { get; set; }

        public int? pNSId { get; set; }
        public PNS pNS { get; set; }

        //For Type Managing 
        [MaxLength(100)]
        public string orgType { get; set; }

        //Application User LInk
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int? isApproved { get; set; }//0= not approved,1=approved

        public int? shiftGroupId { get; set; }
        public ShiftGroupMaster shiftGroup { get; set; }

        public int? employeeStatusId { get; set; }
        public ServiceStatus employeeStatus { get; set; }

        public int? hrProgramId { get; set; }
        public HrProgram hrProgram { get; set; }

        public int? hrUnitId { get; set; }
        public HrUnit hrUnit { get; set; }

        public int? locationId { get; set; }
        public Location location { get; set; }
        public int? functionInfoId { get; set; }
        public FunctionInfo functionInfo { get; set; }
        
        public string disablityType { get; set; }
        [DefaultValue(1)]
        public int? salaryStatus { get; set; }
        public string salaryStatusComment { get; set; }
    }
}
