using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;

using OPUSERP.VMS.Data.Entity.FuelStation;

using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.FuelStation
{
    public class FuelStationInfoService : IFuelStationInfoService
    {
        private readonly ERPDbContext _context;

        public FuelStationInfoService(ERPDbContext context)
        {
            _context = context;
        }

        #region Fuel Station
        public async Task<int> SaveFuelStationInfo(FuelStationInfo fuelStationInfo)
        {
            if (fuelStationInfo.Id != 0)
            {
                fuelStationInfo.updatedBy = fuelStationInfo.createdBy;
                fuelStationInfo.updatedAt = DateTime.Now;
                _context.FuelStationInfos.Update(fuelStationInfo);
            }
            else
            {
                fuelStationInfo.createdAt = DateTime.Now;
                _context.FuelStationInfos.Add(fuelStationInfo);
            }

            await _context.SaveChangesAsync();
            return fuelStationInfo.Id;
        }

        public async Task<IEnumerable<FuelStationInfo>> GetFuelStationInfo()
        {
            return await _context.FuelStationInfos.AsNoTracking().ToListAsync();
        }

        public async Task<FuelStationInfo> GetFuelStationInfoById(int id)
        {
            return await _context.FuelStationInfos.FindAsync(id);
        }

        public async Task<bool> DeleteFuelStationInfosById(int id)
        {
            _context.FuelStationInfos.Remove(_context.FuelStationInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Contact
        public async Task<int> Savecontact(VMSContact contact)
        {
            if (contact.Id != 0)
            {
                contact.updatedBy = contact.createdBy;
                contact.updatedAt = DateTime.Now;
                _context.VMSContacts.Update(contact);
            }
            else
            {
                contact.createdAt = DateTime.Now;
                _context.VMSContacts.Add(contact);
            }

            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<IEnumerable<VMSContact>> Getcontact()
        {
            return await _context.VMSContacts.AsNoTracking().AsNoTracking().ToListAsync();
        }
        //public async Task<IEnumerable<Contact>> Getcontactbystationid(int id)
        //{
        //    return await _context.Contacts.Where(x=>x.fuelStationInfoId==id).Include(x=>x.resource).AsNoTracking().ToListAsync();
        //}
        public async Task<IEnumerable<System.Object>> Getcontactbystationid(int Id)
        {
            var result = (from D in _context.VMSContacts
                          join I in _context.VMSResources on D.resourceID equals I.Id
                          join U in _context.designations on I.designationID equals U.Id
                          where D.fuelStationInfoId == Id
                          select new
                          {
                              D.Id,
                              D.fuelStationInfoId,
                              resourceId=I.Id,
                              I.nameEnglish,
                              I.phone,
                              I.mobile,
                              U.designationName,
                              designationId=U.Id
                          }).ToListAsync();

            return await result;
        }

        public async Task<VMSContact> GetcontactById(int id)
        {
            return await _context.VMSContacts.FindAsync(id);
        }

        public async Task<bool> DeletecontactById(int id)
        {
            _context.Contacts.Remove(_context.Contacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region FurlStatuionFurlInfo
        public async Task<int> SavestationFuelInfo(StationFuelInfo stationFuelInfo)
        {
            if (stationFuelInfo.Id != 0)
            {
                stationFuelInfo.updatedBy = stationFuelInfo.createdBy;
                stationFuelInfo.updatedAt = DateTime.Now;
                _context.StationFuelInfos.Update(stationFuelInfo);
            }
            else
            {
                stationFuelInfo.createdAt = DateTime.Now;
                _context.StationFuelInfos.Add(stationFuelInfo);
            }

            await _context.SaveChangesAsync();
            return stationFuelInfo.Id;
        }

        public async Task<IEnumerable<StationFuelInfo>> GetStationFuelInfo()
        {
            return await _context.StationFuelInfos.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<StationFuelInfo> GetStationFuelInfoById(int id)
        {
            return await _context.StationFuelInfos.FindAsync(id);
        }
        public async Task<IEnumerable<StationFuelInfo>> GetStationFuelInfobystationId(int Id)
        {
            return await _context.StationFuelInfos.Where(x=>x.fuelStationInfoId==Id).Include(x=>x.fuelType).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteStationFuelInfoById(int id)
        {
            _context.StationFuelInfos.Remove(_context.StationFuelInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteStationFuelInfoBystationId(int id)
        {
            IEnumerable<StationFuelInfo> stationFuelInfos =await _context.StationFuelInfos.Where(x => x.fuelStationInfoId == id).AsNoTracking().ToListAsync();
            foreach (StationFuelInfo data in stationFuelInfos)
            {
                _context.StationFuelInfos.Remove(_context.StationFuelInfos.Find(data.Id));
            }
           // _context.StationFuelInfos.Remove(_context.StationFuelInfos.Where(x=>x.fuelStationInfoId==id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Fuel Station Comment
        public async Task<int> SaveFuelStationComment(FuelStationComment fuelStationComment)
        {
            if (fuelStationComment.Id != 0)
            {
                fuelStationComment.updatedBy = fuelStationComment.createdBy;
                fuelStationComment.updatedAt = DateTime.Now;
                _context.FuelStationComments.Update(fuelStationComment);
            }
            else
            {
                fuelStationComment.createdAt = DateTime.Now;
                _context.FuelStationComments.Add(fuelStationComment);
            }

            await _context.SaveChangesAsync();
            return fuelStationComment.Id;
        }

        public async Task<IEnumerable<FuelStationComment>> GetCommentByFuelStationId(int vid)
        {
            return await _context.FuelStationComments.Where(x => x.fuelStationInfoId == vid).Include(x => x.fuelStationInfo).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<FuelStationComment> GetFuelStationCommentById(int id)
        {
            return await _context.FuelStationComments.FindAsync();
        }

        public async Task<bool> DeleteFuelStationCommentById(int id)
        {
            _context.FuelStationComments.Remove(_context.FuelStationComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion



    }
}
