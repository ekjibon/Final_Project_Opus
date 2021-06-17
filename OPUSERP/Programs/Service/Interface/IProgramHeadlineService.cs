using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramHeadlineService
    {
        Task<int> SaveProgramHeadline(ProgramHeadline programHeadline);
        Task<IEnumerable<ProgramHeadline>> GetProgramHeadline();
        Task<ProgramHeadline> GetProgramHeadlineById(int id);
        Task<bool> DeleteProgramHeadlineById(int id);
        #region outcome
        Task<int> SaveProgramOutCome(ProgramOutCome programHeadline);
        Task<bool> UpdateProgramOutCome(int? id, int statusid, string updateBy);
        Task<IEnumerable<ProgramOutCome>> GetProgramOutCome();
        Task<ProgramOutCome> GetProgramOutComeById(int id);
        Task<IEnumerable<ProgramOutCome>> GetProgramOutComebymasterid(int? id);
        Task<bool> DeleteProgramOutComeById(int id);
        #endregion
        #region output
        Task<int> SaveProgramOutPut(ProgramOutPut programHeadline);
        Task<bool> UpdateProgramOutPut(int? id, int statusid, string updateBy);
        Task<IEnumerable<ProgramOutPut>> GetProgramOutPut();
        Task<IEnumerable<ProgramOutPut>> GetProgramOutPutbymastercomeid(int masterid, int outcomeid);
        Task<IEnumerable<ProgramOutPut>> GetProgramOutPutbymasterid(int masterid);
        Task<ProgramOutPut> GetProgramOutPutById(int id);
        Task<bool> DeleteProgramOutPutById(int id);
        #endregion
        #region Indicator
        Task<int> SaveProgramIndicator(ProgramIndicator programHeadline);
        Task<IEnumerable<ProgramIndicator>> GetProgramIndicator();
        Task<ProgramIndicator> GetProgramIndicatorById(int id);
        Task<IEnumerable<ProgramIndicator>> GetProgramIndicatormasteridoutcomeid(int masterid, int outcomeid);
        Task<IEnumerable<ProgramIndicator>> GetProgramIndicatormasterid(int masterid);
        Task<bool> DeleteProgramIndicatorById(int id);
        #endregion

        #region Activity
        Task<int> SaveProgramActivity(ProgramActivity programHeadline);
        Task<bool> UpdateProgramActivity(int? id, string description, string updateBy);
        Task<IEnumerable<ProgramActivity>> GetProgramActivity();
        Task<ProgramActivity> GetProgramActivityById(int id);
        Task<IEnumerable<ProgramActivity>> GetProgramActivitybymasterId(int masterId);
        Task<IEnumerable<ProgramActivity>> GetActivityDetailsByMasterId(int masterId);
        Task<bool> DeleteProgramActivityById(int id);
        Task<bool> DeleteProgramActivityByMasterId(int masterId);
        #endregion
        #region Activityprogree
        Task<int> SaveProgramActivityProgres(ProgramActivityProgres programHeadline);
        Task<IEnumerable<ProgramActivityProgres>> GetProgramActivityProgres();
        Task<IEnumerable<ProgramActivityProgres>> GetProgramActivityProgresbyactivityId(int masterId);
        Task<IEnumerable<ProgramActivityProgres>> GetProgramActivityProgresbymasterId(int masterId);
        Task<ProgramActivityProgres> GetProgramActivityProgresById(int id);
        Task<bool> DeleteProgramActivityProgresById(int id);
        Task<bool> DeleteProgramActivityProgresByActivityId(int id);
        #endregion

        #region outputprogree
        Task<int> SaveProgramOutputProgres(ProgramOutPutProgres programHeadline);
        Task<IEnumerable<ProgramOutPutProgres>> GetProgramOutPutProgres();
        Task<IEnumerable<ProgramOutPutProgres>> GetProgramOutPutProgresbyOutPutId(int masterId);
        Task<ProgramOutPutProgres> GetProgramOutPutProgresById(int id);
        Task<bool> DeleteProgramOutPutProgresById(int id);
        Task<bool> DeleteProgramOutPutProgresByOutPutId(int id);
        #endregion
        #region outcomeprogree
        Task<int> SaveProgramOutComeProgres(ProgramOutComeProgres programHeadline);
        Task<IEnumerable<ProgramOutComeProgres>> GetProgramOutComeProgres();
        Task<IEnumerable<ProgramOutComeProgres>> GetProgramOutComeProgresbyOutPutId(int masterId);
        Task<ProgramOutComeProgres> GetProgramOutComeProgresById(int id);
        Task<bool> DeleteProgramOutComeProgresById(int id);
        Task<bool> DeleteProgramOutComeProgresByOutPutId(int id);
        #endregion
    }
}
