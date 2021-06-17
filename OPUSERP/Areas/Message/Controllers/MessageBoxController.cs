using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Message.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.MessageBox.Data;
using OPUSERP.MessageBox.Service.Interface;

namespace OPUSERP.Areas.Message.Controllers
{
    [Authorize]
    [Area("Message")]
    public class MessageBoxController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IMessageService messageService;
        private readonly IUserInfoes userInfoes;


        public MessageBoxController(IPersonalInfoService personalInfoService, IUserInfoes userInfoes, IMessageService messageService)
        {
            this.personalInfoService = personalInfoService;
            this.messageService = messageService;
            this.userInfoes = userInfoes;
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MessageBoxViewModel model = new MessageBoxViewModel
            {
                contactListViewModels = await messageService.GetEmployeeInfoExceptMe(empId),
                grpListViewModels = await messageService.GetGroupsForMe(empId),
            };

            return View(model);
        }

        public async Task<IActionResult> ChatRoomSingle(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MessageGroupViewModel model = new MessageGroupViewModel
            {
                employee = await personalInfoService.GetEmployeeInfoById(id),
                employeeId = empId,
                contactListViewModels = await messageService.GetEmployeeInfoExceptMe(empId),
                grpListViewModels = await messageService.GetGroupsForMe(empId),
                messageBoxInfos = await messageService.GetAllMessageBoxInfoBysend(empId, id)
            };
            return View(model);
        }

        public async Task<IActionResult> ChatRoomGroup(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MessageGroupViewModel model = new MessageGroupViewModel
            {
                messageGroup = await messageService.GetMessageGroupById(id),
                employeeId = empId,
                contactListViewModels = await messageService.GetEmployeeInfoExceptMe(empId),
                grpListViewModels = await messageService.GetGroupsForMe(empId),
                messageBoxInfos = await messageService.GetAllMessageBoxInfoBygroup(id)
            };
            return View(model);
        }


