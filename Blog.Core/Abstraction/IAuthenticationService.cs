using Blog.Models;
using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Response<RegisterResDto>> Register(RegisterReqDto model);

        Task<Response<LoginResDto>> Login(LoginReqDto model);

        Task<string> GenerateToken(AppUser user);
    }
}
