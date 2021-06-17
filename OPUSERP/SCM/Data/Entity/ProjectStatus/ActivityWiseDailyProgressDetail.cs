using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ActivityWiseDailyProgressDetails", Schema = "SCM")]
    public class ActivityWiseDailyProgressDetail:Base
    {
        public int? workProgressActivityId { get; set; }
        public WorkProgressActivityDescription workProgressActivity { get; set; }
        public int? itemDetialId { get; set; }
        public ActivityWiseItemDetial itemDetial { get; set; }
        public decimal? materialReceived { get; set; }
        public decimal? materialConsumtion { get; set; }
    }
}
