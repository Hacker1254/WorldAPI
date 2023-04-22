using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldLoader.HookUtils;

namespace WorldAPI.ButtonAPI
{
    public class Tab : ExtentedControl
    {
        public readonly MenuTab menuTab;
        public readonly Image tabIcon;
        public readonly GameObject badgeGameObject;
        public readonly TextMeshProUGUI badgeText;
        public readonly VRCPage Menu;

        public Action OnClick { get; set; }

        public Tab(VRCPage menu, string tooltip, Sprite icon = null, Transform parent = null)
        {
            if (!APIBase.IsReady()) throw new Exception();

            if (parent == null)
                parent = APIBase.Tab.parent;
            Menu = menu;

            gameObject = UnityEngine.Object.Instantiate(APIBase.Tab.gameObject, parent);
            gameObject.name = menu.menuName + "_Tab";
            gameObject.active = true;
            menuTab = gameObject.GetOrAddComponent<MenuTab>();
            menuTab.field_Private_MenuStateController_0 = QMUtils.GetMenuStateControllerInstance;
            menuTab.field_Public_String_0 = menu.menuName;
            tabIcon = gameObject.transform.Find("Icon").GetOrAddComponent<Image>();
            tabIcon.sprite = icon;
            tabIcon.overrideSprite = icon;
            badgeGameObject = gameObject.transform.GetChild(0).gameObject;
            badgeText = badgeGameObject.GetComponentInChildren<TextMeshProUGUI>();
            menuTab.gameObject.GetOrAddComponent<StyleElement>().field_Private_Selectable_0 = menuTab.gameObject.GetOrAddComponent<Button>();
            GameObject.Destroy(menuTab.gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>());
            var tooltipObj = menuTab.gameObject.GetOrAddComponent<VRC.UI.Elements.Tooltips.UiTooltip>();

            //if (!string.IsNullOrEmpty(tooltip)) tooltipObj.field_Public_String_0 = tooltip;
            //else tooltipObj.enabled = false;
            menuTab.gameObject.GetOrAddComponent<Button>().onClick.AddListener((Action)delegate {
                menuTab.gameObject.GetOrAddComponent<StyleElement>().field_Private_Selectable_0 = menuTab.gameObject.GetOrAddComponent<Button>();
                    Menu.OpenMenu();
                OnClick?.Invoke();
            });
        }

    }
}
