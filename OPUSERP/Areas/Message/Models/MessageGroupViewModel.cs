using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.MessageBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Message.Models
{
    public class MessageGroupViewModel
    {
        public int Id { get; set; }

        public string name { get; set; }

        public int? employeeId { get; set; }

        public int?[] ids { get; set; }

        public string tagline { get; set; }

        public string praivacy { get; set; }

        public IEnumerable<MessageGroup> messageGroups { get; set; }
        public MessageGroup messageGroup { get; set; }
        public EmployeeInfo employee { get; set; }
        public IEnumerable<EmployeeInfo> employees { get; set; }
        public IEnumerable<MessageGroupMember> messageGroupMembers { get; set; }
        public MessageGroupMember messageGroupMember { get; set; }

        public IEnumerable<SingleMessageConversation> messageBoxInfos { get; set; }

        public IEnumerable<ContactListViewModel> contactListViewModels { get; set; }
        public IEnumerable<ContactListViewModel> grpListViewModels { get; set; }
    }
}
