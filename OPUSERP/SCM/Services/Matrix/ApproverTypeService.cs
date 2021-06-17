using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix
{
    public class ApproverTypeService:IApproverTypeService
    {
        private readonly ERPDbContext _context;

        public ApproverTypeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveApproverType(ApproverType approverType)
        {
            if (approverType.Id != 0)
            {
                approverType.updatedBy = approverType.createdBy;
                approverType.updatedAt = DateTime.Now;
                _context.ApproverTypes.Update(approverType);
            }
            else
            {
                approverType.createdAt = DateTime.Now;
                _context.ApproverTypes.Add(approverType);
            }

            await _context.SaveChangesAsync();
            return approverType.Id;
        }

        public async Task<IEnumerable<ApproverType>> GetApproverTypeList()
        {
            return await _context.ApproverTypes.AsNoTracking().ToListAsync();
        }

        public async Task<ApproverType> GetApproverTypeById(int id)
        {
            return await _context.ApproverTypes.FindAsync(id);
        }

        public async Task<bool> DeleteApproverTypeById(int id)
        {
            _context.ApproverTypes.Remove(_context.ApproverTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
