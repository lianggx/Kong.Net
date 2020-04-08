using System.Net.Http;

namespace Kong.Models
{
    public class KongClientOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="host">Kong server,{host:port}</param>
        public KongClientOptions(HttpClient httpClient, string host)
        {
            this.HttpClient = httpClient;
            this.Host = host;
        }
        public HttpClient HttpClient { get; set; }
        /// <summary>
        ///  Kong server,{host:port}
        /// </summary>
        public string Host { get; set; }
    }
}
