using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration
{
    public class VendorAttachmentService: IVendorAttachmentService
    {
        private readonly ERPDbContext _context;

        public VendorAttachmentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveVendorAttachment(VendorAttachment vendorAttachment)
        {
            if (vendorAttachment.Id != 0)
            {
                vendorAttachment.updatedBy = vendorAttachment.createdBy;
                vendorAttachment.updatedAt = DateTime.Now;
                _context.vendorAttachments.Update(vendorAttachment);
            }
            else
            {
                vendorAttachment.createdAt = DateTime.Now;
                _context.vendorAttachments.Add(vendorAttachment);
            }
            await _context.SaveChangesAsync();
            return vendorAttachment.Id;
        }

        public async Task<IEnumerable<VendorAttachment>> GetVendorAttachment()
        {
            return await _context.vendorAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<VendorAttachment> GetVendorAttachmentById(int id)
        {
            return await _context.vendorAttachments.Where(x => x.Id == id).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VendorAttachment>> GetVendorAttachmentByRegId(int regId)
        {
            return await _context.vendorAttachments.Where(x => x.registrationFormId == regId).Include(x => x.registrationForm).ToListAsync();
        }

        public async Task<bool> DeleteVendorAttachmentById(int id)
        {
            _context.vendorAttachments.Remove(_context.vendorAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
