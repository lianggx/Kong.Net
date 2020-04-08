using Kong.Common;
using Kong.Models;
using System;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class UpStreamApi : BaseApi
    {
        public UpStreamApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// Add Upstream
        /// </summary>
        /// <param name="upstream"></param>
        /// <returns></returns>
        public async Task<UpStream> Add(UpStream upstream)
        {
            var result = await RequestAPI<UpStream>(RequestMethod.Post, RESTfulPath.UPSTREAMS, upstream);
            return result;
        }

        /// <summary>
        /// Update Or Create Upstream
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<UpStream> UpdateOrCreate(UpStream options)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.UPSTREAMS, options.Name);
            var result = await RequestAPI<UpStream>(RequestMethod.Put, path, options);
            return result;
        }

        /// <summary>
        ///  Create Or Update Upstream Associated to a Specific Target
        /// </summary>
        /// <param name="target">{target host:port or id}</param>
        /// <param name="upstream"></param>
        /// <returns></returns>
#pragma warning disable CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        public async Task<UpStream> UpdateOrCreate(string target, UpStream upstream)
#pragma warning restore CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        {
            throw new NotImplementedException();
            //var path = string.Format("{0}/{1}/upstream", RESTfulPath.TARGETS, target);
            //var result = await RequestAPI<UpStream>(RequestMethod.Put, path, upstream);
            //return result;
        }

        /// <summary>
        /// Update Upstream
        /// </summary>
        /// <param name="upstream"></param>
        /// <returns></returns>
        public async Task<UpStream> Update(UpStream upstream)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.UPSTREAMS, upstream.Name);
            var result = await RequestAPI<UpStream>(RequestMethod.Patch, path, upstream);
            return result;
        }

        /// <summary>
        ///  Update Upstream Associated to a Specific Target
        /// </summary>
        /// <param name="target">{target host:port or id}</param>
        /// <param name="upstream"></param>
        /// <returns></returns>
#pragma warning disable CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        public async Task<UpStream> Update(string target, UpStream upstream)
#pragma warning restore CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        {
            throw new NotImplementedException();
            //var path = string.Format("{0}/{1}/upstream", RESTfulPath.TARGETS, target);
            //var result = await RequestAPI<UpStream>(RequestMethod.Patch, path, upstream);
            //return result;
        }

        /// <summary>
        /// Delete  Upstream
        /// </summary>
        /// <param name="name">{name or id}</param>
        /// <returns></returns>
        public async Task<bool> Delete(string name)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.UPSTREAMS, name);
            var result = await RequestDelete(path);
            return result;
        }

        /// <summary>
        ///  Delete Upstream Associated to a Specific Target
        /// </summary>
        /// <param name="target">{target host:port or id}</param>
        /// <returns></returns>
#pragma warning disable CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        public async Task<bool> DeleteByTarget(string target)
#pragma warning restore CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        {
            throw new NotImplementedException();
            //var path = string.Format("{0}/{1}/upstream", RESTfulPath.TARGETS, target);
            //var result = await RequestDelete(path);
            //return result;
        }

        /// <summary>
        ///  Retrieve
        /// </summary>
        /// <param name="key">{name or id}</param>
        /// <returns></returns>
        public async Task<UpStream> Get(string key)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.UPSTREAMS, key);
            var result = await RequestGet<UpStream>(path);
            return result;
        }

        /// <summary>
        /// List UpStreams
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns></returns>
        public async Task<UpStreamCollection> List(string next = null)
        {
            var path = Utils.CreateNextUri(RESTfulPath.UPSTREAMS, next);
            var result = await RequestGet<UpStreamCollection>(path);
            return result;
        }

        /// <summary>
        /// Show Upstream Health for Node
        /// </summary>
        /// <param name="name">upstreams{name or id}</param>
        /// <returns></returns>
        public async Task<UpstreamHealthCollection> ShowHealth(string name)
        {
            var path = string.Format("{0}/{1}/health", RESTfulPath.UPSTREAMS, name);
            var result = await RequestGet<UpstreamHealthCollection>(path);
            return result;
        }
    }
}
