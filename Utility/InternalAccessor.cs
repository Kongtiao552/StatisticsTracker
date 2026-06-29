using System.Reflection;
using System;
using HarmonyLib;
using JumpKing.SaveThread;
using JumpKing.MiscSystems.Achievements;
using JumpKing.MiscSystems.LocationText;

namespace StatisticsTracker.Utility {

    public static class InternalAccessor {

        public static class SaveLubeAccessor {

            public static Type SaveLube_Type = AccessTools.TypeByName("JumpKing.SaveThread.SaveLube");
            
            public static PropertyInfo GeneralSettings_Property = AccessTools.Property(SaveLube_Type, "generalSettings");
    
            public static GeneralSettings GeneralSettings => (GeneralSettings) GeneralSettings_Property.GetValue(null);

        }

        public static class AchievementManagerAccessor {

            public static Type AchievementManager_Type = AccessTools.TypeByName("JumpKing.MiscSystems.Achievements.AchievementManager");
            
            public static FieldInfo Instance_Field = AccessTools.Field(AchievementManager_Type, "instance");
    
            public static object Instance => Instance_Field.GetValue(null);

            public static MethodInfo GetCurrentStats_Method = AccessTools.Method(AchievementManager_Type, "GetCurrentStats");

            public static PlayerStats GetCurrentStats(object instance) {
                return (PlayerStats) GetCurrentStats_Method.Invoke(instance, null);
            }

        }

        public static class LocationTextManager {

            public static Type LocationTextManager_Type = AccessTools.TypeByName("JumpKing.MiscSystems.LocationText.LocationTextManager");

            public static PropertyInfo Settings_Property = LocationTextManager_Type.GetProperty("SETTINGS");

            public static LocationSettings Settings => (LocationSettings) Settings_Property.GetValue(null);

        }

    }

}