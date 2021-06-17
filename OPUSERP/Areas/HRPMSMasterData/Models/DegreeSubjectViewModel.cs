using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class DegreeSubjectViewModel
    {

        public int degreeId { get; set; }

        public Degree degree { get; set; }

        public int subjectId { get; set; }

        public Subject subject { get; set; }      

        public IEnumerable<RelDegreeSubject> relDegreeSubjects { get; set; }
    }
}
