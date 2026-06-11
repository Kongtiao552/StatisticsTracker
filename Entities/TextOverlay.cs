using System;
using JumpKing.SaveThread;
using EntityComponent;
using Microsoft.Xna.Framework;
using System.Reflection;
using JumpKing;
using JumpKing.Util;
using StatisticsTracker.Components;

namespace StatisticsTracker.Entities {
    public class TextOverlay : Entity {

        public TextOverlay() : base() {
            GoToFront();
        }

        public override void Draw() {
            Vector2 pointer = new Vector2(12f, 8f);

            pointer.Y += Game1.instance.contentManager.font.MenuFont.LineSpacing;

            int attempts = 1;

            if (ModEntry.LevelAttempts.TryGetValue(Camera.CurrentScreen, out int att)) {
                attempts = att;
            }

            TextHelper.DrawString(Game1.instance.contentManager.font.MenuFont, $"Attempt:#{attempts}", pointer, Color.White, Vector2.Zero, p_is_outlined: true);
        }

    }
}