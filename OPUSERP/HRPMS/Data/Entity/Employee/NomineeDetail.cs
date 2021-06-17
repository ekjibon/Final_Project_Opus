using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("NomineeDetail", Schema = "HR")]
    public class NomineeDetail:Base
    {
        public int? nomineeFundId { get; set; }
        public NomineeFund nomineeFund { get; set; }

        public int? nomineeId { get; set; }
        public Nominee nominee { get; set; }

        public decimal? persentence { get; set; }
    }
}
