using System;
using System.Collections.Generic;
using EntityComponent;
using JumpKing;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using StatisticsTracker.Models;
using StatisticsTracker.Utility;

namespace StatisticsTracker.Components {
    public class StatTrackerComp : Component {

        private static int CurrentScreen => Camera.CurrentScreen;
        private static Dictionary<int, int> CurrentLevelAttempts => ModEntry.LevelAttempts;
        private static Dictionary<int, int> CurrentLevelSessionAttempts => ModEntry.LevelSessionAttempts;
        private static LevelStats CurrentLevelStats => ModEntry.CurrentLevelStats;

        private int LastScreen = CurrentScreen;

        private BodyComp Body;

        protected override void OnStart() {
            Body = GetComponent<BodyComp>();
        }

        protected override void Update(float p_delta) {
            if (CurrentScreen == LastScreen) return;

            if (!Body.IsOnGround) return;

            if (CurrentLevelAttempts.ContainsKey(CurrentScreen)) {
                CurrentLevelAttempts[CurrentScreen]++;
            } else {
                CurrentLevelAttempts[CurrentScreen] = 1;
            }

            if (CurrentLevelSessionAttempts.ContainsKey(CurrentScreen)) {
                CurrentLevelSessionAttempts[CurrentScreen]++;
            } else {
                CurrentLevelSessionAttempts[CurrentScreen] = 1;
            }
            
            // If the screen hasn't been visited before, Add it with the first arrival time
            if (!CurrentLevelStats.LevelScreens.ContainsKey(CurrentScreen)) {
                CurrentLevelStats.LevelScreens[CurrentScreen] = new Screen() {FirstArrivalTime = SaveData.GetTimePlayed()};
            }

            LastScreen = Camera.CurrentScreen;
        }
    }
}