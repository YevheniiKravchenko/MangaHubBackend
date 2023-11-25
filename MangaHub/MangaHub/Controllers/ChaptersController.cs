using BLL.Contracts;
using BLL.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Extensions;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("get-manga-chapters")]
        public ActionResult GetAllMangaChapters([FromQuery] Guid mangaId)
        {
            var chapters = _chapterService.GetAllMangaChapters(mangaId);

            return Ok(chapters);
        }

        [HttpGet]
        public ActionResult GetById([FromQuery] Guid chapterId)
        {
            var chapter = _chapterService.GetById(chapterId);

            return Ok(chapter);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] Guid chapterId)
        {
            _chapterService.Delete(chapterId);

            return Ok();
        }

        [HttpPost]
        public ActionResult Apply([FromBody] ChapterModel chapterModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _chapterService.Apply(chapterModel);

            return Ok();
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadChapter([FromForm] UploadChapterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fileBytes = await this.GetFileBytes(model.ChapterFile);
            _chapterService.UploadChapter(fileBytes, model.ChapterId);

            return Ok();
        }
    }
}