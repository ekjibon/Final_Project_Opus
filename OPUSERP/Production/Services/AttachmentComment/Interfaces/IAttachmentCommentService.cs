using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Services.AttachmentComment.Interfaces
{
    public interface IAttachmentCommentService
    {
        #region Document Attachment
        Task<int> SaveDocumentAttachment(ProductionDocumentAttachment documentAttachment);
        Task<IEnumerable<ProductionDocumentAttachment>> GetDocumentAttachmentByActionId(int actionTypeId, string actionType, string documentType);
        Task<ProductionDocumentAttachment> GetDocumentAttachmentById(int id);
        Task<bool> DeleteDocumentAttachmentById(int id);
        #endregion

        #region Comment
        Task<int> SaveComment(Comment comment);
        Task<IEnumerable<Comment>> GetComment();
        Task<IEnumerable<Comment>> GetCommentByActionTypeId(int actionTypeId, string actionType);
        Task<Comment> GetCommentById(int id);
        Task<bool> DeleteCommentById(int id);
        #endregion
    }
}
