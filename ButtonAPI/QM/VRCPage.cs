using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI
{
    public class VRCPage 
    {
        public UIPage page;
        public Transform menuContents;
        public TextMeshProUGUI pageTitleText;
        private bool isRoot;
        private GameObject extButtonGameObject;
        public RectMask2D menuMask;
        public string menuName;
        public Action onMenuOpen;

        public VRCPage(string pageTitle, bool root = false, bool backButton = true, bool expandButton = false, Action expandButtonAction = null, string expandButtonTooltip = "", Sprite expandButtonSprite = null, bool preserveColor = false, bool fix = true)
        {
            if (!APIBase.IsReady()) throw new Exception();
            if (APIBase.MenuTab == null) {
                Logs.Error("Fatal Error: ButtonAPI.menuPageBase Is Null!");
                return;
            }

            var region = 0; // Idea from https://github.com/PlagueVRC/PlagueButtonAPI/blob/new-ui/PlagueButtonAPI/PlagueButtonAPI/Pages/MenuPage.cs
            menuName = "WorldMenu_" + pageTitle + Guid.NewGuid();
            try
            {
                var gameObject = Object.Instantiate(APIBase.MenuTab, APIBase.MenuTab.transform.parent);
                gameObject.name = menuName;
                gameObject.transform.SetSiblingIndex(5);
                gameObject.gameObject.active = false;
                region++;
                Object.DestroyImmediate(gameObject.GetOrAddComponent<LaunchPadQMMenu>());
                region++;
                page = gameObject.gameObject.AddComponent<UIPage>();
                region++;
                page.field_Public_String_0 = menuName;
                page.field_Private_Boolean_1 = true;
                page.field_Protected_MenuStateController_0 = QMUtils.GetMenuStateControllerInstance;
                page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
                page.field_Private_List_1_UIPage_0.Add(page);
                region++;
                QMUtils.GetMenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(menuName, page);
                region++;
                if (root)
                {
                    var list = QMUtils.GetMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0.ToList();
                    list.Add(page);
                    QMUtils.GetMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0 = list.ToArray();
                }

                region++;

                gameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
                region++;
                menuContents = gameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
                menuContents.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
                region++;
                pageTitleText = gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
                region++;
                pageTitleText.fontSize = 54.7f;
                pageTitleText.text = pageTitle;
                pageTitleText.richText = true;
                isRoot = root;
                var backButtonGameObject = gameObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
                region++;
                extButtonGameObject = gameObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject;
                region++;
                backButtonGameObject.SetActive(backButton);
                region++;
                backButtonGameObject.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
                backButtonGameObject.GetComponentInChildren<Button>().onClick.AddListener((Action)delegate {
                    if (isRoot)
                        QMUtils.GetMenuStateControllerInstance.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0("QuickMenuDashboard", null, false, UIPage.EnumNPublicSealedvaNoLeRiBoIn6vUnique.Right);
                    else
                        page.Method_Protected_Virtual_New_Void_0();
                });
                region++;
                extButtonGameObject.SetActive(expandButton);
                extButtonGameObject.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
                if (expandButtonAction != null)
                {
                    extButtonGameObject.GetComponentInChildren<Button>().onClick.AddListener(expandButtonAction);
                }
                //extButtonGameObject.GetComponentInChildren<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = expandButtonTooltip;
                if (expandButtonSprite != null) {
                    extButtonGameObject.GetComponentInChildren<Image>().sprite = expandButtonSprite;
                    extButtonGameObject.GetComponentInChildren<Image>().overrideSprite = expandButtonSprite;
                    if (preserveColor) {
                        extButtonGameObject.GetComponentInChildren<Image>().color = Color.white;
                        extButtonGameObject.GetComponentInChildren<StyleElement>(true).enabled = false;
                    }
                }

                region++;
                menuMask = menuContents.parent.gameObject.GetOrAddComponent<RectMask2D>();
                region++;
                menuMask.enabled = true;
                region++;
                gameObject.transform.Find("ScrollRect").GetOrAddComponent<ScrollRect>().enabled = true;
                gameObject.transform.Find("ScrollRect").GetOrAddComponent<ScrollRect>().verticalScrollbar = gameObject.transform.Find("ScrollRect/Scrollbar").GetOrAddComponent<Scrollbar>();
                gameObject.transform.Find("ScrollRect").GetOrAddComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
                if (gameObject.childCount > 2) // Kitten is Gae
                    foreach (var child in gameObject.GetChildren()) // Kitten is Gae
                        if (child.name == "Header_H1" || child.name == "ScrollRect") continue;  // Kitten is Gae
                        else Object.Destroy(child); // Kitten is Gae

                gameObject.transform.Find("ScrollRect/Viewport").GetComponent<ObfPublicBoBoBoBoUnique>().prop_Boolean_0 = true; // Fixes the items falling off of the QM
                gameObject.transform.Find("ScrollRect").GetComponent<VRC.UI.Elements.Controls.ScrollRectEx>().field_Public_Boolean_0 = true; // Fixes the items falling off of the QM

                page.GetComponent<Canvas>().enabled = true; // Fix for Late Menu Creation
                page.GetComponent<CanvasGroup>().enabled = true; // Fix for Late Menu Creation
                page.GetComponent<UIPage>().enabled = true; // Fix for Late Menu Creation
                page.GetComponent<GraphicRaycaster>().enabled = true; // Fix for Late Menu Creation
            }
            catch (Exception ex) {
                throw new Exception("Exception Caught When Making Page At Region: " + region + "\n\n" + ex);
            }
        }
        public void SetTitle(string text) =>
            pageTitleText.text = text;

        public void AddExtButton(Action onClick, string tooltip, Sprite icon)
        {
            var obj = Object.Instantiate(extButtonGameObject, extButtonGameObject.transform.parent);
            obj.SetActive(true);
            obj.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
            obj.GetComponentInChildren<Button>().onClick.AddListener(onClick);
            obj.GetComponentInChildren<Image>().sprite = icon;
            obj.GetComponentInChildren<Image>().overrideSprite = icon;
        }


        public void OpenMenu()
        {
            page.gameObject.active = true;
            QMUtils.GetMenuStateControllerInstance.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(page.field_Public_String_0, null, false, UIPage.EnumNPublicSealedvaNoLeRiBoIn6vUnique.Right);
            onMenuOpen?.Invoke();
        }


        public void CloseMenu() =>
            page.Method_Public_Virtual_New_Void_0();

    }
}
