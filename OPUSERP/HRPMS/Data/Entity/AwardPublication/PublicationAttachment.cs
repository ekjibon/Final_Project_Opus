using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.AwardPublication
{
    [Table("PublicationAttachment", Schema = "HR")]
    public class PublicationAttachment:Base
    {
        public int publicationId { get; set; }
        public PublicationEntry publication { get; set; }

        public string type { get; set; }

        public string fileTitle { get; set; }

        public string uploadFile { get; set; }
    }
}
