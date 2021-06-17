using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Production.Services.AttachmentComment.Interfaces;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Services.AttachmentComment
{
    public class AttachmentCommentService : IAttachmentCommentService
    {
        private readonly ERPDbContext _context;

        public AttachmentCommentService(ERPDbContext context)
        {
            _context = context;
        }

        #region Document Attachment
        public async Task<int> SaveDocumentAttachment(ProductionDocumentAttachment documentAttachment)
        {
            if (documentAttachment.Id != 0)
            {
                documentAttachment.updatedBy = documentAttachment.createdBy;
                documentAttachment.updatedAt = DateTime.Now;
                _context.ProductionDocumentAttachments.Update(documentAttachment);
            }
            else
            {
                documentAttachment.createdAt = DateTime.Now;
                _context.ProductionDocumentAttachments.Add(documentAttachment);
            }

            await _context.SaveChangesAsync();
            return documentAttachment.Id;
        }

        public async Task<IEnumerable<ProductionDocumentAttachment>> GetDocumentAttachmentByActionId(int actionTypeId, string actionType, string documentType)
        {
            return await _context.ProductionDocumentAttachments.Where(x => x.actionTypeId == actionTypeId && x.actionType == actionType && x.documentType == documentType).AsNoTracking().ToListAsync();
        }

        public async Task<ProductionDocumentAttachment> GetDocumentAttachmentById(int id)
        {
            return await _context.ProductionDocumentAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDocumentAttachmentById(int id)
        {
            _context.DocumentAttachments.Remove(_context.DocumentAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Comment
        public async Task<int> SaveComment(Comment  comment)
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
        public async Task<IEnumerable<Comment>> GetCommentByActionTypeId(int actionTypeId, string actionType)
        {
            return await _context.Comments.AsNoTracking().Where(x => x.actionTypeId== actionTypeId && x.actionType == actionType).ToListAsync();
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

        #endregion
    }
}
