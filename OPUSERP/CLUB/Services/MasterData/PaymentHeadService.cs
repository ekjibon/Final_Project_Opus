using CLUB.Areas.Member.Models.NotMappedEntity;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData
{
    public class PaymentHeadService: IPaymentHeadService
    {
        private readonly ERPDbContext _context;

        public PaymentHeadService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SavePaymentHead(PaymentHead paymentHead)
        {
            if (paymentHead.Id != 0)
                _context.paymentHeads.Update(paymentHead);
            else
                _context.paymentHeads.Add(paymentHead);
             await _context.SaveChangesAsync();
            return paymentHead.Id;
        }

        public async Task<IEnumerable<PaymentHead>> GetAllPaymentHead()
        {
            return await _context.paymentHeads.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PaymentHead>> GetAllMonthlyPaymentHead()
        {
            return await _context.paymentHeads.Where(x => x.type == "Monthly").AsNoTracking().ToListAsync();
        }

        public async Task<PaymentHead> GetPaymentHeadById(int id)
        {
            return await _context.paymentHeads.FindAsync(id);
        }

        public async Task<bool> DeletePaymentHeadById(int id)
        {
            _context.paymentHeads.Remove(_context.paymentHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentHeadWithDue>> GetMonthlyPaymentHeadByMemberId(int memberId)
        {
            IEnumerable<PaymentHead> paymentHeads = await _context.paymentHeads.Where(x=>x.type == "Monthly").AsNoTracking().ToListAsync();

            List<PaymentHeadWithDue> data = new List<PaymentHeadWithDue>();
            foreach (PaymentHead paymentHead in paymentHeads)
            {
                var invoiceDue = _context.invoices.Where(x => x.paymentHeadId == paymentHead.Id && x.employeeId == memberId).Sum(s => s.amount);
                var openingDue = _context.balances.Where(x => x.paymentHeadId == paymentHead.Id && x.employeeId == memberId).Sum(s => s.due);

                var collectionDue = _context.collections.Where(x => x.paymentHeadId == paymentHead.Id && x.employeeId == memberId && x.trMaster.status == 1).Sum(s => s.amount);
                var openingPaid = _context.balances.Where(x => x.paymentHeadId == paymentHead.Id && x.employeeId == memberId).Sum(s => s.paid);

                var Due = (invoiceDue + (decimal)openingDue)-(collectionDue + (decimal)openingPaid);
                data.Add(new PaymentHeadWithDue
                {
                    paymentHead = paymentHead,
                    Due = Due,
                });
            }
            return data;
        }

        public async Task<IEnumerable<PaymentHeadWithDue>> GetOnetimePaymentHeadByMemberId(int memberId)
        {
            IEnumerable<PaymentHead> paymentHeads = await _context.paymentHeads.Where(x => x.type == "Onetime").AsNoTracking().ToListAsync();

            List<PaymentHeadWithDue> data = new List<PaymentHeadWithDue>();
            foreach (PaymentHead paymentHead in paymentHeads)
            {
                var invoiceDue = _context.invoices.Where(x => x.paymentHeadId == paymentHead.Id && x.employeeId == memberId).Sum(s => s.amount);
                var collectionDue = _context.collections.Where(x => x.paymentHeadId == paymentHead.Id && x.employeeId == memberId && x.trMaster.status == 1).Sum(s => s.amount);
                var Due = invoiceDue - collectionDue;
                if (Due > 0)
                {
                    data.Add(new PaymentHeadWithDue
                    {
                        paymentHead = paymentHead,
                        Due = Due,
                    });
                }
            }
            return data;
        }
    }
}
