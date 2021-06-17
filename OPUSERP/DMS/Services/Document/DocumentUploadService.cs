using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.DMS.Models;
using OPUSERP.Data;
using OPUSERP.DMS.Data.Entity;
using OPUSERP.DMS.Services.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.DMS.Services.Document
{
    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly ERPDbContext _context;

        public DocumentUploadService(ERPDbContext context)
        {
            _context = context;
        }

        #region FieldType
        public async Task<bool> DeleteFieldTypeById(int id)
        {
            _context.fieldTypes.Remove(_context.fieldTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FieldType>> GetFieldType()
        {
            return await _context.fieldTypes.AsNoTracking().ToListAsync();
        }

        public async Task<FieldType> GetFieldTypeById(int id)
        {
            return await _context.fieldTypes.FindAsync(id);
        }

        public async Task<bool> SaveFieldType(FieldType fieldType)
        {
            if (fieldType.Id != 0)
                _context.fieldTypes.Update(fieldType);
            else
                _context.fieldTypes.Add(fieldType);

            return 1 == await _context.SaveChangesAsync();
        }
#endregion

        #region Document Category
        public async Task<bool> DeleteDocumentCategoryById(int id)
        {
            _context.documentCategories.Remove(_context.documentCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentCategory>> GetDocumentCategory()
        {
            return await _context.documentCategories.AsNoTracking().ToListAsync();
        }

        public async Task<DocumentCategory> GetDocumentCategoryById(int id)
        {
            return await _context.documentCategories.FindAsync(id);
        }

        public async Task<bool> SaveDocumentCategory(DocumentCategory documentCategory)
        {
            if (documentCategory.Id != 0)
                _context.documentCategories.Update(documentCategory);
            else
                _context.documentCategories.Add(documentCategory);

            return 1 == await _context.SaveChangesAsync();
        }
#endregion

        #region Field Settings
        public async Task<bool> DeleteFieldSettingsById(int id)
        {
            _context.fieldSettings.Remove(_context.fieldSettings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FieldSettings>> GetFieldSettings()
        {
            return await _context.fieldSettings.Include(a=>a.documentCategory).Include(a => a.fieldType).AsNoTracking().ToListAsync();
        }

        public async Task<FieldSettings> GetFieldSettingsById(int id)
        {
            return await _context.fieldSettings.FindAsync(id);
        }

        public async Task<bool> SaveFieldSettings(FieldSettings fieldSetting)
        {
            if (fieldSetting.Id != 0)
                _context.fieldSettings.Update(fieldSetting);
            else
                _context.fieldSettings.Add(fieldSetting);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FieldSettings>> GetDocumentDetailsByDocType(int Id)
        {
            return await _context.fieldSettings.Include(x=>x.fieldType).Include(x => x.documentCategory).Where(x => x.documentCategoryId == Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Document Master

        public async Task<int> SaveDocumentMaster(DocumentMaster documentMaster)
        {
            if (documentMaster.Id != 0)
                _context.documentMasters.Update(documentMaster);
            else
                _context.documentMasters.Add(documentMaster);
            await _context.SaveChangesAsync();
            return documentMaster.Id;
        }

        public async Task<IEnumerable<DocumentMaster>> GetAllDocumentMaster()
        {
            return await _context.documentMasters.Include(a => a.documentCategory).Include(a => a.employeeInfo).Include(a => a.leads).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<IEnumerable<TotalDocumentByLeadViewModel>> GetTotalDocumentByLead()
        {
            return await _context.totalDocumentByLeadViewModels.FromSql($"SP_GETTotalDocumentByLead").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DocumentMaster>> GetDocumentMasterByLeadId(int leadId)
        {
            return await _context.documentMasters.Include(a => a.documentCategory).Include(a => a.employeeInfo).Include(a => a.leads).Where(a => a.leadsId == leadId).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<IEnumerable<DocumentMasterAccessViewModel>> GetDocumentMasterByLeadIdBySP(int leadId,string userName)
        {
            return await _context.documentMasterAccessViewModels.FromSql($"SP_DocumentMasterAccessView {leadId},{userName}").AsNoTracking().ToListAsync();
        }

        public async Task<DocumentMaster> GetDocumentMasterById(int id)
        {
            return await _context.documentMasters.Include(a => a.documentCategory).Include(a => a.employeeInfo).Include(a => a.leads).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteDocumentMasterById(int id)
        {
            _context.documentMasters.Remove(_context.documentMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
       
        #endregion

        #region Document Detail

        public async Task<bool> SaveDocumentDetail(DocumentDetail documentDetail)
        {
            if (documentDetail.Id != 0)
                _context.documentDetails.Update(documentDetail);
            else
                _context.documentDetails.Add(documentDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentDetail>> GetDocumentDetailByMasterId(int? masterId)
        {
            return await _context.documentDetails.Include(a => a.fieldSetting).Include(a => a.fieldSetting.fieldType).Where(a => a.documentMasterId == masterId).ToListAsync();
        }

        public async Task<bool> DeleteDocumentDetailByMasterId(int masterId)
        {
            _context.documentDetails.RemoveRange(_context.documentDetails.Where(x => x.documentMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Document Status Log

        public async Task<bool> SaveDocumentStatusLog(DocumentStatusLog documentStatusLog)
        {
            if (documentStatusLog.Id != 0)
                _context.documentStatusLogs.Update(documentStatusLog);
            else
                _context.documentStatusLogs.Add(documentStatusLog);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentStatusLog>> GetDocumentStatusLogByMasterId(int? masterId)
        {
            return await _context.documentStatusLogs.Include(a => a.documentMaster).Where(a => a.documentMasterId == masterId).OrderByDescending(a=>a.Id).ToListAsync();
        }

        public async Task<bool> DeleteDocumentStatusLogByMasterId(int masterId)
        {
            _context.documentStatusLogs.RemoveRange(_context.documentStatusLogs.Where(x => x.documentMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Assign Document

        public async Task<int> SaveAssignDocument(AssignDocument assignDocument)
        {
            if (assignDocument.Id != 0)
            {
                _context.assignDocuments.Update(assignDocument);
            }
            else
            {
                _context.assignDocuments.Add(assignDocument);
            }
            await _context.SaveChangesAsync();
            return assignDocument.Id;
        }

        public async Task<IEnumerable<AssignDocument>> GetAssignDocumentByDocId(int docMasterId)
        {
            return await _context.assignDocuments.Include(a => a.employeeInfo).Where(a => a.documentMasterId == docMasterId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAssignDocumentByDocId(int docMasterId)
        {
            _context.assignDocuments.RemoveRange(_context.assignDocuments.Where(x => x.documentMasterId == docMasterId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Assign Document Master

        public async Task<int> SaveAssignDocumentMaster(AssignDocumentMaster assignDocumentMaster)
        {
            if (assignDocumentMaster.Id != 0)
            {
                _context.assignDocumentMasters.Update(assignDocumentMaster);
            }
            else
            {
                _context.assignDocumentMasters.Add(assignDocumentMaster);
            }
            await _context.SaveChangesAsync();
            return assignDocumentMaster.Id;
        }

        public async Task<IEnumerable<AssignDocumentMaster>> GetAssignDocumentMasterByDocId(int docMasterId)
        {
            return await _context.assignDocumentMasters.Include(a => a.employeeInfo).Where(a => a.documentMasterId == docMasterId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAssignDocumentMasterByDocId(int docMasterId)
        {
            _context.assignDocumentMasters.RemoveRange(_context.assignDocumentMasters.Where(x => x.documentMasterId == docMasterId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
