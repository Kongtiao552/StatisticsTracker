using System;
using JumpKing;
using JumpKing.Util;
using Microsoft.Xna.Framework;

namespace StatisticsTracker.Utility {

    public static class Util {

        public static Vector2 TopLeft = new Vector2(12f, 8f);
        public static Vector2 TopRight = new Vector2(468f, 8f);
        public static Vector2 BottomLeft = new Vector2(12f, 352f);
        public static Vector2 BottomRight = new Vector2(468f, 352f);

        public static string FormatTime(double time) {
            TimeSpan span = TimeSpan.FromSeconds(time);
		    int hours = (int)span.TotalHours;
		    int minutes = span.Minutes;
		    int seconds = span.Seconds;
		    return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        public static void DrawText(string text, Vector2 position, Vector2 justify) {
            TextHelper.DrawString(Game1.instance.contentManager.font.MenuFont, text, position, Color.White, justify, p_is_outlined: true);
        }

    }

}