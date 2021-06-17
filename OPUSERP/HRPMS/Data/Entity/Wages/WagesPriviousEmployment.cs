using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("WagesPriviousEmployment", Schema = "HR")]
    public class WagesPriviousEmployment:Base
    {
        public int employeeID { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        public int? organizationTypeId { get; set; }
        public HRPMSOrganizationType organizationType { get; set; }

        public string companyName { get; set; }

        public string position { get; set; }

        public string department { get; set; }

        public string companyBusiness { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public string companyLocation { get; set; }

        public string responsibilities { get; set; }
    }
}
