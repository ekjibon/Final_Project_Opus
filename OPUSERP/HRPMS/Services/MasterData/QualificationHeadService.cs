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
    public class QualificationHeadService: IQualificationHeadService
    {
        private readonly ERPDbContext _context;

        public QualificationHeadService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveQualificationHead(QualificationHead qualificationHead)
        {
            if (qualificationHead.Id != 0)
                _context.qualificationHeads.Update(qualificationHead);
            else
                _context.qualificationHeads.Add(qualificationHead);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QualificationHead>> GetQualificationHead()
        {
            return await _context.qualificationHeads.AsNoTracking().ToListAsync();
        }

        public async Task<QualificationHead> GetQualificationHeadById(int id)
        {
            return await _context.qualificationHeads.FindAsync(id);
        }

        public async Task<bool> DeleteQualificationHeadById(int id)
        {
            _context.qualificationHeads.Remove(_context.qualificationHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
