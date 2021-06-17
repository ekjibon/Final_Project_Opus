using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramAttendeeService : IProgramAttendeeService
    {
        private readonly ERPDbContext _context;

        public ProgramAttendeeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramAttendee(ProgramPeopleAttendee  programAttendee)
        {
            if (programAttendee.Id != 0)
            {
                programAttendee.updatedBy = programAttendee.createdBy;
                programAttendee.updatedAt = DateTime.Now;
                _context.programAttendees.Update(programAttendee);
            }
            else
            {
                programAttendee.createdAt = DateTime.Now;
                _context.programAttendees.Add(programAttendee);
            }
            await _context.SaveChangesAsync();
            return programAttendee.Id;
        }

        public async Task<IEnumerable<ProgramPeopleAttendee>> GetProgramAttendee()
        {
            return await _context.programAttendees.Include(x=>x.programMaster).Include(x=>x.programActivity).Include(x=>x.gender).Include(x=>x.peopleType).Include(x=>x.benificiaryType).Include(x=>x.benificiaryActiveType).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramPeopleAttendee> GetProgramAttendeeById(int id)
        {
            return await _context.programAttendees.Include(x => x.programMaster).Include(x => x.programActivity).Include(x => x.gender).Include(x => x.peopleType).Include(x => x.benificiaryType).Include(x => x.benificiaryActiveType).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramAttendeeById(int id)
        {
            _context.programAttendees.Remove(_context.programAttendees.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BenificiaryType>> GetBenificiaryType()
        {
            return await _context.benificiaryTypes.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<IdType>> GetIdType()
        {
            return await _context.idTypes.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<DateRange>> GetDateRange()
        {
            return await _context.dateRanges.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Gender>> GetGender()
        {
            return await _context.genders.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PeopleType>> GetPeopleType()
        {
            return await _context.peopleTypes.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BenificiaryActiveType>> GetBenificiaryActiveType()
        {
            return await _context.benificiaryActiveTypes.AsNoTracking().ToListAsync();
        }
    }
}
