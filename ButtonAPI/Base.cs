using System;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.MM.Carousel.Items;
using WorldAPI.ButtonAPI.WIng.Buttons;
using WorldLoader.Mods;

namespace WorldAPI
{

    internal static class Logs {
        internal static UnityMod Mod;

        internal static void Log(string message, ConsoleColor color = ConsoleColor.White) {
            if (Mod == null) {
                Mod = new UnityMod {
                    Name = "WCAPI",
                };
            }
            Mod.Log(message);
        }

        internal static void Debug(string message) => Log("[Debug] " + message, ConsoleColor.DarkGray);

        internal static void Error(Exception e, string message) => Error(message, e);

        internal static void Error(string message, Exception e = null) {
            if (Mod == null) {
                Mod = new UnityMod {
                    Name = "WCAPI",
                };
            }
            Mod.Error(message, e);
        }
    }

    public class APIBase
    {
        public class Events {
            public static Action<VRCToggle, bool> onVRCToggleValChange = new Action<VRCToggle, bool>((er, str) => { });
            public static Action<CToggle, bool> onCToggleValChange = new Action<CToggle, bool>((er, str) => { });
            public static Action<WToggle, bool> onWToggleValChange = new Action<WToggle, bool>((er, str) => { });
        }
        /// <summary>
        ///  Set this if u want to override what happens when a button/ tgl throws an error
        /// </summary>
        public static Action<Exception, string> ErrorCallBack { get; set; } = new Action<Exception, string>((er, str) => {
            Logs.Error($"The ButtonAPI had an Error At {str}", er);
        });
        public static string autoColorHex { get; set; } = null;
        public static Transform LastButtonParent;
        private static bool HasChecked;

        public static Sprite DefaultButtonSprite; // Override these if u want custom ones
        public static Sprite OffSprite, OnSprite; // Override these if u want custom ones
        public static GameObject QuickMenu, ColpButtonGrp, ButtonGrp, ButtonGrpText;
        public static Transform Button, Toggle, Tab, MenuTab, Slider;


        public static GameObject MMM, MMMpageTemplate, MMMCarouselPageTemplate, MMMCarouselButtonTemplate, MMMTabTemplate;
        public static GameObject MMCTgl, MMBtnGRP, MMSlider;

        public static GameObject WPageTemplate, WBtnTemplate, WBtnPgTemplate, WLDefMenu, WRDefMenu;
   
