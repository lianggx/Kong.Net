using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class SNIApiTest : BaseTest
    {
        [Fact]
        public async Task Add()
        {
            SNIInfo sni = CreateSNIInfo();
            var result = await client.SNI.Add(sni);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddByCertificate()
        {
            SNIInfo sni = CreateSNIInfo();
            sni.Name = "Kong.Test.SNICertificate";
            var result = await client.SNI.Add(TestCases.CERTIFICATE.ToString(), sni);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update()
        {
            SNIInfo sni = CreateSNIInfo();
            var result = await client.SNI.Update(sni);
        }

        [Fact]
        public async Task UpdateOrCreate()
        {
            SNIInfo sni = CreateSNIInfo();
            sni.Tags = new string[] { "user-level", "low-priority" };
            var result = await client.SNI.UpdateOrCreate(sni);
            Assert.NotNull(result);
        }

        [Fact(Skip = "Manual")]
        public async Task Delete()
        {
            var result = await client.SNI.Delete(TestCases.SNI);
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.SNI.List();
            while (result.Next != null)
            {
                result = await client.SNI.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var result = await client.SNI.Get(TestCases.SNI);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByCertificate()
        {
            var result = await client.SNI.GetByCertificate(TestCases.CERTIFICATE.ToString());
            Assert.NotNull(result);
        }

        private SNIInfo CreateSNIInfo()
        {
            SNIInfo sni = new SNIInfo()
            {
                Certificate = new SNIInfo.CertificateId() { Id = TestCases.CERTIFICATE },
                Name = "Kong.Test.SNI",
                Id = Guid.NewGuid()
            };

            return sni;
        }
    }
}
