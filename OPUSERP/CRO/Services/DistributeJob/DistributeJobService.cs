using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Services.DistributeJob
{
    public class DistributeJobService : IDistributeJobService
    {
        private readonly ERPDbContext _context;

        public DistributeJobService(ERPDbContext context)
        {
            _context = context;
        }

        #region OperationMaster
        public async Task<int> SaveOperationMaster(OperationMaster operationMaster )
        {
            if (operationMaster.Id != 0)
                _context.operationMasters.Update(operationMaster);
            else
                _context.operationMasters.Add(operationMaster);

            await _context.SaveChangesAsync();
            return operationMaster.Id;
        }
        public async Task<bool> UpdateOperationMaster(int? id, int? leadprogressstatus, string updateBy)
        {
            var VoucherMasters = _context.operationMasters.Find(id);
            VoucherMasters.jobStatusId = leadprogressstatus;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;
    
            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<OperationMaster>> GetAllOperationMaster()
        {
            return await _context.operationMasters.Include(x => x.agreement).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetAllOperationMasterBySp()
        {
            return await _context.getOMByassignToReviewerViewModels.FromSql($"SP_GetAllOperationMaster").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OperationMasterSPViewModel>> OperationMasterSPViewModels(string fromDate,string todate)
        {
            IEnumerable<OperationMasterSPViewModel> rVDetailViewModels = await _context.operationMasterSPViewModels.FromSql($"getdataforarchieve {fromDate},{todate}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPViewModel>> OperationMasterSPViewModelscom(string fromDate,string todate)
        {
            IEnumerable<OperationMasterSPViewModel> rVDetailViewModels = await _context.operationMasterSPViewModels.FromSql($"getdataforarchievedcom {fromDate},{todate}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPJAViewModel>> OperationMasterSPJAViewModels(string fromDate, string todate,string assignTo)
        {
            IEnumerable<OperationMasterSPJAViewModel> rVDetailViewModels = await _context.operationMasterSPJAViewModels.FromSql($"getdataforassign {fromDate},{todate},{assignTo}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPHoldViewModel>> OperationMasterSPHoldViewModels(string fromDate, string todate)
        {
            IEnumerable<OperationMasterSPHoldViewModel> rVDetailViewModels = await _context.operationMasterSPHoldViewModels.FromSql($"getdataforholdlist {fromDate},{todate}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPJAViewModel>> OperationMasterSPUAViewModels(string fromDate, string todate)
        {
            IEnumerable<OperationMasterSPJAViewModel> rVDetailViewModels = await _context.operationMasterSPJAViewModels.FromSql($"getdataforreassign {fromDate},{todate}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPBlockUnViewModel>> OperationMasterSPUnBlockViewModels(string fromDate, string todate,int statusid)
        {
            IEnumerable<OperationMasterSPBlockUnViewModel> rVDetailViewModels = await _context.operationMasterSPBlockUnViewModels.FromSql($"getdataforUnBlocklist {fromDate},{todate},{statusid}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPBlockUnViewModel>> OperationMasterSPBlockViewModels(string fromDate, string todate, int statusid)
        {
            IEnumerable<OperationMasterSPBlockUnViewModel> rVDetailViewModels = await _context.operationMasterSPBlockUnViewModels.FromSql($"getdataforBlocklist {fromDate},{todate},{statusid}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<OperationMasterSPRateViewModel>> OperationMasterSPRateViewModels(string fromDate, string todate)
        {
            IEnumerable<OperationMasterSPRateViewModel> rVDetailViewModels = await _context.operationMasterSPRateViewModels.FromSql($"getdataforratinglist {fromDate},{todate}").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }
        public async Task<IEnumerable<PreviousRatingDataViewModel>> PreviousRatingDataViewModels()
        {
            IEnumerable<PreviousRatingDataViewModel> rVDetailViewModels = await _context.previousRatingDataViewModels.FromSql($"getdataforrating").AsNoTracking().ToListAsync();
            return rVDetailViewModels;
        }



        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByTeamLeader(string TeamLeader)
        {
            
            List<int?> declarations = new List<int?> { 2, 7,4 };
            return await _context.operationMasters.Include(x => x.agreement).Where(x => x.assignTeamLeader == TeamLeader && declarations.Contains(x.declaration)).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetOperationMasterByTeamLeaderSp(string TeamLeader)
        {
            return await _context.getOMByassignToReviewerViewModels.FromSql($"SP_GetOperationMasterByTeamLeader {TeamLeader}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByTLDistribuiteJob(string TeamLeader)
        {
            List<int?> declarations = new List<int?> { 3 };
            return await _context.operationMasters.Include(x => x.agreement).Where(x => x.assignTeamLeader == TeamLeader && declarations.Contains(x.declaration)).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByFAEDeclaration(string Empcode)
        {
            List<int?> declarations = new List<int?> { 4 };
            return await _context.operationMasters.Include(x => x.agreement.ratingCategory).Where(x => x.assignTo == Empcode && declarations.Contains(x.declaration)).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByAssignTo(string assignTo)
        {
            List<int?> declarations = new List<int?> { 3, 5, 7, 15, 19, 21 };
            List<int?> statusId = new List<int?> { 41, 2053 };

            return await _context.operationMasters.Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).Where(x => (x.assignTo == assignTo || x.assignTeamLeader==assignTo) && declarations.Contains(x.declaration) && !statusId.Contains(x.jobStatusId)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetOperationMasterByAssignToBySp(string assignTo)
        {
            return await _context.getOMByassignToReviewerViewModels.FromSql($"SP_GetOperationMasterByAssignTo {assignTo}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOMByassignToClosedViewModel>> GetOMByassignToClosedViewModels(string assignTo)
        {
            return await _context.getOMByassignToClosedViewModels.FromSql($"SP_GetOperationMasterByAssignToClosed {assignTo}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOMByassignToDeclaredViewModel>> GetOMByassignToDeclareViewModels(string assignTo)
        {
            return await _context.getOMByassignToDeclaredViewModels.FromSql($"SP_GetOperationMasterByAssignToDeclared {assignTo}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetAgreementInfoViewModel>> GetAgreementInfoViewModels(int Id)
        {
            return await _context.getAgreementInfoViewModels.FromSql($"SP_GetagrementInfo {Id}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GetLeadInfoInCroViewModel>> GetLeadInfoInCroByOPMstId(int opMstId)
        {
            return await _context.getLeadInfoInCroViewModels.FromSql($"SP_GetLeadInfoInCroByOPMstId {opMstId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByassignToReviewer(string assignToReviewer)
        {
            List<int?> declarations = new List<int?> {15 };
            List<int?> statusId = new List<int?> { 2057 };

            return await _context.operationMasters.Include(x => x.agreement).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).Where(x => (x.assignToReviewer == assignToReviewer ) && declarations.Contains(x.declaration) && statusId.Contains(x.jobStatusId)).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByassignToReviewerTL(string assignToReviewer)
        {
            List<int?> declarations = new List<int?> { 15 };
            List<int?> statusId = new List<int?> { 12 };

            return await _context.operationMasters.Include(x => x.agreement).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).Where(x => (x.assignTeamLeader == assignToReviewer) && statusId.Contains(x.jobStatusId)).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetOperationMasterByassignToReviewerBySp(string assignToReviewer)
        {
            return await _context.getOMByassignToReviewerViewModels.FromSql($"SP_GetOMByassignToReviewer {assignToReviewer}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOMReviewerListViewModel>> GetOMReviewerListViewModels(string assignToReviewer)
        {
            return await _context.getOMReviewerListViewModels.FromSql($"SP_GetOMReviewedList {assignToReviewer}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<OperationMaster>> GetOperationMasterByassignToConverter(string assignToConverter)
        {
            List<int?> declarations = new List<int?> { 7 };
            List<int?> statusId = new List<int?> { 2058 };


            //return await _context.operationMasters.Include(x => x.agreement).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).Where(x =>statusId.Contains(x.jobStatusId)).AsNoTracking().ToListAsync();
            return await _context.operationMasters.Include(x => x.agreement).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).Where(x => (x.assignToConverter == assignToConverter) && statusId.Contains(x.jobStatusId)).AsNoTracking().ToListAsync();
            //return await _context.operationMasters.Include(x => x.agreement).Include(x => x.agreement.agreementType).Include(x => x.agreement.leads).Include(x => x.agreement.agreementCategory).Where(x => (x.assignToConverter == assignToConverter) && declarations.Contains(x.declaration) && statusId.Contains(x.jobStatusId)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GetOMAssignToConverterViewModel>> GetOperationMasterByassignToConverterBySp(string assignToConverter)
        {
            return await _context.getOMAssignToConverterViewModels.FromSql($"SP_GetOpMasterByassignToConverter {assignToConverter}").AsNoTracking().ToListAsync();
        }
         public async Task<IEnumerable<GetOMAssignToConverterdViewModel>> GetOperationMasterByassignToConverterdBySp(string assignToConverter)
        {
            return await _context.getOMAssignToConverterdViewModels.FromSql($"SP_GetOpMasterByassignToConverterted {assignToConverter}").AsNoTracking().ToListAsync();
        }

        public async Task<OperationMaster> GetOperationMasterById(int Id)
        {
            return await _context.operationMasters.Include(x=>x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.agreementCategory).Include(x => x.agreement.ratingCategory).Include(x => x.approvedforCro.agreementDetails.ratingYear).Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteOperationMasterById(int Id)
        {
            _context.operationMasters.Remove(_context.operationMasters.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateOperationMaster(int? Id, int? status,string reportNo,DateTime? reportDate)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.reportNo = reportNo;
            OperationMaster.reportDate = reportDate;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterEthicalDeclaration(int? Id, int? status, int? declaration, DateTime? tLDeclarationDate,string remarks)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.declaration = declaration;
            OperationMaster.tLDeclarationDate = tLDeclarationDate;
            OperationMaster.comments = remarks;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterTLDistribuiteJob(int? Id, int? status, int? declaration, DateTime? assignDate, string remarks,string assignTo)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.declaration = declaration;
            OperationMaster.assignDate = assignDate;
            OperationMaster.comments = remarks;
            OperationMaster.assignTo = assignTo;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterReviewAssign(int? Id, int? status, int? declaration, DateTime? assignDate, string remarks, string assignTo)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.declaration = declaration;
            OperationMaster.assignDateReview = assignDate;
            OperationMaster.comments = remarks;
            OperationMaster.assignToReviewer = assignTo;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterReviewedByReviewer(int? Id, int? status, int? declaration,  string remarks, string reportNo)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.declaration = declaration;
            OperationMaster.comments = remarks;
            OperationMaster.reportNo = reportNo;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterConversionAssign(int? Id, int? status, int? declaration, DateTime? assignDate, string remarks, string assignTo)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.declaration = declaration;
            OperationMaster.assignDateConversion = assignDate;
            OperationMaster.comments = remarks;
            OperationMaster.assignToConverter = assignTo;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterConversionByConverter(int? Id, int? status, int? declaration)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            OperationMaster.declaration = declaration;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateOperationMasterArchieved(int? Id, int? status)
        {
            var OperationMaster = _context.operationMasters.Find(Id);
            OperationMaster.jobStatusId = status;
            _context.Entry(OperationMaster).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region JobStatus
        public async Task<int> SaveJobStatus(JobStatus jobStatus)
        {
            if (jobStatus.Id != 0)
                _context.jobStatuses.Update(jobStatus);
            else
                _context.jobStatuses.Add(jobStatus);

            await _context.SaveChangesAsync();
            return jobStatus.Id;
        }
        public async Task<IEnumerable<JobStatus>> GetAllJobStatus()
        {
            return await _context.jobStatuses.AsNoTracking().ToListAsync();
        }
       
        public async Task<JobStatus> GetJobStatusById(int Id)
        {
            return await _context.jobStatuses.FindAsync(Id);
        }
        public async Task<bool> DeleteJobStatusById(int Id)
        {
            _context.jobStatuses.Remove(_context.jobStatuses.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
       
        #endregion

        #region ProposedRating
        public async Task<int> SaveProposedRating(ProposedRating  proposedRating)
        {
            if (proposedRating.Id != 0)
                _context.proposedRatings.Update(proposedRating);
            else
                _context.proposedRatings.Add(proposedRating);

            await _context.SaveChangesAsync();
            return proposedRating.Id;
        }
        public async Task<IEnumerable<ProposedRating>> GetAllProposedRating()
        {
            return await _context.proposedRatings.Include(x => x.operationMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProposedRating>> GetProposedRatingByOperationMasterId(int OperationMasterId)
        {
            return await _context.proposedRatings.Include(x => x.operationMaster).Where(x => x.operationMasterId == OperationMasterId).AsNoTracking().ToListAsync();
        }
        public async Task<ProposedRating> GetProposedRatingById(int Id)
        {
            return await _context.proposedRatings.FindAsync(Id);
        }

        public async Task<ProposedRating> GetProposedRatingByOpMstId(int opMasterId)
        {
            return await _context.proposedRatings.Include(a => a.operationMaster).Where(a => a.operationMasterId == opMasterId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProposedRatingById(int Id)
        {
            _context.proposedRatings.Remove(_context.proposedRatings.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProposedRatingByOperMasterId(int opMasterId)
        {
            _context.proposedRatings.RemoveRange(_context.proposedRatings.Where(x => x.operationMasterId == opMasterId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region IRCRating
        public async Task<int> SaveIRCRating(IRCRating iRCRating)
        {
            if (iRCRating.Id != 0)
                _context.iRCRatings.Update(iRCRating);
            else
                _context.iRCRatings.Add(iRCRating);

            await _context.SaveChangesAsync();
            return iRCRating.Id;
        }
        public async Task<IEnumerable<IRCRating>> GetAllIRCRating()
        {
            return await _context.iRCRatings.Include(x => x.operationMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<IRCRating>> GetIRCRatingByOperationMasterId(int OperationMasterId)
        {
            return await _context.iRCRatings.Include(x => x.operationMaster).Where(x => x.operationMasterId == OperationMasterId).AsNoTracking().ToListAsync();
        }
        public async Task<IRCRating> GetIRCRatingByOpMstId(int opMasterId)
        {
            return await _context.iRCRatings.Include(a => a.operationMaster).Where(a => a.operationMasterId == opMasterId).FirstOrDefaultAsync();
        }
        public async Task<IRCRating> GetIRCRatingById(int Id)
        {
            return await _context.iRCRatings.FindAsync(Id);
        }
        public async Task<bool> DeleteIRCRatingById(int Id)
        {
            _context.iRCRatings.Remove(_context.iRCRatings.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Archive
        public async Task<int> SaveArchive(Archive archive)
        {
            if (archive.Id != 0)
                _context.archives.Update(archive);
            else
                _context.archives.Add(archive);

            await _context.SaveChangesAsync();
            return archive.Id;
        }
        public async Task<IEnumerable<Archive>> GetAllArchive()
        {
            return await _context.archives.Include(x => x.operationMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Archive>> GetArchiveByOperationMasterId(int OperationMasterId)
        {
            return await _context.archives.Include(x => x.operationMaster).Where(x => x.operationMasterId == OperationMasterId).OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        }
        public async Task<Archive> GetArchiveById(int Id)
        {
            return await _context.archives.FindAsync(Id);
        }
        public async Task<bool> DeleteArchiveById(int Id)
        {
            _context.archives.Remove(_context.archives.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region StatusLog
        public async Task<int> SaveStatusLog(StatusLog statusLog)
        {
            if (statusLog.Id != 0)
                _context.statusLogs.Update(statusLog);
            else
                _context.statusLogs.Add(statusLog);

            await _context.SaveChangesAsync();
            return statusLog.Id;
        }
        public async Task<IEnumerable<StatusLog>> GetAllStatusLog()
        {
            return await _context.statusLogs.Include(x => x.operationMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<StatusLog>> GetStatusLogByOperationMasterId(int OperationMasterId)
        {
            return await _context.statusLogs.Include(x => x.operationMaster).Include(x => x.operationMaster.agreement).Include(x => x.jobStatus).Where(x => x.operationMasterId == OperationMasterId).OrderByDescending(a=>a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<StatusLog> GetStatusLogById(int Id)
        {
            return await _context.statusLogs.FindAsync(Id);
        }
        public async Task<bool> DeleteStatusLogById(int Id)
        {
            _context.statusLogs.Remove(_context.statusLogs.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteStatusLogByOperationMasterId(int OperationMasterId)
        {
            _context.statusLogs.RemoveRange(_context.statusLogs.Where(x => x.operationMasterId == OperationMasterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region RatingType
        public async Task<int> SaveRatingType(RatingType ratingType)
        {
            if (ratingType.Id != 0)
                _context.ratingTypes.Update(ratingType);
            else
                _context.ratingTypes.Add(ratingType);

            await _context.SaveChangesAsync();
            return ratingType.Id;
        }
        public async Task<IEnumerable<RatingType>> GetAllRatingType()
        {
            return await _context.ratingTypes.AsNoTracking().ToListAsync();
        }
       
        public async Task<RatingType> GetRatingTypeById(int Id)
        {
            return await _context.ratingTypes.FindAsync(Id);
        }
        public async Task<bool> DeleteRatingTypeById(int Id)
        {
            _context.ratingTypes.Remove(_context.ratingTypes.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region RatingValue
        public async Task<int> SaveRatingValue(RatingValue ratingValue)
        {
            if (ratingValue.Id != 0)
                _context.ratingValues.Update(ratingValue);
            else
                _context.ratingValues.Add(ratingValue);

            await _context.SaveChangesAsync();
            return ratingValue.Id;
        }
        public async Task<IEnumerable<RatingValue>> GetAllRatingValue()
        {
            return await _context.ratingValues.Include(x => x.ratingType).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RatingValue>> GetRatingValueByRatingTypeId(int RatingTypeId)
        {
            return await _context.ratingValues.Include(x => x.ratingType).Where(x => x.ratingTypeId == RatingTypeId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RatingValue>> GetRatingValueByRatingTypeCategoryId(int RatingTypeId,int categoryId)
        {
            return await _context.ratingValues.Include(x => x.ratingType).Where(x => x.ratingTypeId == RatingTypeId && x.ratingCategoryId==categoryId).AsNoTracking().ToListAsync();
        }
        public async Task<RatingValue> GetRatingValueById(int Id)
        {
            return await _context.ratingValues.FindAsync(Id);
        }
        public async Task<bool> DeleteRatingValueById(int Id)
        {
            _context.ratingValues.Remove(_context.ratingValues.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Attachment Type
        public async Task<int> SaveAttachmentType(AttachmentType attachmentType)
        {
            if (attachmentType.Id != 0)
                _context.AttachmentTypes.Update(attachmentType);
            else
                _context.AttachmentTypes.Add(attachmentType);

            await _context.SaveChangesAsync();
            return attachmentType.Id;
        }
        public async Task<IEnumerable<AttachmentType>> GetAllAttachmentType()
        {
            return await _context.AttachmentTypes.AsNoTracking().ToListAsync();
        }

        public async Task<AttachmentType> GetAttachmentTypeById(int Id)
        {
            return await _context.AttachmentTypes.FindAsync(Id);
        }
        public async Task<bool> DeleteAttachmentTypeById(int Id)
        {
            _context.AttachmentTypes.Remove(_context.AttachmentTypes.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Attachment Status
        public async Task<int> SaveAttachmentStatus(AttachmentStatus attachmentStatus)
        {
            if (attachmentStatus.Id != 0)
                _context.AttachmentStatuses.Update(attachmentStatus);
            else
                _context.AttachmentStatuses.Add(attachmentStatus);

            await _context.SaveChangesAsync();
            return attachmentStatus.Id;
        }

        public async Task<IEnumerable<AttachmentStatus>> GetAllAttachmentStatus()
        {
            return await _context.AttachmentStatuses.AsNoTracking().ToListAsync();
        }

        public async Task<AttachmentStatus> GetAttachmentStatusById(int Id)
        {
            return await _context.AttachmentStatuses.FindAsync(Id);
        }

        public async Task<bool> DeleteAttachmentStatusById(int Id)
        {
            _context.AttachmentStatuses.Remove(_context.AttachmentStatuses.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAttachmentStatusArchieved(int? Id, int? status)
        {
            var attachemnt = _context.AttachmentStatuses.Find(Id);
            attachemnt.archieveId = status;
            _context.Entry(attachemnt).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Document Chart
        public async Task<int> SaveDocumentChart(DocumentChart documentChart)
        {
            if (documentChart.Id != 0)
                _context.DocumentCharts.Update(documentChart);
            else
                _context.DocumentCharts.Add(documentChart);

            await _context.SaveChangesAsync();
            return documentChart.Id;
        }

        public async Task<IEnumerable<DocumentChart>> GetAllDocumentChart()
        {
            return await _context.DocumentCharts.AsNoTracking().ToListAsync();
        }

        public async Task<DocumentChart> GetDocumentChartById(int Id)
        {
            return await _context.DocumentCharts.FindAsync(Id);
        }

        public async Task<bool> DeleteDocumentChartById(int Id)
        {
            _context.DocumentCharts.Remove(_context.DocumentCharts.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Receive Document
        public async Task<int> SaveReceiveDocument(ReceiveDocument receiveDocument)
        {
            if (receiveDocument.Id != 0)
                _context.ReceiveDocuments.Update(receiveDocument);
            else
                _context.ReceiveDocuments.Add(receiveDocument);

            await _context.SaveChangesAsync();
            return receiveDocument.Id;
        }

        public async Task<IEnumerable<ReceiveDocument>> GetAllReceiveDocumentByOperMstid(int operationMasterId)
        {          
            return await _context.ReceiveDocuments.Include(x => x.operationMaster).Include(x => x.documentChart).Where(x => x.operationMasterId == operationMasterId).AsNoTracking().OrderByDescending(x=>x.createdAt).ToListAsync();
        }

        public async Task<ReceiveDocument> GetReceiveDocumentById(int Id)
        {
            return await _context.ReceiveDocuments.FindAsync(Id);
        }

        public async Task<bool> DeleteReceiveDocumentById(int Id)
        {
            _context.ReceiveDocuments.Remove(_context.ReceiveDocuments.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateReceiveDocumentArchieved(int? Id, int? status)
        {
            var attachemnt = _context.ReceiveDocuments.Find(Id);
            attachemnt.archieveId = status;
            _context.Entry(attachemnt).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region IRC Member Comments
        public async Task<int> SaveIRCMemberComments(IRCMemberComments iRCMemberComments)
        {
            if (iRCMemberComments.Id != 0)
                _context.IRCMemberComments.Update(iRCMemberComments);
            else
                _context.IRCMemberComments.Add(iRCMemberComments);

            await _context.SaveChangesAsync();
            return iRCMemberComments.Id;
        }

        public async Task<IEnumerable<IRCMemberComments>> GetAllIRCMemberCommentsByOperMstid(int operationMasterId)
        {
            return await _context.IRCMemberComments.Include(x => x.operationMaster).Include(x => x.employeeInfo).Where(x => x.operationMasterId == operationMasterId).AsNoTracking().ToListAsync();
        }

        public async Task<IRCMemberComments> GetIRCMemberCommentsById(int Id)
        {
            return await _context.IRCMemberComments.FindAsync(Id);
        }

        public async Task<bool> DeleteIRCMemberCommentsById(int Id)
        {
            _context.IRCMemberComments.Remove(_context.IRCMemberComments.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region IRC Meeting Attendance
        public async Task<int> SaveIRCMeetingAttendance(IRCMeetingAttendance iRCMeetingAttendance)
        {
            if (iRCMeetingAttendance.Id != 0)
                _context.IRCMeetingAttendances.Update(iRCMeetingAttendance);
            else
                _context.IRCMeetingAttendances.Add(iRCMeetingAttendance);

            await _context.SaveChangesAsync();
            return iRCMeetingAttendance.Id;
        }

        public async Task<IEnumerable<IRCMeetingAttendance>> GetAllIRCMeetingAttendanceByOperMstid(int operationMasterId)
        {
            return await _context.IRCMeetingAttendances.Include(x => x.operationMaster).Include(x => x.employeeInfo).Where(x => x.operationMasterId == operationMasterId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IRCMeetingAttendance>> GetMeetingAttendanceByMstIdandEmpId(int? operationMasterId, int? empId)
        {
            return await _context.IRCMeetingAttendances.Include(x => x.operationMaster).Include(x => x.employeeInfo).Where(x => x.operationMasterId == operationMasterId && x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<IRCMeetingAttendance> GetIRCMeetingAttendanceById(int Id)
        {
            return await _context.IRCMeetingAttendances.FindAsync(Id);
        }

        public async Task<bool> DeleteIRCMeetingAttendanceById(int Id)
        {
            _context.IRCMeetingAttendances.Remove(_context.IRCMeetingAttendances.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region GetAllDocumentPhotoAttachment
        public async Task<IEnumerable<DocumentPhotoAttachment>> GetAllDocumentPhotoAttachment(int id)
        {
            return await _context.DocumentPhotoAttachments.Where(x=>x.actionTypeId == id && x.documentType == "document" && x.moduleId == 13).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Archive Floor
        public async Task<int> SaveArchiveFloor(ArchiveFloor archiveFloor)
        {
            if (archiveFloor.Id != 0)
                _context.archiveFloors.Update(archiveFloor);
            else
                _context.archiveFloors.Add(archiveFloor);

            await _context.SaveChangesAsync();
            return archiveFloor.Id;
        }
        public async Task<IEnumerable<ArchiveFloor>> GetAllArchiveFloor()
        {
            return await _context.archiveFloors.AsNoTracking().ToListAsync();
        }

        public async Task<ArchiveFloor> GetArchiveFloorById(int Id)
        {
            return await _context.archiveFloors.FindAsync(Id);
        }
        public async Task<bool> DeleteArchiveFloorById(int Id)
        {
            _context.archiveFloors.Remove(_context.archiveFloors.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region ArchiveShelf
        public async Task<int> SaveArchiveShelf(ArchiveShelf archiveShelf)
        {
            if (archiveShelf.Id != 0)
                _context.archiveShelves.Update(archiveShelf);
            else
                _context.archiveShelves.Add(archiveShelf);

            await _context.SaveChangesAsync();
            return archiveShelf.Id;
        }
        public async Task<IEnumerable<ArchiveShelf>> GetAllArchiveShelf()
        {
            return await _context.archiveShelves.Include(x => x.archiveFloor).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ArchiveShelf>> GetArchiveShelfByarchiveFloorId(int archiveFloorId)
        {
            return await _context.archiveShelves.Include(x => x.archiveFloor).Where(x => x.archiveFloorId == archiveFloorId).AsNoTracking().ToListAsync();
        }
        public async Task<ArchiveShelf> GetArchiveShelfById(int Id)
        {
            return await _context.archiveShelves.FindAsync(Id);
        }
        public async Task<bool> DeleteArchiveShelfById(int Id)
        {
            _context.archiveShelves.Remove(_context.archiveShelves.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region IRC Signatory
        public async Task<int> SaveIRCSignatory(IRCSignatory archiveFloor)
        {
            if (archiveFloor.Id != 0)
                _context.IRCSignatories.Update(archiveFloor);
            else
                _context.IRCSignatories.Add(archiveFloor);

            await _context.SaveChangesAsync();
            return archiveFloor.Id;
        }
        public async Task<IEnumerable<IRCSignatory>> GetAllIRCSignatory()
        {
            return await _context.IRCSignatories.Include(x=>x.employeeInfo).AsNoTracking().ToListAsync();
        }

        public async Task<IRCSignatory> GetIRCSignatoryById(int Id)
        {
            return await _context.IRCSignatories.FindAsync(Id);
        }
        public async Task<bool> DeleteIRCSignatoryById(int Id)
        {
            _context.IRCSignatories.Remove(_context.IRCSignatories.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        public async Task<IEnumerable<MasterJobViewModel>> GetLeadMasterJob(string FromDate,string Todate,string TeamLeader,string Fa,string BD,string LeadId,int JobstatusId,string ratingType)
        {

            try
            {
                var data = await _context.masterJobViewModels.FromSql($"jobMasterData {FromDate},{Todate},{TeamLeader},{Fa},{BD},{LeadId},{JobstatusId},{ratingType}").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
          //  var data= await _context.masterJobViewModels.FromSql($"jobMasterData {FromDate},{Todate},{TeamLeader},{Fa},{BD},{LeadId},{JobstatusId},{ratingType}").AsNoTracking().ToListAsync();
           
        }

    }
}


