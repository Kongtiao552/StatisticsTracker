using System;
using JumpKing.PauseMenu.BT.Actions;

namespace StatisticsTracker.Menu {

    public class HideInGameOverlay : ITextToggle {

        public HideInGameOverlay(bool p_start_value) : base(p_start_value) {
        }

        protected override string GetName() {
            return "Hide Ingame Overlay";
        }

        protected override void OnToggle() {
            ModSettings.Instance.HideIngameOverlay = !ModSettings.Instance.HideIngameOverlay;
        }
    }

}