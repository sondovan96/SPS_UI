using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SPS.UI.Data.Core
{
    public interface IResponse <out TData>
    {
        public HttpStatusCode httpStatusCode { get; set; }  
    }
    public class Response<TData> : IResponse<TData>
    {
        public HttpStatusCode httpStatusCode { get; set;}
        public string MessageId { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
        public IList<ErrorDetail> Errors { get; set; }
    }
}
