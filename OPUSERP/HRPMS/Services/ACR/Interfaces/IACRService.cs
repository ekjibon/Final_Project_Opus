using OPUSERP.Areas.HRPMSACR.Models;
using OPUSERP.HRPMS.Data.Entity.ACR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.ACR.Interfaces
{
    public interface IACRService
    {
        #region ACR Authority

        Task<int> SaveACRAuthority(ACRAuthority aCRAuthority);
        Task<IEnumerable<ACRAuthority>> GetAllACRAuthority();
        Task<ACRAuthority> GetACRAuthorityById(int id);
        Task<bool> DeleteACRAuthorityById(int id);
        Task<IEnumerable<ACRAuthority>> GetACRByAuthorityId(int authID);

        #endregion

        #region Evaluation Master

        Task<int> SaveAcrEvaluationMaster(AcrEvaluationMaster acrEvaluationMaster);
        Task<IEnumerable<AcrEvaluationMaster>> GetAllAcrEvaluationMaster();
        Task<bool> DeleteAcrEvaluationMasterById(int id);
        Task<IEnumerable<AcrEvaluationMaster>> GetAcrEvaluationMasterById(int id);

        #endregion

        #region Evaluation Detail

        Task<int> SaveAcrEvaluationDetail(AcrEvaluationDetail acrEvaluationDetail);
        Task<IEnumerable<AcrEvaluationDetail>> GetAllAcrEvaluationDetail();
        Task<bool> DeleteAcrEvaluationDetailById(int id);
        Task<IEnumerable<AcrEvaluationDetail>> GetAcrEvaluationDetailByMasterId(int acrEvaluationMasterId);

        #endregion

        #region Evaluation Value

        Task<int> SaveAcrEvaluationValue(AcrEvaluationValue acrEvaluationValue);
        Task<bool> SaveAcrEvaluationValueList(IList<AcrEvaluationValue> acrEvaluationValue);        
        Task<IEnumerable<AcrEvaluationValue>> GetAllAcrAcrEvaluationValue();
        Task<bool> DeleteAcrEvaluationValueById(int acrInitiateId, int acrEvaluationDetailId);
        Task<IEnumerable<AcrEvaluationValue>> GetAcrEvaluationValueById(int id);
        IEnumerable<AcrTotalMarksViewModel> GetAcrTotalMarks(int acrInitiateId);
        Task<IEnumerable<AcrEvaluationValue>> GetSavedAcrEvaluationValueByAcrInitiateId(int acrInitiateId, int acrEvaluationMasterId);
        #endregion


        #region Acr Initiate

        Task<int> SaveAcrInitiate(AcrInitiate acrInitiate);
        Task<AcrInitiate> GetAcrInitiateInfoById(int id);
        Task<IEnumerable<AcrInitiate>> GetAcrInitiateInfoByAcrType(string acrType);
        Task<bool> DeleteAcrInitiateById(int id);
        Task<AcrInitiate> GetAcrPersonalDataByAcrInitiateId(int acrInitiateId);

        #endregion

        #region Health Info
        Task<int> SaveAcrHealthInfo(AcrHealthInfo acrHealthInfo);
        Task<AcrHealthInfo> GetAcrHealthInfoById(int id);
        Task<bool> DeleteAcrHealthInfoById(int id);
        Task<IEnumerable<AcrHealthInfo>> GetAcrHealthInfoByAcrInitiateId(int acrInitiateId);
        Task<AcrHealthInfo> GetHealthInfoByAcrInitiateId(int acrInitiateId);
        Task<IEnumerable<AcrHealthInfo>> GetAcrHealthInfo();
        #endregion

        #region Acr Personal Work desc/Bio data

        Task<int> SaveAcrPersonalWorkDescription(AcrPersonalWorkDescription acrPersonalWorkDescription);
        Task<AcrPersonalWorkDescription> GetAcrPersonalWorkDescriptionById(int id);
        Task<IEnumerable<AcrPersonalWorkDescription>> GetAcrPersonalWorkDescriptionByAcrInitiateId(int acrInitiateId);
        Task<bool> DeleteAcrPersonalWorkDescriptionById(int id);
        IEnumerable<CountNoofChildrenViewModel> GetemployeeNoofchild(int employeeId);
        #endregion

        #region Work History
        Task<int> SaveWorkHistory(WorkHistory workHistory);
        Task<WorkHistory> GetWorkHistoryById(int id);
        Task<bool> DeleteWorkHistoryById(int id);
        Task<IEnumerable<WorkHistory>> GetWorkHistoryByEmpId(int empId);
        #endregion

        #region Position
        Task<int> SavePosition(Position position);
        Task<Position> GetPositionById(int id);
        Task<bool> DeletePositionById(int id);
        Task<IEnumerable<Position>> GetPositionByEmpId(int empId);
        #endregion

        #region ACRSchedule
        Task<int> SaveACRSchedule(ACRSchedule aCRSchedule);
        Task<ACRSchedule> GetACRScheduleById(int id);
        Task<bool> DeleteACRScheduleById(int id);
        Task<IEnumerable<ACRSchedule>> GetACRScheduleByEmpId(int empId);
        #endregion

        #region ACRProcess
        Task<int> SaveACRProcess(ACRProcess aCRProcess);
        Task<ACRProcess> GetACRProcessById(int id);
        Task<bool> DeleteACRProcessById(int acrInitiateId);
        Task<IEnumerable<ACRProcess>> GetACRProcessByScheduleId(int scheduleId);
        #endregion
    }
}
