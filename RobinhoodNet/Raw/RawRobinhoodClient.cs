using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

namespace BasicallyMe.RobinhoodNet.Raw
{
    public partial class RawRobinhoodClient
    {
        HttpClient _httpClient;

        static readonly string LOGIN_URL = "https://api.robinhood.com/oauth2/token/";
        static readonly string INVESTMENT_PROFILE_URL = "https://api.robinhood.com/user/investment_profile/";
        static readonly string ACCOUNTS_URL = "https://api.robinhood.com/accounts/";
        static readonly string ACH_IAV_AUTH = "https://api.robinhood.com/ach/iav/auth/";
        static readonly string ACH_RELATIONSHIPS_URL = "https://api.robinhood.com/ach/relationships/";
        static readonly string ACH_TRANSFERS_URL = "https://api.robinhood.com/ach/transfers/";
        static readonly string APPLICATIONS_URL = "https://api.robinhood.com/applications/";
        static readonly string DIVIDENDS_URL = "https://api.robinhood.com/dividends/";
        static readonly string EDOCUMENTS_URL = "https://api.robinhood.com/documents/";
        static readonly string INSTRUMENTS_URL = "https://api.robinhood.com/instruments/";
        static readonly string FUNDAMENTALS_URL = "https://api.robinhood.com/fundamentals/";
        static readonly string MARGIN_UPGRADES_URL = "https://api.robinhood.com/margin/upgrades/";
        static readonly string MARKETS_URL = "https://api.robinhood.com/markets/";
        static readonly string NOTIFICATIONS_URL = "https://api.robinhood.com/notifications/";
        static readonly string ORDERS_URL = "https://api.robinhood.com/orders/";
        static readonly string PASSWORD_RESET_URL = "https://api.robinhood.com/password_reset/request/";
        static readonly string QUOTES_URL = "https://api.robinhood.com/quotes/";
        static readonly string DUCUMENT_REQUESTS_URL = "https://api.robinhood.com/upload/document_requests/";
        static readonly string USER_URL = "https://api.robinhood.com/user/";
        //static readonly string WATCHLISTS_URL = "https://api.robinhood.com/watchlists/{NAME}/";
        static readonly string WATCHLISTS_URL = "https://api.robinhood.com/watchlists/Default/";
        static string POSITIONS_URL;

        public RawRobinhoodClient ()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            _httpClient = new HttpClient(handler);

            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add(
                "Accept-Language",
                "en-US,en;q=0.5");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            _httpClient.DefaultRequestHeaders.Add("Host", "api.robinhood.com");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://robinhood.com");
            _httpClient.DefaultRequestHeaders.Add("Referer", "https://robinhood.com/");
            _httpClient.DefaultRequestHeaders.Add("TE", "Trailers");

            _httpClient.DefaultRequestHeaders.Add("X-Robinhood-API-Version", "1.275.0");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:67.0) Gecko/20100101 Firefox/67.0");
        }




        async Task<JToken>
        parseJsonResponse (Task<HttpResponseMessage> response)
        {
              var r = await response;
            r.EnsureSuccessStatusCode();
            string content = await r.Content.ReadAsStringAsync();
            
            JObject result = JObject.Parse(content);
            return result;
        }

        Task<HttpResponseMessage>
        doPost_NativeResponse (Uri uri, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {
            HttpContent content = null;
            if (pairs != null)
            {
                content = new FormUrlEncodedContent(pairs);
            }

            return _httpClient.PostAsync(uri, content);
        }

        Task<HttpResponseMessage>
        doPost_NativeResponse (string uri, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {
            HttpContent content = null;
            if (pairs != null)
            {
                content = new FormUrlEncodedContent(pairs);
            }

            return _httpClient.PostAsync(uri, content);
        }

        Task<JToken>
        doPost (Uri uri, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {           
            return parseJsonResponse(doPost_NativeResponse(uri, pairs));
        }

        Task<JToken>
        doPost (string url, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {
            return parseJsonResponse(doPost_NativeResponse(url, pairs));
        }

        Task<JToken>
        doGet (Uri uri)
        {
            var resp = _httpClient.GetAsync(uri);
            return parseJsonResponse(resp);
        }

        Task<JToken>
        doGet (string url)
        {
            var resp = _httpClient.GetAsync(url);
            return parseJsonResponse(resp);
        }
    }
}
