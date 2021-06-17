using OPUSERP.CLUB.Data.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance.Interface
{
   public interface ICollectionService
    {
        Task<bool> SaveCollection(Collection collection);
        Task<IEnumerable<Collection>> GetCollection();
        Task<IEnumerable<Collection>> GetCollectionByTrMasterId(int id);
        Task<Collection> GetCollectionById(int id);
        Task<bool> DeleteCollectionById(int id);
    }
}
