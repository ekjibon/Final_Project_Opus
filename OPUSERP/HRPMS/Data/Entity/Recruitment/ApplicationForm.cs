using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("ApplicationForm", Schema = "HR")]
    public class ApplicationForm : Base
    {
        public string nameEN { get; set; }
        public string nameBN { get; set; }
        [MaxLength(100)]
        public string nidNO { get; set; }
        [MaxLength(100)]
        public string binNO { get; set; }
        public DateTime birthDate { get; set; }
        [MaxLength(100)]
        public string birtPlace { get; set; }
        public string payRef { get; set; }
        public string  payDoc { get; set; }
        public string fNmaeEN { get; set; }
        public string fNmaeBN { get; set; }
        public string mNmaeBN { get; set; }
        public string mNmaeEN { get; set; }
        public string sNameEN { get; set; }
        public string sNameBN { get; set; }
        [MaxLength(50)]
        public string mobile { get; set; }
        [MaxLength(250)]
        public string email { get; set; }
        [MaxLength(50)]
        public string nationality { get; set; }
        public int? religionId { get; set; }
        //public Religion religion { get; set; }
        public string occupation { get; set; }
        public string gender { get; set; }

        public string type { get; set; }
    }
}
