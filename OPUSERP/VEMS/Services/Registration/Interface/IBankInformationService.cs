using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface IBankInformationService
    {
        Task<int> SaveBankInformation(BankInformation bankInformation);
        Task<IEnumerable<BankInformation>> GetBankInformation();
        Task<BankInformation> GetBankInformationById(int id);
        Task<IEnumerable<BankInformation>> GetBankInformationByRegId(int regId);
        Task<bool> DeleteBankInformationById(int id);
    }
}
