using System;
using System.IO;
using Newtonsoft.Json;

namespace StatisticsTracker {

    public class ModSettings {

        public const string SettingFile = "StatsTrackerSettings.json";

        public static ModSettings Instance { get; set; }

        public bool HideIngameOverlay { get; set; } = false;

        public static void InitSettings() {
            // If the setting file does not exist, construct a new setting object
            if (!File.Exists(SettingFile)) {
                Instance = new ModSettings();
                SaveSettings();
                return;
            }

            Instance = JsonConvert.DeserializeObject<ModSettings>(File.ReadAllText(SettingFile));
        }

        public static void SaveSettings() {
            File.WriteAllText(SettingFile, JsonConvert.SerializeObject(Instance));
        }

    }
}
