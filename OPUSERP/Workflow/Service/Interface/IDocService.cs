using OPUSERP.Areas.Workflow.Models;
using OPUSERP.Workflow.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Service.Interface
{
   public interface IDocService
    {
        #region Doc
        Task<int> SaveDoc(Doc doc);
        Task<IEnumerable<Doc>> GetAllDoc();
        Task<Doc> GetDocById(int id);
        Task<DocWithSgnature> GetDocByIdWithSignature(int id);
        Task<bool> DeleteDocById(int id);
        void UpdateDocToCloded(int docId);
        void UpdateDocToClodedReturn(int docId);

        Task<IEnumerable<Doc>> GetAllCreaedDoc(int empID);
        Task<IEnumerable<Doc>> GetAllProcessedDoc(int empID);
        Task<IEnumerable<Doc>> GetAllReturnDoc(int empID);
        Task<IEnumerable<Doc>> GetAllManagedDoc(int empID);
        Task<IEnumerable<Doc>> GetAllPendingDoc(int empID);
        Task<IEnumerable<DocWithReviewIdModel>> GetAllPendingDocforRevew(int empID);
        Task<IEnumerable<Doc>> GetAllActiveDoc(int empID);
        #endregion

        #region ActionInfo
        Task<int> SaveActionInfo(ActionInfo actionInfo);
        Task<IEnumerable<ActionInfo>> GetAllActionInfo();
        Task<ActionInfo> GetActionInfoById(int id);
        Task<bool> DeleteActionInfoById(int id);
        #endregion

        #region DocAttachment
        Task<int> SaveDocAttachment(DocAttachment docAttachment);
        Task<IEnumerable<DocAttachment>> GetAllDocAttachment();
        Task<IEnumerable<DocAttachment>> GetAllDocAttachmentByDocId(int id);
        Task<DocAttachment> GetDocAttachmentById(int id);
        Task<bool> DeleteDocAttachmentById(int id);
        #endregion

        #region ReviewRoute
        Task<int> SaveReviewRoute(ReviewRoute reviewRoute);
        Task<IEnumerable<ReviewRoute>> GetAllReviewRoute();
        Task<IEnumerable<ReviewRoute>> GetAllReviewRouteByRouteId(int id);
        Task<ReviewRoute> GetReviewRouteById(int id);
        Task<bool> DeleteReviewRouteById(int id);
        void UpdateActionIdInReviewRoute(int routeId, int actionId);
        #endregion

        #region DocRoute
        Task<int> SaveDocRoute(DocRoute docRoute);
        Task<IEnumerable<DocRoute>> GetAllDocRoute();
        Task<IEnumerable<DocRoute>> GetAllDocRouteByDocId(int id);
        Task<IEnumerable<RouteWithSignatureforDoc>> GetAllDocRouteByDocIdWithSignature(int id);
        Task<IEnumerable<DocRoute>> GetAllDocRouteByDocIdDecendaing(int id);
        Task<DocRoute> GetDocRouteByEmpIdAndDocId(int id, int DocId);
        Task<DocRoute> GetDocRouteById(int id);
        Task<bool> DeleteDocRouteById(int id);
        void UpdateActionIdInRoute(int routeId, int actionId);
        void UpdateRouteOrder(int routeId, int order);
        Task<DocRoute> GetDocRouteByDocIdAndOrder(int docId, int order);
        void UpdateRouteStatus(int routeId, int order);
        #endregion

        #region DraftDoc
        Task<int> SaveDraftDoc(DraftDoc draftDoc);
        Task<IEnumerable<DraftDoc>> GetAllDraftDoc();
        Task<DraftDoc> GetDraftDocById(int id);
        Task<bool> DeleteDraftDocById(int id);
        #endregion

        #region DraftAttachment
        Task<int> SaveDraftAttachment(DraftAttachment draftAttachment);
        Task<IEnumerable<DraftAttachment>> GetAllDraftAttachment();
        Task<DraftAttachment> GetDraftAttachmentById(int id);
        Task<bool> DeleteDraftAttachmentById(int id);
        #endregion

        #region DraftRouting
        Task<int> SaveDraftRouting(DraftRouting draftRouting);
        Task<IEnumerable<DraftRouting>> GetAllDraftRouting();
        Task<DraftRouting> GetDraftRoutingById(int id);
        Task<bool> DeleteDraftRoutingById(int id);
        #endregion

        #region Urlgeneration
        Task<int> SaveUrlgeneration(Urlgeneration urlgeneration);
        Task<IEnumerable<Urlgeneration>> GetAllUrlgeneration();
        Task<Urlgeneration> GetUrlgenerationById(int id);
        Task<bool> DeleteUrlgenerationById(int id);
        #endregion

    }
}
