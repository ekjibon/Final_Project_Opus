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
    public class ProgramAttendeeView : Base
    {
        public int? programattendeeId { get; set; }
        public int? programMasterId { get; set; }
      


        public int? programActivityId { get; set; }
       

        public string name { get; set; }

        public string union { get; set; }

        public string village { get; set; }

        public int? benificiaryTypeId { get; set; }
       

        public int? idTypeId { get; set; }
     

        public string idNumber { get; set; }

        public int? dateRangeId { get; set; }
        

        public string mobile { get; set; }

        public int? genderId { get; set; }
       

        public int? peopleTypeId { get; set; }
       
        public int? benificiaryActiveTypeId { get; set; }
       

        public string isEthnic { get; set; }
        public string isFemaleHeaded { get; set; }
        public string isLactatingMother { get; set; }

        public string address { get; set; }

        public string contact { get; set; }

        public string type { get; set; }

        public ProgramPeopleAttendee programPeopleAttendee { get; set; }
        public IEnumerable<ProgramPeopleAttendee> programPeopleAttendees { get; set; }
        public IEnumerable<BenificiaryActiveType> benificiaryActiveTypes { get; set; }
        public IEnumerable<PeopleType> peopleType { get; set; }
        public IEnumerable<Gender> genders { get; set; }
        public IEnumerable<DateRange> dateRanges { get; set; }
        public IEnumerable<IdType> idTypes { get; set; }
        public IEnumerable<BenificiaryType> benificiaryTypes { get; set; }
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
    

      

        public IEnumerable<ProgramOutCome> programOutComes { get; set; }
        public IEnumerable<ProgramOutPut> programOutPuts { get; set; }
        public IEnumerable<ProgramIndicator> programIndicators { get; set; }
        public IEnumerable<ProgramActivity> ProgramActivities { get; set; }
        public IEnumerable<ProgramActivityProgres> programActivityProgres { get; set; }
        public IEnumerable<Company> companies { get; set; }

    }
}
