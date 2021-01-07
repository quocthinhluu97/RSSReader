using RSSReader.Shared;
using RSSReader.UI.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSReader.UI.Services
{
    public interface IRssDataService
    {
        Task<IEnumerable<Post>> GetNewsFeed();
    }
}