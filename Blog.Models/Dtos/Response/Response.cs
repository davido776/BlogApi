using Newtonsoft.Json;

namespace Blog.Models.Dtos.Response
{
    public class Response<T>
    {
        public Response(int statusCode, string message, T details = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = details;
        }
        public Response()
        {

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }



        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
