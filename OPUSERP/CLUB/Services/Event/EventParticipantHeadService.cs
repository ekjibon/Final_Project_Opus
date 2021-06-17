using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event
{
    public class EventParticipantHeadService: IEventParticipantHeadService
    {
        private readonly ERPDbContext _context;

        public EventParticipantHeadService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveEventPerticipantHead(EventPerticipantHead eventPerticipantHead)
        {
            if (eventPerticipantHead.Id != 0)
                _context.eventPerticipantHeads.Update(eventPerticipantHead);
            else
                _context.eventPerticipantHeads.Add(eventPerticipantHead);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventPerticipantHead>> GetAllEventPerticipantHead()
        {
            return await _context.eventPerticipantHeads.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventPerticipantHead>> GetAllEventPerticipantHeadByEventId(int eventId)
        {
            return await _context.eventPerticipantHeads.Where(x=>x.eventInformationId==eventId).Include(x=>x.eventInformation).Include(x => x.participantHead).AsNoTracking().ToListAsync();
        }

        public async Task<EventPerticipantHead> GetEventPerticipantHeadById(int id)
        {
            return await _context.eventPerticipantHeads.FindAsync(id);
        }

        public async Task<bool> DeleteEventPerticipantHeadById(int id)
        {
            _context.eventPerticipantHeads.Remove(_context.eventPerticipantHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
