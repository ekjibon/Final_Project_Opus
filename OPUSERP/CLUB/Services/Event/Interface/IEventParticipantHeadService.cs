using OPUSERP.CLUB.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event.Interface
{
   public interface IEventParticipantHeadService
    {
        Task<bool> SaveEventPerticipantHead(EventPerticipantHead eventPerticipantHead);
        Task<IEnumerable<EventPerticipantHead>> GetAllEventPerticipantHead();
        Task<IEnumerable<EventPerticipantHead>> GetAllEventPerticipantHeadByEventId(int eventId);
        Task<EventPerticipantHead> GetEventPerticipantHeadById(int id);
        Task<bool> DeleteEventPerticipantHeadById(int id);
    }
}
