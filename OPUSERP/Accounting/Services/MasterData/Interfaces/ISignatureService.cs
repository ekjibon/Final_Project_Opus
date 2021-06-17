using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Areas.Accounting.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData.Interfaces
{
   public interface ISignatureService
    {
        #region SignatureType
        Task<IEnumerable<SignatureType>> GetALLSignatureType();
        Task<SignatureType> GetSignatureTypeById(int id);
        Task<bool> SaveSignatureType(SignatureType signatureType);
        Task<bool> DeleteSignatureTypeById(int id);

        #endregion
        #region Signature
        Task<IEnumerable<Signature>> GetALLSignature();
        Task<Signature> GetSignatureById(int id);
        Task<bool> SaveSignature(Signature signature);
        Task<bool> DeleteSignatureById(int id);
        Task<GetSignatureViewModel> GetSignatureByType(int? signatureTypeId);

        #endregion
    }
}
