using AutoMapper;
using BLL.Infrastructure.Models;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Infrastructure.Automapper
{
    public class WebApiProfile : Profile
    {
        public WebApiProfile()
        {
            CreateMap<NewComment, NewCommentModel>();
        }
    }
}
