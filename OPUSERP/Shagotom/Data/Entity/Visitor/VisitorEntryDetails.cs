using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;

namespace OPUSERP.Shagotom.Data.Entity.Visitor
{
    [Table("VisitorEntryDetails", Schema = "Shagotom")]
    public class VisitorEntryDetails : Base
    {
        public int? visitorEntryMasterId { get; set; }
        public VisitorEntryMaster visitorEntryMaster { get; set; }

        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string imgUrl { get; set; }

        public string cardNo { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public int? status { get; set; }


    }
}
