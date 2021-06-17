using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Patient.Data.Entity
{
    [Table("DiseaseInfo", Schema = "HOSPTL")]
    public class DiseaseInfo : Base
    {
        [MaxLength(250)]
        public string diseaseName { get; set; }

        [MaxLength(550)]
        public string diseaseDescription { get; set; }

    }
}
