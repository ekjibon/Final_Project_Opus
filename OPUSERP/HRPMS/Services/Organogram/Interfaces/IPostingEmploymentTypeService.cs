using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Organogram.Interfaces
{
    public interface IPostingEmploymentTypeService
    {
        //Posting Type
        Task<bool> SavePostingType(PostingType postingType);
        Task<IEnumerable<PostingType>> GetAllPostingType();
        Task<PostingType> GetPostingTypeById(int id);
        Task<bool> DeletePostingTypeById(int id);

        //Employment Type
        Task<bool> SaveEmploymentType(EmploymentType employmentType);
        Task<IEnumerable<EmploymentType>> GetAllEmploymentType();
        Task<EmploymentType> GetEmploymentTypeById(int id);
        Task<bool> DeleteEmploymentTypeById(int id);
    }
}
