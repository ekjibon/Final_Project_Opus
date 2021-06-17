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
    public class EmergencyContactService: IEmergencyContactService
    {
        private readonly ERPDbContext _context;

        public EmergencyContactService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEmergencyContactById(int id)
        {
            _context.emergencyContacts.Remove(_context.emergencyContacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmergencyContact>> GetEmergencyContact()
        {
            return await _context.emergencyContacts.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmergencyContact>> GetEmergencyContactByEmpId(int empId)
        {
            return await _context.emergencyContacts.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<EmergencyContact> GetEmergencyContactById(int id)
        {
            return await _context.emergencyContacts.FindAsync(id);
        }

        public async Task<bool> SaveEmergencyContact(EmergencyContact emergencyContact)
        {
            if (emergencyContact.Id != 0)
                _context.emergencyContacts.Update(emergencyContact);
            else
                _context.emergencyContacts.Add(emergencyContact);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
