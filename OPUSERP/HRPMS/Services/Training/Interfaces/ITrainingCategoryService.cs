using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingCategoryService
    {
        Task<bool> SaveModuleTrainingCategory(ModuleTrainingCategory moduleTtrainingCategory);
        Task<IEnumerable<ModuleTrainingCategory>> GetAllModuleTrainingCategory();
        Task<ModuleTrainingCategory> GetModuleTrainingCategoryId(int id);
        Task<bool> DeleteModuleTrainingCategoryId(int id);
    }
}
