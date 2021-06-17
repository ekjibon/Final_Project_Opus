using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IDisciplinaryService
    {
        Task<bool> SaveDisciplinaryLog(DisciplinaryLog disciplinaryLog);
        Task<IEnumerable<DisciplinaryLog>> GetDisciplinaryLog();
        Task<DisciplinaryLog> GetDisciplinaryLogById(int id);
        Task<bool> DeleteDisciplinaryLogById(int id);
        Task<IEnumerable<DisciplinaryLog>> GetDisciplinaryLogByEmpId(int empId);
    }
}
