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

        public static Dictionary<int, int> LevelAttempts { get; set; } = new Dictionary<int, int>();

        private static int LastScreen = 0;

        private static int _CurrentScreen = 0;

        public static int CurrentScreen {
            get {
                if (_CurrentScreen < 0) {
		            _CurrentScreen = 0;
                }

		        if (_CurrentScreen > LevelManager.TotalScreens) {
		        	_CurrentScreen = LevelManager.TotalScreens;
		        }

		        return _CurrentScreen;
	        }
        }

        private BodyComp Body;

        protected override void OnStart() {
            Body = GetComponent<BodyComp>();
        }

        protected override void Update(float p_delta) {
            Vector2 velocity = Body.Velocity;
            //Point center = Body.GetHitbox().Center;
//
            //if (Body.IsOnBlock(typeof(SandBlock)) || !Body.IsOnGround || velocity != Vector2.Zero) return;
//
            //int num = (int) Math.Floor((float) center.Y / 360f);
//
            //if (CurrentScreen == num) return;
            //if (velocity.Y == 0f) return;
//
            //int value = num - _CurrentScreen;
//
            //if (Math.Sign(value) != Math.Sign(0f - velocity.Y) || (velocity.Y < 0f && Math.Abs(velocity.Y) < 3f)) return;
//
	        //_CurrentScreen = num;
	        //_CurrentScreen = Math.Min(_CurrentScreen, LevelManager.TotalScreens - 1);
//
            //if (LevelAttempts.ContainsKey(CurrentScreen)) {
            //    LevelAttempts[CurrentScreen]++;
            //} else {
            //    LevelAttempts[CurrentScreen] = 1;
            //}

            if (Camera.CurrentScreen != LastScreen) {
                if (LevelAttempts.ContainsKey(Camera.CurrentScreen)) {
                    LevelAttempts[Camera.CurrentScreen]++;
                } else {
                    LevelAttempts[Camera.CurrentScreen] = 1;
                }

                LastScreen = Camera.CurrentScreen;
            }

        }
    }
}