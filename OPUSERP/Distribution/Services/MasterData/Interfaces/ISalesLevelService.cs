using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData.Interfaces
{
    public interface ISalesLevelService
    {
        Task<bool> DeleteSalesLevelById(int id);
        Task<IEnumerable<SalesLevel>> GetAllSalesLevel();
        Task<SalesLevel> GetSalesLevelById(int id);
        Task<bool> SaveSalesLevel(SalesLevel salesLevel);
        Task<IEnumerable<SalesLevel>> GetAllSalesLevelbyparentId(int Id);
        Task<IEnumerable<SalesLevel>> GetAllSalesLevelbyparentridId(int Id, int rid);
    }
}
