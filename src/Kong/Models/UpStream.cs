using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class UpStreamCollection
    {
        public List<UpStream> Data { get; set; }
        public string Next { get; set; }
    }
    public class UpStream
    {
        public Guid? Id { get; set; }
        public DateTime? Created_at { get; set; }
        /// <summary>
        ///  This is a hostname, which must be equal to the host of a Service.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  What to use as hashing input: none (resulting in a weighted-round-robin scheme with no hashing), consumer, ip, header, or cookie. Defaults to "none".
        /// </summary>
        public HashInput Hash_on { get; set; } = HashInput.none;
        /// <summary>
        ///  What to use as hashing input if the primary hash_on does not return a hash (eg. header is missing, or no consumer identified). One of: none, consumer, ip, header, or cookie. Not available if hash_on is set to cookie. Defaults to "none".
        /// </summary>
        public HashInput Hash_fallback { get; set; } = HashInput.none;
        /// <summary>
        ///  The header name to take the value from as hash input. Only required when hash_on is set to header.
        /// </summary>
        public string Hash_on_header { get; set; }
        /// <summary>
        ///  The header name to take the value from as hash input. Only required when hash_fallback is set to header.
        /// </summary>
        public string Hash_fallback_header { get; set; }
        /// <summary>
        ///  The cookie name to take the value from as hash input. Only required when hash_on or hash_fallback is set to cookie. If the specified cookie is not in the request, Kong will generate a value and set the cookie in the response.
        /// </summary>
        public string Hash_on_cookie { get; set; }
        /// <summary>
        ///  The cookie path to set in the response headers. Only required when hash_on or hash_fallback is set to cookie. Defaults to "/".
        /// </summary>
        public string Hash_on_cookie_path { get; set; } = "/";
        /// <summary>
        ///  The number of slots in the loadbalancer algorithm (10-65536). Defaults to 10000.
        /// </summary>
        public int Slots { get; set; } = 10000;

        public HealthChecks HealthChecks { get; set; }
        /// <summary>
        ///  An optional set of strings associated with the Upstream, for grouping and filtering.
        /// </summary>
        public string[] Tags { get; set; }
    }

    public class HealthChecks
    {
        public UpStreamActive Active { get; set; }
        public UpStreamPassive Passive { get; set; }
    }

    public class UpStreamActive
    {
        /// <summary>
        ///  Whether to check the validity of the SSL certificate of the remote host when performing active health checks using HTTPS. Defaults to true.
        /// </summary>
        public bool Https_verify_certificate { get; set; } = true;
        public ActiveUnHealthy UnHealthy { get; set; }


        /// <summary>
        /// Path to use in GET HTTP request to run as a probe on active health checks. Defaults to "/".
        /// </summary>
        public string Http_path { get; set; } = "/";
        /// <summary>
        ///  Socket timeout for active health checks (in seconds). Defaults to 1.
        /// </summary>
        public int Timeout { get; set; } = 1;
        public ActiveHealthy Healthy { get; set; }

        /// <summary>
        ///  The hostname to use as an SNI (Server Name Identification) when performing active health checks using HTTPS. This is particularly useful when Targets are configured using IPs, so that the target host’s certificate can be verified with the proper SNI.
        /// </summary>
        public string Https_sni { get; set; }
        /// <summary>
        /// Number of targets to check concurrently in active health checks. Defaults to 10.
        /// </summary>
        public int Concurrency { get; set; } = 10;
        /// <summary>
        ///  Whether to perform active health checks using HTTP or HTTPS, or just attempt a TCP connection. Possible values are tcp, http or https. Defaults to "http".
        /// </summary>
        public HealthyScheme Type { get; set; } = HealthyScheme.http;
    }

    public class ActiveUnHealthy
    {
        /// <summary>
        ///  An array of HTTP statuses to consider a failure, indicating unhealthiness, when returned by a probe in active health checks. Defaults to [429, 404, 500, 501, 502, 503, 504, 505]. With form-encoded, the notation is http_statuses[]=429&http_statuses[]=404. With JSON, use an Array.
        /// </summary>
        public int[] Http_statuses { get; set; }
        /// <summary>
        ///  Number of TCP failures in active probes to consider a target unhealthy. Defaults to 0.
        /// </summary>
        public int Tcp_failures { get; set; }
        /// <summary>
        ///  Number of HTTP failures in active probes (as defined by healthchecks.active.unhealthy.http_statuses) to consider a target unhealthy. Defaults to 0.
        /// </summary>
        public int Http_failures { get; set; }
        /// <summary>
        ///  Number of timeouts in active probes to consider a target unhealthy. Defaults to 0.
        /// </summary>
        public int Timeouts { get; set; }
        /// <summary>
        ///  Interval between active health checks for unhealthy targets (in seconds). A value of zero indicates that active probes for unhealthy targets should not be performed. Defaults to 0.
        /// </summary>
        public int Interval { get; set; }
    }

    public class ActiveHealthy
    {
        /// <summary>
        ///  An array of HTTP statuses to consider a success, indicating healthiness, when returned by a probe in active health checks. Defaults to [200, 302]. With form-encoded, the notation is http_statuses[]=200&http_statuses[]=302. With JSON, use an Array.
        /// </summary>
        public int[] Http_statuses { get; set; }
        /// <summary>
        ///  Interval between active health checks for healthy targets (in seconds). A value of zero indicates that active probes for healthy targets should not be performed. Defaults to 0.
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        ///  Number of successes in active probes (as defined by healthchecks.active.healthy.http_statuses) to consider a target healthy. Defaults to 0.
        /// </summary>
        public int Successes { get; set; }
    }

    public class UpStreamPassive
    {
        public PassiveUnHealthy UnHealthy { get; set; }

        /// <summary>
        ///  Whether to perform passive health checks interpreting HTTP/HTTPS statuses, or just check for TCP connection success. Possible values are tcp, http or https (in passive checks, http and https options are equivalent.). Defaults to "http".
        /// </summary>
        public HealthyScheme Type { get; set; } = HealthyScheme.http;
        public PassiveHealthy Healthy { get; set; }
    }

    public class PassiveUnHealthy
    {
        /// <summary>
        /// Number of TCP failures in proxied traffic to consider a target unhealthy, as observed by passive health checks. Defaults to 0.
        /// </summary>
        public int Tcp_failures { get; set; }
        /// <summary>
        ///  Number of HTTP failures in proxied traffic (as defined by healthchecks.passive.unhealthy.http_statuses) to consider a target unhealthy, as observed by passive health checks. Defaults to 0.
        /// </summary>
        public int Http_failures { get; set; }
        /// <summary>
        /// An array of HTTP statuses which represent unhealthiness when produced by proxied traffic, as observed by passive health checks. Defaults to [429, 500, 503]. With form-encoded, the notation is http_statuses[]=429&http_statuses[]=500. With JSON, use an Array.
        /// </summary>
        public int[] Http_statuses { get; set; }
        /// <summary>
        /// Number of timeouts in proxied traffic to consider a target unhealthy, as observed by passive health checks. Defaults to 0.
        /// </summary>
        public int Timeouts { get; set; }
    }

    public class PassiveHealthy
    {
        /// <summary>
        ///  An array of HTTP statuses which represent healthiness when produced by proxied traffic, as observed by passive health checks. Defaults to [200, 201, 202, 203, 204, 205, 206, 207, 208, 226, 300, 301, 302, 303, 304, 305, 306, 307, 308]. With form-encoded, the notation is http_statuses[]=200&http_statuses[]=201. With JSON, use an Array.
        /// </summary>
        public int[] Http_statuses { get; set; }
        /// <summary>
        ///  Number of successes in proxied traffic (as defined by healthchecks.passive.healthy.http_statuses) to consider a target healthy, as observed by passive health checks. Defaults to 0.
        /// </summary>
        public int Successes { get; set; }
    }

    public enum HealthyScheme
    {
        http = 0,
        https,
        tcp
    }

    public enum HashInput
    {
        none = 0,
        consumer,
        ip,
        header,
        cookie
    }
}