using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Attendance
{
    [Table("MovementTracking", Schema = "HR")]
    public class MovementTracking : Base
    {
        [MaxLength(50)]
        public string empCode { get; set; }
        [MaxLength(250)]
        public string EmpName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? entryDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? LoginTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? LogoutTime { get; set; }
        [MaxLength(100)]
        public string Latitude { get; set; }
        [MaxLength(100)]
        public string Longitude { get; set; }
        [MaxLength(300)]
        public string CompanyName { get; set; }
        [MaxLength(300)]
        public string Contact { get; set; }
        [MaxLength(300)]
        public string DesignationName { get; set; }
        public string Reason { get; set; }
        public int? Status { get; set; }
    }
}