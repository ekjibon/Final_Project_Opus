using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface IFreedomFighterService
    {
        Task<bool> SaveFreedomFighter(FreedomFighter freedomFighter);
        Task<IEnumerable<FreedomFighter>> GetFreedomFighter();
        Task<FreedomFighter> GetFreedomFighterById(int id);
        Task<bool> DeleteFreedomFighterById(int id);
        Task<FreedomFighter> GetFreedomFighterByEmpId(int empId);
        Task<IEnumerable<FreedomFighter>> GetFreedomFighterlistByEmpId(int empId);
    }
}
