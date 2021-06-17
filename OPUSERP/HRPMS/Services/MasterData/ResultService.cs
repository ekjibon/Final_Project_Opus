using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace OPUSERP.HRPMS.Services.MasterData
{
    public class ResultService : IResultService
    {
        private readonly ERPDbContext _context;

        public ResultService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveResult(Result result)
        {
            if(result.Id != 0)
                _context.results.Update(result);
            else 
                _context.results.Add(result);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Result>> GetAllResult()
        {
            return await _context.results.AsNoTracking().ToListAsync();
        }

        public async Task<Result> GetResultById(int id)
        {
            return await _context.results.FindAsync(id);
        }

        public async Task<bool> DeleteResultById(int id)
        {
            _context.results.Remove(_context.results.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

      

        
    }
}
