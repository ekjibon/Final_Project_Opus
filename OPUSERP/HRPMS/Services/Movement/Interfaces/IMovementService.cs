using OPUSERP.HRPMS.Data.Entity.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Movement
{
    public interface IMovementService
    {
        Task<bool> SaveMovement(MovementTracking movementTracking);
        Task<bool> UpdateMovement(int id, MovementTracking movementTracking);
        Task<IEnumerable<MovementTracking>> GetAllMovementTracking(string empcode);
        Task<IEnumerable<MovementTracking>> GetAllMovementByEmpIdAndDateRange(string empcode, DateTime? from, DateTime? to);
    }
}
