using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Services.DistributeJob.Interfaces
{
    public interface IDistributeJobService
    {
        #region OperationMaster
        Task<int> SaveOperationMaster(OperationMaster operationMaster);
        Task<bool> UpdateOperationMaster(int? id, int? leadprogressstatus, string updateBy);
        Task<IEnumerable<OperationMaster>> GetAllOperationMaster();
        Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetAllOperationMasterBySp();
        Task<IEnumerable<OperationMaster>> GetOperationMasterByTeamLeader(string TeamLeader);
        Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetOperationMasterByTeamLeaderSp(string TeamLeader);
        Task<IEnumerable<OperationMaster>> GetOperationMasterByAssignTo(string assignTo);
        Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetOperationMasterByAssignToBySp(string assignTo);
        Task<IEnumerable<GetLeadInfoInCroViewModel>> GetLeadInfoInCroByOPMstId(int opMstId);
        Task<IEnumerable<OperationMaster>> GetOperationMasterByassignToReviewer(string assignToReviewer);
        Task<IEnumerable<GetOMByassignToReviewerViewModel>> GetOperationMasterByassignToReviewerBySp(string assignToReviewer);
        Task<IEnumerable<OperationMaster>> GetOperationMasterByassignToConverter(string assignToConverter);
        Task<IEnumerable<GetOMAssignToConverterViewModel>> GetOperationMasterByassignToConverterBySp(string assignToConverter);
        Task<IEnumerable<OperationMasterSPViewModel>> OperationMasterSPViewModelscom(string fromDate, string todate);
        Task<IEnumerable<GetAgreementInfoViewModel>> GetAgreementInfoViewModels(int Id);
        Task<OperationMaster> GetOperationMasterById(int Id);
        Task<bool> DeleteOperationMasterById(int Id);
        Task<bool> UpdateOperationMaster(int? Id, int? status, string reportNo, DateTime? reportDate);
        Task<bool> UpdateOperationMasterEthicalDeclaration(int? Id, int? status, int? declaration, DateTime? tLDeclarationDate, string remarks);
        Task<IEnumerable<OperationMaster>> GetOperationMasterByFAEDeclaration(string Empcode);
        Task<IEnumerable<OperationMaster>> GetOperationMasterByTLDistribuiteJob(string TeamLeader);
        Task<bool> UpdateOperationMasterTLDistribuiteJob(int? Id, int? status, int? declaration, DateTime? assignDate, string remarks, string assignTo);
        Task<bool> UpdateOperationMasterReviewAssign(int? Id, int? status, int? declaration, DateTime? assignDate, string remarks, string assignTo);
        Task<bool> UpdateOperationMasterReviewedByReviewer(int? Id, int? status, int? declaration, string remarks, string reportNo);
        Task<bool> UpdateOperationMasterConversionAssign(int? Id, int? status, int? declaration, DateTime? assignDate, string remarks, string assignTo);
        Task<bool> UpdateOperationMasterConversionByConverter(int? Id, int? status, int? declaration);
        Task<bool> UpdateOperationMasterArchieved(int? Id, int? status);
        Task<IEnumerable<OperationMasterSPViewModel>> OperationMasterSPViewModels(string fromDate, string todate);
        Task<IEnumerable<OperationMasterSPJAViewModel>> OperationMasterSPJAViewModels(string fromDate, string todate,string AssignTo);
        Task<IEnumerable<OperationMasterSPHoldViewModel>> OperationMasterSPHoldViewModels(string fromDate, string todate);
        Task<IEnumerable<OperationMasterSPBlockUnViewModel>> OperationMasterSPUnBlockViewModels(string fromDate, string todate, int statusid);
        Task<IEnumerable<OperationMasterSPBlockUnViewModel>> OperationMasterSPBlockViewModels(string fromDate, string todate, int statusid);
        Task<IEnumerable<OperationMasterSPRateViewModel>> OperationMasterSPRateViewModels(string fromDate, string todate);
        Task<IEnumerable<PreviousRatingDataViewModel>> PreviousRatingDataViewModels();
        Task<IEnumerable<OperationMasterSPJAViewModel>> OperationMasterSPUAViewModels(string fromDate, string todate);
        Task<IEnumerable<GetOMByassignToClosedViewModel>> GetOMByassignToClosedViewModels(string assignTo);
        Task<IEnumerable<GetOMByassignToDeclaredViewModel>> GetOMByassignToDeclareViewModels(string assignTo);
        Task<IEnumerable<GetOMReviewerListViewModel>> GetOMReviewerListViewModels(string assignToReviewer);
        Task<IEnumerable<GetOMAssignToConverterdViewModel>> GetOperationMasterByassignToConverterdBySp(string assignToConverter);
        #endregion

        #region JobStatus
        Task<int> SaveJobStatus(JobStatus jobStatus);
        Task<IEnumerable<JobStatus>> GetAllJobStatus();
        Task<JobStatus> GetJobStatusById(int Id);
        Task<bool> DeleteJobStatusById(int Id);
        #endregion

        #region ProposedRating
        Task<int> SaveProposedRating(ProposedRating proposedRating);
        Task<IEnumerable<ProposedRating>> GetAllProposedRating();
        Task<IEnumerable<ProposedRating>> GetProposedRatingByOperationMasterId(int OperationMasterId);
        Task<ProposedRating> GetProposedRatingById(int Id);
        Task<ProposedRating> GetProposedRatingByOpMstId(int opMasterId);
        Task<bool> DeleteProposedRatingById(int Id);
        Task<bool> DeleteProposedRatingByOperMasterId(int opMasterId);
        #endregion

        #region IRCRating
        Task<int> SaveIRCRating(IRCRating iRCRating);
        Task<IEnumerable<IRCRating>> GetAllIRCRating();
        Task<IEnumerable<IRCRating>> GetIRCRatingByOperationMasterId(int OperationMasterId);
        Task<IRCRating> GetIRCRatingByOpMstId(int opMasterId);
        Task<IRCRating> GetIRCRatingById(int Id);
        Task<bool> DeleteIRCRatingById(int Id);
        #endregion

        #region Archive
        Task<int> SaveArchive(Archive archive);
        Task<IEnumerable<Archive>> GetAllArchive();
        Task<IEnumerable<Archive>> GetArchiveByOperationMasterId(int OperationMasterId);
        Task<Archive> GetArchiveById(int Id);
        Task<bool> DeleteArchiveById(int Id);
        #endregion

        #region StatusLog
        Task<int> SaveStatusLog(StatusLog statusLog);
        Task<IEnumerable<StatusLog>> GetAllStatusLog();
        Task<IEnumerable<StatusLog>> GetStatusLogByOperationMasterId(int OperationMasterId);
        Task<StatusLog> GetStatusLogById(int Id);
        Task<bool> DeleteStatusLogById(int Id);
        Task<bool> DeleteStatusLogByOperationMasterId(int OperationMasterId);
        #endregion

        #region RatingType
        Task<int> SaveRatingType(RatingType ratingType);
        Task<IEnumerable<RatingType>> GetAllRatingType();
        Task<RatingType> GetRatingTypeById(int Id);
        Task<bool> DeleteRatingTypeById(int Id);
        #endregion

        #region RatingValue
        Task<int> SaveRatingValue(RatingValue ratingValue);
        Task<IEnumerable<RatingValue>> GetAllRatingValue();
        Task<IEnumerable<RatingValue>> GetRatingValueByRatingTypeId(int RatingTypeId);
        Task<IEnumerable<RatingValue>> GetRatingValueByRatingTypeCategoryId(int RatingTypeId, int categoryId);
        Task<RatingValue> GetRatingValueById(int Id);
        Task<bool> DeleteRatingValueById(int Id);
        #endregion

        #region Attachment Type
        Task<int> SaveAttachmentType(AttachmentType attachmentType);
        Task<IEnumerable<AttachmentType>> GetAllAttachmentType();
        Task<AttachmentType> GetAttachmentTypeById(int Id);
        Task<bool> DeleteAttachmentTypeById(int Id);
        #endregion

        #region Attachment Status
        Task<int> SaveAttachmentStatus(AttachmentStatus attachmentStatus);
        Task<IEnumerable<AttachmentStatus>> GetAllAttachmentStatus();
        Task<AttachmentStatus> GetAttachmentStatusById(int Id);
        Task<bool> DeleteAttachmentStatusById(int Id);
        Task<bool> UpdateAttachmentStatusArchieved(int? Id, int? status);
        #endregion

        #region Document Chart
        Task<int> SaveDocumentChart(DocumentChart documentChart);
        Task<IEnumerable<DocumentChart>> GetAllDocumentChart();
        Task<DocumentChart> GetDocumentChartById(int Id);
        Task<bool> DeleteDocumentChartById(int Id);
        #endregion

        #region Receive Document
        Task<int> SaveReceiveDocument(ReceiveDocument receiveDocument);
        Task<IEnumerable<ReceiveDocument>> GetAllReceiveDocumentByOperMstid(int operationMasterId);
        Task<ReceiveDocument> GetReceiveDocumentById(int Id);
        Task<bool> DeleteReceiveDocumentById(int Id);
        Task<bool> UpdateReceiveDocumentArchieved(int? Id, int? status);
        #endregion

        #region IRC Member Comments
        Task<int> SaveIRCMemberComments(IRCMemberComments iRCMemberComments);
        Task<IEnumerable<IRCMemberComments>> GetAllIRCMemberCommentsByOperMstid(int operationMasterId);
        Task<IRCMemberComments> GetIRCMemberCommentsById(int Id);
        Task<bool> DeleteIRCMemberCommentsById(int Id);
        #endregion

        #region IRC Meeting Attendance
        Task<int> SaveIRCMeetingAttendance(IRCMeetingAttendance iRCMeetingAttendance);
        Task<IEnumerable<IRCMeetingAttendance>> GetAllIRCMeetingAttendanceByOperMstid(int operationMasterId);
        Task<IEnumerable<IRCMeetingAttendance>> GetMeetingAttendanceByMstIdandEmpId(int? operationMasterId,int? empId);
        Task<IRCMeetingAttendance> GetIRCMeetingAttendanceById(int Id);
        Task<bool> DeleteIRCMeetingAttendanceById(int Id);
        #endregion


        #region GetDocuments
        Task<IEnumerable<DocumentPhotoAttachment>> GetAllDocumentPhotoAttachment(int id);
        #endregion

        #region Archive Floor
        Task<int> SaveArchiveFloor(ArchiveFloor archiveFloor);
        Task<IEnumerable<ArchiveFloor>> GetAllArchiveFloor();
        Task<ArchiveFloor> GetArchiveFloorById(int Id);
        Task<bool> DeleteArchiveFloorById(int Id);

        #endregion

        #region Archive Shelf
        Task<int> SaveArchiveShelf(ArchiveShelf archiveShelf);
        Task<IEnumerable<ArchiveShelf>> GetAllArchiveShelf();
        Task<IEnumerable<ArchiveShelf>> GetArchiveShelfByarchiveFloorId(int archiveFloorId);
        Task<ArchiveShelf> GetArchiveShelfById(int Id);
        Task<bool> DeleteArchiveShelfById(int Id);

        #endregion
        #region IRC Signatory 
        Task<int> SaveIRCSignatory(IRCSignatory archiveFloor);
        Task<IEnumerable<IRCSignatory>> GetAllIRCSignatory();
        Task<IRCSignatory> GetIRCSignatoryById(int Id);

        Task<bool> DeleteIRCSignatoryById(int Id);

        #endregion

        Task<IEnumerable<MasterJobViewModel>> GetLeadMasterJob(string FromDate, string Todate, string TeamLeader, string Fa, string BD, string LeadId, int JobstatusId, string ratingType);
        Task<IEnumerable<OperationMaster>> GetOperationMasterByassignToReviewerTL(string assignToReviewer);

    }
}