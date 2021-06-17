using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class WagesAddressEducationPhotoService : IWagesEmployeeInfoService
    {
        private readonly ERPDbContext _context;

        public WagesAddressEducationPhotoService(ERPDbContext context)
        {
            _context = context;
        }

        #region Address 

        public async Task<bool> SaveAddress(WagesAddress address)
        {
            if (address.Id != 0)
                _context.wagesAddresses.Update(address);
            else
                _context.wagesAddresses.Add(address);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesAddress>> GetAllAddress()
        {
            return await _context.wagesAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<WagesAddress> GetAddressById(int id)
        {
            return await _context.wagesAddresses.FindAsync(id);
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            _context.wagesAddresses.Remove(_context.wagesAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesAddress>> GetAddressByEmpId(int empId)
        {
            return await _context.wagesAddresses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<WagesAddress> GetAddressByTypeAndEmpId(int empId, string type)
        {
            return await _context.wagesAddresses.Where(x => x.employeeId == empId && x.type == type).Include(x=> x.division).Include(x=>x.district).Include(x=>x.thana).FirstOrDefaultAsync();
        }

        #endregion
        


        #region Photograph

        public async Task<bool> SaveImage(WagesPhotograph photograph)
        {
            if (photograph.Id != 0)
                _context.wagesPhotographs.Update(photograph);
            else
                _context.wagesPhotographs.Add(photograph);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesPhotograph>> GetPhotograph()
        {
            return await _context.wagesPhotographs.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesPhotograph>> GetPhotographByEmpId(int empId)
        {
            return await _context.wagesPhotographs.Where(x => x.employeeId == empId).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.wagesPhotographs.Remove(_context.wagesPhotographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<WagesPhotograph> GetPhotographByTypeAndEmpId(int empId, string type)
        {
            return await _context.wagesPhotographs.Where(x => x.employeeId == empId && x.type == type).AsNoTracking().FirstOrDefaultAsync();
        }

       



        #endregion
    }
}
