using OPUSERP.Data.Entity.Master;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Models
{
    public class PatientServiceViewModel
    {
        public int patientServiceId { get; set; }
        public int? patientInfoId { get; set; }
        public int? hospitalServiceId { get; set; }
        public DateTime? serviceFromDate { get; set; }
        public DateTime? serviceToDate { get; set; }
        public string billingType { get; set; }
        public decimal? billAmount { get; set; }
        public int? itemId { get; set; }
        public int?[] headIdAll { get; set; }

        public IEnumerable<ItemCategory> hospitalServices { get; set; }
        public IEnumerable<PatientService> patientServices { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<PatientServiceDetail> patientServiceDetails { get; set; }
        //public IEnumerable<Item> items { get; set; }
    }
}
