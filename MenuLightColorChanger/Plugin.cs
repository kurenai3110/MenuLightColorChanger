using BS_Utils.Utilities;
using HarmonyLib;
using IPA;
using IPA.Logging;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace MenuLightColorChanger
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        private const string harmonyId = "com.github.kurenai3110.MenuLightColorChanger";
        public static IPA.Logging.Logger Logger { get; private set; }

        private Harmony harmony;

        [Init]
        public void Init(IPA.Logging.Logger logger)
        {
            Logger = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            this.ApplyHarmonyPatches();
            BSEvents.OnLoad();
            BSEvents.menuSceneLoadedFresh += MenuSceneLoadedFresh;
        }


        /// <summary>
        /// Attempts to apply all the Harmony patches in this assembly.
        /// </summary>
        public void ApplyHarmonyPatches()
        {
            try
            {
                Logger.Debug("Applying Harmony patches.");
                this.harmony = new Harmony(harmonyId);
                this.harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Logger.Critical("Error applying Harmony patches: " + ex.Message);
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Attempts to remove all the Harmony patches that used our HarmonyId.
        /// </summary>
        public void RemoveHarmonyPatches()
        {
            try
            {
                // Removes all patches with this HarmonyId
                this.harmony.UnpatchAll(harmonyId);
            }
            catch (Exception ex)
            {
                Logger.Critical("Error removing Harmony patches: " + ex.Message);
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Called when the a scene's assets are loaded.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="sceneMode"></param>
        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "HealthWarning")
            {
                MenuLightColorChanger.ChangeColors();
            }
        }

        private void MenuSceneLoadedFresh()
        {
            MenuLightColorChanger.InitResources();
            MenuLightColorChanger.ChangeColors();
        }
    }
}
