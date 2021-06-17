using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.MasterData.Interfaces
{
    public interface IRegistrationTypeService
    {
        Task<bool> SaveRegistrationType(RegistrationType registrationType);
        Task<IEnumerable<RegistrationType>> GetAllRegistrationType();
        Task<RegistrationType> GetRegistrationTypeById(int id);
        Task<bool> DeleteRegistrationTypeById(int id);

        #region Procurement Source

        Task<IEnumerable<ProcurementSource>> GetAllProcurementSource();

        #endregion
    }
}
