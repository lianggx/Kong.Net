using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class TargetApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            var target = new TargetInfo()
            {
                Created_at = DateTime.Now,
                Id = Guid.NewGuid(),
                Tags = new string[] { "user-level", "low-priority" },
                Target = "172.16.10.227:80",
                UpStream = new TargetInfo.UpStreamId() { Id = TestCases.UPSTREAM_ID },
                Weight = 100
            };
            target = await client.Target.Add(target);

            Assert.NotNull(target);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Target.List(TestCases.UPSTREAM_NAME);
            while (result.Next != null)
            {
                result = await client.Target.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListAll()
        {
            var result = await client.Target.ListAll(TestCases.UPSTREAM_NAME);
            Assert.NotNull(result);
        }

        [Fact(Skip = "")]
        public async Task Delete()
        {
            var result = await client.Target.Delete(TestCases.UPSTREAM_NAME, TestCases.TARGET);
            Assert.True(result);
        }

        [Fact]
        public async Task SetHealthy()
        {
            var result = await client.Target.SetHealthy(TestCases.UPSTREAM_NAME, TestCases.TARGET, true);
            Assert.True(result);
        }
    }
}
