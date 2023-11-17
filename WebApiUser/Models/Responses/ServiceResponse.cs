using System.Net;

namespace WebApiUser.Models.Responses
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
    }
}
