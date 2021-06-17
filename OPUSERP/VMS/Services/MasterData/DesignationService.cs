
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.VMS.Services.MasterData
{
    public class DesignationService : IDesignationService
    {
        private readonly ERPDbContext _context;

        public DesignationService(ERPDbContext context)
        {
            _context = context;
        }

        #region Country
        public async Task<IEnumerable<Designation>> GetAllDesignation()
        {
            return await _context.designations.AsNoTracking().ToListAsync();
        }

      
        #endregion

    
    }
}
