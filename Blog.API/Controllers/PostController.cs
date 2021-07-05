using Blog.Core.Abstraction;
using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly string id;

        public PostController(IServiceProvider serviceProvider,
                                IHttpContextAccessor httpcontext
                                )
        {
            _postService = serviceProvider.GetRequiredService<IPostService>();
            _httpcontext = httpcontext;
            id = _httpcontext.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

        }



        [HttpPost]
        public Response<AddPostResDto> Add([FromForm] AddPostReqDto model)
        {
            
            var response = _postService.AddPost(model, id);
            return response;
            
        }


        [HttpGet("{id}")]
        
        public Response<GetPostDto> Get(string id)
        {
            var response = _postService.GetPost(id);
            return response;
        }

        [HttpPut("{postid}")]
        public async Task<Response<string>> Update([FromForm] UpdatePostDto model, string postid)
        {
            var response = await _postService.UpdatePost(model, postid, id);
            return response;
        }


        [HttpPost]
        [Route("like-post/{postid}")]

        public async Task<Response<string>> Like(string postid)
        {
            var response = await _postService.LikePost(postid, id);
            return response;
        }

        [HttpPost]
        [Route("comment/{postid}")]
        public async Task<Response<string>> Comment([FromBody] string Body,string postid)
        {
            var response = await _postService.CommentOnPost(Body, postid, id);
            return response;
        }


       


    }
}
