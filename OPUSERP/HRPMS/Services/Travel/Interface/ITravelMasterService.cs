using OPUSERP.Areas.HRPMSTravle.Models;
using OPUSERP.HRPMS.Data.Entity.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel.Interface
{
   public interface ITravelMasterService
    {
        Task<int> SaveTravelMaster(TravelMaster travelMaster);
        Task<IEnumerable<TravelMaster>> GetTravelMaster();
        Task<TravelMaster> GetTravelMasterById(int id);
        Task<bool> DeleteTravelMasterById(int id);
        Task<bool> UpdateTravelApproval(int Id, int Type);
        Task<IEnumerable<TravelMaster>> GetTravelMasterByEmpId(int empId);
        Task<TravelReportModel> GetTravelReportByMasterId(int id);
    }
}
