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
    public class BankInformationService: IBankInformationService
    {
        private readonly ERPDbContext _context;

        public BankInformationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveBankInformation(BankInformation bankInformation)
        {
            if (bankInformation.Id != 0)
            {
                bankInformation.updatedBy = bankInformation.createdBy;
                bankInformation.updatedAt = DateTime.Now;
                _context.bankInformation.Update(bankInformation);
            }
            else
            {
                bankInformation.createdAt = DateTime.Now;
                _context.bankInformation.Add(bankInformation);
            }
            await _context.SaveChangesAsync();
            return bankInformation.Id;
        }

        public async Task<IEnumerable<BankInformation>> GetBankInformation()
        {
            return await _context.bankInformation.AsNoTracking().ToListAsync();
        }

        public async Task<BankInformation> GetBankInformationById(int id)
        {
            return await _context.bankInformation.Where(x => x.Id == id).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BankInformation>> GetBankInformationByRegId(int regId)
        {
            return await _context.bankInformation.Where(x => x.registrationFormId == regId).Include(x => x.registrationForm).ToListAsync();
        }

        public async Task<bool> DeleteBankInformationById(int id)
        {
            _context.bankInformation.Remove(_context.bankInformation.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
