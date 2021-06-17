using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
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
    }
}
