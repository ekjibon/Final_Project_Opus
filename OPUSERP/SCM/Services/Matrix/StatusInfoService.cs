using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix
{
    public class StatusInfoService : IStatusInfoService
    {
        private readonly ERPDbContext _context;

        public StatusInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveStatusInfo(StatusInfo statusInfo)
        {
            if (statusInfo.Id != 0)
                _context.StatusInfos.Update(statusInfo);
            else
                _context.StatusInfos.Add(statusInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StatusInfo>> GetAllStatusInfo()
        {
            return await _context.StatusInfos.AsNoTracking().ToListAsync();
        }

        public async Task<StatusInfo> GetStatusInfoById(int id)
        {
            return await _context.StatusInfos.FindAsync(id);
        }

        public async Task<bool> DeleteStatusInfoById(int id)
        {
            _context.StatusInfos.Remove(_context.StatusInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
