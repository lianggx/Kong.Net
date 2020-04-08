using Kong.Common;
using Kong.Models;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class NodeApi : BaseApi
    {
        public NodeApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Retrieve Node Information
        /// </summary>
        /// <returns></returns>
        public async Task<NodeInfo> Get()
        {
            var result = await RequestGet<NodeInfo>(RESTfulPath.NODE);
            return result;
        }

        /// <summary>
        /// Retrieve Node Status
        /// </summary>
        /// <returns></returns>
        public async Task<NodeStatus> Status()
        {
            var result = await RequestGet<NodeStatus>(RESTfulPath.STATUS);
            return result;
        }

        /// <summary>
        ///  Retrieve Node Status
        /// </summary>
        /// <param name="unit">one of b/B, k/K, m/M, g/G, which will return results in bytes, kibibytes, mebibytes, or gibibytes, respectively. When “bytes” are requested, the memory values in the response will have a number type instead of string. Defaults to m.</param>
        /// <param name="scale">the number of digits to the right of the decimal points when values are given in human-readable memory strings (unit other than “bytes”). Defaults to 2. You can get the shared dictionaries memory usage in kibibytes with 4 digits of precision by doing: </param>
        /// <returns></returns>
        public async Task<NodeStatus> Status(string unit, int scale)
        {
            var path = string.Format("{0}?unit={1}&scale={2}", RESTfulPath.STATUS, unit, scale);
            var result = await RequestGet<NodeStatus>(path);
            return result;
        }
    }
}
