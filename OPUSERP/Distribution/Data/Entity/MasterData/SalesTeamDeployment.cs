using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("SalesTeamDeployment", Schema = "Distribution")]
    public class SalesTeamDeployment:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? salesLevelId { get; set; }
        public SalesLevel salesLevel { get; set; }
        public int? areaId { get; set; }
        public Area area { get; set; }
        public int? salesTeamDeploymentId { get; set; }
        public SalesTeamDeployment salesTeamDeployment { get; set; }
    }
}
