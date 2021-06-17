using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData
{
    public class ParticipantTypeService: IParticipantTypeService
    {
        private readonly ApplicationDbContext _context;

        public ParticipantTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveParticipantType(ParticipantType participantType)
        {
            if (participantType.Id != 0)
                _context.participantTypes.Update(participantType);
            else
                _context.participantTypes.Add(participantType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ParticipantType>> GetAllParticipantType()
        {
            return await _context.participantTypes.AsNoTracking().ToListAsync();
        }

        public async Task<ParticipantType> GetParticipantTypeById(int id)
        {
            return await _context.participantTypes.FindAsync(id);
        }

        public async Task<bool> DeleteParticipantTypeById(int id)
        {
            _context.participantTypes.Remove(_context.participantTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
