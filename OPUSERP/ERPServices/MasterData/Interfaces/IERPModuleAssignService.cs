using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IERPModuleAssignService
    {
        Task<int> SaveERPModuleAssign(ERPModuleAssign eRPModuleAssign);

        Task<IEnumerable<ERPModuleAssign>> GetAllERPModuleAssign();

        Task<ERPModuleAssign> GetERPModuleAssignById(int id);

        Task<bool> DeleteERPModuleAssignById(int id);
    }
}
