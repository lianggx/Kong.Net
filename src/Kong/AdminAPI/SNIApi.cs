using Kong.Common;
using Kong.Models;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class SNIApi : BaseApi
    {
        public SNIApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Add SNI
        /// </summary>
        /// <param name="sni"></param>
        /// <returns></returns>
        public async Task<SNIInfo> Add(SNIInfo sni)
        {
            var result = await RequestAPI<SNIInfo>(RequestMethod.Post, RESTfulPath.SNIS, sni);
            return result;
        }

        /// <summary>
        /// Create SNI Associated to a Specific Certificate
        /// </summary>
        /// <param name="name">{certificate name or id}</param>
        /// <param name="sni"></param>
        /// <returns></returns>
        public async Task<SNIInfo> Add(string name, SNIInfo sni)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.CERTIFICATES, name, RESTfulPath.SNIS);
            var result = await RequestAPI<SNIInfo>(RequestMethod.Post, path, sni);
            return result;
        }

        /// <summary>
        /// Update SNI
        /// </summary>
        /// <param name="sni"></param>
        /// <returns></returns>
        public async Task<SNIInfo> Update(SNIInfo sni)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SNIS, sni.Name);
            var result = await RequestAPI<SNIInfo>(RequestMethod.Patch, path, sni);
            return result;
        }

        /// <summary>
        /// Create Or Update SNI
        /// </summary>
        /// <param name="sni"></param>
        /// <returns></returns>
        public async Task<SNIInfo> UpdateOrCreate(SNIInfo sni)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SNIS, sni.Name);
            var result = await RequestAPI<SNIInfo>(RequestMethod.Put, path, sni);
            return result;
        }

        /// <summary>
        /// Delete  SNI
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SNIS, name);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        /// List All SNIs
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<SNIInfoCollection> List(string next = null)
        {
            var path = Utils.CreateNextUri(RESTfulPath.SNIS, next);
            var result = await RequestGet<SNIInfoCollection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve SNI
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<SNIInfo> Get(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SNIS, name);
            var result = await RequestGet<SNIInfo>(path);
            return result;
        }

        /// <summary>
        /// List SNIs Associated to a Specific Certificate
        /// </summary>
        /// <param name="name">{certificate name or id}</param>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<SNIInfoCollection> GetByCertificate(string name, string next = null)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.CERTIFICATES, name, RESTfulPath.SNIS);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<SNIInfoCollection>(path);
            return result;
        }
    }
}
