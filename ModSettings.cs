using System;
using System.IO;
using System.Xml.Serialization;

namespace StatisticsTracker {

    public class ModSettings {

        public const string SettingFile = "StatsTrackerSettings.xml";

        public static ModSettings Instance { get; set; }

        public bool HideIngameOverlay { get; set; } = false;

        public static void InitSettings() {
            if (File.Exists(SettingFile)) {
                XmlSerializer serializer = new XmlSerializer(typeof(ModSettings));
                using (FileStream fileStream = new FileStream(SettingFile, FileMode.Open)) {
                    Instance = (ModSettings) serializer.Deserialize(fileStream);
                }
            } else {
                Instance = new ModSettings();
                SaveSettings();
            }
        }

        public static void SaveSettings() {
            XmlSerializer serializer = new XmlSerializer(typeof(ModSettings));
            StreamWriter streamWriter = new StreamWriter(SettingFile);
            serializer.Serialize(streamWriter, Instance);
            streamWriter.Close();
        }

    }
}
