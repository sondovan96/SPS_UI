using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using SPS.UI.Data.Core;
using SPS.UI.Data.Core.Exceptions;
using SPS.UI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SPS.UI.Service.Extensions
{
    public class HttpRequestExtension : IHttpRequestExtension
    {
        private IHttpContextAccessor _httpContextAccessor;
        private RestSharpConfiguration _restSharpConfiguration;
        private ILogger<HttpRequestExtension> _logger;

        public HttpRequestExtension(IHttpContextAccessor httpContextAccessor, IOptions<RestSharpConfiguration> options, ILogger<HttpRequestExtension> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _restSharpConfiguration = options.Value;
            _logger = logger;
        }
        #region Method Private
        private string Token { get => _httpContextAccessor.HttpContext.Session.GetString(Constants.SessionKey.Token); }
        private string TokenScheme { get => _httpContextAccessor.HttpContext.Session.GetString(Constants.SessionKey.TokenScheme); }

        private BodyParamsModel ToBodyParams(object body)
        {
            BodyParamsModel bodyParams = new BodyParamsModel();
            if (body == null)
            {
                return null;
            }
            bodyParams.Params = (IDictionary<string, object>)body.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.PropertyType != typeof(IFormFile))
                .ToDictionary(x => x.Name, x => x.GetValue(body, null));
            return bodyParams;
        }

        private RestRequest CreateRestRequest(string url, List<QueryPamaramsModel> queries)
        {
            RestRequest request = new RestRequest(url, (Method)DataFormat.Json);

            if (!string.IsNullOrEmpty(Token))
            {
                request.AddHeader(Constants.Request.Header.Authorization, $"{TokenScheme} {Token}");
            }
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

            if (queries != null)
            {
                foreach (var query in queries)
                {
                    request.AddQueryParameter(query.Key, query.Value);
                }
            }
            return request;
        }

        private RestRequest CreateJsonRestRequest(string url, object data, List<QueryPamaramsModel> queries)
        {
            RestRequest request = CreateRestRequest(url, queries);

            if (data != null)
            {
                request.AddJsonBody(data);
            }
            return request;
        }

        private RestRequest CreateFormRestRequest(string url, object data, List<QueryPamaramsModel> queries)
        {
            RestRequest request = CreateRestRequest(url, queries);

            if (data == null)
            {
                return request;
            }
            var bodyParams = ToBodyParams(data);
            if (bodyParams == null)
            {
                return request;
            }
            if (bodyParams.Params != null)
            {
                foreach (var param in bodyParams.Params)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return request;
        }
        private void HandleResponse<T>(T response)
        {
            HttpStatusCode statusCode = (HttpStatusCode)response.GetType()
                .GetProperty("HttpStatusCode")
                .GetValue(response);
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
            }
        }
        #endregion

        public async Task<T> DeleteFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateFormRestRequest(url, data, queries);
            T response = default(T);
            try
            {
                response = await new RestClient(_restSharpConfiguration.HostUrl).DeleteAsync<T>(request);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR DELETE {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Delete {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }

        public async Task<T> DeleteJsonFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateJsonRestRequest(url, data, queries);
            T response = default(T);
            try
            {
                response = await new RestClient(_restSharpConfiguration.HostUrl).DeleteAsync<T>(request);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR DELETE {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Delete {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }

        public async Task<T> GetRequestAsync<T>(string url, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateRestRequest(url, queries);
            T response = default(T);
            try
            {
                var responseContent = await new RestClient(_restSharpConfiguration.HostUrl).GetAsync<string>(request);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR GET {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Get {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }

        public async Task<T> PostFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateFormRestRequest(url, data, queries);
            T response = default(T);
            try
            {
                response = await new RestClient(_restSharpConfiguration.HostUrl).PostAsync<T>(request);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR Post {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Post {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }

        public async Task<T> PostJsonRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateJsonRestRequest(url, data, queries);
            T response = default(T);
            try
            {
                var responseContent = await new RestClient(_restSharpConfiguration.HostUrl).PostAsync<string>(request);
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR Post {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Post {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }

        public async Task<T> PutFormRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateFormRestRequest(url, data, queries);
            T response = default(T);
            try
            {
                response = await new RestClient(_restSharpConfiguration.HostUrl).PutAsync<T>(request);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR Put {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Put {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }

        public async Task<T> PutJsonRequestAsync<T>(string url, object data, List<QueryPamaramsModel> queries = null)
        {
            RestRequest request = CreateJsonRestRequest(url, data, queries);
            T response = default(T);
            try
            {
                response = await new RestClient(_restSharpConfiguration.HostUrl).PutAsync<T>(request);
                HandleResponse(response);
            }
            catch (ResponseFailureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ERROR Put {url} : {ex.Message}");
            }
            _logger?.LogInformation($"Put {url} : {JsonSerializer.Serialize(response)}");
            return response;
        }
    }
}
