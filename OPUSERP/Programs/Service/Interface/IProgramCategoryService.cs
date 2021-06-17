using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramCategoryService
    {
        Task<int> SaveProgramCategory(ProgramCategory programCategory);
        Task<IEnumerable<ProgramCategory>> GetProgramCategory();
        Task<ProgramCategory> GetProgramCategoryById(int id);
        Task<bool> DeleteProgramCategoryById(int id);
        Task<IEnumerable<ProgramCategory>> GetProgramCategoryByCatId(int id);
    }
}
