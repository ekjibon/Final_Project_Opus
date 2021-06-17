using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseProcess.Interfaces
{
    public interface IProcurementService
    {
        #region  CS Item Category

        Task<int> SaveCSItemCategory(CSItemCategory cSItemCategory);
        Task<IEnumerable<CSItemCategory>> GetCSItemCategoryList();
        Task<bool> DeleteCSItemCategoryById(int id);

        #endregion

        #region  Procurement Type

        Task<int> SaveProcurementType(ProcurementType procurementType);
        Task<IEnumerable<ProcurementType>> GetProcurementTypeList();
        Task<bool> DeleteProcurementTypeById(int id);

        #endregion

        #region  Procurement Value

        Task<int> SaveProcurementValue(ProcurementValue procurementValue);
        Task<IEnumerable<ProcurementValue>> GetProcurementValueList();
        Task<bool> DeleteProcurementValueById(int id);

        #endregion

        #region  Justification Type

        Task<int> SaveJustificationType(JustificationType justificationType);
        Task<IEnumerable<JustificationType>> GetJustificationTypeList();
        Task<bool> DeleteJustificationTypeById(int id);

        #endregion

        #region  Justification

        Task<int> SaveJustification(Justification justification);
        void SaveJustificationList(List<Justification> justifications);
        Task<IEnumerable<Justification>> GetJustificationList();
        Task<bool> DeleteJustificationById(int id);
        Task<IEnumerable<Justification>> GetJustificationListByCSMasterId(int CSMasterId);

        #endregion



    }
}
