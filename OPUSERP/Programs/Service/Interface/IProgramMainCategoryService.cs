using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramMainCategoryService
    {
        Task<int> SaveProgramMainCategory(ProgramMainCategory programMainCategory);
        Task<IEnumerable<ProgramMainCategory>> GetProgramMainCategory();
        Task<ProgramMainCategory> GetProgramMainCategoryById(int id);
        Task<bool> DeleteProgramMainCategoryById(int id);
    }
}
