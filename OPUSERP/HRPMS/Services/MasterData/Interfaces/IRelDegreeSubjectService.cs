using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IRelDegreeSubjectService
    {
        Task<bool> SaveDegreeSubject(RelDegreeSubject relDegreeSubject);
        Task<IEnumerable<RelDegreeSubject>> GetAllDegreeSubject();
        Task<RelDegreeSubject> GetDegreeSubjectById(int id);
        Task<bool> DeleteDegreeSubjectById(int id);
        Task<IEnumerable<RelDegreeSubject>> GetSubjectByDegreeId(int DegId);
    }
}
