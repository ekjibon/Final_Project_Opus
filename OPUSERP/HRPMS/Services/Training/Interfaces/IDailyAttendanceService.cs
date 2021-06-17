using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface IDailyAttendanceService
    {
        Task<bool> SaveDailyAttendance(SessionAttendance dailyAttendance);
        Task<IEnumerable<SessionAttendance>> GetAllDailyAttendance();
        Task<SessionAttendance> GetDailyAttendanceId(int id);
        Task<bool> DeleteDailyAttendanceId(int id);
    }
}
