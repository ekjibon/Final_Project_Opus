using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.SOR
{
    [Table("SORMaster", Schema = "VMS")]
    public class SORMaster : Base
    {
        public SORMaster()
        {
            this.SORDetails = new HashSet<SORDetails>();
        }

        public string sorNumber { get; set; }

        public string sorTitle { get; set; }

        public string duration { get; set; }

        public int? numberOfItems { get; set; }

        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }

        public decimal? grandTotal { get; set; }
        public int? statusId { get; set; }

        public virtual ICollection<SORDetails> SORDetails { get; set; }
    }
}
