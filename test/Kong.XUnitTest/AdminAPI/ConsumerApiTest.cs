using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class ConsumerApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            var consumer = Create();
            var result = await client.Consumer.Add(consumer);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update()
        {
            var consumer = Create();
            var result = await client.Consumer.Update(consumer);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateByPlugin()
        {
            var consumer = Create();
            var result = await client.Consumer.Update(TestCases.PLUGIN_ID, consumer);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreate()
        {
            var consumer = Create();
            var result = await client.Consumer.UpdateOrCreate(consumer);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreateByPlugin()
        {
            var consumer = Create();
            var result = await client.Consumer.UpdateOrCreate(TestCases.PLUGIN_ID, consumer);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete()
        {
            var result = await client.Consumer.Delete(TestCases.CONSUMER_USERNAME);
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Consumer.List();
            while (result.Next != null)
            {
                result = await client.Consumer.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var result = await client.Consumer.Get(TestCases.CONSUMER_USERNAME);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByPlugin()
        {
            var result = await client.Consumer.GetByPlugin(TestCases.PLUGIN_ID);
            Assert.NotNull(result);
        }

        private Consumer Create()
        {
            Consumer consumer = new Consumer()
            {
                Id = Guid.NewGuid(),
                UserName = "my-username",
                Custom_id = "my-custom-id",
                Tags = new string[] { "user-level", "low-priority" }
            };

            return consumer;
        }
    }
}
