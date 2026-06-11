using System;
using System.Collections.Generic;
using System.Text;
using StatisticsTracker.Components;
using EntityComponent;
using JumpKing.Mods;
using JumpKing.Player;
using StatisticsTracker.Entities;

namespace StatisticsTracker
{
    [JumpKingMod("Administrator.StatisticsTracker")]
    public static class ModEntry
	{

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad() {}

        /// <summary>
        /// Called by Jump King when the level unloads
        /// </summary>
        [OnLevelUnload]
        public static void OnLevelUnload() {}

        /// <summary>
        /// Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        public static void OnLevelStart() {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            player?.AddComponents(new StatTrackerComp());
            new TextOverlay();
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() {}
    }
}
