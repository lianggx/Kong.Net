using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class TagsApiTest : BaseTest
    {
        [Fact]
        public async Task ListAll()
        {
            var result = await client.Tags.ListAll();
            while (result.Next != null)
            {
                result = await client.Tags.ListAll(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Tags.List(TestCases.TAGS);
            while (result.Next != null)
            {
                result = await client.Tags.List(TestCases.TAGS, result.Next);
            }
            Assert.NotNull(result);
        }
    }
}
