using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosCustomer", Schema = "POS")]
    public class PosCustomer : Base
    {
        [MaxLength(200)]
        public string name { get; set; }
        [MaxLength(50)]
        public string phone { get; set; }
        
        public string address { get; set; }
    }
}
