using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MenuLightColorChanger
{
    internal static class Utils
    {
        public static Color makeLight(this Color color, float percentage)
        {
            Color new_color = color;

            new_color.r = 1f - (1f - color.r) * (1f - percentage);
            new_color.g = 1f - (1f - color.g) * (1f - percentage);
            new_color.b = 1f - (1f - color.b) * (1f - percentage);

            return new_color;
        }

        public static Color makeDark(this Color color, float percentage)
        {
            Color new_color = color;

            new_color.r = (color.r) * (1f - percentage);
            new_color.g = (color.g) * (1f - percentage);
            new_color.b = (color.b) * (1f - percentage);

            return new_color;
        }


        public static void SetAnimationCurveColor(AnimationClip ac, Color color, Type type, string colorPropertyname, string relativePath)
        {
            AnimationCurve curve;

            //curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, color.a);
            //ac.SetCurve("BG", typeof(UnityEngine.UI.Image), "m_Color.a", curve);
            //ac.SetCurve("Wrapper/BG", typeof(UnityEngine.UI.Image), "m_Color.a", curve);

            curve = AnimationCurve.Linear(0.0f, color.r, 0.0f, color.r);
            ac.SetCurve(relativePath, type, colorPropertyname + ".r", curve);
            //ac.SetCurve("Wrapper/BG", typeof(UnityEngine.UI.Image), "m_Color.r", curve);

            curve = AnimationCurve.Linear(0.0f, color.g, 0.0f, color.g);
            ac.SetCurve(relativePath, type, colorPropertyname + ".g", curve);
            //ac.SetCurve("Wrapper/BG", typeof(UnityEngine.UI.Image), "m_Color.g", curve);

            curve = AnimationCurve.Linear(0.0f, color.b, 0.0f, color.b);
            ac.SetCurve(relativePath, type, colorPropertyname + ".b", curve);
            //ac.SetCurve("Wrapper/BG", typeof(UnityEngine.UI.Image), "m_Color.b", curve);
        }

        public static void SetAnimationCurveAlpha(AnimationClip ac, float alpha, Type type, string colorPropertyname, string relativePath)
        {
            AnimationCurve curve;

            curve = AnimationCurve.Linear(0.0f, alpha, 0.0f, alpha);
            ac.SetCurve(relativePath, type, colorPropertyname + ".a", curve);

            //ac.SetCurve("Wrapper/BG", typeof(UnityEngine.UI.Image), "m_Color.a", curve);
        }

        public static List<Transform> GetAllChildren(this Transform parent)
        {
            List<Transform> allChildren = new List<Transform>();

            foreach (Transform t in parent)
            {
                allChildren.Add(t);
                allChildren.AddRange(t.GetAllChildren());
            }

            return allChildren;
        }
    }
}
