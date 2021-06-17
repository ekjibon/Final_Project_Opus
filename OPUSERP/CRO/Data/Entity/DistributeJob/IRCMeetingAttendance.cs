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
    [Table("IRCMeetingAttendance", Schema = "CRO")]
    public class IRCMeetingAttendance : Base
    {
        public int? operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }       
       
    }
}
