using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.Shagotom.Services.MasterData.Interfaces
{
    public interface IEmployeeInfoServiceForShagotom
    {
 
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeeInfo();
        Task<VisitorEntryViewModel> GetEmployeeInfoById(int id);

    }
}
