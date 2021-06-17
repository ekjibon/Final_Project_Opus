using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Shagotom.Data.Entity.Visitor;

namespace OPUSERP.Areas.Shagotom.Models
{
    public class VisitorEntryViewModel
    {

        public int? masterId { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string imgUrl { get; set; }

        public string status { get; set; }
        public int? numberOfVisitor { get; set; }
        public int? detailsId { get; set; }
        public string cardNo { get; set; }

        public string[] nameArray { get; set; }
        public string[] mobileArray { get; set; }
        public string[] emailArray { get; set; }
        public string[] companyArray { get; set; }
        public string[] imgUrlArray { get; set; }
        public string[] dateArray { get; set; }
        public string[] timeArray { get; set; }


        // employee info

        public int? Id { get; set; }
        public string employeeCode { get; set; }
        public string emailAddress { get; set; }
        public string nameEnglish { get; set; }
        public string mobileNumberPersonal { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string empImgUrl { get; set; }

        
        public IEnumerable<VisitorEntryViewModel> newRequstList { get; set; }
        public IEnumerable<VisitorEntryViewModel> listOfDetailsInfoByMasterId { get; set; }
        public IEnumerable<VisitorEntryViewModel> approvedList { get; set; }
        public IEnumerable<VisitorEntryViewModel> inMeetingRoomList { get; set; }

    }
}
