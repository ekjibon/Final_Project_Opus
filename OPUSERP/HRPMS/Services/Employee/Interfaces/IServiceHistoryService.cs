using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IServiceHistoryService
    {
        Task<bool> SaveServiceHistory(TransferLog traveInfo);
        Task<IEnumerable<TransferLog>> GetServiceHistory();
        Task<TransferLog> GetServiceHistoryById(int id);
        Task<bool> DeleteServiceHistoryById(int id);
        Task<IEnumerable<TransferLog>> GetServiceHistoryByEmpId(int empId);
    }
}
