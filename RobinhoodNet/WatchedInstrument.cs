using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicallyMe.RobinhoodNet
{
    public class WatchedInstrument
    {
        public DateTime CreatedAt { get; set; }

        public Url<Instrument> InstrumentUrl { get; set; }

        // TODO, NYI
        // public Url<Watchlist> Watchlist { get; set; }

        // TODO what is this for, url to self?
        // public string Url { get; set; }

        public WatchedInstrument()
        {

        }

        internal WatchedInstrument(Newtonsoft.Json.Linq.JToken json)
        {
            //"instrument": "https:\\/\\/api.robinhood.com\\/instruments\\/376ca781-1333-40ca-bd61-a48f46ebd950\\/",
            this.InstrumentUrl = new Url<Instrument>((string)json["instrument"]);

            //"created_at": "2019-06-08T13:14:15.971465Z",
            this.CreatedAt = (DateTime)json["created_at"];
        }
    }


}
