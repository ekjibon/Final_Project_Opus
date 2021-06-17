using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IPromotionLogService
    {
        Task<bool> SavePromotionLog(PromotionLog traveInfo);
        Task<IEnumerable<PromotionLog>> GetPromotionLog();
        Task<PromotionLog> GetPromotionLogById(int id);
        Task<bool> DeletePromotionLogById(int id);
        Task<IEnumerable<PromotionLog>> GetPromotionLogByEmpId(int empId);
    }
}
