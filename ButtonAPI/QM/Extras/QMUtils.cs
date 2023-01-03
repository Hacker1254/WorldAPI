using Il2CppGen.Runtime;
using Il2CppGen.Runtime.XrefScans;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldLoader.HookUtils;
using WorldLoader.Il2CppGen.Internal.XrefScans;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.Extras
{
    public static class QMUtils
    {
        internal static MenuStateController WLcontroller;
        internal static MenuStateController WRcontroller;

        public static VRC.UI.Elements.QuickMenu GetQuickMenuInstance
        {
            get =>
                Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().FirstOrDefault();
        }

        public static MenuStateController GetMenuStateControllerInstance
        {
            get =>
                GetQuickMenuInstance.GetComponent<MenuStateController>();
        }

        public static VRC.UI.Elements.MainMenu GetMainMenuInstance
        {
            get =>
                Resources.FindObjectsOfTypeAll<VRC.UI.Elements.MainMenu>().FirstOrDefault(x => x.name == "Canvas_MainMenu(Clone)");
        }

        public static MenuStateController GetMainMenuStateControllerInstance
        {
            get =>
                GetMainMenuInstance.GetComponent<MenuStateController>();
        }

        public static MenuStateController GetWngLMenuStateControllerInstance
        {
            get { 
                if (WLcontroller == null) WLcontroller = GetQuickMenuInstance.transform.Find("CanvasGroup/Container/Window/Wing_Left").GetComponent<MenuStateController>();
                return WLcontroller;
            }
        }

        public static MenuStateController GetWngRMenuStateControllerInstance
        {
            get {
                if (WRcontroller == null) WRcontroller = GetQuickMenuInstance.transform.Find("CanvasGroup/Container/Window/Wing_Right").GetComponent<MenuStateController>();
                return WRcontroller;
            }
        }

        public static Color HexToColor(string hexColor)
        {
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            float r = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier) / 255f;
            float g = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier) / 255f;
            float b = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier) / 255f;
            return new Color(r, g, b);
        }
        
        public static Texture2D ConvertSprite(Sprite sprite) {
            if (sprite == null) return null;
            Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                      (int)sprite.textureRect.y,
                                                      (int)sprite.textureRect.width,
                                                      (int)sprite.textureRect.height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
}
