using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class NodeApiTest : BaseTest
    {
        [Fact]
        public async Task Get()
        {
            var result = await client.Node.Get();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Status()
        {
            var result = await client.Node.Status();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Status_unit_scale()
        {
            string unit = "k";
            int scale = 1;
            var result = await client.Node.Status(unit, scale);
            Assert.NotNull(result);
        }
    }
}
