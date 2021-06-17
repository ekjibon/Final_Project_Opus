using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ISubjectService
    {
        Task<bool> SaveSubject(Subject employeeType);
        Task<IEnumerable<Subject>> GetAllSubject();
        Task<Subject> GetSubjectById(int id);
        Task<bool> DeleteSubjectById(int id);
        //for validation
        Task<int> GetSubjectCheck(int id, string name);
    }
}
