using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IMembershipLanguageService
    {
        Task<bool> SaveMembershipInfo(Membership membership);
        Task<IEnumerable<Membership>> GetMembershipInfo();
        Task<Membership> GetMembershipById(int id);
        Task<bool> DeleteMembershipById(int id);

        Task<bool> SaveLanguageInfo(Language language);
        Task<IEnumerable<Language>> GetLanguageInfo();
        Task<Language> GetLanguageInfoById(int id);
        Task<bool> DeleteLanguageInfoById(int id);
        //for validation
        Task<int> GetLanguageCheck(int id, string name);
    }
}
