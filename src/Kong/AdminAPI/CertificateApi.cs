using Kong.Common;
using Kong.Models;
using System;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class CertificateApi : BaseApi
    {
        public CertificateApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Add Certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public async Task<Certificate> Add(Certificate certificate)
        {
            var result = await RequestAPI<Certificate>(RequestMethod.Post, RESTfulPath.CERTIFICATES, certificate);
            return result;
        }

        /// <summary>
        /// Update Certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public async Task<Certificate> Update(Certificate certificate)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CERTIFICATES, certificate.Id);
            var result = await RequestAPI<Certificate>(RequestMethod.Patch, path, certificate);
            return result;
        }

        /// <summary>
        /// Create Or Update Certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public async Task<Certificate> UpdateOrCreate(Certificate certificate)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CERTIFICATES, certificate.Id);
            var result = await RequestAPI<Certificate>(RequestMethod.Put, path, certificate);
            return result;
        }

        /// <summary>
        /// Delete Certificate
        /// </summary>
        /// <param name="certificateId">{certificate id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid certificateId)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CERTIFICATES, certificateId);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        /// List Certificates
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<CertificateCollection> List(string next = null)
        {
            string path = Utils.CreateNextUri(RESTfulPath.CERTIFICATES, next);
            var result = await RequestGet<CertificateCollection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Certificates
        /// </summary>
        /// <param name="name">{certificates id}</param>
        /// <returns></returns>
        public async Task<Certificate> Get(Guid id)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CERTIFICATES, id);
            var result = await RequestGet<Certificate>(path);
            return result;
        }
    }
}
