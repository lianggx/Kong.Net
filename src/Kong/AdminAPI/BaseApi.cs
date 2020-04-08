using Kong.Common;
using Kong.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public abstract class BaseApi
    {
        protected readonly KongClientOptions kongOptions;
        protected const string MEDIA_TYPE = "application/json";
        protected BaseApi(KongClientOptions kongOptions)
        {
            this.kongOptions = kongOptions;
        }

        public Uri CreateUri(string path)
        {
            return new Uri(kongOptions.Host + path);
        }

        public async Task<T> RequestGet<T>(string path)
        {
            var uri = CreateUri(path);
            var response = await kongOptions.HttpClient.GetAsync(uri); ;
            var result = await response.Content.ReadAsStringAsync(); ;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<T>(result, Utils.CreateJsonSetting());
            }
            else
            {
                throw new HttpRequestException(response.StatusCode.ToString());
            }
        }

        public async Task<bool> RequestDelete(string path)
        {
            var uri = CreateUri(path);
            var response = await kongOptions.HttpClient.DeleteAsync(uri); ;
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException(response.StatusCode.ToString());
            }
        }

        public async Task<T> RequestAPI<T>(RequestMethod method, string path, object data = null)
        {
            var uri = CreateUri(path);
            HttpResponseMessage response;
            StringContent content = null;

            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, Utils.CreateJsonSetting());
                content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);
            }

            if (method == RequestMethod.Post)
            {
                response = await kongOptions.HttpClient.PostAsync(uri, content);
            }
            else if (method == RequestMethod.Put)
            {
                response = await kongOptions.HttpClient.PutAsync(uri, content);
            }
            else
            {
                var _method = new HttpMethod(method.ToString());
                var request = new HttpRequestMessage(_method, uri)
                {
                    Content = content
                };
                response = await kongOptions.HttpClient.SendAsync(request);
            }

            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Created ||
                response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<T>(result, Utils.CreateJsonSetting());
            }
            else
            {
                throw new HttpRequestException(result);
            }
        }

        public async Task<HttpResponseMessage> RequestPost(string path, object data = null)
        {
            var uri = CreateUri(path);
            StringContent content = null;

            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, Utils.CreateJsonSetting());
                content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);
            }

            return await kongOptions.HttpClient.PostAsync(uri, content);
        }
    }
}
