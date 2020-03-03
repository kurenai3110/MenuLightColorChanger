using Harmony;
using SongBrowser.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuLightColorChanger.HarmonyPatches
{
    [HarmonyPatch(typeof(SongBrowserUI),
    "CreateUI",
    new Type[] { typeof(MainMenuViewController.MenuButton) })]
    class SongBrowerCreateUIPatch
    {
        public static event Action SongBrowerUICreated;
        private static bool uiCreated = false;

        static void Postfix(ref bool ____uiCreated)
        {
            if (____uiCreated && uiCreated == false)
            {
                SongBrowerUICreated();
                uiCreated = true;
            }
        }
    }
}
