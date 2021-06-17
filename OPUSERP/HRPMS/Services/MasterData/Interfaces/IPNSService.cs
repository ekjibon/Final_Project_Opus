using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IPNSService
    {
        Task<bool> SavePNS(PNS pNS);
        Task<IEnumerable<PNS>> GetPNS();
        Task<PNS> GetPNSById(int id);
        Task<bool> DeletePNSById(int id);
    }
}
