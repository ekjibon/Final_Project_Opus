using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Publication = OPUSERP.Areas.HRPMSEmployee.Models.Lang.Publication;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class PublicationViewModel
    {
        public string employeeID { get; set; }

        public string publicationId { get; set; }

        [Required]
        [Display(Name = "Publication Name")]
        public string publicationName { get; set; }

        [Required]
        [Display(Name = "Publication Type")]
        public string publicationType { get; set; }

        [Display(Name = "Publication Place")]
        public string publicationPlace { get; set; }

        [Required]
        [Display(Name = "Publication Date")]
        public DateTime? publicationDate { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public Publication fLang { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<HRPMS.Data.Entity.Employee.Publication> publications { get; set; }
    }
}
