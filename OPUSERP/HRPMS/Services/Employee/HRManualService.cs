using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class HRManualService : IHRManualService
    {
        private readonly ERPDbContext _context;

        public HRManualService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteHRManualAttachmentById(int id)
        {
            _context.hRManualAttachments.Remove(_context.hRManualAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRManualAttachment>> GetHRManualAttachment()
        {
            return await _context.hRManualAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<HRManualAttachment> GetHRManualAttachmentById(int id)
        {
            return await _context.hRManualAttachments.FindAsync(id);
        }

        public async Task<bool> SaveHRManualAttachment(HRManualAttachment hRManualAttachment)
        {
            if (hRManualAttachment.Id != 0)
                _context.hRManualAttachments.Update(hRManualAttachment);
            else
                _context.hRManualAttachments.Add(hRManualAttachment);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
