using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Models
{
    public class PatientRepresentativeViewModel
    {
        public int patientRepresentativeId { get; set; }
        public int? patientInfoId { get; set; }
        public int? employeeInfoId { get; set; }
        public string representativeName { get; set; }
        public string representativeAddress { get; set; }
        public string representativeMobile { get; set; }
        public string representativeEmail { get; set; }
        public int? representativeDesignation { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? isactive { get; set; }

        public IEnumerable<PatientRepresentative> patientRepresentatives { get; set; }
    }
}
