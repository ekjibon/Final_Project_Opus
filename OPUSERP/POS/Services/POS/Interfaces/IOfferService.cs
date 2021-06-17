using OPUSERP.POS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
    public interface IOfferService
    {
        #region Offer Master

        Task<int> SaveOfferMaster(OfferMaster offerMaster);
        Task<IEnumerable<OfferMaster>> GetOfferMaster();
        Task<OfferMaster> GetOfferMasterById(int id);
        Task<bool> DeleteOfferMasterById(int id);

        #endregion

        #region Offer Details

        Task<bool> SaveOfferDetails(OfferDetails offerDetails);
        Task<IEnumerable<OfferDetails>> GetOfferDetails();
        Task<OfferDetails> GetOfferDetailsById(int id);
        Task<bool> DeleteOfferDetailsById(int id);
        Task<bool> DeleteOfferDetailsByMasterId(int masterId);
        Task<IEnumerable<OfferDetails>> GetOfferDetailsByMasterId(int masterId);
        Task<IEnumerable<OfferDetails>> GetOfferDetailsByspecId(int specid);

        #endregion
    }
}
