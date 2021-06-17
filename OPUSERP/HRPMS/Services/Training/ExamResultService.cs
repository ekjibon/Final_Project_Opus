using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class ExamResultService : IExamResultService
    {
        private readonly ERPDbContext _context;

        public ExamResultService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteExamResultId(int id)
        {
            _context.examResults.Remove(_context.examResults.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExamResult>> GetAllExamResult()
        {
            return await _context.examResults.Include(x => x.trainingExam).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<ExamResult> GetExamResultId(int id)
        {
            return await _context.examResults.FindAsync(id);
        }

        public async Task<bool> SaveExamResult(ExamResult examResult)
        {
            if (examResult.Id != 0)
                _context.examResults.Update(examResult);
            else
                _context.examResults.Add(examResult);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
