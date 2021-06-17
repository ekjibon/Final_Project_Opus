using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Data.Forum;
using OPUSERP.CLUB.Services.Forum.Interface;
using OPUSERP.Helpers.Errors;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ForumController : ControllerBase
    {
        private readonly ITopicService topicService;
        private readonly ICommentService commentService;

        public ForumController(ITopicService topicService, ICommentService commentService)
        {
            this.topicService = topicService;
            this.commentService = commentService;
        }

        // GET: api/Topic
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await topicService.GetTopics());
        }

        // POST: api/Topic
        [HttpPost]
        public async Task<IActionResult> post([FromBody] Topic model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await topicService.SaveTopic(model);

            if (!result)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Topic Created." });
        }

        // GET: api/Topic
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await topicService.GetTopicById(id));
        }
        
        // GET: api/Topic
        [HttpGet("TopicComments/{id}")]
        public async Task<IActionResult> GetTopicComments(int id)
        {
            return new OkObjectResult(await commentService.GetCommentsByTopic(id));
        }

        // POST: api/Topic
        [HttpPost("TopicComments")]
        public async Task<IActionResult> TopicComments ([FromBody] MemberComment model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await commentService.SaveComment(model);

            if (!result)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Your Comment is successfully send." });
        }


    }
}