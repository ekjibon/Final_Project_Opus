using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Agreement;

namespace OPUSERP.VMS.Services.Agreement.Interfaces
{
    public interface IVMSAgreementService
    {
        #region Agreement Cost Head
        Task<int> SaveCostHead(AgreementCostHead costHead);
        Task<IEnumerable<AgreementCostHead>> GetAllCostHead();
        Task<bool> DeleteCostHeadById(int id);
        #endregion

        #region Agreement Information

        Task<int> SaveAgreementInformation(AgreementInformation agreementInformation);
        Task<IEnumerable<AgreementInformation>> GetAllAgreementInformation();
        Task<AgreementInformation> GetAgreementInformationById(int id);
        IQueryable<AgreementInformation> GetAgreementInformationByVehicleId(int vehicleId);
        Task<bool> DeleteAgreementInformationId(int masterId);

        #endregion


        #region Agreement Cost Information

        Task<int> SaveAgreementCostInformation(AgreementCostInformation costInformation);
        Task<AgreementCostInformation> GetAgreementCostInformationById(int id);
        IQueryable<AgreementCostInformation> GetAgreementCostInformationByVehicleId(int vehicleId);
        IQueryable<AgreementCostInformation> GetAgreementCostInformationByAgreementId(int agreementId);
        Task<bool> DeleteAgreementCostInformationByAgreementId(int agreeId);

        #endregion
    }
}
