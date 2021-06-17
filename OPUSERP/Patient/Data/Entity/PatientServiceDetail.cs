using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Patient.Data.Entity
{
    [Table("PatientServiceDetail", Schema = "HOSPTL")]
    public class PatientServiceDetail : Base
    {
        public int? patientServiceId { get; set; }
        public PatientService patientService { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

    }
}
