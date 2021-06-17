using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("ExamType", Schema = "RCRT")]
    public class ExamType : Base
    {
        public string examTypeName { get; set; }
    }
}
