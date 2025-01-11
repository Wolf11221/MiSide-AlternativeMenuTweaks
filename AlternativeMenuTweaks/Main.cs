using HarmonyLib;
using UnityEngine;

namespace AlternativeMenuTweaks;

public class Main : MonoBehaviour
{
    [HarmonyPatch(typeof(Menu), "Start")]
    public class MenuStartPatch
    {
        private static void Postfix(Menu __instance)
        {
            if (GlobalGame.gameEndingMenu) return;

            if (Plugin.CustomChanceEnabled.Value)
            {
                int randomValue = Random.Range(Plugin.CustomMinInclusive.Value, Plugin.CustomMaxExclusive.Value);

                if (Plugin.CustomMinInclusive.Value < Plugin.CustomMaxExclusive.Value - 1)
                {
                    if (randomValue == Plugin.CustomEqualsTo.Value)
                    {
                        __instance.Alternative();

                        __instance.audioMusicMenu.clip = __instance.alternativeMusic;
                        __instance.audioMusicMenu.Play();
                    }
                }
                else
                {
                    Plugin.Log.LogError(
                        $"Invalid range for CustomMinInclusive({Plugin.CustomMinInclusive.Value}) and CustomMaxExclusive({Plugin.CustomMaxExclusive.Value})");
                }
            }
            else
            {
                __instance.Alternative();

                __instance.audioMusicMenu.clip = __instance.alternativeMusic;
                __instance.audioMusicMenu.Play();
            }
        }
    }

    [HarmonyPatch(typeof(Menu), "Update")]
    public class MenuUpdatePatch
    {
        private static float _newDatamoshTime = 0.0f;

        private static void Prefix(Menu __instance)
        {
            if (!__instance.alternative || !Plugin.CustomDatamoshEnabled.Value) return;

            __instance.timeDatamoshGlitch = 1.0f;

            _newDatamoshTime -= Time.deltaTime;
            if (_newDatamoshTime < 0.0f)
            {
                _newDatamoshTime = Random.Range(Time.deltaTime, Plugin.CustomDatamoshMaxExclusive.Value);
                __instance.datamosh.Glitch();
            }
        }
    }
}