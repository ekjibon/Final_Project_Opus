using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IRlnSBUPNSService
    {
        Task<bool> SaveRlnSBUPNS(RlnSBUPNS rlnSBUPNS);
        Task<IEnumerable<RlnSBUPNS>> GetRlnSBUPNS();
        Task<RlnSBUPNS> GetRlnSBUPNSById(int id);
        Task<bool> DeleteRlnSBUPNSById(int id);
        Task<IEnumerable<RlnSBUPNS>> GetRlnSBUPNSBySBUId(int sbuId);
        Task<IEnumerable<RlnSBUPNS>> GetRlnSBUPNSByPNSId(int pnsId);
    }
}
