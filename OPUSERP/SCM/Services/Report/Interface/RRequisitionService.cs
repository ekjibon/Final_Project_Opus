using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;

namespace OPUSERP.SCM.Services.Report.Interface
{
    public class RRequisitionService : IRequisition
    {
        private readonly ERPDbContext _context;

        public RRequisitionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemSpecification>> GetItemSpecifications()
        {
            return  await _context.ItemSpecifications
                .Include(x=>x.Item)
                .ToListAsync();
        }

        public async Task<IEnumerable<RequisitionDetail>> GetRequisitionDetails()
        {
            return await _context.RequisitionDetails
                 .Include(x => x.requisitionMaster)
                 .Include(x => x.itemSpecification)
                 .Include(x => x.teamMember)
                 .Include(x => x.employee)
                 .Include(x=>x.employee)
                 .ToListAsync();
        }

        //public async Task<IEnumerable<StockDetails>> GetStockDetails()
        //{
        //    return await _context.StockDetails
        //        .Include(x=>x.stockMaster)
        //        .ToListAsync();

        //}
    }
}
