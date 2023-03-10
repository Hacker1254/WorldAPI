using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldLoader.HookUtils;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.MM;

public class MMPage
{
    public UIPage page;
    public Transform menuContents;
    public int Pageint;
    public GameObject gameObject;
    public Transform transform;

    public string MenuName { get; internal set; }

    public MMPage(string menuName, bool root = false) {
        if (!APIBase.IsReady()) throw new Exception();
        if (APIBase.MMMpageTemplate == null) {
            Logs.Error("Fatal Error: MMMpageTemplate Is Null!");
            return;
        }
        var region = 0; // Idea from https://github.com/PlagueVRC/PlagueButtonAPI/blob/new-ui/PlagueButtonAPI/PlagueButtonAPI/Pages/MenuPage.cs

        try {
            gameObject = Object.Instantiate(APIBase.MMMpageTemplate, APIBase.MMMpageTemplate.transform.parent);
            transform = gameObject.transform;
            gameObject.name = menuName;
            gameObject.transform.Find("Loading_Display").gameObject.active = false;
            MenuName = menuName;
            var ttext = gameObject.transform.Find("Header_MM_UserName/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUIEx>();
            ttext.text = MenuName;
            ttext.richText = true;
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

            if (!root) {
                transform.Find("Header_MM_UserName/LeftItemContainer/Button_Back").gameObject.active = true;
            }
            
            page.GetComponent<UnityEngine.Canvas>().enabled = true;
            page.GetComponent<CanvasGroup>().enabled = true;
            page.GetComponent<UIPage>().enabled = true;
            page.GetComponent<GraphicRaycaster>().enabled = true;
            page.transform.Find("ScrollRect").GetComponent<VRC.UI.Elements.Controls.ScrollRectEx>().field_Public_Boolean_0 = true;
            menuContents = gameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
            menuContents.DestroyChildren();
            gameObject.SetActive(false); // Set it off, as we had to enable the page comps, that shows the page, and it will be overlapping - but the controller fixes it when u select and deselect the menu
        }
        catch (Exception ex) {
            throw new Exception("Exception Caught When Making Page At Region: " + region + "\n\n" + ex);
        }
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        QMUtils.GetMainMenuStateControllerInstance.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(page.field_Public_String_0, null, true, UIPage.EnumNPublicSealedvaNoLeRiBoIn6vUnique.Right);
    }

}
