using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IBelongingsService
    {
        Task<bool> DeleteBelongingsById(int id);
        Task<IEnumerable<Belongings>> GetBelongings();
        Task<IEnumerable<Belongings>> GetBelongingsByEmpId(int empId);
        Task<Belongings> GetBelongingsById(int id);
        Task<bool> SaveBelongings(Belongings belongings);
    }
}
