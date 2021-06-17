using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{

    [Authorize]
    [Area("HRPMSMasterData")]
    public class EmployeeExitInterviewController : Controller
    {
        private readonly IEmployeeExitInterviewService  employeeExitInterviewService;

        public EmployeeExitInterviewController(IHostingEnvironment hostingEnvironment, IEmployeeExitInterviewService  employeeExitInterviewService)
        {
            this.employeeExitInterviewService = employeeExitInterviewService;
        }


        #region ReasonForLeaving

        // GET: ReasonForLeaving
        public async Task<IActionResult> Index()
        {
            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                reasonForLeavings = await employeeExitInterviewService.GetAllReasonForLeaving()
            };
            return View(model);
        }

        // POST: ReasonForLeaving/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] EmployeeExitInterviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.reasonForLeavings = await employeeExitInterviewService.GetAllReasonForLeaving();
                return View(model);
            }

            ReasonForLeaving reasonForLeaving = new ReasonForLeaving
            {
                Id = model.reasonId,
                reasonName = model.reasonName
            };

            await employeeExitInterviewService.SaveReasonForLeaving(reasonForLeaving);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            await employeeExitInterviewService.DeleteReasonForLeavingById(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region CommentORSuggestion

        // GET: CommentORSuggestion
        public async Task<IActionResult> IndexCommentORSuggestion()
        {
            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                commentORSuggestions = await employeeExitInterviewService.GetAllCommentORSuggestion()
            };
            return View(model);
        }

        // POST: CommentORSuggestion/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexCommentORSuggestion([FromForm] EmployeeExitInterviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.commentORSuggestions = await employeeExitInterviewService.GetAllCommentORSuggestion();
                return View(model);
            }

            CommentORSuggestion commentORSuggestion = new CommentORSuggestion
            {
                Id = model.commentORSuggestionId,
                commentORSuggestionName = model.commentORSuggestionName
            };

            await employeeExitInterviewService.SaveCommentORSuggestion(commentORSuggestion);

            return RedirectToAction(nameof(IndexCommentORSuggestion));

        }

        public async Task<IActionResult> DeleteCommentORSuggestion(int id)
        {
            await employeeExitInterviewService.DeleteCommentORSuggestionById(id);
            return RedirectToAction("IndexCommentORSuggestion");
        }
        #endregion   
        
        #region PayAndBenefits

        // GET: PayAndBenefits
        public async Task<IActionResult> IndexPayAndBenefits()
        {
            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                payAndBenefits = await employeeExitInterviewService.GetAllPayAndBenefits()
            };
            return View(model);
        }

        // POST: PayAndBenefits/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexPayAndBenefits([FromForm] EmployeeExitInterviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.payAndBenefits = await employeeExitInterviewService.GetAllPayAndBenefits();
                return View(model);
            }

            PayAndBenefits payAndBenefits = new PayAndBenefits
            {
                Id = model.payAndBenefitsId,
                payAndBenefitsName = model.payAndBenefitsName
            };

            await employeeExitInterviewService.SavePayAndBenefits(payAndBenefits);

            return RedirectToAction(nameof(IndexPayAndBenefits));

        }

        public async Task<IActionResult> DeletePayAndBenefits(int id)
        {
            await employeeExitInterviewService.DeletePayAndBenefitsById(id);
            return RedirectToAction("IndexPayAndBenefits");
        }
        #endregion

        #region FeelAboutTheFollowing

        // GET: FeelAboutTheFollowing
        public async Task<IActionResult> IndexFeelAboutTheFollowing()
        {
            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                feelAboutTheFollowings = await employeeExitInterviewService.GetAllFeelAboutTheFollowing()
            };
            return View(model);
        }

        // POST: FeelAboutTheFollowing/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexFeelAboutTheFollowing([FromForm] EmployeeExitInterviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.feelAboutTheFollowings = await employeeExitInterviewService.GetAllFeelAboutTheFollowing();
                return View(model);
            }

            FeelAboutTheFollowing feelAboutTheFollowing = new FeelAboutTheFollowing
            {
                Id = model.feelAboutTheFollowingId,
                feelAboutTheFollowingName = model.feelAboutTheFollowingName
            };

            await employeeExitInterviewService.SaveFeelAboutTheFollowing(feelAboutTheFollowing);

            return RedirectToAction(nameof(IndexFeelAboutTheFollowing));

        }

        public async Task<IActionResult> DeleteFeelAboutTheFollowing(int id)
        {
            await employeeExitInterviewService.DeleteFeelAboutTheFollowingById(id);
            return RedirectToAction("IndexFeelAboutTheFollowing");
        }
        #endregion

        #region InterviewComment

        // GET: InterviewComment
        public async Task<IActionResult> IndexInterviewComment()
        {
            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                interviewComments = await employeeExitInterviewService.GetAllInterviewComment()
            };
            return View(model);
        }

        // POST: InterviewComment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexInterviewComment([FromForm] EmployeeExitInterviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.interviewComments = await employeeExitInterviewService.GetAllInterviewComment();
                return View(model);
            }

            InterviewComment interviewComment = new InterviewComment
            {
                Id = model.commentTextId,
                commentText = model.commentText
            };

            await employeeExitInterviewService.SaveInterviewComment(interviewComment);

            return RedirectToAction(nameof(IndexInterviewComment));

        }

        public async Task<IActionResult> DeleteInterviewComment(int id)
        {
            await employeeExitInterviewService.DeleteInterviewCommentById(id);
            return RedirectToAction("IndexInterviewComment");
        }
        #endregion
    }
}