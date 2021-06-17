using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.TrainingNew
{
    [Table("ResourcePerson", Schema = "HR")]
    public class ResourcePerson : Base
    {
        public string name { get; set; }

        public string designation { get; set; }

        public string workPlace { get; set; }

        public string specialization { get; set; }

        public string contactNumber { get; set; }

        public string performance { get; set; }

        public string email { get; set; }

        public string address  { get; set; }

        public string remarks { get; set; }


        //Other For Future
        public string orgType { get; set; }
    }
}
