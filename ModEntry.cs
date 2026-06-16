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
using StatisticsTracker.Menu;
using BehaviorTree;
using JumpKing.PauseMenu;
using JumpKing;

namespace StatisticsTracker
{
    [JumpKingMod("Kongtiao.StatisticsTracker")]
    public static class ModEntry {

        #region Events

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad() {
            InitRootFolder();
            ModSettings.InitSettings();
        }

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
            ModSettings.SaveSettings();
        }

        #endregion

        #region Stats

        public const string RootFolder = "./StatsTracker/";

        public static void InitRootFolder() {
            if (!Directory.Exists(RootFolder)) {
                Directory.CreateDirectory(RootFolder);
            }
        }

        public const string RawLevelStatsFilePath = "Level_Stats_{0}.txt";

        public static string GetLevelName() {
            return Game1.instance.contentManager.level.Name;
        }

        public static string GetLevelNameSanitized() {
            return GetLevelName().Replace(" ", "_");
        }

        public static string GetLevelStatsFilePath() {
            return Path.Combine(RootFolder, string.Format(RawLevelStatsFilePath, GetLevelNameSanitized()));
        }

        public static Dictionary<int, int> LevelAttempts { get; set; } = new Dictionary<int, int>();
        public static Dictionary<int, int> LevelSessionAttempts { get; set; } = new Dictionary<int, int>();

        public static void SaveLevelStats(Dictionary<int, int> Stats) {
            File.WriteAllLines(GetLevelStatsFilePath(), Stats.Select(pair => $"{pair.Key}:{pair.Value}"));
        }

        public static void LoadLevelStats(Dictionary<int, int> Stats) {
            if (!File.Exists(GetLevelStatsFilePath())) return;

            foreach (string line in File.ReadAllLines(GetLevelStatsFilePath())) {
                string[] array = line.Split(':');
                int roomNumber = int.Parse(array[0]);
                int roomEntries = int.Parse(array[1]);
                Stats[roomNumber] = roomEntries;
            }
        }

        #endregion

        #region Menu

        [PauseMenuItemSetting]
        public static IBTSimpleMenuItem CreateHideInGameOverlayEntry(object factory, GuiFormat format) {
            return new HideInGameOverlay(ModSettings.Instance.HideIngameOverlay);
        }

        #endregion

    }
}
