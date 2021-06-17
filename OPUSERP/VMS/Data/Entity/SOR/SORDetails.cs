using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;

namespace OPUSERP.VMS.Data.Entity.SOR
{
    [Table("SORDetails", Schema = "VMS")]
    public class SORDetails : Base
    {
        public int sORMasterId { get; set; }
        public SORMaster sORMaster { get; set; }
        public int? sparePartsId { get; set; }
        public SpareParts  spareParts { get; set; }
        public int? supplierId { get; set; }
        public Supplier supplier { get; set; }

        public decimal? rate { get; set; }

        public int? isActive { get; set; }
   
    }
}
