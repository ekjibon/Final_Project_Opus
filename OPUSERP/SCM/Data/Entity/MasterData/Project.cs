using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("Project", Schema = "SCM")]
    public class Project:Base
    {
        [MaxLength(250)]
        public string projectName { get; set; }
        [MaxLength(250)]
        public string projectNameBN { get; set; }
        [MaxLength(50)]
        public string projectShortName { get; set; }
        [MaxLength(450)]
        public string projectLocation { get; set; }
        [MaxLength(20)]
        public string inChargeEmpCode { get; set; }
        [MaxLength(20)]
        public string projectStatus { get; set; }
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? isdefault { get; set; }
    }
}
