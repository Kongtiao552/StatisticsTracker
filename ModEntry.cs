using System;
using System.Collections.Generic;
using System.Text;
using StatisticsTracker.Components;
using EntityComponent;
using JumpKing.Mods;
using JumpKing.Player;
using System.IO;
using System.Linq;
using StatisticsTracker.Menu;
using BehaviorTree;
using JumpKing.PauseMenu;
using JumpKing;
using HarmonyLib;
using JumpKing.GameManager;
using Microsoft.Xna.Framework;
using JumpKing.Util;
using StatisticsTracker.Models;
using Newtonsoft.Json;
using StatisticsTracker.Utility;
using JumpKing.PauseMenu.BT;

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
            InitHarmony();
        }

        /// <summary>
        /// Called by Jump King when the level unloads
        /// </summary>
        [OnLevelUnload]
        public static void OnLevelUnload() {
            ModSettings.SaveSettings();
        }

        /// <summary>
        /// Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        public static void OnLevelStart() {
            LoadLevelStats(LevelAttempts);
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            player?.AddComponents(new StatTrackerComp());
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() {
            SaveLevelStats(LevelAttempts);
            LevelAttempts.Clear();
            LevelSessionAttempts.Clear();
        }

        #endregion

        public const string HarmonyIndentifier = "Kongtiao.StatisticsTracker.Harmony";

        public static void InitHarmony() {
            Harmony harmony = new Harmony(HarmonyIndentifier);

            harmony.Patch(typeof(GameLoop).GetMethod("DrawIngameOverlayItems"), postfix: AccessTools.Method(typeof(ModEntry), nameof(After_GameLoop_DrawIngameOverlayItems)));
        }

        private static void After_GameLoop_DrawIngameOverlayItems() {
            if (ModSettings.Instance.HideIngameOverlay) return;

            DrawAttemptTextOverlay();
        }

        #region Ingame Overlay

        private static void DrawAttemptTextOverlay() {
            Vector2 pointer = Util.TopLeft;

            if (InternalAccessor.SaveLubeAccessor.GeneralSettings.gui_use_timer) {
                pointer.Y += Game1.instance.contentManager.font.MenuFont.LineSpacing;
            }

            string attempt;
            string sessionAttempt;

            if (LevelAttempts.TryGetValue(Camera.CurrentScreen, out int att)) {
                attempt = $"#{att}";
            } else {
                attempt = "-";
            }

            if (LevelSessionAttempts.TryGetValue(Camera.CurrentScreen, out int att1)) {
                sessionAttempt = $"#{att1}";
            } else {
                sessionAttempt = "-";
            }

            Util.DrawText($"Attempt: {attempt}\nSession: {sessionAttempt}", pointer, Vector2.Zero);
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
        public const string RawLevelTimeSpentStatsFilePath = "Level_Stats_{0}_Time_Spent.json";

        public static string GetLevelName() {
            return Game1.instance.contentManager.level.Name;
        }

        public static string GetLevelNameSanitized() {
            return GetLevelName().Replace(" ", "_");
        }

        public static string GetLevelAttemptStatsFilePath() {
            return Path.Combine(RootFolder, string.Format(RawLevelStatsFilePath, GetLevelNameSanitized()));
        }

        public static string GetLevelTimeSpentStatsFilePath() {
            return Path.Combine(RootFolder, string.Format(RawLevelTimeSpentStatsFilePath, GetLevelNameSanitized()));
        }

        public static Dictionary<int, int> LevelAttempts { get; set; } = new Dictionary<int, int>();
        public static Dictionary<int, int> LevelSessionAttempts { get; set; } = new Dictionary<int, int>();

        public static LevelStats CurrentLevelStats { get; set; }

        public static void SaveLevelStats(Dictionary<int, int> stats) {
            File.WriteAllLines(GetLevelAttemptStatsFilePath(), stats.Select(pair => $"{pair.Key}:{pair.Value}"));
            File.WriteAllText(GetLevelTimeSpentStatsFilePath(), JsonConvert.SerializeObject(CurrentLevelStats, Formatting.Indented));
        }

        public static void LoadLevelStats(Dictionary<int, int> stats) {
            if (!File.Exists(GetLevelAttemptStatsFilePath())) return;

            foreach (string line in File.ReadAllLines(GetLevelAttemptStatsFilePath())) {
                string[] array = line.Split(':');
                int roomNumber = int.Parse(array[0]);
                int roomEntries = int.Parse(array[1]);
                stats[roomNumber] = roomEntries;
            }

            if (!File.Exists(GetLevelTimeSpentStatsFilePath())) {
                CurrentLevelStats = new LevelStats();
                return;
            }

            CurrentLevelStats = JsonConvert.DeserializeObject<LevelStats>(File.ReadAllText(GetLevelTimeSpentStatsFilePath()));
        }

        #endregion

        #region Menu

        [PauseMenuItemSetting]
        public static IBTSimpleMenuItem CreateHideInGameOverlayEntry(object factory, GuiFormat format) {
            return new HideInGameOverlay(ModSettings.Instance.HideIngameOverlay);
        }

        [PauseMenuItemSetting]
        public static IBTSimpleMenuItem CreateExportFirstArrivalTimeSummaryEntry(object factory, GuiFormat format) {
            return new TextButton("Export First Arrival Time Summary", new ExportFirstArrivalTimeSummary());
        }

        #endregion

    }
}
