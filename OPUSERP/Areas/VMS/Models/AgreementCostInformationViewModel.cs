using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VMS.Models
{
    public class AgreementCostInformationViewModel
    {
        public int? agreementId { get; set; }

        public int?[] costHeadId { get; set; }

        public decimal?[] value { get; set; }

        public int?[] periodTypeId { get; set; }

        public int?[] amountTypeId { get; set; }
    }
}
