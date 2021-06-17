using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Patient.Data.Entity
{
    [Table("PatientInfo", Schema = "HOSPTL")]
    public class PatientInfo : Base
    {
        [MaxLength(250)]
        public string patientName { get; set; }
        public int? ageInYears { get; set; }
        public int? ageInMonths { get; set; }
        public int? ageInDays { get; set; }
        [MaxLength(6)]
        public string gender { get; set; }
        [MaxLength(15)]
        public string patientMobile { get; set; }
        [MaxLength(15)]
        public string patientPhone { get; set; }
        [MaxLength(100)]
        public string patientEmail { get; set; }
        public DateTime? admissionDate { get; set; }
        [MaxLength(10)]
        public string patientCode { get; set; }
        [MaxLength(200)]
        public string fatherName { get; set; }
        [MaxLength(200)]
        public string motherName { get; set; }
        [MaxLength(15)]
        public string maritalStatus { get; set; }
        [MaxLength(10)]
        public string bloodGroup { get; set; }
        [MaxLength(20)]
        public string nationalId { get; set; }
    }
}
