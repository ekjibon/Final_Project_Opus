using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Services.Bulk.Interface;
using OPUSERP.CLUB.Services.Channel.Interfaces;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Data.Bulk;
using OPUSERP.Areas.Bulk.Models;

namespace OPUSERP.Areas.Bulk.Controllers
{
    [Area("Bulk")]
    [Authorize]
    public class BulkSMSEmailController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IGroupService groupService;
        private readonly IRlnGroupMemberService rlnGroupMemberService;
        private readonly ISMSMailService iSMSMailService;


        public BulkSMSEmailController(IPersonalInfoService personalInfoService, IGroupService groupService, IRlnGroupMemberService rlnGroupMemberService, ISMSMailService iSMSMailService)
        {
            this.personalInfoService = personalInfoService;
            this.groupService = groupService;
            this.rlnGroupMemberService = rlnGroupMemberService;
            this.iSMSMailService = iSMSMailService;
        }

        // GET: BulkSMSEmail
        public async Task<IActionResult> Index()
        {
            BulkSMSViewModel model = new BulkSMSViewModel
            {
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                memberGroups = await groupService.GetMemberGroup()
            };
            return View(model);
        }

        // GET: BulkSMSEmail/
        public async Task<IActionResult> Group()
        {
            GroupViewModel model = new GroupViewModel
            {
                memberGroups = await groupService.GetMemberGroup()
            };
            return View(model);
        }

        // GET: BulkSMSEmail/Create
        public ActionResult CreateGroup()
        {
            return View();
        }

        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index ([FromForm] BulkSMSViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.SMS == "on")
            {
                foreach (var id in model.ids)
                {
                    OPUSERP.CLUB.Data.Member.MemberInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                    await iSMSMailService.SendSMSAsync(empInfo.mobileNumberPersonal, model.description);
                }

                if(model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        IEnumerable<RlnMemberGroup> grpInfo = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id);
                        if (grpInfo.Count() > 0)
                        {
                            foreach (var data in grpInfo)
                            {
                                OPUSERP.CLUB.Data.Member.MemberInfo empInfo = await personalInfoService.GetEmployeeInfoById((int)data.employeeId);
                                await iSMSMailService.SendSMSAsync(empInfo.mobileNumberPersonal, model.description);
                            }
                        }
                    }
                }
                    
            }

            if(model.Email == "on")
            {
                foreach (var id in model.ids)
                {
                    OPUSERP.CLUB.Data.Member.MemberInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                    iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                }

                if (model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        IEnumerable<RlnMemberGroup> grpInfo = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id);
                        if (grpInfo.Count() > 0)
                        {
                            foreach (var data in grpInfo)
                            {
                                OPUSERP.CLUB.Data.Member.MemberInfo empInfo = await personalInfoService.GetEmployeeInfoById((int)data.employeeId);
                                iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                            }
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Index));

        }
        
        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Group([FromForm] GroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            MemberGroup data = new MemberGroup
            {
                Id = model.groupId,
                name = model.name,
            };
            await groupService.SaveMemberGroup(data);

            return RedirectToAction(nameof(Group));
        }

        // GET: BulkSMSEmail/AddMemberInGroup/5
        public async Task<IActionResult> AddMemberInGroup(int id)
        {
            GroupViewModel model = new GroupViewModel
            {
                groupId = id,
                employeeInfos = await rlnGroupMemberService.GetEmployeeInfoByGroupId(id),
                rlnMemberGroups = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id)
            };
            return View(model);
        }

        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemberInGroup([FromForm] GroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach(var id in model.ids)
            {
                RlnMemberGroup data = new RlnMemberGroup
                {
                    memberGroupId = model.groupId,
                    employeeId = id,
                };
                await rlnGroupMemberService.SaveRlnMemberGroup(data);
            }
            //return RedirectToAction(nameof(Group));
            return RedirectToAction("AddMemberInGroup", "BulkSMSEmail", new
            {
                id = model.groupId
            });
        }

        // GET: BulkSMSEmail/
        public async Task<IActionResult> GroupDetails (int id)
        {
            GroupViewModel model = new GroupViewModel
            {
                rlnMemberGroups = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id)
            };
            return View(model);
        }

        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupDetails([FromForm] GroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.SMS == "on")
            {
                foreach (var id in model.ids)
                {
                    OPUSERP.CLUB.Data.Member.MemberInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                    await iSMSMailService.SendSMSAsync(empInfo.mobileNumberPersonal, model.description);
                }
            }

            if (model.Email == "on")
            {
                foreach (var id in model.ids)
                {
                    OPUSERP.CLUB.Data.Member.MemberInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                    iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                }
            }

            return RedirectToAction(nameof(GroupDetails));
        }

        // GET: BulkSMSEmail/
        public async Task<IActionResult> DeleteMemberfromGroup(int id)
        {
            GroupViewModel model = new GroupViewModel
            {
                rlnMemberGroup = await rlnGroupMemberService.GetRlnMemberGroupById(id)
            };
            //return Json(model.rlnMemberGroup);
            int? groupId = model.rlnMemberGroup.memberGroupId;

            await rlnGroupMemberService.DeleteRlnMemberGroupById(id);

            return RedirectToAction("AddMemberInGroup", "BulkSMSEmail", new
            {
                id = groupId
            });
        }
    }
}