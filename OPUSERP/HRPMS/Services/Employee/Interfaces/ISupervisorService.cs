using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface ISupervisorService
    {
        Task<bool> DeleteSupervisorById(int id);
        Task<IEnumerable<Supervisor>> GetSupervisor();
        Task<IEnumerable<Supervisor>> GetSupervisorByEmpId(int empId);
        Task<Supervisor> GetFirstSupervisorByEmpId(int empId);
        Task<Supervisor> GetSupervisorById(int id);
        Task<bool> SaveSupervisor(Supervisor supervisor);
    }
}
