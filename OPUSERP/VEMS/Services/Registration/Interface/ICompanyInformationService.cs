using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface ICompanyInformationService
    {
        Task<int> SaveCompanyInformation(CompanyInformation companyInformation);
        Task<IEnumerable<CompanyInformation>> GetCompanyInformation();
        Task<CompanyInformation> GetCompanyInformationById(int id);
        Task<CompanyInformation> GetCompanyInformationByRegId(int regId);
        Task<bool> DeleteCompanyInformationById(int id);
    }
}
