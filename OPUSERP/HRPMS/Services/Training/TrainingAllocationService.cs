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
    public class TrainingAllocationService : ITrainingAllocationService
    {
        private readonly ERPDbContext _context;

        public TrainingAllocationService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeletetrainingAllocationById(int id)
        {
            _context.trainingAllocations.Remove(_context.trainingAllocations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingAllocation>> GetAlltrainingAllocation()
        {
            return await _context.trainingAllocations.Include(x=>x.employee).Include(x => x.trainingInfo).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingAllocation> GettrainingAllocationById(int id)
        {
            return await _context.trainingAllocations.FindAsync(id);
        }

        public async Task<bool> SaveTrainingAllocationInfo(TrainingAllocation trainingAllocation)
        {
            if (trainingAllocation.Id != 0)
                _context.trainingAllocations.Update(trainingAllocation);
            else
                _context.trainingAllocations.Add(trainingAllocation);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
