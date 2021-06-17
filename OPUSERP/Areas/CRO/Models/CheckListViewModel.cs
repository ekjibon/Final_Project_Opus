using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class CheckListViewModel
    {
        public OperationMaster operationMaster { get; set; }
        public Contacts contacts { get; set; } 
        public IEnumerable<LeadsBankInfo> leadsBankInfo { get; set; }
        public Agreement agreement { get; set; }
        public Company company { get; set; } 
        public AspNetUsersViewModel aspNetUsersViewModel { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Contacts> contactlist { get; set; }
        public ApprovedforCro approvedforCro { get; set; }
        public ProposedRating proposedRating { get; set; }
        public IEnumerable<IRCMeetingAttendance> iRCMeetingAttendances { get; set; }
        public IEnumerable<IRCMemberComments> iRCMemberComments { get; set; }
        public IRCRating iRCRating { get; set; }
        public IEnumerable<IRCSignatory> IRCSignatories { get; set; }
        public IEnumerable<Archive>  archives { get; set; }
    }
}
