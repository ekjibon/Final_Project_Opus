using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IHolidayService
    {
        Task<bool> SaveHoliday(Holiday holiday);
        Task<IEnumerable<Holiday>> GetAllHoliday();
        Task<Holiday> GetHolidayById(int id);
        Task<bool> DeleteHolidayById(int id);
    }
}
