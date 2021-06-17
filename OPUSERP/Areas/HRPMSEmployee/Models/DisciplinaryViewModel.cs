using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using OPUSERP.HRPMS.Data.Entity.Employee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class DisciplinaryViewModel
    {
        public int disciplinaryId { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string designation { get; set; }

        public int? offenseId { get; set; }

        public string punishmentId { get; set; }

        public DateTime? punishmentDate { get; set; }

        public DateTime? startFrom { get; set; }

        public DateTime? endTo { get; set; }

        public string goNo { get; set; }

        [Display(Name = "GO Attachment")]
        public IFormFile goAttachment { get; set; }

        public string remarks { get; set; }

        public string employeeNameCode { get; set; }

        public Disciplinary fLang { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<DisciplinaryLog> disciplinaries { get; set; }
        public IEnumerable<Offense> offenses { get; set; }
        public IEnumerable<NaturalPunishment> punishments { get; set; }
        
    }
}
