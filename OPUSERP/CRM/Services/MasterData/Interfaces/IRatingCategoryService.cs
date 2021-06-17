using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IRatingCategoryService
    {
        Task<bool> SaveRatingCategory(RatingCategory agreementCategory);
        Task<IEnumerable<RatingCategory>> GetAllRatingCategory();
        Task<RatingCategory> GetRatingCategoryById(int id);
        Task<bool> DeleteRatingCategoriesById(int id);

    }
}