        public async Task<IActionResult> Group()
        {
            MessageGroupViewModel model = new MessageGroupViewModel
            {
                messageGroups = await messageService.GetAllMessageGroup()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Group([FromForm] MessageGroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            MessageGroup data = new MessageGroup
            {
                Id = model.Id,
                name = model.name,
                tagline = model.tagline
            };
            await messageService.SaveMessageGroup(data);

            return RedirectToAction(nameof(Group));
        }

        public async Task<IActionResult> AddMemberInGroup(int id)
        {
            MessageGroupViewModel model = new MessageGroupViewModel
            {
                Id = id,
                messageGroup = await messageService.GetMessageGroupById(id),
                employees = await messageService.GetEmployeeInfoByGroupId(id),
                messageGroupMembers = await messageService.GetAllMessageGroupMemberBygrpId(id)
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteMemberfromGroup(int id)
        {
            MessageGroupViewModel model = new MessageGroupViewModel
            {
                messageGroupMember = await messageService.GetMessageGroupMemberById(id)
            };
            //return Json(model.rlnMemberGroup);
            int? groupId = model.messageGroupMember.messageGroupId;

            await messageService.DeleteMessageGroupMemberById(id);

            return RedirectToAction("AddMemberInGroup", "MessageBox", new
            {
                id = groupId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemberInGroup([FromForm] MessageGroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach (var id in model.ids)
            {
                MessageGroupMember data = new MessageGroupMember
                {
                    messageGroupId = model.Id,
                    employeeId = id,
                };
                await messageService.SaveMessageGroupMember(data);
            }
            return RedirectToAction("AddMemberInGroup", "MessageBox", new
            {
                id = model.Id
            });
        }

        [HttpPost]
        public async Task<ActionResult> SendMassage([FromForm] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            MessageBoxInfo data = new MessageBoxInfo
            {
                Id = 0,
                employeeId = model.sender,
                receiverId = model.receiver,
                messageGroupId = model.groupId,
                message = model.message,
                date = DateTime.Now
            };

            int lstId = await messageService.SaveMessageBoxInfo(data);

            return Json(lstId);
        }


        [HttpPost]
        public async Task<ActionResult> ForwordMassage([FromForm] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var message = await messageService.GetMessageBoxInfoById((int)model.messageId);

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            if (model.groups != null)
            {
                foreach (var group in model.groups)
                {
                    MessageBoxInfo mgs = new MessageBoxInfo
                    {
                        employeeId = empId,
                        messageGroupId = group,
                        message = message.message,
                        date = DateTime.Now
                    };

                    int lstId = await messageService.SaveMessageBoxInfo(mgs);
                }

            }

            if (model.ids != null)
            {
                foreach (var id in model.ids)
                {
                    MessageBoxInfo mgs1 = new MessageBoxInfo
                    {
                        employeeId = empId,
                        receiverId = id,
                        message = message.message,
                        date = DateTime.Now
                    };

                    int lstId = await messageService.SaveMessageBoxInfo(mgs1);
                }
            }

            if (model.ModalValue == 1)
            {
                return RedirectToAction("ChatRoomGroup", "MessageBox", new
                {
                    id = model.groupId
                });
            }

            return RedirectToAction("ChatRoomSingle", "MessageBox", new
            {
                id = model.groupId
            });


        }



        [HttpPost]
        public async Task<ActionResult> ReplyMassage([FromForm] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            if (model.messageId != null)
            {
                MessageBoxInfo mgs = new MessageBoxInfo
                {
                    employeeId = empId,
                    receiverId = model.receiver,
                    messageGroupId = model.groupId,
                    message = model.reply,
                    messageBoxId = model.messageId,
                    date = DateTime.Now
                };

                int lstId = await messageService.SaveMessageBoxInfo(mgs);

            }

            if (model.ModalValue == 1)
            {
                return RedirectToAction("ChatRoomGroup", "MessageBox", new
                {
                    id = model.groupId
                });
            }

            return RedirectToAction("ChatRoomSingle", "MessageBox", new
            {
                id = model.receiver
            });


        }


        [HttpPost]
        public async Task<ActionResult> AttachmentMassage([FromForm] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //return Json(model);

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            if (model.message != null)
            {
                MessageBoxInfo mgs = new MessageBoxInfo
                {
                    employeeId = empId,
                    receiverId = model.receiver,
                    messageGroupId = model.groupId,
                    message = model.message,
                    date = DateTime.Now
                };

                int lstId = await messageService.SaveMessageBoxInfo(mgs);

                if (model.attachmentFileAll != null)
                {
                    for (int i = 0; i < model.attachmentFileAll.Length; i++)
                    {
                        string fileName;
                        string fileNameOrg = String.Empty;
                        string message = FileSave.SaveEmpAttachment(out fileName, model.attachmentFileAll[i]);

                        if (message == "success")
                        {
                            fileNameOrg = fileName;
                        }

                        var file = await messageService.GetAllMessageBoxFile();
                        int count = 1;
                        if (file != null)
                        {
                            count = file.Count() + 1;
                        }
                        else
                        {
                            count = 1;
                        }


                        MessageBoxFile Attachment = new MessageBoxFile
                        {
                            messageBoxId = lstId,
                            name = "File-00" + count.ToString(),
                            fileUrl = fileNameOrg
                        };
                        await messageService.SaveMessageBoxFile(Attachment);
                    }
                }

            }

            if (model.ModalValue == 1)
            {
                return RedirectToAction("ChatRoomGroup", "MessageBox", new
                {
                    id = model.groupId
                });
            }

            return RedirectToAction("ChatRoomSingle", "MessageBox", new
            {
                id = model.receiver
            });


        }

        #region Mobile Aps APIs

        [Route("api/Message/GetEmployeeInfoExceptMe/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeeInfoExceptMe(int Id)
        {
            var employee = await userInfoes.GetUserInfoList();
            return Json(employee.Where(x=>x.EmployeeId!=Id).ToList());
        }

        [Route("api/Message/GetGroupsForMe/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGroupsForMe(int Id)
        {
            return Json(await messageService.GetGroupsForMe(Id));
        }

        [Route("api/Message/GetAllMessageGroupMemberBygrpId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMessageGroupMemberBygrpId(int Id)
        {
            return Json(await messageService.GetAllMessageGroupMemberBygrpId(Id));
        }

        [Route("api/Message/GetAllEmployeesNotInGroupBygrpId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEmployeesNotInGroupBygrpId(int Id)
        {
            return Json(await messageService.GetEmployeeInfoByGroupId(Id));
        }

        [Route("api/Message/GetAllMessageGroup")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMessageGroup(int Id)
        {
            return Json(await messageService.GetAllMessageGroup());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GroupCreate([FromBody] MessageGroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            MessageGroup data = new MessageGroup
            {
                Id = model.Id,
                name = model.name,
                tagline = model.tagline
            };
            int id = await messageService.SaveMessageGroup(data);

            return Json(id);
        }

        [Route("api/Message/DeleteMemberfromGroupApi/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteMemberfromGroupApi(int id)
        {
            await messageService.DeleteMessageGroupMemberById(id);

            return Json("Member Removed Successfully!!");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddMemberInGroupApi([FromBody] MessageGroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach (var id in model.ids)
            {
                MessageGroupMember data = new MessageGroupMember
                {
                    messageGroupId = model.Id,
                    employeeId = id,
                };
                await messageService.SaveMessageGroupMember(data);
            }
            return RedirectToAction("Member Added Successfully!!");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendMassageAPI([FromBody] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            MessageBoxInfo data = new MessageBoxInfo
            {
                employeeId = model.sender,
                receiverId = model.receiver,
                messageGroupId = model.groupId,
                message = model.message,
                date = DateTime.Now
            };

            int lstId = await messageService.SaveMessageBoxInfo(data);

            return Json(lstId);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForwordMassageAPI([FromBody] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var message = await messageService.GetMessageBoxInfoById((int)model.messageId);


            if (model.groups != null)
            {
                foreach (var group in model.groups)
                {
                    MessageBoxInfo mgs = new MessageBoxInfo
                    {
                        employeeId = model.sender,
                        messageGroupId = group,
                        message = message.message,
                        date = DateTime.Now
                    };

                    int lstId = await messageService.SaveMessageBoxInfo(mgs);
                }

            }

            if (model.ids != null)
            {
                foreach (var id in model.ids)
                {
                    MessageBoxInfo mgs1 = new MessageBoxInfo
                    {
                        employeeId = model.sender,
                        receiverId = id,
                        message = message.message,
                        date = DateTime.Now
                    };

                    int lstId = await messageService.SaveMessageBoxInfo(mgs1);
                }
            }

            return RedirectToAction("Forwarded Successfilly!!");

        }


        [HttpPost]
        public async Task<ActionResult> ReplyMassageAPI([FromBody] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.messageId != null)
            {
                MessageBoxInfo mgs = new MessageBoxInfo
                {
                    employeeId = model.sender,
                    receiverId = model.receiver,
                    messageGroupId = model.groupId,
                    message = model.reply,
                    messageBoxId = model.messageId,
                    date = DateTime.Now
                };

                int lstId = await messageService.SaveMessageBoxInfo(mgs);

            }

            return RedirectToAction("Replyed Successfilly!!");
            
        }

        [HttpPost]
        public async Task<ActionResult> AttachmentMassageApi([FromBody] SendMassageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //return Json(model);

            if (model.message != null)
            {
                MessageBoxInfo mgs = new MessageBoxInfo
                {
                    employeeId = model.sender,
                    receiverId = model.receiver,
                    messageGroupId = model.groupId,
                    message = model.message,
                    date = DateTime.Now
                };

                int lstId = await messageService.SaveMessageBoxInfo(mgs);

                if (model.attachmentFileAll != null)
                {
                    for (int i = 0; i < model.attachmentFileAll.Length; i++)
                    {
                        string fileName;
                        string fileNameOrg = String.Empty;
                        string message = FileSave.SaveEmpAttachment(out fileName, model.attachmentFileAll[i]);

                        if (message == "success")
                        {
                            fileNameOrg = fileName;
                        }

                        var file = await messageService.GetAllMessageBoxFile();
                        int count = 1;
                        if (file != null)
                        {
                            count = file.Count() + 1;
                        }
                        else
                        {
                            count = 1;
                        }


                        MessageBoxFile Attachment = new MessageBoxFile
                        {
                            messageBoxId = lstId,
                            name = "File-00" + count.ToString(),
                            fileUrl = fileNameOrg
                        };
                        await messageService.SaveMessageBoxFile(Attachment);
                    }
                }

            }

            return RedirectToAction("Attachment Saved Successfilly!!");

        }

        [Route("api/Message/GetAllMessageBoxInfoBysend/{sender}/{receiver}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMessageBoxInfoBysend(int sender,int receiver)
        {
            return Json(await messageService.GetAllMessageBoxInfoBysend(sender, receiver));
        }

        [Route("api/Message/GetAllMessageBoxInfoBygroup/{group}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMessageBoxInfoBygroup(int group)
        {
            return Json(await messageService.GetAllMessageBoxInfoBygroup(group));
        }

        [Route("api/Message/GetMessageEmployeeInfoById/{Id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMessageEmployeeInfoById(int Id)
        {
            return Json(await messageService.GetMessageEmployeeInfoById(Id));
        }

        #endregion

    }
}