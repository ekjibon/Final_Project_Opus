using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.Rental.Services.Sales.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Services.Sales
{
    public class RentInvoiceDetailService : IRentInvoiceDetailService
    {
        private readonly ERPDbContext _context;

        public RentInvoiceDetailService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleDetails
        public async Task<bool> SaveSalesInvoiceDetail(RentInvoiceDetail salesInvoiceDetail)
        {
            if (salesInvoiceDetail.Id != 0)
                _context.RentInvoiceDetails.Update(salesInvoiceDetail);
            else
                _context.RentInvoiceDetails.Add(salesInvoiceDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentInvoiceDetail>> GetAllSalesInvoiceDetail()
        {
            return await _context.RentInvoiceDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RentInvoiceDetail>> GetAllSalesInvoiceDetailByMasterId(int masterId)
        {
            return await _context.RentInvoiceDetails.Include(x => x.itemSpecication).Include(x => x.itemSpecication.Item.unit).AsNoTracking().Where(x => x.salesInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<IEnumerable<SalesInvoiceDetailViewModel>> GetAllSalesInvoiceDetailByAgreementId(int masterId)
        {
            return await _context.salesInvoiceDetailViewModels.FromSql($"SP_SalesInvoiceDetailByAgreementId {masterId}").ToListAsync();
            //return await data.FirstOrDefaultAsync();
            //return await _context.RentInvoiceDetails.Include(x => x.itemSpecication).Include(x => x.itemSpecication.Item.unit).AsNoTracking().Where(x => x.salesInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<IEnumerable<PatientInvoiceDetailViewModel>> GetPatientInvoiceItemByLeadsId(int leadId)
        {
            var result= await _context.patientInvoiceDetailViewModels.FromSql($"SP_PatientInvoiceDetailByLeadsId {leadId}").Distinct().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PatientServiceDetail>> GetAllSalesInvoiceDetailPatientByMasterId(int masterId)
        {
            return await _context.PatientServiceDetails.Include(x => x.itemSpecification.Item).AsNoTracking().Where(x => x.patientService.leadsId == masterId).ToListAsync();
        }
        public async Task<IEnumerable<RentInvoiceDetail>> GetAllSalesInvoiceDetailByMasterItemId(int itemId,int masterId)
        {
            return await _context.RentInvoiceDetails.Include(x => x.itemSpecication.Item.unit).Distinct().AsNoTracking().Where(x => x.salesInvoiceMasterId == masterId&&x.itemSpecication.itemId==itemId).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<PatientServiceDetail>> GetAllSalesInvoiceDetailByMasterpatItemId(int itemId, int masterId)
        {
            return await _context.PatientServiceDetails.Include(x => x.itemSpecification.Item.unit).Distinct().AsNoTracking().Where(x => x.patientService.leadsId == masterId && x.itemSpecification.itemId == itemId).ToListAsync();
        }

        public async Task<RentInvoiceDetail> GetSalesInvoiceDetailById(int id)
        {
            return await _context.RentInvoiceDetails.AsNoTracking().Where(x=>x.Id==id).FirstOrDefaultAsync();
        }


        public async Task<bool> DeleteSalesInvoiceDetailById(int id)
        {
            _context.RentInvoiceDetails.Remove(_context.RentInvoiceDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSalesInvoiceDetailByMasterId(int masterId)
        {
            _context.RentInvoiceDetails.RemoveRange(_context.RentInvoiceDetails.Where(x => x.salesInvoiceMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
       
    }
}
