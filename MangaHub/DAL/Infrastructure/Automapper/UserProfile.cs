using AutoMapper;
using DAL.Infrastructure.Models;

namespace DAL.Infrastructure.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserProfileInfo, Domain.Models.UserProfile>()
                .ReverseMap();
        }
    }
}