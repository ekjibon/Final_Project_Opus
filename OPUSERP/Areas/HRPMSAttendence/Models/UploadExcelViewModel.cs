using OPUSERP.HRPMS.Data.Entity.Attendance;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    public class UploadExcelViewModel
    {
        public string EmployeeCode { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Location { get; set; }

        public int?[] heads { get; set; }
        public decimal?[] amounts { get; set; }

        public string[] dbField { get; set; }
        public string[] headName { get; set; }
        public DateTime?[] col1 { get; set; }
        public string[] col2 { get; set; }
        public string[] col3 { get; set; }
        public string[] col4 { get; set; }

        public IEnumerable<AttendenceUpload> attendenceUploads { get; set; }
    }
}