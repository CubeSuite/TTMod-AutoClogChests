using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace AutoClogChests.Patches
{
    public class ChestDefinitionPatch
    {
        [HarmonyPatch(typeof(ChestDefinition), "InitInstance")]
        [HarmonyPostfix]
        private static void autoClogChest(ref ChestInstance newInstance) {
            if (AutoClogChestsPlugin.NumEmptySlots.Value == 56) return;

            if(AutoClogChestsPlugin.limestoneResID == -1) {
                findIDs();
            }

            if (AutoClogChestsPlugin.finishedLoadingBuilds) {
                int remainingCount;
                for (int i = AutoClogChestsPlugin.NumEmptySlots.Value; i < 55; i++) {
                    newInstance.GetInventory(0).AddResourcesToSlot(AutoClogChestsPlugin.limestoneResID, i, out remainingCount, 1);
                }

                newInstance.GetInventory(0).AddResourcesToSlot(AutoClogChestsPlugin.ironOreResID, 55, out remainingCount, 1);
            }
        }

        private static void findIDs() {
            foreach(ResourceInfo info in TechTreeState.instance.knownResources) {
                if(info.displayName == "Limestone") {
                    AutoClogChestsPlugin.limestoneResID = SaveState.GetIdForResInfo(info);
                }
                else if(info.displayName == "Iron Ore") {
                    AutoClogChestsPlugin.ironOreResID = SaveState.GetIdForResInfo(info);
                }
                else {
                    Debug.Log($"Resource name: {info.displayName}");
                }
            }
        }
    }
}
