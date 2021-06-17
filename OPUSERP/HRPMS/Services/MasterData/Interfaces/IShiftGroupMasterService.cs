using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IShiftGroupMasterService
    {
        Task<bool> SaveShiftGroupMaster(ShiftGroupMaster shiftGroupMaster);
        Task<IEnumerable<ShiftGroupMaster>> GetAllShiftGroupMaster();
        Task<ShiftGroupMaster> GetShiftGroupMasterById(int id);
        Task<bool> DeleteShiftGroupMasterById(int id);
        Task<IEnumerable<ShiftGroupMaster>> UpdateShiftGroupId(string ShiftType, int? sbu, int? department, int? employeeInfoId, int? shiftGroup);
        Task<IEnumerable<ShiftGroupMaster>> UpdateShiftGroupIdForWages(string ShiftType, int? sbu, int? department, int? employeeInfoId, int? shiftGroup);
    }
}
