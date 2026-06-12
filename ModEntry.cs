using System;
using System.Collections.Generic;
using System.Text;
using StatisticsTracker.Components;
using EntityComponent;
using JumpKing.Mods;
using JumpKing.Player;
using StatisticsTracker.Entities;
using System.IO;
using System.Linq;

namespace StatisticsTracker
{
    [JumpKingMod("Administrator.StatisticsTracker")]
    public static class ModEntry {

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad() {}

        /// <summary>
        /// Called by Jump King when the level unloads
        /// </summary>
        [OnLevelUnload]
        public static void OnLevelUnload() {}

        /// <summary>
        /// Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        public static void OnLevelStart() {
            LoadLevelStats(LevelAttempts);
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            player?.AddComponents(new StatTrackerComp());
            new TextOverlay();
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() {
            SaveLevelStats(LevelAttempts);
            LevelSessionAttempts.Clear();
        }

        public static Dictionary<int, int> LevelAttempts { get; set; } = new Dictionary<int, int>();
        public static Dictionary<int, int> LevelSessionAttempts { get; set; } = new Dictionary<int, int>();

        public static readonly string LevelStatsFilePath = "Level_Stats.txt";

        public static void SaveLevelStats(Dictionary<int, int> Stats) {
            File.WriteAllLines(LevelStatsFilePath, Stats.Select(pair => $"{pair.Key}:{pair.Value}"));
        }

        public static void LoadLevelStats(Dictionary<int, int> Stats) {
            if (!File.Exists(LevelStatsFilePath)) return;

            foreach (string line in File.ReadAllLines(LevelStatsFilePath)) {
                string[] array = line.Split(':');
                int roomNumber = int.Parse(array[0]);
                int roomEntries = int.Parse(array[1]);
                Stats[roomNumber] = roomEntries;
            }
        }
    }
}
