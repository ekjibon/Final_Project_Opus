using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IHolidayService
    {
        Task<bool> SaveHoliday(Holiday holiday);
        Task<IEnumerable<Holiday>> GetAllHoliday();
        Task<Holiday> GetHolidayById(int id);
        Task<bool> DeleteHolidayById(int id);
    }
}
