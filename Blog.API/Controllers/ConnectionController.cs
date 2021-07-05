using Blog.Core.Abstraction;
using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        private readonly string id;
        private readonly IHttpContextAccessor _httpcontext;

        public ConnectionController(IHttpContextAccessor httpcontext, IServiceProvider serviceProvider)
        {
            _connectionService = serviceProvider.GetRequiredService<IConnectionService>();
            _httpcontext = httpcontext;
            id = _httpcontext.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        }

        [HttpPost]
       
        public async Task<Response<string>> Send([FromQuery] string userid)
        {
            var res = await _connectionService.SendConnection(userid, id);
            return res;
        }

        
        [HttpGet]
        public async Task<Response<IEnumerable<ConnectionDto>>> Get()
        {
            var res = await _connectionService.GetConnections(id);
            return res;
        }

        [HttpPost]
        [Route("approve")]

        public async Task<Response<string>> Approve([FromQuery] string connectionid)
        {
            var res = await _connectionService.ApproveConnection(connectionid, id);
            return res;
        }

        [HttpPost]
        [Route("reject")]

        public async Task<Response<string>> Reject([FromQuery] string connectionid)
        {
            var res = await _connectionService.RejectConnection(connectionid, id);
            return res;
        }
    }
}
