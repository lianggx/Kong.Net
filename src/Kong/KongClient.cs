using Kong.AdminAPI;
using Kong.Models;

namespace Kong
{
    public class KongClient
    {
        private readonly KongClientOptions options;
        public KongClient(KongClientOptions options)
        {
            this.options = options;
        }

        public CertificateApi Certificate => new CertificateApi(options);
        public ConsumerApi Consumer => new ConsumerApi(options);
        public NodeApi Node => new NodeApi(options);
        public PluginApi Plugin => new PluginApi(options);
        public ServiceApi Service => new ServiceApi(options);
        public RouteApi Route => new RouteApi(options);
        public SNIApi SNI => new SNIApi(options);
        public TagsApi Tags => new TagsApi(options);
        public UpStreamApi UpStream => new UpStreamApi(options);
        public TargetApi Target => new TargetApi(options);
    }
}
