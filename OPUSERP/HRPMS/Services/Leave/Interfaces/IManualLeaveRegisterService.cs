using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IManualLeaveRegisterService
    {
        Task<bool> SaveManualLeaveRegister(LeaveRegister leaveRegister);
        Task<IEnumerable<LeaveRegister>> GetAllManualLeaveRegister();
        Task<LeaveRegister> GetManualLeaveRegisterById(int id);
        Task<bool> DeleteManualLeaveRegisterById(int id);
    }
}
