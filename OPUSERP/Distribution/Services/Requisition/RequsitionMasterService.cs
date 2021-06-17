using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.Requisition;
using OPUSERP.Distribution.Services.Requisition.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.Requisition
{
    public class RequisitionMasterService : IRequsitionMasterService
    {
        private readonly ERPDbContext _context;

        public RequisitionMasterService(ERPDbContext context)
        {
            _context = context;
        }
        #region Requisition Master
        public async Task<int> SaveRequisitionMaster(RequisitionMaster requisitionMaster)
        {
            if (requisitionMaster.Id != 0)
                _context.distributionRequisitionMasters.Update(requisitionMaster);
            else
                _context.distributionRequisitionMasters.Add(requisitionMaster);
            await _context.SaveChangesAsync();
            return requisitionMaster.Id;
        }

        public async Task<IEnumerable<RequisitionMaster>> GetAllRequistionMaster()
        {
            return await _context.distributionRequisitionMasters.AsNoTracking().Include(x=>x.relSupplierCustomerResourse.resource).OrderByDescending(x=>x.Id).ToListAsync();
        }

       

        public async Task<RequisitionMaster> GetRequistionMasterById(int id)
        {
            return await _context.distributionRequisitionMasters.Include(x=>x.relSupplierCustomerResourse.distributorType).Include(x=>x.relSupplierCustomerResourse.resource).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<RequisitionMaster>> GetRequistionMasterBydistributorId(int id)
        {
            return await _context.distributionRequisitionMasters.Include(x => x.relSupplierCustomerResourse.resource).Where(x => x.relSupplierCustomerResourse.organizationId == id).ToListAsync();
        }
        public async Task<IEnumerable<RequisitionMaster>> GetRequistionMasterByareaId(int id)
        {
            List<int?> relid =await _context.RelCustomerAreas.Select(x =>x.relSupplierCustomerResourseId).ToListAsync();
            return await _context.distributionRequisitionMasters.Include(x => x.relSupplierCustomerResourse.resource).Where(x =>relid.Contains(x.relSupplierCustomerResourseId)).ToListAsync();
        }
       

       

        public async Task<bool> DeleteRequisitionMasterById(int id)
        {
            _context.distributionRequisitionMasters.Remove(_context.distributionRequisitionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }



        #endregion
        #region Requisition Approver
        public async Task<IEnumerable<RequisitionApprovalViewModel>> GetBudgetRequsitionMasterForApproval(int userId)
        {
            return await _context.requisitionApprovalViewModels.FromSql(@"Sp_GetDistributionRequisitionListForApproved @p0", userId).AsNoTracking().ToListAsync();
        }
        #endregion

    }
}
