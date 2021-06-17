using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Meeting.Models;
using OPUSERP.Data;
using OPUSERP.Meeting.Data.Entity;
using OPUSERP.Meeting.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Service
{
    public class MeetingService : IMeetingService
    {
        private readonly ERPDbContext _context;

        public MeetingService(ERPDbContext context)
        {
            _context = context;
        }

        #region Doc

        public async Task<int> SaveDoc(MeetingDocument doc)
        {
            if (doc.Id != 0)
                _context.meetingDocuments.Update(doc);
            else
                _context.meetingDocuments.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllDoc()
        {
            return await _context.meetingDocuments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllCreaedDoc(int empID)
        {
            return await _context.meetingDocuments.Where(x => x.employeeId == empID).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllDocForMeeting()
        {
            return await _context.meetingDocuments.Where(x => x.isClose == 1 && x.isMeeting != 1).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllProcessedDoc(int empID)
        {
            return await _context.meetingDocuments.Where(x => x.employeeId == empID && x.isClose == 1).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllActiveDoc(int empID)
        {
            return await _context.meetingDocuments.Where(x => x.employeeId == empID && x.isClose == 0).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllReturnDoc(int empID)
        {
            return await _context.meetingDocuments.Where(x => x.employeeId == empID && x.isClose == 2).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllManagedDoc(int empID)
        {
            List<int?> docIds = await _context.meetingDocRoutes.Where(x => x.employeeId == empID && x.actionId != null).Select(x => x.docId).ToListAsync();
            return await _context.meetingDocuments.Where(x => docIds.Contains(x.Id)).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocument>> GetAllPendingDoc(int empID)
        {
            List<int?> docIds = await _context.meetingDocRoutes.Where(x => x.employeeId == empID && x.isActive == 1).Select(x => x.docId).ToListAsync();
            return await _context.meetingDocuments.Where(x => docIds.Contains(x.Id)).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<MeetingDocument> GetDocById(int id)
        {
            return await _context.meetingDocuments.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<MeetingDocWithSgnature> GetDocByIdWithSignature(int id)
        {
            var doc = await _context.meetingDocuments.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
            MeetingDocWithSgnature docWithSgnature = new MeetingDocWithSgnature
            {
                doc = doc,
                employeeSignature = await _context.employeeSignatures.Where(x => x.employeeId == doc.employeeId).FirstOrDefaultAsync()
            };
            return docWithSgnature;
        }

        public async Task<bool> DeleteDocById(int id)
        {
            _context.meetingDocuments.Remove(_context.meetingDocuments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateDocToCloded(int docId)
        {
            var user = _context.meetingDocuments.Find(docId);
            user.isClose = 1;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateDocToClodedReturn(int docId)
        {
            var user = _context.meetingDocuments.Find(docId);
            user.isClose = 2;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateDocToMeetingReturn(int docId)
        {
            var user = _context.meetingDocuments.Find(docId);
            user.isMeeting = 1;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateDocDecisionSummary(int docId,string summary,string decision)
        {
            var user = _context.meetingDocuments.Find(docId);
            user.summary = summary;
            user.decision = decision;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion

        #region ActionInfo
        public async Task<int> SaveActionInfo(MeetingActionInfo doc)
        {
            if (doc.Id != 0)
                _context.meetingActionInfos.Update(doc);
            else
                _context.meetingActionInfos.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingActionInfo>> GetAllActionInfo()
        {
            return await _context.meetingActionInfos.AsNoTracking().ToListAsync();
        }

        public async Task<MeetingActionInfo> GetActionInfoById(int id)
        {
            return await _context.meetingActionInfos.FindAsync(id);
        }

        public async Task<bool> DeleteActionInfoById(int id)
        {
            _context.meetingActionInfos.Remove(_context.meetingActionInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region DocAttachment
        public async Task<int> SaveDocAttachment(MeetingDocAttachment doc)
        {
            if (doc.Id != 0)
                _context.meetingDocAttachments.Update(doc);
            else
                _context.meetingDocAttachments.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingDocAttachment>> GetAllDocAttachment()
        {
            return await _context.meetingDocAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocAttachment>> GetAllDocAttachmentByDocId(int id)
        {
            return await _context.meetingDocAttachments.Where(x => x.docId == id).AsNoTracking().ToListAsync();
        }

        public async Task<MeetingDocAttachment> GetDocAttachmentById(int id)
        {
            return await _context.meetingDocAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDocAttachmentById(int id)
        {
            _context.meetingDocAttachments.Remove(_context.meetingDocAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region DocRoute
        public async Task<int> SaveDocRoute(MeetingDocRoute doc)
        {
            if (doc.Id != 0)
                _context.meetingDocRoutes.Update(doc);
            else
                _context.meetingDocRoutes.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingDocRoute>> GetAllDocRoute()
        {
            return await _context.meetingDocRoutes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocRoute>> GetAllDocRouteByDocId(int id)
        {
            return await _context.meetingDocRoutes.Where(x => x.docId == id).Include(x => x.action).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocRoute>> GetAllDocRouteByDocIdDecendaing(int id)
        {
            return await _context.meetingDocRoutes.Where(x => x.docId == id).Include(x => x.action).Include(x => x.employee).OrderByDescending(x => x.order).AsNoTracking().ToListAsync();
        }

        public async Task<MeetingDocRoute> GetDocRouteById(int id)
        {
            return await _context.meetingDocRoutes.FindAsync(id);
        }

        public async Task<MeetingDocRoute> GetDocRouteByEmpIdAndDocId(int id, int DocId)
        {
            return await _context.meetingDocRoutes.Where(x => x.docId == DocId && x.employeeId == id).FirstOrDefaultAsync();
        }

        public async Task<MeetingDocRoute> GetDocRouteByDocIdAndOrder(int docId, int order)
        {
            return await _context.meetingDocRoutes.Where(x => x.order == order && x.docId == docId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteDocRouteById(int id)
        {
            _context.meetingDocRoutes.Remove(_context.meetingDocRoutes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MeetingRouteWithSignatureforDoc>> GetAllDocRouteByDocIdWithSignature(int id)
        {
            var route = await _context.meetingDocRoutes.Where(x => x.docId == id).Include(x => x.action).Include(x => x.employee).OrderBy(x => x.order).AsNoTracking().ToListAsync();
            List<MeetingRouteWithSignatureforDoc> routeWithSignatureforDocs = new List<MeetingRouteWithSignatureforDoc>();
            foreach (var data in route)
            {
                routeWithSignatureforDocs.Add(new MeetingRouteWithSignatureforDoc
                {
                    docRoute = data,
                    employeeSignature = await _context.employeeSignatures.Where(x => x.employeeId == data.employeeId).FirstOrDefaultAsync()
                });
            }
            return routeWithSignatureforDocs;
        }

        public void UpdateActionIdInRoute(int routeId, int actionId)
        {
            var user = _context.meetingDocRoutes.Find(routeId);
            user.actionId = actionId;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateRouteOrder(int routeId, int order)
        {
            var user = _context.meetingDocRoutes.Find(routeId);
            user.order = order;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateRouteStatus(int routeId, int order)
        {
            var user = _context.meetingDocRoutes.Find(routeId);
            user.isActive = order;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion


        #region MeetingInfo
        public async Task<int> SaveMeetingInfo(MeetingInfo doc)
        {
            if (doc.Id != 0)
                _context.meetingInfos.Update(doc);
            else
                _context.meetingInfos.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingInfo>> GetAllMeetingInfo()
        {
            return await _context.meetingInfos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingInfo>> GetAllMeetingInfoByStatus(int status)
        {
            return await _context.meetingInfos.Where(x => x.status == status && x.isAchived == 0).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingInfo>> GetAllMeetingInfoArchiveds()
        {
            return await _context.meetingInfos.Where(x => x.isAchived == 1).AsNoTracking().ToListAsync();
        }

        public async Task<MeetingInfo> GetMeetingInfoById(int id)
        {
            return await _context.meetingInfos.Where(x=>x.Id==id).Include(x=>x.action).FirstOrDefaultAsync();
        }

        public async Task<MeetingInfo> GetMeetingInfoByDocId(int id)
        {
            var doc =  await _context.meetingDocs.Where(x=>x.docId==id).Include(x=>x.meetingInfo).FirstOrDefaultAsync();
            return doc.meetingInfo;
        }

        public async Task<bool> DeleteMeetingInfoById(int id)
        {
            _context.meetingInfos.Remove(_context.meetingInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateMeetingStatus(int meetingId, int status)
        {
            var user = _context.meetingInfos.Find(meetingId);
            user.status = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateMeetingAction(int meetingId, int action)
        {
            var user = _context.meetingInfos.Find(meetingId);
            user.actionId = action;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateMeetingArchived(int meetingId)
        {
            var user = _context.meetingInfos.Find(meetingId);
            user.isAchived = 1;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion


        #region MeetingDocs
        public async Task<int> SaveMeetingDocs(MeetingDocs doc)
        {
            if (doc.Id != 0)
                _context.meetingDocs.Update(doc);
            else
                _context.meetingDocs.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingDocs>> GetAllMeetingDocs()
        {
            return await _context.meetingDocs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDocs>> GetAllMeetingDocsByMeetingId(int Id)
        {
            return await _context.meetingDocs.Where(x => x.meetingInfoId == Id).Include(x => x.doc).AsNoTracking().ToListAsync();
        }

        public async Task<MeetingDocs> GetMeetingDocsById(int id)
        {
            return await _context.meetingDocs.FindAsync(id);
        }

        public async Task<bool> DeleteMeetingDocsById(int id)
        {
            _context.meetingDocs.Remove(_context.meetingDocs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMeetingDocsByMeetingId(int id)
        {
            _context.meetingDocs.RemoveRange(_context.meetingDocs.Where(x => x.meetingInfoId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region MeetingDetail
        public async Task<int> SaveMeetingDetail(MeetingDetail doc)
        {
            if (doc.Id != 0)
                _context.meetingDetails.Update(doc);
            else
                _context.meetingDetails.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingDetail>> GetAllMeetingDetail()
        {
            return await _context.meetingDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingDetail>> GetAllMeetingDetailByMeetingId(int Id)
        {
            return await _context.meetingDetails.Where(x => x.meetingInfoId == Id).Include(x=>x.action).Include(x=>x.doc).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DocWiseMeetingDetails>> DocWiseMeetingDetails(int Id)
        {
            List<int?> meetingDetails = _context.meetingDetails.Where(x => x.meetingInfoId == Id).Include(x=>x.action).Include(x=>x.doc).Select(x=>x.docId).Distinct().ToList();
            List<DocWiseMeetingDetails> docWiseMeetingDetails = new List<DocWiseMeetingDetails>();

            foreach(int data in meetingDetails)
            {
                docWiseMeetingDetails.Add(new DocWiseMeetingDetails
                {
                    meetingDocument = await _context.meetingDocuments.FindAsync(data),
                    meetingDetails = await _context.meetingDetails.Where(x=>x.docId==data&& x.meetingInfoId==Id).Include(x=>x.action).Include(x=>x.employee).ToListAsync()
                });
            }

            return docWiseMeetingDetails;
        }

        public async Task<MeetingDetail> GetMeetingDetailById(int id)
        {
            return await _context.meetingDetails.FindAsync(id);
        }

        public async Task<bool> DeleteMeetingDetailById(int id)
        {
            _context.meetingDetails.Remove(_context.meetingDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region MeetingAttendance
        public async Task<int> SaveMeetingAttendance(MeetingAttendance doc)
        {
            if (doc.Id != 0)
                _context.meetingAttendances.Update(doc);
            else
                _context.meetingAttendances.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingAttendance>> GetAllMeetingAttendance()
        {
            return await _context.meetingAttendances.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingAttendance>> GetAllMeetingAttendancePresentsByMeetingId(int Id)
        {
            return await _context.meetingAttendances.Where(x => x.meetingInfoId == Id && x.isAttendance == 1).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingAttendance>> GetAllMeetingAttendanceAbsentsByMeetingId(int Id)
        {
            return await _context.meetingAttendances.Where(x => x.meetingInfoId == Id && x.isAttendance == 0).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<MeetingAttendance> GetMeetingAttendanceById(int id)
        {
            return await _context.meetingAttendances.FindAsync(id);
        }

        public async Task<bool> DeleteMeetingAttendanceById(int id)
        {
            _context.meetingAttendances.Remove(_context.meetingAttendances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateAttendance(int meetingId,int empId)
        {
            var user = _context.meetingAttendances.Where(x=>x.meetingInfoId==meetingId&&x.employeeId==empId).FirstOrDefault();
            user.isAttendance = 1;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion


        #region MeetingUser
        public async Task<int> SaveMeetingUser(MeetingUser doc)
        {
            if (doc.Id != 0)
                _context.meetingUsers.Update(doc);
            else
                _context.meetingUsers.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MeetingUser>> GetAllMeetingUser()
        {
            return await _context.meetingUsers.Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingUser>> GetAllMeetingUserBoardMember()
        {
            return await _context.meetingUsers.Where(x => x.role == "Board Member").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MeetingUser>> GetAllMeetingUserBoardSecritary()
        {
            return await _context.meetingUsers.Where(x => x.role == "Board Secretary").AsNoTracking().ToListAsync();
        }

        public async Task<MeetingUser> GetMeetingUserById(int id)
        {
            return await _context.meetingUsers.FindAsync(id);
        }

        public async Task<bool> DeleteMeetingUserById(int id)
        {
            _context.meetingUsers.Remove(_context.meetingUsers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


    }
}
