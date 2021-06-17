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
    public class HRPMSAttachmentService: IHRPMSAttachmentService
    {
        private readonly ERPDbContext _context;

        public HRPMSAttachmentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteHRPMSAttachmentById(int id)
        {
            _context.hRPMSAttachments.Remove(_context.hRPMSAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRPMSAttachment>> GetHRPMSAttachment()
        {
            return await _context.hRPMSAttachments.Include(x=>x.atttachmentCategory).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HRPMSAttachment>> GetHRPMSAttachmentByEmpId(int empId)
        {
            return await _context.hRPMSAttachments.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.atttachmentCategory).Include(x => x.atttachmentCategory.atttachmentGroup).AsNoTracking().ToListAsync();
        }

        public async Task<HRPMSAttachment> GetHRPMSAttachmentById(int id)
        {
            return await _context.hRPMSAttachments.FindAsync(id);
        }

        public async Task<bool> SaveHRPMSAttachment(HRPMSAttachment hRPMSAttachment)
        {
            if (hRPMSAttachment.Id != 0)
                _context.hRPMSAttachments.Update(hRPMSAttachment);
            else
                _context.hRPMSAttachments.Add(hRPMSAttachment);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
