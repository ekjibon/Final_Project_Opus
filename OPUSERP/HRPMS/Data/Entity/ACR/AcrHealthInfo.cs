using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("AcrHealthInfo", Schema = "HR")]
    public class AcrHealthInfo : Base
    {
        public int? acrInitiateId { get; set; }
        public AcrInitiate acrInitiate { get; set; }

        public string height { get; set; }

        public string weight { get; set; }

        public string vision { get; set; }

        public string bloodGroup { get; set; }

        public string bloodPressure { get; set; }

        public string xray { get; set; }

        public string ecg { get; set; }

        public string treatementClassification { get; set; }

        public string physicalDisorder { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? signDate { get; set; }

        public string hoName { get; set; }

        public string hoDesignation { get; set; }

        public string hoOrganization { get; set; }

    }
}
