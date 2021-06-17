using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class AttachmentCategoryViewModel
    {
        public int? categoryId { get; set; }
        public int? atttachmentGroupId { get; set; }
        public string categoryName { get; set; }

        public IEnumerable<AtttachmentGroup> atttachmentGroups { get; set; }
        public IEnumerable<AtttachmentCategory> atttachmentCategories { get; set; }

    }

}
