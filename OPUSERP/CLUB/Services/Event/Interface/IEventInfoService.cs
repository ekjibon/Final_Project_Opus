using OPUSERP.Areas.Event.Models;
using OPUSERP.CLUB.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event.Interface
{
   public interface IEventInfoService
    {
        Task<int> SaveEventInformation(EventInformation eventInformation);
        Task<IEnumerable<EventInformation>> GetEventInformation();
        Task<EventInformation> GetEventInformationById(int id);
        Task<EventData> GetEventInformationByIdAndEmpId(int id, int empId);
        Task<bool> DeleteEventInformationById(int id);

        //By Jaggesher
        Task<List<EventData>> GetAllUpcomigEventDataByEmpID(int empId);
    }
}
