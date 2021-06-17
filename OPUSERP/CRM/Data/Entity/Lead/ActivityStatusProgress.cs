using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("ActivityStatusProgress", Schema = "CRM")]
    public class ActivityStatusProgress:Base
    {
        public int? activityMasterId { get; set; }
        public ActivityMaster activityMaster { get; set; }
        public int? activityStatusId { get; set; }
        public ActivityStatus activityStatus { get; set; }
        public string remarks { get; set; }
    }
}
