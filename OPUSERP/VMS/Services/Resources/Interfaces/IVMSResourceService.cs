using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Services.Resources.Interfaces
{
    public interface IVMSResourceService
    {
        #region Resource
        Task<int> SaveResource(VMSResource resource);
        Task<IEnumerable<VMSResource>> GetResource();
        Task<VMSResource> GetResourceById(int id);
        Task<bool> DeleteResourcesById(int id);
        #endregion
        


    }
}
