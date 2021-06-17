using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class LeaveApprovalService : ILeaveApprovalService
    {
        private readonly ERPDbContext _context;

        public LeaveApprovalService(ERPDbContext context)
        {
            _context = context;
        }

        //JobCircular
        public async Task<bool> DeleteLeaveApprovalById(int id)
        {
            _context.jobCirculars.Remove(_context.jobCirculars.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<LeaveRegister>> GetAllLeaveApproval(string status)
        {
            throw new NotImplementedException();

            //return await _context.leaveRegisters.Where(x => x.leaveStatus == status).AsNoTracking().ToListAsync();
        }

        public Task<LeaveRegister> GetLeaveApprovalById(int id)
        {
            throw new NotImplementedException();

            // return await _context.leaveRegisters.Where(x => x.Id == id).FirstAsync();
        }

        public Task<int> SaveLeaveApproval(LeaveRegister leaveRegister)
        {
            throw new NotImplementedException();

            //if (leaveRegister.Id != 0)
            //    _context.leaveRegisters.Update(leaveRegister);
            //else
            //    _context.leaveRegisters.Add(leaveRegister);
            //await _context.SaveChangesAsync();
            //return leaveRegister.Id;
        }

        public Task<bool> UpdateLeaveApproval(int Id, string Type)
        {
            throw new NotImplementedException();

            //LeaveRegister data =  await _context.leaveRegisters.FindAsync(Id);
            //if(data != null)
            //{
            //    data.leaveStatus = Type;
            //    return 1 == await _context.SaveChangesAsync();
            //}
            //return false;
        }
        
    }
}
