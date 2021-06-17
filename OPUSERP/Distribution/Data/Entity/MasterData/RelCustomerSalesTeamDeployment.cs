using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("RelCustomerSalesTeamDeployment", Schema = "Distribution")]
    public class RelCustomerSalesTeamDeployment:Base
    {
        public int? nsmsalesTeamDeploymentId { get; set; }

        public SalesTeamDeployment nsmsalesTeamDeployment { get; set; }

        public int? rsmsalesTeamDeploymentId { get; set; }

        public SalesTeamDeployment rsmsalesTeamDeployment { get; set; }
        public int? tsmsalesTeamDeploymentId { get; set; }

        public SalesTeamDeployment tsmsalesTeamDeployment { get; set; }
        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }
        public int? areaId { get; set; }
        public Area area { get; set; }
        public int? salesLevelId { get; set; }
        public SalesLevel salesLevel { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
    }
}
