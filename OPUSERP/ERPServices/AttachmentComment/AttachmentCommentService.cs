using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.Areas.DMS.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.AttachmentComment
{
    public class AttachmentCommentService : IAttachmentCommentService
    {
        private readonly ERPDbContext _context;

        public AttachmentCommentService(ERPDbContext context)
        {
            _context = context;
        }

        #region Document Attachment
        public async Task<int> SaveDocumentAttachment(DocumentPhotoAttachment documentAttachment)
        {
            if (documentAttachment.Id != 0)
            {
                documentAttachment.updatedBy = documentAttachment.createdBy;
                documentAttachment.updatedAt = DateTime.Now;
                _context.DocumentPhotoAttachments.Update(documentAttachment);
            }
            else
            {
                documentAttachment.createdAt = DateTime.Now;
                _context.DocumentPhotoAttachments.Add(documentAttachment);
            }

            await _context.SaveChangesAsync();
            return documentAttachment.Id;
        }

        public async Task<IEnumerable<DocumentPhotoAttachment>> GetDocumentAttachmentByActionId(int actionTypeId, string actionType, string documentType,int moduleId)
        {
            return await _context.DocumentPhotoAttachments.Where(x => x.actionTypeId == actionTypeId && x.actionType == actionType && x.documentType == documentType && x.moduleId == moduleId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<DocumentPhotoAttachment> GetDocumentAttachmentByActionIdContact(int actionTypeId, string actionType, string documentType, int moduleId)
        {
            return await _context.DocumentPhotoAttachments.Where(x => x.actionTypeId == actionTypeId && x.actionType == actionType && x.documentType == documentType && x.moduleId == moduleId).OrderByDescending(x=>x.Id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<DocumentPhotoAttachment>> GetDocumentAttachmentList(string actionType, string documentType,int module)
        {
            return await _context.DocumentPhotoAttachments.Where(x =>x.actionType == actionType && x.documentType == documentType && x.moduleId==module).AsNoTracking().ToListAsync();
        }

        public async Task<DocumentPhotoAttachment> GetDocumentAttachmentById(int id)
        {
            return await _context.DocumentPhotoAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDocumentAttachmentById(int id)
        {
            _context.DocumentPhotoAttachments.Remove(_context.DocumentPhotoAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CroAttachmentViewModel>> GetCroDocumentAttachment(int actionTypeId, string documentType, int moduleId)
        {
            return await _context.CroAttachmentViewModels.FromSql($"SP_GET_CRO_DocumentsList {actionTypeId},{documentType},{moduleId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<CroDocumentsViewModel>> GetCroDocumentsViewModels(int actionTypeId)
        {
            return await _context.croDocumentsViewModels.FromSql($"SP_GET_CRO_ReceivedDocumentsList {actionTypeId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<CroStatusViewModel>> GetCroStatusViewModels(int actionTypeId)
        {
            return await _context.croStatusViewModels.FromSql($"SP_GET_CRO_StatusLogList {actionTypeId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DocumentAccessViewModel>> GetDocumentAttachmentForDMSByActionId(int actionTypeId, string actionType, string documentType, int moduleId, string userName)
        {
            return await _context.documentAccessViewModels.FromSql($"SP_DocumentAccessView {actionTypeId},{actionType},{documentType},{moduleId},{userName}").AsNoTracking().ToListAsync();
        }

        #endregion

        //#region Comment
        public async Task<int> SaveComment(Comment comment)
        {
            if (comment.Id != 0)
            {
                comment.updatedBy = comment.createdBy;
                comment.updatedAt = DateTime.Now;
                _context.Comments.Update(comment);
            }
            else
            {
                comment.createdAt = DateTime.Now;
                _context.Comments.Add(comment);
            }

            await _context.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<IEnumerable<Comment>> GetComment()
        {
            return await _context.Comments.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentByActionTypeId(int actionTypeId, string actionType, int moduleId)
        {
            return await _context.Comments.AsNoTracking().Where(x => x.actionTypeId == actionTypeId && x.actionType == actionType && x.moduleId == moduleId).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync();
        }

        public async Task<bool> DeleteCommentById(int id)
        {
            _context.Comments.Remove(_context.Comments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //#endregion
    }
}
