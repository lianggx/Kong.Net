using Kong.Common;
using Kong.Models;
using System;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class RouteApi : BaseApi
    {
        public RouteApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Create Route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<RouteInfo> Add(RouteInfo route)
        {
            var result = await RequestAPI<RouteInfo>(RequestMethod.Post, RESTfulPath.ROUTES, route);
            return result;
        }

        /// <summary>
        /// Create Route Associated to a Specific Service
        /// </summary>
        /// <param name="name">{service name or id}</param>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<RouteInfo> Add(string name, RouteInfo route)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.SERVICES, name, RESTfulPath.ROUTES);
            var result = await RequestAPI<RouteInfo>(RequestMethod.Post, path, route);
            return result;
        }

        /// <summary>
        /// Update Route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<RouteInfo> Update(RouteInfo route)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.ROUTES, route.Name);
            var result = await RequestAPI<RouteInfo>(RequestMethod.Patch, path, route);
            return result;
        }

        /// <summary>
        ///  Update Route Associated to a Specific Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<RouteInfo> Update(Guid pluginId, RouteInfo route)
        {
            var path = string.Format("{0}/{1}/route", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestAPI<RouteInfo>(RequestMethod.Patch, path, route);
            return result;
        }

        /// <summary>
        /// Create Or Update Route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<RouteInfo> UpdateOrCreate(RouteInfo route)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.ROUTES, route.Name);
            var result = await RequestAPI<RouteInfo>(RequestMethod.Put, path, route);
            return result;
        }

        /// <summary>
        ///  Create Or Update Route Associated to a Specific Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<RouteInfo> UpdateOrCreate(Guid pluginId, RouteInfo route)
        {
            var path = string.Format("{0}/{1}/route", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestAPI<RouteInfo>(RequestMethod.Put, path, route);
            return result;
        }

        /// <summary>
        /// Delete Route
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.ROUTES, name);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        /// List All Routes
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<RouteInfoCollection> List(string next = null)
        {
            var path = Utils.CreateNextUri(RESTfulPath.ROUTES, next);
            var result = await RequestGet<RouteInfoCollection>(path);
            return result;
        }

        /// <summary>
        /// List Routes Associated to a Specific Service
        /// </summary>
        /// <param name="name">{service name or id}</param>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<RouteInfoCollection> List(string name, string next = null)
        {
            var path = string.Format("", RESTfulPath.SERVICES, name, RESTfulPath.ROUTES);
            path = Utils.CreateNextUri(RESTfulPath.ROUTES, next);
            var result = await RequestGet<RouteInfoCollection>(path);
            return result;
        }

        /// <summary>
        /// List All Routes
        /// </summary>
        /// <param name="name">{service name or id}</param>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<RouteInfoCollection> ListByService(string name, string next = null)
        {
            var path = string.Format("{0}/{1}{2}", RESTfulPath.SERVICES, name, RESTfulPath.ROUTES);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<RouteInfoCollection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Route Associated to a Specific Plugin
        /// </summary>
        /// <param name="name">{plugin id}</param>
        /// <returns></returns>
        public async Task<RouteInfo> GetByPlugin(Guid id)
        {
            var path = string.Format("{0}/{1}/{2}", RESTfulPath.PLUGINS, id, RESTfulPath.ROUTES);
            var result = await RequestGet<RouteInfo>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Route
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<RouteInfo> Get(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.ROUTES, name);
            var result = await RequestGet<RouteInfo>(path);
            return result;
        }
    }
}
