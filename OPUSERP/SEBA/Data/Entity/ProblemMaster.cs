using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SEBA.Data.Entity
{
    [Table("ProblemMaster", Schema = "SEBA")]
    public class ProblemMaster : Base
    {       
        [MaxLength(20)]
        public string tokenNo { get; set; }
        [MaxLength(10)]
        public string priority { get; set; }
        public DateTime? targetDate { get; set; }        
        
    }
}
