using AutoMapper;
using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Mapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<RegisterReqDto, AppUser>();
            CreateMap<AddPostReqDto, Post>().ReverseMap();
            CreateMap<Like, GetLikeDto>().ReverseMap();
                 
            CreateMap<Post, GetPostDto>()
                .ForMember(getpostdto=>getpostdto.NumberOfLikes, x=>x.MapFrom(x=>x.Likes.Count))
                .ForMember(getpostdto=>getpostdto.Likes, x=>x.MapFrom(x=>x.Likes))
                .ReverseMap();
            CreateMap<Like, GetLikeDto>().ReverseMap();


            CreateMap<AppUser, GetUserDto>().ReverseMap();

            CreateMap<Connection, ConnectionDto>()
                .ForMember(dto => dto.RequestBy, opt => opt.MapFrom(src => src.RequestBy));
                



        }
    }
}
