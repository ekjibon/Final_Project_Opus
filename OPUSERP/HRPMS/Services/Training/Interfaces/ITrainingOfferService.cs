using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingOfferService
    {
        Task<bool> SaveTrainingOffer(TrainingOffer trainingOffer);
        Task<IEnumerable<TrainingOffer>> GetAllTrainingOffer();
        Task<TrainingOffer> GetTrainingOfferId(int id);
        Task<bool> DeleteTrainingOfferId(int id);
    }
}
