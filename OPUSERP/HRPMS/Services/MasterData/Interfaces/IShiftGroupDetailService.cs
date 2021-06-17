using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IShiftGroupDetailService
    {
        Task<bool> SaveShiftGroupDetail(ShiftGroupDetail shiftGroupDetail);
        Task<IEnumerable<ShiftGroupDetail>> GetAllShiftGroupDetail();
        Task<ShiftGroupDetail> GetShiftGroupDetailById(int id);
        Task<bool> DeleteShiftGroupDetailById(int id);
    }
}
