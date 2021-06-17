using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class IRCMemberCommentsViewModel
    {
        public int ircMemberCommentsId { get; set; }
        public int? operationMasterId { get; set; }
        public string comments { get; set; }

        public int? employeeInfoIdComment { get; set; }

        public IEnumerable<OperationMaster> operationMasters { get; set; }
        public IEnumerable<IRCMemberComments> iRCMemberComments { get; set; }
        public IEnumerable<LeadsBankInfo> LeadsBankInfos { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
    }
}
