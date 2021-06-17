using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Event.Models;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event
{
    public class EventInfoService : IEventInfoService
    {
        private readonly ERPDbContext _context;

        public EventInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveEventInformation(EventInformation eventInformation)
        {
            if (eventInformation.Id != 0)
                _context.eventInformations.Update(eventInformation);
            else
                _context.eventInformations.Add(eventInformation);
            await _context.SaveChangesAsync();
            return eventInformation.Id;
        }

        public async Task<IEnumerable<EventInformation>> GetEventInformation()
        {
            return await _context.eventInformations.AsNoTracking().ToListAsync();
        }

        public async Task<EventInformation> GetEventInformationById(int id)
        {
            return await _context.eventInformations.FindAsync(id);
        }

        public async Task<EventData> GetEventInformationByIdAndEmpId(int id ,int empId)
        {
            var eventInfo =  await _context.eventInformations.FindAsync(id);
            var Data = new EventData();
            Data.eventInformation = eventInfo;
            Data.isRegistered = _context.eventRegisters.Where(x => x.eventInformationId == eventInfo.Id && x.employeeId == empId).ToList().Count > 0 ? true : false;
            return Data;
        }

        public async Task<bool> DeleteEventInformationById(int id)
        {
            _context.eventInformations.Remove(_context.eventInformations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<List<EventData>> GetAllUpcomigEventDataByEmpID(int empId)
        {
            DateTime dt = DateTime.Now.Date;
            IEnumerable<EventInformation> eventInformations = await _context.eventInformations.Where(x => x.deadline >= dt).OrderBy(x=> x.deadline).AsNoTracking().ToListAsync();
            List<EventData> data = new List<EventData>();
            foreach (EventInformation eventInformation in eventInformations)
            {
                data.Add(new EventData
                {
                    eventInformation = eventInformation,
                    isRegistered = _context.eventRegisters.Where(x => x.eventInformationId == eventInformation.Id && x.employeeId == empId).ToList().Count > 0 ? true : false
                });
            }
            return data;
        }
    }
}
