using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banshi.Ip_Tools
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Currency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class TimeZone
    {
        public string name { get; set; }
        public int offset { get; set; }
        public string current_time { get; set; }
        public double current_time_unix { get; set; }
        public bool is_dst { get; set; }
        public int dst_savings { get; set; }
    }

    public class Root
    {
        public string ip { get; set; }
        public string hostname { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_code2 { get; set; }
        public string country_code3 { get; set; }
        public string country_name { get; set; }
        public string country_capital { get; set; }
        public string state_prov { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool is_eu { get; set; }
        public string calling_code { get; set; }
        public string country_tld { get; set; }
        public string languages { get; set; }
        public string country_flag { get; set; }
        public string geoname_id { get; set; }
        public string isp { get; set; }
        public string connection_type { get; set; }
        public string organization { get; set; }
        public string asn { get; set; }
        public Currency currency { get; set; }
        public TimeZone time_zone { get; set; }
    }


}
