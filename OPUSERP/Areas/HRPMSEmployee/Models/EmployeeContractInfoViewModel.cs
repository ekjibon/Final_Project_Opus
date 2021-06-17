using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeContractInfoViewModel
    {
        public int? contractId { get; set; }
        public int employeeID { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string remarks { get; set; }
        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<EmployeeContractInfo> employeeContractInfos { get; set; }
    }
}
