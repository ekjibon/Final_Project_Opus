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
    public class ProgramAddressService : IProgramAddressService
    {
        private readonly ERPDbContext _context;

        public ProgramAddressService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramAddress(ProgramAddress  programAddress)
        {
            if (programAddress.Id != 0)
            {
                programAddress.updatedBy = programAddress.createdBy;
                programAddress.updatedAt = DateTime.Now;
                _context.programAddresses.Update(programAddress);
            }
            else
            {
                programAddress.createdAt = DateTime.Now;
                _context.programAddresses.Add(programAddress);
            }
            await _context.SaveChangesAsync();
            return programAddress.Id;
        }

        public async Task<IEnumerable<ProgramAddress>> GetProgramAddress()
        {
            return await _context.programAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramAddress> GetProgramAddressById(int id)
        {
            return await _context.programAddresses.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramAddressById(int id)
        {
            _context.programAddresses.Remove(_context.programAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
