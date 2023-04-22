using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;


namespace WorldAPI.ButtonAPI.MM;

public class MMTab
{
    public MenuTab menuTab;
    public GameObject gameObject;
    public static Action OnClick;
    public GameObject Menu;

    public MMTab(MMPage page, string toolTip = "", Sprite sprite = null) {
        if (!APIBase.IsReady()) throw new Exception();
        if (APIBase.MMMTabTemplate == null) {
            Logs.Error("Fatal Error: MMMpageTemplate Is Null!");
            return;
        }
        gameObject = Object.Instantiate(APIBase.MMMTabTemplate, APIBase.MMMTabTemplate.transform.parent);
        menuTab = gameObject.GetComponent<MenuTab>();
        menuTab.field_Private_AnalyticsController_0 = null;
        menuTab.field_Public_Int32_0 = page.Pageint - 1;
        Menu = page.gameObject;
        //menuTab.field_Public_String_0 = page.MenuName; // Dont Do this 
        if (sprite != null) {
            gameObject.transform.Find("Icon").GetComponent<UIWidgets.ImageAdvanced>().sprite = sprite;
            gameObject.transform.Find("Icon").GetComponent<UIWidgets.ImageAdvanced>().overrideSprite = sprite;
        }
        else gameObject.transform.Find("Icon").gameObject.active = false;


        gameObject.GetOrAddComponent<StyleElement>().field_Private_Selectable_0 = gameObject.GetOrAddComponent<Button>();
        gameObject.GetOrAddComponent<Button>().onClick.AddListener(new Action(() => Menu.gameObject.SetActive(true)));
    }

    public MMTab(MMCarousel page, string toolTip = "", Sprite sprite = null)
    {
        if (!APIBase.IsReady()) throw new Exception();
        if (APIBase.MMMTabTemplate == null)
        {
            Logs.Error("Fatal Error: MMMpageTemplate Is Null!");
            return;
        }
        gameObject = Object.Instantiate(APIBase.MMMTabTemplate, APIBase.MMMTabTemplate.transform.parent);
        menuTab = gameObject.GetComponent<MenuTab>();
        menuTab.field_Private_AnalyticsController_0 = null;
        menuTab.field_Public_Int32_0 = page.Pageint - 1;
        Menu = page.gameObject;
        //menuTab.field_Public_String_0 = page.MenuName; // Dont Do this 
        if (sprite != null)
        {
            gameObject.transform.Find("Icon").GetComponent<UIWidgets.ImageAdvanced>().sprite = sprite;
            gameObject.transform.Find("Icon").GetComponent<UIWidgets.ImageAdvanced>().overrideSprite = sprite;
        }
        else gameObject.transform.Find("Icon").gameObject.active = false;


        gameObject.GetOrAddComponent<StyleElement>().field_Private_Selectable_0 = gameObject.GetOrAddComponent<Button>();
        gameObject.GetOrAddComponent<Button>().onClick.AddListener(new Action(() => Menu.gameObject.SetActive(true)));
    }
}
