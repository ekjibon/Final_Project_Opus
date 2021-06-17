using OPUSERP.Areas.Meeting.Models;
using OPUSERP.Meeting.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Service.Interface
{
   public interface IMeetingService
    {
        #region Doc
        Task<int> SaveDoc(MeetingDocument doc);
        Task<IEnumerable<MeetingDocument>> GetAllDoc();
        Task<MeetingDocument> GetDocById(int id);
        Task<MeetingDocWithSgnature> GetDocByIdWithSignature(int id);
        Task<bool> DeleteDocById(int id);
        void UpdateDocToCloded(int docId);
        void UpdateDocToClodedReturn(int docId);
        Task<IEnumerable<MeetingDocument>> GetAllDocForMeeting();
        void UpdateDocDecisionSummary(int docId, string summary, string decision);
        void UpdateDocToMeetingReturn(int docId);

        Task<IEnumerable<MeetingDocument>> GetAllCreaedDoc(int empID);
        Task<IEnumerable<MeetingDocument>> GetAllProcessedDoc(int empID);
        Task<IEnumerable<MeetingDocument>> GetAllReturnDoc(int empID);
        Task<IEnumerable<MeetingDocument>> GetAllManagedDoc(int empID);
        Task<IEnumerable<MeetingDocument>> GetAllPendingDoc(int empID);
        Task<IEnumerable<MeetingDocument>> GetAllActiveDoc(int empID);
        #endregion

        #region ActionInfo
        Task<int> SaveActionInfo(MeetingActionInfo actionInfo);
        Task<IEnumerable<MeetingActionInfo>> GetAllActionInfo();
        Task<MeetingActionInfo> GetActionInfoById(int id);
        Task<bool> DeleteActionInfoById(int id);
        #endregion

        #region DocAttachment
        Task<int> SaveDocAttachment(MeetingDocAttachment docAttachment);
        Task<IEnumerable<MeetingDocAttachment>> GetAllDocAttachment();
        Task<IEnumerable<MeetingDocAttachment>> GetAllDocAttachmentByDocId(int id);
        Task<MeetingDocAttachment> GetDocAttachmentById(int id);
        Task<bool> DeleteDocAttachmentById(int id);
        #endregion

        #region DocRoute
        Task<int> SaveDocRoute(MeetingDocRoute docRoute);
        Task<IEnumerable<MeetingDocRoute>> GetAllDocRoute();
        Task<IEnumerable<MeetingDocRoute>> GetAllDocRouteByDocId(int id);
        Task<IEnumerable<MeetingRouteWithSignatureforDoc>> GetAllDocRouteByDocIdWithSignature(int id);
        Task<IEnumerable<MeetingDocRoute>> GetAllDocRouteByDocIdDecendaing(int id);
        Task<MeetingDocRoute> GetDocRouteByEmpIdAndDocId(int id, int DocId);
        Task<MeetingDocRoute> GetDocRouteById(int id);
        Task<bool> DeleteDocRouteById(int id);
        void UpdateActionIdInRoute(int routeId, int actionId);
        void UpdateRouteOrder(int routeId, int order);
        Task<MeetingDocRoute> GetDocRouteByDocIdAndOrder(int docId, int order);
        void UpdateRouteStatus(int routeId, int order);
        #endregion

        #region MeetingInfo
        Task<int> SaveMeetingInfo(MeetingInfo meetingInfo);
        Task<IEnumerable<MeetingInfo>> GetAllMeetingInfo();
        Task<MeetingInfo> GetMeetingInfoById(int id);
        Task<bool> DeleteMeetingInfoById(int id);
        void UpdateMeetingStatus(int meetingId, int status);
        void UpdateMeetingAction(int meetingId, int action);
        void UpdateMeetingArchived(int meetingId);
        Task<IEnumerable<MeetingInfo>> GetAllMeetingInfoArchiveds();
        Task<IEnumerable<MeetingInfo>> GetAllMeetingInfoByStatus(int status);
        #endregion

        #region MeetingDocs
        Task<int> SaveMeetingDocs(MeetingDocs meetingInfo);
        Task<IEnumerable<MeetingDocs>> GetAllMeetingDocs();
        Task<IEnumerable<MeetingDocs>> GetAllMeetingDocsByMeetingId(int Id);
        Task<MeetingDocs> GetMeetingDocsById(int id);
        Task<bool> DeleteMeetingDocsById(int id);
        Task<bool> DeleteMeetingDocsByMeetingId(int id);
        Task<MeetingInfo> GetMeetingInfoByDocId(int id);
        #endregion

        #region MeetingDetail
        Task<int> SaveMeetingDetail(MeetingDetail meetingInfo);
        Task<IEnumerable<MeetingDetail>> GetAllMeetingDetail();
        Task<IEnumerable<MeetingDetail>> GetAllMeetingDetailByMeetingId(int Id);
        Task<MeetingDetail> GetMeetingDetailById(int id);
        Task<bool> DeleteMeetingDetailById(int id);
        Task<IEnumerable<DocWiseMeetingDetails>> DocWiseMeetingDetails(int Id);
        #endregion

        #region MeetingAttendance
        Task<int> SaveMeetingAttendance(MeetingAttendance meetingInfo);
        Task<IEnumerable<MeetingAttendance>> GetAllMeetingAttendance();
        Task<IEnumerable<MeetingAttendance>> GetAllMeetingAttendancePresentsByMeetingId(int Id);
        Task<IEnumerable<MeetingAttendance>> GetAllMeetingAttendanceAbsentsByMeetingId(int Id);
        Task<MeetingAttendance> GetMeetingAttendanceById(int id);
        Task<bool> DeleteMeetingAttendanceById(int id);
        void UpdateAttendance(int meetingId, int empId);
        #endregion

        #region MeetingUser
        Task<int> SaveMeetingUser(MeetingUser actionInfo);
        Task<IEnumerable<MeetingUser>> GetAllMeetingUser();
        Task<MeetingUser> GetMeetingUserById(int id);
        Task<IEnumerable<MeetingUser>> GetAllMeetingUserBoardMember();
        Task<IEnumerable<MeetingUser>> GetAllMeetingUserBoardSecritary();
        Task<bool> DeleteMeetingUserById(int id);
        #endregion
    }
}
