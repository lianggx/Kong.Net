using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Kong.Models
{
    public class NodeInfo
    {
        public Guid? Node_id { get; set; }
        public string Version { get; set; }
        public string Lua_version { get; set; }
        public NodePlugin Plugins { get; set; }
        public string Tagline { get; set; }
        public JToken Configuration { get; set; }
        public JToken Prng_seeds { get; set; }

        public NodeTimers Timers { get; set; }
        public class NodeTimers
        {
            public int Pending { get; set; }
            public int Running { get; set; }
        }
        public string HostName { get; set; }
    }

    public class NodePlugin
    {
        public string[] Enabled_in_cluster { get; set; }
        public NodePluginAvailable Available_on_server { get; set; }
    }

    public class NodePluginAvailable
    {
        [JsonProperty("correlation-id")]
        public bool CorrelationId { get; set; }
        [JsonProperty("pre-function")]
        public bool PreFunction { get; set; }
        public bool Cors { get; set; }
        [JsonProperty("ldap-auth")]
        public bool LdapAuth { get; set; }
        public bool Loggly { get; set; }
        [JsonProperty("hmac-auth")]
        public bool HmacAuth { get; set; }
        public bool Zipkin { get; set; }
        [JsonProperty("request-size-limiting")]
        public bool RequestSizeLimiting { get; set; }
        [JsonProperty("azure-functions")]
        public bool AzureFunctions { get; set; }
        [JsonProperty("request-transformer")]
        public bool RequestTansformer { get; set; }
        public bool Oauth2 { get; set; }
        [JsonProperty("response-transformer")]
        public bool ResponseTransformer { get; set; }
        [JsonProperty("ip-restriction")]
        public bool IpRestriction { get; set; }
        public bool Statsd { get; set; }
        public bool Jwt { get; set; }
        [JsonProperty("proxy-cache")]
        public bool ProxyCache { get; set; }
        [JsonProperty("basic-auth")]
        public bool BasicAuth { get; set; }
        [JsonProperty("key-auth")]
        public bool KeyAuth { get; set; }
        [JsonProperty("http-log")]
        public bool HttpLog { get; set; }
        [JsonProperty("datadog")]
        public bool Datadog { get; set; }
        [JsonProperty("tcp-log")]
        public bool TcpLog { get; set; }
        [JsonProperty("post-function")]
        public bool PostFunction { get; set; }
        public bool Prometheus { get; set; }
        public bool Acl { get; set; }
        [JsonProperty("kubernetes-sidecar-injector")]
        public bool KubernetesSidecarInjector { get; set; }
        public bool SysLog { get; set; }
        [JsonProperty("file-log")]
        public bool FileLog { get; set; }
        [JsonProperty("udp-log")]
        public bool UdpLog { get; set; }
        [JsonProperty("response-ratelimiting")]
        public bool ResponseRatelimiting { get; set; }
        [JsonProperty("aws-lambda")]
        public bool AwsLambda { get; set; }
        [JsonProperty("bot-detection")]
        public bool BotDetection { get; set; }
        [JsonProperty("rate-limiting")]
        public bool RateLimiting { get; set; }
        [JsonProperty("request-termination")]
        public bool RequestTermination { get; set; }
    }

    public class NodeStatus
    {
        public NodeDatabase Database { get; set; }
        public NodeMemory Memory { get; set; }
        public NodeServer Server { get; set; }


    }

    /// <summary>
    ///  Metrics about the database.
    /// </summary>
    public class NodeDatabase
    {
        /// <summary>
        /// A boolean value reflecting the state of the database connection. Please note that this flag does not reflect the health of the database itself.
        /// </summary>
        public bool Reachable { get; set; }
    }

    /// <summary>
    /// Metrics about the memory usage.
    /// </summary>
    public class NodeMemory
    {
        /// <summary>
        /// An array with all workers of the Kong node, where each entry contains:
        /// </summary>
        public List<NodeWorkers_lua_vms> Workers_lua_vms { get; set; }
        /// <summary>
        /// An array of information about dictionaries that are shared with all workers in a Kong node, where each array node contains how much memory is dedicated for the specific shared dictionary (capacity) and how much of said memory is in use (allocated_slabs). These shared dictionaries have least recent used (LRU) eviction capabilities, so a full dictionary, where allocated_slabs == capacity, will work properly. However for some dictionaries, e.g. cache HIT/MISS shared dictionaries, increasing their size can be beneficial for the overall performance of a Kong node.
        /// </summary>
        public NodeLua_shared_dicts Lua_shared_dicts { get; set; }
    }

    public class NodeWorkers_lua_vms
    {
        /// <summary>
        /// HTTP submodule’s Lua virtual machine’s memory usage information, as reported by collectgarbage("count"), for every active worker, i.e. a worker that received a proxy call in the last 10 seconds.
        /// </summary>
        public string Http_allocated_gc { get; set; }
        /// <summary>
        /// worker’s process identification number.
        /// </summary>
        public int Pid { get; set; }
    }

    /// <summary>
    /// An array of information about dictionaries that are shared with all workers in a Kong node, where each array node contains how much memory is dedicated for the specific shared dictionary (capacity) and how much of said memory is in use (allocated_slabs). These shared dictionaries have least recent used (LRU) eviction capabilities, so a full dictionary, where allocated_slabs == capacity, will work properly. However for some dictionaries, e.g. cache HIT/MISS shared dictionaries, increasing their size can be beneficial for the overall performance of a Kong node.
    /// </summary>
    public class NodeLua_shared_dicts
    {
        public LuaKongInfo Kong_locks { get; set; }
        public LuaKongInfo Kong { get; set; }
        public LuaKongInfo Kong_db_cache { get; set; }
        public LuaKongInfo Kong_process_events { get; set; }
        public LuaKongInfo Kong_cluster_events { get; set; }
        public LuaKongInfo Kong_db_cache_miss { get; set; }
        public LuaKongInfo Krometheus_metrics { get; set; }
        public LuaKongInfo Kong_healthchecks { get; set; }
        public LuaKongInfo Kong_rate_limiting_counters { get; set; }

        public class LuaKongInfo
        {
            public string Allocated_slabs { get; set; }
            public string Capacity { get; set; }
        }
    }

    /// <summary>
    /// Metrics about the nginx HTTP/S server.
    /// </summary>
    public class NodeServer
    {
        /// <summary>
        /// The total number of client requests.
        /// </summary>
        public long Total_requests { get; set; }
        /// <summary>
        /// The current number of active client connections including Waiting connections.
        /// </summary>
        public long Connections_active { get; set; }
        /// <summary>
        /// The total number of accepted client connections.
        /// </summary>
        public long Connections_accepted { get; set; }
        /// <summary>
        /// The total number of handled connections. Generally, the parameter value is the same as accepts unless some resource limits have been reached.
        /// </summary>
        public long Connections_handled { get; set; }
        /// <summary>
        ///  The current number of connections where Kong is reading the request header.
        /// </summary>
        public long Connections_reading { get; set; }
        /// <summary>
        ///  The current number of connections where nginx is writing the response back to the client.
        /// </summary>
        public long Connections_writing { get; set; }
        /// <summary>
        /// The current number of idle client connections waiting for a request.
        /// </summary>
        public long Connections_waiting { get; set; }
    }
}
