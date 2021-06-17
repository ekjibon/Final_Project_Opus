using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
{
   public interface IParticipantTypeService
    {
        Task<bool> SaveParticipantType(ParticipantType participantType);
        Task<IEnumerable<ParticipantType>> GetAllParticipantType();
        Task<ParticipantType> GetParticipantTypeById(int id);
        Task<bool> DeleteParticipantTypeById(int id);
    }
}
