using AutoMapper;
using Domain.Models;

namespace DAL.Infrastructure.Automapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonProfile, Person>()
                .ReverseMap();
        }
    }
}