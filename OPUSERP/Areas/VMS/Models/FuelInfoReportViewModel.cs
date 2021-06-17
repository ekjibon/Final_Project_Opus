using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class FuelInfoReportViewModel
    {
        public string vehicleName { get; set; }
  
        public string referenceNo { get; set; }
        public string  fuelStation { get; set; }
        public DateTime? date { get; set; }
        public string fuelType { get; set; }
        public string unit { get; set; }
        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
        public decimal? total { get; set; }
        public int? stationId { get; set; }
        public int? fuelTypeId { get; set; }
        public int? vehicleId { get; set; }




    } 
}
