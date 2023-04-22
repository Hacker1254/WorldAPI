using System;
using VRC.UI.Elements;
using WorldAPI.ButtonAPI.Extras;
using UnityEngine;
using Object = UnityEngine.Object;
using static WorldAPI.APIBase;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Wing.Buttons;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.WIng.Buttons;

namespace WorldAPI.ButtonAPI.Wing;

public class WPage
{
    public WingSide wingSide;
    public UIPage page;
    public GameObject gameObject;
    public Transform transform;
    public Transform menuContents;

    public WPage(string pageName, WingSide WingSide) {
        if (!APIBase.IsReady()) throw new Exception();

        wingSide = WingSide;

        gameObject = Object.Instantiate(APIBase.WPageTemplate, APIBase.WPageTemplate.transform.parent);
        transform = gameObject.transform;
        gameObject.name = pageName + Guid.NewGuid();
        page = gameObject.GetComponent<UIPage>();
        page.field_Public_String_0 = pageName + Guid.NewGuid();
        page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
        page.field_Private_List_1_UIPage_0.Add(page);
        menuContents = gameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
        menuContents.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = true;
        menuContents.DestroyChildren();
        var ttext = gameObject.transform.Find("WngHeader_H1/LeftItemContainer/Text_QM_H2 (1)").GetComponent<TextMeshProUGUIEx>(); 
        ttext.text = pageName + Guid.NewGuid();
        ttext.richText = true;
        if (wingSide == WingSide.Left) QMUtils.GetWngLMenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(pageName + Guid.NewGuid(), page);
        else QMUtils.GetWngRMenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(pageName + Guid.NewGuid(), page);

        page.transform.Find("ScrollRect").GetComponent<VRC.UI.Elements.Controls.ScrollRectEx>().field_Public_Boolean_0 = true;
        page.GetComponent<Canvas>().enabled = true;
        page.GetComponent<CanvasGroup>().enabled = true;
        page.GetComponent<UIPage>().enabled = true;
        page.GetComponent<GraphicRaycaster>().enabled = true;
        gameObject.SetActive(false);  // Set off as enabling the comps above makes the page visable, but we dont want that so we set it off, once we go into and out of the menu it syncs
    }

    public void OpenMenu() {
        gameObject.SetActive(true);
        if (wingSide == WingSide.Left)
            QMUtils.GetWngLMenuStateControllerInstance.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(page.field_Public_String_0);
        else QMUtils.GetWngRMenuStateControllerInstance.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(page.field_Public_String_0);

    }
    public WButton AddButton(string buttonName, Action listener, string toolTip, Sprite Icon = null, bool SubMenu = false, string Header = "") => new(this, buttonName, listener, toolTip, Icon, SubMenu, Header);
    public WToggle AddToggle(string text, Action<bool> listener, bool DefaultState = false,
            string OffTooltip = null, string OnToolTip = null,
            Sprite onimage = null, Sprite offimage = null) => new(this, text, listener, DefaultState, OffTooltip, OnToolTip, onimage, offimage);

}