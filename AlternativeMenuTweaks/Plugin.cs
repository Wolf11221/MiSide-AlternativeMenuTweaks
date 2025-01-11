using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace AlternativeMenuTweaks;

[BepInPlugin(GUID, MODNAME, VERSION)]
public class Plugin : BasePlugin
{
    public const string
        MODNAME = "AlternativeMenuTweaks",
        AUTHOR = "Nie",
        GUID = AUTHOR + "_" + MODNAME,
        VERSION = "1.0.0";

    private static ConfigFile _configFile =
        new(Path.Combine(Paths.ConfigPath, "AlternativeMenuTweaks.cfg"), true);

    public static ConfigEntry<bool> Enabled = _configFile.Bind("General", "Enabled", true,
        "Global plugin state. If set to false, all other tweaks will also be disabled.");

    public static ConfigEntry<bool> CustomChanceEnabled = _configFile.Bind("Tweaks.AltMenu", "Enabled", false,
        "Enables custom probability for the alternative menu. If set to false, the alternative menu will always have a 100% chance to appear.\n" +
        "If a random value (from 0 to 450 by default in game) is equal to (100) alternative menu will be enabled.");

    public static ConfigEntry<int> CustomMinInclusive =
        _configFile.Bind("Tweaks.AltMenu", "CustomMinInclusiveValue", 0, "");

    public static ConfigEntry<int> CustomMaxExclusive =
        _configFile.Bind("Tweaks.AltMenu", "CustomMaxExclusiveValue", 450, "");

    public static ConfigEntry<int> CustomEqualsTo = _configFile.Bind("Tweaks.AltMenu", "CustomEqualsToValue", 100, "");

    public static ConfigEntry<bool> CustomDatamoshEnabled = _configFile.Bind("Tweaks.Datamosh", "Enabled", false,
        "Enables custom probability for the datamosh effect when the alternative menu is active.");

    public static ConfigEntry<float> CustomDatamoshMaxExclusive = _configFile.Bind("Tweaks.Datamosh",
        "CustomMaxExclusiveValue", 12.0f, "");

    public override void Load()
    {
        Log = base.Log;

        if (Enabled.Value)
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }
    }

    public static ManualLogSource Log;
}