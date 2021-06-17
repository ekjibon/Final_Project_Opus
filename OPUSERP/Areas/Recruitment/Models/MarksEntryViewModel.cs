using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class MarksEntryViewModel
    {
        public int?[] examTypeId { get; set; }
        public int? marksId { get; set; }
        public int? candidateId { get; set; }
        public int? reqId { get; set; }
        public string examOrder { get; set; }
        public string remarks { get; set; }
        public int?[] percentage { get; set; }
        public int?[] marks { get; set; }
        public string[] status { get; set; }
        public int?[] detailids { get; set; }

        public IEnumerable<ResultMaster> resultMasters { get; set; }
        public IEnumerable<ResultDetails> resultDetails { get; set; }
    }
}
