using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ERPDbContext _context;

        public LeaveTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveLeaveType(LeaveType leaveType)
        {
            if (leaveType.Id != 0)
                _context.leaveTypes.Update(leaveType);
            else
                _context.leaveTypes.Add(leaveType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveType()
        {
            return await _context.leaveTypes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveTypeBySP(int? empId)
        {
            return await _context.leaveTypes.FromSql($"SP_GET_LeaveTypes {empId}").AsNoTracking().ToListAsync();
        }

        public async Task<LeaveType> GetLeaveTypeById(int id)
        {

            return await _context.leaveTypes.FindAsync(id);
        }

        public async Task<bool> DeleteLeaveTypeById(int id)
        {
            _context.leaveTypes.Remove(_context.leaveTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
