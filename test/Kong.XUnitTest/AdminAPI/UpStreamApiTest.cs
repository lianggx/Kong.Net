using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class UpStreamApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            UpStream upstream = Create();
            upstream.Name = "Test-" + upstream.Id.Value.ToString("N");
            upstream = await client.UpStream.Add(upstream);
            Assert.NotNull(upstream);
        }

        [Fact]
        public async Task Update()
        {
            UpStream upstream = Create();
            upstream.Id = TestCases.UPSTREAM_ID;
            upstream = await client.UpStream.Update(upstream);
            Assert.NotNull(upstream);
        }

        [Fact(Skip = "NotImplementedException")]
        public async Task UpdateTarget()
        {
            UpStream upstream = Create();
            upstream = await client.UpStream.Update(TestCases.TARGET, upstream);
            Assert.NotNull(upstream);
        }


        [Fact]
        public async Task UpdateOrCreate()
        {
            UpStream upstream = Create();
            upstream = await client.UpStream.UpdateOrCreate(upstream);
            Assert.NotNull(upstream);
        }

        [Fact(Skip = "NotImplementedException")]
        public async Task UpdateOrCreateByTarget()
        {
            UpStream upstream = Create();
            upstream = await client.UpStream.UpdateOrCreate("172.16.10.227:80", upstream);
            Assert.NotNull(upstream);
        }

        [Fact(Skip = "Manual")]
        public async Task Delete()
        {
            var id = Guid.Parse("683d859e-3a7a-436d-b484-d85a810707a2");
            bool result = await client.UpStream.Delete(id.ToString());
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.UpStream.List();
            while (result.Next != null)
            {
                result = await client.UpStream.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var result = await client.UpStream.Get(TestCases.UPSTREAM_NAME);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShowHealth()
        {
            var result = await client.UpStream.ShowHealth(TestCases.UPSTREAM_NAME);
            Assert.NotNull(result);
        }

        private UpStream Create()
        {
            return new UpStream()
            {
                Id = Guid.NewGuid(),
                Created_at = DateTime.Now,
                Name = "Kong.Test",
                Hash_on = HashInput.none,
                Hash_fallback = HashInput.none,
                Hash_on_cookie_path = "/",
                Slots = 10000,
                HealthChecks = new HealthChecks
                {
                    Active = new UpStreamActive
                    {
                        Https_verify_certificate = true,
                        UnHealthy = new ActiveUnHealthy
                        {
                            Http_statuses = new int[] { 429, 500, 501, 502, 503, 504, 505 },
                            Tcp_failures = 1,
                            Timeouts = 1,
                            Http_failures = 1,
                            Interval = 5
                        },
                        Http_path = "/",
                        Timeout = 1,
                        Healthy = new ActiveHealthy
                        {
                            Http_statuses = new int[] { 200, 302, 404 },
                            Interval = 5,
                            Successes = 1
                        },
                        Https_sni = null,
                        Concurrency = 1,
                        Type = HealthyScheme.http
                    },
                    Passive = new UpStreamPassive
                    {
                        UnHealthy = new PassiveUnHealthy
                        {
                            Http_statuses = new int[] { 429, 500, 501, 502, 503, 504, 505 },
                            Tcp_failures = 0,
                            Timeouts = 0,
                            Http_failures = 0,
                        },
                        Type = HealthyScheme.http,
                        Healthy = new PassiveHealthy
                        {
                            Http_statuses = new int[] { 200, 302, 404 },
                            Successes = 0
                        }
                    }
                },
                Tags = new string[] { "user-level", "low-priority" }
            };
        }
    }
}
