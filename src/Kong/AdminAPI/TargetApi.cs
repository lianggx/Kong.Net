using Kong.Common;
using Kong.Models;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class TargetApi : BaseApi
    {
        public TargetApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Add Target
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<TargetInfo> Add(TargetInfo target)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.UPSTREAMS, target.UpStream.Id, RESTfulPath.TARGETS);
            var result = await RequestAPI<TargetInfo>(RequestMethod.Post, path, target);
            return result;
        }

        /// <summary>
        /// List Targets
        /// </summary>
        /// <param name="upstream">{upstream host:port or id}</param>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<TargetInfoCollection> List(string upstream, string next = null)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.UPSTREAMS, upstream, RESTfulPath.TARGETS);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<TargetInfoCollection>(path);
            return result;
        }

        /// <summary>
        /// List All Targets
        /// </summary>
        /// <param name="upstream">{upstream host:port or id}</param>
        /// <returns></returns>
        public async Task<TargetInfoAllCollection> ListAll(string upstream)
        {
            var path = string.Format("{0}/{1}{2}/all", RESTfulPath.UPSTREAMS, upstream, RESTfulPath.TARGETS);
            var result = await RequestGet<TargetInfoAllCollection>(path);
            return result;
        }

        /// <summary>
        /// Delete Target
        /// </summary>
        /// <param name="upstream">{upstream name or id}</param>
        /// <param name="target">{host:port or id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(string upstream, string target)
        {
            var path = string.Format("{0}/{1}{2}/{3}", RESTfulPath.UPSTREAMS, upstream, RESTfulPath.TARGETS, target);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        /// Set Target As Healthy
        /// </summary>
        /// <param name="upstream">{upstream name or id}</param>
        /// <param name="target">{host:port or id}</param>
        /// <param name="healthy">true=healthy,false=unhealthy</param>
        /// <returns></returns>
        public async Task<bool> SetHealthy(string upstream, string target, bool healthy)
        {
            var action = healthy ? "healthy" : "unhealthy";
            var path = string.Format("{0}/{1}{2}/{3}/{4}", RESTfulPath.UPSTREAMS, upstream, RESTfulPath.TARGETS, target, action);
            var response = await RequestPost(path);
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
