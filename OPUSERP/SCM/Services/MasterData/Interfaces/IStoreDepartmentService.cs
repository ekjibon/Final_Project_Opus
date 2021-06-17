using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData.Interfaces
{
   public interface IStoreDepartmentService
    {
        Task<int> SaveDepartmentType(StoreDepartmentType storeDepartmentType);
        void UpdateDepartmentType(StoreDepartmentType storeDepartmentType);
        Task<IEnumerable<StoreDepartmentType>> GetDeartmentTypeList();
        Task<StoreDepartmentType> GetDeartmentTypeById(int id);
        Task<bool> DeleteDeartmentType(int id);
       
    }
}
