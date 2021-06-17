using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class TraveInfoViewModel
    {
        public string employeeID { get; set; }

        public int travelId { get; set; }

        [Required]
        [Display(Name = "Tour Type")]
        public string type { get; set; }

        [Display(Name = "Tour purpose")]
        public string purpose { get; set; }

        [Display(Name = "Tour location")]
        public string location { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }


        [Display(Name = "Sponsoring Agency")]
        public string sponsoringAgency { get; set; }


        [Display(Name = "Start Date")]
        public DateTime? startDate { get; set; }


        [Display(Name = "End Date")]
        public DateTime? endDate { get; set; }

        [Display(Name = "Go Number")]
        public string goNumber { get; set; }

        [Display(Name = "Go Date")]
        public DateTime? goDate { get; set; }

        [Display(Name = "File")]
        public string file { get; set; }

        [Display(Name = "Title Of File")]
        public string titleOfFile { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }

        public string titleName { get; set; }
        public string accountCode { get; set; }
        public int? hrProgramId { get; set; }
        public int? projectId { get; set; }
        public DateTime? leaveStartDate { get; set; }
        public DateTime? leaveEndDate { get; set; }

        public Travel fLang { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<TraveInfo> traveInfos { get; set; }
        
        public IEnumerable<TravelPurpose> travelPurposes { get; set; }
        
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<HrProgram> hrPrograms { get; set; }
        public IEnumerable<Project> projects { get; set; }
    }
}
