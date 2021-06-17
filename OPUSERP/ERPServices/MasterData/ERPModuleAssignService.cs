using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData
{
    public class ERPModuleAssignService: IERPModuleAssignService
    {
        private readonly ERPDbContext _context;

        public ERPModuleAssignService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveERPModuleAssign(ERPModuleAssign eRPModuleAssign)
        {
            if (eRPModuleAssign.Id != 0)
                _context.ERPModuleAssigns.Update(eRPModuleAssign);
            else
                _context.ERPModuleAssigns.Add(eRPModuleAssign);
            await _context.SaveChangesAsync();
            return eRPModuleAssign.Id;
        }

        public async Task<IEnumerable<ERPModuleAssign>> GetAllERPModuleAssign()
        {
            return await _context.ERPModuleAssigns.OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<ERPModuleAssign> GetERPModuleAssignById(int Id)
        {
            return await _context.ERPModuleAssigns.FindAsync(Id);
        }

        public async Task<bool> DeleteERPModuleAssignById(int id)
        {
            _context.ERPModuleAssigns.Remove(_context.ERPModuleAssigns.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
