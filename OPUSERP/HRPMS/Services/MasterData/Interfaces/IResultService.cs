using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IResultService
    {
        Task<bool> SaveResult(Result orga);
        Task<IEnumerable<Result>> GetAllResult();
        Task<Result> GetResultById(int id);
        Task<bool> DeleteResultById(int id);
    }
}
