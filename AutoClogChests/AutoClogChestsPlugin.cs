using AutoClogChests.Patches;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AutoClogChests
{
    // TODO Review this file and update to your own requirements.

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class AutoClogChestsPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.equinox.AutoClogChests";
        private const string PluginName = "AutoClogChests";
        private const string VersionString = "1.0.0";

        private const string NumEmptySlotsKey = "NumEmptySlots";
        public static ConfigEntry<int> NumEmptySlots;

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        public static bool finishedLoadingBuilds = false;
        public static int resIDToTry = 0;
        
        public static int limestoneResID = -1;
        public static int ironOreResID = -1;

        private void Awake() {
            NumEmptySlots = Config.Bind("General", NumEmptySlotsKey, 1, new ConfigDescription("Number of empty slots at the start of the chest.", new AcceptableValueRange<int>(0, 56)));

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");
            Harmony.PatchAll();
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loaded.");
            Log = Logger;

            Harmony.CreateAndPatchAll(typeof(ChestDefinitionPatch));
            Harmony.CreateAndPatchAll(typeof(SaveStatePatch));
        }
    }
}
