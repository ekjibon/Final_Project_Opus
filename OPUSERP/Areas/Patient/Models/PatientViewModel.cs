using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Models
{
    public class PatientViewModel
    {        
        public int patientId { get; set; }
        public int resourceId { get; set; }
        public int contactId { get; set; }

        public string patientName { get; set; }
        public int? ageInYears { get; set; }
        public int? ageInMonths { get; set; }
        public int? ageInDays { get; set; }
        public string gender { get; set; }
        public string patientMobile { get; set; }
        public string patientPhone { get; set; }
        public string patientEmail { get; set; }
        public DateTime? admissionDate { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string maritalStatus { get; set; }
        public string bloodGroup { get; set; }
        public string nationalId { get; set; }
        public string patientCode { get; set; }

    }
}
