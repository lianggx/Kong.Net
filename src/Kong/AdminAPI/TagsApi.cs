using Kong.Common;
using Kong.Models;
using System.Threading.Tasks;

namespace Kong.AdminAPI
{
    public class TagsApi : BaseApi
    {
        public TagsApi(KongClientOptions options) : base(options) { }

        /// <summary>
        /// List All Tags
        /// </summary>
        /// <param name="next">{next page uri}</param>
        /// <returns>Returns the entities that have been tagged with the specified tag.</returns>
        public async Task<TagsInfoCollection> ListAll(string next = null)
        {
            var path = Utils.CreateNextUri(RESTfulPath.TAGS, next);
            var result = await RequestGet<TagsInfoCollection>(path);
            return result;
        }

        /// <summary>
        /// List Entity Ids by Tag
        /// </summary>
        /// <remarks>
        /// The list of entities will not be restricted to a single entity type: all the entities tagged with tags will be present on this list.
        /// </remarks>
        /// <param name="tags"></param>
        /// <param name="next">{next page uri}</param>
        /// <returns>Returns the entities that have been tagged with the specified tag.</returns>
        public async Task<TagsInfoCollection> List(string tags, string next = null)
        {
            var path = string.Format("{0}/{1}", RESTfulPath.TAGS, tags);
            path = Utils.CreateNextUri(path, next);
            var result = await RequestGet<TagsInfoCollection>(path);
            return result;
        }
    }
}
