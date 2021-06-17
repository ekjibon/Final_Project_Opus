using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Models
{
    public class PatientContactViewModel
    {
        public int resourceId { get; set; }
        public int contactId { get; set; }
        public int? patientInfoId { get; set; }

        public string contactName { get; set; }
        public string contactAddress { get; set; }
        public string contactMobile { get; set; }
        public string contactEmail { get; set; }
        public int? contactPersonAge { get; set; }
        public string contactRelation { get; set; }
        public string gender { get; set; }
        public int? professionTypeId { get; set; }

        public IEnumerable<ProfessionType> professionTypes { get; set; }
        public IEnumerable<Contacts> patientContacts { get; set; }
    }
}
