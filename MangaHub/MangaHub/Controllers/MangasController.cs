using BLL.Contracts;
using BLL.Infrastructure.Models;
using BLL.Services;
using DAL.Infrastructure.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult GetAll(MangaFilter filter)
        {
            var mangas = _mangaService.GetAll(filter);

            return Ok(mangas);
        }

        [HttpGet]
        public ActionResult GetById(Guid mangaId)
        {
            var manga = _mangaService.GetById(mangaId);

            return Ok(manga);
        }

        [HttpPost]
        public ActionResult Apply(MangaModel mangaModel)
        {
            _mangaService.Apply(mangaModel);

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(Guid mangaId)
        {
            _mangaService.Delete(mangaId);

            return Ok();
        }

        [HttpPost("set-rating")]
        public ActionResult SetRating(Guid mangaId, int userId, int mark)
        {
            _mangaService.SetRating(mangaId, userId, mark);

            return Ok();
        }

        [HttpPost("upload-cover-image")]
        public async Task<ActionResult> UploadCoverImage(IFormFile file, Guid mangaId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is Empty");

            var fileBytes = await this.GetFileBytes(file);
            _mangaService.UploadCoverImage(fileBytes, mangaId);

            return Ok();
        }

    }
}
