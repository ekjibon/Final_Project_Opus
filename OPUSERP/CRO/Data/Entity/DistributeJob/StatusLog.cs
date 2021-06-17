using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("StatusLog", Schema = "CRO")]
    public class StatusLog : Base
    {
        public int? operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }
        public int? jobStatusId { get; set; }
        public JobStatus jobStatus { get; set; }

        [MaxLength(250)]
        public string currentUser { get; set; }
        public string remarks { get; set; }

    }
}
