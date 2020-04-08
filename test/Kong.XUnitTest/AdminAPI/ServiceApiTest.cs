using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class ServiceApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            var service = Create();
            var result = await client.Service.Add(service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update()
        {
            var service = Create();
            var result = await client.Service.Update(service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateByRoute()
        {
            var service = Create();
            service.Id = TestCases.SERVICE_ID;
            var result = await client.Service.Update(TestCases.ROUTE, service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateByPlugin()
        {
            var service = Create();
            service.Id = TestCases.SERVICE_ID;
            var result = await client.Service.Update(TestCases.PLUGIN_ID, service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreate()
        {
            var service = Create();
            var result = await client.Service.UpdateOrCreate(service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreateByRoute()
        {
            var service = Create();
            service.Id = TestCases.SERVICE_ID;
            var result = await client.Service.UpdateOrCreate(TestCases.ROUTE, service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreateByPlugin()
        {
            var service = Create();
            service.Id = TestCases.SERVICE_ID;
            var result = await client.Service.Update(TestCases.PLUGIN_ID, service);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete()
        {
            var result = await client.Service.Delete(TestCases.SERVICE);
            Assert.True(result);
        }

        [Fact(Skip = "MethodNotAllowed")]
        public async Task DeleteByRoute()
        {
            var result = await client.Service.DeleteByRoute("2de59a8a-79f9-4f98-a990-ad14b388eac1");
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Service.List();
            while (result.Next != null)
            {
                result = await client.Service.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var service = await client.Service.Get(TestCases.SERVICE);
            Assert.NotNull(service);
        }

        [Fact]
        public async Task GetByRoute()
        {
            var result = await client.Service.GetByRoute(TestCases.ROUTE);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByPlugin()
        {
            var result = await client.Service.GetByPlugin(TestCases.PLUGIN_ID);
            Assert.NotNull(result);
        }

        private ServiceInfo Create()
        {
            var service = new ServiceInfo()
            {
                Name = "Kong.Test.Service",
                Id = Guid.NewGuid(),
                Port = 80,
                Protocol = "https",
                Path = "/",
                Tags = new string[] { "user-level", "low-priority" },
                Host = "konghq.com",
                Connect_timeout = (int)TimeSpan.FromSeconds(5).TotalSeconds,
                Read_timeout = (int)TimeSpan.FromSeconds(60).TotalSeconds,
                Write_timeout = (int)TimeSpan.FromSeconds(60).TotalSeconds,

            };

            return service;
        }
    }
}
