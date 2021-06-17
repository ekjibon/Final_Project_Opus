using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VMSDocumentAttachment", Schema = "VMS")]
    public class VMSDocumentAttachment:Base
    {
        [MaxLength(250)]
        public string actionType { get; set; }
        public int? actionTypeId { get; set; }
        [MaxLength(250)]
        public string documentType { get; set; }
        [MaxLength(350)]
        public string fileName { get; set; }
        [MaxLength(350)]
        public string filePath { get; set; }
        [MaxLength(250)]
        public string fileType { get; set; }
        [MaxLength(350)]
        public string subject { get; set; }
        [MaxLength(450)]
        public string description { get; set; }
    }
}
