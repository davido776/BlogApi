using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Abstraction
{
    public interface IPostService
    {
        Response<AddPostResDto> AddPost(AddPostReqDto model, string userid);

        Response<GetPostDto> GetPost(string postid);

        Task<Response<string>> LikePost(string postid, string userid);

        Task<Response<string>> CommentOnPost(string body,string postid, string userid);

        Task<Response<string>> UpdatePost(UpdatePostDto model, string postid, string userid);
    }
}
