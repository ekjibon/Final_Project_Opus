using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ProjectEquipmentModel
    {
        public int? projectEquipmentId { get; set; }
        public int? itemspecificationId { get; set; }
        public int? projectId { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<ProjectWiseEquipment> ProjectWiseEquipment { get; set; }
        //public IEnumerable<ItemSpecification> itemSpecifications { get; set; }
    }
}
