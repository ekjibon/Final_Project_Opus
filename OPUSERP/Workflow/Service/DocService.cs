using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Workflow.Models;
using OPUSERP.Data;
using OPUSERP.Workflow.Data.Entity;
using OPUSERP.Workflow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Service
{
    public class DocService : IDocService
    {
        private readonly ERPDbContext _context;

        public DocService(ERPDbContext context)
        {
            _context = context;
        }

        #region Doc

        public async Task<int> SaveDoc(Doc doc)
        {
            if (doc.Id != 0)
                _context.docs.Update(doc);
            else
                _context.docs.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<Doc>> GetAllDoc()
        {
            return await _context.docs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Doc>> GetAllCreaedDoc(int empID)
        {
            return await _context.docs.Where(x => x.employeeId == empID).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Doc>> GetAllProcessedDoc(int empID)
        {
            return await _context.docs.Where(x => x.employeeId == empID && x.isClose == 1).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Doc>> GetAllActiveDoc(int empID)
        {
            return await _context.docs.Where(x => x.employeeId == empID && x.isClose == 0).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Doc>> GetAllReturnDoc(int empID)
        {
            return await _context.docs.Where(x => x.employeeId == empID && x.isClose == 2).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Doc>> GetAllManagedDoc(int empID)
        {
            List<int?> docIds = await _context.docRoutes.Where(x => x.employeeId == empID && x.actionId != null).Select(x => x.docId).ToListAsync();
            return await _context.docs.Where(x => docIds.Contains(x.Id)).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Doc>> GetAllPendingDoc(int empID)
        {
            List<int?> docIds = await _context.docRoutes.Where(x => x.employeeId == empID && x.isActive == 1).Select(x => x.docId).ToListAsync();
            return await _context.docs.Where(x => docIds.Contains(x.Id)).OrderByDescending(x => x.docId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<DocWithReviewIdModel>> GetAllPendingDocforRevew(int empID)
        {
            List<ReviewRoute> reviewRoutes = await _context.reviewRoutes.Where(x => x.employeeId == empID && x.actionId == null).Include(x=>x.docRoute).ToListAsync();
            List<DocWithReviewIdModel> docWithReviewIdModels = new List<DocWithReviewIdModel>();
            foreach (var data in reviewRoutes)
            {
                docWithReviewIdModels.Add(new DocWithReviewIdModel {
                    doc = await _context.docs.Where(x=>x.Id==data.docRoute.docId).FirstOrDefaultAsync(),
                    reviewId = data.Id
                });
            }
            return docWithReviewIdModels;
        }

        public async Task<Doc> GetDocById(int id)
        {
            return await _context.docs.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DocWithSgnature> GetDocByIdWithSignature(int id)
        {
            var doc = await _context.docs.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
            DocWithSgnature docWithSgnature = new DocWithSgnature
            {
                doc = doc,
                employeeSignature = await _context.employeeSignatures.Where(x => x.employeeId == doc.employeeId).FirstOrDefaultAsync()
            };
            return docWithSgnature;
        }

        public async Task<bool> DeleteDocById(int id)
        {
            _context.docs.Remove(_context.docs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateDocToCloded(int docId)
        {
            var user = _context.docs.Find(docId);
            user.isClose = 1;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateDocToClodedReturn(int docId)
        {
            var user = _context.docs.Find(docId);
            user.isClose = 2;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion

        #region ActionInfo
        public async Task<int> SaveActionInfo(ActionInfo doc)
        {
            if (doc.Id != 0)
                _context.actionInfos.Update(doc);
            else
                _context.actionInfos.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<ActionInfo>> GetAllActionInfo()
        {
            return await _context.actionInfos.AsNoTracking().ToListAsync();
        }

        public async Task<ActionInfo> GetActionInfoById(int id)
        {
            return await _context.actionInfos.FindAsync(id);
        }

        public async Task<bool> DeleteActionInfoById(int id)
        {
            _context.actionInfos.Remove(_context.actionInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region DocAttachment
        public async Task<int> SaveDocAttachment(DocAttachment doc)
        {
            if (doc.Id != 0)
                _context.docAttachments.Update(doc);
            else
                _context.docAttachments.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<DocAttachment>> GetAllDocAttachment()
        {
            return await _context.docAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DocAttachment>> GetAllDocAttachmentByDocId(int id)
        {
            return await _context.docAttachments.Where(x => x.docId == id).AsNoTracking().ToListAsync();
        }

        public async Task<DocAttachment> GetDocAttachmentById(int id)
        {
            return await _context.docAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDocAttachmentById(int id)
        {
            _context.docAttachments.Remove(_context.docAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region ReviewRoute
        public async Task<int> SaveReviewRoute(ReviewRoute doc)
        {
            if (doc.Id != 0)
                _context.reviewRoutes.Update(doc);
            else
                _context.reviewRoutes.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<ReviewRoute>> GetAllReviewRoute()
        {
            return await _context.reviewRoutes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ReviewRoute>> GetAllReviewRouteByRouteId(int id)
        {
            return await _context.reviewRoutes.Where(x => x.docRoute.docId == id).Include(x => x.docRoute).Include(x => x.employee).Include(x => x.action).AsNoTracking().ToListAsync();
        }

        public async Task<ReviewRoute> GetReviewRouteById(int id)
        {
            return await _context.reviewRoutes.FindAsync(id);
        }

        public async Task<bool> DeleteReviewRouteById(int id)
        {
            _context.reviewRoutes.Remove(_context.reviewRoutes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateActionIdInReviewRoute(int routeId, int actionId)
        {
            var user = _context.reviewRoutes.Find(routeId);
            user.actionId = actionId;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region DocRoute
        public async Task<int> SaveDocRoute(DocRoute doc)
        {
            if (doc.Id != 0)
                _context.docRoutes.Update(doc);
            else
                _context.docRoutes.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<DocRoute>> GetAllDocRoute()
        {
            return await _context.docRoutes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DocRoute>> GetAllDocRouteByDocId(int id)
        {
            return await _context.docRoutes.Where(x => x.docId == id).Include(x => x.action).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DocRoute>> GetAllDocRouteByDocIdDecendaing(int id)
        {
            return await _context.docRoutes.Where(x => x.docId == id).Include(x => x.action).Include(x => x.employee).OrderByDescending(x => x.order).AsNoTracking().ToListAsync();
        }

        public async Task<DocRoute> GetDocRouteById(int id)
        {
            return await _context.docRoutes.FindAsync(id);
        }

        public async Task<DocRoute> GetDocRouteByEmpIdAndDocId(int id, int DocId)
        {
            return await _context.docRoutes.Where(x => x.docId == DocId && x.employeeId == id).FirstOrDefaultAsync();
        }

        public async Task<DocRoute> GetDocRouteByDocIdAndOrder(int docId, int order)
        {
            return await _context.docRoutes.Where(x => x.order == order && x.docId == docId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteDocRouteById(int id)
        {
            _context.docRoutes.Remove(_context.docRoutes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RouteWithSignatureforDoc>> GetAllDocRouteByDocIdWithSignature(int id)
        {
            var route = await _context.docRoutes.Where(x => x.docId == id).Include(x => x.action).Include(x => x.employee).OrderBy(x => x.order).AsNoTracking().ToListAsync();
            List<RouteWithSignatureforDoc> routeWithSignatureforDocs = new List<RouteWithSignatureforDoc>();
            foreach (var data in route)
            {
                routeWithSignatureforDocs.Add(new RouteWithSignatureforDoc
                {
                    docRoute = data,
                    employeeSignature = await _context.employeeSignatures.Where(x => x.employeeId == data.employeeId).FirstOrDefaultAsync()
                });
            }
            return routeWithSignatureforDocs;
        }

        public void UpdateActionIdInRoute(int routeId, int actionId)
        {
            var user = _context.docRoutes.Find(routeId);
            user.actionId = actionId;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateRouteOrder(int routeId, int order)
        {
            var user = _context.docRoutes.Find(routeId);
            user.order = order;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateRouteStatus(int routeId, int order)
        {
            var user = _context.docRoutes.Find(routeId);
            user.isActive = order;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region DraftDoc
        public async Task<int> SaveDraftDoc(DraftDoc doc)
        {
            if (doc.Id != 0)
                _context.draftDocs.Update(doc);
            else
                _context.draftDocs.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<DraftDoc>> GetAllDraftDoc()
        {
            return await _context.draftDocs.AsNoTracking().ToListAsync();
        }

        public async Task<DraftDoc> GetDraftDocById(int id)
        {
            return await _context.draftDocs.FindAsync(id);
        }

        public async Task<bool> DeleteDraftDocById(int id)
        {
            _context.draftDocs.Remove(_context.draftDocs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region DraftAttachment
        public async Task<int> SaveDraftAttachment(DraftAttachment doc)
        {
            if (doc.Id != 0)
                _context.draftAttachments.Update(doc);
            else
                _context.draftAttachments.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<DraftAttachment>> GetAllDraftAttachment()
        {
            return await _context.draftAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<DraftAttachment> GetDraftAttachmentById(int id)
        {
            return await _context.draftAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDraftAttachmentById(int id)
        {
            _context.draftAttachments.Remove(_context.draftAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region DraftRouting
        public async Task<int> SaveDraftRouting(DraftRouting doc)
        {
            if (doc.Id != 0)
                _context.draftRoutings.Update(doc);
            else
                _context.draftRoutings.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<DraftRouting>> GetAllDraftRouting()
        {
            return await _context.draftRoutings.AsNoTracking().ToListAsync();
        }

        public async Task<DraftRouting> GetDraftRoutingById(int id)
        {
            return await _context.draftRoutings.FindAsync(id);
        }

        public async Task<bool> DeleteDraftRoutingById(int id)
        {
            _context.draftRoutings.Remove(_context.draftRoutings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Urlgeneration
        public async Task<int> SaveUrlgeneration(Urlgeneration doc)
        {
            if (doc.Id != 0)
                _context.urlgenerations.Update(doc);
            else
                _context.urlgenerations.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<Urlgeneration>> GetAllUrlgeneration()
        {
            return await _context.urlgenerations.AsNoTracking().ToListAsync();
        }

        public async Task<Urlgeneration> GetUrlgenerationById(int id)
        {
            return await _context.urlgenerations.FindAsync(id);
        }

        public async Task<bool> DeleteUrlgenerationById(int id)
        {
            _context.urlgenerations.Remove(_context.urlgenerations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


    }
}
