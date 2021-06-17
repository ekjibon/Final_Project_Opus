using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IHRPMSOrganizationTypeService
    {
        Task<bool> SaveHRPMSOrganizationType(HRPMSOrganizationType hRPMSOrganizationType);
        Task<IEnumerable<HRPMSOrganizationType>> GetHRPMSOrganizationType();
        Task<HRPMSOrganizationType> GetHRPMSOrganizationTypeById(int id);
        Task<bool> DeleteHRPMSOrganizationTypeById(int id);
    }
}
