using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Rental.Models;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.Data;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.Rental.Services.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Services.Sales
{
    public class RentInvoiceMasterService : IRentInvoiceMasterService
    {
        private readonly ERPDbContext _context;

        public RentInvoiceMasterService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleInvoice
        public async Task<int> SaveSalesInvoiceMaster(RentInvoiceMaster salesInvoiceMaster)
        {
            if (salesInvoiceMaster.Id != 0)
                _context.RentInvoiceMasters.Update(salesInvoiceMaster);
            else
                _context.RentInvoiceMasters.Add(salesInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesInvoiceMaster.Id;
        }
        public async Task<bool> UpdateSalesInvoiceMaster(int Id,DateTime startDate,DateTime endDate)
        {
            var VoucherMasters = _context.RentInvoiceDetails.Find(Id);
            VoucherMasters.StartDate = startDate;
            VoucherMasters.EndDate = endDate;
           

            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateRSalesInvoiceMaster(int Id,DateTime startDate)
        {
            var VoucherMasters = _context.RentInvoiceMasters.Find(Id);
            VoucherMasters.isreceive = 1;
            VoucherMasters.receiveDate = startDate;
           

            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<RentInvoiceMaster>> GetAllSalesInvoiceMaster()
        {
            return await _context.RentInvoiceMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<RantalListwithDateViewModel>> GetAllRantalListwithDateViewModel()
        {
            var master =  await _context.RentInvoiceMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).OrderByDescending(x => x.Id).ToListAsync();
            List<RantalListwithDateViewModel> rantalListwithDateViewModels = new List<RantalListwithDateViewModel>();
            foreach(var data in master)
            {
                var details = await _context.RentInvoiceDetails.Where(x => x.salesInvoiceMasterId == data.Id).FirstOrDefaultAsync();
                if (details != null)
                {
                    TimeSpan timeSpan = ((DateTime)details.EndDate - DateTime.Now);
                    var dd = DateTime.Now.ToShortDateString();
                    rantalListwithDateViewModels.Add(new RantalListwithDateViewModel
                    {
                        rentInvoiceMaster = data,
                        startDate = details.StartDate,
                        endDate = details.EndDate,
                        dateDiff = (int)timeSpan.TotalDays+1,
                        rentInvoiceDetail=details
                    });
                }
            }

            return rantalListwithDateViewModels;
        }

        //public async Task<IEnumerable<RentalInvoiceDataViewModel>> GetSalesInvoiceMasterById(int id)
        //{
        //    var master = await _context.RentInvoiceMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).Include(x => x.relSupplierCustomerResourse.resource).Where(x => x.Id == id).FirstOrDefaultAsync();
        //    List<RentalInvoiceDataViewModel> rentalInvoiceDataViewModels = new List<RentalInvoiceDataViewModel>();
        //    foreach (var data in master)
        //    {
        //        var details = await _context.RentInvoiceDetails.Where(x => x.salesInvoiceMasterId == data.Id).FirstOrDefaultAsync();
        //        if (details != null)
        //        {
        //            rentalInvoiceDataViewModels.Add(new RentalInvoiceDataViewModel
        //            {
        //                invoiceNumber = master.invoiceNumber,
        //                leadName = master.leads.leadName,
        //                relSupplierCustomerResourseId=master.relSupplierCustomerResourseId
        //            });
        //        }
        //    }
        //    return rentalInvoiceDataViewModels;
        //}

        public async Task<RentInvoiceMaster> GetSalesInvoiceMasterById(int id)
        {
            return await _context.RentInvoiceMasters.Include(x => x.relSupplierCustomerResourse.Leads).Include(x => x.relSupplierCustomerResourse.resource).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<RentInvoiceMaster>> GetSalesInvoiceMasterByLeadId(int id)
        {
            return await _context.RentInvoiceMasters.Include(x => x.relSupplierCustomerResourse.Leads).Where(x => x.relSupplierCustomerResourseId == id).ToListAsync();
        }


        public async Task<bool> UpdateSalesInvoiceMasterById(int id)
        {
            RentInvoiceMaster salesInvoiceMaster = await _context.RentInvoiceMasters.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.isClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }
        public async Task<bool> UpdateSalesInvoiceMasterStockCloseById(int id)
        {
            RentInvoiceMaster salesInvoiceMaster = await _context.RentInvoiceMasters.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.isStockClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }


        public async Task<bool> DeleteSalesInvoiceMasterById(int id)
        {
            _context.RentInvoiceMasters.Remove(_context.RentInvoiceMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<LeadAutoNumber> GetAutoRentInvoiceNoBySp(string invoiceDate)
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql($"SP_AutoRentInvoiceNo {invoiceDate}");
            return await data.FirstOrDefaultAsync();
        }
        public async Task<LeadAutoNumber> GetAutoSalesInvoiceNoBySp(string invoiceDate)
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql($"SP_AutoSalestInvoiceNo {invoiceDate}");
            return await data.FirstOrDefaultAsync();
        }

        #endregion

        #region Terms
        public async Task<int> SaveRentTerms(RentTermsConditions salesInvoiceMaster)
        {
            if (salesInvoiceMaster.Id != 0)
                _context.RentTermsConditions.Update(salesInvoiceMaster);
            else
                _context.RentTermsConditions.Add(salesInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesInvoiceMaster.Id;
        }





        public async Task<RentTermsConditions> GetTermsConditionsById(int id)
        {
            return await _context.RentTermsConditions.Include(x => x.salesInvoiceMaster).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<RentTermsConditions>> GetTermsConditionsByMasterId(int id)
        {
            return await _context.RentTermsConditions.Include(x => x.salesInvoiceMaster).Where(x => x.salesInvoiceMasterId == id).ToListAsync();
        }




        public async Task<bool> DeleteTermsConditionsById(int id)
        {
            _context.RentTermsConditions.Remove(_context.RentTermsConditions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteTermsConditionsByMasterId(int id)
        {
            _context.RentTermsConditions.RemoveRange(_context.RentTermsConditions.Where(x => x.salesInvoiceMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Terms and Condition

        public async Task<int> SaveTermsAndConditions(TermsAndConditions termsAndConditions)
        {
            if (termsAndConditions.Id != 0)
                _context.TermsAndConditions.Update(termsAndConditions);
            else
                _context.TermsAndConditions.Add(termsAndConditions);
            await _context.SaveChangesAsync();
            return termsAndConditions.Id;
        }

        public async Task<IEnumerable<TermsAndConditions>> GetAllTermsAndConditions()
        {
            return await _context.TermsAndConditions.OrderBy(a => a.shortOrder).ToListAsync();
        }

        public async Task<bool> DeleteTermsAndConditionsById(int id)
        {
            _context.TermsAndConditions.Remove(_context.TermsAndConditions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
