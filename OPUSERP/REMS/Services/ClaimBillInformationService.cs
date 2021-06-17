using OPUSERP.Data;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services
{
    public class ClaimBillInformationService: IClaimBillInformationService
    {
        private readonly ERPDbContext _context;

        public ClaimBillInformationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveClaimBillInformation(ClaimBillInformation claimBillInformation)
        {
            if (claimBillInformation.Id != 0)
            {
                _context.ClaimBillInformation.Update(claimBillInformation);
            }
            else
            {
                _context.ClaimBillInformation.Add(claimBillInformation);
            }

            await _context.SaveChangesAsync();
            return claimBillInformation.Id;
        }
    }
}
