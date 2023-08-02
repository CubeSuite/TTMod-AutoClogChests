using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace AutoClogChests.Patches
{
    public class SaveStatePatch
    {
        [HarmonyPatch(typeof(SaveState), "GenerateBuildingsOnLoad")]
        [HarmonyPostfix]
        private static void setFinishedLoadingBuildings() {
            AutoClogChestsPlugin.finishedLoadingBuilds = true;
        }
    }
}
