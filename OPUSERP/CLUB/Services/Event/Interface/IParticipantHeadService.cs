using OPUSERP.CLUB.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event.Interface
{
   public interface IParticipantHeadService
    {
        Task<bool> SaveParticipantHead(ParticipantHead participantHead);
        Task<IEnumerable<ParticipantHead>> GetAllParticipantHead();
        Task<ParticipantHead> GetParticipantHeadById(int id);
        Task<bool> DeleteParticipantHeadById(int id);
    }
}
