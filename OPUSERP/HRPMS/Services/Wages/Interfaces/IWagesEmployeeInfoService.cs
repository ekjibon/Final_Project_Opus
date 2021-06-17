using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Wages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesOTSetupMasterService
    {
        #region OTMaster
        Task<bool> DeleteOTSetupMasterById(int id);
        Task<IEnumerable<OTSetupMaster>> GetOTSetupMaster();
        Task<OTSetupMaster> GetOTSetupMasterById(int id);
        Task<int> SaveoTSetupMaster(OTSetupMaster oTSetupMaster);
        #endregion
        #region OTDetail
        Task<bool> DeleteOTSetupDetailById(int id);
        Task<bool> DeleteOTSetupDetailByMasterId(int id);

        Task<IEnumerable<OTSetupDetail>> GetOTSetupDetail();
        Task<OTSetupDetail> GetOTSetupDetailById(int id);
        Task<IEnumerable<OTSetupDetail>> GetOTSetupDetailByMasterId(int id);
        Task<int> SaveoTSetupDetail(OTSetupDetail oTSetupDetail);
        #endregion
        #region OTSlotYpe
        Task<bool> DeleteOTSloteById(int id);
        Task<IEnumerable<OTSlotType>> GetOTSlotType();
        Task<int> SaveOTSlotType(OTSlotType oTSlotType);
        #endregion

    }
}
