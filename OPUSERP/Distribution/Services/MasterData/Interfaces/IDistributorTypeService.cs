using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData.Interfaces
{
    public interface IDistributorTypeService
    {
        Task<bool> DeleteDistributorTypeById(int id);
        Task<IEnumerable<DistributorType>> GetAllDistributorType();
        Task<DistributorType> GetDistributorTypeById(int id);
        Task<bool> SaveDistributorType(DistributorType distributorType);
    }
}
