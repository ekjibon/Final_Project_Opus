using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Inventory;
using OPUSERP.VMS.Services.Inventory.interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.Inventory
{
    public class PartsIssueService: IPartsIssueService
    {
        private readonly ERPDbContext _context;

        public PartsIssueService(ERPDbContext context)
        {
            _context = context;
        }

        #region Parts Issue Master
        public async Task<int> SavePartsIssueMaster(PartsIssueMaster partsIssue)
        {
            if (partsIssue.Id != 0)
            {
                partsIssue.updatedBy = partsIssue.createdBy;
                partsIssue.updatedAt = DateTime.Now;
                _context.PartsIssueMasters.Update(partsIssue);
            }
            else
            {
                partsIssue.createdAt = DateTime.Now;
                _context.PartsIssueMasters.Add(partsIssue);
            }

            await _context.SaveChangesAsync();
            return partsIssue.Id;
        }

        public async Task<IEnumerable<PartsIssueMaster>> GetAllPartsIssueMaster()
        {
            return await _context.PartsIssueMasters.AsNoTracking().ToListAsync();
        }

        public async Task<PartsIssueMaster> GetPartsIssueMasterById(int id)
        {
            return await _context.PartsIssueMasters.FindAsync(id);
        }

        public IQueryable<PartsIssueMaster> GePartsIssueMasterByPartsId(int partsId)
        {
            return _context.PartsIssueMasters.Where(x => x.purchasePartsId == partsId);
        }

        public async Task<bool> DeletePartsIssueMasterById(int id)
        {
            _context.PartsIssueMasters.Remove(_context.PartsIssueMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Purchase Parts Details
        public async Task<int> SavePartsIssueDetails(PartsIssueDetails issueDetails)
        {
            if (issueDetails.Id != 0)
            {
                issueDetails.updatedBy = issueDetails.createdBy;
                issueDetails.updatedAt = DateTime.Now;
                _context.PartsIssueDetails.Update(issueDetails);
            }
            else
            {
                issueDetails.createdAt = DateTime.Now;
                _context.PartsIssueDetails.Add(issueDetails);
            }

            await _context.SaveChangesAsync();
            return issueDetails.Id;
        }

        public async Task<IEnumerable<PartsIssueDetails>> GetAllPartsIssueDetails()
        {
            return await _context.PartsIssueDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PartsIssueDetails> GetPartsIssueDetailsById(int id)
        {
            return await _context.PartsIssueDetails.FindAsync(id);
        }

        public IQueryable<PartsIssueDetails> GetPartsIssueDetailsByMasterId(int masterId)
        {
            return _context.PartsIssueDetails.Where(x => x.partsIssueId == masterId);
        }

        public async Task<bool> DeletePartsIssueDetailsByMasterId(int masterId)
        {
            _context.PartsIssueDetails.RemoveRange(_context.PartsIssueDetails.Where(x => x.partsIssueId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
