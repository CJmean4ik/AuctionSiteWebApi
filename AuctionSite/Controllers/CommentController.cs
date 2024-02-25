using AuctionSite.API.DTO;
using AuctionSite.Application.Services;
using AuctionSite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentsService _commentsService;

        public CommentController(CommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost("api/v1/comments")]
        public async Task<IActionResult> EnterComment([FromForm] CreateCommentDto createComment)
        {
            if (User.Identity.IsAuthenticated)
            {
                var buyerName = User.FindFirstValue(ClaimTypes.Name);
                var buyerLastName = User.FindFirstValue(ClaimTypes.Surname);
                var userName = buyerLastName + " " + buyerName;
                createComment.UserName = userName;
            }

            var replyComments = ReplyComments
                                   .Create(createComment.Text, createComment.UserName, 0, createComment.BetId)
                                   .Value;
            var result = await _commentsService.AddCommentAsync(replyComments);

            if (result.IsFailure)           
               return Json(new { status = 500, message = result.Value });


            return Json(new { status = 200, message = result.Value });
        }
    }
}
