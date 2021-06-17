using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HrProgramViewModel
    {
        public int hrProgramId { get; set; }

        public string programName { get; set; }
        public string programNameBn { get; set; }
        public string shortName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public IEnumerable<HrProgram> hrPrograms { get; set; }
    }
}
