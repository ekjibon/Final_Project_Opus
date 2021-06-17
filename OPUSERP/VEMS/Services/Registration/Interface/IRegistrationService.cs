using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface IRegistrationService
    {
        Task<int> SaveRegistrationForm(RegistrationForm registrationForm);
        Task<IEnumerable<RegistrationForm>> GetRegistrationForm();
        Task<RegistrationForm> GetRegistrationFormById(int id);
        Task<bool> DeleteRegistrationFormById(int id);
        Task<IEnumerable<RegistrationForm>> GetRegistrationFormByApproveType(int Type);
        Task<int> UpdateRegistrationForm(int? Id, int? appType);
        Task<RegistrationForm> GetLoginInfoByLoginIdAndPassword(string loginId, string password);
        Task<int> UpdateGeneralInformation(RegistrationForm registrationForm);
        Task<int> UpdateRegistrationFormSupplierId(int? Id, int? supplierId);
    }
}
