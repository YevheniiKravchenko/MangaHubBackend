using AutoMapper;
using BLL.Infrastructure.Models;
using Domain.Models;

namespace BLL.Infrastructure.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserProfileModel>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(src => src.UserProfile.Avatar))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.UserProfile.Description))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.UserProfile.PhoneNumber))
                .ForMember(dst => dst.ShowConfidentialInformation, opt => opt.MapFrom(src => src.UserProfile.ShowConfidentialInformation))
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.UserProfile.BirthDate))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.UserProfile.Email))
                .ReverseMap();
        }
    }
}