﻿using BS_Utils.Utilities;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace MenuLightColorChanger.HarmonyPatches
{
    [HarmonyPatch(typeof(StandardLevelDetailView))]
    [HarmonyPatch("RefreshContent")]
    class PlayButtonGlowPatch
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(ref Button ____playButton)
        {
            var glow = ____playButton.gameObject.GetComponentInChildren<Image>();
            if (glow.color == new Color(0f, 0.706f, 1f, 0.784f))
            {
                var css = MenuLightColorChanger.colorSchemesSettings;
                var colorManager = MenuLightColorChanger.colorManager;
                var cs = css.overrideDefaultColors ? css.GetSelectedColorScheme() : colorManager.GetField<ColorSchemeSO>("_defaultColorScheme").colorScheme;
                Utils.AdjustColorBW(cs);

                glow.color = cs.environmentColor1.ColorWithAlpha(glow.color.a);
            }
        }
    }
}
