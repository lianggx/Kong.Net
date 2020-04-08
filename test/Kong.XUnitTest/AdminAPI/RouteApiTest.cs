using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class RouteApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            var route = Create();
            route.Id = Guid.NewGuid();
            var result = await client.Route.Add(route);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddByService()
        {
            var route = Create();
            route.Id = Guid.NewGuid();
            var result = await client.Route.Add(TestCases.SERVICE, route);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update()
        {
            var route = Create();
            var result = await client.Route.Update(route);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateByPlugin()
        {
            var route = Create();
            var result = await client.Route.Update(Guid.Parse("fd842bbc-1a45-48ce-b578-eeb693c42566"), route);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreate()
        {
            var route = Create();
            route.Id = Guid.NewGuid();
            var result = await client.Route.UpdateOrCreate(route);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreateByPlugin()
        {
            var route = Create();
            var result = await client.Route.UpdateOrCreate(Guid.Parse("fd842bbc-1a45-48ce-b578-eeb693c42566"), route);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete()
        {
            var result = await client.Route.Delete("1d6cf7f8fb5d40a89a0a694da86c9ba8");
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Route.List();
            while (result.Next != null)
            {
                result = await client.Route.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListByService()
        {
            var result = await client.Route.ListByService(TestCases.SERVICE);
            while (result.Next != null)
            {
                result = await client.Route.ListByService(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var result = await client.Route.Get(TestCases.ROUTE);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByPlugin()
        {
            var result = await client.Route.GetByPlugin(Guid.Parse("fd842bbc-1a45-48ce-b578-eeb693c42566"));
            Assert.NotNull(result);
        }

        private RouteInfo Create()
        {
            var route = new RouteInfo()
            {
                Name = Guid.NewGuid().ToString("N"),
                Hosts = new string[] { "www.konghq.com" },
                Methods = new string[] { "GET" },
                Protocols = new string[] { "http" },
                Https_redirect_status_code = 301,
                Service = new RouteInfo.ServiceId()
                {
                    Id = TestCases.SERVICE_ID
                }
            };

            return route;
        }
    }
}
