using Kong.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kong.XUnitTest.AdminAPI
{
    public class CertificateApiTest : BaseTest
    {

        [Fact]
        public async Task Add()
        {
            var cert = Create();
            var result = await client.Certificate.Add(cert);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update()
        {
            var cert = Create();
            cert.Id = TestCases.CERTIFICATE;
            var result = await client.Certificate.Update(cert);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrCreate()
        {
            var cert = Create();
            cert.Id = TestCases.CERTIFICATE;
            var result = await client.Certificate.UpdateOrCreate(cert);
            Assert.NotNull(result);
        }

        [Fact(Skip = "Manual")]
        public async Task Delete()
        {
            var result = await client.Certificate.Delete(Guid.Parse("242d323f-d699-4469-8667-4e9a70e6b925"));
            Assert.True(result);
        }

        [Fact]
        public async Task List()
        {
            var result = await client.Certificate.List();
            while (result.Next != null)
            {
                result = await client.Certificate.List(result.Next);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get()
        {
            var result = await client.Certificate.Get(TestCases.CERTIFICATE);
            Assert.NotNull(result);
        }

        private Certificate Create()
        {
            var certificate = new Certificate()
            {
                Id = Guid.NewGuid(),
                Cert = @"-----BEGIN CERTIFICATE-----
MIIDlzCCAn+gAwIBAgIJAL+HNBxkpgyYMA0GCSqGSIb3DQEBCwUAMGIxCzAJBgNV
BAYTAlVTMRMwEQYDVQQIDApDYWxpZm9ybmlhMRYwFAYDVQQHDA1TYW4gRnJhbmNp
c2NvMREwDwYDVQQKDAhLb25nIEluYzETMBEGA1UEAwwKa29uZ2hxLmNvbTAeFw0x
ODA0MDMwNDExMTdaFw0yMzA0MDIwNDExMTdaMGIxCzAJBgNVBAYTAlVTMRMwEQYD
VQQIDApDYWxpZm9ybmlhMRYwFAYDVQQHDA1TYW4gRnJhbmNpc2NvMREwDwYDVQQK
DAhLb25nIEluYzETMBEGA1UEAwwKa29uZ2hxLmNvbTCCASIwDQYJKoZIhvcNAQEB
BQADggEPADCCAQoCggEBAL0yIZMgr6DmdZFyCaT7FhyPlgIpBYTFOztB/FUsBtLz
ypUtZ/siobDwKd0EYYVPSPhKiWrPt8Fa9hde5vw38dnDseo4ZfgnVvK89ZfEQQUw
EI/CwmGI7viWa/VIwbmzA2JLyjUCjW447qcK5+EMtPPdyE7GXRmCKiNOzlRXd2nm
eGfCKoYmVQN/sH+JVMze4uMyOlkL/MJpHzK5cG/238W8cgUkyaXfecaTYc+aeL0s
nkbcbC1y8+OklruQpw8eJa5AoBUzCB9Lj/kCG4OPY/HOsiCD8d/IC8VUoySGixeE
a/keHAuLAvM5apC6tsONtUii8K+Ae/TABSvFCvWb2ccCAwEAAaNQME4wHQYDVR0O
BBYEFJBCvtafFLmsN3PFKtIyrrNJW8DDMB8GA1UdIwQYMBaAFJBCvtafFLmsN3PF
KtIyrrNJW8DDMAwGA1UdEwQFMAMBAf8wDQYJKoZIhvcNAQELBQADggEBABamHDdY
soKbG/4SbdAfmysPBLkfThy9OyInXGejzEIFNEfMZAWVxZNwClBgug4bgC4UCrbO
0pF0Z0PJR3+pXdF883RYBpI4GihslSf1gbXBw8ukiDZlTeXGMJKC2js2xxY4IBUi
Mf3A7XHcrxLvSLTk0U6XMA70YxMcW8mqa3mSUnZZls0n1vKvh2dxVN0DVJFP9AV0
l10djLk1uVrAUQQswlESn/M4TzIExkvjbjDoBPmcLHbUMVMdsMd3Bpa8s9e44wbL
RUfZgrmFt0XLq0jgEpeLnNc1Wqw3428EXfkVcytHCVpRk5G7V80Sf7/heW8wNeVk
4C09Gcs1JivKP3Y=
-----END CERTIFICATE-----",
                Key = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAvTIhkyCvoOZ1kXIJpPsWHI+WAikFhMU7O0H8VSwG0vPKlS1n
+yKhsPAp3QRhhU9I+EqJas+3wVr2F17m/Dfx2cOx6jhl+CdW8rz1l8RBBTAQj8LC
YYju+JZr9UjBubMDYkvKNQKNbjjupwrn4Qy0893ITsZdGYIqI07OVFd3aeZ4Z8Iq
hiZVA3+wf4lUzN7i4zI6WQv8wmkfMrlwb/bfxbxyBSTJpd95xpNhz5p4vSyeRtxs
LXLz46SWu5CnDx4lrkCgFTMIH0uP+QIbg49j8c6yIIPx38gLxVSjJIaLF4Rr+R4c
C4sC8zlqkLq2w421SKLwr4B79MAFK8UK9ZvZxwIDAQABAoIBAGsbrGJMyOEAV2LF
+qvJ8hStPTFv483sksHTc3UMfbiDiBa4I/vK+VrgO/MB/euonRjjqbQscE0on9VP
RtlXGrY70cdVsnSwYMr/KtKGqoCzW0zn53+sNA3LqsasL/BfZfUKDym/ji3uUT2E
MQ35UaAV2MawChjc1dozTR/2fIYYmAOWLI8cLoKDFxjfeX15GYMfswj9PM1Slq0/
z4hKAs/BmUvmjngPe6JcWqLrfIyB4Btz+aT+fi44QhXyJVHAy0M0/T2sd4uSivQA
Cr2H5I6wukOZvFN3lTG5HBphPkXt41Gp3rBkGt6qeE5G/i//wwe2n4/INwG3NCDf
+S8jskECgYEA7SwugjJZepUjCvc/XYSzcq2cV4pILuasNXoKMI6vfvGyfY8dthTj
zvl4L6CoA3cNLPNSMsGqmbtjcS5G+Y69WI0MbmbqXo5WYj/vFWwI66mJ/5nhhp16
rauQ4lU4UiXqf3f8mL14y+7TPoBb6c3GkXBibmwz2IfHVKQINXu1SdECgYEAzDb3
YzyY/wmECK8lLFXpA1rgwzcW5OZv3mSV0y2maOqj3t4pcvzXtQAVL61FAxu2j1v8
HsfeIkpu/My5TzHCiclk4GxLlFsOHHguSX44Dw4JxAILx+qT2jT9U71ezichKsIl
uCuQ8Ysvrw9sR6Imx4AIRBB9dOSrXtIzXCQEuBcCgYAzvy8Kkye4ui9iJh36Lojk
nYJ+CxrCuOub41uzyn356YwzHvWxk488ymtxoNDnqKMESraFgoHRdvQ0bo9nxcAE
QQoUUHoUVWP9nctxVhgAKwaD8TQmpddtawB6kXNvYPxwAWLohHaFsD8A5Qqo0Y/g
ja+8Pfl15fIUwpFT8gDU8QKBgQCwSSrZkbAJSS+fR4JxeWACs2qfWmj7BCnB81aa
zCeBHjyD4YgqaTXUW9PuKkcO3deEfcVw1NxfAZ45wIifYrcqtp3MVfAQi2HtFZnv
e3PtGxM3DwUYeNlVXrTomurCT2kEPkDNcV5YBO0O0+OHGuUbBt0b1JhYViXRXudT
PQyN1QKBgQCk0fbqY3+5Ue9ak+Wih67dmhJMzF+p1oxA1nywpd6VIEexcWFj8Oqp
pGS1psLykor2INzS8VufJKK2/bn3Uo2QimTcKxEniSvIIEf9EhrreXw2Kv32O7gy
L2frADlA8GJizhYdZKR/VTCbP4MhmFccJ+fSQ/Ty66scBvk1jKX0Gg==
-----END RSA PRIVATE KEY-----"
            };

            return certificate;
        }
    }
}
