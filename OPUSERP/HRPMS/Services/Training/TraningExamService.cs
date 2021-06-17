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
    public class TraningExamService: ITraningExamService
    {
        private readonly ERPDbContext _context;

        public TraningExamService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTrainingExamId(int id)
        {
            _context.trainingExams.Remove(_context.trainingExams.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingExam>> GetAllTrainingExam()
        {
            return await _context.trainingExams.Include(x => x.trainingInfo).Include(x => x.trainer).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingExam> GetTrainingExamId(int id)
        {
            return await _context.trainingExams.FindAsync(id);
        }

        public async Task<bool> SaveTrainingExam(TrainingExam trainingExam)
        {
            if (trainingExam.Id != 0)
                _context.trainingExams.Update(trainingExam);
            else
                _context.trainingExams.Add(trainingExam);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
