using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("AttendenceApi", Schema = "HR")]
    public class AttendenceApi : Base
    {
        
        public string empCode { get; set; }
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
        public string Latitudein { get; set; }
        //public string Date { get; set; }
        public string Longitudein { get; set; }
        public string Latitudeout { get; set; }
        public string Longitudeout { get; set; }
        public string Reason { get; set; }

        public int status { get; set; }

    }
}
