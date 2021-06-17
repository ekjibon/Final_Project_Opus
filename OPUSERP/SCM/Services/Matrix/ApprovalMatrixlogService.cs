using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix
{
    public class ApprovalMatrixlogService : IApprovalMatrixlogService
    {
        private readonly ERPDbContext _context;

        public ApprovalMatrixlogService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveApproverMatrixLog(ApprovalMatrixlog approvalLog)
        {
            if (approvalLog.Id != 0)
            {
                approvalLog.updatedBy = approvalLog.createdBy;
                approvalLog.updatedAt = DateTime.Now;
                _context.ApprovalMatriceslog.Update(approvalLog);
            }
            else
            {
                approvalLog.createdAt = DateTime.Now;
                _context.ApprovalMatriceslog.Add(approvalLog);
            }

            await _context.SaveChangesAsync();
            return approvalLog.Id;
        }

        public void SaveApproverMatrixLogList(List<ApprovalMatrixlog> approvalLogs)
        {
            
            _context.ApprovalMatriceslog.AddRange(approvalLogs);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ApprovalMatrixlog>> GetApproverMatrixLogList()
        {
            return await _context.ApprovalMatriceslog.AsNoTracking().ToListAsync();
        }

        public async Task<ApprovalMatrixlog> GetApproverMatrixLogById(int id)
        {
            return await _context.ApprovalMatriceslog.FindAsync(id);
        }

        
    }
}
