using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration
{
    public class RegistrationService: IRegistrationService
    {
        private readonly ERPDbContext _context;

        public RegistrationService(ERPDbContext context)
        {
            _context = context;
        }

        #region Registration Form

        public async Task<int> SaveRegistrationForm(RegistrationForm registrationForm)
        {
            if (registrationForm.Id != 0)
            {
                registrationForm.updatedBy = registrationForm.createdBy;
                registrationForm.updatedAt = DateTime.Now;
                _context.registrationForms.Update(registrationForm);
            }
            else
            {
                registrationForm.createdAt = DateTime.Now;
                _context.registrationForms.Add(registrationForm);
            }
            await _context.SaveChangesAsync();
            return registrationForm.Id;
        }

        public async Task<IEnumerable<RegistrationForm>> GetRegistrationForm()
        {
            return await _context.registrationForms.AsNoTracking().ToListAsync();
        }

        public async Task<RegistrationForm> GetRegistrationFormById(int id)
        {
            return await _context.registrationForms.Where(x => x.Id == id).Include(x=>x.procurementCategory).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RegistrationForm>> GetRegistrationFormByApproveType(int Type)
        {
            return await _context.registrationForms.Where(x => x.approveType == Type).ToListAsync();
        }

        public async Task<bool> DeleteRegistrationFormById(int id)
        {
            _context.registrationForms.Remove(_context.registrationForms.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRegistrationForm(int? Id, int? appType)
        {
            var regForm = _context.registrationForms.Find(Id);
            if (regForm != null)
            {
                regForm.approveType = appType;
            }
            await _context.SaveChangesAsync();
            return regForm.Id;
        }

        public async Task<int> UpdateRegistrationFormSupplierId(int? Id, int? supplierId)
        {
            var regForm = _context.registrationForms.Find(Id);
            if (regForm != null)
            {
                regForm.organizationId = supplierId;
            }
            await _context.SaveChangesAsync();
            return regForm.Id;
        }

        public async Task<int> UpdateGeneralInformation(RegistrationForm registrationForm)
        {
            var regForm = _context.registrationForms.Find(registrationForm.Id);
            if (regForm != null)
            {
                regForm.companyName = registrationForm.companyName;
                regForm.tradeLicenseNumber = registrationForm.tradeLicenseNumber;
                regForm.eTinNumber = registrationForm.eTinNumber;
                regForm.vatNumber = registrationForm.vatNumber;
                regForm.contactPersonName = registrationForm.contactPersonName;
                regForm.contactNumber = registrationForm.contactNumber;
                regForm.companyEmail = registrationForm.companyEmail;
                regForm.alternativeEmail = registrationForm.alternativeEmail;
            }
            await _context.SaveChangesAsync();
            return regForm.Id;
        }

        public async Task<RegistrationForm> GetLoginInfoByLoginIdAndPassword(string loginId, string password)
        {
            return await _context.registrationForms.Where(x => x.loginId == loginId && x.password == password && x.approveType == 1).FirstOrDefaultAsync();
        }
        #endregion

    }
}
