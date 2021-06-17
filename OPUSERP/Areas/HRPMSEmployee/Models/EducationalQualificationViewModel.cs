using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EducationalQualificationViewModel
    {
        public int employeeId { get; set; }
        public int educationId { get; set; }        
        public string institution { get; set; }
        public int? resultId { get; set; }
        public string majorGroup { get; set; }        
        public string grade { get; set; }                
        public int? passingYear { get; set; }        
        public int? degreeId { get; set; }        
        public int? organizationId { get; set; }
        public int? reldegreesubjectId { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public EducationalQualificationLn fLang { get; set; }
        public IEnumerable<EducationalQualification> educationalQualifications { get; set; }
    }
}
