using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSTravle.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Travel;
using OPUSERP.HRPMS.Services.Travel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel
{
    public class TravelMasterService: ITravelMasterService
    {
        private readonly ERPDbContext _context;

        public TravelMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveTravelMaster(TravelMaster travelMaster)
        {
            if (travelMaster.Id != 0)
                _context.travelMasters.Update(travelMaster);
            else
                _context.travelMasters.Add(travelMaster);
             await _context.SaveChangesAsync();
            return travelMaster.Id;
        }

        public async Task<IEnumerable<TravelMaster>> GetTravelMaster()
        {
            return await _context.travelMasters.Include(x => x.travelProject).Include(x => x.hRDoner).Include(x => x.hRActivity).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TravelMaster>> GetTravelMasterByEmpId(int empId)
        {
            return await _context.travelMasters.Where(x=>x.employeeID ==empId).Include(x=>x.travelProject).Include(x => x.hRDoner).Include(x => x.hRActivity).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<TravelMaster> GetTravelMasterById(int id)
        {

            return await _context.travelMasters.FindAsync(id);
        }

        public async Task<TravelReportModel> GetTravelReportByMasterId(int id)
        {
            var data= await _context.Companies.OrderBy(a => a.Id).Take(1).AsNoTracking().ToListAsync();
            TravelReportModel travelReportModel = new TravelReportModel();
            travelReportModel.company = data.FirstOrDefault();
            travelReportModel.travelMaster =  await _context.travelMasters.Where(x=>x.Id==id).Include(x=>x.hRDoner).Include(x=>x.hRActivity).Include(x=>x.travelProject).FirstOrDefaultAsync();
            travelReportModel.travellerInfos = await _context.travellerInfos.Where(x => x.travelMasterId == id).Include(x => x.employee.department).ToListAsync();
            travelReportModel.travellingInfos = await _context.travellingInfos.Where(x => x.travelMasterId == id).Include(x => x.travelVehicleType).ToListAsync();
            travelReportModel.travelStatusLogs = await _context.travelStatusLogs.Where(x => x.travelMasterId == id).Include(x => x.employee.branch).ToListAsync();

            var empInfo = await _context.travelMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            int empId = empInfo.employeeID;
            var appMaster= await _context.approvalMasters.Where(x => x.employeeInfoId == empId && x.approvalType.approvalTypeName== "Travel").FirstOrDefaultAsync();
            int mstId = appMaster.Id;
            travelReportModel.approvalDetail = await _context.approvalDetails.Where(x => x.approvalMasterId == mstId).Include(x => x.approver).FirstOrDefaultAsync();             

            return travelReportModel;
        }

        public async Task<bool> DeleteTravelMasterById(int id)
        {
            _context.travelMasters.Remove(_context.travelMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTravelApproval(int Id, int Type)
        {
            TravelMaster data = await _context.travelMasters.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                _context.travelMasters.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

    }
}
