using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{
   
    public class BillGenerateService : IBillGenerateService
    {
        private readonly ERPDbContext _context;

        public BillGenerateService(ERPDbContext context)
        {
            _context = context;
        }


        #region Bill Generate

        public async Task<int> SaveBillGenerate(BillGenerate billGenerate)
        {
            if (billGenerate.Id != 0)
                _context.BillGenerates.Update(billGenerate);
            else
                _context.BillGenerates.Add(billGenerate);
            await _context.SaveChangesAsync();
            return billGenerate.Id;
        }

        public async Task<IEnumerable<BillGenerate>> GetAllBillGenerate()
        { 
            return await _context.BillGenerates?.Include(x => x.approvedforCro)?.Include(x => x.bankBranchDetails)?.Include(x => x.bankBranchDetails.bank)?.Include(x => x.bankBranchDetails.bankBranch)?.Include(x => x.approvedforCro.agreementDetails.agreement.leads)?.Include(x => x.approvedforCro.agreementDetails.agreement.agreementCategory)?.Include(x => x.approvedforCro.agreementDetails.ratingYear).OrderByDescending(a=>a.Id).ToListAsync();       
        }

        public async Task<BillGenerate> GetBillGenerateById(int Id)
        {
            return await _context.BillGenerates.Include(x => x.approvedforCro.agreementDetails.agreement.leads).Include(x => x.bankBranchDetails).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BillGenerate>> GetBillGenerateByLeadId(int Id)
        {
            return await _context.BillGenerates.Include(x=>x.approvedforCro.agreementDetails.agreement.leads).Include(x => x.approvedforCro).Include(x => x.bankBranchDetails).Where(x => x.approvedforCro.agreementDetails.agreement.leadsId == Id).ToListAsync();
        }

        public async Task<IEnumerable<BillGenerate>> GetBillGenerateByCroId(int Id)
        {
            return await _context.BillGenerates.Include(x => x.approvedforCro.agreementDetails.agreement.leads).Include(x => x.approvedforCro).Include(x => x.bankBranchDetails).Where(x => x.approvedforCroId == Id).ToListAsync();
        }


        public async Task<bool> DeleteBillGenerateById(int id)
        {
            _context.BillGenerates.Remove(_context.BillGenerates.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
        public async Task<BillGenerateInfoViewModel> GetbillgenerateInfobybillId(int Id)
        {
            decimal? collectedAmount = _context.BillCollections.Where(x => x.billGenerateId == Id).Sum(x => x.bankAmount + x.cashAmount + x.mobileBankAmount+x.payOrderAmount);
            BillGenerate billGenerate = await _context.BillGenerates.Include(x => x.approvedforCro.agreementDetails.vatCategory).Include(x => x.approvedforCro.agreementDetails.ratingYear).Include(x => x.approvedforCro.agreementDetails.agreement.leads).Where(x => x.Id == Id).FirstOrDefaultAsync();
            List<BillGenerateInfoViewModel> data = new List<BillGenerateInfoViewModel>();
            data.Add(new BillGenerateInfoViewModel {
                leadName=billGenerate.approvedforCro.agreementDetails.agreement.leads.leadName,
                agreementNo=billGenerate.approvedforCro.agreementDetails.agreement.agreementNo,
                invoiceNo=billGenerate.invoiceNo,
                billDate=billGenerate.billDate,
                ratingType=billGenerate.approvedforCro.agreementDetails.ratingYear.ratingYearName,
                billAmount=billGenerate.totalAmount,
                vatType=billGenerate.approvedforCro.agreementDetails.vatCategory.vatCategoryName,
                vatAmount=billGenerate.approvedforCro.agreementDetails.vatAmount,
                collectedAmount=collectedAmount,
                dueAmount=billGenerate.totalAmount-collectedAmount


            });

            return data.FirstOrDefault();           

        }

        public async Task<IEnumerable<BillGenerateListSPViewModel>> GetAllBillGenerateBySP()
        {
            return await _context.BillGenerateListSPViewModels.FromSql($"SP_GET_BillGenerateList").AsNoTracking().ToListAsync();
        }


        #endregion

        #region Approved for Cro

        public async Task<IEnumerable<ApprovedforCro>> GetClientForBillGenerate()
        {
            //return await _context.ApprovedforCros.Include(x => x.agreementDetails.agreement.leads).Where(a => a.agreementDetails.agreement.leads.isClient == 1).ToListAsync();
            List<int?> appcroIds = _context.BillGenerates.Select(x => x.approvedforCroId).ToList();

            return await _context.ApprovedforCros.Where(x => !appcroIds.Contains(x.Id)).Include(x => x.agreementDetails.agreement.leads).Include(x => x.agreementDetails.agreement.agreementType).Include(x => x.agreementDetails.agreement.agreementCategory).Include(x => x.agreementDetails.ratingYear).ToListAsync();
        }

        public async Task<IEnumerable<BillGenerateSPViewModel>> BillGeneratePendingList(int appCroId)
        {
            return await _context.BillGenerateSPViewModels.FromSql($"SP_GET_BillGeneratePendingList {appCroId}").AsNoTracking().ToListAsync();
        }

        public async Task<BillGenerateSPViewModel> BillGeneratePendingListById(int appCroId)
        {
            return await _context.BillGenerateSPViewModels.FromSql($"SP_GET_BillGeneratePendingList {appCroId}").FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<ApprovedforCro>> GetRatingYearForBillGenerate(int clientId)
        {
            return await _context.ApprovedforCros.Include(x => x.agreementDetails.ratingYear).Where(a => a.agreementDetails.agreement.leads.Id == clientId).ToListAsync();
        }

        public async Task<ApprovedforCro> GetBillGenerateInfoByClient(int clientId, int ratingYearId)
        {
            return await _context.ApprovedforCros.Include(x => x.agreementDetails.agreement.leads).Include(x => x.agreementDetails.ratingYear).Where(a => a.agreementDetails.agreement.leads.Id == clientId && a.agreementDetails.ratingYear.Id == ratingYearId).FirstOrDefaultAsync();
        }

        public async Task<ApprovedforCro> GetBillGenerateInfoById(int id)
        {
            return await _context.ApprovedforCros?.Include(x => x.agreementDetails.agreement.leads)?.Include(x => x.agreementDetails.ratingYear)?.Include(x => x.agreementDetails.agreement.agreementType)?.Include(x => x.agreementDetails.agreement.agreementCategory)?.Include(x => x.agreementDetails.vatCategory)?.Include(x => x.agreementDetails.taxCategory)?.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        #endregion

        #region Bank Branch Details

        public async Task<IEnumerable<BankBranchDetails>> GetBankBranchDetails()
        {
            return await _context.BankBranchDetails.Include(x => x.bank).Include(x => x.bankBranch).ToListAsync();
        }

        #endregion

        #region Bill Generate History

        public async Task<bool> SaveBillGenerateHistory(BillGenerateHistory billGenerateHistory)
        {
            if (billGenerateHistory.Id != 0)
                _context.BillGenerateHistories.Update(billGenerateHistory);
            else
                _context.BillGenerateHistories.Add(billGenerateHistory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BillGenerateHistory>> GetBillGenerateHistoryByLeadId(int billId)
        {
            return await _context.BillGenerateHistories.Include(a => a.billGenerate).Where(a => a.billGenerateId == billId).OrderByDescending(a => a.Id).ToListAsync();
        }

        #endregion

        #region Invoice Report

        public async Task<IEnumerable<BillGenerateReportViewModel>> GetInvoiceReportByInvoiceId(int invoiceId)
        {
            return await _context.BillGenerateReportViewModels.FromSql($"spRPTInvoiceGenerateFirst {invoiceId}").AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
