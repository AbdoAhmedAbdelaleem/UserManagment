using System.Net;

namespace UserManagement.HttpResult
{
    public class HttpResponse<T> : IHttpResponse<T>
    {
        public HttpStatusCode StrtausCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public HttpResponse(HttpStatusCode httpStatusCode, string msg, T data=default)
        {
            this.StrtausCode = httpStatusCode;
            this.Message = msg;
            this.Data = data;
        }

        public static HttpResponse<T> Ok(T data = default)
        {
            return new HttpResponse<T>(HttpStatusCode.OK, "", data);
        }

        public static HttpResponse<T> BadRequest(string msg)
        {
            return new HttpResponse<T>(HttpStatusCode.BadRequest, msg);
        }
    }
}
