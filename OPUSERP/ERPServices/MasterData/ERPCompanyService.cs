using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData
{
    public class ERPCompanyService: IERPCompanyService
    {
        private readonly ERPDbContext _context;

        public ERPCompanyService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveERPCompany(Company company)
        {
            if (company.Id != 0)
                _context.Companies.Update(company);
            else
                _context.Companies.Add(company);
             await _context.SaveChangesAsync();
            return company.Id;
        }

        public void UpdateCompanyLogoById(int compId, string fileName,string fileLocation)
        {
            var user = _context.Companies.Find(compId);
            user.fileName = fileName;
            user.filePath = fileLocation;
            _context.Entry(user).State = EntityState.Modified;
            
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Company>> GetAllCompany()
        {
            var result= await _context.Companies.OrderBy(a => a.Id).Take(1).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Company> GetCompanyById(int Id)
        {
            return await _context.Companies.FindAsync(Id);
        }

        public async Task<bool> DeleteCompanyById(int id)
        {
            _context.Companies.Remove(_context.Companies.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
