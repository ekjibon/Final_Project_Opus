using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IDegreeService
    {
        Task<bool> SaveDegree(Degree degree);
        Task<IEnumerable<Degree>> GetAllDegree();
        Task<Degree> GetDegreeById(int id);
        Task<IEnumerable<Degree>> GetDegreeByLOEId(int loEId);
        Task<bool> DeleteDegreeById(int id);

        //for validation 
        Task<int> GetDegreeCheck(int id, string name, int leoId);
    }
}
