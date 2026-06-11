using System;
using System.Collections.Generic;
using EntityComponent;
using JumpKing;
using JumpKing.GameManager.MultiEnding;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;

namespace StatisticsTracker.Components {
    public class StatTrackerComp : Component {

        private static int LastScreen = -1;

        private BodyComp Body;

        protected override void OnStart() {
            Body = GetComponent<BodyComp>();
        }

        protected override void Update(float p_delta) {
            if (LastScreen == -1) {
                LastScreen = Camera.CurrentScreen;
                return;
            }

            if (Camera.CurrentScreen != LastScreen && Body.IsOnGround) {
                if (ModEntry.LevelAttempts.ContainsKey(Camera.CurrentScreen)) {
                    ModEntry.LevelAttempts[Camera.CurrentScreen]++;
                } else {
                    ModEntry.LevelAttempts[Camera.CurrentScreen] = 1;
                }

                LastScreen = Camera.CurrentScreen;
            }

        }
    }
}