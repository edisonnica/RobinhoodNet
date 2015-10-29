// The MIT License (MIT)
// 
// Copyright (c) 2015 Filip Frącz
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BasicallyMe.RobinhoodNet
{
    public partial class RobinhoodClient
    {
        Raw.RawRobinhoodClient _rawClient;


        public RobinhoodClient ()
        {
            _rawClient = new Raw.RawRobinhoodClient();
        }

        public string AuthToken
        {
            get { return _rawClient.AuthToken; }
        }

        public Task
        Authenticate (string userName, string password)
        {
            return _rawClient.Authenticate(userName, password);
        }

        public Task Authenticate (string token)
        {
            return _rawClient.Authenticate(token);
        }

        async Task<IList<TResult>>
        downloadAll<TResult>(
            Func<Url<TResult>, Task<PagedResponse<TResult>>> downloadSingle,
            Url<TResult> cursor = null)
        {
            var all = new List<TResult>();

            PagedResponse<TResult> r = null;
            do
            {
                r = await downloadSingle(cursor);
                all.AddRange(r.Items);
                cursor = r.Next;

            } while (cursor != null);

            return all;
        }

        async Task<PagedResponse<TResult>>
        downloadPagedResult<TResult> (
            Url<TResult> cursor,
            Func<string, Task<JToken>> downloader,
            Func<JToken, TResult> decoder)
        {
            var resp = await downloader(cursor == null ? null : cursor.Uri.ToString());
            var result = new PagedJsonResponse<TResult>(resp, decoder);
            return result;
        }

        public Task<IList<Account>>
        DownloadAllAccounts ()
        {
            return downloadAll<Account>(this.DownloadAccounts);
        }

        public Task<PagedResponse<Account>>
        DownloadAccounts (Url<Account> cursor = null)
        {
            return downloadPagedResult<Account>(
                cursor,
                _rawClient.DownloadAccounts,
                json => json.ToObject<Account>());
        }



        public Task<IList<Position>>
        DownloadAllAccountPositions (Url<Position> accountPositionUrl)
        {
            return downloadAll<Position>(
                this.DownloadAccountPositions,
                accountPositionUrl);
        }

        public Task<PagedResponse<Position>>
        DownloadAccountPositions (Url<Position> accountPositionUrl)
        {
            return downloadPagedResult<Position>(
                accountPositionUrl,
                _rawClient.DownloadAccountPositions,
                json => json.ToObject<Position>());
        }



        public Task<IList<OrderSnapshot>>
        DownloadAllOrders ()
        {
            return downloadAll<OrderSnapshot>(this.DownloadOrders);
        }

        public Task<PagedResponse<OrderSnapshot>>
        DownloadOrders (Url<OrderSnapshot> cursor = null)
        {
            return downloadPagedResult<OrderSnapshot>(
                cursor,
                _rawClient.DownloadOrders,
                json => json.ToObject<OrderSnapshot>());
        }


        public async Task<OrderSnapshot>
        PlaceOrder (NewOrderSingle newOrderSingle)
        {
            var json = await _rawClient.PlaceOrder(newOrderSingle.ToDictionary());
            return json.ToObject<OrderSnapshot>();
        }

        public Task
        CancelOrder (Url<OrderCancellation> cancellationUrl)
        {
            return _rawClient.CancelOrder(cancellationUrl.Uri.ToString());
        }


        public async Task<Instrument>
        DownloadInstrument (Url<Instrument> instrumentUrl)
        {
            var json = await _rawClient.DownloadInstrument(instrumentUrl.Uri.ToString());
            return json.ToObject<Instrument>();
        }

        public async Task<InstrumentFundamentals>
        DownloadInstrumentFundamentals (string symbol)
        {
            var json = await _rawClient.DownloadInstrumentFundamentalsForSymbol(symbol);
            return json.ToObject<InstrumentFundamentals>();
        }

        public async Task<InstrumentFundamentals>
        DownloadInstrumentFundamentals (Url<InstrumentFundamentals> url)
        {
            var json = await _rawClient.DownloadInstrumentFundamentalsForUrl(url.Uri.ToString());
            return json.ToObject<InstrumentFundamentals>();
        }


        public Task<IList<InstrumentSplit>>
        DownloadAllInstrumentSplits (Url<InstrumentSplit> splitsUrl)
        {
            return downloadAll<InstrumentSplit>(
                this.DownloadInstrumentSplits,
                splitsUrl);
        }

        public Task<PagedResponse<InstrumentSplit>>
        DownloadInstrumentSplits (Url<InstrumentSplit> splitsUrl)
        {
            return downloadPagedResult<InstrumentSplit>(
                splitsUrl,
                _rawClient.DownloadOrders,
                json => json.ToObject<InstrumentSplit>());
        }



        public async Task<IList<Instrument>>
        FindInstrument (string symbol)
        {
            var resp = await _rawClient.FindInstrument(symbol);
            var result = new PagedJsonResponse<Instrument>(resp, item => item.ToObject<Instrument>());
            return result.Items;
        }

        public async Task<Quote>
        DownloadQuote (string symbol)
        {
            var q = await _rawClient.DownloadQuote(symbol);
            return q.ToObject<Quote>();
        }

        public async Task<IList<Quote>>
        DownloadQuote (IEnumerable<string> symbols)
        {
            var qq = await _rawClient.DownloadQuote(symbols);

            List<Quote> quotes = new List<Quote>();
            foreach (var o in (JArray)qq)
            {
                Quote q = null;
                if (o != null && o.HasValues)
                {
                    q = o.ToObject<Quote>();
                }
                quotes.Add(q);
            }

            return quotes;
        }

        public Task<IList<Quote>>
        DownloadQuote (params string[] symbols)
        {
            return DownloadQuote((IEnumerable<string>)symbols);
        }
    }
}
