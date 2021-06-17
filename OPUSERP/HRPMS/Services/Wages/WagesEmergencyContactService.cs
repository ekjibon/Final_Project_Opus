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
    public class WagesEmergencyContactService : IWagesEmergencyContactService
    {
        private readonly ERPDbContext _context;

        public WagesEmergencyContactService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEmergencyContactById(int id)
        {
            _context.wagesEmergencyContacts.Remove(_context.wagesEmergencyContacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesEmergencyContact>> GetEmergencyContact()
        {
            return await _context.wagesEmergencyContacts.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesEmergencyContact>> GetEmergencyContactByEmpId(int empId)
        {
            return await _context.wagesEmergencyContacts.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<WagesEmergencyContact> GetEmergencyContactById(int id)
        {
            return await _context.wagesEmergencyContacts.FindAsync(id);
        }

        public async Task<bool> SaveEmergencyContact(WagesEmergencyContact emergencyContact)
        {
            if (emergencyContact.Id != 0)
                _context.wagesEmergencyContacts.Update(emergencyContact);
            else
                _context.wagesEmergencyContacts.Add(emergencyContact);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
