using Kong.Common;
using Kong.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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

        public string CreateUri(string path)
        {
            var uri = new Uri(kongOptions.Host + path);

            return uri.AbsoluteUri;
        }

        public async Task<T> RequestGet<T>(string path)
        {
            T obj = default(T);
            var uri = CreateUri(path);
            var response = await kongOptions.HttpClient.GetAsync(uri);

            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                obj = JsonConvert.DeserializeObject<T>(result, Utils.CreateJsonSetting());
            }
            else
            {
                throw new HttpRequestException(response.StatusCode.ToString());
            }

            return obj;
        }

        public async Task<bool> RequestDelete(string path)
        {
            var uri = CreateUri(path);
            var response = await kongOptions.HttpClient.DeleteAsync(uri);

            var result = await response.Content.ReadAsStringAsync();
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
            T obj = default(T);
            var uri = CreateUri(path);

            HttpResponseMessage response = null;
            StringContent content = null;

            if (data != null)
            {
                var jsr = JsonSerializer.Create(Utils.CreateJsonSetting());
                using (StringWriter sw = new StringWriter())
                {
                    jsr.Serialize(sw, data);
                    sw.Flush();
                    var json = sw.ToString();
                    content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);
                }
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
                obj = JsonConvert.DeserializeObject<T>(result, Utils.CreateJsonSetting());
            }
            else
            {
                throw new HttpRequestException(result);
            }

            return obj;
        }

        public async Task<HttpResponseMessage> RequestPost(string path, object data = null)
        {
            var uri = CreateUri(path);
            StringContent content = null;

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data, Utils.CreateJsonSetting());
                content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);
            }

            HttpResponseMessage response = await kongOptions.HttpClient.PostAsync(uri, content);
            return response;
        }
    }
}
