using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Message.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.MessageBox.Data;
using OPUSERP.MessageBox.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.MessageBox.Service
{
    public class MessageService : IMessageService
    {

        private readonly ERPDbContext _context;

        public MessageService(ERPDbContext context)
        {
            _context = context;
        }

        #region MessageGroup
        public async Task<int> SaveMessageGroup(MessageGroup doc)
        {
            if (doc.Id != 0)
                _context.messageGroups.Update(doc);
            else
                _context.messageGroups.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MessageGroup>> GetAllMessageGroup()
        {
            return await _context.messageGroups.Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MessageGroup>> GetAllMessageGroupByempId(int Id)
        {
            return await _context.messageGroups.Where(x => x.employeeId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByGroupId(int groupId)
        {
            List<int?> ids = await _context.messageGroupMembers.Where(x => x.messageGroupId == groupId).Select(x => x.employeeId).ToListAsync();
            return await _context.employeeInfos.Where(x => !ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ContactListViewModel>> GetEmployeeInfoExceptMe(int empId)
        {
            List<ContactListViewModel> ContactListViewModel = new List<ContactListViewModel>();
            
            var employee = await _context.employeeInfos.Where(x => x.Id != empId).AsNoTracking().ToListAsync();
            foreach (var data in employee)
            {
                string emphoto =await _context.photographs.Where(x => x.employeeId == data.Id).Select(x => x.url).FirstOrDefaultAsync();
                if (emphoto == null)
                {
                    emphoto = "images/user-icon-png.png";
                }

                ContactListViewModel.Add(new ContactListViewModel
                {
                    Id = data.Id,
                    name = data.nameEnglish,
                    code = data.employeeCode,
                    empPhoto = emphoto,

                });
            }

            return ContactListViewModel;
        }

        public async Task<IEnumerable<ContactListViewModel>> GetGroupsForMe(int empId)
        {
            List<ContactListViewModel> ContactListViewModel = new List<ContactListViewModel>();

            var grpmember = await _context.messageGroupMembers.Where(x => x.employeeId == empId).ToListAsync();

            foreach (var member in grpmember)
            {
                var grp = await _context.messageGroups.FindAsync(member.messageGroupId);

                ContactListViewModel.Add(new ContactListViewModel
                {
                    grpId = grp.Id,
                    groupName = grp.name,
                    tagline = grp.tagline,
                    grpPhoto = "images/group-icon-clipart.png",
                });

            }
            return ContactListViewModel;
        }

        public async Task<MessageGroup> GetMessageGroupById(int id)
        {
            return await _context.messageGroups.FindAsync(id);
        }

        public async Task<bool> DeleteMessageGroupById(int id)
        {
            _context.messageGroups.Remove(_context.messageGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region MessageGroupMember
        public async Task<int> SaveMessageGroupMember(MessageGroupMember doc)
        {
            if (doc.Id != 0)
                _context.messageGroupMembers.Update(doc);
            else
                _context.messageGroupMembers.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MessageGroupMember>> GetAllMessageGroupMember()
        {
            return await _context.messageGroupMembers.Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MessageGroupMember>> GetAllMessageGroupMemberBygrpId(int Id)
        {
            return await _context.messageGroupMembers.Where(x => x.messageGroupId == Id).Include(x => x.messageGroup).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<MessageGroupMember> GetMessageGroupMemberById(int id)
        {
            return await _context.messageGroupMembers.FindAsync(id);
        }

        public async Task<bool> DeleteMessageGroupMemberById(int id)
        {
            _context.messageGroupMembers.Remove(_context.messageGroupMembers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region MessageBoxInfo
        public async Task<int> SaveMessageBoxInfo(MessageBoxInfo doc)
        {
            if (doc.Id != 0)
                _context.messageBoxes.Update(doc);
            else
                _context.messageBoxes.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MessageBoxInfo>> GetAllMessageBoxInfo()
        {
            return await _context.messageBoxes.Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MessageBoxInfo>> GetAllMessageBoxInfoBygrpId(int Id)
        {
            return await _context.messageBoxes.Where(x => x.messageGroupId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SingleMessageConversation>> GetAllMessageBoxInfoBysend(int sender,int receiver)
        {
            List<SingleMessageConversation> SingleMessageConversation = new List<SingleMessageConversation>();

            var message = await _context.messageBoxes.Where(x => x.employeeId == sender || x.receiverId == sender).Where(x => x.employeeId == receiver || x.receiverId == receiver).Include(x=>x.employee).Include(x => x.receiver).Include(x => x.messageBox.employee).AsNoTracking().ToListAsync();

            foreach (var data in message)
            {
                string emphoto = await _context.photographs.Where(x => x.employeeId == receiver).Select(x => x.url).FirstOrDefaultAsync();
                if (emphoto == null)
                {
                    emphoto = "images/user-icon-png.png";
                }

                string riceverhoto = await _context.photographs.Where(x => x.employeeId == data.receiverId).Select(x => x.url).FirstOrDefaultAsync();
                if (riceverhoto == null)
                {
                    riceverhoto = "images/user-icon-png.png";
                }

                SingleMessageConversation.Add(new SingleMessageConversation
                {
                    senderId = (int)data.employeeId,
                    senderName = data.employee.nameEnglish,
                    senderPhoto = emphoto,
                    messageId = data.Id,

                    receiverId = (int)data.receiverId,
                    reacevierName = data.receiver.nameEnglish,
                    receiverPhoto = riceverhoto,

                    message = data.message,
                    date = data.date,

                    replyMassage = data?.messageBox?.message,
                    replyMassageContact = data?.messageBox?.employee?.nameEnglish,

                    messageBoxFiles = await _context.messageBoxFiles.Where(x=>x.messageBoxId==data.Id).ToListAsync()
                });

            }

            return SingleMessageConversation;
        }

        public async Task<IEnumerable<SingleMessageConversation>> GetAllMessageBoxInfoBygroup(int id)
        {
            List<SingleMessageConversation> SingleMessageConversation = new List<SingleMessageConversation>();

            var message = await _context.messageBoxes.Where(x => x.messageGroupId == id).Include(x => x.employee).Include(x => x.messageBox.employee).AsNoTracking().ToListAsync();

            foreach (var data in message)
            {
                string emphoto = await _context.photographs.Where(x => x.employeeId == data.employeeId).Select(x => x.url).FirstOrDefaultAsync();
                if (emphoto == null)
                {
                    emphoto = "images/user-icon-png.png";
                }

                SingleMessageConversation.Add(new SingleMessageConversation
                {
                    messageId = data.Id,
                    senderId = (int)data.employeeId,
                    senderName = data.employee.nameEnglish,
                    senderPhoto = emphoto,
                    message = data.message,
                    date = data.date,

                    replyMassage = data?.messageBox?.message,
                    replyMassageContact = data?.messageBox?.employee?.nameEnglish,

                    messageBoxFiles = await _context.messageBoxFiles.Where(x => x.messageBoxId == data.Id).ToListAsync()
                });

            }

            return SingleMessageConversation;
        }


        public async Task<MessageBoxInfo> GetMessageBoxInfoById(int id)
        {
            return await _context.messageBoxes.FindAsync(id);
        }

        public async Task<bool> DeleteMessageBoxInfoById(int id)
        {
            _context.messageBoxes.Remove(_context.messageBoxes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region MessageBoxFile
        public async Task<int> SaveMessageBoxFile(MessageBoxFile doc)
        {
            if (doc.Id != 0)
                _context.messageBoxFiles.Update(doc);
            else
                _context.messageBoxFiles.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<MessageBoxFile>> GetAllMessageBoxFile()
        {
            return await _context.messageBoxFiles.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MessageBoxFile>> GetAllMessageBoxFileByMessageId(int Id)
        {
            return await _context.messageBoxFiles.Where(x => x.messageBoxId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<MessageBoxFile> GetMessageBoxFileById(int id)
        {
            return await _context.messageBoxFiles.FindAsync(id);
        }

        public async Task<ContactListViewModel> GetMessageEmployeeInfoById(int id)
        {
            string emphoto = await _context.photographs.Where(x => x.employeeId == id).Select(x => x.url).FirstOrDefaultAsync();
            if (emphoto == null)
            {
                emphoto = "images/user-icon-png.png";
            }
            var emp = await _context.employeeInfos.FindAsync(id);
            ContactListViewModel contactListViewModel = new ContactListViewModel
            {
                Id = emp.Id,
                code = emp.employeeCode,
                name = emp.nameEnglish,
                empPhoto = emphoto,
                employeeInfo =emp,
            };
            return contactListViewModel;
        }

        public async Task<bool> DeleteMessageBoxFileById(int id)
        {
            _context.messageBoxFiles.Remove(_context.messageBoxFiles.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion



    }
}
