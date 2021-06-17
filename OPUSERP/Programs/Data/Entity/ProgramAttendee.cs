using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramAttendee", Schema = "PM")]
    public class ProgramPeopleAttendee : Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }


        public int? programActivityId { get; set; }
        public ProgramActivity programActivity { get; set; }

        public string name { get; set; }

        public string union { get; set; }

        public string village { get; set; }

        public int? benificiaryTypeId { get; set; }
        public BenificiaryType benificiaryType { get; set; }

        public int? idTypeId { get; set; }
        public IdType idType { get; set; }

        public string idNumber { get; set; }

        public int? dateRangeId { get; set; }
        public DateRange dateRange { get; set; }

        public string mobile { get; set; }

        public int? genderId { get; set; }
        public Gender gender { get; set; }

        public int? peopleTypeId   {get;set;}
        public PeopleType peopleType   {get;set;}
        public int? benificiaryActiveTypeId { get; set; }
        public BenificiaryActiveType benificiaryActiveType { get; set; }

        public string isEthnic { get; set; }
        public string isFemaleHeaded { get; set; }
        public string isLactatingMother { get; set; }
       
        public string address { get; set; }

        public string contact { get; set; }

        public string type { get; set; }
    }
}
