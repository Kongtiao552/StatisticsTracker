
using Newtonsoft.Json;

namespace StatisticsTracker.Models {

    public class Screen {

        [JsonProperty("first_arrival_time")]
        public double FirstArrivalTime { get; set; }        

    }

}