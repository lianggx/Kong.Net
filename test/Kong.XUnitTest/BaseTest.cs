using Kong.Models;

namespace Kong.XUnitTest
{
    public class BaseTest
    {
        protected KongClient client;
        public BaseTest()
        {
            var options = new KongClientOptions(httpClient: new System.Net.Http.HttpClient(), host: "http://172.16.1.243:8001");
            client = new KongClient(options);
        }
    }
}
