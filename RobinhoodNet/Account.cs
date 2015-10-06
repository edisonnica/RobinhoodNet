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

    public class Account
    {
        [JsonProperty("deactivated")]
        public bool IsDeactivated { get; set; }

        [JsonProperty("withdrawal_halted")]
        public bool IsWithdrawalHalted { get; set; }

        [JsonProperty("sweep_enabled")]
        public bool IsSweepEnabled { get; set; }

        [JsonProperty("only_position_closing_trades")]
        public bool OnlyPositionClosingTrades { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("type")]
        public string AccountType { get; set; }

        // TODO: What is this? Separately managed accounts?
        [JsonProperty("sma")]
        public dynamic Sma { get; set; }  // TODO: What is this?

        [JsonProperty("sma_held_for_orders")]
        public dynamic SmaHeldForOrders { get; set; }

        [JsonProperty("margin_balances")]
        public dynamic MarginBalances { get; set; }

        [JsonProperty("cash_balances")]
        public Balance CashBalance { get; set; }

        [JsonProperty("portfolio")]
        [JsonConverter(typeof(TypedUrlConverter<AccountPortfolio>))]
        public Url<AccountPortfolio> PortfolioUrl { get; set; }

        [JsonProperty("user")]
        [JsonConverter(typeof(TypedUrlConverter<User>))]
        public Url<User> UserUrl { get; set; }

        [JsonProperty("url")]
        [JsonConverter(typeof(TypedUrlConverter<Account>))]
        public Url<Account> AccountUrl { get; set; }

        [JsonProperty("positions")]
        [JsonConverter(typeof(TypedUrlConverter<AccountPositions
                              >))]
        public Url<AccountPositions> PositionsUrl { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("max_ach_early_access_amount")]
        public decimal MaxAchEarlyAccessAmount { get; set; }

        public Account()
        {
            CashBalance = new Balance();
        }
    }

}
