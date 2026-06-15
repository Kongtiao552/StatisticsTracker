using System;
using JumpKing.SaveThread;
using EntityComponent;
using Microsoft.Xna.Framework;
using System.Reflection;
using JumpKing;
using JumpKing.Util;
using StatisticsTracker.Components;
using JumpKing.Util.Tags;

namespace StatisticsTracker.Entities {
    public class TextOverlay : Entity, IForeground {

        public void ForegroundDraw() {
            if (ModSettings.Instance.HideIngameOverlay) return;

            Vector2 pointer = new Vector2(12f, 8f);

            pointer.Y += Game1.instance.contentManager.font.MenuFont.LineSpacing;

            string attempt;
            string sessionAttempt;

            if (ModEntry.LevelAttempts.TryGetValue(Camera.CurrentScreen, out int att)) {
                attempt = $"#{att}";
            } else {
                attempt = "-";
            }

            if (ModEntry.LevelSessionAttempts.TryGetValue(Camera.CurrentScreen, out int att1)) {
                sessionAttempt = $"#{att1}";
            } else {
                sessionAttempt = "-";
            }

            TextHelper.DrawString(Game1.instance.contentManager.font.MenuFont, $"Attempt: {attempt}\nSession: {sessionAttempt}", pointer, Color.White, Vector2.Zero, p_is_outlined: true);
        }

    }
}