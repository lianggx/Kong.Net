using Kong.Common;
using Kong.Models;
using System;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class PluginApi : BaseApi
    {
        public PluginApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Create  Plugin
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> Add(Plugin plugin)
        {
            var result = await RequestAPI<Plugin>(RequestMethod.Post, RESTfulPath.PLUGINS, plugin);
            return result;
        }

        /// <summary>
        /// Create  Plugin Associated to a Specific Route
        /// </summary>
        /// <param name="routeId">{route id}</param>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> Add(Guid routeId, Plugin plugin)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.ROUTES, routeId, RESTfulPath.PLUGINS);
            var result = await RequestAPI<Plugin>(RequestMethod.Post, path, plugin);
            return result;
        }

        /// <summary>
        /// Create  Plugin Associated to a Specific Service
        /// </summary>
        /// <param name="serviceId">{service id}</param>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> AddByService(Guid serviceId, Plugin plugin)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.SERVICES, serviceId, RESTfulPath.PLUGINS);
            var result = await RequestAPI<Plugin>(RequestMethod.Post, path, plugin);
            return result;
        }

        /// <summary>
        /// Create  Plugin Associated to a Specific Consumer
        /// </summary>
        /// <param name="consumerId">{consumer id}</param>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> AddByConsumer(Guid consumerId, Plugin plugin)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.CONSUMERS, consumerId, RESTfulPath.PLUGINS);
            var result = await RequestAPI<Plugin>(RequestMethod.Post, path, plugin);
            return result;
        }

        /// <summary>
        /// Update Plugin
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> Update(Plugin plugin)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.PLUGINS, plugin.Id);
            var result = await RequestAPI<Plugin>(RequestMethod.Patch, path, plugin);
            return result;
        }

        /// <summary>
        /// Update Or Create Plugin
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> UpdateOrCreate(Plugin plugin)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.PLUGINS, plugin.Id);
            var result = await RequestAPI<Plugin>(RequestMethod.Put, path, plugin);
            return result;
        }

        /// <summary>
        /// Delete Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid pluginId)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        /// List All Plugins
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<PluginCollection> List(string next = null)
        {
            string path = Utils.CreateNextUri(RESTfulPath.PLUGINS, next);
            var result = await RequestGet<PluginCollection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Plugin
        /// </summary>
        /// <param name="id">{plugin id}</param>
        /// <returns></returns>
        public async Task<Plugin> Get(Guid id)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.PLUGINS, id);
            var result = await RequestGet<Plugin>(path);
            return result;
        }

        /// <summary>
        /// List Plugins Associated to a Specific Route
        /// </summary>
        /// <param name="id">{route id}</param>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<PluginCollection> ListByRoute(Guid id, string next = null)
        {
            string path = string.Format("{0}/{1}{2}", RESTfulPath.ROUTES, id, RESTfulPath.PLUGINS);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<PluginCollection>(path);
            return result;
        }

        /// <summary>
        /// List Plugins Associated to a Specific Service
        /// </summary>
        /// <param name="id">{service id}</param>
        /// <returns></returns>
        public async Task<PluginCollection> ListByService(Guid id, string next = null)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.SERVICES, id, RESTfulPath.PLUGINS);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<PluginCollection>(path);
            return result;
        }

        /// <summary>
        /// List Plugins Associated to a Specific Consumer
        /// </summary>
        /// <param name="id">{consumers id}</param>
        /// <returns></returns>
        public async Task<PluginCollection> ListByConsumer(Guid id, string next = null)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.CONSUMERS, id, RESTfulPath.PLUGINS);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<PluginCollection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Enabled Plugins
        /// </summary>
        /// <returns></returns>
        public async Task<PluginEnabled> GetEnabled()
        {
            var path = string.Format("{0}/enabled", RESTfulPath.PLUGINS);
            var result = await RequestGet<PluginEnabled>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Plugin Schema
        /// </summary>
        /// <param name="name">{plugin name}</param>
        /// <returns></returns>
        public async Task<PluginSchema> GetSchema(string name)
        {
            var path = string.Format("{0}/schema/{1}", RESTfulPath.PLUGINS, name);
            var result = await RequestGet<PluginSchema>(path);
            return result;
        }
    }
}
