using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Agreement
{
    [Table("AgreementCostInformation", Schema = "VMS")]
    public class AgreementCostInformation:Base
    {
        public int? agreementId { get; set; }
        public AgreementInformation agreement { get; set; }

        public int? costHeadId { get; set; }
        public AgreementCostHead costHead { get; set; }

        public decimal? value { get; set; }

        public int? periodTypeId { get; set; }
        public LimitPeriodType periodType { get; set; }

        public int? amountTypeId { get; set; }
        public LimitAmountType amountType { get; set; }
    }
}
