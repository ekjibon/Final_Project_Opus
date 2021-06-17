using OPUSERP.Areas.Event.Models;
using OPUSERP.CLUB.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event.Interface
{
   public interface IEventRegisterService
    {
        Task<bool> SaveEventRegister(EventRegister eventRegister);
        Task<IEnumerable<EventRegister>> GetEventRegister();
        Task<IEnumerable<EventRegister>> GetEventRegisterByEmpId(int empId);
        Task<IEnumerable<EventRegister>> GetEventRegisterByEventIdandEmpId(int id, int empId);
        Task<IEnumerable<EventRegister>> GetEventRegisterByEventId(int eventId);
        Task<EventRegister> GetEventRegisterById(int id);
        Task<bool> DeleteEventRegisterById(int id);

        //perticipants Summary
        Task<IEnumerable<EventParticipants>> GetEventRegisterParticipantsSummary(int eventId);
    }
}
