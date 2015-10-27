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

namespace BasicallyMe.RobinhoodNet
{
    public class OrderSnapshot
    {
        [JsonProperty("cumulative_quantity")]
        public decimal CumulativeQuantity { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("account")]
        public string AccountId { get; set; }

        [JsonProperty("stop_price")]
        public decimal? StopPrice { get; set; }

        [JsonProperty("reject_reason")]
        public string RejectReason { get; set; }

        [JsonProperty("url")]
        public string OrderId { get; set; }

        [JsonProperty("average_price")]
        public decimal? AveragePrice { get; set; }

        [JsonProperty("time_in_force")]
        [JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("instrument")]
        public string InstrumentId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("trigger")]
        public string Trigger { get; set; }

        [JsonProperty("last_transaction_at")]
        public DateTime LastTransactionAt { get; set; }

        [JsonProperty("fees")]
        public decimal Fees { get; set; }

        [JsonProperty("cancel")]
        [JsonConverter(typeof(TypedUrlConverter<OrderCancellation>))]
        public Url<OrderCancellation> CancelUrl { get; set; }

        // TODO: Wrap position
        [JsonProperty("position")]
        [JsonConverter(typeof(TypedUrlConverter<Position>))]
        public Url<Position> PositionUrl { get; set; }

        [JsonProperty("executions")]
        public IList<Execution> Executions { get; private set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }

        [JsonProperty("side")]
        [JsonConverter(typeof(SideConverter))]
        public Side Side { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public OrderSnapshot ()
        {
            this.Executions = new List<Execution>();
        }
    }
}
