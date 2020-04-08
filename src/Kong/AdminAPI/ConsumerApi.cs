using Kong.Common;
using Kong.Models;
using System;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class ConsumerApi : BaseApi
    {
        public ConsumerApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Add Consumer
        /// </summary>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public async Task<Consumer> Add(Consumer consumer)
        {
            var result = await RequestAPI<Consumer>(RequestMethod.Post, RESTfulPath.CONSUMERS, consumer);
            return result;
        }

        /// <summary>
        /// Update Consumer
        /// </summary>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public async Task<Consumer> Update(Consumer consumer)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CONSUMERS, consumer.UserName);
            var result = await RequestAPI<Consumer>(RequestMethod.Patch, path, consumer);
            return result;
        }

        /// <summary>
        ///  Update Consumer Associated to a Specific Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public async Task<Consumer> Update(Guid pluginId, Consumer consumer)
        {
            var path = string.Format("{0}/{1}/consumer", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestAPI<Consumer>(RequestMethod.Patch, path, consumer);
            return result;
        }

        /// <summary>
        /// Create Or Update Consumer
        /// </summary>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public async Task<Consumer> UpdateOrCreate(Consumer consumer)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CONSUMERS, consumer.UserName);
            var result = await RequestAPI<Consumer>(RequestMethod.Put, path, consumer);
            return result;
        }

        /// <summary>
        ///  Create Or Update Consumer Associated to a Specific Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public async Task<Consumer> UpdateOrCreate(Guid pluginId, Consumer consumer)
        {
            var path = string.Format("{0}/{1}/consumer", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestAPI<Consumer>(RequestMethod.Put, path, consumer);
            return result;
        }

        /// <summary>
        /// Delete  Consumer
        /// </summary>
        /// <param name="name">{username or id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CONSUMERS, name);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        /// List Consumers
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<ConsumerCollection> List(string next = null)
        {
            string path = Utils.CreateNextUri(RESTfulPath.CONSUMERS, next);
            var result = await RequestGet<ConsumerCollection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Consumer
        /// </summary>
        /// <param name="name">{username or id}</param>
        /// <returns></returns>
        public async Task<Consumer> Get(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.CONSUMERS, name);
            var result = await RequestGet<Consumer>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Consumer Associated to a Specific Plugin
        /// </summary>
        /// <param name="id">{plugin id}</param>
        /// <returns></returns>
        public async Task<Consumer> GetByPlugin(Guid id)
        {
            var path = string.Format("{0}/{1}/{2}", RESTfulPath.PLUGINS, id, RESTfulPath.CONSUMERS);
            var result = await RequestGet<Consumer>(path);
            return result;
        }
    }
}
