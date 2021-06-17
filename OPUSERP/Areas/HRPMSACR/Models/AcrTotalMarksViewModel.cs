using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    [NotMapped]
    public class AcrTotalMarksViewModel
    {      

        public int TotalMarksObtained { get; set; }        
    }
}
