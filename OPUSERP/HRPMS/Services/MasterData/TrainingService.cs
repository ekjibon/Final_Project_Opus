using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class TrainingService : ITrainingService
    {
        private readonly ERPDbContext _context;

        public TrainingService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTrainingCategoryById(int id)
        {
            _context.trainingCategories.Remove(_context.trainingCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingCategory>> GetTrainingCategories()
        {
            return await _context.trainingCategories.AsNoTracking().ToListAsync();
        }

        public async Task<TrainingCategory> GetTrainingCategoryById(int id)
        {
            return await _context.trainingCategories.FindAsync(id);
        }

        public async Task<bool> SaveTrainingCategory(TrainingCategory trainingCategory)
        {
            if(trainingCategory.Id != 0)
                _context.trainingCategories.Update(trainingCategory);
            else
                _context.trainingCategories.Add(trainingCategory);
            return 1 == await _context.SaveChangesAsync();
        }
        //Training Institute
        public async Task<bool> DeleteTrainingInstituteById(int id)
        {
            _context.trainingInstitutes.Remove(_context.trainingInstitutes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingInstitute>> GetTrainingInstitute()
        {
            return await _context.trainingInstitutes.AsNoTracking().ToListAsync();
        }

        public async Task<TrainingInstitute> GetTrainingInstituteById(int id)
        {
            return await _context.trainingInstitutes.FindAsync(id);
        }

        public async Task<bool> SaveTrainingInstitute(TrainingInstitute trainingInstitute)
        {
            if(trainingInstitute.Id != 0)
                _context.trainingInstitutes.Update(trainingInstitute);
            else
                _context.trainingInstitutes.Add(trainingInstitute);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
