using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance
{
    public class InvoiceService: IInvoiceService
    {
        private readonly ERPDbContext _context;

        public InvoiceService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveInvoice(Invoice invoice)
        {
            if (invoice.Id != 0)
                _context.invoices.Update(invoice);
            else
                _context.invoices.Add(invoice);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoice()
        {
            return await _context.invoices.AsNoTracking().ToListAsync();
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _context.invoices.FindAsync(id);
        }

        public async Task<bool> DeleteInvoiceById(int id)
        {
            _context.invoices.Remove(_context.invoices.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
