using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class RelDegreeSubjectViewModel
    {
        public string relDegSubId { get; set; }
        public int degreeId { get; set; }

        public Degree degree { get; set; }

        public int subjectId { get; set; }

        public Subject subject { get; set; }   
        
        public DegSubRelationInfoLn fLang { get; set; }

        public IEnumerable<RelDegreeSubject> relDegreeSubjectslist { get; set; }
        public IEnumerable<Degree> degreeslist { get; set; }
        public IEnumerable<Subject> subjectslist { get; set; }
    }
}
