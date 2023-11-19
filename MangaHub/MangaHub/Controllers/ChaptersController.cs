using BLL.Contracts;
using BLL.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("get-manga-chapters")]
        public ActionResult GetAllMangaChapters(Guid mangaId)
        {
            var chapters = _chapterService.GetAllMangaChapters(mangaId);

            return Ok(chapters);
        }

        [HttpGet]
        public ActionResult GetById(Guid chapterId)
        {
            var chapter = _chapterService.GetById(chapterId);

            return Ok(chapter);
        }

        [HttpDelete]
        public ActionResult Delete(Guid chapterId)
        {
            _chapterService.Delete(chapterId);

            return Ok();
        }

        [HttpPost]
        public ActionResult Apply(ChapterModel chapterModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _chapterService.Apply(chapterModel);

            return Ok();
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadChapter(IFormFile file, Guid chapterId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is Empty");

            var fileBytes = await this.GetFileBytes(file);
            _chapterService.UploadChapter(fileBytes, chapterId);

            return Ok();
        }
    }
}