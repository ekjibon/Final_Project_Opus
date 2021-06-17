using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class ColdActivityMasterCAViewModel
    {
        
        public string number { get; set; }
        public ColdActivityMasters activityMasters { get; set; }
        public List<ColdActivityDetails> activityDetails { get; set; }
        public List<ColdActivityTeams> activityTeam { get; set; }

        
    }
}
