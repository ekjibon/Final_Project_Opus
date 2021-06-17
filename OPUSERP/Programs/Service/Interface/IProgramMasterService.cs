using OPUSERP.Areas.Program.Models;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramMasterService
    {
        #region ProgramMaster
        Task<int> SaveProgramMaster(ProgramMaster programCategory);
        Task<IEnumerable<ProgramMaster>> GetProgramMaster();
        Task<ProgramMaster> GetProgramMasterById(int id);
        Task<bool> DeleteProgramMasterById(int id);
        Task<ProgramReportModel> GetProgramReportByMasterId(int id);
        #endregion

        #region location
        Task<int> SaveProgramLocation(ProgramLocation programMaster);
        Task<IEnumerable<ProgramLocation>> GetProgramLocation();
        Task<ProgramLocation> GetProgramLocationById(int id);
        Task<IEnumerable<ProgramLocation>> GetProgramLocationBymasterId(int id);
        Task<IEnumerable<ProgramLocation>> GetAllProgramLocationBymasterId(int masterId);
        Task<bool> DeleteProgramLocationById(int id);
        Task<bool> DeleteProgramLocationByMasterId(int id);
        #endregion

        #region Implementor
        Task<int> SaveProgramImplementor(ProgramImplementor programMaster);
        Task<IEnumerable<ProgramImplementor>> GetProgramImplementor();
        Task<ProgramImplementor> GetProgramImplementorById(int id);
        Task<IEnumerable<ProgramImplementor>> GetProgramImplementorBymasterId(int id);
        Task<IEnumerable<ProgramImplementor>> GetAllProgramImplementorBymasterId(int masterId);
        Task<bool> DeleteProgramImplementorById(int id);
        Task<bool> DeleteProgramImplementorByMasterId(int id);
        #endregion

        #region SourceFund
        Task<int> SaveProgramSourceFund(ProgramSourceFund programMaster);
        Task<IEnumerable<ProgramSourceFund>> GetProgramSourceFund();
        Task<ProgramSourceFund> GetProgramSourceFundById(int id);
        Task<IEnumerable<ProgramSourceFund>> GetProgramSourceFundBymasterId(int id);
        Task<IEnumerable<ProgramSourceFund>> GetAllProgramSourceFundBymasterId(int masterId);
        Task<bool> DeleteProgramSourceFundById(int id);
        Task<bool> DeleteProgramSourceFundByMasterId(int id);
        #endregion

        #region SourceFund Approve
        Task<int> SaveProgramSourceFundApprove(ProgramSourceFundApprove programMaster);

        Task<IEnumerable<ProgramSourceFundApprove>> GetProgramSourceFundApprove();

        Task<ProgramSourceFundApprove> GetProgramSourceFundApproveById(int id);
        Task<IEnumerable<ProgramSourceFundApprove>> GetProgramSourceFundApproveBymasterId(int id);
        Task<bool> DeleteProgramSourceFundApproveById(int id);
        Task<bool> DeleteProgramSourceFundApproveByMasterId(int id);
        #endregion

    }
}
