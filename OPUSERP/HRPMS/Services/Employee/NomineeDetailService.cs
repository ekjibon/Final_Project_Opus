using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class NomineeDetailService: INomineeDetailService
    {
        private readonly ERPDbContext _context;

        public NomineeDetailService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteNomineeDetailById(int id)
        {
            _context.nomineeDetails.Remove(_context.nomineeDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteNomineeDetailByNomineeId(int nomineeId)
        {
            _context.nomineeDetails.RemoveRange(_context.nomineeDetails.Where(x=>x.nomineeId == nomineeId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NomineeDetail>> GetNomineeDetail()
        {
            return await _context.nomineeDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<NomineeDetail>> GetNomineeDetailByNomineeId(int NomineeId)
        {
            return await _context.nomineeDetails.Where(x => x.nomineeId == NomineeId).Include(x=>x.nomineeFund).AsNoTracking().ToListAsync();
        }
        
        public async Task<decimal> GetNomineeRemainingFunByEmpIdAndFundId(int empId, int fundId)
        {
            return await _context.nomineeDetails.Where(x => x.nominee.employeeID == empId && x.nomineeFundId == fundId).SumAsync(x=> (int)x.persentence);
        }

        public async Task<NomineeDetail> GetNomineeDetailById(int id)
        {
            return await _context.nomineeDetails.FindAsync(id);
        }

        public async Task<IEnumerable<NomineeDetail>> GetNomineeDetailByEmployeeId(int employeeId)
        {
            return await _context.nomineeDetails.Where(x => x.nominee.employeeID == employeeId).Include(x=>x.nominee).Include(x => x.nomineeFund).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveNomineeDetail(NomineeDetail nomineeDetail)
        {
            if (nomineeDetail.Id != 0)
                _context.nomineeDetails.Update(nomineeDetail);
            else
                _context.nomineeDetails.Add(nomineeDetail);

            await _context.SaveChangesAsync();
            return nomineeDetail.Id;
        }
    }
}
