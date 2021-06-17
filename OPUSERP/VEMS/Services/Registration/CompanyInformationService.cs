using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration
{
    public class CompanyInformationService: ICompanyInformationService
    {
        private readonly ERPDbContext _context;

        public CompanyInformationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveCompanyInformation(CompanyInformation companyInformation)
        {
            if (companyInformation.Id != 0)
            {
                companyInformation.updatedBy = companyInformation.createdBy;
                companyInformation.updatedAt = DateTime.Now;
                _context.companyInformation.Update(companyInformation);
            }
            else
            {
                companyInformation.createdAt = DateTime.Now;
                _context.companyInformation.Add(companyInformation);
            }
            await _context.SaveChangesAsync();
            return companyInformation.Id;
        }

        public async Task<IEnumerable<CompanyInformation>> GetCompanyInformation()
        {
            return await _context.companyInformation.AsNoTracking().ToListAsync();
        }

        public async Task<CompanyInformation> GetCompanyInformationById(int id)
        {
            return await _context.companyInformation.Where(x => x.Id == id).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<CompanyInformation> GetCompanyInformationByRegId(int regId)
        {
            return await _context.companyInformation.Where(x => x.registrationFormId == regId).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCompanyInformationById(int id)
        {
            _context.companyInformation.Remove(_context.companyInformation.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
