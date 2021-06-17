using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("DailyProgressReports", Schema = "SCM")]
    public class DailyProgressReport:Base
    {
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int? projectId { get; set; }
        public Project project { get; set; }
        public DateTime? reportDate { get; set; }
        public string siteCondition { get; set; }
        public string siteWeather { get; set; }
        public string reportNo { get; set; }
        public string remarks { get; set; }
        public int? statusId { get; set; }
        [NotMapped]
        public EmployeeInfo employeeInfo { get; set; }
    }
}
