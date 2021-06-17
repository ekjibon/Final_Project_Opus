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
    public class EvaluationFectorsService : IEvaluationFectorsService
    {
        private readonly ERPDbContext _context;

        public EvaluationFectorsService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteEvaluationFectorsId(int id)
        {
            _context.evaluationFectors.Remove(_context.evaluationFectors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EvaluationFectors>> GetAllEvaluationFectors()
        {
            return await _context.evaluationFectors.AsNoTracking().ToListAsync();
        }

        public async Task<EvaluationFectors> GetEvaluationFectorsId(int id)
        {
            return await _context.evaluationFectors.FindAsync(id);
        }

        public async Task<bool> SaveEvaluationFectors(EvaluationFectors dailyAttendance)
        {
            if (dailyAttendance.Id != 0)
                _context.evaluationFectors.Update(dailyAttendance);
            else
                _context.evaluationFectors.Add(dailyAttendance);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
