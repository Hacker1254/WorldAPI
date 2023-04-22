using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.MM;

public class MMCarousel
{
    public UIPage page { get; private set; }
    public Transform menuContents { get; private set; }
    public Transform barContents { get; private set; }
    public int Pageint { get; private set; }
    public GameObject gameObject { get; private set; }
    public Transform transform { get; private set; }
    public Image ImageComp { get; private set; }
    public Transform LogOutBtn { get; private set; }
    public Transform ExitBtn { get; private set; }
    public string MenuName { get; internal set; }

    public MMCarousel(string menuName, string HeaderText, Sprite Icon = null)
    {
        if (!APIBase.IsReady()) throw new Exception();
        if (APIBase.MMMpageTemplate == null) {
            Logs.Error("Fatal Error: MMMpageTemplate Is Null!");
            return;
        }
        var region = 0; // Idea from https://github.com/PlagueVRC/PlagueButtonAPI/blob/new-ui/PlagueButtonAPI/PlagueButtonAPI/Pages/MenuPage.cs

        try
        {
            gameObject = Object.Instantiate(APIBase.MMMCarouselPageTemplate, APIBase.MMMCarouselPageTemplate.transform.parent);
            transform = gameObject.transform;
            gameObject.name = menuName;
            MenuName = menuName;
            page = gameObject.GetComponent<UIPage>();
            var GuidName = menuName + Guid.NewGuid();
            page.field_Public_String_0 = GuidName;
            page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            page.field_Private_List_1_UIPage_0.Add(page);
            region++;
            QMUtils.GetMainMenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(GuidName, page);
            region++;

            var list = QMUtils.GetMainMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0.ToList();
            list.Add(page);
            QMUtils.GetMainMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0 = list.ToArray();
            Pageint = QMUtils.GetMainMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0.Count;
            page.GetComponent<Canvas>().enabled = true;
            page.GetComponent<CanvasGroup>().enabled = true;
            page.GetComponent<UIPage>().enabled = true;
            page.GetComponent<GraphicRaycaster>().enabled = true;
            var scrolNav = gameObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation");
            scrolNav.GetComponent<VRC.UI.Elements.Controls.ScrollRectEx>().field_Public_Boolean_0 = true;
            scrolNav.transform.Find("Viewport/VerticalLayoutGroup").GetComponent<Canvas>().enabled = true;
            scrolNav.transform.Find("Viewport/VerticalLayoutGroup").GetComponent<GraphicRaycaster>().enabled = true;
            scrolNav.transform.Find("ScrollRect_Content").GetComponent<VRC.UI.Elements.Controls.ScrollRectEx>().field_Public_Boolean_0 = true;
            //scrolNav.transform.Find("ScrollRect_Content/Viewport").GetComponent<RectMask2DEx>().field_Public_Boolean_0 = true; // Fixes the items falling off of the Menu
            //scrolNav.transform.Find("Viewport").GetComponent<RectMask2DEx>().field_Public_Boolean_0 = true; // Fixes the items falling off of the Menu

            ImageComp = scrolNav.transform.Find("Viewport/VerticalLayoutGroup/DynamicSidePanel_Header/Icon").GetComponent<Image>();
            ImageComp.transform.localScale = new(0.8f, 0.8f, 0.8f);
            ImageComp.transform.localPosition = new(88.5f, -50, 0);

            if (Icon != null) {
                ImageComp.sprite = Icon;
                ImageComp.overrideSprite = Icon;
            } else ImageComp.gameObject.SetActive(false);

            var textt = scrolNav.transform.Find("Viewport/VerticalLayoutGroup/DynamicSidePanel_Header/Text_Name").GetComponent<TextMeshProUGUI>();
            textt.text = HeaderText;
            textt.overflowMode = TextOverflowModes.Overflow;
            textt.autoSizeTextContainer = true;
            textt.enableAutoSizing = true;
            textt.richText = true;


            menuContents = scrolNav.transform.Find("ScrollRect_Content/Viewport/VerticalLayoutGroup");
            menuContents.DestroyChildren();
            barContents = scrolNav.transform.Find("Viewport/VerticalLayoutGroup");
            barContents.DestroyChildren(a => a.name != "DynamicSidePanel_Header");

            LogOutBtn = scrolNav.transform.Find("Viewport/VerticalLayoutGroup/DynamicSidePanel_Header/Button_Logout");
            GameObject.Destroy(LogOutBtn.GetComponent<LogoutButton>());
            ExitBtn = scrolNav.transform.Find("Viewport/VerticalLayoutGroup/DynamicSidePanel_Header/Button_Exit");
            LogOutBtn.gameObject.active = false;
            ExitBtn.gameObject.active = false;
            //var dyncmHeadr = page.gameObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/DynamicSidePanel_Header");
            //dyncmHeadr.GetComponent<LayoutElement>().minHeight = 100;
            //dyncmHeadr.transform.Find("Separator").localPosition = new(168, -100, 0);

            gameObject.SetActive(false); // Set it off, as we had to enable the page comps, that shows the page, and it will be overlapping - but the controller fixes it when u select and deselect the menu
        }
        catch (Exception ex)
        {
            throw new Exception("Exception Caught When Making Page At Region: " + region + "\n\n" + ex);
        }
    }

    public void SetExtraButtons(string text1, Action listener1, string toolTip1, string text2, Action listener2, string toolTip2, Sprite sprite1 = null, Sprite sprite2 = null) {
        if (sprite1 == null) sprite1 = APIBase.DefaultButtonSprite;
        if (sprite2 == null) sprite2 = APIBase.DefaultButtonSprite;

        var logOutBRN = LogOutBtn.GetComponent<Button>();
        logOutBRN.onClick = new();
        logOutBRN.onClick.AddListener(listener1);
        foreach (var obj in LogOutBtn.GetComponentsInChildren<TextMeshProUGUI>()) {
            obj.text = text1;
            obj.richText = true;
        }
        foreach (var obj in LogOutBtn.GetComponentsInChildren<Image>()) {
            if (obj.name != "Icon") continue;
            obj.sprite = sprite1; 
        }


        var ExitBRN = ExitBtn.GetComponent<Button>();
        ExitBRN.onClick = new();
        ExitBRN.onClick.AddListener(listener1);
        foreach (var obj in ExitBRN.GetComponentsInChildren<TextMeshProUGUI>()) {
            obj.text = text2;
            obj.richText = true;
        }
        foreach (var obj in ExitBRN.GetComponentsInChildren<Image>()) {
            if (obj.name != "Icon") continue;
            obj.sprite = sprite2;
        }

        LogOutBtn.gameObject.active = true;
        ExitBtn.gameObject.active = true;
        //var dyncmHeadr = page.gameObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/DynamicSidePanel_Header");
        //dyncmHeadr.GetComponent<LayoutElement>().minHeight = 290;
        //dyncmHeadr.transform.Find("Separator").localPosition = new(168, -290, 0);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        QMUtils.GetMainMenuStateControllerInstance.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(page.field_Public_String_0, null, true, UIPage.EnumNPublicSealedvaNoLeRiBoIn6vUnique.Right);
    }
}