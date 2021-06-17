using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service.Interface
{
   public interface IUnitOfTakaService
    {
        Task<int> SaveUnitOfTaka(UnitOfTaka unitOfTaka);
        Task<IEnumerable<UnitOfTaka>> GetUnitOfTaka();
        Task<UnitOfTaka> GetUnitOfTakaById(int id);
        Task<bool> DeleteUnitOfTakaById(int id);
        Task<int> UpdateUnitOfTakaStatus(UnitOfTaka unitOfTaka);
    }
}
