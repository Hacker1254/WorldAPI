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
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.Extras
{
    public static class QMUtils
    {
        internal static MenuStateController WLcontroller;
        internal static MenuStateController WRcontroller;

        public static VRC.UI.Elements.QuickMenu GetQuickMenuInstance => Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().FirstOrDefault();
        public static MenuStateController GetMenuStateControllerInstance => GetQuickMenuInstance.GetComponent<MenuStateController>();
        public static MainMenu GetMainMenuInstance => Resources.FindObjectsOfTypeAll<MainMenu>().FirstOrDefault(x => x.name == "Canvas_MainMenu(Clone)");
        public static MenuStateController GetMainMenuStateControllerInstance => GetMainMenuInstance.GetComponent<MenuStateController>();

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

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
                return gameObject.AddComponent<T>();

            return component;
        }

        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            T component = transform.GetComponent<T>();
            if (component == null)
                return transform.gameObject.AddComponent<T>();

            return component;
        }

        public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude = null)
        {
            for (var childcount = transform.childCount - 1; childcount >= 0; childcount--)
                if (exclude == null || exclude(transform.GetChild(childcount)))
                    Object.DestroyImmediate(transform.GetChild(childcount).gameObject);
        }

        public static void DestroyChildren(this GameObject gameObj, Func<Transform, bool> exclude = null) =>
            gameObj.transform.DestroyChildren(exclude);

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

        public static List<GameObject> GetChildren(this Transform transform)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++) {
                GameObject gameObject = transform.GetChild(i).gameObject;
                list.Add(gameObject);
            }
            return list;
        }

        internal static bool IsObfuscated(this string str)
        {
            foreach (var it in str)
                if (!char.IsDigit(it) && !((it >= 'a' && it <= 'z') || (it >= 'A' && it <= 'Z')) && it != '_' &&
                    it != '`' && it != '.' && it != '<' && it != '>')
                    return true;

            return false;
        }

        public static void RemoveUnknownComps(GameObject gameObject, Action<string> callBackOnDestroy = null) {
            Component[] components = gameObject.GetComponents<Component>();
            for (int D = 0; D < components.Length; D++)
            {
                var name = components[D].GetIl2CppType().Name;

                if (name.IsObfuscated() && components[D].GetIl2CppType().BaseType.Name != nameof(TMPro.TextMeshProUGUI)) {
                    Object.Destroy(components[D]);
                    callBackOnDestroy?.Invoke(name);
                }
            }
        }
    }
}
