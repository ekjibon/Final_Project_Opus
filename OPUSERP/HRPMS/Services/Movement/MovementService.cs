using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Movement
{
    public class MovementService : IMovementService
    {
        private readonly ERPDbContext _context;

        public MovementService(ERPDbContext context)
        {
            _context = context;
        }


        public async Task<bool> SaveMovement(MovementTracking movementTracking)
        {
            if (movementTracking.Id != 0)
                _context.MovementTracking.Update(movementTracking);
            else
                _context.MovementTracking.Add(movementTracking);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateMovement(int id, MovementTracking movementTracking)
        {

            _context.MovementTracking.Update(movementTracking);

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<MovementTracking>> GetAllMovementTracking(string empcode)
        {
            return await _context.MovementTracking.Where(x => x.empCode == empcode).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MovementTracking>> GetAllMovementByEmpIdAndDateRange(string empcode, DateTime? from, DateTime? to)
        {
            return await _context.MovementTracking.Where(x => x.empCode == empcode && x.entryDate >= from && x.entryDate <= to).AsNoTracking().ToListAsync();
        }
    }
}
