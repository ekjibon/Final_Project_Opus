using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Patient.Data.Entity
{
    [Table("PatientRepresentative", Schema = "HOSPTL")]
    public class PatientRepresentative : Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        [MaxLength(150)]
        public string representativeName { get; set; }
        [MaxLength(550)]
        public string representativeAddress { get; set; }
        [MaxLength(15)]
        public string representativeMobile { get; set; }
        [MaxLength(100)]
        public string representativeEmail { get; set; }
        [MaxLength(200)]
        public int? representativeDesignation { get; set; }

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public int? isactive { get; set; }
    }
}
