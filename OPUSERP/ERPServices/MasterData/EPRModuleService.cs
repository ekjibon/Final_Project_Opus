using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData
{
    public class EPRModuleService: IERPModuleService
    {
        private readonly ERPDbContext _context;

        public EPRModuleService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveERPModule(ERPModule eRPModule)
        {
            if (eRPModule.Id != 0)
                _context.ERPModules.Update(eRPModule);
            else
                _context.ERPModules.Add(eRPModule);
            await _context.SaveChangesAsync();
            return eRPModule.Id;
        }

        public async Task<IEnumerable<ERPModule>> GetAllERPModule()
        {
            return await _context.ERPModules.ToListAsync();
        }

        public async Task<ERPModule> GetERPModuleById(int id)
        {
            return await _context.ERPModules.FindAsync(id);
        }

        public async Task<ERPModule> GetERPModuleByModuleName(string moduleName)
        {
            return await _context.ERPModules.Where(x => x.moduleName == moduleName).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteERPModuleById(int id)
        {
            _context.ERPModules.Remove(_context.ERPModules.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
