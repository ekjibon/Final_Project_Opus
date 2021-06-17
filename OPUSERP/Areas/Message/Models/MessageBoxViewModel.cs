using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.MessageBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Message.Models
{
    public class MessageBoxViewModel
    {
        public IEnumerable<MessageGroup> messageGroups { get; set; }
        public IEnumerable<EmployeeInfo> employees { get; set; }
        public IEnumerable<ContactListViewModel> contactListViewModels { get; set; }
        public IEnumerable<ContactListViewModel> grpListViewModels { get; set; }
    }
}
