using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IHRDonerSevice
    {
        Task<bool> SaveHRDoner(HRDoner hRDoner);
        Task<IEnumerable<HRDoner>> GetHRDoner();
        Task<HRDoner> GetHRDonerById(int id);
        Task<bool> DeleteHRDonerById(int id);
    }
}
