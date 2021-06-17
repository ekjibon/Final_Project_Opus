using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesBankInfoService
    {
        Task<bool> SaveBankInfo(WagesBankInfo bankInfo);
        Task<IEnumerable<WagesBankInfo>> GetBankInfo();
        Task<WagesBankInfo> GetBankInfoById(int id);
        Task<bool> DeleteBankInfoById(int id);
        Task<IEnumerable<WagesBankInfo>> GetBankInfoByEmpId(int empId);
    }
}
