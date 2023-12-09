using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Extensions;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MangasController : ControllerBase
    {
        private readonly IMangaService _mangaService;

        public MangasController(IMangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            var mangas = _mangaService.GetAll();

            return Ok(mangas);
        }

        [HttpGet("get-all-filter")]
        public ActionResult GetAll([FromQuery] MangaFilter filter)
        {
            var mangas = _mangaService.GetAll(filter);

            return Ok(mangas);
        }

        [HttpGet]
        public ActionResult GetById([FromQuery] Guid mangaId)
        {
            var currentUserId = this.GetCurrentUserId();
            var manga = _mangaService.GetById(mangaId, currentUserId);

            return Ok(manga);
        }

        [HttpPost]
        public ActionResult Apply([FromBody] MangaModel mangaModel)
        {
            _mangaService.Apply(mangaModel);

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] Guid mangaId)
        {
            _mangaService.Delete(mangaId);

            return Ok();
        }

        [HttpPost("set-rating")]
        public ActionResult SetRating([FromBody] SetRatingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mangaService.SetRating(model.MangaId, model.UserId, model.Mark);

            return Ok();
        }

        [HttpPost("upload-cover-image")]
        public async Task<ActionResult> UploadCoverImage([FromForm] UploadCoverImageModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fileBytes = await this.GetFileBytes(model.CoverImage);
            _mangaService.UploadCoverImage(fileBytes, model.MangaId);

            return Ok();
        }
    }
}
