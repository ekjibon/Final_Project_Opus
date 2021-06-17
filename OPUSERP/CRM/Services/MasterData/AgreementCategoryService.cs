using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class AgreementCategoryService : IAgreementCategoryService
    {
        private readonly ERPDbContext _context;

        public AgreementCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAgreementCategory(AgreementCategory agreementCategory)
        {
            if (agreementCategory.Id != 0)
                _context.AgreementCategories.Update(agreementCategory);
            else
                _context.AgreementCategories.Add(agreementCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AgreementCategory>> GetAllAgreementCategory()
        {
            return await _context.AgreementCategories.ToListAsync();
        }

        public async Task<AgreementCategory> GetAgreementCategoryById(int id)
        {
            return await _context.AgreementCategories.FindAsync(id);
        }

        public async Task<bool> DeleteAgreementCategoryById(int id)
        {
            _context.AgreementCategories.Remove(_context.AgreementCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
