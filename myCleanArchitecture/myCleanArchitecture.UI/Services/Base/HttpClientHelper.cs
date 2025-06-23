using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.Results;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace myCleanArchitecture.UI.Services.Base
{
    public interface IHttpClientHelper
    {
        Task<T> GetAsync<T>(string endpoint, string args = null);
        Task<T> PostAsync<T>(string endpoint, object data, string args = null);
        Task<T> PutAsync<T>(string endpoint, object data, string args = null);
        Task<T> DeleteAsync<T>(string endpoint, string args = null);
        Task<T> Login<T>(string endpoint, object data, string args = null);
        Task<T> PostFileAsync<T>(string endpoint, object data, List<IFormFile> formFiles, string args = null);
        Task<T> PutFileAsync<T>(string endpoint, object data, List<IFormFile> formFiles, string args = null);
    }
    public class HttpClientHelper : IHttpClientHelper
    {

        private HttpClient _HttpClient;

        public HttpClientHelper( HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, // Optional: ignore nulls in serialization
            MaxDepth = 128 // Default is 64 in newer System.Text.Json, Newtonsoft has different defaults
        };
        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data, JsonSerializerSettings);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        private static MultipartFormDataContent GetPayload(object data, List<IFormFile> formFiles)
        {
            var multiPartContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(data, JsonSerializerSettings);

            multiPartContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), "entity");

            if (formFiles != null)
            {
                foreach (var item in formFiles)
                {
                    if (item != null && item.Length > 0)
                    {
                        var fileStreamContent = new StreamContent(item.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(item.ContentType);
                        multiPartContent.Add(fileStreamContent, "files", item.FileName);
                    }
                }
            }
            return multiPartContent;
        }
        private async Task<T> HandleHttpResponseAsync<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var serverErrorResult = JsonConvert.DeserializeObject<Result>(errorContent, JsonSerializerSettings);
                    throw new Exception(serverErrorResult?.Meta?.Message);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var serverErrorResult = JsonConvert.DeserializeObject<Result>(errorContent, JsonSerializerSettings);
                    throw new BadHttpRequestException(serverErrorResult?.Meta?.Message ?? "Bad Request");
                }

                return JsonConvert.DeserializeObject<T>(errorContent, JsonSerializerSettings);
            }

            var resultContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resultContent, JsonSerializerSettings);
        }
        public async Task<T> GetAsync<T>(string endpoint, string args = null)
        {
            var response = await _HttpClient.GetAsync($"{endpoint}?{args}");
            return await HandleHttpResponseAsync<T>(response);
        }
        public async Task<T> PostAsync<T>(string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            var response = await _HttpClient.PostAsync($"{endpoint}?{args}", payload);
            return await HandleHttpResponseAsync<T>(response);
        }
        public async Task<T> PostFileAsync<T>(string endpoint, object data, List<IFormFile> formFiles, string args = null)
        {
            var payload = GetPayload(data, formFiles);
            var response = await _HttpClient.PostAsync($"{endpoint}?{args}", payload);
            return await HandleHttpResponseAsync<T>(response);
        }
        public async Task<T> Login<T>(string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            var response = await _HttpClient.PostAsync($"{endpoint}?{args}", payload);
            var resultContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(resultContent, JsonSerializerSettings);
        }
        public async Task<T> PutAsync<T>(string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            var response = await _HttpClient.PutAsync($"{endpoint}?{args}", payload);
            return await HandleHttpResponseAsync<T>(response);
        }
        public async Task<T> PutFileAsync<T>(string endpoint, object data, List<IFormFile> formFiles, string args = null)
        {
            var payload = GetPayload(data, formFiles);
            var response = await _HttpClient.PutAsync($"{endpoint}?{args}", payload);
            return await HandleHttpResponseAsync<T>(response);
        }
        public async Task<T> DeleteAsync<T>(string endpoint, string args = null)
        {
            var response = await _HttpClient.DeleteAsync($"{endpoint}?{args}");
            return await HandleHttpResponseAsync<T>(response);
        }
    }
}
