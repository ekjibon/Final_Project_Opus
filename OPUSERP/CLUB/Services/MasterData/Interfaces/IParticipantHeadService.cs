using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
{
   public interface IParticipantHeadService
    {
        Task<bool> SaveParticipantHead(ParticipantHead participantHead);
        Task<IEnumerable<ParticipantHead>> GetAllParticipantHead();
        Task<ParticipantHead> GetParticipantHeadById(int id);
        Task<bool> DeleteParticipantHeadById(int id);
    }
}
