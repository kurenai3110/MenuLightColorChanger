using BS_Utils.Utilities;
using HarmonyLib;
using UnityEngine;
using VRUIControls;

namespace MenuLightColorChanger.HarmonyPatches
{
    [HarmonyPatch(typeof(VRPointer))]
    [HarmonyPatch("CreateLaserPointerAndLaserHit")]
    class CreateLaserPointerPatch
    {
        static bool Prefix(ref Transform ____laserPointerPrefab, ref Transform ____cursorPrefab, ref bool ____lastControllerUsedWasRight)
        {
            var css = MenuLightColorChanger.colorSchemesSettings;
            var colorManager = MenuLightColorChanger.colorManager;
            var cs = css.overrideDefaultColors ? css.GetSelectedColorScheme() : colorManager.GetField<ColorSchemeSO>("_defaultColorScheme").colorScheme;
            Utils.AdjustColorBW(cs);

            var laser = ____laserPointerPrefab.GetComponent<MeshRenderer>();
            var cursor = ____cursorPrefab.GetComponent<MeshRenderer>();

            if (____lastControllerUsedWasRight)
            {
                laser.material.color = cs.saberBColor.ColorWithAlpha(laser.material.color.a);
                cursor.material.color = cs.saberBColor.ColorWithAlpha(cursor.material.color.a);
            }
            else
            {
                laser.material.color = cs.saberAColor.ColorWithAlpha(laser.material.color.a);
                cursor.material.color = cs.saberAColor.ColorWithAlpha(cursor.material.color.a);
            }

            Plugin.Logger.Info("applied pointer colors");

            return true;
        }
    }

    [HarmonyPatch(typeof(VRPointer))]
    [HarmonyPatch("Awake")]
    class AwakeLaserPointerPatch
    {
        static void Postfix(ref VRController ____leftVRController, ref bool ____lastControllerUsedWasRight)
        {
            if (____leftVRController.active == false)
            {
                ____lastControllerUsedWasRight = true;
            }
        }
    }
}
