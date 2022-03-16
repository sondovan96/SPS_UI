using SPS.UI.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPS.UI.Service.Extensions
{
    public interface IHttpRequestExtension
    {
        public Task<T> GetRequestAsync<T>(string url, List<QueryPamaramsModel> queries = default);
        public Task<T> PostJsonRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = default);
        public Task<T> PostFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = default);
        public Task<T> PutJsonRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = default);
        public Task<T> PutFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = default);
        public Task<T> DeleteFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = default);
        public Task<T> DeleteJsonFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = default);
    }
}
