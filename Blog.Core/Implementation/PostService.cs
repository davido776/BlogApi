using AutoMapper;
using Blog.Core.Abstraction;
using Blog.Data;
using Blog.Models;
using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Implementation
{

    
    public class PostService : IPostService
    {
        private IFileUpload _fileUpload;
        private readonly IMapper _mapper;
        private readonly BlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PostService(IServiceProvider serviceProvider,
                            IMapper mapper, 
                            BlogDbContext context,
                            UserManager<AppUser> userManager
                            )
        {
            _fileUpload = serviceProvider.GetRequiredService<IFileUpload>();
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
           
           
        }

        public Response<AddPostResDto> AddPost(AddPostReqDto model, string userid)
        {
            
            var UploadPhotoRes = new UploadPhotoRes();
            if(model.PostPhoto != null)
            {
                UploadPhotoRes = _fileUpload.UploadPhoto(model.PostPhoto);
            }

            var post = _mapper.Map<Post>(model);

            post.ImageUrl = UploadPhotoRes.AvatarUrl;
            post.AppUserId = userid;

            _context.Posts.Add(post);

            _context.SaveChanges();

            /*var postres = new AddPostResDto()
            {
                PostId = post.Id,
                CreatedAt = post.CreatedAt
            };*/

            var res = new Response<AddPostResDto>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Post Added Successfully",
                Success = true,

            };

            return res;


        }

        public Response<GetPostDto> GetPost(string id)
        {
            var post = _context.Posts.Where(p => p.Id == id).Include(p=>p.Likes).FirstOrDefault();
           
            if(post == null)
            {
                return new Response<GetPostDto>()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Post Not Found",
                    Success = false,
                };
            }

            var postToReturn = _mapper.Map<GetPostDto>(post);
            

            var res = new Response<GetPostDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Found Posts",
                Success = true,
                Data = postToReturn
            };

            return res;

        }

        public async Task<Response<string>> UpdatePost(UpdatePostDto model,string postid, string userid)
        {
            var existingPost = _context.Posts.Find(postid);
            if(existingPost == null)
            {
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "post not found",
                    Success = false,
                };
            }
               
            if (existingPost.AppUserId != userid)
            {
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "cannot update post",
                    Success = false,
                };
            }

            existingPost.Title = model.Title;
            existingPost.Body = model.Body;
            existingPost.Subtitle = model.Subtitle;

            _context.Posts.Update(existingPost);
            var save = await _context.SaveChangesAsync();

            if (save < 1)
            {
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "could not update post",
                    Success = false,
                };
            }

            return new Response<string>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Post update Successful",
                Success = true,
                Data = existingPost.Id
            };


        }

        public async Task<Response<string>> LikePost(string postid, string userid)
        {
            if(postid == null)
            {
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post id is null",
                    Success = false,
                };
            }
            var existinglike = _context.Likes.Where(like => like.PostId == postid && like.AppUserId == userid).FirstOrDefault();
            if(existinglike != null)
            {
                return  new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post Already  Liked",
                    Success = false,
                };
            }
            var currentuser = await _userManager.FindByIdAsync(userid);
            var like = new Like
            {
                AppUserId = userid,
                PostId = postid,
                AppUserName = $"{currentuser.FirstName} {currentuser.LastName}"

            };

            _context.Likes.Add(like);
            _context.SaveChanges();

            var res = new Response<string>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Post Liked",
                Success = true,
            };

            return res;
        }


        public async Task<Response<string>> CommentOnPost(string body,string postid, string userid)
        {
            var post = await _context.Posts.FindAsync(postid);
            if (post == null)
            {
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post Not Found",
                    Success = false,
                };
            }

            var newcomment = new Comment
            {
                PostId = postid,
                AppUserId = userid,
                Body = body
            };

            _context.Comments.Add(newcomment);
            var saveRes = _context.SaveChanges();
            if(saveRes < 1)
            {
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Could not comment on Post",
                    Success = false,
                };
            }

            return new Response<string>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Comment on Post successful",
                Success = true,
            };



        }
    }
}
