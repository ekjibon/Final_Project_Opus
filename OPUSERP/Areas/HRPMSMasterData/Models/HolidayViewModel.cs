using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HolidayViewModel
    {
        public int holidayId { get; set; }
        public string holidayName { get; set; }
        public string holidayNameBn { get; set; }
        public DateTime? weeklyHoliday { get; set; }
        public int? year { get; set; }

        public IEnumerable<Holiday> holidays { get; set; }
        public IEnumerable<SalaryYear> salaryYearsList { get; set; }


    }
}
