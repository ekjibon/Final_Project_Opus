using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.PromotionAndTransfer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.PromotionAndTransfer
{
    public class PromotionTransferEntryService : IPromotionTransferEntryService
    {
        private readonly ERPDbContext _context;
        private readonly IServiceHistoryService serviceHistoryService;

        public PromotionTransferEntryService(ERPDbContext context, IServiceHistoryService serviceHistoryService)
        {
            _context = context;
            this.serviceHistoryService = serviceHistoryService;
        }
        
        //Promotion
        public async Task<bool> DeletePromotionEntryById(int id)
        {
            _context.promotionEntries.Remove(_context.promotionEntries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PromotionEntry>> GetPromotionEntry()
        {
            return await _context.promotionEntries.Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PromotionEntry>> GetPromotionEntryByOrg(string org)
        {
            return await _context.promotionEntries.Include(x => x.employee).Where(x=>x.employee.orgType==org).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PromotionEntry>> GetPromotionEntryByEmpId(int empId)
        {
            return await _context.promotionEntries.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<PromotionEntry> GetPromotionEntryById(int id)
        {
            return await _context.promotionEntries.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SavePromotionEntry(PromotionEntry promotionEntry)
        {
            if (promotionEntry.Id != 0)
                _context.promotionEntries.Update(promotionEntry);
            else
                _context.promotionEntries.Add(promotionEntry);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PromotionEntry>> GetPromotionEntryByStatus(string status)
        {
            return await _context.promotionEntries.Where(x => x.status == status).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PromotionEntry>> GetPromotionEntryByStatusAndOrg(string status,string org)
        {
            return await _context.promotionEntries.Where(x => x.status == status).Include(x => x.employee).Where(x => x.employee.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdatePromotionStatus(int Id, string Type)
        {
            PromotionEntry data = await _context.promotionEntries.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }


        //Transfar
        public async Task<bool> DeleteTransferEntryById(int id)
        {
            _context.transferEntries.Remove(_context.transferEntries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransferEntry>> GetTransferEntry()
        {
            return await _context.transferEntries.Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TransferEntry>> GetTransferEntryByOrg(string org)
        {
            return await _context.transferEntries.Include(x => x.employee).Where(x => x.employee.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TransferEntry>> GetTransferEntryByEmpId(int empId)
        {
            return await _context.transferEntries.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<TransferEntry> GetTransferEntryById(int id)
        {
            return await _context.transferEntries.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveTransferEntry(TransferEntry transferEntry)
        {
            if (transferEntry.Id != 0)
                _context.transferEntries.Update(transferEntry);
            else
                _context.transferEntries.Add(transferEntry);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransferEntry>> GetTransferEntryByStatus(string status)
        {
            return await _context.transferEntries.Where(x => x.status == status).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TransferEntry>> GetTransferEntryByStatusByOrg(string status,string org)
        {
            return await _context.transferEntries.Where(x => x.status == status).Include(x => x.employee).Where(x => x.employee.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateTransferStatus(int Id, string Type)
        {
            int tm = 0;
            TransferEntry data = await _context.transferEntries.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                tm = await _context.SaveChangesAsync();

                if (tm != 0 && Type == "approved") // saving Log
                {
                    TransferLog transferLog = new TransferLog
                    {
                        workStation = data.newPlace,
                        employeeId = data.employeeId,
                        from = Convert.ToDateTime(data.joiningDate),
                        //salaryGradeId = data.newGrade
                        designation = data.newDesignation
                    };
                    await serviceHistoryService.SaveServiceHistory(transferLog);
                }
            }
            if (tm == 0)
                return false;

            return true;
        }
    }
}
