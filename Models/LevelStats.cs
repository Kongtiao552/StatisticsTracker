
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StatisticsTracker.Models {

    public class LevelStats {


        [JsonProperty("screens")]
        public Dictionary<int, Screen> LevelScreens { get; set; } = new Dictionary<int, Screen>();

    }

}