using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using OPUSERP.Models.Dashboard;
using OPUSERP.VMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{
    public class BillCollectionService:IBillCollectionService
    {
        private readonly ERPDbContext _context;

        public BillCollectionService(ERPDbContext context)
        {
            _context = context;
        }

        #region BillCollection
        public async Task<int> SaveBillCollection(BillCollection billCollection)
        {
            if (billCollection.Id != 0)
                _context.BillCollections.Update(billCollection);
            else
                _context.BillCollections.Add(billCollection);
            await _context.SaveChangesAsync();
            return billCollection.Id;
        }

        public async Task<IEnumerable<BillCollection>> GetBillCollection()
        {
            return await _context.BillCollections.AsNoTracking().Include(x => x.billGenerate.approvedforCro.agreementDetails.agreement.leads).Include(x => x.billGenerate.bankBranchDetails.bankBranch).Include(x => x.billGenerate.bankBranchDetails.bank).ToListAsync();
        }
        public async Task<IEnumerable<BillCollection>> GetBillCollectionbyLeadId(int LeadId)
        {
            return await _context.BillCollections.Include(x => x.billGenerate.approvedforCro.agreementDetails.agreement.leads).Include(x => x.billGenerate.bankBranchDetails.bankBranch).Include(x => x.billGenerate.bankBranchDetails.bank).Where(x => x.billGenerate.approvedforCro.agreementDetails.agreement.leadsId == LeadId).ToListAsync();
        }
        public async Task<BillCollection> getBillCollectionById(int Id)
        {
            return await _context.BillCollections.Include(x => x.billGenerate.approvedforCro.agreementDetails.agreement.leads).Include(x => x.billGenerate.bankBranchDetails.bankBranch).Include(x => x.billGenerate.bankBranchDetails.bank).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BillCollection>> GetBillCollectionbybillId(int LeadId)
        {
            return await _context.BillCollections.Include(x => x.billGenerate.approvedforCro.agreementDetails.agreement.leads).Include(x => x.billGenerate.bankBranchDetails.bankBranch).Include(x => x.billGenerate.bankBranchDetails.bank).Where(x => x.billGenerateId == LeadId).ToListAsync();
        }
       

        public async Task<bool> DeleteById(int id)
        {
            _context.BillCollections.Remove(_context.BillCollections.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteBybillId(int id)
        {
            _context.BillCollections.RemoveRange(_context.BillCollections.Where(x=>x.billGenerateId==id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingList()
        {
            return await _context.BillCollectionSPViewModels.FromSql($"SP_GET_BillCollectionPendingList").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingListfilter(string ownername, string TeamLeader, string Fa, string BD, string LeadId)
        {
            return await _context.BillCollectionSPViewModels.FromSql($"SP_GET_BillCollectionPendingListfilter {ownername},{TeamLeader},{Fa},{BD},{LeadId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingListDatefilter(string ownername, DateTime fromDate, DateTime toDate)
        {
            return await _context.BillCollectionSPViewModels.FromSql($"SP_GET_BillCollectionPendingListDatefilter {ownername},{fromDate},{toDate}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingListFC()
        {
            return await _context.BillCollectionSPViewModels.FromSql($"SP_GET_BillCollectionPendingListforCollection").AsNoTracking().ToListAsync();
        }
        #endregion

        #region BillCollectionHistory
        public async Task<int> SaveBillCollectionHistory(BillCollectionHistory billCollection)
        {
            if (billCollection.Id != 0)
                _context.BillCollectionHistories.Update(billCollection);
            else
                _context.BillCollectionHistories.Add(billCollection);
            await _context.SaveChangesAsync();
            return billCollection.Id;
        }

        public async Task<IEnumerable<BillCollectionHistory>> GetBillCollectionHistory()
        {
            return await _context.BillCollectionHistories.AsNoTracking().Include(x => x.billCollection).ToListAsync();
        }
        public async Task<IEnumerable<BillCollectionHistory>> GetBillCollectionHistorybyCollectionId(int LeadId)
        {
            return await _context.BillCollectionHistories.Include(x => x.billCollection).Where(x => x.billCollectionId == LeadId).ToListAsync();
        }
        #endregion

        #region Dashboard

        public async Task<IEnumerable<ChartViewModel>> GetCollectionDashboardByDateArea(DateTime? frmDate, DateTime? toDate, int? areaId)
        {
            return await _context.chartViewModels.FromSql($"SP_Dashboard_CRM_BillCollection {frmDate},{toDate},{areaId}").ToListAsync();
        }

        #endregion
    }
}
