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
    public class OtherQualificationHeadService: IOtherQualificationHeadService
    {
        private readonly ERPDbContext _context;

        public OtherQualificationHeadService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOtherQualificationHead(OtherQualificationHead otherQualificationHead)
        {
            if (otherQualificationHead.Id != 0)
                _context.otherQualificationHeads.Update(otherQualificationHead);
            else
                _context.otherQualificationHeads.Add(otherQualificationHead);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OtherQualificationHead>> GetOtherQualificationHead()
        {
            return await _context.otherQualificationHeads.AsNoTracking().ToListAsync();
        }

        public async Task<OtherQualificationHead> GetOtherQualificationHeadById(int id)
        {
            return await _context.otherQualificationHeads.FindAsync(id);
        }

        public async Task<bool> DeleteOtherQualificationHeadById(int id)
        {
            _context.otherQualificationHeads.Remove(_context.otherQualificationHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
