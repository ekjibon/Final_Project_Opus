using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Attendance
{
    [Table("AttendenceUpload", Schema = "HR")]
    public class AttendenceUpload : Base
    {
        [MaxLength(50)]
        public string empCode { get; set; }
        public DateTime? entryDate { get; set; }
    }
}