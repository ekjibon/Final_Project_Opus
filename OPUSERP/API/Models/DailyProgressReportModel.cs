using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class DailyProgressReportModel
    {
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int? projectId { get; set; }
        public Project project { get; set; }
        public DateTime? reportDate { get; set; }
        public string reportNo { get; set; }
        public string remarks { get; set; }
        public int? statusId { get; set; }

        public int MyProperty { get; set; }
    }
}
