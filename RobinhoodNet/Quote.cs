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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BasicallyMe.RobinhoodNet
{
	public class Quote
	{
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("ask_price")]
        public decimal AskPrice { get; set; }

        [JsonProperty("ask_size")]
        public int     AskSize  { get; set; }

        [JsonProperty("bid_price")]
        public decimal BidPrice { get; set; }

        [JsonProperty("bid_size")]
        public int     BidSize  { get; set; }

        [JsonProperty("last_traded_price")]
        public decimal  LastTradePrice { get; set; }

        [JsonProperty("last_extended_hours_trade_price")]
        public decimal? LastExtendedHoursTradePrice { get; set; }

        [JsonProperty("previous_close")]
        public decimal PreviousClose { get; set; }

        [JsonProperty("adjusted_previous_close")]
        public decimal AdjustedPreviousClose { get; set; }

        [JsonProperty("previous_close_date")]
        public DateTime PreviousCloseDate { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("trading_halted")]
        public bool IsTradingHalted { get; set; }

        [JsonIgnore]
        public decimal Change
        {
            get
            {
                if (this.AdjustedPreviousClose != 0)
                {
                    return (this.LastTradePrice - this.AdjustedPreviousClose) / this.AdjustedPreviousClose;
                }
                return 0;
            }
        }

        [JsonIgnore]
        public decimal ChangePercentage
        {
            get
            {
                return 100 * this.Change;
            }
        }
	}

}
