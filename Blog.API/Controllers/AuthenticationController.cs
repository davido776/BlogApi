using Blog.Core.Abstraction;
using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IServiceProvider serviceProvider)
        {
            _authService = serviceProvider.GetRequiredService<IAuthenticationService>();
        }

        [HttpPost]
        [Route("register")]
        public async Task<Response<RegisterResDto>> Register(RegisterReqDto model)
        {
            var res = await _authService.Register(model);
            return res;
        }

        [HttpPost]
        [Route("login")]
        public async Task<Response<LoginResDto>> Login(LoginReqDto model)
        {
            var res = await _authService.Login(model);
            return res;
        }
    }
}
