using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseOrder
{
    public class PODismissService:IPODismissService
    {
        private readonly ERPDbContext _context;

        public PODismissService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SavePODismissMaster(PODismissMaster pODismissMaster)
        {
            try
            {
                if (pODismissMaster.Id != 0)
                {
                    _context.pODismissMasters.Update(pODismissMaster);
                }
                else
                {
                    _context.pODismissMasters.Add(pODismissMaster);
                }

                await _context.SaveChangesAsync();
                return pODismissMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<PODismissMaster>> GetPODismissMaster()
        {
            return await _context.pODismissMasters.Include(x=>x.purchase).AsNoTracking().ToListAsync();
        }

        public async Task<int> SavePODismissDetails(PODismissDetail pODismissDetail)
        {
            try
            {
                if (pODismissDetail.Id != 0)
                {
                    _context.pODismissDetails.Update(pODismissDetail);
                }
                else
                {
                    _context.pODismissDetails.Add(pODismissDetail);
                }

                await _context.SaveChangesAsync();
                return pODismissDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<PODismissDetail>> GetPODismissDetailsByMaster(int masterId)
        {
            return await _context.pODismissDetails.Include(x => x.pODismiss).Include(x=>x.purchaseDetails).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrderDismissViewModel>> GetPOForDismisses(string userName)
        {
            return await _context.purchaseOrderDismisses.FromSql($"SCM.SP_GETPOLISTForDismiss {userName}").AsNoTracking().ToListAsync();
        }
    }
}