        public static bool IsReady()
        {
            if (HasChecked) return true;
            if ((QuickMenu = GameObject.Find("Canvas_QuickMenu(Clone)")) == null) {
                Logs.Error("QuickMenu Is Null!");
                return false;
            }
            if ((MMM = GameObject.Find("Canvas_MainMenu(Clone)")) == null) {
                Logs.Error("MainMenu Is Null!"); 
                return false;
            }
            if ((Button = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn")) == null) {
                Logs.Error("Button Is Null!");
                return false;
            }
            if ((Toggle = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_Debug_Row_1/Button_PinFPSAndPing")) == null) {
                Logs.Error("Toggle Is Null!");
                return false;
            }
            if ((Slider = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_AudioSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Audio/Audio/VolumeSlider_World")) == null) {
                Logs.Error("Slider Is Null!");
                return false;
            }
            if ((MenuTab = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard")) == null) {
                Logs.Error("MenuTab Is Null!");
                return false;
            }
            if ((OffSprite = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvInteractions/Button_ToggleSelfInteract/Icon_Off").GetComponent<Image>().sprite) == null) {
                Logs.Error("OffSprite Is Null!");
                return false;
            }
            if ((OnSprite = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Controls_ColorFilters/EnableFilters/Toggle_ColorFiltersEnable/ButtonElement_CheckBox/Checkmark").GetComponent<UIWidgets.ImageAdvanced>().sprite) == null) {
                Logs.Error("OnSprite Is Null!");
                return false;
            }
            if ((Tab = QuickMenu.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools")) == null) {
                Logs.Error("Tab Is Null!");
                return false;
            }
            if ((ButtonGrp = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions").gameObject) == null) {
                Logs.Error("ButtonGrp Is Null!");
                return false;
            }
            if ((ButtonGrpText = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions").gameObject) == null) {
                Logs.Error("ButtonGrpText Is Null!");
                return false;
            }
            if ((ColpButtonGrp = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/QM_Foldout_Accessibility").gameObject) == null) {
                Logs.Error("ColpButtonGrp Is Null!");
                return false;
            }
            if ((WPageTemplate = QuickMenu.transform.Find("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/Profile").gameObject) == null) {
                Logs.Error("WPageTemplate Is Null!");
                return false;
            }
            //if ((WBtnTemplate = QuickMenu.transform.Find("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Profile").gameObject) == null) {
            //    Logs.Error("WBtnTemplate Is Null!");
            //    return false;
            //}
            //if ((WBtnPgTemplate = QuickMenu.transform.Find("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/Profile/ScrollRect/Viewport/VerticalLayoutGroup/InfoPanel/Status").gameObject) == null) {
            //    Logs.Error("WBtnPgTemplate Is Null!");
            //    return false;
            //}
            //if (WBtnPgTemplate.GetComponent<VRC.UI.Elements.Analytics.AnalyticsController>() != null)
            //    GameObject.Destroy(WBtnPgTemplate.GetComponent<VRC.UI.Elements.Analytics.AnalyticsController>()); // Fuck Analytics
            //if ((WRDefMenu = QuickMenu.transform.Find("CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup").gameObject) == null) {
            //    Logs.Error("WRDefMenu Is Null!");
            //    return false;
            //}
            //if ((WLDefMenu = QuickMenu.transform.Find("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup").gameObject) == null) {
            //    Logs.Error("WLDefMenu Is Null!");
            //    return false;
            //}
            //if ((MMMpageTemplate = MMM.transform.Find("Container/MMParent/Menu_MM_Profile").gameObject) == null) {
            //    Logs.Error("Main Menu Template Is Null!");
            //    return false;
            //}
            //if ((MMMCarouselPageTemplate = MMM.transform.Find("Container/MMParent/Menu_Settings").gameObject) == null) {
            //    Logs.Error("Menu_Settings Is Null!");
            //    return false;
            //}
            //if ((MMMCarouselButtonTemplate = MMM.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Audio & Voice").gameObject) == null) {
            //    Logs.Error("MMMCarouselButtonTemplate Is Null!");
            //    return false;
            //}
            //if ((MMCTgl = MMM.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/Comfort&Safety/Comfort/Settings_Panel_1/VerticalLayoutGroup/AFKDetection").gameObject) == null) {
            //    Logs.Error("MMCTgl Is Null!");
            //    return false;
            //}
            //if ((MMBtnGRP = MMM.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/Comfort&Safety/Safety").gameObject) == null) {
            //    Logs.Error("MMBtnGRP Is Null!");
            //    return false;
            //}
            //if ((MMSlider = MMM.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/Audio&Voice/Microphone/Settings_Panel_1/VerticalLayoutGroup/MicOutputVolume").gameObject) == null) {
            //    Logs.Error("MMSlider Is Null!");
            //    return false;
            //}
            //if ((MMMTabTemplate = MMM.transform.Find("Container/PageButtons/HorizontalLayoutGroup/Page_Profile").gameObject) == null) {
            //    Logs.Error("Main Menu Tab Is Null!");
            //    return false;
            //}
            HasChecked = true;
            return true;
        }

        internal static void SafelyInvolk(Action action, string name) { 
            try {
                action.Invoke();
            } catch (Exception e) {
                ErrorCallBack.Invoke(e, name);
            }
        }

        internal static void SafelyInvolk(bool state, Action<bool> action, string name) { 
            try {
                action.Invoke(state);
            } catch (Exception e) {
                ErrorCallBack.Invoke(e, name);
            }
        }

        public enum WingSide
        {
            Left,
            Right
        }
    }
}
