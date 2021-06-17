using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class ProfessionalQualificationsViewModel
    {
        public int? employeeID { get; set; }
        public int? qualificationID { get; set; }
        public int? qualificationHeadId { get; set; }
        public string subject { get; set; }
        public int? result { get; set; }
        public string instituteName { get; set; }
        public string passingYear { get; set; }
        public string markGrade { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<QualificationHead> qualificationHeads { get; set; }
        public IEnumerable<ProfessionalQualifications> professionalQualifications { get; set; }
        public IEnumerable<Result> results { get; set; }
    }
}
