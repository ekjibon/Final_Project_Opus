using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Event.Models;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event
{
    public class EventRegisterService: IEventRegisterService
    {
        private readonly ERPDbContext _context;

        public EventRegisterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveEventRegister(EventRegister eventRegister)
        {
            if (eventRegister.Id != 0)
                _context.eventRegisters.Update(eventRegister);
            else
                _context.eventRegisters.Add(eventRegister);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventRegister>> GetEventRegister()
        {
            return await _context.eventRegisters.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventParticipants>> GetEventRegisterParticipantsSummary(int eventId)
        {
            IEnumerable<ParticipantHead> EventParticipants   =  await _context.participantHeads.AsNoTracking().ToListAsync();

            List<EventParticipants> data = new List<EventParticipants>();
            foreach (ParticipantHead participantHead in EventParticipants)
            {
                data.Add(new EventParticipants
                {
                    participantHead = participantHead,
                    count = _context.eventRegisters.Where(x => x.eventInformationId == eventId && x.eventPerticipantHead.participantHeadId == participantHead.Id).Sum(s=>s.count),
                });
            }
            return data;
        }

        public async Task<IEnumerable<EventRegister>> GetEventRegisterByEmpId(int empId)
        {
            return await _context.eventRegisters.Where(x=>x.employeeId == empId).Include(x=>x.eventInformation).GroupBy(x=>x.eventInformationId).Select(x=>x.FirstOrDefault()).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventRegister>> GetEventRegisterByEventIdandEmpId(int id, int empId)
        {
            return await _context.eventRegisters.Where(x=>x.employeeId == empId).Where(x=>x.eventInformationId == id).Include(x=>x.eventInformation).Include(x => x.employee).Include(x => x.eventPerticipantHead.participantHead).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventRegister>> GetEventRegisterByEventId(int eventId)
        {
            return await _context.eventRegisters.Where(x=>x.eventInformationId == eventId).Include(x=>x.employee).Include(x => x.eventInformation).Include(x => x.eventPerticipantHead.participantHead).AsNoTracking().ToListAsync();
        }

        public async Task<EventRegister> GetEventRegisterById(int id)
        {
            return await _context.eventRegisters.FindAsync(id);
        }

        public async Task<bool> DeleteEventRegisterById(int id)
        {
            _context.eventRegisters.Remove(_context.eventRegisters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
