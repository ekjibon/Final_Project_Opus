using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.SOR;

namespace OPUSERP.VMS.Services.SOR.Interfaces
{
   public interface ISORService
    {
        #region SOR Master

        Task<int> SaveSORMaster(SORMaster sORMaster);

        Task<IEnumerable<SORMaster>> GetAllSORList();

        Task<SORMaster> GetSORListById(int Id);
        Task<IEnumerable<SORMaster>> GetSORListByRaiser(int userId);

        Task<SORMaster> DeleteSORById(int id);

        string GetSORNumber();

        #endregion

        #region SOR Details

        Task<int> SaveSORDetails(SORDetails sORDetails);
        Task<bool> SaveSORDetailsList(List<SORDetails> sORDetails);
        Task<IEnumerable<SORDetails>> GetSORDetailsInfoBySORId(int sorId);
        Task<IEnumerable<SORDetails>> GetSORDetailListByVendor(int Id);

        #endregion
    }
}
