using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IResultService
    {
        Task<bool> SaveResult(Result orga);
        Task<IEnumerable<Result>> GetAllResult();
        Task<Result> GetResultById(int id);
        Task<bool> DeleteResultById(int id);
        //for validation
        Task<int> GetResultCheck(int id, string name);
    }
}
