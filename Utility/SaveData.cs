using System;

namespace StatisticsTracker.Utility {

    public static class SaveData {

        public static double GetTimePlayed() {
            return InternalAccessor.AchievementManagerAccessor.GetCurrentStats(InternalAccessor.AchievementManagerAccessor.Instance).timeSpan.TotalSeconds;
        }

    }

}