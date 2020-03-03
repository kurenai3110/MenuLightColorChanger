using BS_Utils.Utilities;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using VRUIControls;

namespace MenuLightColorChanger.HarmonyPatches
{
    [HarmonyPatch(typeof(VRPointer))]
    [HarmonyPatch("CreateLaserPointerAndLaserHit")]
    class CreateLaserPointerPatch
    {
        static void Prefix(ref Transform ____laserPointerPrefab, ref Transform ____cursorPrefab, ref bool ____lastControllerUsedWasRight)
        {
            var css = MenuLightColorChanger.colorSchemesSettings;
            var colorManager = MenuLightColorChanger.colorManager;
            var cs = css.overrideDefaultColors ? css.GetSelectedColorScheme() : colorManager.GetField<ColorSchemeSO>("_defaultColorScheme").colorScheme;

            var laser = ____laserPointerPrefab.GetComponent<MeshRenderer>();
            var cursor = ____cursorPrefab.GetComponent<MeshRenderer>();

            if (____lastControllerUsedWasRight)
            {
                laser.material.color = cs.saberBColor.ColorWithAlpha(0.5f);
                cursor.material.color = cs.saberBColor.ColorWithAlpha(0.5f);
            }
            else
            {
                laser.material.color = cs.saberAColor.ColorWithAlpha(0.5f);
                cursor.material.color = cs.saberAColor.ColorWithAlpha(0.5f);
            }

            Logger.log.Info("applied pointer colors");
        }
    }

    [HarmonyPatch(typeof(VRPointer))]
    [HarmonyPatch("RefreshLaserPointerAndLaserHit")]
    [HarmonyPatch(new Type[] { typeof(PointerEventData) })]
    class RefreshLaserPointerPatch
    {
        static void Prefix(ref Transform ____laserPointerPrefab, ref Transform ____cursorPrefab, ref bool ____lastControllerUsedWasRight)
        {
            var css = MenuLightColorChanger.colorSchemesSettings;
            var colorManager = MenuLightColorChanger.colorManager;
            var cs = css.overrideDefaultColors ? css.GetSelectedColorScheme() : colorManager.GetField<ColorSchemeSO>("_defaultColorScheme").colorScheme;

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

            Logger.log.Info("applied pointer colors");
        }
    }
}
