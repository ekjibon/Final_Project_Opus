using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;

namespace CLUB.Services.MasterData
{
    public class ResultService : IResultService
    {
        private readonly ERPDbContext _context;

        public ResultService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetResultCheck(int id, string name)
        {
            var Result = await _context.results.Where(x => x.resultName == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
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
