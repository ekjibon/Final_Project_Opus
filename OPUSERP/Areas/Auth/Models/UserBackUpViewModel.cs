using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Auth.Models
{
    public class UserBackUpViewModel
    {
        public UserBackup userBackup { get; set; }

        public EmployeeInfo employeeInfo { get; set; }
    }
}
