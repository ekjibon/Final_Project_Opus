using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramAttachmentService: IProgramAttachmentService
    {
        private readonly ERPDbContext _context;

        public ProgramAttachmentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramAttachment(ProgramAttachment programAttachment)
        {
            if (programAttachment.Id != 0)
            {
                programAttachment.updatedBy = programAttachment.createdBy;
                programAttachment.updatedAt = DateTime.Now;
                _context.programAttachments.Update(programAttachment);
            }
            else
            {
                programAttachment.createdAt = DateTime.Now;
                _context.programAttachments.Add(programAttachment);
            }
            await _context.SaveChangesAsync();
            return programAttachment.Id;
        }

        public async Task<IEnumerable<ProgramAttachment>> GetProgramAttachment()
        {
            return await _context.programAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramAttachment> GetProgramAttachmentById(int id)
        {
            return await _context.programAttachments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramAttachmentById(int id)
        {
            _context.programAttachments.Remove(_context.programAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
