using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VMSSupplier", Schema = "VMS")]
    public class VMSSupplier:Base
    {
        [MaxLength(250)]
        public string supplierName { get; set; }
        [MaxLength(20)]
        public string supplierCode { get; set; }
        [MaxLength(20)]
        public string phoneNo { get; set; }
        [MaxLength(100)]
        public string website { get; set; }
        [MaxLength(20)]
        public string tinNo { get; set; }
        [MaxLength(20)]
        public string vatRegNo { get; set; }
        [MaxLength(20)]
        public string isFuel { get; set; }
        [MaxLength(20)]
        public string isService { get; set; }
        [MaxLength(20)]
        public string isParts { get; set; }
        [MaxLength(20)]
        public string isVehicle { get; set; }
    }
}
