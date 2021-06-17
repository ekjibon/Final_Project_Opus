using OPUSERP.Data;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services
{
    public class ClaimAttachmentService: IClaimAttachmentService
    {
        private readonly ERPDbContext _context;

        public ClaimAttachmentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveClaimAttachment(ClaimAttachment claimAttachment)
        {
            if (claimAttachment.Id != 0)
            {
                _context.ClaimAttachments.Update(claimAttachment);
            }
            else
            {
                _context.ClaimAttachments.Add(claimAttachment);
            }

            await _context.SaveChangesAsync();
            return claimAttachment.Id;
        }

    }
}
