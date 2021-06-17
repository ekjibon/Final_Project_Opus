using OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.PromotionAndTransfer.Interfaces
{
    public interface IPromotionTransferEntryService
    {
        // PromotionEntry
        Task<bool> SavePromotionEntry(PromotionEntry promotionEntry);
        Task<IEnumerable<PromotionEntry>> GetPromotionEntry();
        Task<IEnumerable<PromotionEntry>> GetPromotionEntryByOrg(string org);
        Task<IEnumerable<PromotionEntry>> GetPromotionEntryByStatus(string status);
        Task<IEnumerable<PromotionEntry>> GetPromotionEntryByStatusAndOrg(string status, string org);
        Task<PromotionEntry> GetPromotionEntryById(int id);
        Task<bool> DeletePromotionEntryById(int id);
        Task<IEnumerable<PromotionEntry>> GetPromotionEntryByEmpId(int empId);
        Task<bool> UpdatePromotionStatus(int Id, string Type);

        // TransferEntry
        Task<bool> SaveTransferEntry(TransferEntry promotionEntry);
        Task<IEnumerable<TransferEntry>> GetTransferEntry();
        Task<IEnumerable<TransferEntry>> GetTransferEntryByOrg(string org);
        Task<IEnumerable<TransferEntry>> GetTransferEntryByStatus(string status);
        Task<IEnumerable<TransferEntry>> GetTransferEntryByStatusByOrg(string status,string org);
        Task<TransferEntry> GetTransferEntryById(int id);
        Task<bool> DeleteTransferEntryById(int id);
        Task<IEnumerable<TransferEntry>> GetTransferEntryByEmpId(int empId);
        Task<bool> UpdateTransferStatus(int Id, string Type);
    }
}
