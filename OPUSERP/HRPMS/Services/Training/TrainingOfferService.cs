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
    public class TrainingOfferService : ITrainingOfferService
    {
        private readonly ERPDbContext _context;

        public TrainingOfferService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTrainingOfferId(int id)
        {
            _context.trainingOffers.Remove(_context.trainingOffers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingOffer>> GetAllTrainingOffer()
        {
            return await _context.trainingOffers.AsNoTracking().ToListAsync();
        }

        public async Task<TrainingOffer> GetTrainingOfferId(int id)
        {
            return await _context.trainingOffers.FindAsync(id);
        }

        public async Task<bool> SaveTrainingOffer(TrainingOffer trainingOffer)
        {
            if (trainingOffer.Id != 0)
                _context.trainingOffers.Update(trainingOffer);
            else
                _context.trainingOffers.Add(trainingOffer);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
