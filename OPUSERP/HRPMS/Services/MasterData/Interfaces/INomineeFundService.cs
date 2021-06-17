using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface INomineeFundService
    {
        Task<bool> SaveNomineeFund(NomineeFund nomineeFund);
        Task<IEnumerable<NomineeFund>> GetNomineeFund();
        Task<NomineeFund> GetNomineeFundById(int id);
        Task<bool> DeleteNomineeFundById(int id);
    }
}
