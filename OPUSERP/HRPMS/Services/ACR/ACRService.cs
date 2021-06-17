using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSACR.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Services.ACR.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.ACR
{
    public class ACRService : IACRService
    {
        private readonly ERPDbContext _context;

        public ACRService(ERPDbContext context)
        {
            _context = context;
        }

        #region ACR Authority
        public async Task<bool> DeleteACRAuthorityById(int id)
        {
            _context.aCRAuthorities.Remove(_context.aCRAuthorities.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<ACRAuthority> GetACRAuthorityById(int id)
        {
            return await _context.aCRAuthorities.Where(x => x.Id == id).FirstAsync();
        }

        public Task<IEnumerable<ACRAuthority>> GetACRByAuthorityId(int authID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ACRAuthority>> GetAllACRAuthority()
        {
            return await _context.aCRAuthorities.OrderBy(x => x.CADesignationName).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveACRAuthority(ACRAuthority aCRAuthority)
        {
            if (aCRAuthority.Id != 0)
                _context.aCRAuthorities.Update(aCRAuthority);
            else
                _context.aCRAuthorities.Add(aCRAuthority);

            await _context.SaveChangesAsync();
            return aCRAuthority.Id;
        }
        #endregion

        #region Evaluation Master

        public async Task<int> SaveAcrEvaluationMaster(AcrEvaluationMaster acrEvaluationMaster)
        {
            if (acrEvaluationMaster.Id != 0)
                _context.acrEvaluationMasters.Update(acrEvaluationMaster);
            else
                _context.acrEvaluationMasters.Add(acrEvaluationMaster);

            await _context.SaveChangesAsync();
            return acrEvaluationMaster.Id;
        }

        public async Task<IEnumerable<AcrEvaluationMaster>> GetAllAcrEvaluationMaster()
        {
            return await _context.acrEvaluationMasters.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAcrEvaluationMasterById(int id)
        {
            _context.acrEvaluationMasters.Remove(_context.acrEvaluationMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AcrEvaluationMaster>> GetAcrEvaluationMasterById(int id)
        {
            return await _context.acrEvaluationMasters.Where(x => x.Id == id).AsNoTracking().ToListAsync();
        }


        #endregion

        #region Evaluation Details

        public async Task<int> SaveAcrEvaluationDetail(AcrEvaluationDetail acrEvaluationDetail)
        {
            if (acrEvaluationDetail.Id != 0)
                _context.acrEvaluationDetails.Update(acrEvaluationDetail);
            else
                _context.acrEvaluationDetails.Add(acrEvaluationDetail);

            await _context.SaveChangesAsync();
            return acrEvaluationDetail.Id;
        }

        public async Task<IEnumerable<AcrEvaluationDetail>> GetAllAcrEvaluationDetail()
        {
            return await _context.acrEvaluationDetails.Include(a=>a.acrEvaluationMaster).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAcrEvaluationDetailById(int id)
        {
            _context.acrEvaluationDetails.Remove(_context.acrEvaluationDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AcrEvaluationDetail>> GetAcrEvaluationDetailByMasterId(int acrEvaluationMasterId)
        {
            return await _context.acrEvaluationDetails.Where(x => x.acrEvaluationMasterId == acrEvaluationMasterId).AsNoTracking().ToListAsync();
        }


        #endregion

        #region Evaluation Value

        public async Task<int> SaveAcrEvaluationValue(AcrEvaluationValue acrEvaluationValue)
        {
            if (acrEvaluationValue.Id != 0)
                _context.acrEvaluationValues.Update(acrEvaluationValue);
            else
                _context.acrEvaluationValues.Add(acrEvaluationValue);
            await _context.SaveChangesAsync();
            return acrEvaluationValue.Id;
        }

        public async Task<bool> SaveAcrEvaluationValueList(IList<AcrEvaluationValue> acrEvaluationValue)
        {           
            try
            {
                _context.acrEvaluationValues.AddRange(acrEvaluationValue);
                return 1 == await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

        public async Task<IEnumerable<AcrEvaluationValue>> GetAllAcrAcrEvaluationValue()
        {
            return await _context.acrEvaluationValues.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAcrEvaluationValueById(int acrInitiateId,int acrEvaluationDetailId)
        {
            _context.acrEvaluationValues.RemoveRange(_context.acrEvaluationValues.Where(x => x.acrInitiateId == acrInitiateId && x.acrEvaluationDetailId == acrEvaluationDetailId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AcrEvaluationValue>> GetAcrEvaluationValueById(int id)
        {
            return await _context.acrEvaluationValues.Where(x => x.Id == id).AsNoTracking().ToListAsync();
        }

        public IEnumerable<AcrTotalMarksViewModel> GetAcrTotalMarks(int acrInitiateId)
        {
            return _context.acrTotalMarksViewModels.FromSql(@"call spGetAcrTotalMarks ({0})", acrInitiateId).ToList();
        }

        public async Task<IEnumerable<AcrEvaluationValue>> GetSavedAcrEvaluationValueByAcrInitiateId(int acrInitiateId, int acrEvaluationMasterId)
        {
            return await _context.acrEvaluationValues.Where(x => x.acrInitiateId == acrInitiateId && x.acrEvaluationDetail.acrEvaluationMaster.Id == acrEvaluationMasterId).Include(x => x.acrEvaluationDetail).AsNoTracking().ToListAsync();
        }
        #endregion


        #region  ACR Initiate

        public async Task<int> SaveAcrInitiate(AcrInitiate acrInitiate)
        {
            if (acrInitiate.Id != 0)
                _context.acrInitiates.Update(acrInitiate);
            else
                _context.acrInitiates.Add(acrInitiate);

            await _context.SaveChangesAsync();
            return acrInitiate.Id;
        }

        public async Task<AcrInitiate> GetAcrInitiateInfoById(int id)
        {
            return await _context.acrInitiates.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<AcrInitiate>> GetAcrInitiateInfoByAcrType(string acrType)
        {
            return await _context.acrInitiates.Where(x => x.acrType == acrType).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAcrInitiateById(int id)
        {
            _context.acrInitiates.Remove(_context.acrInitiates.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<AcrInitiate> GetAcrPersonalDataByAcrInitiateId(int acrInitiateId)
        {
            return await _context.acrInitiates.Where(x => x.Id == acrInitiateId).Include(x => x.employee).AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion

        #region Health
        public async Task<bool> DeleteAcrHealthInfoById(int id)
        {
            _context.acrHealthInfos.Remove(_context.acrHealthInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<AcrHealthInfo> GetAcrHealthInfoById(int id)
        {
            return await _context.acrHealthInfos.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<AcrHealthInfo>> GetAcrHealthInfoByAcrInitiateId(int acrInitiateId)
        {
            return await _context.acrHealthInfos.Where(x=>x.acrInitiateId == acrInitiateId).Include(x => x.acrInitiate).Include(x => x.acrInitiate.employee).AsNoTracking().ToListAsync();
        }

        public async Task<AcrHealthInfo> GetHealthInfoByAcrInitiateId(int acrInitiateId)
        {
            return await _context.acrHealthInfos.Where(x => x.acrInitiateId == acrInitiateId).Include(x => x.acrInitiate).Include(x => x.acrInitiate.employee).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> SaveAcrHealthInfo(AcrHealthInfo acrHealthInfo)
        {
            if (acrHealthInfo.Id != 0)
                _context.acrHealthInfos.Update(acrHealthInfo);
            else
                _context.acrHealthInfos.Add(acrHealthInfo);

            await _context.SaveChangesAsync();
            return acrHealthInfo.Id;
        }

        public async Task<IEnumerable<AcrHealthInfo>> GetAcrHealthInfo()
        {
            return await _context.acrHealthInfos.AsNoTracking().Include(x => x.acrInitiate).Include(x => x.acrInitiate.employee).ToListAsync();
        }
        #endregion

        #region  Acr Personal Work desc/Bio data

        public async Task<int> SaveAcrPersonalWorkDescription(AcrPersonalWorkDescription acrPersonalWorkDescription)
        {
            if (acrPersonalWorkDescription.Id != 0)
                _context.acrPersonalWorkDescriptions.Update(acrPersonalWorkDescription);
            else
                _context.acrPersonalWorkDescriptions.Add(acrPersonalWorkDescription);

            await _context.SaveChangesAsync();
            return acrPersonalWorkDescription.Id;
        }

        public async Task<AcrPersonalWorkDescription> GetAcrPersonalWorkDescriptionById(int id)
        {
            return await _context.acrPersonalWorkDescriptions.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<AcrPersonalWorkDescription>> GetAcrPersonalWorkDescriptionByAcrInitiateId(int acrInitiateId)
        {
            return await _context.acrPersonalWorkDescriptions.Where(x => x.acrInitiateId == acrInitiateId).Include(x => x.acrInitiate).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAcrPersonalWorkDescriptionById(int id)
        {
            _context.acrPersonalWorkDescriptions.Remove(_context.acrPersonalWorkDescriptions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       

        public IEnumerable<CountNoofChildrenViewModel> GetemployeeNoofchild(int employeeId)
        {
            return  _context.countNoofChildrenViewModels.FromSql(@"call spGetNoofChildren ({0})", employeeId).ToList();
        }

        #endregion

        #region Work history
        public async Task<bool> DeleteWorkHistoryById(int id)
        {
            _context.workHistories.Remove(_context.workHistories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<WorkHistory> GetWorkHistoryById(int id)
        {
            return await _context.workHistories.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<WorkHistory>> GetWorkHistoryByEmpId(int empId)
        {
            return await _context.workHistories.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveWorkHistory(WorkHistory workHistory)
        {
            if (workHistory.Id != 0)
                _context.workHistories.Update(workHistory);
            else
                _context.workHistories.Add(workHistory);

            await _context.SaveChangesAsync();
            return workHistory.Id;
        }
        #endregion

        #region Position
        public async Task<bool> DeletePositionById(int id)
        {
            _context.positions.Remove(_context.positions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Position> GetPositionById(int id)
        {
            return await _context.positions.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<Position>> GetPositionByEmpId(int empId)
        {
            return await _context.positions.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<int> SavePosition(Position position)
        {
            if (position.Id != 0)
                _context.positions.Update(position);
            else
                _context.positions.Add(position);

            await _context.SaveChangesAsync();
            return position.Id;
        }
        #endregion

        #region ACRSchedole
        public async Task<bool> DeleteACRScheduleById(int id)
        {
            _context.aCRSchedules.Remove(_context.aCRSchedules.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<ACRSchedule> GetACRScheduleById(int id)
        {
            return await _context.aCRSchedules.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<ACRSchedule>> GetACRScheduleByEmpId(int empId)
        {
            return await _context.aCRSchedules.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveACRSchedule(ACRSchedule aCRSchedule)
        {
            if (aCRSchedule.Id != 0)
                _context.aCRSchedules.Update(aCRSchedule);
            else
                _context.aCRSchedules.Add(aCRSchedule);

            await _context.SaveChangesAsync();
            return aCRSchedule.Id;
        }
        #endregion

        #region ACRProcess
        public async Task<bool> DeleteACRProcessById(int id)
        {
            _context.aCRProcesses.Remove(_context.aCRProcesses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<ACRProcess> GetACRProcessById(int id)
        {
            return await _context.aCRProcesses.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<ACRProcess>> GetACRProcessByScheduleId(int scheduleId)
        {
            return await _context.aCRProcesses.Where(x => x.acrSeduleId == scheduleId).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveACRProcess(ACRProcess aCRProcess)
        {
            if (aCRProcess.Id != 0)
                _context.aCRProcesses.Update(aCRProcess);
            else
                _context.aCRProcesses.Add(aCRProcess);

            await _context.SaveChangesAsync();
            return aCRProcess.Id;
        }

        



        #endregion

    }
}
