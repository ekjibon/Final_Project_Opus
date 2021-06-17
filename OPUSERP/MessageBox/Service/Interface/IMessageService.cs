using OPUSERP.Areas.Message.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.MessageBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.MessageBox.Service.Interface
{
   public interface IMessageService
    {
        #region MessageGoup
        Task<int> SaveMessageGroup(MessageGroup messageGroup);
        Task<IEnumerable<MessageGroup>> GetAllMessageGroup();
        Task<IEnumerable<MessageGroup>> GetAllMessageGroupByempId(int Id);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByGroupId(int groupId);
        Task<MessageGroup> GetMessageGroupById(int id);
        Task<bool> DeleteMessageGroupById(int id);

        Task<IEnumerable<ContactListViewModel>> GetEmployeeInfoExceptMe(int empId);
        Task<IEnumerable<ContactListViewModel>> GetGroupsForMe(int empId);
        #endregion

        #region MessageGroupMember
        Task<int> SaveMessageGroupMember(MessageGroupMember messageGroup);
        Task<IEnumerable<MessageGroupMember>> GetAllMessageGroupMember();
        Task<IEnumerable<MessageGroupMember>> GetAllMessageGroupMemberBygrpId(int Id);
        Task<MessageGroupMember> GetMessageGroupMemberById(int id);
        Task<bool> DeleteMessageGroupMemberById(int id);
        #endregion

        #region MessageBoxInfo
        Task<int> SaveMessageBoxInfo(MessageBoxInfo messageGroup);
        Task<IEnumerable<MessageBoxInfo>> GetAllMessageBoxInfo();
        Task<IEnumerable<MessageBoxInfo>> GetAllMessageBoxInfoBygrpId(int Id);
        Task<MessageBoxInfo> GetMessageBoxInfoById(int id);
        Task<bool> DeleteMessageBoxInfoById(int id);

        Task<IEnumerable<SingleMessageConversation>> GetAllMessageBoxInfoBysend(int sender, int receiver);
        Task<IEnumerable<SingleMessageConversation>> GetAllMessageBoxInfoBygroup(int id);
        #endregion

        #region MessageBoxFile
        Task<int> SaveMessageBoxFile(MessageBoxFile messageGroup);
        Task<IEnumerable<MessageBoxFile>> GetAllMessageBoxFile();
        Task<IEnumerable<MessageBoxFile>> GetAllMessageBoxFileByMessageId(int Id);
        Task<MessageBoxFile> GetMessageBoxFileById(int id);
        Task<bool> DeleteMessageBoxFileById(int id);

        Task<ContactListViewModel> GetMessageEmployeeInfoById(int id);
        #endregion
    }
}
