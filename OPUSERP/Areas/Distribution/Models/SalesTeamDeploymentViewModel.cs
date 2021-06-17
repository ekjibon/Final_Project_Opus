using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class SalesTeamDeploymentViewModel
    {
        public int? Id { get; set; }

        public int? employeeInfoId { get; set; }
       

        public int? salesLevelId { get; set; }
      
        public int? areaId { get; set; }
        public int? salesTeamDeploymentId { get; set; }
      

       
        public IEnumerable<SalesLevel> salesLevels { get; set; }
        public IEnumerable<Area> areas { get; set; }
        public IEnumerable<SalesTeamDeployment> salesTeamDeployments { get; set; }
    }
}
