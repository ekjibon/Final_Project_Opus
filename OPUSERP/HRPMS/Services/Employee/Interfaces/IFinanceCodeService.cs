using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface IFinanceCodeService
    {
        Task<bool> SaveFinanceCode(FinanceCode financeCode);
        Task<IEnumerable<FinanceCode>> GetFinanceCode();
        Task<FinanceCode> GetFinanceCodeById(int id);
        Task<bool> DeleteFinanceCodeById(int id);
        Task<IEnumerable<FinanceCode>> GetFinanceCodeByEmpId(int empId);
    }
}
