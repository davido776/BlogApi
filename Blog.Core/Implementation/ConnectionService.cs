using AutoMapper;
using Blog.Core.Abstraction;
using Blog.Data;
using Blog.Models;
using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Implementation
{
    public class ConnectionService : IConnectionService
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ConnectionService(BlogDbContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Response<string>> SendConnection(string userid, string currentuserid)
        {
            //throw new NotImplementedException();
            var otherUser = await _context.Users.FindAsync(userid);

            var connection = new Connection
            {
                RequestById = currentuserid,
                RequestToId = userid,
                connectionStatus = Connection.ConnectionStatus.Pending
            };
            _context.Connections.Add(connection);
            _context.SaveChanges();

            return  new Response<string>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Connection Sent",
                Success = true,
                Data =  "Connection Sent"
            };

        }



        public async Task<Response<IEnumerable<ConnectionDto>>> GetConnections(string userid)
        {
            if (string.IsNullOrWhiteSpace(userid))
                return new Response<IEnumerable<ConnectionDto>> ()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid Request",
                    Success = false,
                   
                };
            
            
            
            if(await _userManager.FindByIdAsync(userid) == null)
                return new Response<IEnumerable<ConnectionDto>>()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Success = false,

                };


            var connections = _context.Connections
                                    .Include(u => u.RequestBy)
                                    .Where(c => c.RequestToId == userid) as IEnumerable<Connection>;


            var connectionsToReturn = _mapper.Map<IEnumerable<ConnectionDto>>(connections);
                                    

            return new Response<IEnumerable<ConnectionDto>>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Connections retrived",
                Success = true,
                Data = connectionsToReturn
            };


        }

        public async Task<Response<string>> ApproveConnection(string connectionid, string userid)
        {
           

            var foundConnection = await _context.Connections.FindAsync(connectionid);

            var checkResponse = ChecksBeforeConnectionOperation(connectionid, userid, foundConnection);

            if (checkResponse.Success == false)
                return checkResponse;

            if (foundConnection.connectionStatus == Connection.ConnectionStatus.Approved)
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Message = "Connection already approved!",

                };



            foundConnection.connectionStatus = Connection.ConnectionStatus.Approved;

            _context.Connections.Update(foundConnection);
            await _context.SaveChangesAsync();

            return new Response<string>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Connection approved",
                Success = true,
            };
        }

        public async Task<Response<string>> RejectConnection(string connectionid, string userid)
        {


            var foundConnection = await _context.Connections.FindAsync(connectionid);

            var checkResponse = ChecksBeforeConnectionOperation(connectionid, userid, foundConnection);

            if (checkResponse.Success == false)
                return checkResponse;

            if (foundConnection.connectionStatus == Connection.ConnectionStatus.Rejected)
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Message = "Connection already rejected!",

                };



            foundConnection.connectionStatus = Connection.ConnectionStatus.Rejected;

            _context.Connections.Update(foundConnection);
            await _context.SaveChangesAsync();

            return new Response<string>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Connection rejected!",
                Success = true,
            };
        }

        private Response<string> ChecksBeforeConnectionOperation(string connectionid, string userid, Connection foundConnection)
        {
            if (string.IsNullOrWhiteSpace(connectionid))
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid Request",
                    Success = false,

                };

            if (foundConnection == null)
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Connection not found",
                    Success = false,
                };

            if (foundConnection.RequestToId != userid)
                return new Response<string>()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Invalid Request",
                    Success = false,
                };

           


            return new Response<string>()
            {
                Success = true,
            };

        }
    }
}
