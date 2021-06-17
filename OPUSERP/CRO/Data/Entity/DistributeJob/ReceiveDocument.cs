using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("ReceiveDocument", Schema = "CRO")]
    public class ReceiveDocument : Base
    {
        public int? operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        public int? documentChartId { get; set; }
        public DocumentChart documentChart{ get; set; }

        public int? archieveId { get; set; }
        public string description { get; set; }
        public DateTime? receiveDate { get; set; }
    }
}
