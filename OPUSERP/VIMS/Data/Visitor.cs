using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VIMS.Data
{
    public class Visitor : Base
    {
        public string contactPerson { get; set; }

        //public int? typeOfNDAAgree { get; set; }

        public string department { get; set; }

        public string contactNo { get; set; }

        public string designation { get; set; }

        public string email { get; set; }

        public string floorNo { get; set; }

        public string visitorName { get; set; }

        public string visitorContact { get; set; }

        public string visitorCompany { get; set; }

        public string visitorEmail { get; set; }

        public string imagePath { get; set; }
    }
}
