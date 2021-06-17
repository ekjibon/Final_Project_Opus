using OPUSERP.Areas.DMS.Models;
using OPUSERP.DMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.DMS.Services.Document.Interfaces
{
    public interface IDocumentUploadService
    {
        #region  Field Type

        Task<bool> SaveFieldType(FieldType fieldType);
        Task<IEnumerable<FieldType>> GetFieldType();
        Task<FieldType> GetFieldTypeById(int id);
        Task<bool> DeleteFieldTypeById(int id);

        #endregion

        #region Document Category

        Task<bool> SaveDocumentCategory(DocumentCategory documentCategory);
        Task<IEnumerable<DocumentCategory>> GetDocumentCategory();
        Task<DocumentCategory> GetDocumentCategoryById(int id);
        Task<bool> DeleteDocumentCategoryById(int id);

        #endregion

        #region  Field Settings

        Task<bool> SaveFieldSettings(FieldSettings fieldSettings);
        Task<IEnumerable<FieldSettings>> GetFieldSettings();
        Task<FieldSettings> GetFieldSettingsById(int id);
        Task<bool> DeleteFieldSettingsById(int id);
        Task<IEnumerable<FieldSettings>> GetDocumentDetailsByDocType(int Id);

        #endregion

        #region Document Master

        Task<int> SaveDocumentMaster(DocumentMaster documentMaster);
        Task<IEnumerable<DocumentMaster>> GetAllDocumentMaster();
        Task<IEnumerable<TotalDocumentByLeadViewModel>> GetTotalDocumentByLead();
        Task<IEnumerable<DocumentMaster>> GetDocumentMasterByLeadId(int leadId);
        Task<IEnumerable<DocumentMasterAccessViewModel>> GetDocumentMasterByLeadIdBySP(int leadId, string userName);
        Task<DocumentMaster> GetDocumentMasterById(int id);
        Task<bool> DeleteDocumentMasterById(int id);        

        #endregion

        #region Document Detail

        Task<bool> SaveDocumentDetail(DocumentDetail documentDetail);
        Task<IEnumerable<DocumentDetail>> GetDocumentDetailByMasterId(int? masterId);
        Task<bool> DeleteDocumentDetailByMasterId(int masterId);

        #endregion

        #region Document Status Log

        Task<bool> SaveDocumentStatusLog(DocumentStatusLog documentStatusLog);
        Task<IEnumerable<DocumentStatusLog>> GetDocumentStatusLogByMasterId(int? masterId);
        Task<bool> DeleteDocumentStatusLogByMasterId(int masterId);

        #endregion

        #region Assign Document

        Task<int> SaveAssignDocument(AssignDocument assignDocument);
        Task<IEnumerable<AssignDocument>> GetAssignDocumentByDocId(int docMasterId);
        Task<bool> DeleteAssignDocumentByDocId(int docMasterId);

        #endregion

        #region Assign Document Master

        Task<int> SaveAssignDocumentMaster(AssignDocumentMaster assignDocumentMaster);
        Task<IEnumerable<AssignDocumentMaster>> GetAssignDocumentMasterByDocId(int docMasterId);
        Task<bool> DeleteAssignDocumentMasterByDocId(int docMasterId);

        #endregion

    }
}
