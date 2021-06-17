using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Data.Entity
{
    [Table("DocAttachment", Schema = "WMS")]
    public class DocAttachment:Base
    {
        public int? docId { get; set; }
        public Doc doc { get; set; }

        public string fileUrl { get; set; }

        public string title { get; set; }
    }
}
