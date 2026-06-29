using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BehaviorTree;
using JumpKing.MiscSystems.LocationText;
using StatisticsTracker.Models;
using StatisticsTracker.Utility;

namespace StatisticsTracker.Menu {

    public class ExportFirstArrivalTimeSummary : IBTnode {

        protected override BTresult MyRun(TickData p_data) {
            return WriteSummary();
        }

        public static BTresult WriteSummary() {
            Location[] locations = InternalAccessor.LocationTextManager.Settings.locations;

            // Filter for visited locations
            locations = locations.Where(location => ModEntry.CurrentLevelStats.LevelScreens.Any(pair => pair.Key >= location.start - 1 && pair.Key < location.end)).ToArray();

            // If none of the locations were visited, do nothing
            if (locations.Length == 0) {
                return BTresult.Success;
            }

            StringBuilder builder = new StringBuilder();

            foreach (Location location in locations) {
                builder.Append($"=== {location.name} ===");
                builder.AppendLine();
                builder.AppendLine();

                for (int i = location.start - 1; i < location.end; i++) {
                    KeyValuePair<int, Screen> screen = ModEntry.CurrentLevelStats.LevelScreens.FirstOrDefault(s => s.Key == i);

                    if (screen.Value == null) continue;

                    builder.AppendLine($"Screen {screen.Key - location.start + 2}: {Util.FormatTime(screen.Value.FirstArrivalTime)}");
                }

                builder.AppendLine();
            }

            string path = Path.Combine(ModEntry.RootFolder, $"Summary_{ModEntry.GetLevelNameSanitized()}.txt");
            File.WriteAllText(path, builder.ToString());

            return BTresult.Success;
        }
    }

}