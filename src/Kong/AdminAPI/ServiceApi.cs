using Kong.Common;
using Kong.Models;
using System;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class ServiceApi : BaseApi
    {
        public ServiceApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Add Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> Add(ServiceInfo service)
        {
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Post, RESTfulPath.SERVICES, service);
            return result;
        }

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> Update(ServiceInfo service)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SERVICES, service.Name);
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Patch, path, service);
            return result;
        }

        /// <summary>
        ///  Update Service Associated to a Specific Route
        /// </summary>
        /// <param name="route">{route name or id}</param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> Update(string route, ServiceInfo service)
        {
            var path = string.Format("{0}/{1}/service", RESTfulPath.ROUTES, route);
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Patch, path, service);
            return result;
        }

        /// <summary>
        ///  Update Service Associated to a Specific Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> Update(Guid pluginId, ServiceInfo service)
        {
            var path = string.Format("{0}/{1}/service", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Patch, path, service);
            return result;
        }

        /// <summary>
        /// Update Or Create Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> UpdateOrCreate(ServiceInfo service)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SERVICES, service.Name);
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Put, path, service);
            return result;
        }

        /// <summary>
        ///  Create Or Update Service Associated to a Specific Route
        /// </summary>
        /// <param name="route">{route name or id}</param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> UpdateOrCreate(string route, ServiceInfo service)
        {
            var path = string.Format("{0}/{1}/service", RESTfulPath.ROUTES, route);
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Put, path, service);
            return result;
        }

        /// <summary>
        ///  Create Or Update Service Associated to a Specific Plugin
        /// </summary>
        /// <param name="pluginId">{plugin id}</param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceInfo> UpdateOrCreate(Guid pluginId, ServiceInfo service)
        {
            var path = string.Format("{0}/{1}/service", RESTfulPath.PLUGINS, pluginId);
            var result = await RequestAPI<ServiceInfo>(RequestMethod.Put, path, service);
            return result;
        }

        /// <summary>
        /// Delete  Service
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SERVICES, name);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        ///  Delete Service Associated to a Specific Route
        /// </summary>
        /// <param name="route">{route name or id}</param>
        /// <returns></returns>
#pragma warning disable CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        public async Task<bool> DeleteByRoute(string route)
#pragma warning restore CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        {
            throw new NotImplementedException("MethodNotAllowed");
            //var path = string.Format("{0}/{1}/service", RESTfulPath.ROUTES, route);
            //var result = await RequestDelete(path);
            //return result;
        }

        /// <summary>
        /// List All Services
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<ServiceInfoConllection> List(string next = null)
        {
            var path = Utils.CreateNextUri(RESTfulPath.SERVICES, next);
            var result = await RequestGet<ServiceInfoConllection>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Service
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<ServiceInfo> Get(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.SERVICES, name);
            var result = await RequestGet<ServiceInfo>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Service Associated to a Specific Route
        /// </summary>
        /// <param name="name">{route name or id}</param>
        /// <returns></returns>
        public async Task<ServiceInfo> GetByRoute(string name)
        {
            var path = string.Format("{0}/{1}/service", RESTfulPath.ROUTES, name);
            var result = await RequestGet<ServiceInfo>(path);
            return result;
        }

        /// <summary>
        /// Retrieve Service Associated to a Specific Plugin
        /// </summary>
        /// <param name="name">{plugin id}</param>
        /// <returns></returns>
        public async Task<ServiceInfo> GetByPlugin(Guid id)
        {
            var path = string.Format("{0}/{1}/service", RESTfulPath.PLUGINS, id);
            var result = await RequestGet<ServiceInfo>(path);
            return result;
        }
    }
}
