
using System.Collections.Generic;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.Master;

namespace OPUSERP.VMS.Services.MasterData.Interfaces
{
    public interface IDesignationService
    {
       

        Task<IEnumerable<Designation>> GetAllDesignation();
       

    }
}
