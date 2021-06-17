using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("TraveInfo", Schema = "HR")]
    public class TraveInfo : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? travelPurposeId { get; set; }
        public TravelPurpose travelPurpose { get; set; }

        public string purpose { get; set; }

        public string location { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public string sponsoringAgency { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }

        public string goNumber { get; set; }

        public DateTime? goDate { get; set; }

        public string file { get; set; }

        public string titleOfFile { get; set; }

        public string remarks { get; set; }
        [MaxLength(550)]
        public string titleName { get; set; }
        [MaxLength(250)]
        public string accountCode { get; set; }

        public int? hrProgramId { get; set; }
        public HrProgram hrProgram { get; set; }

        public int? projectId { get; set; }
        public Project project { get; set; }
        public DateTime? leaveStartDate { get; set; }
        public DateTime? leaveEndDate { get; set; }
    }
}
