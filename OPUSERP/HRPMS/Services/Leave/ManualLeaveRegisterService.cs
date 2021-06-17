using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class ManualLeaveRegisterService : IManualLeaveRegisterService
    {
        private readonly ERPDbContext _context;

        public ManualLeaveRegisterService(ERPDbContext context)
        {
            _context = context;
        }

        public Task<bool> SaveManualLeaveRegister(LeaveRegister leaveRegisterManual)
        {
            throw new NotImplementedException();

            //_context.leaveRegisters.Add(leaveRegisterManual);
            //return 1 == await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<LeaveRegister>> GetAllManualLeaveRegister()
        {
            throw new NotImplementedException();

            //return await _context.leaveRegisters.AsNoTracking().ToListAsync();
        }

        public Task<LeaveRegister> GetManualLeaveRegisterById(int id)
        {
            throw new NotImplementedException();

            //return await _context.leaveRegisters.FindAsync(id);
        }

        public Task<bool> DeleteManualLeaveRegisterById(int id)
        {
            throw new NotImplementedException();

            //_context.leaveRegisters.Remove(_context.leaveRegisters.Find(id));
            //return 1 == await _context.SaveChangesAsync();
        }

    }
}
