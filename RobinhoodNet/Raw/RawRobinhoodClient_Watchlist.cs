using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using Newtonsoft.Json.Linq;

namespace BasicallyMe.RobinhoodNet.Raw
{
    public partial class RawRobinhoodClient
    {
        public Task<JToken>
        //DownloadWatchlist (string listName = "Default", string watchListTemplateUrl = null)
        DownloadWatchlist(string cursor)
        {
            string url = cursor ?? WATCHLISTS_URL;
            //url = url.Replace("{NAME}", listName);
            return doGet(url);
        }
    }
}
