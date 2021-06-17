using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IPassportInfoService
    {
        Task<bool> SavePassportInfo(PassportDetails passportDetail);
        Task<IEnumerable<PassportDetails>> GetPassportInfo();
        Task<PassportDetails> GetPassportInfoById(int id);
        Task<bool> DeletePassportInfoById(int id);
        Task<IEnumerable<PassportDetails>> GetPassportInfoByEmpId(int empId);
    }
}
