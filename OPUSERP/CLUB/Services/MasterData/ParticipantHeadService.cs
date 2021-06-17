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
    public class ParticipantHeadService: IParticipantHeadService
    {
        private readonly ApplicationDbContext _context;

        public ParticipantHeadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveParticipantHead(ParticipantHead participantHead)
        {
            if (participantHead.Id != 0)
                _context.participantHeads.Update(participantHead);
            else
                _context.participantHeads.Add(participantHead);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ParticipantHead>> GetAllParticipantHead()
        {
            return await _context.participantHeads.AsNoTracking().ToListAsync();
        }

        public async Task<ParticipantHead> GetParticipantHeadById(int id)
        {
            return await _context.participantHeads.FindAsync(id);
        }

        public async Task<bool> DeleteParticipantHeadById(int id)
        {
            _context.participantHeads.Remove(_context.participantHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
