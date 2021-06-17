using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Forum.Models;
using OPUSERP.CLUB.Data.Forum;
using OPUSERP.CLUB.Services.Forum.Interface;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Services.Auth.Interfaces;

namespace OPUSERP.Areas.Forum.Controllers
{
    [Authorize]
    [Area("Forum")]
    public class TopicController : Controller
    {
        private readonly ITopicService topicService;
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly ILoggedinUser loggedinUser;

        public TopicController(ITopicService topicService, ICommentService commentService, UserManager<ApplicationUser> userManage, ILoggedinUser loggedinUser)
        {
            this.topicService = topicService;
            this.commentService = commentService;
            _userManage = userManage;
            this.loggedinUser = loggedinUser;
        }

        // GET: Topic
        public async Task<IActionResult> Index()
        {
            ForumViewModel model = new ForumViewModel
            {
                topics = await topicService.GetTopics(),
            };
            return View(model);
        }

        // GET: Topic
        public async Task<IActionResult> Create()
        {
            ForumViewModel model = new ForumViewModel
            {

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ForumViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //return Json(model);
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            Topic data = new Topic
            {
                topic = model.topic,
                details = model.details,
                date = model.date,
                memberId = employeeId,
                //status = "pending"
            };
            await topicService.SaveTopic(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Comments(int id)
        {
            ForumViewModel model = new ForumViewModel
            {
                topicz = await topicService.GetTopicById(id),
                comments = await commentService.GetCommentsByTopic(id),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Comments([FromForm] ForumViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //return Json(model);
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);
            DateTime? currDate = DateTime.Now;

            MemberComment data = new MemberComment
            {
                details = model.details,
                topicId = model.topicId,
                date = currDate,
                memberId = employeeId,
                //status = "pending"
            };
            await commentService.SaveComment(data);

            return RedirectToAction(nameof(Comments), new { @id = model.topicId });
        }

    }
}