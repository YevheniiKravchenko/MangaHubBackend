using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(
            ICommentService commentService,
            IMapper mapper
            )
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddComment(NewComment newComment)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value ?? throw new ArgumentException("UNABLE_TO_GET_USER_ID"));
            var commentMap = _mapper.Map<NewCommentModel>(newComment);

            commentMap.UserId = userId;

            return Ok(_commentService.Create(commentMap));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_commentService.Delete(id));
        }

        [HttpPost("update")]
        public IActionResult Update(UpdateCommentModel newComment)
        {
            return Ok(_commentService.Update(newComment));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMangaComments([FromQuery]Guid mangaId)
        {
            return Ok(_commentService.GetMangaComments(mangaId));
        }
    }
}
