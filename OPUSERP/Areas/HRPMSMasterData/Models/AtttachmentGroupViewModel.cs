using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class AtttachmentGroupViewModel
    {
        public int? groupId { get; set; }
        public string groupName { get; set; }
        public string groupNameBn { get; set; }

        public IEnumerable<AtttachmentGroup> atttachmentGroups { get; set; }

    }

}
