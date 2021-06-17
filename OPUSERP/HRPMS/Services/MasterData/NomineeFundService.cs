using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class NomineeFundService: INomineeFundService
    {
        private readonly ERPDbContext _context;

        public NomineeFundService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveNomineeFund(NomineeFund nomineeFund)
        {
            if (nomineeFund.Id != 0)
                _context.nomineeFunds.Update(nomineeFund);
            else
                _context.nomineeFunds.Add(nomineeFund);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NomineeFund>> GetNomineeFund()
        {
            return await _context.nomineeFunds.AsNoTracking().ToListAsync();
        }

        public async Task<NomineeFund> GetNomineeFundById(int id)
        {
            return await _context.nomineeFunds.FindAsync(id);
        }

        public async Task<bool> DeleteNomineeFundById(int id)
        {
            _context.nomineeFunds.Remove(_context.nomineeFunds.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
