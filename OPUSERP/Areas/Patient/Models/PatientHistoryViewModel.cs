using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Models
{
    public class PatientHistoryViewModel
    {
        public int patientHistoryId { get; set; }
        public int? patientInfoId { get; set; }
        public int? diseaseInfoId { get; set; }
        public DateTime? diseaseStartDate { get; set; }        

        public IEnumerable<DiseaseInfo> diseaseInfos { get; set; }
        public IEnumerable<PatientHistory> patientHistories { get; set; }
    }
}
