using OPUSERP.Areas.CRO.Models;
using OPUSERP.Areas.DMS.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.AttachmentComment.Interfaces
{
    public interface IAttachmentCommentService
    {
        #region Document Attachment
        Task<int> SaveDocumentAttachment(DocumentPhotoAttachment documentAttachment);
        Task<IEnumerable<DocumentPhotoAttachment>> GetDocumentAttachmentByActionId(int actionTypeId, string actionType, string documentType, int moduleId);
        Task<DocumentPhotoAttachment> GetDocumentAttachmentById(int id);
        Task<IEnumerable<DocumentPhotoAttachment>> GetDocumentAttachmentList(string actionType, string documentType, int module);
        Task<bool> DeleteDocumentAttachmentById(int id);
        Task<DocumentPhotoAttachment> GetDocumentAttachmentByActionIdContact(int actionTypeId, string actionType, string documentType, int moduleId);

        Task<IEnumerable<CroAttachmentViewModel>> GetCroDocumentAttachment(int actionTypeId, string documentType, int moduleId);
        Task<IEnumerable<CroDocumentsViewModel>> GetCroDocumentsViewModels(int actionTypeId);
        Task<IEnumerable<CroStatusViewModel>> GetCroStatusViewModels(int actionTypeId);
        Task<IEnumerable<DocumentAccessViewModel>> GetDocumentAttachmentForDMSByActionId(int actionTypeId, string actionType, string documentType, int moduleId, string userName);
        #endregion

        //#region Comment
        Task<int> SaveComment(Comment comment);
        Task<IEnumerable<Comment>> GetComment();
        Task<IEnumerable<Comment>> GetCommentByActionTypeId(int actionTypeId, string actionType, int moduleId);
        Task<Comment> GetCommentById(int id);
        Task<bool> DeleteCommentById(int id);
        //#endregion
    }
}
