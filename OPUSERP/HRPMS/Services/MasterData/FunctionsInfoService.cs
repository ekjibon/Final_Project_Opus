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
    public class FunctionsInfoService : IFunctionsInfoService
    {
        private readonly ERPDbContext _context;

        public FunctionsInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FunctionInfo>> GetFunctionInfo()
        {
            return await _context.FunctionInfos.Include(x=>x.company).AsNoTracking().ToListAsync();
        }

        public async Task<FunctionInfo> GetFunctionInfoById(int id)
        {
            return await _context.FunctionInfos.FindAsync(id);
        }

       
        public async Task<bool> SaveFunctionInfo(FunctionInfo functionInfo)
        {
            if (functionInfo.Id != 0)
                _context.FunctionInfos.Update(functionInfo);
            else
                _context.FunctionInfos.Add(functionInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteFunctionInfoById(int id)
        {
            _context.FunctionInfos.Remove(_context.FunctionInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
