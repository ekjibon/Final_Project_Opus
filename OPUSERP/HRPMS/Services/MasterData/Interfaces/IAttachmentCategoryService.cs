using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IAttachmentCategoryService
    {
        #region Atttachment Group
        Task<bool> SaveAtttachmentGroup(AtttachmentGroup atttachmentGroup);
        Task<IEnumerable<AtttachmentGroup>> GetAllAtttachmentGroup();
        Task<AtttachmentGroup> GetAtttachmentGroupById(int id);
        Task<bool> DeleteAtttachmentGroupById(int id);
        #endregion

        #region Atttachment Category
        Task<bool> SaveAttachmentCategory(AtttachmentCategory  attachmentCategory);
        Task<IEnumerable<AtttachmentCategory>> GetAllAttachmentCategory();
        Task<IEnumerable<AtttachmentCategory>> GetAllAttachmentCategoryByGroupId(int groupId);
        Task<AtttachmentCategory> GetAttachmentCategoryById(int id);
        Task<bool> DeleteAttachmentCategoryById(int id);
        #endregion
    }
}
