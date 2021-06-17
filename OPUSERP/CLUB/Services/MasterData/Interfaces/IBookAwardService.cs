using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IBookAwardService
    {
        Task<bool> SaveBookCategory(BookCategory bookCategory);
        Task<IEnumerable<BookCategory>> GetBookCategory();
        Task<BookCategory> GetBookCategoryById(int id);
        Task<bool> DeleteBookCategoryById(int id);

        Task<bool> SaveAwardInfo(Award award);
        Task<IEnumerable<Award>> GetAwardInfo();
        Task<Award> GetAwardInfoById(int id);
        Task<bool> DeleteAwardInfoById(int id);

        //for validation
        Task<int> GetAwardCheck(int id, string name);

    }
}
