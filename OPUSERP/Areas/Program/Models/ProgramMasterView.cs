using Microsoft.AspNetCore.Http;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramMasterView : Base
    {
        public int masterId { get; set; }

        public int? activityId { get; set; }
        public int? outcomestatusId { get; set; }
        public int? outputstatusId { get; set; }
        public string description { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public DateTime? date { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public string[] place { get; set; }
        public string[] partner { get; set; }
        public string[] source { get; set; }
        public decimal?[] percent { get; set; }
        public decimal?[] amount { get; set; }
        public string[] approveBudget { get; set; }
        public decimal?[] apppercent { get; set; }
        public int? isDiscussion { get; set; }
        public int? programYearId { get; set; }

        public Decimal? totalBudget { get; set; }

        public string subject { get; set; }
        public string grantNumber { get; set; }

        public int? programCategoryId { get; set; }
        public ProgramCategory programCategory { get; set; }

        public int? thanaId { get; set; }

        public int? districtId { get; set; }

        public int? divisionId { get; set; }
        public int? programImpactId { get; set; }

        public string[] headlineAll { get; set; }
        public string[] subjectNameAll { get; set; }
        public string[] programNameAll { get; set; }

        public string[] discussionNameAll { get; set; }
        public decimal[] discussionQuantityAll { get; set; }

        public string[] attendeeNameAll { get; set; }
        public string[] attendeeAddressAll { get; set; }
        public string[] attendeeContactAll { get; set; }
        public string[] attendeeTypeAll { get; set; }

        public string[] viewerNameAll { get; set; }
        public decimal[] viewerQuantityAll { get; set; }

        public string[] captionAll { get; set; }
        public IFormFile[] attachmentFileAll { get; set; }

        public IEnumerable<ProgramMainCategory> programMainCategories { get; set; }
        public IEnumerable<ProgramCategory> programCategorys { get; set; }
        public ProgramReportListModel programReportListModel { get; set; }
        public ProgramReportModel programReportModels { get; set; }
        public IEnumerable<ProgramMaster> programMasters { get; set; }
        public IEnumerable<Division>  divisions { get; set; }
        public IEnumerable<District>  districts { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<ProgramYear> programYears { get; set; }
        public IEnumerable<ProgramImpact> programImpacts { get; set; }
        public IEnumerable<ProgramStatus> programStatuses { get; set; }
    

        public string[] outcome { get; set; }
        public string[] output { get; set; }
        public string[] outPutValues { get; set; }
        public string[] activity { get; set; }
        public string[] indicator { get; set; }
        public string[] activityv { get; set; }
        public string[] outputIndicator { get; set; }

        public int?[] outcomeIdAll { get; set; }
        public int?[] outputIdAll { get; set; }

        public IEnumerable<ProgramPeopleAttendee> programPeopleAttendees { get; set; }
        public IEnumerable<ProgramOutCome> programOutComes { get; set; }
        public IEnumerable<ProgramOutPut> programOutPuts { get; set; }
        public IEnumerable<ProgramIndicator> programIndicators { get; set; }
        public IEnumerable<ProgramActivity> ProgramActivities { get; set; }
        public IEnumerable<ProgramActivityProgres> programActivityProgres { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<ProgramLocation> programLocations { get; set; }
        public IEnumerable<ProgramImplementor> programImplementors { get; set; }
        public IEnumerable<ProgramSourceFund> programSourceFunds { get; set; }
        public IEnumerable<ProgramPlanReportViewModel> programPlanReportViewModels { get; set; }
    }
}
