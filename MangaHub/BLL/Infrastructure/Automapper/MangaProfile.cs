﻿using AutoMapper;
using BLL.Infrastructure.Models;
using Domain.Enums;
using Domain.Models;

namespace BLL.Infrastructure.Automapper
{
    public class MangaProfile : Profile
    {
        public MangaProfile()
        {
            #region Manga

            CreateMap<Manga, MangaListItem>()
                .ForMember(dst => dst.Genre, opt => opt.MapFrom(src => src.Genre.ToString()))
                .ForMember
                (
                    dst => dst.Rating, 
                    opt => opt.MapFrom(src => src.Ratings.Any() 
                        ? src.Ratings.Average(r => r.Mark) 
                        : 0)
                );

            CreateMap<Manga, MangaModel>()
                .ForMember
                (
                    dst => dst.Genre, 
                    opt => opt.MapFrom(src => (Genre)src.Genre)
                )
                .ForMember
                (
                    dst => dst.Rating, 
                    opt => opt.MapFrom(src => src.Ratings.Any() 
                        ? src.Ratings.Average(r => r.Mark) 
                        : 0)
                )
                .ReverseMap()
                .ForMember
                (
                    dst => dst.Genre, 
                    opt => opt.MapFrom(src => (int)src.Genre)
                );

            #endregion Manga

            #region Chapter

            CreateMap<Chapter, ChapterListItem>();

            CreateMap<Chapter, ChapterModel>()
                .ReverseMap();

            #endregion

            #region Comment

            CreateMap<Comment, CommentModel>()
                .ForMember(cm => cm.Login, config => config.MapFrom(c => c.User.Login))
                .ReverseMap();
            CreateMap<Comment, NewCommentModel>().ReverseMap();
            CreateMap<Comment, UpdateCommentModel>().ReverseMap();

            #endregion
        }
    }
}
