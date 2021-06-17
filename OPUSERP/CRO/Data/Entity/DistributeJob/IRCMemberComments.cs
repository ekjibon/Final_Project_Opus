using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("IRCMemberComments", Schema = "CRO")]
    public class IRCMemberComments : Base
    {
        public int? operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        
        public string comments { get; set; }
    }
}
