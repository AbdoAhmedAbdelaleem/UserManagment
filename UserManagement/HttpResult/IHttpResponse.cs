using System.Net;

namespace UserManagement.HttpResult
{
    public interface IHttpResponse<T>
    {
        public HttpStatusCode StrtausCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
