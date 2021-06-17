using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ProjectWiseEquipment", Schema = "SCM")]
    public class ProjectWiseEquipment:Base
    {
        public int? projectId { get; set; }
        public Project project { get; set; }
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public int? status { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string remarks { get; set; }
    }
}
