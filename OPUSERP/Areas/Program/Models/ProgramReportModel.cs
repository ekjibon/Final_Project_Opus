using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramReportModel
    {
        public ProgramMaster programMaster { get; set; }
        public IEnumerable<ProgramHeadline> programHeadlines { get; set; }
        public IEnumerable<ProgramSubject> programSubjects { get; set; }
        public IEnumerable<ProgramAddress> programAddresses { get; set; }
        public IEnumerable<ProgramViewer> programViewers { get; set; }
        public List<ProgramPeopleAttendee> programAttendees { get; set; }
        public IEnumerable<ProgramAttachment> programAttachments { get; set; }
        public IEnumerable<ProgramPeopleInDiscussion> programPeopleInDiscussions { get; set; }
    }
}
