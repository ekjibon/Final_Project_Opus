using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.HRPMS.Services.Employee
{
    public class WagesOTSetupMasterService:IWagesOTSetupMasterService
    {
        private readonly ERPDbContext _context;

        public WagesOTSetupMasterService(ERPDbContext context)
        {
            _context = context;
        }

        //EmployeeInfo
        #region OTMaster
        public async Task<bool> DeleteOTSetupMasterById(int id)
        {
            _context.oTSetupMasters.Remove(_context.oTSetupMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OTSetupMaster>> GetOTSetupMaster()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.oTSetupMasters.Include(x=>x.shiftGroupMaster).ToListAsync();
        }
        

        public async Task<OTSetupMaster> GetOTSetupMasterById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.oTSetupMasters.Include(x => x.shiftGroupMaster).Where(x=>x.Id == id).FirstAsync();
        }

        public async Task<int> SaveoTSetupMaster(OTSetupMaster oTSetupMaster)
        {
            if (oTSetupMaster.Id != 0)
                _context.oTSetupMasters.Update(oTSetupMaster);
            else
                _context.oTSetupMasters.Add(oTSetupMaster);
            await _context.SaveChangesAsync();
            return oTSetupMaster.Id;
        }
        #endregion
        #region OTDetails
        public async Task<bool> DeleteOTSetupDetailById(int id)
        {
            _context.oTSetupDetails.Remove(_context.oTSetupDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteOTSetupDetailByMasterId(int id)
        {
            _context.oTSetupDetails.RemoveRange(_context.oTSetupDetails.Where(x=>x.oTSetupMasterId==id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OTSetupDetail>> GetOTSetupDetail()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.oTSetupDetails.Include(x => x.oTSetupMaster).ToListAsync();
        }


        public async Task<OTSetupDetail> GetOTSetupDetailById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.oTSetupDetails.Include(x => x.oTSetupMaster).Where(x => x.Id == id).FirstAsync();
        }
        public async Task<IEnumerable<OTSetupDetail>> GetOTSetupDetailByMasterId(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.oTSetupDetails.Include(x=>x.oTSlotType).Include(x => x.oTSetupMaster).Where(x => x.oTSetupMasterId == id).ToListAsync();
        }

        public async Task<int> SaveoTSetupDetail(OTSetupDetail oTSetupDetail)
        {
            if (oTSetupDetail.Id != 0)
                _context.oTSetupDetails.Update(oTSetupDetail);
            else
                _context.oTSetupDetails.Add(oTSetupDetail);
            await _context.SaveChangesAsync();
            return oTSetupDetail.Id;
        }
        #endregion
        #region OTSlotType
        public async Task<bool> DeleteOTSloteById(int id)
        {
            _context.oTSlotTypes.Remove(_context.oTSlotTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        

        public async Task<IEnumerable<OTSlotType>> GetOTSlotType()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.oTSlotTypes.ToListAsync();
        }


      

        public async Task<int> SaveOTSlotType(OTSlotType oTSlotType)
        {
            if (oTSlotType.Id != 0)
                _context.oTSlotTypes.Update(oTSlotType);
            else
                _context.oTSlotTypes.Add(oTSlotType);
            await _context.SaveChangesAsync();
            return oTSlotType.Id;
        }
        #endregion



    }
}
