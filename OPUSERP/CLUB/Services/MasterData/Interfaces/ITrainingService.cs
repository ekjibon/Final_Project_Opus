using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ITrainingService
    {
        Task<bool> SaveTrainingCategory(TrainingCategory trainingCategory);
        Task<IEnumerable<TrainingCategory>> GetTrainingCategories();
        Task<TrainingCategory> GetTrainingCategoryById(int id);
        Task<bool> DeleteTrainingCategoryById(int id);

        Task<bool> SaveTrainingInstitute(TrainingInstitute trainingInstitute);
        Task<IEnumerable<TrainingInstitute>> GetTrainingInstitute();
        Task<TrainingInstitute> GetTrainingInstituteById(int id);
        Task<bool> DeleteTrainingInstituteById(int id);

    }
}
