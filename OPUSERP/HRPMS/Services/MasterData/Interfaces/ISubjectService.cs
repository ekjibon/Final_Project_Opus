using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ISubjectService
    {
        Task<bool> SaveSubject(Subject employeeType);
        Task<IEnumerable<Subject>> GetAllSubject();
        Task<Subject> GetSubjectById(int id);
        Task<bool> DeleteSubjectById(int id);
    }
}
