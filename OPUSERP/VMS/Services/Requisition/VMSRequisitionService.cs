using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Requisition;
using OPUSERP.VMS.Services.Requisition.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.Requisition
{
    public class VMSRequisitionService: IVMSRequisitionService
    {
        private readonly ERPDbContext _context;

        public VMSRequisitionService(ERPDbContext context)
        {
            _context = context;
        }
        #region Requisition master
        public async Task<int> SaverequisitionMaster(VMSRequisitionMaster requisitionMaster)
        {
            if (requisitionMaster.Id != 0)
            {
                requisitionMaster.updatedBy = requisitionMaster.createdBy;
                requisitionMaster.createdAt = requisitionMaster.createdAt;
                requisitionMaster.updatedAt = DateTime.Now;
                _context.VMSRequisitionMasters.Update(requisitionMaster);
            }
            else
            {
                requisitionMaster.createdAt = DateTime.Now;
                _context.VMSRequisitionMasters.Add(requisitionMaster);
            }

            await _context.SaveChangesAsync();
            return requisitionMaster.Id;
        }

        public async Task<IEnumerable<VMSRequisitionMaster>> GetrequisitionMaster()
        {
            return await _context.VMSRequisitionMasters.AsNoTracking().ToListAsync();
        }
       
        public async Task<VMSRequisitionMaster> GetrequisitionMasterById(int id)
        {
            return await _context.VMSRequisitionMasters .Where(x => x.Id == id).Include(x=>x.vehicleType).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleterequisitionMasterById(int id)
        {
            _context.VMSRequisitionMasters.Remove(_context.VMSRequisitionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Requisition Detail
        public async Task<int> SaverequisitionDetail(VMSRequisitionDetails requisitionDetails)
        {
            if (requisitionDetails.Id != 0)
            {
                requisitionDetails.updatedBy = requisitionDetails.createdBy;
                requisitionDetails.createdAt = requisitionDetails.createdAt;
                requisitionDetails.updatedAt = DateTime.Now;
                _context.VMSRequisitionDetails.Update(requisitionDetails);
            }
            else
            {
                requisitionDetails.createdAt = DateTime.Now;
                _context.VMSRequisitionDetails.Add(requisitionDetails);
            }

            await _context.SaveChangesAsync();
            return requisitionDetails.Id;
        }

        public async Task<IEnumerable<VMSRequisitionDetails>> GetrequisitionDetails()
        {
            return await _context.VMSRequisitionDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VMSRequisitionDetails>> GetrequisitionDetailsbyMasterId(int masterId)
        {
            return await _context.VMSRequisitionDetails.Where(x=>x.requisitionId== masterId).Include(x=>x.requisition).AsNoTracking().ToListAsync();
        }

        public async Task<VMSRequisitionDetails> GetrequisitionDetailsById(int id)
        {
            return await _context.VMSRequisitionDetails.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleterequisitionDetailsById(int id)
        {
            _context.VMSRequisitionDetails.Remove(_context.VMSRequisitionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleterequisitionDetailsByMasterId(int id)
        {
            _context.VMSRequisitionDetails.RemoveRange(_context.VMSRequisitionDetails.Where(x=>x.requisitionId==id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region Requisition Comment
        public async Task<int> SaveRequisitionComment(RequisitionComment requisitionComment)
        {
            if (requisitionComment.Id != 0)
            {
                requisitionComment.updatedBy = requisitionComment.createdBy;
                requisitionComment.updatedAt = DateTime.Now;
                _context.RequisitionComments.Update(requisitionComment);
            }
            else
            {
                requisitionComment.createdAt = DateTime.Now;
                _context.RequisitionComments.Add(requisitionComment);
            }

            await _context.SaveChangesAsync();
            return requisitionComment.Id;
        }

        public async Task<IEnumerable<RequisitionComment>> GetCommentByRequisitionId(int vid)
        {
            return await _context.RequisitionComments.Where(x => x.requisitionMasterId == vid).Include(x => x.requisitionMaster).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<RequisitionComment> GetRequisitionCommentById(int id)
        {
            return await _context.RequisitionComments.FindAsync();
        }

        public async Task<bool> DeleteRequisitionCommentById(int id)
        {
            _context.RequisitionComments.Remove(_context.RequisitionComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
