using Blog.Models.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Abstraction
{
    public interface IConnectionService
    {
        Task<Response<string>> SendConnection(string userid, string currentuserid);

        Task<Response<IEnumerable<ConnectionDto>>> GetConnections(string userid);

        Task<Response<string>> ApproveConnection(string connectionid, string userid);

        Task<Response<string>> RejectConnection(string connectionid, string userid);
    }
}
