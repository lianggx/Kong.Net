using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class PluginApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            var plugin = Create();
            var result = await client.Plugin.Add(plugin);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddByRoute()
        {
            var plugin = Create();
            var result = await client.Plugin.Add(TestCases.ROUTE_ID, plugin);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddByService()
        {
            var plugin = Create();
            var result = await client.Plugin.AddByService(TestCases.SERVICE_ID, plugin);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddByConsumer()
        {
            var plugin = Create();
            var result = await client.Plugin.AddByService(TestCases.CONSUMER_ID, plugin);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update()
        {
            var plugin = Create();
            plugin.Id = TestCases.PLUGIN_ID;
            var result = await client.Plugin.Update(plugin);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreate()
        {
            var plugin = Create();
            plugin.Id = TestCases.PLUGIN_ID;
            var result = await client.Plugin.UpdateOrCreate(plugin);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete()
        {
            var result = await client.Plugin.Delete(TestCases.PLUGIN_ID);
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Plugin.List();
            while (result.Next != null)
            {
                result = await client.Plugin.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var result = await client.Plugin.Get(TestCases.PLUGIN_ID);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListByRoute()
        {
            var result = await client.Plugin.ListByRoute(TestCases.ROUTE_ID);
            while (result.Next != null)
            {
                result = await client.Plugin.ListByRoute(TestCases.ROUTE_ID, result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListByService()
        {
            var result = await client.Plugin.ListByService(TestCases.SERVICE_ID);
            while (result.Next != null)
            {
                result = await client.Plugin.ListByService(TestCases.SERVICE_ID, result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListByConsumer()
        {
            var result = await client.Plugin.ListByConsumer(TestCases.CONSUMER_ID);
            while (result.Next != null)
            {
                result = await client.Plugin.ListByConsumer(TestCases.CONSUMER_ID, result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEnabled()
        {
            var result = await client.Plugin.GetEnabled();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSchema()
        {
            var result = await client.Plugin.GetSchema(TestCases.PLUGIN_NAME);
            Assert.NotNull(result);
        }

        private Plugin Create()
        {
            var plugin = new Plugin()
            {
                Id = Guid.NewGuid(),
                Name = "rate-limiting",
                Route = null,
                Service = null,
                Consumer = null,
                Config = new PluginConfig
                {
                    Hour = 500,
                    Minute = 20
                },
                Run_on = "first",
                Protocols = new string[] { "http", "https" },
                Enabled = true,
                Tags = new string[] { "user-level", "low-priority" }
            };

            return plugin;
        }
    }
}
